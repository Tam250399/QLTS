CREATE OR REPLACE FUNCTION GS_UPDATE_TSOTO_45_TO_GSTEMP_DONG_BO (
    PMA_TAI_SAN          IN                   VARCHAR2,
    PLOAI_HINH_TAI_SAN   IN                   NUMBER DEFAULT 4
) RETURN NUMBER AS

    BIENDONGJSON           CLOB;
    NGUONVONJSON           CLOB;
    HIENTRANGJSON          CLOB;
    DATAJSON               CLOB;
    TRANGTHAI              NUMBER;
    BIENDONGGUID           VARCHAR2(36);
    BDOTOCUOI              BD_OTO%ROWTYPE;
    MA_NHAN_XE             VARCHAR2(50);
    MA_CHUC_DANH           VARCHAR2(50);
    GSLYDOBD               GS_MAP_LY_DO_BIEN_DONG%ROWTYPE;
    TSOTO                  TS_OTO%ROWTYPE;
    GSMAPLTS               GS_MAP_LOAI_TAI_SAN%ROWTYPE;
    NGUYENGIABANDAU        NUMBER;
    GSTEMPDONGBO           GS_TEMP_DONG_BO_DU_LIEU%ROWTYPE;
    L_OBJ                  JSON_ARRAY_T;
    HAOMONJSON             CLOB;
    L_TONG_NGUYEN_GIA_HM   NUMBER;
BEGIN

--HTSD  ?
-- ID   Tên                 Ki?u d? li?u    Nhóm    Hi?n th?    S?p x?p
-- 99	  Qu?n lý nhà n??c	  2	              	      1	          1
-- 100	H?SN-Không KD	      2	              1	      1	          2
-- 101	H?SN-Kinh doanh	    2	              1	      1	          3
-- 102	S? d?ng khác	      2	              0	      1	          6
-- 103	H?SN-LDLK	          2	              	      1	          4
-- 104	H?SN-Cho thuê	      2	              0	      1	          5
-- 184	Qu?n lý d? án	      2	              2	      1	          1
-- 185	S? d?ng khác	      2	              2	      1	          2
    SELECT
        *
    INTO GSTEMPDONGBO
    FROM
        GS_TEMP_DONG_BO_DU_LIEU
    WHERE
        MA_TAI_SAN = PMA_TAI_SAN;
--

    BEGIN
        SELECT
            *
        INTO TSOTO
        FROM
            TS_OTO
        WHERE
            MA_TAI_SAN = PMA_TAI_SAN;

    EXCEPTION
        WHEN NO_DATA_FOUND THEN --DBMS_OUTPUT.PUT_LINE('NO_DATA_FOUND ' || PMA_TAI_SAN);
            RETURN 0;
    END;
