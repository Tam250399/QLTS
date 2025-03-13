CREATE OR REPLACE FUNCTION GS_UPDATE_TSNHA_45_TO_GSTEMP_DONG_BO (
    PMA_TAI_SAN          IN                   VARCHAR2,
    PLOAI_HINH_TAI_SAN   IN                   NUMBER DEFAULT 2
) RETURN NUMBER AS

    BIENDONGJSON           CLOB;
    NGUONVONJSON           CLOB;
    HIENTRANGJSON          CLOB;
    TRANGTHAI              NUMBER;
    DATAJSON               CLOB;
    BIENDONGJSONGUID       VARCHAR2(36);
    BDNHACUOI              BD_NHA%ROWTYPE;
    TSNHA                  TS_NHA%ROWTYPE;
    NGUYENGIABANDAU        NUMBER;
    IS_DAT_AO              VARCHAR(1);
    MA_DIA_BAN_DAT         VARCHAR(500);
    GSLYDOBD               GS_MAP_LY_DO_BIEN_DONG%ROWTYPE;
    DIACHI                 VARCHAR2(500);
    GSMAPLTS               GS_MAP_LOAI_TAI_SAN%ROWTYPE;
    GSTEMPDONGBO           GS_TEMP_DONG_BO_DU_LIEU%ROWTYPE;
    L_OBJ                  JSON_ARRAY_T;
    HAOMONJSON             CLOB;
    L_TONG_NGUYEN_GIA_HM   NUMBER;
    L_ERRMSG               VARCHAR2(255);
    L_ERRCODE              NUMBER;
