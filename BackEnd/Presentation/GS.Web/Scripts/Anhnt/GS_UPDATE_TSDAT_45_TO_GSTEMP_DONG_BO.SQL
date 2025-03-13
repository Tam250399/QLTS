CREATE OR REPLACE FUNCTION GS_UPDATE_TSDAT_45_TO_GSTEMP_DONG_BO (
    PMA_TAI_SAN          IN                   VARCHAR2,
    PLOAI_HINH_TAI_SAN   IN                   NUMBER
) RETURN NUMBER AS

    NGUYENGIABANDAU        NUMBER;
    GSLYDOBD               GS_MAP_LY_DO_BIEN_DONG%ROWTYPE;
    BDDATCUOI              BD_DAT%ROWTYPE;
    BIENDONGJSON           CLOB;
    TRANGTHAI              NUMBER;
    NGUONVONJSON           CLOB;
    HIENTRANGJSON          CLOB;
    DATAJSON               CLOB;
    BIENDONGJSONGUID       VARCHAR2(36);
    GSMAPLTS               GS_MAP_LOAI_TAI_SAN%ROWTYPE;
    TSDAT                  TS_DAT%ROWTYPE;
    GSTEMPDONGBO           GS_TEMP_DONG_BO_DU_LIEU%ROWTYPE;
    L_OBJ                  JSON_ARRAY_T;
    HAOMONJSON             CLOB;
    L_TONG_NGUYEN_GIA_HM   NUMBER;
    L_TEN_DAT              VARCHAR2(2000);
BEGIN