--
    --lay bien dong + nguon von + hien trang

    HIENTRANGJSON := '';
    BIENDONGJSON := '';
    NGUONVONJSON := '';
    DATAJSON := '';
    FOR R_BD_OTO IN (
        SELECT
            *
        FROM
            BD_OTO
        WHERE
            BD_OTO.MA_TAI_SAN = PMA_TAI_SAN
        ORDER BY
            BD_OTO.BD_OTO_ID DESC
    ) LOOP
        BEGIN
            SELECT
                DM_NHAN_XE.MA_NHAN_XE
            INTO MA_NHAN_XE
            FROM
                DM_NHAN_XE
            WHERE
                ROWNUM = 1
                AND DM_NHAN_XE.TEN_NHAN_XE = R_BD_OTO.NHAN_HIEU;

        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                MA_NHAN_XE := '';
        END;

        BEGIN
            SELECT
                GSCD.MA_NEW
            INTO MA_CHUC_DANH
            FROM
                DM_CHUC_DANH          LEFT
                JOIN GS_MAP_DM_CHUC_DANH   GSCD ON DM_CHUC_DANH.MA_CHUC_DANH = GSCD.MA_OLD
            WHERE
                DM_CHUC_DANH.ID = R_BD_OTO.CHUC_DANH_ID;

        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                MA_CHUC_DANH := '';
        END;

        IF R_BD_OTO.LOAI_BIEN_DONG = 4 THEN
            R_BD_OTO.MA_LY_DO := '047';
        END IF;
        BEGIN
            SELECT
                GSTG.*
            INTO GSLYDOBD
            FROM
                GS_MAP_LY_DO_BIEN_DONG GSTG
            WHERE
                ROWNUM = 1
                AND GSTG.MA_OLD = R_BD_OTO.MA_LY_DO;

        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                GSLYDOBD := NULL;
        END;

        BEGIN
            SELECT
                *
            INTO GSMAPLTS
            FROM
                GS_MAP_LOAI_TAI_SAN
            WHERE
                MA_OLD = (
                    SELECT
                        MA_LOAI_TAI_SAN
                    FROM
                        DM_LOAITAISAN
                    WHERE
                        LOAI_TAI_SAN_ID = R_BD_OTO.LOAI_TAI_SAN_ID
                )
                AND CHE_DO_HAO_MON_OLD = GSTEMPDONGBO.CHE_DO_HAO_MON_ID_OLD;

        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                GSMAPLTS := NULL;
        END;

        IF R_BD_OTO.MA_LY_DO IN (
            '050',
            '051',
            '052'
        ) THEN
            BEGIN
                SELECT
                    *
                INTO BDOTOCUOI
                FROM
                    BD_OTO
                WHERE
                    DUYET_BIEN_DONG = 2
                    AND MA_TAI_SAN = PMA_TAI_SAN
                    AND NGAY_BIEN_DONG < R_BD_OTO.NGAY_BIEN_DONG
                ORDER BY
                    NGAY_BIEN_DONG DESC
                FETCH FIRST 1 ROW ONLY;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    BDOTOCUOI := NULL;
            END;
        END IF;

        BIENDONGGUID := RAW_TO_GUID(LPAD(R_BD_OTO.BD_OTO_ID, 32, '0'));
        BIENDONGJSON := BIENDONGJSON
                        || '{"GUID":"'
                        || BIENDONGGUID
                        || '",
        "NGUYEN_GIA":'
                        || RTRIM(TO_CHAR((CASE
            WHEN R_BD_OTO.MA_LY_DO IN(
                '050', '051', '052'
            ) THEN
                ABS(NVL(R_BD_OTO.NGUYEN_GIA_NS, 0) + NVL(R_BD_OTO.NGUYEN_GIA_KHAC, 0) - NVL(BDOTOCUOI.NGUYEN_GIA_NS, 0) - NVL(BDOTOCUOI.NGUYEN_GIA_KHAC, 0))
            ELSE NVL(R_BD_OTO.NGUYEN_GIA_NS, 0) + NVL(R_BD_OTO.NGUYEN_GIA_KHAC, 0)
        END), 'FM9999999999999999999999990.99'), '.')
                        || ',
        "NGAY_BIEN_DONG":"'
                        || TO_CHAR(R_BD_OTO.NGAY_BIEN_DONG, 'yyyy-MM-dd HH:MI:SS')
                        || '",
        "DON_VI_NHAN_DIEU_CHUYEN_MA":"'
                        || R_BD_OTO.MA_DONVI_NHAN_DIEU_CHUYEN
                        || '",
        "LOAI_BIEN_DONG_ID":'
                        || R_BD_OTO.LOAI_BIEN_DONG
                        || ',
        "LOAI_TAI_SAN_MA":"'
                        || GSMAPLTS.MA_NEW
                        || '",
        "LY_DO_BIEN_DONG_MA":"'
                        || NVL(GSLYDOBD.MA_NEW, '015')
                        || '",
        "LY_DO_BIEN_DONG_TEN":"'
                        || GS_STRING_TO_STRINGJSON(NVL(GSLYDOBD.TEN_NEW, 'KhÃ¡c'))
                        || '",
        "HINH_THUC_XU_LY_ID":'
                        || (
            CASE
                WHEN NVL(R_BD_OTO.HINH_THUC_THANH_LY, R_BD_OTO.HINH_THUC_BAN) = '001' THEN
                    2
                WHEN NVL(R_BD_OTO.HINH_THUC_THANH_LY, R_BD_OTO.HINH_THUC_BAN) = '002' THEN
                    3
                WHEN NVL(R_BD_OTO.HINH_THUC_THANH_LY, R_BD_OTO.HINH_THUC_BAN) = '003' THEN
                    1
            END
        )
                        || ',
        "DON_VI_MA":"'
                        || R_BD_OTO.MA_DON_VI
                        || '",
        "NGAY_SU_DUNG":"'
                        || TO_CHAR(R_BD_OTO.THOI_GIAN_SU_DUNG, 'yyyy-MM-dd HH:MI:SS')
                        || '",
        "NHAN_HIEU":"'
                        || GS_STRING_TO_STRINGJSON(R_BD_OTO.NHAN_HIEU)
                        || '",
        "TRANG_THAI":'
                        || NVL(R_BD_OTO.DUYET_BIEN_DONG, 1)
                        || ',
        "QUYET_DINH_SO":"'
                        || GS_STRING_TO_STRINGJSON(R_BD_OTO.SO_QD)
                        || '",
        "QUYET_DINH_NGAY":"'
                        || TO_CHAR(R_BD_OTO.NGAY_BIEN_DONG, 'yyyy-MM-dd HH:MI:SS')
                        || '",
        "OTO_BIEN_KIEM_SOAT":"'
                        || GS_STRING_TO_STRINGJSON(R_BD_OTO.BIEN_KIEM_SOAT)
                        || '",
        "OTO_SO_CHO_NGOI":'
                        || R_BD_OTO.SO_CHO_NGOI
                        || ',
        "OTO_CHUC_DANH_MA":"'
                        || MA_CHUC_DANH
                        || '",
        "OTO_NHAN_XE_MA":"'
                        || MA_NHAN_XE
                        || '",
        "OTO_TAI_TRONG":'
                        || RTRIM(TO_CHAR(R_BD_OTO.TAI_TRONG, 'FM9999999999999999999999990.99'), '.')
                        || ',
        "OTO_XI_LANH":'
                        || RTRIM(TO_CHAR(R_BD_OTO.DUNGTICH_XILANH, 'FM9999999999999999999999990.99'), '.')
                        || ',
        "NGAY_DUYET":"'
                        || TO_CHAR(R_BD_OTO.NGAY_DUYET_BIEN_DONG, 'yyyy-MM-dd HH:MI:SS')
                        || '",
        "GIA_TRI_CON_LAI":'
                        || RTRIM(TO_CHAR(R_BD_OTO.GIA_TRI_CON_LAI, 'FM9999999999999999999999990.99'), '.')
                        || ',
        "KH_GIA_TINH_KHAU_HAO":'
                        || RTRIM(TO_CHAR(NVL(R_BD_OTO.KH_NGUYEN_GIA_NS, 0) + NVL(R_BD_OTO.KH_NGUYEN_GIA_KHAC, 0) + NVL(R_BD_OTO.KH_NGUYEN_GIA_ODA, 0) + NVL(R_BD_OTO.KH_NGUYEN_GIA_VIEN_TRO, 0), 'FM9999999999999999999999990.99'
                        ), '.')
                        || ',
        "KH_GIA_TRI_CON_LAI":'
                        || R_BD_OTO.KH_GIA_TRI_CON_LAI
                        || ',
        "KH_THANG_CON_LAI":'
                        || R_BD_OTO.SO_THANG_CON_SU_DUNG
                        || ',
        "PHI_THU":'
                        || RTRIM(TO_CHAR(R_BD_OTO.SO_TIEN_THU_DUOC, 'FM9999999999999999999999990.99'), '.')
                        || ',   
        "HS_KHAC":"'
                        || GS_STRING_TO_STRINGJSON(R_BD_OTO.SO_QD)
                        || '"},';

        NGUONVONJSON := NGUONVONJSON
                        || '{"NGUON_VON_ID":1, "GIA_TRI":'
                        || R_BD_OTO.NGUYEN_GIA_NS
                        || ', "BIEN_DONG_GUID":"'
                        || BIENDONGGUID
                        || '"},
        {"NGUON_VON_ID":3, "GIA_TRI":'
                        || R_BD_OTO.NGUYEN_GIA_KHAC
                        || ', "BIEN_DONG_GUID":"'
                        || BIENDONGGUID
                        || '"},';

        IF GSTEMPDONGBO.MA_DU_AN IS NULL THEN
            HIENTRANGJSON := HIENTRANGJSON
                             || '{"HIEN_TRANG_ID":99,
          "TEN_HIEN_TRANG":"Quáº£n lÃ½ nhÃ  nÆ°á»›c",
          "KIEU_DU_LIEU_ID":2,
          "NHOM_HIEN_TRANG_ID":null,
          "GIA_TRI_CHECKBOX":'
                             || (
                CASE
                    WHEN R_BD_OTO.HIEN_TRANG_SU_DUNG IN (
                        '0',
                        '1'
                    ) OR R_BD_OTO.HIEN_TRANG_SU_DUNG IS NULL THEN
                        'true'
                    ELSE 'false'
                END
            )
                             || ',
          "BIEN_DONG_GUID":"'
                             || BIENDONGGUID
                             || '"},

          {"HIEN_TRANG_ID":100,
          "TEN_HIEN_TRANG":"HÄ?SN-KhÃ´ng KD",
          "KIEU_DU_LIEU_ID":2,
          "NHOM_HIEN_TRANG_ID":1,
          "GIA_TRI_CHECKBOX":'
                             || (
                CASE
                    WHEN R_BD_OTO.HIEN_TRANG_SU_DUNG = '3' THEN
                        'true'
                    ELSE 'false'
                END
            )
                             || ',
          "BIEN_DONG_GUID":"'
                             || BIENDONGGUID
                             || '"},

          {"HIEN_TRANG_ID":101,
          "TEN_HIEN_TRANG":"HÄ?SN-Kinh doanh",
          "KIEU_DU_LIEU_ID":2,
          "NHOM_HIEN_TRANG_ID":1,
          "GIA_TRI_CHECKBOX":'
                             || (
                CASE
                    WHEN R_BD_OTO.HIEN_TRANG_SU_DUNG = '2' THEN
                        'true'
                    ELSE 'false'
                END
            )
                             || ',
          "BIEN_DONG_GUID":"'
                             || BIENDONGGUID
                             || '"},

          {"HIEN_TRANG_ID":102,
          "TEN_HIEN_TRANG":"Hoáº¡t Ä‘á»™ng khÃ¡c",
          "KIEU_DU_LIEU_ID":2,
          "NHOM_HIEN_TRANG_ID":0,
          "GIA_TRI_CHECKBOX":'
                             || (
                CASE
                    WHEN R_BD_OTO.HIEN_TRANG_SU_DUNG = '4' THEN
                        'true'
                    ELSE 'false'
                END
            )
                             || ',
          "BIEN_DONG_GUID":"'
                             || BIENDONGGUID
                             || '"},

          {"HIEN_TRANG_ID":103,
          "TEN_HIEN_TRANG":"HÄ?SN-LDLK",
          "KIEU_DU_LIEU_ID":2,
          "NHOM_HIEN_TRANG_ID":null,
          "GIA_TRI_CHECKBOX": false,
          "BIEN_DONG_GUID":"'
                             || BIENDONGGUID
                             || '"},

          {"HIEN_TRANG_ID":104,
          "TEN_HIEN_TRANG":"H?SN-Cho thuê",
          "KIEU_DU_LIEU_ID":2,
          "NHOM_HIEN_TRANG_ID":0,
          "GIA_TRI_CHECKBOX": false,
          "BIEN_DONG_GUID":"'
                             || BIENDONGGUID
                             || '"},';
        ELSE
            HIENTRANGJSON := HIENTRANGJSON
                             || '{"HIEN_TRANG_ID":184,
          "TEN_HIEN_TRANG":"Qu?n lý d? án",
          "KIEU_DU_LIEU_ID":2,
          "NHOM_HIEN_TRANG_ID":2,
          "GIA_TRI_CHECKBOX": false,
          "BIEN_DONG_GUID":"'
                             || BIENDONGGUID
                             || '"},

          {"HIEN_TRANG_ID":185,
          "TEN_HIEN_TRANG":"S? d?ng khác",
          "KIEU_DU_LIEU_ID":2,
          "NHOM_HIEN_TRANG_ID":2,
          "GIA_TRI_CHECKBOX": false,
          "BIEN_DONG_GUID":"'
                             || BIENDONGGUID
                             || '"},';
        END IF;

    END LOOP;

    --lay du lieu hao mon

    HAOMONJSON := '';
    FOR R_HAO_MON IN (
        SELECT
            *
        FROM
            KT_HAO_MON_OTO
        WHERE
            MA_TAI_SAN = PMA_TAI_SAN
    ) LOOP
        BEGIN
            SELECT
                SUM((NVL(BD.NGUYEN_GIA_NS, 0) + NVL(BD.NGUYEN_GIA_KHAC, 0) + NVL(BD.NGUYEN_GIA_ODA, 0) + NVL(BD.NGUYEN_GIA_VIEN_TRO, 0)) *(
                    CASE
                        WHEN BD.LOAI_BIEN_DONG IN(
                            '3', '5'
                        ) THEN
                            - 1
                        WHEN BD.LOAI_BIEN_DONG = '4' THEN
                            0
                        ELSE
                            1
                    END
                ))
            INTO L_TONG_NGUYEN_GIA_HM
            FROM
                BD_KHAC BD
            WHERE
                BD.MA_TAI_SAN = PMA_TAI_SAN
                AND BD.DUYET_BIEN_DONG = '2'
                AND EXTRACT(YEAR FROM BD.NGAY_BIEN_DONG) <= R_HAO_MON.NAM;

        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                L_TONG_NGUYEN_GIA_HM := 0;
        END;

        HAOMONJSON := HAOMONJSON
                      || '{
          "NAM_HAO_MON":'
                      || R_HAO_MON.NAM
                      || ',
          "GIA_TRI_HAO_MON":'
                      || RTRIM(TO_CHAR(NVL(R_HAO_MON.GIA_TRI_HAO_MON, 0), 'FM9999999999999999999999990.99'), '.')
                      || ',
          "TONG_HAO_MON_LUY_KE":'
                      || RTRIM(TO_CHAR((L_TONG_NGUYEN_GIA_HM - NVL(R_HAO_MON.GIA_TRI_CON_LAI, 0)), 'FM9999999999999999999999990.99'), '.')
                      || ',
          "TONG_GIA_TRI_CON_LAI":'
                      || RTRIM(TO_CHAR(NVL(R_HAO_MON.GIA_TRI_CON_LAI, 0), 'FM9999999999999999999999990.99'), '.')
                      || ',
          "TY_LE_HAO_MON":'
                      || RTRIM(TO_CHAR(NVL(R_HAO_MON.TY_LE_HAO_MON, 0), 'FM9999999999999999999999990.99'), '.')
                      || ',
          "TONG_NGUYEN_GIA":'
                      || RTRIM(TO_CHAR(NVL(L_TONG_NGUYEN_GIA_HM, 0), 'FM9999999999999999999999990.99'), '.')
                      || '
        },';

    END LOOP;

    BIENDONGJSON := REPLACE('['
                            || BIENDONGJSON
                            || ']', '":,', '":null,');
    NGUONVONJSON := REPLACE('['
                            || NGUONVONJSON
                            || ']', '":,', '":null,');
    HIENTRANGJSON := REPLACE('['
                             || HIENTRANGJSON
                             || ']', '":,', '":null,');
    HAOMONJSON := REPLACE('['
                          || HAOMONJSON
                          || ']', '":,', '":null,');

    -- l_obj := JSON_ARRAY_T(REPLACE(bienDongJson,'},]','}]'));
    -- bienDongJson:= l_obj.stringify;

    -- l_obj := JSON_ARRAY_T(REPLACE(nguonVonJson,'},]','}]'));
    -- nguonVonJson:= l_obj.stringify;

    -- l_obj := JSON_ARRAY_T(REPLACE(hienTrangJson,'},]','}]'));
    -- hienTrangJson:= l_obj.stringify;

    -- l_obj := JSON_ARRAY_T(REPLACE(haomonJson,'},]','}]'));
    -- haomonJson:= l_obj.stringify;
    --UPDATE láº¡i temp Ä‘á»“ng bá»™
    BEGIN
        SELECT
            ( NVL(BD_OTO.NGUYEN_GIA_NS, 0) + NVL(BD_OTO.NGUYEN_GIA_KHAC, 0) + NVL(BD_OTO.NGUYEN_GIA_ODA, 0) + NVL(BD_OTO.NGUYEN_GIA_VIEN_TRO, 0) )
        INTO NGUYENGIABANDAU
        FROM
            BD_OTO
        WHERE
            BD_OTO.MA_TAI_SAN = PMA_TAI_SAN
        ORDER BY
            BD_OTO.NGAY_BIEN_DONG
        FETCH FIRST 1 ROW ONLY;

    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            NGUYENGIABANDAU := 0;
    END;

    SELECT
        GSMAP.*
    INTO GSMAPLTS
    FROM
        GS_MAP_LOAI_TAI_SAN   GSMAP,
        TS_TAI_SAN            TS,
        DM_LOAITAISAN         DMLTS
    WHERE
        DMLTS.LOAI_TAI_SAN_ID = TS.LOAI_TAI_SAN_ID
        AND GSMAP.MA_OLD = DMLTS.MA_LOAI_TAI_SAN
        AND TS.MA_TAI_SAN = PMA_TAI_SAN
        AND GSMAP.LOAI_HINH_TAI_SAN_ID = PLOAI_HINH_TAI_SAN
        AND GSMAP.CHE_DO_HAO_MON_OLD = GSTEMPDONGBO.CHE_DO_HAO_MON_ID_OLD;

    BEGIN
        SELECT
            *
        INTO BDOTOCUOI
        FROM
            BD_OTO
        WHERE
            DUYET_BIEN_DONG = 2
            AND MA_TAI_SAN = PMA_TAI_SAN
        ORDER BY
            NGAY_BIEN_DONG DESC
        FETCH FIRST 1 ROW ONLY;

    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            BDOTOCUOI := NULL;
    END;

     --lay du lieu json cua tai san

    BEGIN
        SELECT
            TO_CLOB('{"BIEN_KIEM_SOAT":"'
                    || NVL(BDOTOCUOI.BIEN_KIEM_SOAT, TS.BIEN_KIEM_SOAT)
                    || '","SO_CHO_NGOI":'
                    ||(
                CASE
                    WHEN BDOTOCUOI.SO_CHO_NGOI > 0 THEN
                        BDOTOCUOI.SO_CHO_NGOI
                    ELSE
                        TS.SO_CHO_NGOI
                END
            )
                    || ',"DUNG_TICH":'
                    || RTRIM(TO_CHAR(NVL(BDOTOCUOI.DUNGTICH_XILANH, TS.DUNGTICH_XILANH), 'FM9999999999999999999999990.99'), '.')
                    || ',"CHUC_DANH_MA":"'
                    ||(
                SELECT
                    GSCD.MA_NEW
                FROM
                    DM_CHUC_DANH          LEFT
                    JOIN GS_MAP_DM_CHUC_DANH   GSCD ON DM_CHUC_DANH.MA_CHUC_DANH = GSCD.MA_OLD
                WHERE
                    DM_CHUC_DANH.ID = NVL(BDOTOCUOI.CHUC_DANH_ID, TS.CHUC_DANH_ID)
            )
                    || '","TAI_TRONG":'
                    || RTRIM(TO_CHAR((
                CASE
                    WHEN BDOTOCUOI.TAI_TRONG > 0 THEN
                        BDOTOCUOI.TAI_TRONG
                    ELSE
                        TS.TAI_TRONG
                END
            ), 'FM9999999999999999999999990.99'), '.')
                    || ',"NHAN_XE_MA":"'
                    ||(
                SELECT
                    DM_NHAN_XE.MA_NHAN_XE
                FROM
                    DM_NHAN_XE
                WHERE
                    ROWNUM = 1
                    AND DM_NHAN_XE.TEN_NHAN_XE = NVL(BDOTOCUOI.NHAN_HIEU, TS.NHAN_HIEU)
            )
                    || '","TRANG_THAI_KH":'
                    ||(
                CASE
                    WHEN NVL(BDOTOCUOI.TINH_KHAU_HAO, TS.TINH_KHAU_HAO) IS NULL THEN
                        'null'
                    ELSE
                        NVL(BDOTOCUOI.TINH_KHAU_HAO, TS.TINH_KHAU_HAO)
                        || ''
                END
            )
                    || '}')
        INTO DATAJSON
        FROM
            TS_OTO TS
        WHERE
            TS.MA_TAI_SAN = PMA_TAI_SAN;

    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            DATAJSON := NULL;
            DATAJSON := NULL;
    END;

    BEGIN
        SELECT
            (
                CASE
                    WHEN LOAI_BIEN_DONG = 5
                         AND DUYET_BIEN_DONG = 2 THEN
                        4
                    ELSE
                        TO_NUMBER(DUYET_BIEN_DONG)
                END
            )
        INTO TRANGTHAI
        FROM
            BD_OTO
        WHERE
            DUYET_BIEN_DONG <> 0
            AND MA_TAI_SAN = PMA_TAI_SAN
        ORDER BY
            NGAY_BIEN_DONG DESC
        FETCH FIRST 1 ROW ONLY;

        IF TRANGTHAI IN (
            1,
            3
        ) THEN
            SELECT
                (
                    CASE
                        WHEN COUNT(*) >= 1 THEN
                            2
                    END
                )
            INTO TRANGTHAI
            FROM
                BD_OTO
            WHERE
                DUYET_BIEN_DONG <> 2
                AND MA_TAI_SAN = PMA_TAI_SAN;

        END IF;

    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            BEGIN
                SELECT
                    2
                INTO TRANGTHAI
                FROM
                    TS_DULIEU_TRAODOI
                WHERE
                    MA_TAI_SAN_PHAT_SINH = PMA_TAI_SAN
                    AND ROWNUM = 1;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    TRANGTHAI := NULL;
            END;
    END;

    UPDATE GS_TEMP_DONG_BO_DU_LIEU GSTEMP
    SET
        GSTEMP.TAI_SAN_JSON = REPLACE(DATAJSON, '":,', '":null,'),
        GSTEMP.LST_BIEN_DONG_JSON = BIENDONGJSON,
        GSTEMP.LST_NGUON_VON_JSON = NGUONVONJSON,
        GSTEMP.LST_HIEN_TRANG_JSON = HIENTRANGJSON,
        GSTEMP.NUOC_SAN_XUAT_MA = (
            CASE
                WHEN NVL(BDOTOCUOI.NUOC_SX, TSOTO.NUOC_SX) = '999' THEN
                    'VN'
                WHEN NVL(BDOTOCUOI.NUOC_SX, TSOTO.NUOC_SX) NOT IN (
                    SELECT
                        MA_QUOC_GIA
                    FROM
                        DM_QUOCGIA
                ) THEN
                    'VN'
                ELSE
                    NVL(BDOTOCUOI.NUOC_SX, TSOTO.NUOC_SX)
            END
        ),
        GSTEMP.NGAY_SU_DUNG = NVL(BDOTOCUOI.THOI_GIAN_SU_DUNG, TSOTO.THOI_GIAN_SU_DUNG),
        GSTEMP.NAM_SAN_XUAT = NVL(BDOTOCUOI.NAM_SX, TSOTO.NAM_SX),
        GSTEMP.NGUYEN_GIA_BAN_DAU = NGUYENGIABANDAU,
        GSTEMP.MA_LOAI_TAI_SAN = GSMAPLTS.MA_NEW,
        GSTEMP.LOAI_TAI_SAN_TEN = GSMAPLTS.TEN_NEW,
        GSTEMP.TRANG_THAI = NVL(TRANGTHAI, GSTEMP.TRANG_THAI),
        GSTEMP.KT_HAO_MON_JSON = HAOMONJSON
    WHERE
        GSTEMP.MA_TAI_SAN = PMA_TAI_SAN;

    COMMIT;
    RETURN 0;
END GS_UPDATE_TSOTO_45_TO_GSTEMP_DONG_BO;