BEGIN
    --HTSD  ?
    -- ID   Tên                 Ki?u d? li?u    Nhóm    Hi?n th?    S?p x?p
    -- 82	Di?n tích làm vi?c	1	            0	    1	        1
    -- 83	H?SN-Không KD	    1	            	    1	        2
    -- 84	H?SN-Kinh doanh	    1	            0	    1	        3
    -- 85	H?SN-Cho thuê	    1	            	    1	        4
    -- 86	H?SN-LDLK	        1	            	    1	        5
    -- 87	SD h?n h?p	        1	            	    1	        9
    -- 178	?? ?	            1	            0	    1	        6
    -- 179	B? tr?ng	        1	            0	    1	        7
    -- 180	B? l?n chi?m	    1	            0	    1	        8
    -- 206	Tr? s? làm vi?c	    1	            2	    1	        1
    -- 207	S? d?ng khác	    1	            2	    1	        2
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
        INTO TSNHA
        FROM
            TS_NHA
        WHERE
            MA_TAI_SAN = PMA_TAI_SAN;

    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            RETURN 0;
    END;
    --

        --lay bien dong + nguon von + hien trang

    HIENTRANGJSON := '';
    BIENDONGJSON := '';
    NGUONVONJSON := '';
    DATAJSON := '';
    FOR R_BD_NHA IN (
        SELECT
            *
        FROM
            BD_NHA
        WHERE
            BD_NHA.MA_TAI_SAN = PMA_TAI_SAN
        ORDER BY
            BD_NHA.BD_NHA_ID DESC
    ) LOOP
        BIENDONGJSONGUID := RAW_TO_GUID(LPAD(R_BD_NHA.BD_NHA_ID, 32, '0'));
        IF R_BD_NHA.LOAI_BIEN_DONG = 4 THEN
            R_BD_NHA.MA_LY_DO := '047';
        END IF;
        BEGIN
            SELECT
                GSTG.*
            INTO GSLYDOBD
            FROM
                GS_MAP_LY_DO_BIEN_DONG GSTG
            WHERE
                ROWNUM = 1
                AND GSTG.MA_OLD = R_BD_NHA.MA_LY_DO;

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
                        LOAI_TAI_SAN_ID = R_BD_NHA.LOAI_TAI_SAN_ID
                )
                AND CHE_DO_HAO_MON_OLD = GSTEMPDONGBO.CHE_DO_HAO_MON_ID_OLD;

        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                GSMAPLTS := NULL;
        END;

        IF R_BD_NHA.MA_LY_DO IN (
            '050',
            '051',
            '052'
        ) THEN
            BEGIN
                SELECT
                    *
                INTO BDNHACUOI
                FROM
                    BD_NHA
                WHERE
                    MA_TAI_SAN = PMA_TAI_SAN
                    AND DUYET_BIEN_DONG = 2
                    AND NGAY_BIEN_DONG < R_BD_NHA.NGAY_BIEN_DONG
                ORDER BY
                    NGAY_BIEN_DONG DESC
                FETCH FIRST 1 ROW ONLY;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    BDNHACUOI := NULL;
            END;
        END IF;

        BIENDONGJSON := BIENDONGJSON
                        || '{"GUID":"'
                        || BIENDONGJSONGUID
                        || '",
            "NGUYEN_GIA":'
                        || RTRIM(TO_CHAR((CASE
            WHEN R_BD_NHA.MA_LY_DO IN(
                '050', '051', '052'
            ) THEN
                ABS(NVL(R_BD_NHA.NGUYEN_GIA_NS, 0) + NVL(R_BD_NHA.NGUYEN_GIA_KHAC, 0) - NVL(BDNHACUOI.NGUYEN_GIA_NS, 0) - NVL(BDNHACUOI.NGUYEN_GIA_KHAC, 0))
            ELSE NVL(R_BD_NHA.NGUYEN_GIA_NS, 0) + NVL(R_BD_NHA.NGUYEN_GIA_KHAC, 0)
        END), 'FM9999999999999999999999990.99'), '.')
                        || ',
            "NGAY_BIEN_DONG":"'
                        || TO_CHAR(R_BD_NHA.NGAY_BIEN_DONG, 'yyyy-MM-dd HH:MI:SS')
                        || '",
            "NGAY_SU_DUNG":"'
                        || TO_CHAR(R_BD_NHA.THOI_GIAN_SU_DUNG, 'yyyy-MM-dd HH:MI:SS')
                        || '",
            "DON_VI_NHAN_DIEU_CHUYEN_MA":"'
                        || R_BD_NHA.MA_DONVI_NHAN_DIEU_CHUYEN
                        || '",
            "LOAI_BIEN_DONG_ID":'
                        || R_BD_NHA.LOAI_BIEN_DONG
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
                WHEN NVL(R_BD_NHA.HINH_THUC_THANH_LY, R_BD_NHA.HINH_THUC_BAN) = '001' THEN
                    2
                WHEN NVL(R_BD_NHA.HINH_THUC_THANH_LY, R_BD_NHA.HINH_THUC_BAN) = '002' THEN
                    3
                WHEN NVL(R_BD_NHA.HINH_THUC_THANH_LY, R_BD_NHA.HINH_THUC_BAN) = '003' THEN
                    1
            END
        )
                        || ',
            "DON_VI_MA":"'
                        || R_BD_NHA.MA_DON_VI
                        || '",
            "NHA_SO_TANG":'
                        || R_BD_NHA.SO_TANG
                        || ',
            "NHA_TONG_DIEN_TICH_XD":'
                        || RTRIM(TO_CHAR((CASE
            WHEN R_BD_NHA.MA_LY_DO IN(
                '050', '051', '052'
            ) THEN
                ABS(NVL(R_BD_NHA.TONG_DIEN_TICH_SAN, 0) - NVL(BDNHACUOI.TONG_DIEN_TICH_SAN, 0))
            ELSE R_BD_NHA.TONG_DIEN_TICH_SAN
        END), 'FM9999999999999999999999990.99'), '.')
                        || ',
            "NHA_NAM_XAY_DUNG":'
                        || R_BD_NHA.NAM_XAY_DUNG
                        || ',
            "NGAY_DUYET":"'
                        || TO_CHAR(R_BD_NHA.NGAY_DUYET_BIEN_DONG, 'yyyy-MM-dd HH:MI:SS')
                        || '",
            "TRANG_THAI":'
                        || NVL(R_BD_NHA.DUYET_BIEN_DONG, 1)
                        || ',
            "GIA_TRI_CON_LAI":'
                        || RTRIM(TO_CHAR(R_BD_NHA.GIA_TRI_CON_LAI, 'FM9999999999999999999999990.99'), '.')
                        || ',
            "KH_GIA_TINH_KHAU_HAO":'
                        || RTRIM(TO_CHAR((NVL(R_BD_NHA.KH_NGUYEN_GIA_NS, 0) + NVL(R_BD_NHA.KH_NGUYEN_GIA_KHAC, 0) + NVL(R_BD_NHA.KH_NGUYEN_GIA_ODA, 0) + NVL(R_BD_NHA.KH_NGUYEN_GIA_VIEN_TRO, 0)), 'FM9999999999999999999999990.99'
                        ), '.')
                        || ',
            "KH_GIA_TRI_CON_LAI":'
                        || R_BD_NHA.KH_GIA_TRI_CON_LAI
                        || ',
            "KH_THANG_CON_LAI":'
                        || R_BD_NHA.SO_THANG_CON_SU_DUNG
                        || ',
            "QUYET_DINH_SO":"'
                        || GS_STRING_TO_STRINGJSON(R_BD_NHA.SO_QD)
                        || '",
            "QUYET_DINH_NGAY":"'
                        || TO_CHAR(R_BD_NHA.NGAY_BIEN_DONG, 'yyyy-MM-dd HH:MI:SS')
                        || '",
            "PHI_THU":'
                        || RTRIM(TO_CHAR(R_BD_NHA.SO_TIEN_THU_DUOC, 'FM9999999999999999999999990.99'), '.')
                        || ',   
            "HS_KHAC":"'
                        || GS_STRING_TO_STRINGJSON(R_BD_NHA.SO_QD)
                        || '"},';

        NGUONVONJSON := NGUONVONJSON
                        || '{"NGUON_VON_ID":1, "GIA_TRI":'
                        || R_BD_NHA.NGUYEN_GIA_NS
                        || ', "BIEN_DONG_GUID":"'
                        || BIENDONGJSONGUID
                        || '"},
            {"NGUON_VON_ID":3, "GIA_TRI":'
                        || R_BD_NHA.NGUYEN_GIA_KHAC
                        || ', "BIEN_DONG_GUID":"'
                        || BIENDONGJSONGUID
                        || '"},';

        IF GSTEMPDONGBO.MA_DU_AN IS NULL THEN
            HIENTRANGJSON := HIENTRANGJSON
                             || '{"HIEN_TRANG_ID":82,
                "TEN_HIEN_TRANG":"Trá»¥ sá»Ÿ lÃ m viá»‡c",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":0,
                "GIA_TRI_NUMBER":'
                             || RTRIM(TO_CHAR(R_BD_NHA.LAM_TRU_SO_LV, 'FM9999999999999999999999990.99'), '.')
                             || ',
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},

                {"HIEN_TRANG_ID": 83,
                "TEN_HIEN_TRANG":"HÄ?SN-KhÃ´ng KD",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":null,
                "GIA_TRI_NUMBER":'
                             || RTRIM(TO_CHAR(R_BD_NHA.HD_SN, 'FM9999999999999999999999990.99'), '.')
                             || ',
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},

                {"HIEN_TRANG_ID": 84,
                "TEN_HIEN_TRANG":"HÄ?SN-KD",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":0,
                "GIA_TRI_NUMBER": 0,
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},

                {"HIEN_TRANG_ID": 85,
                "TEN_HIEN_TRANG":"HÄ?SN-Cho thuÃª",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":null,
                "GIA_TRI_NUMBER":'
                             || RTRIM(TO_CHAR(R_BD_NHA.CHO_THUE, 'FM9999999999999999999999990.99'), '.')
                             || ',
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},

                {"HIEN_TRANG_ID": 86,
                "TEN_HIEN_TRANG":"HÄ?SN-LDLK",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":null,
                "GIA_TRI_NUMBER": 0,
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},

                {"HIEN_TRANG_ID": 87,
                "TEN_HIEN_TRANG":"SD há»—n há»£p",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":null,
                "GIA_TRI_NUMBER": 0,
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},

                {"HIEN_TRANG_ID": 178,
                "TEN_HIEN_TRANG":"?? ?",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":null,
                "GIA_TRI_NUMBER":'
                             || RTRIM(TO_CHAR((NVL(R_BD_NHA.DE_O, 0)), 'FM9999999999999999999999990.99'), '.')
                             || ',
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},

                {"HIEN_TRANG_ID": 179,
                "TEN_HIEN_TRANG":"B? tr?ng",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":0,
                "GIA_TRI_NUMBER":0,
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},

                {"HIEN_TRANG_ID": 209,
                "TEN_HIEN_TRANG":"S? d?ng khác",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":0,
                "GIA_TRI_NUMBER":'
                             || RTRIM(TO_CHAR((NVL(R_BD_NHA.SD_KHAC, 0)), 'FM9999999999999999999999990.99'), '.')
                             || ',
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},

                {"HIEN_TRANG_ID": 180,
                "TEN_HIEN_TRANG":"B? l?n chi?m",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":0,
                "GIA_TRI_NUMBER":0,
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},';
        ELSE
            HIENTRANGJSON := HIENTRANGJSON
                             || '{"HIEN_TRANG_ID": 206,
                "TEN_HIEN_TRANG":"Tr? s? làm vi?c",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":2,
                "GIA_TRI_NUMBER":0,
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},

                {"HIEN_TRANG_ID": 207,
                "TEN_HIEN_TRANG":"S? d?ng khác",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":2,
                "GIA_TRI_NUMBER":'
                             || RTRIM(TO_CHAR((NVL(R_BD_NHA.SD_KHAC, 0)), 'FM9999999999999999999999990.99'), '.')
                             || ',
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},';
        END IF;

    END LOOP;
        --lay du lieu hao mon

    HAOMONJSON := '';
    FOR R_HAO_MON IN (
        SELECT
            *
        FROM
            KT_HAO_MON
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
                BD_NHA BD
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



        --lay du lieu json cua tai san

    BEGIN
        SELECT
            TSDAT.DIA_CHI
        INTO DIACHI
        FROM
            TS_DAT TSDAT
        WHERE
            TSDAT.MA_TAI_SAN = PMA_TAI_SAN;

    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            BEGIN
                SELECT
                    DV.DIA_CHI
                INTO DIACHI
                FROM
                    DM_DONVI                  DV,
                    GS_TEMP_DONG_BO_DU_LIEU   GSTEMP
                WHERE
                    DV.MA_DON_VI = GSTEMP.MA_DON_VI
                    AND GSTEMP.MA_TAI_SAN = PMA_TAI_SAN;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    DIACHI := NULL;
            END;
    END;

    BEGIN
        SELECT
            *
        INTO BDNHACUOI
        FROM
            BD_NHA
        WHERE
            MA_TAI_SAN = PMA_TAI_SAN
            AND DUYET_BIEN_DONG = 2
        ORDER BY
            NGAY_BIEN_DONG DESC
        FETCH FIRST 1 ROW ONLY;

    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            BDNHACUOI := NULL;
    END;

    BEGIN
        SELECT
            TSDAT.MA_DIA_BAN,
            TSDAT.LA_DAT_AO
        INTO
            MA_DIA_BAN_DAT,
            IS_DAT_AO
        FROM
            TS_DAT   TSDAT,
            TS_NHA   TS
        WHERE
            TSDAT.MA_TAI_SAN = TS.MA_TAI_SAN_DAT
            AND TS.MA_TAI_SAN = PMA_TAI_SAN;

    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            MA_DIA_BAN_DAT := NULL;
            IS_DAT_AO := '0';
    END;

    BEGIN
        SELECT
            TO_CLOB('{"DIA_BAN_MA":"'
                    || MA_DIA_BAN_DAT
                    || '","TAI_SAN_DAT_MA":"'
                    ||(
                CASE
                    WHEN IS_DAT_AO = '1' THEN
                        ''
                    ELSE
                        NVL(BDNHACUOI.MA_TAI_SAN_DAT, TS.MA_TAI_SAN_DAT)
                END
            )
                    || '","DIA_CHI":"'
                    || GS_STRING_TO_STRINGJSON(DIACHI)
                    || '","NHA_SO_TANG":'
                    || NVL(BDNHACUOI.SO_TANG, TS.SO_TANG)
                    || ',"NAM_XAY_DUNG":'
                    || NVL(BDNHACUOI.NAM_XAY_DUNG, TS.NAM_XAY_DUNG)
                    || ',"DIEN_TICH_SAN_XAY_DUNG":'
                    || RTRIM(TO_CHAR(NVL(BDNHACUOI.TONG_DIEN_TICH_SAN, TS.TONG_DIEN_TICH_SAN), 'FM9999999999999999999999990.99'), '.')
                    || ',"NGAY_SU_DUNG":"'
                    || NVL(BDNHACUOI.THOI_GIAN_SU_DUNG, TS.THOI_GIAN_SU_DUNG)
                    || '"}')
        INTO DATAJSON
        FROM
            TS_NHA TS
        WHERE
            TS.MA_TAI_SAN = PMA_TAI_SAN;

    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            DATAJSON := NULL;
    END;

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

        -- l_obj := JSON_ARRAY_T(REPLACE(biendongJson,'},]','}]'));
        -- biendongJson:= l_obj.stringify;

        -- l_obj := JSON_ARRAY_T(REPLACE(nguonVonJson,'},]','}]'));
        -- nguonVonJson:= l_obj.stringify;

        -- l_obj := JSON_ARRAY_T(REPLACE(hienTrangJson,'},]','}]'));
        -- hienTrangJson:= l_obj.stringify;

        -- l_obj := JSON_ARRAY_T(REPLACE(haomonJson,'},]','}]'));
        -- haomonJson:= l_obj.stringify;
        --UPDATE láº¡i temp Ä‘á»“ng bá»™
    BEGIN
        SELECT
            ( NVL(BD_NHA.NGUYEN_GIA_NS, 0) + NVL(BD_NHA.NGUYEN_GIA_KHAC, 0) + NVL(BD_NHA.NGUYEN_GIA_ODA, 0) + NVL(BD_NHA.NGUYEN_GIA_VIEN_TRO, 0) )
        INTO NGUYENGIABANDAU
        FROM
            BD_NHA
        WHERE
            BD_NHA.MA_TAI_SAN = PMA_TAI_SAN
        ORDER BY
            BD_NHA.NGAY_BIEN_DONG
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
            BD_NHA
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
                BD_NHA
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
        GSTEMP.NGUYEN_GIA_BAN_DAU = NGUYENGIABANDAU,
        GSTEMP.NGAY_SU_DUNG = NVL(BDNHACUOI.THOI_GIAN_SU_DUNG, TSNHA.THOI_GIAN_SU_DUNG),
        GSTEMP.MA_LOAI_TAI_SAN = GSMAPLTS.MA_NEW,
        GSTEMP.LOAI_TAI_SAN_TEN = GSMAPLTS.TEN_NEW,
        GSTEMP.TRANG_THAI = NVL(TRANGTHAI, GSTEMP.TRANG_THAI),
        GSTEMP.KT_HAO_MON_JSON = HAOMONJSON
    WHERE
        GSTEMP.MA_TAI_SAN = PMA_TAI_SAN;

    COMMIT;
    RETURN 0;
EXCEPTION
    WHEN OTHERS THEN
        L_ERRMSG := SUBSTR(SQLERRM, 1, 200);
        L_ERRCODE := SQLCODE;
        UPDATE GS_TEMP_DONG_BO_DU_LIEU GSTEMP
        SET
            GSTEMP.ERROR = '{"err_code":"'
                           || L_ERRCODE
                           || '","err_msg":"'
                           || L_ERRMSG
                           || '"}'
        WHERE
            GSTEMP.MA_TAI_SAN = PMA_TAI_SAN;

END GS_UPDATE_TSNHA_45_TO_GSTEMP_DONG_BO;