--HTSD  
-- ID   Tên                 Ki?u d? li?u    Nhóm    Hi?n th?    S?p x?p
-- 72	Di?n tích làm vi?c	1	            0	    1	        1
-- 73	H?SN-Không KD	    1	            	    1	        2
-- 75	H?SN-Kinh doanh	    1	            0	    1	        3
-- 78	H?SN-Cho thuê	    1	            	    1	        4
-- 79	H?SN-LDLK	        1	            	    1	        5
-- 81	SD h?n h?p	        1	            	    1	        9
-- 181	?? ?	            1	            0	    1	        6
-- 182	B? tr?ng	        1	            0	    1	        7
-- 183	B? l?n chi?m	    1	            0	    1	        8
-- 204	Tr? s? làm vi?c	    1	            2	    1	        1
-- 205	S? d?ng khác	    1	            2	    1	        2
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
        INTO TSDAT
        FROM
            TS_DAT
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
    FOR R_BD_DAT IN (
        SELECT
            *
        FROM
            BD_DAT
        WHERE
            BD_DAT.MA_TAI_SAN = PMA_TAI_SAN
        ORDER BY
            BD_DAT.BD_DAT_ID DESC
    ) LOOP
        BIENDONGJSONGUID := RAW_TO_GUID(LPAD(R_BD_DAT.BD_DAT_ID, 32, '0'));
        IF R_BD_DAT.LOAI_BIEN_DONG = 4 THEN
            R_BD_DAT.MA_LY_DO := '047';
        END IF;
        BEGIN
            SELECT
                GSTG.*
            INTO GSLYDOBD
            FROM
                GS_MAP_LY_DO_BIEN_DONG GSTG
            WHERE
                ROWNUM = 1
                AND GSTG.MA_OLD = R_BD_DAT.MA_LY_DO;
                -- AND (
                --         gstg.LOAI_HINH_TAI_SAN_NEW = pLOAI_HINH_TAI_SAN
                --         or gstg.LOAI_HINH_TAI_SAN_NEW = 0
                --     );

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
                        LOAI_TAI_SAN_ID = R_BD_DAT.LOAI_TAI_SAN_ID
                )
                AND CHE_DO_HAO_MON_OLD = GSTEMPDONGBO.CHE_DO_HAO_MON_ID_OLD;

        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                GSMAPLTS := NULL;
        END;

        IF R_BD_DAT.MA_LY_DO IN (
            '050',
            '051',
            '052'
        ) THEN
            BEGIN
                SELECT
                    *
                INTO BDDATCUOI
                FROM
                    BD_DAT
                WHERE
                    MA_TAI_SAN = PMA_TAI_SAN
                    AND DUYET_BIEN_DONG = 2
                    AND NGAY_BIEN_DONG < R_BD_DAT.NGAY_BIEN_DONG
                ORDER BY
                    NGAY_BIEN_DONG DESC
                FETCH FIRST 1 ROW ONLY;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    BDDATCUOI := NULL;
            END;
        END IF;

        BIENDONGJSON := BIENDONGJSON
                        || '{"GUID":"'
                        || BIENDONGJSONGUID
                        || '",
            "NGUYEN_GIA":'
                        || RTRIM(TO_CHAR((CASE
            WHEN R_BD_DAT.MA_LY_DO IN(
                '050', '051', '052'
            ) THEN
                ABS(NVL(R_BD_DAT.GIA_TRI, 0) - NVL(BDDATCUOI.GIA_TRI, 0))
            ELSE R_BD_DAT.GIA_TRI
        END), 'FM9999999999999999999999990.99'), '.')
                        || ',
            "NGAY_BIEN_DONG":"'
                        || TO_CHAR(R_BD_DAT.NGAY_BIEN_DONG, 'yyyy-MM-dd HH:MI:SS')
                        || '",
            "NGAY_SU_DUNG":"'
                        || '2008-01-01 01:00:00'
                        || '",
            "LOAI_BIEN_DONG_ID":'
                        || R_BD_DAT.LOAI_BIEN_DONG
                        || ',
            "LOAI_TAI_SAN_MA":"'
                        || GSMAPLTS.MA_NEW
                        || '",
            "LY_DO_BIEN_DONG_MA":"'
                        || NVL(GSLYDOBD.MA_NEW, '015')
                        || '",
            "LY_DO_BIEN_DONG_TEN":"'
                        || GS_STRING_TO_STRINGJSON(GSLYDOBD.TEN_NEW)
                        || '",
            "HINH_THUC_XU_LY_ID":'
                        || (
            CASE
                WHEN NVL(R_BD_DAT.HINH_THUC_THANH_LY, R_BD_DAT.HINH_THUC_BAN) = '001' THEN
                    2
                WHEN NVL(R_BD_DAT.HINH_THUC_THANH_LY, R_BD_DAT.HINH_THUC_BAN) = '002' THEN
                    3
                WHEN NVL(R_BD_DAT.HINH_THUC_THANH_LY, R_BD_DAT.HINH_THUC_BAN) = '003' THEN
                    1
            END
        )
                        || ',
            "DON_VI_MA":"'
                        || R_BD_DAT.MA_DON_VI
                        || '",
            "DIA_BAN_MA":"'
                        || R_BD_DAT.MA_DIA_BAN
                        || '",
            "DON_VI_NHAN_DIEU_CHUYEN_MA":"'
                        || R_BD_DAT.MA_DONVI_NHAN_DIEU_CHUYEN
                        || '",
            "QUYET_DINH_SO":"'
                        || GS_STRING_TO_STRINGJSON(R_BD_DAT.QUYET_DINH_GIAO_DAT_SO)
                        || '",
            "QUYET_DINH_NGAY":"'
                        || TO_CHAR(R_BD_DAT.QUYET_DINH_GIAO_DAT_NGAY, 'yyyy-MM-dd HH:MI:SS')
                        || '",
            "TRANG_THAI":'
                        || NVL(R_BD_DAT.DUYET_BIEN_DONG, 1)
                        || ',
            "DAT_TONG_DIEN_TICH":'
                        || RTRIM(TO_CHAR((CASE
            WHEN R_BD_DAT.MA_LY_DO IN(
                '050', '051', '052'
            ) THEN
                ABS(NVL(R_BD_DAT.DIEN_TICH, 0) - NVL(BDDATCUOI.DIEN_TICH, 0))
            ELSE R_BD_DAT.DIEN_TICH
        END), 'FM9999999999999999999999990.99'), '.')
                        || ',
            "NGAY_DUYET":"'
                        || TO_CHAR(R_BD_DAT.NGAY_DUYET_BIEN_DONG, 'yyyy-MM-dd HH:MI:SS')
                        || '",
            "HS_CNQSD_SO":"'
                        || GS_STRING_TO_STRINGJSON(R_BD_DAT.CN_QUYEN_SD_DAT_SO)
                        || '",
            "HS_CNQSD_NGAY":"'
                        || TO_CHAR(R_BD_DAT.CN_QUYEN_SD_DAT_NGAY, 'yyyy-MM-dd HH:MI:SS')
                        || '",
            "HS_QUYET_DINH_GIAO_SO":"'
                        || GS_STRING_TO_STRINGJSON(R_BD_DAT.QUYET_DINH_GIAO_DAT_SO)
                        || '",
            "HS_QUYET_DINH_GIAO_NGAY":"'
                        || TO_CHAR(R_BD_DAT.QUYET_DINH_GIAO_DAT_NGAY, 'yyyy-MM-dd HH:MI:SS')
                        || '",
            "HS_CHUYEN_NHUONG_SD_SO":"'
                        || GS_STRING_TO_STRINGJSON(R_BD_DAT.HD_CHUYEN_NHUONG_SD_SO)
                        || '",
            "HS_CHUYEN_NHUONG_SD_NGAY":"'
                        || TO_CHAR(R_BD_DAT.HD_CHUYEN_NHUONG_SD_NGAY, 'yyyy-MM-dd HH:MI:SS')
                        || '",
            "HS_QUYET_DINH_CHO_THUE_SO":"'
                        || GS_STRING_TO_STRINGJSON(R_BD_DAT.HD_THUE_DAT_SO)
                        || '",
            "HS_QUYET_DINH_CHO_THUE_NGAY":"'
                        || TO_CHAR(R_BD_DAT.HD_THUE_DAT_NGAY, 'yyyy-MM-dd HH:MI:SS')
                        || '",
            "QUYET_DINH_SO":"'
                        || GS_STRING_TO_STRINGJSON(R_BD_DAT.SO_QD)
                        || '",
            "QUYET_DINH_NGAY":"'
                        || TO_CHAR(R_BD_DAT.NGAY_BIEN_DONG, 'yyyy-MM-dd HH:MI:SS')
                        || '",
            "PHI_THU":'
                        || RTRIM(TO_CHAR(R_BD_DAT.SO_TIEN_THU_DUOC, 'FM9999999999999999999999990.99'), '.')
                        || ',
            "HS_KHAC":"'
                        || GS_STRING_TO_STRINGJSON(R_BD_DAT.GIAY_TO_KHAC)
                        || '"},';

        NGUONVONJSON := NGUONVONJSON
                        || '{"NGUON_VON_ID":1, "GIA_TRI":'
                        || RTRIM(TO_CHAR(R_BD_DAT.GIA_TRI, 'FM9999999999999999999999990.99'), '.')
                        || ',  "BIEN_DONG_GUID":"'
                        || BIENDONGJSONGUID
                        || '"},';

        IF GSTEMPDONGBO.MA_DU_AN IS NULL THEN
            HIENTRANGJSON := HIENTRANGJSON
                             || '{"HIEN_TRANG_ID":72,
                "TEN_HIEN_TRANG":"Di?n tích làm vi?c",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":0,
                "GIA_TRI_NUMBER":'
                             || RTRIM(TO_CHAR(R_BD_DAT.LAM_TRU_SO_NN, 'FM9999999999999999999999990.99'), '.')
                             || ',
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},

                {"HIEN_TRANG_ID":73,
                "TEN_HIEN_TRANG":"H?SN-Không KD",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":null,
                "GIA_TRI_NUMBER":'
                             || RTRIM(TO_CHAR(R_BD_DAT.LAM_CO_SO_HD_SN, 'FM9999999999999999999999990.99'), '.')
                             || ',
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},

                {"HIEN_TRANG_ID":75,
                "TEN_HIEN_TRANG":"H?SN-KD",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":0,
                "GIA_TRI_NUMBER":0,
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},

                {"HIEN_TRANG_ID":78,
                "TEN_HIEN_TRANG":"H?SN-Cho thuê",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":null,
                "GIA_TRI_NUMBER":'
                             || RTRIM(TO_CHAR(R_BD_DAT.KHAC_CHO_THUE, 'FM9999999999999999999999990.99'), '.')
                             || ',
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},

                {"HIEN_TRANG_ID":79,
                "TEN_HIEN_TRANG":"H?SN-LDLK",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":null,
                "GIA_TRI_NUMBER":0,
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},

                {"HIEN_TRANG_ID":81,
                "TEN_HIEN_TRANG":"SD há»—n há»£p",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":null,
                "GIA_TRI_NUMBER":0,
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},

                {"HIEN_TRANG_ID":181,
                "TEN_HIEN_TRANG":"?? ?",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":0,
                "GIA_TRI_NUMBER":'
                             || RTRIM(TO_CHAR(NVL(R_BD_DAT.KHAC_LAM_NHA_O, 0), 'FM9999999999999999999999990.99'), '.')
                             || ',
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},

                {"HIEN_TRANG_ID":182,
                "TEN_HIEN_TRANG":"B? l?n chi?m",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":0,
                "GIA_TRI_NUMBER":'
                             || RTRIM(TO_CHAR(NVL(R_BD_DAT.KHAC_BI_LAN_CHIEM, 0), 'FM9999999999999999999999990.99'), '.')
                             || ',
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},

                {"HIEN_TRANG_ID":208,
                "TEN_HIEN_TRANG":"S? d?ng khác",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":0,
                "GIA_TRI_NUMBER":'
                             || RTRIM(TO_CHAR(NVL(R_BD_DAT.KHAC, 0), 'FM9999999999999999999999990.99'), '.')
                             || ',
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},

                {"HIEN_TRANG_ID":183,
                "TEN_HIEN_TRANG":"B? tr?ng",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":0,
                "GIA_TRI_NUMBER":'
                             || RTRIM(TO_CHAR(NVL(R_BD_DAT.KHAC_BO_TRONG, 0), 'FM9999999999999999999999990.99'), '.')
                             || ',
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},';
        ELSE
            HIENTRANGJSON := HIENTRANGJSON
                             || '{"HIEN_TRANG_ID":204,
                "TEN_HIEN_TRANG":"Tr? s? làm vi?c",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":2,
                "GIA_TRI_NUMBER":0,
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},

                {"HIEN_TRANG_ID":205,
                "TEN_HIEN_TRANG":"S? d?ng khác",
                "KIEU_DU_LIEU_ID":1,
                "NHOM_HIEN_TRANG_ID":2,
                "GIA_TRI_NUMBER":'
                             || RTRIM(TO_CHAR(NVL(R_BD_DAT.KHAC, 0), 'FM9999999999999999999999990.99'), '.')
                             || ',
                "BIEN_DONG_GUID":"'
                             || BIENDONGJSONGUID
                             || '"},';
        END IF;

    END LOOP;
    --lay du lieu hao mon

    --lay du lieu json cua tai san

    BEGIN
        SELECT
            *
        INTO BDDATCUOI
        FROM
            BD_DAT
        WHERE
            MA_TAI_SAN = PMA_TAI_SAN
            AND DUYET_BIEN_DONG = 2
        ORDER BY
            NGAY_BIEN_DONG DESC
        FETCH FIRST 1 ROW ONLY;

    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            BDDATCUOI := NULL;
    END;

    BEGIN
        SELECT
            TO_CLOB('{"DIA_CHI":"'
                    || GS_STRING_TO_STRINGJSON(NVL(BDDATCUOI.DIA_CHI, TSDAT.DIA_CHI))
                    || '","DIA_BAN_MA":"'
                    || NVL(BDDATCUOI.MA_DIA_BAN, TSDAT.MA_DIA_BAN)
                    || '","DIEN_TICH":'
                    || RTRIM(TO_CHAR((
                CASE
                    WHEN BDDATCUOI.DIEN_TICH > 0 THEN
                        BDDATCUOI.DIEN_TICH
                    ELSE
                        TSDAT.DIEN_TICH
                END
            ), 'FM9999999999999999999999990.99'), '.')
                    || ',"HS_QUYET_DINH_GIAO_SO":"'
                    || GS_STRING_TO_STRINGJSON(NVL(BDDATCUOI.QUYET_DINH_GIAO_DAT_SO, TSDAT.QUYET_DINH_GIAO_DAT_SO))
                    || '","HS_QUYET_DINH_GIAO_NGAY":"'
                    || NVL(BDDATCUOI.QUYET_DINH_GIAO_DAT_NGAY, TSDAT.QUYET_DINH_GIAO_DAT_NGAY)
                    || '","HS_QUYET_DINH_CHO_THUE_SO":"'
                    || GS_STRING_TO_STRINGJSON(NVL(BDDATCUOI.HD_THUE_DAT_SO, TSDAT.HD_THUE_DAT_SO))
                    || '","HS_QUYET_DINH_CHO_THUE_NGAY":"'
                    || NVL(BDDATCUOI.HD_THUE_DAT_NGAY, TSDAT.HD_THUE_DAT_NGAY)
                    || '","HS_CHUYEN_NHUONG_SD_SO":"'
                    || GS_STRING_TO_STRINGJSON(NVL(BDDATCUOI.HD_CHUYEN_NHUONG_SD_SO, TSDAT.HD_CHUYEN_NHUONG_SD_SO))
                    || '","HS_CHUYEN_NHUONG_SD_NGAY":"'
                    || NVL(BDDATCUOI.HD_CHUYEN_NHUONG_SD_NGAY, TSDAT.HD_CHUYEN_NHUONG_SD_NGAY)
                    || '","HS_KHAC":"'
                    || GS_STRING_TO_STRINGJSON(NVL(BDDATCUOI.KHAC, TSDAT.KHAC))
                    || '","HS_CNQSD_SO":"'
                    || GS_STRING_TO_STRINGJSON(NVL(BDDATCUOI.CN_QUYEN_SD_DAT_SO, TSDAT.CN_QUYEN_SD_DAT_SO))
                    || '","HS_CNQSD_NGAY":"'
                    || NVL(BDDATCUOI.CN_QUYEN_SD_DAT_NGAY, TSDAT.CN_QUYEN_SD_DAT_NGAY)
                    || '"}'),
            ( TS.TEN_TAI_SAN
              || ','
              || GS_STRING_TO_STRINGJSON(NVL(BDDATCUOI.DIA_CHI, TSDAT.DIA_CHI))
              || ','
              || (
                SELECT
                    LISTAGG(DM_DIABAN.TEN_DIA_BAN, ', ') WITHIN GROUP(
                        ORDER BY
                            DM_DIABAN.MA_PHAN_CAP DESC
                    ) AS DESCRIPTION
                FROM
                    DM_DIABAN
                START WITH
                    MA_DIA_BAN = NVL(BDDATCUOI.MA_DIA_BAN, TSDAT.MA_DIA_BAN)
                CONNECT BY
                    PRIOR MA_DIA_BAN_CHA = MA_DIA_BAN
            ) )
        INTO
            DATAJSON,
            L_TEN_DAT
        FROM
            TS_DAT       TSDAT
            INNER JOIN TS_TAI_SAN   TS ON TS.MA_TAI_SAN = TSDAT.MA_TAI_SAN
        WHERE
            TSDAT.MA_TAI_SAN = PMA_TAI_SAN;

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
    L_OBJ := JSON_ARRAY_T(REPLACE(BIENDONGJSON, '},]', '}]'));
    BIENDONGJSON := L_OBJ.STRINGIFY;
    L_OBJ := JSON_ARRAY_T(REPLACE(NGUONVONJSON, '},]', '}]'));
    NGUONVONJSON := L_OBJ.STRINGIFY;
    L_OBJ := JSON_ARRAY_T(REPLACE(HIENTRANGJSON, '},]', '}]'));
    HIENTRANGJSON := L_OBJ.STRINGIFY;
    --UPDATE láº¡i temp Ä‘á»“ng bá»™
    BEGIN
        SELECT
            BD_DAT.GIA_TRI
        INTO NGUYENGIABANDAU
        FROM
            BD_DAT
        WHERE
            BD_DAT.MA_TAI_SAN = PMA_TAI_SAN
        ORDER BY
            BD_DAT.NGAY_BIEN_DONG
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
            BD_DAT
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
                BD_DAT
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
        GSTEMP.NGAY_SU_DUNG = TO_DATE('2008-01-01 01:00:00', 'yyyy-MM-dd HH:MI:SS'),
        GSTEMP.MA_LOAI_TAI_SAN = GSMAPLTS.MA_NEW,
        GSTEMP.LOAI_TAI_SAN_TEN = GSMAPLTS.TEN_NEW,
        GSTEMP.TRANG_THAI = NVL(TRANGTHAI, GSTEMP.TRANG_THAI),
        GSTEMP.TEN_TAI_SAN = L_TEN_DAT
    WHERE
        GSTEMP.MA_TAI_SAN = PMA_TAI_SAN;

    COMMIT;
    RETURN 0;
END GS_UPDATE_TSDAT_45_TO_GSTEMP_DONG_BO;