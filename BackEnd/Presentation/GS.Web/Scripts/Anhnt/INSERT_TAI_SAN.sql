create or replace PROCEDURE INSERT_TAI_SAN (
    P_JSON_STRING      IN    CLOB,
    P_CHE_DO_HAO_MON   IN    NUMBER DEFAULT 23,
    MSS_OUT            OUT   SYS_REFCURSOR
) AS

    L_TAI_SAN                        TS_TAI_SAN%ROWTYPE;
    L_BIEN_DONG_ID                   NUMBER;
    L_JSON_OBJECT                    JSON_OBJECT_T;
    L_BIEN_DONG_ARR                  JSON_ARRAY_T;
    L_BIEN_DONG_OBJECT               JSON_OBJECT_T;
    L_HIEN_TRANG_ARR                 JSON_ARRAY_T;
    L_HIEN_TRANG_OBJECT              JSON_OBJECT_T;
    L_NGUON_VON_ARR                  JSON_ARRAY_T;
    L_NGUON_VON_OBJECT               JSON_OBJECT_T;
    L_HAO_MON_ARR                    JSON_ARRAY_T;
    L_HAO_MON_OBJECT                 JSON_OBJECT_T;
--object tài sản
    L_TS_DAT                         TS_TAI_SAN_DAT%ROWTYPE;
    L_TS_NHA                         TS_TAI_SAN_NHA%ROWTYPE;
    L_TS_VKT                         TS_TAI_SAN_VKT%ROWTYPE;
    L_TS_OTO                         TS_TAI_SAN_OTO%ROWTYPE;
    L_TS_PTK                         TS_TAI_SAN_OTO%ROWTYPE;
    L_TS_CLN                         TS_TAI_SAN_CLN%ROWTYPE;
    L_TS_MAYMOC                      TS_TAI_SAN_MAY_MOC%ROWTYPE;
    L_TS_HUUHINH                     TS_TAI_SAN_MAY_MOC%ROWTYPE;
    L_TS_VOHINH                      TS_TAI_SAN_VO_HINH%ROWTYPE;
    L_TS_DACTHU                      TS_TAI_SAN_MAY_MOC%ROWTYPE;
    L_BD                             BD_BIEN_DONG%ROWTYPE;
    L_BD_CT                          BD_BIEN_DONG_CHI_TIET%ROWTYPE;
    L_YC                             NV_YEU_CAU%ROWTYPE;
    L_YC_CT                          NV_YEU_CAU_CHI_TIET%ROWTYPE;
    L_TS_HIEN_TRANG                  TS_TAI_SAN_HIEN_TRANG_SU_DUNG%ROWTYPE;
    L_TEN_HIEN_TRANG                 VARCHAR(1000);
    L_TS_NGUON_VON                   TS_TAI_SAN_NGUON_VON%ROWTYPE;
    L_HAOMON                         KT_HAO_MON_TAI_SAN%ROWTYPE;
    L_DIA_BAN                        DM_DIA_BAN%ROWTYPE;
-- object tài sản json
    L_TS_DAT_OBJECT                  JSON_OBJECT_T;
    L_TS_NHA_OBJECT                  JSON_OBJECT_T;
    L_TS_VKT_OBJECT                  JSON_OBJECT_T;
    L_TS_OTO_OBJECT                  JSON_OBJECT_T;
    L_TS_PTK_OBJECT                  JSON_OBJECT_T;
    L_TS_CLN_OBJECT                  JSON_OBJECT_T;
    L_TS_MAYMOC_OBJECT               JSON_OBJECT_T;
    L_TS_HUUHINH_OBJECT              JSON_OBJECT_T;
    L_TS_VOHINH_OBJECT               JSON_OBJECT_T;
    L_TS_DACTHU_OBJECT               JSON_OBJECT_T;
--tai san
    L_EXC_FN                         NUMBER;
    L_ID                             NUMBER;
    L_BD_ID                          NUMBER;
    L_YC_ID                          NUMBER;
    L_MA_DB                          VARCHAR(50);
    L_MA_TSDAT                       VARCHAR(50);
    L_LOAITAISAN_MA                  VARCHAR(50);
    L_LOAITAISANBD_MA                VARCHAR(50);
    L_LOAITAISANVH_MA                VARCHAR(50);
    L_LOAITAISAN_TEN                 VARCHAR(4000);
    L_DONVI_MA                       VARCHAR(50);
    L_DONVI_ID                       NUMBER;
    L_NSX_MA                         VARCHAR(50);
    L_NSX_ID                         NUMBER;
    L_LOAI_HINH_TAI_SAN              NUMBER;
    L_LYDO_MA                        VARCHAR(50);
    L_LYDO_TEN                       VARCHAR(4000);
    L_LYDO_ID                        NUMBER;
    L_DUAN_MA                        VARCHAR(50);
    L_DUAN_ID                        NUMBER;
    L_DIABAN_MA                      VARCHAR(50);
    L_NHANXE_MA                      VARCHAR(50);
    L_NHANXE_ID                      NUMBER;
    L_CHUCDANH_MA                    VARCHAR(50);
    L_CHUCDANH_ID                    NUMBER;
    L_LOAITAISAN_ID                  NUMBER;
    L_LOAITAISANVOHINH_ID            NUMBER;
    L_DONVI_BOPHAN_ID                NUMBER;
    L_DONVI_BOPHAN_MA                VARCHAR(50);
    L_HINH_THUC_MUA_SAM_ID           NUMBER;
    L_HINH_THUC_MUA_SAM_MA           VARCHAR(50);
    L_HINH_THUC_XU_LY_ID             NUMBER;
    L_HINH_THUC_XU_LY_MA             VARCHAR(50);
    L_MUC_DICH_SU_DUNG_ID            NUMBER;
    L_MUC_DICH_SU_DUNG_MA            VARCHAR(50);
    L_DONVI_DIEU_CHUYEN_ID           NUMBER;
    L_DONVI_DIEU_CHUYEN_MA           VARCHAR(50);
    L_BD_HTSD_JSON                   CLOB;
    L_BD_NV_JSON                     CLOB;
    L_TAI_SAN_NHAT_KY_ID             NUMBER;
    L_NAM_MIN                        NUMBER;
    L_NAM_MAX                        NUMBER;
    L_TONG_NGUYEN_GIA                NUMBER;
    L_TSDAT_TENDIABAN                VARCHAR(4000);
    L_LOAI_HINH_TAI_SAN_BD_FOR_LTS   NUMBER;
    L_HAO_MON_ID                     NUMBER;
BEGIN
    L_JSON_OBJECT := JSON_OBJECT_T(P_JSON_STRING);
    L_MA_DB := L_JSON_OBJECT.GET_STRING('MA_TAI_SAN');
    ----DBMS_OUTPUT.PUT_LINE('l_ma_db=' || l_ma_db);
    L_LOAITAISAN_MA := L_JSON_OBJECT.GET_STRING('MA_LOAI_TAI_SAN');
    L_LOAITAISAN_TEN := UNESCAPE_STRINGJSON(L_JSON_OBJECT.GET_STRING('LOAI_TAI_SAN_TEN'));
    L_DONVI_MA := L_JSON_OBJECT.GET_STRING('MA_DON_VI');
    L_NSX_MA := L_JSON_OBJECT.GET_STRING('NUOC_SAN_XUAT_MA');
    L_LOAI_HINH_TAI_SAN := L_JSON_OBJECT.GET_NUMBER('LOAI_HINH_TAI_SAN_ID');
    L_LYDO_MA := L_JSON_OBJECT.GET_STRING('LY_DO_BIEN_DONG_MA');
    L_LYDO_TEN := UNESCAPE_STRINGJSON(L_JSON_OBJECT.GET_STRING('LY_DO_BIEN_DONG_TEN'));
    L_DUAN_MA := L_JSON_OBJECT.GET_STRING('MA_DU_AN');
    BEGIN
        SELECT
            *
        INTO L_TAI_SAN
        FROM
            TS_TAI_SAN
        WHERE
            MA_QLDKTS40 = L_MA_DB
            AND TRANG_THAI_ID != 6
            AND NGUON_TAI_SAN_ID = 1;

--        OPEN MSS_OUT FOR SELECT
--                            JSON_OBJECT ( 'Code' VALUE '00', 'Message' VALUE 'Done', 'IdRecord' VALUE NULL, 'ObjectInfo' VALUE L_TAI_SAN.MA ) AS STRJSON
--                        FROM
--                            DUAL;

--        RETURN;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            L_TAI_SAN := NULL;
    END;

    IF l_tai_san.id IS NULL THEN 
        l_tai_san.GUID := SYS_GUID();
    ELSE 
        l_exc_fn := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(l_tai_san.id);
    END IF;
--BEGIN prepare TAI_SAN
--check loaiHinhTaiSan 

    IF L_LOAI_HINH_TAI_SAN IS NULL THEN
        OPEN MSS_OUT FOR SELECT
                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'LOAI_HINH_TAI_SAN_ID IS NULL' ) AS STRJSON
                         FROM
                             DUAL;

        RETURN;
    ELSE
        L_TAI_SAN.LOAI_HINH_TAI_SAN_ID := L_LOAI_HINH_TAI_SAN;
    END IF;
--check biENDong
--l_bien_dong_arr :=JSON_ARRAY_T(REPLACE(l_JSON_OBJECT.get_Array('LST_BIEN_DONG').stringIFy,'\\\"','\"'));

    L_BIEN_DONG_ARR := L_JSON_OBJECT.GET_ARRAY('LST_BIEN_DONG');
    IF L_BIEN_DONG_ARR.GET_SIZE = 0 THEN
        OPEN MSS_OUT FOR SELECT
                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'List BiENDong IS NULL' ) AS STRJSON
                         FROM
                             DUAL;

        RETURN;
    END IF;
--check loaiTaiSan

    IF L_LOAITAISAN_MA IS NULL THEN
        IF L_LOAITAISAN_TEN IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'LOAI_TAI_SAN_TEN IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            RETURN;
        ELSE
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'MA_LOAI_TAI_SAN IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            RETURN;
        END IF;
    ELSE
        BEGIN
            SELECT
                ID
            INTO L_LOAITAISAN_ID
            FROM
                DM_LOAI_TAI_SAN
            WHERE
                MA = L_LOAITAISAN_MA
                AND CHE_DO_HAO_MON_ID = P_CHE_DO_HAO_MON
                AND ROWNUM = 1;

        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                BEGIN
                    SELECT
                        ID
                    INTO L_LOAITAISAN_ID
                    FROM
                        DM_LOAI_TAI_SAN
                    WHERE
                        TEN = L_LOAITAISAN_TEN
                        AND CHE_DO_HAO_MON_ID = P_CHE_DO_HAO_MON
                        AND ROWNUM = 1;

                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        L_LOAITAISAN_ID := NULL;
                END;
        END;

        IF L_LOAITAISAN_ID IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'MA_LOAI_TAI_SAN('
                                                                                  || L_LOAITAISAN_MA
                                                                                  || ') NOT FOUND' ) AS STRJSON
                             FROM
                                 DUAL;

            RETURN;
        ELSE
            L_TAI_SAN.LOAI_TAI_SAN_ID := L_LOAITAISAN_ID;
        END IF;

    END IF;
--END check loaiTaiSan
--check donVi

    IF L_DONVI_MA IS NULL THEN
        OPEN MSS_OUT FOR SELECT
                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'MA_DON_VI IS NULL' ) AS STRJSON
                         FROM
                             DUAL;

        RETURN;
    ELSE
        BEGIN
            SELECT
                ID
            INTO L_DONVI_ID
            FROM
                DM_DON_VI
            WHERE
                MA = L_DONVI_MA;

        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                OPEN MSS_OUT FOR SELECT
                                     JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'MA_DON_VI('
                                                                                      || L_DONVI_MA
                                                                                      || ') NOT FOUND' ) AS STRJSON
                                 FROM
                                     DUAL;

                RETURN;
        END;
    END IF;

    L_TAI_SAN.DON_VI_ID := L_DONVI_ID;
--trạng thái
    L_TAI_SAN.TEN := UNESCAPE_STRINGJSON(L_JSON_OBJECT.GET_STRING('TEN_TAI_SAN'));
    L_TAI_SAN.MA_QLDKTS40 := L_MA_DB;
    IF L_JSON_OBJECT.GET_NUMBER('TRANG_THAI') IS NULL THEN
        OPEN MSS_OUT FOR SELECT
                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TRANG_THAI IS NULL' ) AS STRJSON
                         FROM
                             DUAL;

        RETURN;
    END IF;

    L_TAI_SAN.TRANG_THAI_ID := ( CASE
        WHEN L_JSON_OBJECT.GET_NUMBER('TRANG_THAI') = 2 THEN
            4 --Đã duyệt
        WHEN L_JSON_OBJECT.GET_NUMBER('TRANG_THAI') = 4 THEN
            8 --Đã duyệt giảm toàn bộ
        WHEN L_JSON_OBJECT.GET_NUMBER('TRANG_THAI') = 3 THEN
            5 --Từ chối
        ELSE 3 --Chờ duyệt
    END );
--

    L_TAI_SAN.QUYET_DINH_SO := UNESCAPE_STRINGJSON(L_JSON_OBJECT.GET_STRING('QUYET_DINH_SO'));
    L_TAI_SAN.QUYET_DINH_NGAY := ( CASE
        WHEN L_TAI_SAN.QUYET_DINH_SO IS NULL THEN
            NULL
        ELSE L_JSON_OBJECT.GET_DATE('QUYET_DINH_NGAY')
    END );

    L_TAI_SAN.GHI_CHU := UNESCAPE_STRINGJSON(L_JSON_OBJECT.GET_STRING('GHI_CHU'));
    L_TAI_SAN.CHUNG_TU_NGAY := L_JSON_OBJECT.GET_DATE('CHUNG_TU_NGAY');
    L_TAI_SAN.CHUNG_TU_SO := UNESCAPE_STRINGJSON(L_JSON_OBJECT.GET_STRING('CHUNG_TU_SO'));
    L_TAI_SAN.NGUYEN_GIA_BAN_DAU := ( CASE
        WHEN L_JSON_OBJECT.GET_NUMBER('NGUYEN_GIA_BAN_DAU') = 0 THEN
            NULL
        ELSE L_JSON_OBJECT.GET_NUMBER('NGUYEN_GIA_BAN_DAU')
    END );

    L_TAI_SAN.NAM_SAN_XUAT := L_JSON_OBJECT.GET_NUMBER('NAM_SAN_XUAT');
    -- L_TAI_SAN.HM_TY_LE := L_JSON_OBJECT.GET_NUMBER('HM_TY_LE');
--Nước sản xuất
    BEGIN
        SELECT
            ID
        INTO L_NSX_ID
        FROM
            DM_QUOC_GIA
        WHERE
            MA = L_NSX_MA
            AND ROWNUM = 1;

    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            L_NSX_ID := NULL;
    END;

    L_TAI_SAN.NUOC_SAN_XUAT_ID := L_NSX_ID;
--Lý do biến động
    IF L_LYDO_MA IS NULL AND L_LYDO_TEN IS NULL THEN
        OPEN MSS_OUT FOR SELECT
                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'LY_DO_BIEN_DONG_MA IS NULL' ) AS STRJSON
                         FROM
                             DUAL;

        RETURN;
    END IF;

    BEGIN
        SELECT
            ID
        INTO L_LYDO_ID
        FROM
            DM_LY_DO_BIEN_DONG
        WHERE
            MA = L_LYDO_MA
            -- AND loai_hinh_tai_san_id in(l_loai_hinh_tai_san,0) 
            AND ROWNUM = 1;

    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            BEGIN
                SELECT
                    ID
                INTO L_LYDO_ID
                FROM
                    DM_LY_DO_BIEN_DONG
                WHERE
                    TEN = L_LYDO_TEN
                -- AND loai_hinh_tai_san_id in(l_loai_hinh_tai_san,0) 
                    AND ROWNUM = 1;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    OPEN MSS_OUT FOR SELECT
                                         JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'LY_DO_BIEN_DONG_MA('
                                                                                          || L_LYDO_MA
                                                                                          || ') NOT FOUND' ) AS STRJSON
                                     FROM
                                         DUAL;

                    RETURN;
            END;
    END;

    L_TAI_SAN.LY_DO_BIEN_DONG_ID := L_LYDO_ID;
--ngày sử dụng + ngày nhập
    IF L_JSON_OBJECT.GET_DATE('NGAY_NHAP') IS NULL THEN
        OPEN MSS_OUT FOR SELECT
                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'NGAY_NHAP IS NULL' ) AS STRJSON
                         FROM
                             DUAL;

        RETURN;
    ELSE
        L_TAI_SAN.NGAY_NHAP := L_JSON_OBJECT.GET_DATE('NGAY_NHAP');
    END IF;

    IF L_JSON_OBJECT.GET_DATE('NGAY_SU_DUNG') IS NULL AND L_LOAI_HINH_TAI_SAN <> 1 THEN
        OPEN MSS_OUT FOR SELECT
                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'NGAY_SU_DUNG IS NULL' ) AS STRJSON
                         FROM
                             DUAL;

        RETURN;
    ELSE
        L_TAI_SAN.NGAY_SU_DUNG := L_JSON_OBJECT.GET_DATE('NGAY_SU_DUNG');
    END IF;
--dự án

    IF L_DUAN_MA IS NOT NULL THEN
        BEGIN
            SELECT
                ID
            INTO L_DUAN_ID
            FROM
                DM_DU_AN
            WHERE
                MA = L_DUAN_MA
                AND DON_VI_ID = L_DONVI_ID
                AND ROWNUM = 1;

            L_TAI_SAN.DU_AN_ID := L_DUAN_ID;
        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                L_DUAN_ID := NULL;
                L_TAI_SAN.DU_AN_ID := NULL;
        END;
    END IF; 
--loại tài sản đơn vị

    IF L_LOAI_HINH_TAI_SAN IN (
        9,
        10
    ) THEN
        BEGIN
            SELECT
                ID,
                MA
            INTO
                L_LOAITAISANVOHINH_ID,
                L_LOAITAISANVH_MA
            FROM
                DM_LOAI_TAI_SAN_DON_VI
            WHERE
                LOAI_TAI_SAN_ID = L_LOAITAISAN_ID
                AND LOAI_HINH_TAI_SAN_ID = L_LOAI_HINH_TAI_SAN
                AND CHE_DO_HAO_MON_ID = P_CHE_DO_HAO_MON
                AND ROWNUM = 1;

            L_TAI_SAN.LOAI_TAI_SAN_DON_VI_ID := L_LOAITAISANVOHINH_ID;
        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                BEGIN
                    SELECT
                        ID,
                        MA
                    INTO
                        L_LOAITAISANVOHINH_ID,
                        L_LOAITAISANVH_MA
                    FROM
                        DM_LOAI_TAI_SAN_DON_VI
                    WHERE
                        ID = (
                            SELECT
                                TO_NUMBER(LTRIM(SUBSTR(TREE_NODE, 0, 7), '0'))
                            FROM
                                DM_LOAI_TAI_SAN_DON_VI
                            WHERE
                                ID = L_TAI_SAN.LOAI_TAI_SAN_DON_VI_ID
                        );

                    L_TAI_SAN.LOAI_TAI_SAN_DON_VI_ID := L_LOAITAISANVOHINH_ID;
                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        OPEN MSS_OUT FOR SELECT
                                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'LOAI_TAI_SAN_DON_VI NOT FOUND' ) AS STRJSON
                                         FROM
                                             DUAL;

                        RETURN;
                END;
        END;
    END IF;

--END prepare TAI_SAN

    L_TAI_SAN.NGUON_TAI_SAN_ID := 1;
    L_TAI_SAN.NGAY_TAO := CURRENT_DATE;
    IF L_TAI_SAN.ID IS NULL THEN
        --INSERT tài sản
        INSERT INTO TS_TAI_SAN (
            TEN,
            LOAI_TAI_SAN_ID,
            DU_AN_ID,
            LOAI_HINH_TAI_SAN_ID,
            TRANG_THAI_ID,
            QUYET_DINH_SO,
            QUYET_DINH_NGAY,
            NUOC_SAN_XUAT_ID,
            LY_DO_BIEN_DONG_ID,
            DOI_TAC_ID,
            NGAY_DUYET,
            NAM_SAN_XUAT,
            NGAY_NHAP,
            NGAY_SU_DUNG,
            GHI_CHU,
            DON_VI_BO_PHAN_ID,
            DON_VI_ID,
            NGAY_TAO,
            GUID,
            CHUNG_TU_SO,
            CHUNG_TU_NGAY,
            NGUYEN_GIA_BAN_DAU,
            GIA_MUA_TIEP_NHAN,
            LOAI_TAI_SAN_DON_VI_ID,
            MA_QLDKTS40,
            NGUON_TAI_SAN_ID,
            HM_TY_LE
        ) VALUES (
            L_TAI_SAN.TEN,
            L_TAI_SAN.LOAI_TAI_SAN_ID,
            L_TAI_SAN.DU_AN_ID,
            L_TAI_SAN.LOAI_HINH_TAI_SAN_ID,
            L_TAI_SAN.TRANG_THAI_ID,
            L_TAI_SAN.QUYET_DINH_SO,
            L_TAI_SAN.QUYET_DINH_NGAY,
            L_TAI_SAN.NUOC_SAN_XUAT_ID,
            L_TAI_SAN.LY_DO_BIEN_DONG_ID,
            L_TAI_SAN.DOI_TAC_ID,
            L_TAI_SAN.NGAY_DUYET,
            L_TAI_SAN.NAM_SAN_XUAT,
            L_TAI_SAN.NGAY_NHAP,
            L_TAI_SAN.NGAY_SU_DUNG,
            L_TAI_SAN.GHI_CHU,
            L_TAI_SAN.DON_VI_BO_PHAN_ID,
            L_TAI_SAN.DON_VI_ID,
            CURRENT_DATE,
            SYS_GUID(),
            L_TAI_SAN.CHUNG_TU_SO,
            L_TAI_SAN.CHUNG_TU_NGAY,
            L_TAI_SAN.NGUYEN_GIA_BAN_DAU,
            L_TAI_SAN.GIA_MUA_TIEP_NHAN,
            L_TAI_SAN.LOAI_TAI_SAN_DON_VI_ID,
            L_TAI_SAN.MA_QLDKTS40,
            1,
            - 1
        ) RETURNING ID INTO L_ID;

    ELSE
        L_ID := L_TAI_SAN.ID;
        UPDATE TS_TAI_SAN
        SET
            TEN = L_TAI_SAN.TEN,
            LOAI_TAI_SAN_ID = L_TAI_SAN.LOAI_TAI_SAN_ID,
            DU_AN_ID = L_TAI_SAN.DU_AN_ID,
            TRANG_THAI_ID = L_TAI_SAN.TRANG_THAI_ID,
            QUYET_DINH_SO = L_TAI_SAN.QUYET_DINH_SO,
            QUYET_DINH_NGAY = L_TAI_SAN.QUYET_DINH_NGAY,
            NUOC_SAN_XUAT_ID = L_TAI_SAN.NUOC_SAN_XUAT_ID,
            LY_DO_BIEN_DONG_ID = L_TAI_SAN.LY_DO_BIEN_DONG_ID,
            DOI_TAC_ID = L_TAI_SAN.DOI_TAC_ID,
            NGAY_DUYET = L_TAI_SAN.NGAY_DUYET,
            NAM_SAN_XUAT = L_TAI_SAN.NAM_SAN_XUAT,
            NGAY_NHAP = L_TAI_SAN.NGAY_NHAP,
            NGAY_SU_DUNG = L_TAI_SAN.NGAY_SU_DUNG,
            GHI_CHU = L_TAI_SAN.GHI_CHU,
            DON_VI_BO_PHAN_ID = L_TAI_SAN.DON_VI_BO_PHAN_ID,
            DON_VI_ID = L_TAI_SAN.DON_VI_ID,
            CHUNG_TU_SO = L_TAI_SAN.CHUNG_TU_SO,
            CHUNG_TU_NGAY = L_TAI_SAN.CHUNG_TU_NGAY,
            NGUYEN_GIA_BAN_DAU = L_TAI_SAN.NGUYEN_GIA_BAN_DAU,
            GIA_MUA_TIEP_NHAN = L_TAI_SAN.GIA_MUA_TIEP_NHAN,
            LOAI_TAI_SAN_DON_VI_ID = L_TAI_SAN.LOAI_TAI_SAN_DON_VI_ID,
            NGUON_TAI_SAN_ID = 1
        WHERE
            ID = L_TAI_SAN.ID;

    END IF;
    ----DBMS_OUTPUT.PUT_LINE('l_id=' || l_id);

    L_TAI_SAN.ID := L_ID;
    IF L_LOAI_HINH_TAI_SAN IN (
        9,
        10
    ) THEN --LOAI_HINH_TAI_SAN.VO_HINH
        IF L_LOAITAISANVH_MA IS NOT NULL THEN
            L_TAI_SAN.MA := L_DONVI_MA
                            || '-'
                            || L_LOAITAISANVH_MA
                            || '-'
                            || L_ID;

        ELSE
            L_TAI_SAN.MA := L_DONVI_MA
                            || '-'
                            || L_LOAITAISAN_MA
                            || '-'
                            || L_ID;
        END IF;
    ELSE
        L_TAI_SAN.MA := L_DONVI_MA
                        || '-'
                        || L_LOAITAISAN_MA
                        || '-'
                        || L_ID;
    END IF;

    UPDATE TS_TAI_SAN
    SET
        MA = L_TAI_SAN.MA
    WHERE
        ID = L_ID;
--INSERT tài sản nhật ký

    BEGIN
        SELECT
            TAI_SAN_ID
        INTO L_TAI_SAN_NHAT_KY_ID
        FROM
            DB_TAI_SAN_NHAT_KY
        WHERE
            TAI_SAN_ID = L_ID;

        UPDATE DB_TAI_SAN_NHAT_KY
        SET
            TRANG_THAI_ID = 2,
            NGAY_CAP_NHAT = CURRENT_DATE;

    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            INSERT INTO DB_TAI_SAN_NHAT_KY (
                TAI_SAN_ID,
                LOAI_HINH_TAI_SAN_ID,
                TRANG_THAI_ID,
                NGAY_CAP_NHAT,
                IS_TAI_SAN_TOAN_DAN
            ) VALUES (
                L_ID,
                L_LOAI_HINH_TAI_SAN,
                0,
                CURRENT_DATE,
                0
            );

    END;
--BEGIN prepare từng loại tài sản

    IF L_LOAI_HINH_TAI_SAN = 1 THEN --enumLOAI_HINH_TAI_SAN.DAT
        L_TS_DAT_OBJECT := L_JSON_OBJECT.GET_OBJECT('TS_DAT');
        IF L_TS_DAT_OBJECT IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_DAT IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
            RETURN;
        END IF;

        L_DIABAN_MA := L_TS_DAT_OBJECT.GET_STRING('DIA_BAN_MA');
        BEGIN
            SELECT
                *
            INTO L_DIA_BAN
            FROM
                DM_DIA_BAN
            WHERE
                MA = L_DIABAN_MA
                AND ROWNUM = 1;

        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                L_DIA_BAN := NULL;
        END;

        L_TS_DAT.DIA_BAN_ID := L_DIA_BAN.ID;
        L_TS_DAT.DIA_CHI := UNESCAPE_STRINGJSON(L_TS_DAT_OBJECT.GET_STRING('DIA_CHI'));
        IF NVL(L_DIA_BAN.LOAI_DIA_BAN_ID, 1) = 1 THEN
            L_TS_DAT.TINH_ID := L_DIA_BAN.ID;
        ELSIF L_DIA_BAN.LOAI_DIA_BAN_ID = 2 THEN
            L_TS_DAT.HUYEN_ID := L_DIA_BAN.ID;
            L_TS_DAT.TINH_ID := L_DIA_BAN.PARENT_ID;
        ELSIF L_DIA_BAN.LOAI_DIA_BAN_ID = 3 THEN
            L_TS_DAT.XA_ID := L_DIA_BAN.ID;
            L_TS_DAT.HUYEN_ID := L_DIA_BAN.PARENT_ID;
            BEGIN
                SELECT
                    *
                INTO L_DIA_BAN
                FROM
                    DM_DIA_BAN
                WHERE
                    ID = L_TS_DAT.HUYEN_ID;

                L_TS_DAT.TINH_ID := L_DIA_BAN.PARENT_ID;
            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    L_DIA_BAN := NULL;
            END;

        END IF;
        --[TENTRUSO],[DIACHI],[XAPHUONG], [QUANHUYEN], [TINHTHANH]
        -- IF l_ts_dat.DIA_CHI IS NOT NULL THEN
        --     l_tai_san.TEN := l_tai_san.TEN || ',' || l_ts_dat.DIA_CHI;
        -- END IF;

        -- IF l_ts_dat.DIA_BAN_ID IS NOT NULL THEN
        --     SELECT 
        --         LISTAGG(dm_dia_ban.TEN, ', ') WITHIN GROUP (ORDER BY dm_dia_ban.tree_node desc) AS description INTO l_tsdat_tendiaban
        --     FROM  
        --         dm_dia_ban
        --     START WITH id = l_ts_dat.DIA_BAN_ID
        --     CONNECT BY prior parent_id = id;
        --     l_tai_san.TEN := l_tai_san.TEN || ',' || l_tsdat_tendiaban;
        -- END IF;

        L_TS_DAT.DIEN_TICH := L_TS_DAT_OBJECT.GET_NUMBER('DIEN_TICH');
        L_TS_DAT.TAI_SAN_ID := L_ID;
        --INSERT tai san dat
        INSERT INTO TS_TAI_SAN_DAT (
            TAI_SAN_ID,
            DIA_CHI,
            DIA_BAN_ID,
            DIEN_TICH,
            TINH_ID,
            HUYEN_ID,
            XA_ID
        ) VALUES (
            L_TS_DAT.TAI_SAN_ID,
            L_TS_DAT.DIA_CHI,
            L_TS_DAT.DIA_BAN_ID,
            L_TS_DAT.DIEN_TICH,
            L_TS_DAT.TINH_ID,
            L_TS_DAT.HUYEN_ID,
            L_TS_DAT.XA_ID
        );
        
        -- UPDATE
        --     TS_TAI_SAN
        -- SET
        --     TEN = l_tai_san.TEN
        -- WHERE
        --     ID = l_id;

    ELSIF L_LOAI_HINH_TAI_SAN = 7 THEN --enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV
        L_TS_CLN_OBJECT := L_JSON_OBJECT.GET_OBJECT('TS_CLN');
        IF L_TS_CLN_OBJECT IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_CLN IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
            RETURN;
        END IF;

        IF L_TS_CLN_OBJECT.GET_NUMBER('NAM_SINH') IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_CLN-NAM_SINH IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
            RETURN;
        END IF;

        L_TS_CLN.TAI_SAN_ID := L_ID;
        L_TS_CLN.NAM_SINH := L_TS_CLN_OBJECT.GET_NUMBER('NAM_SINH');
        --INSERT cay lau nam
        INSERT INTO TS_TAI_SAN_CLN (
            TAI_SAN_ID,
            NAM_SINH
        ) VALUES (
            L_TS_CLN.TAI_SAN_ID,
            L_TS_CLN.NAM_SINH
        );

    ELSIF L_LOAI_HINH_TAI_SAN = 2 THEN --enumLOAI_HINH_TAI_SAN.NHA
        L_TS_NHA_OBJECT := L_JSON_OBJECT.GET_OBJECT('TS_NHA');
        IF L_TS_NHA_OBJECT IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_NHA IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
            RETURN;
        END IF;

        IF L_TS_NHA_OBJECT.GET_NUMBER('DIEN_TICH_SAN_XAY_DUNG') IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_NHA-DIEN_TICH_SAN_XAY_DUNG IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
            RETURN;
        END IF;

        L_TS_NHA.DIEN_TICH_SAN_XAY_DUNG := L_TS_NHA_OBJECT.GET_NUMBER('DIEN_TICH_SAN_XAY_DUNG');
        IF L_TS_NHA_OBJECT.GET_NUMBER('NAM_XAY_DUNG') IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_NHA-NAM_XAY_DUNG IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
            RETURN;
        END IF;

        L_TS_NHA.NAM_XAY_DUNG := L_TS_NHA_OBJECT.GET_NUMBER('NAM_XAY_DUNG');
        L_MA_TSDAT := L_TS_NHA_OBJECT.GET_STRING('TAI_SAN_DAT_MA');
        --Lấy tài sản đất id
        IF L_MA_TSDAT IS NOT NULL THEN
            BEGIN
                SELECT
                    TS.ID
                INTO L_TS_DAT.TAI_SAN_ID
                FROM
                    TS_TAI_SAN_DAT   TSDAT,
                    TS_TAI_SAN       TS
                WHERE
                    TSDAT.TAI_SAN_ID = TS.ID
                    AND TS.MA_QLDKTS40 = L_MA_TSDAT
                    AND TS.NGUON_TAI_SAN_ID = 1
                    AND TS.TRANG_THAI_ID != 6
                    AND ROWNUM = 1;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    L_TS_DAT.TAI_SAN_ID := NULL;
            END;
        END IF;

        L_TS_NHA.TAI_SAN_DAT_ID := L_TS_DAT.TAI_SAN_ID;
        L_TS_NHA.DIA_CHI := NVL(UNESCAPE_STRINGJSON(L_TS_NHA_OBJECT.GET_STRING('DIA_CHI')), NVL(L_TS_DAT.DIA_CHI, 'Việt Nam'));

        L_TS_NHA.NGAY_SU_DUNG := L_TS_NHA_OBJECT.GET_DATE('NGAY_SU_DUNG');
        L_TS_NHA.TAI_SAN_ID := L_ID;
        L_DIABAN_MA := L_TS_NHA_OBJECT.GET_STRING('DIA_BAN_MA');
        BEGIN
            SELECT
                *
            INTO L_DIA_BAN
            FROM
                DM_DIA_BAN
            WHERE
                MA = L_DIABAN_MA
                AND ROWNUM = 1;

        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                L_DIA_BAN := NULL;
        END;

        IF NVL(L_DIA_BAN.LOAI_DIA_BAN_ID, 1) = 1 THEN
            L_TS_NHA.TINH_ID := L_DIA_BAN.ID;
        ELSIF L_DIA_BAN.LOAI_DIA_BAN_ID = 2 THEN
            L_TS_NHA.HUYEN_ID := L_DIA_BAN.ID;
            L_TS_NHA.TINH_ID := L_DIA_BAN.PARENT_ID;
        ELSIF L_DIA_BAN.LOAI_DIA_BAN_ID = 3 THEN
            L_TS_NHA.XA_ID := L_DIA_BAN.ID;
            L_TS_NHA.HUYEN_ID := L_DIA_BAN.PARENT_ID;
            BEGIN
                SELECT
                    *
                INTO L_DIA_BAN
                FROM
                    DM_DIA_BAN
                WHERE
                    ID = L_TS_NHA.HUYEN_ID;

                L_TS_NHA.TINH_ID := L_DIA_BAN.PARENT_ID;
            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    L_DIA_BAN := NULL;
            END;

        END IF;
        --INSERT tai san nha

        INSERT INTO TS_TAI_SAN_NHA (
            TAI_SAN_ID,
            TAI_SAN_DAT_ID,
            DIEN_TICH_SAN_XAY_DUNG,
            NAM_XAY_DUNG,
            NGAY_SU_DUNG,
            DIA_CHI,
            MA_DB_DAT,
            TINH_ID,
            HUYEN_ID,
            XA_ID
        ) VALUES (
            L_TS_NHA.TAI_SAN_ID,
            L_TS_NHA.TAI_SAN_DAT_ID,
            L_TS_NHA.DIEN_TICH_SAN_XAY_DUNG,
            L_TS_NHA.NAM_XAY_DUNG,
            L_TS_NHA.NGAY_SU_DUNG,
            L_TS_NHA.DIA_CHI,
            L_MA_TSDAT,
            L_TS_NHA.TINH_ID,
            L_TS_NHA.HUYEN_ID,
            L_TS_NHA.XA_ID
        );

    ELSIF L_LOAI_HINH_TAI_SAN = 4 THEN --enumLOAI_HINH_TAI_SAN.OTO
        L_TS_OTO_OBJECT := L_JSON_OBJECT.GET_OBJECT('TS_OTO');
        IF L_TS_OTO_OBJECT IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_OTO IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
            RETURN;
        END IF;

        IF L_TS_OTO_OBJECT.GET_STRING('BIEN_KIEM_SOAT') IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_OTO-BIEN_KIEM_SOAT IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
            RETURN;
        END IF;

        L_TS_OTO.BIEN_KIEM_SOAT := UNESCAPE_STRINGJSON(L_TS_OTO_OBJECT.GET_STRING('BIEN_KIEM_SOAT'));
        IF L_TS_OTO_OBJECT.GET_NUMBER('TAI_TRONG') IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_OTO-TAI_TRONG IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
            RETURN;
        END IF;

        L_TS_OTO.TAI_TRONG := L_TS_OTO_OBJECT.GET_NUMBER('TAI_TRONG');
        IF L_TS_OTO_OBJECT.GET_NUMBER('SO_CHO_NGOI') IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_OTO-SO_CHO_NGOI IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
            RETURN;
        END IF;

        L_TS_OTO.SO_CHO_NGOI := L_TS_OTO_OBJECT.GET_NUMBER('SO_CHO_NGOI');
        L_TS_OTO.CONG_XUAT := L_TS_OTO_OBJECT.GET_NUMBER('CONG_XUAT');
        L_TS_OTO.SO_KHUNG := UNESCAPE_STRINGJSON(L_TS_OTO_OBJECT.GET_STRING('SO_KHUNG'));
        L_TS_OTO.SO_MAY := UNESCAPE_STRINGJSON(L_TS_OTO_OBJECT.GET_STRING('SO_MAY'));
        L_NHANXE_MA := L_TS_OTO_OBJECT.GET_STRING('NHAN_XE_MA');
        IF L_NHANXE_MA IS NOT NULL THEN
            BEGIN
                SELECT
                    ID
                INTO L_NHANXE_ID
                FROM
                    DM_NHAN_XE
                WHERE
                    MA = L_NHANXE_MA
                    AND ROWNUM = 1;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    L_NHANXE_ID := NULL;
            END;

            L_TS_OTO.NHAN_XE_ID := L_NHANXE_ID;
        END IF;

        L_CHUCDANH_MA := L_TS_OTO_OBJECT.GET_STRING('CHUC_DANH_MA');
        ----DBMS_OUTPUT.PUT_LINE('CHUC_DANH_MA=' || l_chucdanh_ma);
        IF L_CHUCDANH_MA IS NOT NULL THEN
            BEGIN
                SELECT
                    ID
                INTO L_CHUCDANH_ID
                FROM
                    DM_CHUC_DANH
                WHERE
                    MA_CHUC_DANH = L_CHUCDANH_MA
                    AND ROWNUM = 1;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    L_CHUCDANH_ID := NULL;
            END;

            L_TS_OTO.CHUC_DANH_ID := L_CHUCDANH_ID;
        END IF;
        ----DBMS_OUTPUT.PUT_LINE('l_chucdanh_id=' || l_chucdanh_id);
        ----DBMS_OUTPUT.PUT_LINE('l_ts_oto.CHUC_DANH_ID=' || l_ts_oto.CHUC_DANH_ID);

        L_TS_OTO.TAI_SAN_ID := L_ID;
        --INSERT oto
        INSERT INTO TS_TAI_SAN_OTO (
            TAI_SAN_ID,
            BIEN_KIEM_SOAT,
            SO_CHO_NGOI,
            DUNG_TICH,
            CHUC_DANH_ID,
            TAI_TRONG,
            SO_KHUNG,
            NHAN_XE_ID,
            CONG_XUAT,
            SO_MAY
        ) VALUES (
            L_TS_OTO.TAI_SAN_ID,
            L_TS_OTO.BIEN_KIEM_SOAT,
            L_TS_OTO.SO_CHO_NGOI,
            L_TS_OTO.DUNG_TICH,
            L_TS_OTO.CHUC_DANH_ID,
            L_TS_OTO.TAI_TRONG,
            L_TS_OTO.SO_KHUNG,
            L_TS_OTO.NHAN_XE_ID,
            L_TS_OTO.CONG_XUAT,
            L_TS_OTO.SO_MAY
        );

    ELSIF L_LOAI_HINH_TAI_SAN = 5 THEN --enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC
        L_TS_PTK_OBJECT := L_JSON_OBJECT.GET_OBJECT('TS_PTK');
        IF L_TS_PTK_OBJECT IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_PTK IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
            RETURN;
        END IF;

        IF L_TS_PTK_OBJECT.GET_NUMBER('TAI_TRONG') IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_OTO-TAI_TRONG IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
            RETURN;
        END IF;

        L_TS_PTK.TAI_TRONG := L_TS_PTK_OBJECT.GET_NUMBER('TAI_TRONG');
        IF L_TS_PTK_OBJECT.GET_NUMBER('SO_CHO_NGOI') IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_OTO-SO_CHO_NGOI IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
            RETURN;
        END IF;

        L_TS_PTK.SO_CHO_NGOI := L_TS_PTK_OBJECT.GET_NUMBER('SO_CHO_NGOI');
        L_TS_PTK.CONG_XUAT := L_TS_PTK_OBJECT.GET_NUMBER('CONG_XUAT');
        L_TS_PTK.SO_KHUNG := UNESCAPE_STRINGJSON(L_TS_PTK_OBJECT.GET_STRING('SO_KHUNG'));
        L_TS_PTK.SO_MAY := UNESCAPE_STRINGJSON(L_TS_PTK_OBJECT.GET_STRING('SO_MAY'));
        L_NHANXE_MA := L_TS_PTK_OBJECT.GET_STRING('NHAN_XE_MA');
        IF L_NHANXE_MA IS NOT NULL THEN
            BEGIN
                SELECT
                    ID
                INTO L_NHANXE_ID
                FROM
                    DM_NHAN_XE
                WHERE
                    MA = L_NHANXE_MA
                    AND ROWNUM = 1;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    L_NHANXE_ID := NULL;
            END;

            L_TS_PTK.NHAN_XE_ID := L_NHANXE_ID;
        END IF;

        L_CHUCDANH_MA := L_TS_PTK_OBJECT.GET_STRING('CHUC_DANH_MA');
        IF L_CHUCDANH_MA IS NOT NULL THEN
            BEGIN
                SELECT
                    ID
                INTO L_CHUCDANH_ID
                FROM
                    DM_CHUC_DANH
                WHERE
                    MA_CHUC_DANH = L_CHUCDANH_MA
                    AND ROWNUM = 1;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    L_CHUCDANH_ID := NULL;
            END;

            L_TS_PTK.CHUC_DANH_ID := L_CHUCDANH_ID;
        END IF;

        L_TS_PTK.TAI_SAN_ID := L_ID;
        --INSERT ptk
        INSERT INTO TS_TAI_SAN_OTO (
            TAI_SAN_ID,
            BIEN_KIEM_SOAT,
            SO_CHO_NGOI,
            DUNG_TICH,
            CHUC_DANH_ID,
            TAI_TRONG,
            SO_KHUNG,
            NHAN_XE_ID,
            CONG_XUAT,
            SO_MAY
        ) VALUES (
            L_TS_PTK.TAI_SAN_ID,
            NULL,
            L_TS_PTK.SO_CHO_NGOI,
            L_TS_PTK.DUNG_TICH,
            L_TS_PTK.CHUC_DANH_ID,
            L_TS_PTK.TAI_TRONG,
            L_TS_PTK.SO_KHUNG,
            L_TS_PTK.NHAN_XE_ID,
            L_TS_PTK.CONG_XUAT,
            L_TS_PTK.SO_MAY
        );

    ELSIF L_LOAI_HINH_TAI_SAN = 3 THEN --enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC
        L_TS_VKT_OBJECT := L_JSON_OBJECT.GET_OBJECT('TS_VKT');
        IF L_TS_VKT_OBJECT IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_VKT IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
            RETURN;
        END IF;

        L_TS_VKT.CHIEU_DAI := NVL(L_TS_VKT_OBJECT.GET_NUMBER('CHIEU_DAI'), 0);
        L_TS_VKT.CHIEU_DAI := NVL(L_TS_VKT_OBJECT.GET_NUMBER('THE_TICH'), 0);
        L_TS_VKT.CHIEU_DAI := NVL(L_TS_VKT_OBJECT.GET_NUMBER('DIEN_TICH'), 0);
        L_TS_VKT.TAI_SAN_ID := L_ID;
    --INSERT vkt
        INSERT INTO TS_TAI_SAN_VKT (
            TAI_SAN_ID,
            DIEN_TICH,
            THE_TICH,
            CHIEU_DAI
        ) VALUES (
            L_TS_VKT.TAI_SAN_ID,
            L_TS_VKT.DIEN_TICH,
            L_TS_VKT.THE_TICH,
            L_TS_VKT.CHIEU_DAI
        );

    ELSIF L_LOAI_HINH_TAI_SAN = 10 THEN --enumLOAI_HINH_TAI_SAN.DAC_THU
        L_TS_DACTHU_OBJECT := L_JSON_OBJECT.GET_OBJECT('TS_DAC_THU');
        IF L_TS_DACTHU_OBJECT IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_DAC_THU IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
            RETURN;
        END IF;

        L_TS_DACTHU.TAI_SAN_ID := L_ID;
        L_TS_DACTHU.THONG_SO_KY_THUAT := UNESCAPE_STRINGJSON(L_TS_DACTHU_OBJECT.GET_STRING('THONG_SO_KY_THUAT'));
        L_TS_DACTHU.THONG_SO_KY_HIEU := UNESCAPE_STRINGJSON(L_TS_DACTHU_OBJECT.GET_STRING('THONG_SO_KY_HIEU'));
        L_TS_DACTHU.PHU_KIEN_JSON := L_TS_DACTHU_OBJECT.GET_STRING('PHU_KIEN_JSON');
    --INSERT dac thu
        INSERT INTO TS_TAI_SAN_MAY_MOC (
            TAI_SAN_ID,
            THONG_SO_KY_THUAT,
            PHU_KIEN_JSON,
            THONG_SO_KY_HIEU
        ) VALUES (
            L_TS_DACTHU.TAI_SAN_ID,
            L_TS_DACTHU.THONG_SO_KY_THUAT,
            L_TS_DACTHU.PHU_KIEN_JSON,
            L_TS_DACTHU.THONG_SO_KY_HIEU
        );

    ELSIF L_LOAI_HINH_TAI_SAN = 8 THEN --enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC
        L_TS_HUUHINH_OBJECT := L_JSON_OBJECT.GET_OBJECT('TS_HUU_HINH_KHAC');
        IF L_TS_HUUHINH_OBJECT IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_HUU_HINH_KHAC IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
            RETURN;
        END IF;

        L_TS_HUUHINH.TAI_SAN_ID := L_ID;
        L_TS_HUUHINH.THONG_SO_KY_THUAT := UNESCAPE_STRINGJSON(L_TS_HUUHINH_OBJECT.GET_STRING('THONG_SO_KY_THUAT'));
        L_TS_HUUHINH.THONG_SO_KY_HIEU := UNESCAPE_STRINGJSON(L_TS_HUUHINH_OBJECT.GET_STRING('THONG_SO_KY_HIEU'));
        L_TS_HUUHINH.PHU_KIEN_JSON := L_TS_HUUHINH_OBJECT.GET_STRING('PHU_KIEN_JSON');
    --INSERT huu hinh
        INSERT INTO TS_TAI_SAN_MAY_MOC (
            TAI_SAN_ID,
            THONG_SO_KY_THUAT,
            PHU_KIEN_JSON,
            THONG_SO_KY_HIEU
        ) VALUES (
            L_TS_HUUHINH.TAI_SAN_ID,
            L_TS_HUUHINH.THONG_SO_KY_THUAT,
            L_TS_HUUHINH.PHU_KIEN_JSON,
            L_TS_HUUHINH.THONG_SO_KY_HIEU
        );

    ELSIF L_LOAI_HINH_TAI_SAN = 6 THEN --enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI
        L_TS_MAYMOC_OBJECT := L_JSON_OBJECT.GET_OBJECT('TS_MAY_MOC');
        IF L_TS_MAYMOC_OBJECT IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_MAY_MOC IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
            RETURN;
        END IF;

        L_TS_MAYMOC.TAI_SAN_ID := L_ID;
        L_TS_MAYMOC.THONG_SO_KY_THUAT := UNESCAPE_STRINGJSON(L_TS_MAYMOC_OBJECT.GET_STRING('THONG_SO_KY_THUAT'));
        L_TS_MAYMOC.THONG_SO_KY_HIEU := UNESCAPE_STRINGJSON(L_TS_MAYMOC_OBJECT.GET_STRING('THONG_SO_KY_HIEU'));
        L_TS_MAYMOC.PHU_KIEN_JSON := L_TS_MAYMOC_OBJECT.GET_STRING('PHU_KIEN_JSON');
    --INSERT may moc
        INSERT INTO TS_TAI_SAN_MAY_MOC (
            TAI_SAN_ID,
            THONG_SO_KY_THUAT,
            PHU_KIEN_JSON,
            THONG_SO_KY_HIEU
        ) VALUES (
            L_TS_MAYMOC.TAI_SAN_ID,
            L_TS_MAYMOC.THONG_SO_KY_THUAT,
            L_TS_MAYMOC.PHU_KIEN_JSON,
            L_TS_MAYMOC.THONG_SO_KY_HIEU
        );

    ELSIF L_LOAI_HINH_TAI_SAN = 9 THEN --enumLOAI_HINH_TAI_SAN.VO_HINH
        L_TS_VOHINH_OBJECT := L_JSON_OBJECT.GET_OBJECT('TS_VO_HINH');
        IF L_TS_VOHINH_OBJECT IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_VO_HINH IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
            RETURN;
        END IF;

        L_TS_MAYMOC.TAI_SAN_ID := L_ID;
        L_TS_MAYMOC.THONG_SO_KY_THUAT := UNESCAPE_STRINGJSON(L_TS_VOHINH_OBJECT.GET_STRING('THONG_SO_KY_THUAT'));
        --INSERT vo hinh
        INSERT INTO TS_TAI_SAN_VO_HINH (
            TAI_SAN_ID,
            THONG_SO_KY_THUAT
        ) VALUES (
            L_TS_MAYMOC.TAI_SAN_ID,
            L_TS_MAYMOC.THONG_SO_KY_THUAT
        );

    END IF;
    --END prepare từng loại tài sản

    L_BD_HTSD_JSON := '';
    FOR I IN 0..L_BIEN_DONG_ARR.GET_SIZE - 1 LOOP
        L_BIEN_DONG_OBJECT := TREAT(L_BIEN_DONG_ARR.GET(I) AS JSON_OBJECT_T);
--        IF L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') = 1 THEN
--            CONTINUE;
--            END IF;
        L_HIEN_TRANG_ARR := L_BIEN_DONG_OBJECT.GET_ARRAY('HIEN_TRANG_BD');
        L_NGUON_VON_ARR := L_BIEN_DONG_OBJECT.GET_ARRAY('NGUON_VON_BD');
        L_BD_HTSD_JSON := '';
        FOR HT IN 0..L_HIEN_TRANG_ARR.GET_SIZE - 1 LOOP
            L_HIEN_TRANG_OBJECT := TREAT(L_HIEN_TRANG_ARR.GET(HT) AS JSON_OBJECT_T);
            L_BD_HTSD_JSON := L_BD_HTSD_JSON
                              || '{"HienTrangId":'
                              || L_HIEN_TRANG_OBJECT.GET_NUMBER('HIEN_TRANG_ID')
                              || ',"GiaTriText":"'
                              || L_HIEN_TRANG_OBJECT.GET_NUMBER('GIA_TRI_TEXT')
                              || '","GiaTriNumber":'
                              || ABS(NVL(L_HIEN_TRANG_OBJECT.GET_NUMBER('GIA_TRI_NUMBER'), 0))
                              || ',"GiaTriCheckbox":'
                              || L_HIEN_TRANG_OBJECT.GET_STRING('GIA_TRI_CHECKBOX')
                              || ',"NhomHienTrangId":'
                              || L_HIEN_TRANG_OBJECT.GET_NUMBER('NHOM_HIEN_TRANG_ID')
                              || ',"TenHienTrang":"'
                              || UNESCAPE_STRINGJSON(L_HIEN_TRANG_OBJECT.GET_STRING('TEN_HIEN_TRANG'))
                              || '","KieuDuLieuId":'
                              || L_HIEN_TRANG_OBJECT.GET_NUMBER('KIEU_DU_LIEU_ID')
                              || '},';

        END LOOP;

        L_BD_HTSD_JSON := '{"TaiSanId":'
                          || L_ID
                          || ',
                                    "lstObjHienTrang":['
                          || REPLACE(L_BD_HTSD_JSON, '":,', '":null,')
                          || ']}';

        L_BD_HTSD_JSON := REPLACE(L_BD_HTSD_JSON, '},]', '}]');
        FOR NV IN 0..L_NGUON_VON_ARR.GET_SIZE - 1 LOOP
            L_NGUON_VON_OBJECT := TREAT(L_NGUON_VON_ARR.GET(NV) AS JSON_OBJECT_T);
            
            L_BD_NV_JSON := L_BD_NV_JSON
                            || '{
                                "ID":'
                            || L_NGUON_VON_OBJECT.GET_NUMBER('NGUON_VON_ID')
                            || ',
                                "TEN":"'
                            || L_NGUON_VON_OBJECT.GET_NUMBER('TEN_NGUON_VON')
                            || '",
                                "LoaiHinhTaiSanId":'
                            || L_LOAI_HINH_TAI_SAN
                            || ',
                                "GiaTri":'
                            || (
                CASE
                    WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                        3,
                        6
                    ) THEN
                        -ABS(NVL(L_NGUON_VON_OBJECT.GET_NUMBER('GIA_TRI'), 0))
                    WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                        4,
                        5,
                        7
                    ) THEN
                        0
                    ELSE ABS(NVL(L_NGUON_VON_OBJECT.GET_NUMBER('GIA_TRI'), 0))
                END
            )
                            || '},';

        END LOOP;

        L_BD_NV_JSON := '['
                        || REPLACE(L_BD_NV_JSON, '":,', '":null,')
                        || ']';
        L_BD_NV_JSON := REPLACE(L_BD_NV_JSON, '},]', '}]');
        --check loaiTaiSan
        L_LOAITAISANBD_MA := NVL(L_BIEN_DONG_OBJECT.GET_STRING('LOAI_TAI_SAN_MA'), L_LOAITAISAN_MA);
        IF L_LOAITAISANBD_MA IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG - LOAI_TAI_SAN_MA IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
            RETURN;
        ELSE
            BEGIN
                SELECT
                    ID,
                    LOAI_HINH_TAI_SAN_ID
                INTO
                    L_LOAITAISAN_ID,
                    L_LOAI_HINH_TAI_SAN_BD_FOR_LTS
                FROM
                    DM_LOAI_TAI_SAN
                WHERE
                    MA = L_LOAITAISANBD_MA
                    AND CHE_DO_HAO_MON_ID = P_CHE_DO_HAO_MON
                    AND ROWNUM = 1;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    BEGIN
                        SELECT
                            ID,
                            LOAI_HINH_TAI_SAN_ID
                        INTO
                            L_LOAITAISAN_ID,
                            L_LOAI_HINH_TAI_SAN_BD_FOR_LTS
                        FROM
                            DM_LOAI_TAI_SAN
                        WHERE
                            TEN = L_LOAITAISAN_TEN
                            AND CHE_DO_HAO_MON_ID = P_CHE_DO_HAO_MON
                            AND ROWNUM = 1;

                    EXCEPTION
                        WHEN NO_DATA_FOUND THEN
                            L_LOAITAISAN_ID := NULL;
                    END;
            END;

            IF L_LOAI_HINH_TAI_SAN_BD_FOR_LTS <> L_LOAI_HINH_TAI_SAN THEN
                L_LOAITAISANBD_MA := L_LOAITAISAN_MA;
                L_LOAITAISAN_ID := L_TAI_SAN.LOAI_TAI_SAN_ID;
            END IF;

            IF L_LOAITAISAN_ID IS NULL THEN
                OPEN MSS_OUT FOR SELECT
                                     JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG - LOAI_TAI_SAN_MA('
                                                                                      || L_LOAITAISANBD_MA
                                                                                      || ') NOT FOUND' ) AS STRJSON
                                 FROM
                                     DUAL;

                --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
                RETURN;
            END IF;

        END IF;
--END check loaiTaiSan

        IF L_BIEN_DONG_OBJECT.GET_NUMBER('TRANG_THAI') = 2 THEN --prepare biENDong
            L_BD.TAI_SAN_ID := L_ID;
            L_BD.TAI_SAN_MA := L_TAI_SAN.MA;
            L_BD.TAI_SAN_TEN := L_TAI_SAN.TEN;
            L_BD.LOAI_HINH_TAI_SAN_ID := L_LOAI_HINH_TAI_SAN;
            L_BD.LOAI_TAI_SAN_DON_VI_ID := L_TAI_SAN.LOAI_TAI_SAN_DON_VI_ID;
            L_BD.DON_VI_ID := L_TAI_SAN.DON_VI_ID;
            L_BD.LOAI_TAI_SAN_ID := L_LOAITAISAN_ID;
            L_BD.NGUYEN_GIA := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                    3,
                    6
                ) THEN
                    -ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('NGUYEN_GIA'))
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                    4,
                    5,
                    7
                ) THEN
                    0
                ELSE ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('NGUYEN_GIA'))
            END );

            L_BD.CHUNG_TU_SO := UNESCAPE_STRINGJSON(L_BIEN_DONG_OBJECT.GET_STRING('CHUNG_TU_SO'));
            L_BD.CHUNG_TU_NGAY := L_BIEN_DONG_OBJECT.GET_DATE('CHUNG_TU_NGAY');
            L_BD.NGAY_BIEN_DONG := L_BIEN_DONG_OBJECT.GET_DATE('NGAY_BIEN_DONG');
            L_BD.NGAY_DUYET := L_BIEN_DONG_OBJECT.GET_DATE('NGAY_DUYET');
            L_BD.NGAY_SU_DUNG := L_BIEN_DONG_OBJECT.GET_DATE('NGAY_SU_DUNG');
            L_BD.LOAI_BIEN_DONG_ID := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') = 4 THEN
                    11
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') = 7 THEN
                    5
                ELSE L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID')
            END );

            L_BD.TRANG_THAI_ID := 3; --trang thai da duyet
            L_BD.NGAY_TAO := NVL(L_BIEN_DONG_OBJECT.GET_DATE('NGAY_TAO'), CURRENT_DATE);
            L_BD.GHI_CHU := UNESCAPE_STRINGJSON(L_BIEN_DONG_OBJECT.GET_STRING('GHI_CHU'));
            L_BD.QUYET_DINH_SO := UNESCAPE_STRINGJSON(L_BIEN_DONG_OBJECT.GET_STRING('QUYET_DINH_SO'));
            L_BD.QUYET_DINH_NGAY := ( CASE
                WHEN L_BD.QUYET_DINH_SO IS NULL THEN
                    NULL
                ELSE L_BIEN_DONG_OBJECT.GET_DATE('QUYET_DINH_NGAY')
            END );

            L_BD.GUID := SYS_GUID();
            L_BD.IS_BIENDONG_CUOI := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_STRING('IS_BIENDONG_CUOI') = 'true' THEN
                    1
                ELSE 0
            END );

            L_BD.ID_DB := L_BIEN_DONG_OBJECT.GET_STRING('GUID');
            L_LYDO_MA := L_BIEN_DONG_OBJECT.GET_STRING('LY_DO_BIEN_DONG_MA');
            L_LYDO_TEN := UNESCAPE_STRINGJSON(L_BIEN_DONG_OBJECT.GET_STRING('LY_DO_BIEN_DONG_TEN'));
            IF L_LYDO_MA IS NULL AND L_LYDO_TEN IS NULL THEN
                OPEN MSS_OUT FOR SELECT
                                     JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-LY_DO_BIEN_DONG_MA IS NULL' ) AS STRJSON
                                 FROM
                                     DUAL;

                --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
                RETURN;
            END IF;

            BEGIN
                SELECT
                    ID
                INTO L_LYDO_ID
                FROM
                    DM_LY_DO_BIEN_DONG
                WHERE
                    MA = L_LYDO_MA
                    -- AND loai_hinh_tai_san_id in(l_loai_hinh_tai_san,0) 
                    AND ROWNUM = 1;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    BEGIN
                        SELECT
                            ID
                        INTO L_LYDO_ID
                        FROM
                            DM_LY_DO_BIEN_DONG
                        WHERE
                            TEN = L_LYDO_TEN
                        -- AND loai_hinh_tai_san_id in(l_loai_hinh_tai_san,0) 
                            AND ROWNUM = 1;

                    EXCEPTION
                        WHEN NO_DATA_FOUND THEN
                            OPEN MSS_OUT FOR SELECT
                                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-LY_DO_BIEN_DONG_MA('
                                                                                                  || L_LYDO_MA
                                                                                                  || ') NOT FOUND' ) AS STRJSON
                                             FROM
                                                 DUAL;

                            --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
                            RETURN;
                    END;
            END;

            L_BD.LY_DO_BIEN_DONG_ID := L_LYDO_ID;
            L_DONVI_BOPHAN_MA := L_BIEN_DONG_OBJECT.GET_STRING('DON_VI_BO_PHAN_MA');
            IF L_DONVI_BOPHAN_MA IS NOT NULL THEN
                BEGIN
                    SELECT
                        ID
                    INTO L_DONVI_BOPHAN_ID
                    FROM
                        DM_DON_VI_BO_PHAN
                    WHERE
                        MA = L_DONVI_BOPHAN_MA
                        AND ROWNUM = 1;

                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        OPEN MSS_OUT FOR SELECT
                                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-DON_VI_BO_PHAN_MA('
                                                                                              || L_DONVI_BOPHAN_MA
                                                                                              || ') NOT FOUND' ) AS STRJSON
                                         FROM
                                             DUAL;

                        --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
                        RETURN;
                END;

                L_BD.DON_VI_BO_PHAN_ID := L_DONVI_BOPHAN_ID;
            END IF;

            L_DIABAN_MA := L_BIEN_DONG_OBJECT.GET_STRING('DIA_BAN_MA');
            BEGIN
                SELECT
                    *
                INTO L_DIA_BAN
                FROM
                    DM_DIA_BAN
                WHERE
                    MA = L_DIABAN_MA
                    AND ROWNUM = 1;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    L_DIA_BAN := NULL;
            END;

            L_BD_CT.DIA_BAN_ID := L_DIA_BAN.ID;
            IF NVL(L_DIA_BAN.LOAI_DIA_BAN_ID, 1) = 1 THEN
                L_BD.TINH_ID := L_DIA_BAN.ID;
            ELSIF L_DIA_BAN.LOAI_DIA_BAN_ID = 2 THEN
                L_BD.HUYEN_ID := L_DIA_BAN.ID;
                L_BD.TINH_ID := L_DIA_BAN.PARENT_ID;
            ELSIF L_DIA_BAN.LOAI_DIA_BAN_ID = 3 THEN
                L_BD.XA_ID := L_DIA_BAN.ID;
                L_BD.HUYEN_ID := L_DIA_BAN.PARENT_ID;
                BEGIN
                    SELECT
                        *
                    INTO L_DIA_BAN
                    FROM
                        DM_DIA_BAN
                    WHERE
                        ID = L_BD.HUYEN_ID;

                    L_BD.TINH_ID := L_DIA_BAN.PARENT_ID;
                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        L_DIA_BAN := NULL;
                END;

            END IF;

            DBMS_OUTPUT.PUT_LINE('l_bd.TINH_ID = ' || L_BD.TINH_ID);
            
        --INSERT bien dong
            INSERT INTO BD_BIEN_DONG (
                TAI_SAN_ID,
                TAI_SAN_MA,
                TAI_SAN_TEN,
                LOAI_TAI_SAN_ID,
                LOAI_HINH_TAI_SAN_ID,
                NGUYEN_GIA,
                DON_VI_BO_PHAN_ID,
                CHUNG_TU_SO,
                CHUNG_TU_NGAY,
                NGAY_BIEN_DONG,
                NGAY_SU_DUNG,
                LOAI_BIEN_DONG_ID,
                LY_DO_BIEN_DONG_ID,
                TRANG_THAI_ID,
                DON_VI_ID,
                NGAY_TAO,
                GUID,
                GHI_CHU,
                QUYET_DINH_SO,
                QUYET_DINH_NGAY,
                IS_BIENDONG_CUOI,
                LOAI_TAI_SAN_DON_VI_ID,
                NGAY_DUYET,
                NGUOI_DUYET_ID,
                ID_DB,
                TINH_ID,
                HUYEN_ID,
                XA_ID
            ) VALUES (
                L_ID,
                L_BD.TAI_SAN_MA,
                L_BD.TAI_SAN_TEN,
                L_BD.LOAI_TAI_SAN_ID,
                L_BD.LOAI_HINH_TAI_SAN_ID,
                L_BD.NGUYEN_GIA,
                L_BD.DON_VI_BO_PHAN_ID,
                L_BD.CHUNG_TU_SO,
                L_BD.CHUNG_TU_NGAY,
                L_BD.NGAY_BIEN_DONG,
                L_BD.NGAY_SU_DUNG,
                L_BD.LOAI_BIEN_DONG_ID,
                L_BD.LY_DO_BIEN_DONG_ID,
                3,
                L_BD.DON_VI_ID,
                CURRENT_DATE,
                SYS_GUID(),
                L_BD.GHI_CHU,
                L_BD.QUYET_DINH_SO,
                L_BD.QUYET_DINH_NGAY,
                L_BD.IS_BIENDONG_CUOI,
                L_BD.LOAI_TAI_SAN_DON_VI_ID,
                L_BD.NGAY_DUYET,
                L_BD.NGUOI_DUYET_ID,
                L_BD.ID_DB,
                L_BD.TINH_ID,
                L_BD.HUYEN_ID,
                L_BD.XA_ID
            ) RETURNING ID INTO L_BD_ID;
        --prepare bien dong chi tiet

            L_BD_CT.BIEN_DONG_ID := L_BD_ID;
            L_BD_CT.HTSD_JSON := L_BD_HTSD_JSON;
            L_HINH_THUC_MUA_SAM_MA := L_BIEN_DONG_OBJECT.GET_STRING('HINH_THUC_MUA_SAM_MA');
            IF L_HINH_THUC_MUA_SAM_MA IS NOT NULL THEN
                BEGIN
                    SELECT
                        ID
                    INTO L_HINH_THUC_MUA_SAM_ID
                    FROM
                        DM_HINH_THUC_MUA_SAM
                    WHERE
                        MA = L_HINH_THUC_MUA_SAM_MA
                        AND ROWNUM = 1;

                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        OPEN MSS_OUT FOR SELECT
                                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-HINH_THUC_MUA_SAM_MA('
                                                                                              || L_HINH_THUC_MUA_SAM_MA
                                                                                              || ') NOT FOUND' ) AS STRJSON
                                         FROM
                                             DUAL;

                        --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
                        RETURN;
                END;

                L_BD_CT.HINH_THUC_MUA_SAM_ID := L_HINH_THUC_MUA_SAM_ID;
            END IF;

            L_MUC_DICH_SU_DUNG_MA := L_BIEN_DONG_OBJECT.GET_STRING('MUC_DICH_SU_DUNG_MA');
            IF L_MUC_DICH_SU_DUNG_MA IS NOT NULL THEN
                BEGIN
                    SELECT
                        ID
                    INTO L_MUC_DICH_SU_DUNG_ID
                    FROM
                        DM_MUC_DICH_SU_DUNG
                    WHERE
                        MA = L_MUC_DICH_SU_DUNG_MA
                        AND ROWNUM = 1;

                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        OPEN MSS_OUT FOR SELECT
                                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-MUC_DICH_SU_DUNG_MA('
                                                                                              || L_MUC_DICH_SU_DUNG_MA
                                                                                              || ') NOT FOUND' ) AS STRJSON
                                         FROM
                                             DUAL;

                        --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
                        RETURN;
                END;

                L_BD_CT.MUC_DICH_SU_DUNG_ID := L_MUC_DICH_SU_DUNG_ID;
            END IF;

            L_CHUCDANH_MA := L_BIEN_DONG_OBJECT.GET_STRING('OTO_CHUC_DANH_MA');
            IF L_CHUCDANH_MA IS NOT NULL THEN
                BEGIN
                    SELECT
                        ID
                    INTO L_CHUCDANH_ID
                    FROM
                        DM_CHUC_DANH
                    WHERE
                        MA_CHUC_DANH = L_CHUCDANH_MA
                        AND ROWNUM = 1;

                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        OPEN MSS_OUT FOR SELECT
                                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-OTO_CHUC_DANH_MA('
                                                                                              || L_CHUCDANH_MA
                                                                                              || ') NOT FOUND' ) AS STRJSON
                                         FROM
                                             DUAL;

                        --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
                        RETURN;
                END;

                L_BD_CT.OTO_CHUC_DANH_ID := L_CHUCDANH_ID;
            END IF;

            L_NHANXE_MA := L_BIEN_DONG_OBJECT.GET_STRING('OTO_NHAN_XE_MA');
            IF L_NHANXE_MA IS NOT NULL THEN
                BEGIN
                    SELECT
                        ID
                    INTO L_NHANXE_ID
                    FROM
                        DM_NHAN_XE
                    WHERE
                        MA = L_NHANXE_MA
                        AND ROWNUM = 1;

                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        OPEN MSS_OUT FOR SELECT
                                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-OTO_NHAN_XE_MA('
                                                                                              || L_NHANXE_MA
                                                                                              || ') NOT FOUND' ) AS STRJSON
                                         FROM
                                             DUAL;

                        --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
                        RETURN;
                END;

                L_BD_CT.OTO_NHAN_XE_ID := L_NHANXE_ID;
            END IF;

            L_DONVI_DIEU_CHUYEN_MA := L_BIEN_DONG_OBJECT.GET_STRING('DON_VI_NHAN_DIEU_CHUYEN_MA');
            IF L_DONVI_DIEU_CHUYEN_MA IS NOT NULL THEN
                BEGIN
                    SELECT
                        ID
                    INTO L_DONVI_DIEU_CHUYEN_ID
                    FROM
                        DM_DON_VI
                    WHERE
                        MA = L_DONVI_DIEU_CHUYEN_MA
                        AND ROWNUM = 1;

                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        OPEN MSS_OUT FOR SELECT
                                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-DON_VI_NHAN_DIEU_CHUYEN_MA('
                                                                                              || L_DONVI_DIEU_CHUYEN_MA
                                                                                              || ') NOT FOUND' ) AS STRJSON
                                         FROM
                                             DUAL;

                        --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
                        RETURN;
                END;

                L_BD_CT.DON_VI_NHAN_DIEU_CHUYEN_ID := L_DONVI_DIEU_CHUYEN_ID;
            END IF;

            L_HINH_THUC_XU_LY_MA := L_BIEN_DONG_OBJECT.GET_STRING('HINH_THUC_XU_LY_MA');
            IF L_HINH_THUC_XU_LY_MA IS NOT NULL THEN
                BEGIN
                    SELECT
                        ID
                    INTO L_HINH_THUC_XU_LY_ID
                    FROM
                        DM_DON_VI
                    WHERE
                        MA = L_HINH_THUC_XU_LY_MA
                        AND ROWNUM = 1;

                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        OPEN MSS_OUT FOR SELECT
                                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-HINH_THUC_XU_LY_MA('
                                                                                              || L_NHANXE_MA
                                                                                              || ') NOT FOUND' ) AS STRJSON
                                         FROM
                                             DUAL;

                        --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
                        RETURN;
                END;

                L_BD_CT.HINH_THUC_XU_LY_ID := L_HINH_THUC_XU_LY_ID;
            END IF;

            L_BD_CT.NHAN_HIEU := L_BIEN_DONG_OBJECT.GET_STRING('NHAN_HIEU');
            L_BD_CT.SO_HIEU := UNESCAPE_STRINGJSON(L_BIEN_DONG_OBJECT.GET_STRING('SO_HIEU'));
            --L_BD_CT.DATA_JSON := REPLACE(REPLACE(L_BIEN_DONG_OBJECT.GET_STRING('DATA_JSON'),'\"','"'), '\\', '\');
            L_BD_CT.DATA_JSON := L_BIEN_DONG_OBJECT.GET_STRING('DATA_JSON');

            L_BD_CT.NGUYEN_GIA := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                    3,
                    6
                ) THEN
                    -ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('NGUYEN_GIA'))
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                    4,
                    5,
                    7
                ) THEN
                    0
                ELSE ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('NGUYEN_GIA'))
            END );

            L_BD_CT.DAT_TONG_DIEN_TICH := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                    3,
                    6
                ) THEN
                    -ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('DAT_TONG_DIEN_TICH'))
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                    4,
                    5,
                    7
                ) THEN
                    0
                ELSE ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('DAT_TONG_DIEN_TICH'))
            END );

            L_BD_CT.HM_SO_NAM_CON_LAI := L_BIEN_DONG_OBJECT.GET_NUMBER('HM_SO_NAM_CON_LAI');
            L_BD_CT.HM_TY_LE_HAO_MON := L_BIEN_DONG_OBJECT.GET_NUMBER('HM_TY_LE_HAO_MON');
            L_BD_CT.HM_LUY_KE := L_BIEN_DONG_OBJECT.GET_NUMBER('HM_LUY_KE');
            L_BD_CT.HM_GIA_TRI_CON_LAI := L_BIEN_DONG_OBJECT.GET_NUMBER('GIA_TRI_CON_LAI');
            L_BD_CT.KH_NGAY_BAT_DAU := L_BIEN_DONG_OBJECT.GET_DATE('KH_NGAY_BAT_DAU');
            L_BD_CT.KH_THANG_CON_LAI := L_BIEN_DONG_OBJECT.GET_NUMBER('KH_THANG_CON_LAI');
            L_BD_CT.KH_GIA_TINH_KHAU_HAO := L_BIEN_DONG_OBJECT.GET_NUMBER('KH_GIA_TINH_KHAU_HAO');
            L_BD_CT.KH_GIA_TRI_TRICH_THANG := L_BIEN_DONG_OBJECT.GET_NUMBER('KH_GIA_TRI_TRICH_THANG');
            L_BD_CT.KH_LUY_KE := L_BIEN_DONG_OBJECT.GET_NUMBER('KH_LUY_KE');
            L_BD_CT.KH_CON_LAI := L_BIEN_DONG_OBJECT.GET_NUMBER('KH_CON_LAI');
            L_BD_CT.NHA_SO_TANG := L_BIEN_DONG_OBJECT.GET_NUMBER('NHA_SO_TANG');
            L_BD_CT.NHA_NAM_XAY_DUNG := L_BIEN_DONG_OBJECT.GET_NUMBER('NHA_NAM_XAY_DUNG');
            L_BD_CT.NHA_DIEN_TICH_XD := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                    3,
                    6
                ) THEN
                    -ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('NHA_DIEN_TICH_XD'))
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                    4,
                    5,
                    7
                ) THEN
                    0
                ELSE ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('NHA_DIEN_TICH_XD'))
            END );

            L_BD_CT.NHA_TONG_DIEN_TICH_XD := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                    3,
                    6
                ) THEN
                    -ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('NHA_TONG_DIEN_TICH_XD'))
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                    4,
                    5,
                    7
                ) THEN
                    0
                ELSE ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('NHA_TONG_DIEN_TICH_XD'))
            END );

            L_BD_CT.VKT_DIEN_TICH := L_BIEN_DONG_OBJECT.GET_NUMBER('VKT_DIEN_TICH');
            L_BD_CT.VKT_THE_TICH := L_BIEN_DONG_OBJECT.GET_NUMBER('VKT_THE_TICH');
            L_BD_CT.VKT_CHIEU_DAI := L_BIEN_DONG_OBJECT.GET_NUMBER('VKT_CHIEU_DAI');
            L_BD_CT.OTO_BIEN_KIEM_SOAT := UNESCAPE_STRINGJSON(L_BIEN_DONG_OBJECT.GET_STRING('OTO_BIEN_KIEM_SOAT'));
            L_BD_CT.OTO_SO_CHO_NGOI := L_BIEN_DONG_OBJECT.GET_NUMBER('OTO_SO_CHO_NGOI');
            L_BD_CT.THONG_SO_KY_THUAT := UNESCAPE_STRINGJSON(L_BIEN_DONG_OBJECT.GET_STRING('THONG_SO_KY_THUAT'));
            L_BD_CT.OTO_TAI_TRONG := L_BIEN_DONG_OBJECT.GET_NUMBER('OTO_TAI_TRONG');
            L_BD_CT.OTO_CONG_XUAT := L_BIEN_DONG_OBJECT.GET_NUMBER('OTO_CONG_XUAT');
            L_BD_CT.OTO_XI_LANH := L_BIEN_DONG_OBJECT.GET_NUMBER('OTO_XI_LANH');
            L_BD_CT.OTO_SO_KHUNG := UNESCAPE_STRINGJSON(L_BIEN_DONG_OBJECT.GET_STRING('OTO_SO_KHUNG'));
            L_BD_CT.OTO_SO_MAY := UNESCAPE_STRINGJSON(L_BIEN_DONG_OBJECT.GET_STRING('OTO_SO_MAY'));
            L_BD_CT.CLN_SO_NAM := L_BIEN_DONG_OBJECT.GET_NUMBER('CLN_SO_NAM');
            L_BD_CT.PHI_THU := L_BIEN_DONG_OBJECT.GET_NUMBER('PHI_THU');
            L_BD_CT.PHI_BU_DAP := L_BIEN_DONG_OBJECT.GET_NUMBER('PHI_BU_DAP');
            L_BD_CT.PHI_NOP_NGAN_SACH := L_BIEN_DONG_OBJECT.GET_NUMBER('PHI_NOP_NGAN_SACH');
            L_BD_CT.PHI_KHAC := L_BIEN_DONG_OBJECT.GET_NUMBER('PHI_KHAC');
            L_BD_CT.IS_BAN_THANH_LY := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_STRING('DA_BAN_THANH_LY') = 'true' THEN
                    1
                ELSE 0
            END );
        --HTSD_JSON

            L_BD_CT.DIA_CHI := UNESCAPE_STRINGJSON(L_BIEN_DONG_OBJECT.GET_STRING('DIA_CHI'));
        --INSERT bien dong chi tiet
            INSERT INTO BD_BIEN_DONG_CHI_TIET (
                BIEN_DONG_ID,
                HINH_THUC_MUA_SAM_ID,
                MUC_DICH_SU_DUNG_ID,
                NHAN_HIEU,
                SO_HIEU,
                DIA_BAN_ID,
                DATA_JSON,
                NGUYEN_GIA,
                DAT_TONG_DIEN_TICH,
                HM_SO_NAM_CON_LAI,
                HM_TY_LE_HAO_MON,
                HM_LUY_KE,
                HM_GIA_TRI_CON_LAI,
                KH_NGAY_BAT_DAU,
                KH_THANG_CON_LAI,
                KH_GIA_TINH_KHAU_HAO,
                KH_GIA_TRI_TRICH_THANG,
                KH_LUY_KE,
                KH_CON_LAI,
                NHA_SO_TANG,
                NHA_NAM_XAY_DUNG,
                NHA_DIEN_TICH_XD,
                NHA_TONG_DIEN_TICH_XD,
                VKT_DIEN_TICH,
                VKT_THE_TICH,
                VKT_CHIEU_DAI,
                OTO_BIEN_KIEM_SOAT,
                OTO_SO_CHO_NGOI,
                OTO_CHUC_DANH_ID,
                OTO_NHAN_XE_ID,
                OTO_TAI_TRONG,
                OTO_CONG_XUAT,
                OTO_XI_LANH,
                OTO_SO_KHUNG,
                OTO_SO_MAY,
                THONG_SO_KY_THUAT,
                CLN_SO_NAM,
                PHI_THU,
                PHI_BU_DAP,
                PHI_NOP_NGAN_SACH,
                PHI_KHAC,
                DON_VI_NHAN_DIEU_CHUYEN_ID,
                HTSD_JSON,
                HINH_THUC_XU_LY_ID,
                IS_BAN_THANH_LY,
                DIA_CHI,
                DIEU_CHUYEN_KEM_THEO
            ) VALUES (
                L_BD_ID,
                L_BD_CT.HINH_THUC_MUA_SAM_ID,
                L_BD_CT.MUC_DICH_SU_DUNG_ID,
                L_BD_CT.NHAN_HIEU,
                L_BD_CT.SO_HIEU,
                L_BD_CT.DIA_BAN_ID,
                L_BD_CT.DATA_JSON,
                L_BD_CT.NGUYEN_GIA,
                L_BD_CT.DAT_TONG_DIEN_TICH,
                L_BD_CT.HM_SO_NAM_CON_LAI,
                L_BD_CT.HM_TY_LE_HAO_MON,
                L_BD_CT.HM_LUY_KE,
                L_BD_CT.HM_GIA_TRI_CON_LAI,
                L_BD_CT.KH_NGAY_BAT_DAU,
                L_BD_CT.KH_THANG_CON_LAI,
                L_BD_CT.KH_GIA_TINH_KHAU_HAO,
                L_BD_CT.KH_GIA_TRI_TRICH_THANG,
                L_BD_CT.KH_LUY_KE,
                L_BD_CT.KH_CON_LAI,
                L_BD_CT.NHA_SO_TANG,
                L_BD_CT.NHA_NAM_XAY_DUNG,
                L_BD_CT.NHA_DIEN_TICH_XD,
                L_BD_CT.NHA_TONG_DIEN_TICH_XD,
                L_BD_CT.VKT_DIEN_TICH,
                L_BD_CT.VKT_THE_TICH,
                L_BD_CT.VKT_CHIEU_DAI,
                L_BD_CT.OTO_BIEN_KIEM_SOAT,
                L_BD_CT.OTO_SO_CHO_NGOI,
                L_BD_CT.OTO_CHUC_DANH_ID,
                L_BD_CT.OTO_NHAN_XE_ID,
                L_BD_CT.OTO_TAI_TRONG,
                L_BD_CT.OTO_CONG_XUAT,
                L_BD_CT.OTO_XI_LANH,
                L_BD_CT.OTO_SO_KHUNG,
                L_BD_CT.OTO_SO_MAY,
                L_BD_CT.THONG_SO_KY_THUAT,
                L_BD_CT.CLN_SO_NAM,
                L_BD_CT.PHI_THU,
                L_BD_CT.PHI_BU_DAP,
                L_BD_CT.PHI_NOP_NGAN_SACH,
                L_BD_CT.PHI_KHAC,
                L_BD_CT.DON_VI_NHAN_DIEU_CHUYEN_ID,
                L_BD_CT.HTSD_JSON,
                L_BD_CT.HINH_THUC_XU_LY_ID,
                L_BD_CT.IS_BAN_THANH_LY,
                L_BD_CT.DIA_CHI,
                L_BD_CT.DIEU_CHUYEN_KEM_THEO
            );

            FOR I IN 0..L_HIEN_TRANG_ARR.GET_SIZE - 1 LOOP
                L_HIEN_TRANG_OBJECT := TREAT(L_HIEN_TRANG_ARR.GET(I) AS JSON_OBJECT_T);
                L_TS_HIEN_TRANG.HIEN_TRANG_ID := L_HIEN_TRANG_OBJECT.GET_NUMBER('HIEN_TRANG_ID');
                --DBMS_OUTPUT.PUT_LINE(l_hien_trang_object.get_string('TEN_HIEN_TRANG'));
                BEGIN
                    SELECT
                        TEN_HIEN_TRANG
                    INTO L_TEN_HIEN_TRANG
                    FROM
                        DM_HIEN_TRANG
                    WHERE
                        ID = L_TS_HIEN_TRANG.HIEN_TRANG_ID;
                    --DBMS_OUTPUT.PUT_LINE(l_ts_hien_trang.HIEN_TRANG_ID);

                    L_TS_HIEN_TRANG.NHOM_HIEN_TRANG_ID := L_HIEN_TRANG_OBJECT.GET_NUMBER('NHOM_HIEN_TRANG_ID');
                    L_TS_HIEN_TRANG.KIEU_DU_LIEU_ID := L_HIEN_TRANG_OBJECT.GET_NUMBER('KIEU_DU_LIEU_ID');
                    L_TS_HIEN_TRANG.TEN_HIEN_TRANG := NVL(L_TEN_HIEN_TRANG, UNESCAPE_STRINGJSON(L_HIEN_TRANG_OBJECT.GET_STRING('TEN_HIEN_TRANG')));

                    L_TS_HIEN_TRANG.GIA_TRI_TEXT := L_HIEN_TRANG_OBJECT.GET_NUMBER('GIA_TRI_TEXT');
                    L_TS_HIEN_TRANG.GIA_TRI_NUMBER := ABS(NVL(L_HIEN_TRANG_OBJECT.GET_NUMBER('GIA_TRI_NUMBER'), 0));

                    L_TS_HIEN_TRANG.GIA_TRI_CHECKBOX := ( CASE
                        WHEN L_HIEN_TRANG_OBJECT.GET_STRING('GIA_TRI_CHECKBOX') = 'true' THEN
                            1
                        WHEN L_HIEN_TRANG_OBJECT.GET_STRING('GIA_TRI_CHECKBOX') = 'false' THEN
                            0
                        ELSE NULL
                    END );

                    INSERT INTO TS_TAI_SAN_HIEN_TRANG_SU_DUNG (
                        TAI_SAN_ID,
                        BIEN_DONG_ID,
                        NHOM_HIEN_TRANG_ID,
                        KIEU_DU_LIEU_ID,
                        TEN_HIEN_TRANG,
                        GIA_TRI_TEXT,
                        GIA_TRI_NUMBER,
                        GIA_TRI_CHECKBOX,
                        HIEN_TRANG_ID
                    ) VALUES (
                        L_ID,
                        L_BD_ID,
                        L_TS_HIEN_TRANG.NHOM_HIEN_TRANG_ID,
                        L_TS_HIEN_TRANG.KIEU_DU_LIEU_ID,
                        L_TS_HIEN_TRANG.TEN_HIEN_TRANG,
                        L_TS_HIEN_TRANG.GIA_TRI_TEXT,
                        L_TS_HIEN_TRANG.GIA_TRI_NUMBER,
                        L_TS_HIEN_TRANG.GIA_TRI_CHECKBOX,
                        L_TS_HIEN_TRANG.HIEN_TRANG_ID
                    );

                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                    --DBMS_OUTPUT.PUT_LINE('NOT FOUND ID '||l_hien_trang_object.get_number('HIEN_TRANG_ID'));
                        L_TEN_HIEN_TRANG := NULL;
                END;

            END LOOP;
            -- END IF;

            FOR NV IN 0..L_NGUON_VON_ARR.GET_SIZE - 1 LOOP
                L_NGUON_VON_OBJECT := TREAT(L_NGUON_VON_ARR.GET(NV) AS JSON_OBJECT_T);
                L_TS_NGUON_VON.NGUON_VON_ID := L_NGUON_VON_OBJECT.GET_NUMBER('NGUON_VON_ID');
                L_TS_NGUON_VON.GIA_TRI := ( CASE
                    WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                        3,
                        6
                    ) THEN
                        -ABS(NVL(L_NGUON_VON_OBJECT.GET_NUMBER('GIA_TRI'), 0))
                    WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                        4,
                        5,
                        7
                    ) THEN
                        0
                    ELSE ABS(NVL(L_NGUON_VON_OBJECT.GET_NUMBER('GIA_TRI'), 0))
                END );

                INSERT INTO TS_TAI_SAN_NGUON_VON (
                    TAI_SAN_ID,
                    NGUON_VON_ID,
                    GIA_TRI,
                    BIEN_DONG_ID
                ) VALUES (
                    L_ID,
                    L_TS_NGUON_VON.NGUON_VON_ID,
                    L_TS_NGUON_VON.GIA_TRI,
                    L_BD_ID
                );

            END LOOP;

        ELSE
            L_LYDO_MA := L_BIEN_DONG_OBJECT.GET_STRING('LY_DO_BIEN_DONG_MA');
            L_LYDO_TEN := UNESCAPE_STRINGJSON(L_BIEN_DONG_OBJECT.GET_STRING('LY_DO_BIEN_DONG_TEN'));
            IF L_LYDO_MA IS NULL AND L_LYDO_TEN IS NULL THEN
                OPEN MSS_OUT FOR SELECT
                                     JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-LY_DO_BIEN_DONG_MA IS NULL' ) AS STRJSON
                                 FROM
                                     DUAL;

                --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
                RETURN;
            END IF;

            BEGIN
                SELECT
                    ID
                INTO L_LYDO_ID
                FROM
                    DM_LY_DO_BIEN_DONG
                WHERE
                    MA = L_LYDO_MA
                    -- AND loai_hinh_tai_san_id IN(l_loai_hinh_tai_san,0) 
                    AND ROWNUM = 1;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    BEGIN
                        SELECT
                            ID
                        INTO L_LYDO_ID
                        FROM
                            DM_LY_DO_BIEN_DONG
                        WHERE
                            TEN = L_LYDO_TEN
                        -- AND loai_hinh_tai_san_id in(l_loai_hinh_tai_san,0) 
                            AND ROWNUM = 1;

                    EXCEPTION
                        WHEN NO_DATA_FOUND THEN
                            OPEN MSS_OUT FOR SELECT
                                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-LY_DO_BIEN_DONG_MA('
                                                                                                  || L_LYDO_MA
                                                                                                  || ') NOT FOUND' ) AS STRJSON
                                             FROM
                                                 DUAL;

                            --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
                            RETURN;
                    END;
            END;

            L_YC.LY_DO_BIEN_DONG_ID := L_LYDO_ID;
            L_DONVI_BOPHAN_MA := L_BIEN_DONG_OBJECT.GET_STRING('DON_VI_BO_PHAN_MA');
            IF L_DONVI_BOPHAN_MA IS NOT NULL THEN
                BEGIN
                    SELECT
                        ID
                    INTO L_DONVI_BOPHAN_ID
                    FROM
                        DM_DON_VI_BO_PHAN
                    WHERE
                        MA = L_DONVI_BOPHAN_MA
                        AND ROWNUM = 1;

                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        OPEN MSS_OUT FOR SELECT
                                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-DON_VI_BO_PHAN_MA('
                                                                                              || L_DONVI_BOPHAN_MA
                                                                                              || ') NOT FOUND' ) AS STRJSON
                                         FROM
                                             DUAL;

                        --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
                        RETURN;
                END;

                L_YC.DON_VI_BO_PHAN_ID := L_DONVI_BOPHAN_ID;
            END IF;

            L_YC.TAI_SAN_ID := L_ID;
            L_YC.TAI_SAN_MA := L_TAI_SAN.MA;
            L_YC.TAI_SAN_TEN := L_TAI_SAN.TEN;
            L_YC.LOAI_HINH_TAI_SAN_ID := L_LOAI_HINH_TAI_SAN;
            L_YC.LOAI_TAI_SAN_DON_VI_ID := L_TAI_SAN.LOAI_TAI_SAN_DON_VI_ID;
            L_YC.DON_VI_ID := L_TAI_SAN.DON_VI_ID;
            L_YC.LOAI_TAI_SAN_ID := L_LOAITAISAN_ID;
            L_YC.NGUYEN_GIA := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                    3,
                    6
                ) THEN
                    -ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('NGUYEN_GIA'))
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                    4,
                    5,
                    7
                ) THEN
                    0
                ELSE ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('NGUYEN_GIA'))
            END );

            L_YC.CHUNG_TU_SO := UNESCAPE_STRINGJSON(L_BIEN_DONG_OBJECT.GET_STRING('CHUNG_TU_SO'));
            L_YC.CHUNG_TU_NGAY := L_BIEN_DONG_OBJECT.GET_DATE('CHUNG_TU_NGAY');
            L_YC.NGAY_BIEN_DONG := L_BIEN_DONG_OBJECT.GET_DATE('NGAY_BIEN_DONG');
            L_YC.NGAY_SU_DUNG := L_BIEN_DONG_OBJECT.GET_DATE('NGAY_SU_DUNG');
            L_YC.LOAI_BIEN_DONG_ID := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') = 4 THEN
                    11
                ELSE L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID')
            END );

            L_YC.TRANG_THAI_ID := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('TRANG_THAI') = 3 THEN
                    4--từ chối
                ELSE 2
            END );--chờ duyệt

            L_YC.NGAY_TAO := NVL(L_BIEN_DONG_OBJECT.GET_DATE('NGAY_TAO'), CURRENT_DATE);
            L_YC.GHI_CHU := UNESCAPE_STRINGJSON(L_BIEN_DONG_OBJECT.GET_STRING('GHI_CHU'));
            L_YC.QUYET_DINH_SO := UNESCAPE_STRINGJSON(L_BIEN_DONG_OBJECT.GET_STRING('QUYET_DINH_SO'));
            L_YC.QUYET_DINH_NGAY := ( CASE
                WHEN L_YC.QUYET_DINH_SO IS NULL THEN
                    NULL
                ELSE L_BIEN_DONG_OBJECT.GET_DATE('QUYET_DINH_NGAY')
            END );

            L_DIABAN_MA := L_BIEN_DONG_OBJECT.GET_STRING('DIA_BAN_MA');
            BEGIN
                SELECT
                    *
                INTO L_DIA_BAN
                FROM
                    DM_DIA_BAN
                WHERE
                    MA = L_DIABAN_MA
                    AND ROWNUM = 1;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    L_DIA_BAN := NULL;
            END;

            L_YC_CT.DIA_BAN_ID := L_DIA_BAN.ID;
            IF NVL(L_DIA_BAN.LOAI_DIA_BAN_ID, 1) = 1 THEN
                L_YC.TINH_ID := L_DIA_BAN.ID;
            ELSIF L_DIA_BAN.LOAI_DIA_BAN_ID = 2 THEN
                L_YC.HUYEN_ID := L_DIA_BAN.ID;
                L_YC.TINH_ID := L_DIA_BAN.PARENT_ID;
            ELSIF L_DIA_BAN.LOAI_DIA_BAN_ID = 3 THEN
                L_YC.XA_ID := L_DIA_BAN.ID;
                L_YC.HUYEN_ID := L_DIA_BAN.PARENT_ID;
                BEGIN
                    SELECT
                        *
                    INTO L_DIA_BAN
                    FROM
                        DM_DIA_BAN
                    WHERE
                        ID = L_TS_DAT.HUYEN_ID;

                    L_YC.TINH_ID := L_DIA_BAN.PARENT_ID;
                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        L_DIA_BAN := NULL;
                END;

            END IF;
            
            
        --nguồn vốn Json

            L_YC.NGUON_VON_JSON := L_BD_NV_JSON;
        --INSERT nguon von
            INSERT INTO NV_YEU_CAU (
                TAI_SAN_ID,
                TAI_SAN_MA,
                TAI_SAN_TEN,
                LOAI_TAI_SAN_ID,
                LOAI_HINH_TAI_SAN_ID,
                NGUYEN_GIA,
                DON_VI_BO_PHAN_ID,
                CHUNG_TU_SO,
                CHUNG_TU_NGAY,
                NGAY_BIEN_DONG,
                NGAY_SU_DUNG,
                LOAI_BIEN_DONG_ID,
                LY_DO_BIEN_DONG_ID,
                TRANG_THAI_ID,
                DON_VI_ID,
                NGUOI_TAO_ID,
                NGAY_TAO,
                GUID,
                GHI_CHU,
                QUYET_DINH_SO,
                QUYET_DINH_NGAY,
                NGUON_VON_JSON,
                IS_BIENDONG_CUOI,
                LY_DO_KHONG_DUYET,
                LOAI_TAI_SAN_DON_VI_ID,
                TINH_ID,
                HUYEN_ID,
                XA_ID
            ) VALUES (
                L_ID,
                L_YC.TAI_SAN_MA,
                L_YC.TAI_SAN_TEN,
                L_YC.LOAI_TAI_SAN_ID,
                L_LOAI_HINH_TAI_SAN,
                L_YC.NGUYEN_GIA,
                L_YC.DON_VI_BO_PHAN_ID,
                L_YC.CHUNG_TU_SO,
                L_YC.CHUNG_TU_NGAY,
                L_YC.NGAY_BIEN_DONG,
                L_YC.NGAY_SU_DUNG,
                L_YC.LOAI_BIEN_DONG_ID,
                L_YC.LY_DO_BIEN_DONG_ID,
                L_YC.TRANG_THAI_ID,
                L_YC.DON_VI_ID,
                L_YC.NGUOI_TAO_ID,
                L_YC.NGAY_TAO,
                SYS_GUID(),
                L_YC.GHI_CHU,
                L_YC.QUYET_DINH_SO,
                L_YC.QUYET_DINH_NGAY,
                L_YC.NGUON_VON_JSON,
                L_YC.IS_BIENDONG_CUOI,
                L_YC.LY_DO_KHONG_DUYET,
                L_YC.LOAI_TAI_SAN_DON_VI_ID,
                L_YC.TINH_ID,
                L_YC.HUYEN_ID,
                L_YC.XA_ID
            ) RETURNING ID INTO L_YC_ID;

        --yeu cau chi tiet

            L_HINH_THUC_MUA_SAM_MA := L_BIEN_DONG_OBJECT.GET_STRING('HINH_THUC_MUA_SAM_MA');
            IF L_HINH_THUC_MUA_SAM_MA IS NOT NULL THEN
                BEGIN
                    SELECT
                        ID
                    INTO L_HINH_THUC_MUA_SAM_ID
                    FROM
                        DM_HINH_THUC_MUA_SAM
                    WHERE
                        MA = L_HINH_THUC_MUA_SAM_MA
                        AND ROWNUM = 1;

                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        OPEN MSS_OUT FOR SELECT
                                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-HINH_THUC_MUA_SAM_MA('
                                                                                              || L_HINH_THUC_MUA_SAM_MA
                                                                                              || ') NOT FOUND' ) AS STRJSON
                                         FROM
                                             DUAL;

                        --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
                        RETURN;
                END;

                L_YC_CT.HINH_THUC_MUA_SAM_ID := L_HINH_THUC_MUA_SAM_ID;
            END IF;

            L_MUC_DICH_SU_DUNG_MA := L_BIEN_DONG_OBJECT.GET_STRING('MUC_DICH_SU_DUNG_MA');
            IF L_MUC_DICH_SU_DUNG_MA IS NOT NULL THEN
                BEGIN
                    SELECT
                        ID
                    INTO L_MUC_DICH_SU_DUNG_ID
                    FROM
                        DM_MUC_DICH_SU_DUNG
                    WHERE
                        MA = L_MUC_DICH_SU_DUNG_MA
                        AND ROWNUM = 1;

                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        OPEN MSS_OUT FOR SELECT
                                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-MUC_DICH_SU_DUNG_MA('
                                                                                              || L_MUC_DICH_SU_DUNG_MA
                                                                                              || ') NOT FOUND' ) AS STRJSON
                                         FROM
                                             DUAL;

                        --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
                        RETURN;
                END;

                L_YC_CT.MUC_DICH_SU_DUNG_ID := L_MUC_DICH_SU_DUNG_ID;
            END IF;

            L_CHUCDANH_MA := L_BIEN_DONG_OBJECT.GET_STRING('OTO_CHUC_DANH_MA');
            IF L_CHUCDANH_MA IS NOT NULL THEN
                BEGIN
                    SELECT
                        ID
                    INTO L_CHUCDANH_ID
                    FROM
                        DM_CHUC_DANH
                    WHERE
                        MA_CHUC_DANH = L_CHUCDANH_MA
                        AND ROWNUM = 1;

                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        OPEN MSS_OUT FOR SELECT
                                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-OTO_CHUC_DANH_MA('
                                                                                              || L_CHUCDANH_MA
                                                                                              || ') NOT FOUND' ) AS STRJSON
                                         FROM
                                             DUAL;

                        --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
                        RETURN;
                END;

                L_YC_CT.OTO_CHUC_DANH_ID := L_CHUCDANH_ID;
            END IF;

            L_NHANXE_MA := L_BIEN_DONG_OBJECT.GET_STRING('OTO_NHAN_XE_MA');
            IF L_NHANXE_MA IS NOT NULL THEN
                BEGIN
                    SELECT
                        ID
                    INTO L_NHANXE_ID
                    FROM
                        DM_NHAN_XE
                    WHERE
                        MA = L_NHANXE_MA
                        AND ROWNUM = 1;

                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        OPEN MSS_OUT FOR SELECT
                                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-OTO_NHAN_XE_MA('
                                                                                              || L_NHANXE_MA
                                                                                              || ') NOT FOUND' ) AS STRJSON
                                         FROM
                                             DUAL;

                        --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
                        RETURN;
                END;

                L_YC_CT.OTO_NHAN_XE_ID := L_NHANXE_ID;
            END IF;

            L_DONVI_DIEU_CHUYEN_MA := L_BIEN_DONG_OBJECT.GET_STRING('DON_VI_NHAN_DIEU_CHUYEN_MA');
            IF L_DONVI_DIEU_CHUYEN_MA IS NOT NULL THEN
                BEGIN
                    SELECT
                        ID
                    INTO L_DONVI_DIEU_CHUYEN_ID
                    FROM
                        DM_DON_VI
                    WHERE
                        MA = L_DONVI_DIEU_CHUYEN_MA
                        AND ROWNUM = 1;

                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        OPEN MSS_OUT FOR SELECT
                                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-DON_VI_NHAN_DIEU_CHUYEN_MA('
                                                                                              || L_DONVI_DIEU_CHUYEN_MA
                                                                                              || ') NOT FOUND' ) AS STRJSON
                                         FROM
                                             DUAL;

                        --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
                        RETURN;
                END;

                L_YC_CT.DON_VI_NHAN_DIEU_CHUYEN_ID := L_DONVI_DIEU_CHUYEN_ID;
            END IF;

            L_HINH_THUC_XU_LY_MA := L_BIEN_DONG_OBJECT.GET_STRING('HINH_THUC_XU_LY_MA');
            IF L_HINH_THUC_XU_LY_MA IS NOT NULL THEN
                BEGIN
                    SELECT
                        ID
                    INTO L_HINH_THUC_XU_LY_ID
                    FROM
                        DM_DON_VI
                    WHERE
                        MA = L_HINH_THUC_XU_LY_MA
                        AND ROWNUM = 1;

                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        OPEN MSS_OUT FOR SELECT
                                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-HINH_THUC_XU_LY_MA('
                                                                                              || L_HINH_THUC_XU_LY_MA
                                                                                              || ') NOT FOUND' ) AS STRJSON
                                         FROM
                                             DUAL;

                        --L_EXC_FN := DEL_TAI_SAN_DB_QLDKTS40(L_MA_DB);
                        RETURN;
                END;

                L_YC_CT.HINH_THUC_XU_LY_ID := L_HINH_THUC_XU_LY_ID;
            END IF;

            L_YC_CT.YEU_CAU_ID := L_YC_ID;
            L_YC_CT.THONG_SO_KY_THUAT := UNESCAPE_STRINGJSON(L_BIEN_DONG_OBJECT.GET_STRING('THONG_SO_KY_THUAT'));
            L_YC_CT.NHAN_HIEU := L_BIEN_DONG_OBJECT.GET_STRING('NHAN_HIEU');
            L_YC_CT.SO_HIEU := UNESCAPE_STRINGJSON(L_BIEN_DONG_OBJECT.GET_STRING('SO_HIEU'));
            --L_YC_CT.DATA_JSON :=  REPLACE(REPLACE(L_BIEN_DONG_OBJECT.GET_STRING('DATA_JSON'),'\"','"'), '\\', '\');
            L_YC_CT.DATA_JSON := L_BIEN_DONG_OBJECT.GET_STRING('DATA_JSON');

            L_YC_CT.HTSD_JSON := L_BD_HTSD_JSON;
            L_YC_CT.NGUYEN_GIA := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                    3,
                    6
                ) THEN
                    -ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('NGUYEN_GIA'))
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                    4,
                    5,
                    7
                ) THEN
                    0
                ELSE ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('NGUYEN_GIA'))
            END );

            L_YC_CT.DAT_TONG_DIEN_TICH := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                    3,
                    6
                ) THEN
                    -ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('DAT_TONG_DIEN_TICH'))
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                    4,
                    5,
                    7
                ) THEN
                    0
                ELSE ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('DAT_TONG_DIEN_TICH'))
            END );

            L_YC_CT.HM_SO_NAM_CON_LAI := L_BIEN_DONG_OBJECT.GET_NUMBER('HM_SO_NAM_CON_LAI');
            L_YC_CT.HM_TY_LE_HAO_MON := L_BIEN_DONG_OBJECT.GET_NUMBER('HM_TY_LE_HAO_MON');
            L_YC_CT.HM_LUY_KE := L_BIEN_DONG_OBJECT.GET_NUMBER('HM_LUY_KE');
            L_YC_CT.HM_GIA_TRI_CON_LAI := L_BIEN_DONG_OBJECT.GET_NUMBER('GIA_TRI_CON_LAI');
            L_YC_CT.KH_NGAY_BAT_DAU := L_BIEN_DONG_OBJECT.GET_DATE('KH_NGAY_BAT_DAU');
            L_YC_CT.KH_THANG_CON_LAI := L_BIEN_DONG_OBJECT.GET_NUMBER('KH_THANG_CON_LAI');
            L_YC_CT.KH_GIA_TINH_KHAU_HAO := L_BIEN_DONG_OBJECT.GET_NUMBER('KH_GIA_TINH_KHAU_HAO');
            L_YC_CT.KH_GIA_TRI_TRICH_THANG := L_BIEN_DONG_OBJECT.GET_NUMBER('KH_GIA_TRI_TRICH_THANG');
            L_YC_CT.KH_LUY_KE := L_BIEN_DONG_OBJECT.GET_NUMBER('KH_LUY_KE');
            L_YC_CT.KH_CON_LAI := L_BIEN_DONG_OBJECT.GET_NUMBER('KH_CON_LAI');
            L_YC_CT.NHA_SO_TANG := L_BIEN_DONG_OBJECT.GET_NUMBER('NHA_SO_TANG');
            L_YC_CT.NHA_NAM_XAY_DUNG := L_BIEN_DONG_OBJECT.GET_NUMBER('NHA_NAM_XAY_DUNG');
            L_YC_CT.NHA_DIEN_TICH_XD := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                    3,
                    6
                ) THEN
                    -ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('NHA_DIEN_TICH_XD'))
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                    4,
                    5,
                    7
                ) THEN
                    0
                ELSE ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('NHA_DIEN_TICH_XD'))
            END );

            L_YC_CT.NHA_TONG_DIEN_TICH_XD := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                    3,
                    6
                ) THEN
                    -ABS(NVL(L_BIEN_DONG_OBJECT.GET_NUMBER('NHA_TONG_DIEN_TICH_XD'), 0))
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') IN (
                    4,
                    5,
                    7
                ) THEN
                    0
                ELSE ABS(NVL(L_BIEN_DONG_OBJECT.GET_NUMBER('NHA_TONG_DIEN_TICH_XD'), 0))
            END );

            L_YC_CT.VKT_DIEN_TICH := NVL(L_BIEN_DONG_OBJECT.GET_NUMBER('VKT_DIEN_TICH'), 0);
            L_YC_CT.VKT_THE_TICH := L_BIEN_DONG_OBJECT.GET_NUMBER('VKT_THE_TICH');
            L_YC_CT.VKT_CHIEU_DAI := L_BIEN_DONG_OBJECT.GET_NUMBER('VKT_CHIEU_DAI');
            L_YC_CT.OTO_BIEN_KIEM_SOAT := UNESCAPE_STRINGJSON(L_BIEN_DONG_OBJECT.GET_STRING('OTO_BIEN_KIEM_SOAT'));
            L_YC_CT.OTO_SO_CHO_NGOI := L_BIEN_DONG_OBJECT.GET_NUMBER('OTO_SO_CHO_NGOI');
            L_YC_CT.OTO_TAI_TRONG := L_BIEN_DONG_OBJECT.GET_NUMBER('OTO_TAI_TRONG');
            L_YC_CT.OTO_CONG_XUAT := L_BIEN_DONG_OBJECT.GET_NUMBER('OTO_CONG_XUAT');
            L_YC_CT.OTO_XI_LANH := L_BIEN_DONG_OBJECT.GET_NUMBER('OTO_XI_LANH');
            L_YC_CT.OTO_SO_KHUNG := UNESCAPE_STRINGJSON(L_BIEN_DONG_OBJECT.GET_STRING('OTO_SO_KHUNG'));
            L_YC_CT.OTO_SO_MAY := UNESCAPE_STRINGJSON(L_BIEN_DONG_OBJECT.GET_STRING('OTO_SO_MAY'));
            L_YC_CT.THONG_SO_KY_THUAT := UNESCAPE_STRINGJSON(L_BIEN_DONG_OBJECT.GET_STRING('THONG_SO_KY_THUAT'));
            L_YC_CT.CLN_SO_NAM := L_BIEN_DONG_OBJECT.GET_NUMBER('CLN_SO_NAM');
            L_YC_CT.PHI_THU := L_BIEN_DONG_OBJECT.GET_NUMBER('PHI_THU');
            L_YC_CT.PHI_BU_DAP := L_BIEN_DONG_OBJECT.GET_NUMBER('PHI_BU_DAP');
            L_YC_CT.PHI_NOP_NGAN_SACH := L_BIEN_DONG_OBJECT.GET_NUMBER('PHI_NOP_NGAN_SACH');
            L_YC_CT.PHI_KHAC := L_BIEN_DONG_OBJECT.GET_NUMBER('PHI_KHAC');
            L_YC_CT.IS_BAN_THANH_LY := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_STRING('DA_BAN_THANH_LY') = 'true' THEN
                    1
                WHEN L_BIEN_DONG_OBJECT.GET_STRING('DA_BAN_THANH_LY') = 'false' THEN
                    0
                ELSE NULL
            END );

            L_YC_CT.DIA_CHI := UNESCAPE_STRINGJSON(L_BIEN_DONG_OBJECT.GET_STRING('DIA_CHI'));
            INSERT INTO NV_YEU_CAU_CHI_TIET (
                YEU_CAU_ID,
                HINH_THUC_MUA_SAM_ID,
                MUC_DICH_SU_DUNG_ID,
                NHAN_HIEU,
                SO_HIEU,
                DIA_BAN_ID,
                DATA_JSON,
                NGUYEN_GIA,
                DAT_TONG_DIEN_TICH,
                HM_SO_NAM_CON_LAI,
                HM_TY_LE_HAO_MON,
                HM_LUY_KE,
                HM_GIA_TRI_CON_LAI,
                KH_NGAY_BAT_DAU,
                KH_THANG_CON_LAI,
                KH_GIA_TINH_KHAU_HAO,
                KH_GIA_TRI_TRICH_THANG,
                KH_LUY_KE,
                KH_CON_LAI,
                NHA_SO_TANG,
                NHA_NAM_XAY_DUNG,
                NHA_DIEN_TICH_XD,
                NHA_TONG_DIEN_TICH_XD,
                VKT_DIEN_TICH,
                VKT_THE_TICH,
                VKT_CHIEU_DAI,
                OTO_BIEN_KIEM_SOAT,
                OTO_SO_CHO_NGOI,
                OTO_CHUC_DANH_ID,
                OTO_NHAN_XE_ID,
                OTO_TAI_TRONG,
                OTO_CONG_XUAT,
                OTO_XI_LANH,
                OTO_SO_KHUNG,
                OTO_SO_MAY,
                THONG_SO_KY_THUAT,
                CLN_SO_NAM,
                HTSD_JSON,
                PHI_THU,
                PHI_BU_DAP,
                PHI_NOP_NGAN_SACH,
                PHI_KHAC,
                DON_VI_NHAN_DIEU_CHUYEN_ID,
                HINH_THUC_XU_LY_ID,
                IS_BAN_THANH_LY,
                DIA_CHI,
                DIEU_CHUYEN_KEM_THEO
            ) VALUES (
                L_YC_ID,
                L_YC_CT.HINH_THUC_MUA_SAM_ID,
                L_YC_CT.MUC_DICH_SU_DUNG_ID,
                L_YC_CT.NHAN_HIEU,
                L_YC_CT.SO_HIEU,
                L_YC_CT.DIA_BAN_ID,
                L_YC_CT.DATA_JSON,
                L_YC_CT.NGUYEN_GIA,
                L_YC_CT.DAT_TONG_DIEN_TICH,
                L_YC_CT.HM_SO_NAM_CON_LAI,
                L_YC_CT.HM_TY_LE_HAO_MON,
                L_YC_CT.HM_LUY_KE,
                L_YC_CT.HM_GIA_TRI_CON_LAI,
                L_YC_CT.KH_NGAY_BAT_DAU,
                L_YC_CT.KH_THANG_CON_LAI,
                L_YC_CT.KH_GIA_TINH_KHAU_HAO,
                L_YC_CT.KH_GIA_TRI_TRICH_THANG,
                L_YC_CT.KH_LUY_KE,
                L_YC_CT.KH_CON_LAI,
                L_YC_CT.NHA_SO_TANG,
                L_YC_CT.NHA_NAM_XAY_DUNG,
                L_YC_CT.NHA_DIEN_TICH_XD,
                L_YC_CT.NHA_TONG_DIEN_TICH_XD,
                L_YC_CT.VKT_DIEN_TICH,
                L_YC_CT.VKT_THE_TICH,
                L_YC_CT.VKT_CHIEU_DAI,
                L_YC_CT.OTO_BIEN_KIEM_SOAT,
                L_YC_CT.OTO_SO_CHO_NGOI,
                L_YC_CT.OTO_CHUC_DANH_ID,
                L_YC_CT.OTO_NHAN_XE_ID,
                L_YC_CT.OTO_TAI_TRONG,
                L_YC_CT.OTO_CONG_XUAT,
                L_YC_CT.OTO_XI_LANH,
                L_YC_CT.OTO_SO_KHUNG,
                L_YC_CT.OTO_SO_MAY,
                L_YC_CT.THONG_SO_KY_THUAT,
                L_YC_CT.CLN_SO_NAM,
                L_YC_CT.HTSD_JSON,
                L_YC_CT.PHI_THU,
                L_YC_CT.PHI_BU_DAP,
                L_YC_CT.PHI_NOP_NGAN_SACH,
                L_YC_CT.PHI_KHAC,
                L_YC_CT.DON_VI_NHAN_DIEU_CHUYEN_ID,
                L_YC_CT.HINH_THUC_XU_LY_ID,
                L_YC_CT.IS_BAN_THANH_LY,
                L_YC_CT.DIA_CHI,
                L_YC_CT.DIEU_CHUYEN_KEM_THEO
            );

        END IF;

    END LOOP;

    IF L_TAI_SAN.TRANG_THAI_ID = 3 THEN
        UPDATE TS_TAI_SAN
        SET
            TRANG_THAI_ID = (
                CASE
                    WHEN 1 <= (
                        SELECT
                            COUNT(*)
                        FROM
                            BD_BIEN_DONG
                        WHERE
                            BD_BIEN_DONG.TAI_SAN_ID = L_ID
                            AND BD_BIEN_DONG.LOAI_BIEN_DONG_ID = 5
                    ) THEN
                        8
                    WHEN 1 <= (
                        SELECT
                            COUNT(*)
                        FROM
                            BD_BIEN_DONG
                        WHERE
                            BD_BIEN_DONG.TAI_SAN_ID = L_ID
                    ) THEN
                        4
                    ELSE
                        3
                END
            )
        WHERE
            TRANG_THAI_ID = 3
            AND ID = L_ID;

    END IF;
    --insert kt hao mon

    L_HAO_MON_ARR := L_JSON_OBJECT.GET_ARRAY('LST_HAO_MON');
    IF L_HAO_MON_ARR IS NOT NULL AND L_HAO_MON_ARR.GET_SIZE > 0 THEN
        FOR I IN 0..L_HAO_MON_ARR.GET_SIZE - 1 LOOP
            L_HAO_MON_OBJECT := TREAT(L_HAO_MON_ARR.GET(I) AS JSON_OBJECT_T);
            L_HAOMON.TAI_SAN_ID := L_ID;
            L_HAOMON.MA_TAI_SAN := L_TAI_SAN.MA;
            L_HAOMON.NAM_HAO_MON := L_HAO_MON_OBJECT.GET_NUMBER('NAM_HAO_MON');
            L_HAOMON.GIA_TRI_HAO_MON := L_HAO_MON_OBJECT.GET_NUMBER('GIA_TRI_HAO_MON');
            L_HAOMON.TONG_HAO_MON_LUY_KE := L_HAO_MON_OBJECT.GET_NUMBER('TONG_HAO_MON_LUY_KE');
            L_HAOMON.TONG_GIA_TRI_CON_LAI := L_HAO_MON_OBJECT.GET_NUMBER('TONG_GIA_TRI_CON_LAI');
            L_HAOMON.TY_LE_HAO_MON := L_HAO_MON_OBJECT.GET_NUMBER('TY_LE_HAO_MON');
            L_HAOMON.TONG_NGUYEN_GIA := L_HAO_MON_OBJECT.GET_NUMBER('TONG_NGUYEN_GIA');
            BEGIN
                SELECT
                    ID
                INTO L_HAO_MON_ID
                FROM
                    KT_HAO_MON_TAI_SAN
                WHERE
                    TAI_SAN_ID = L_ID
                    AND NAM_HAO_MON = L_HAOMON.NAM_HAO_MON;

                UPDATE KT_HAO_MON_TAI_SAN
                SET
                    MA_TAI_SAN = L_HAOMON.MA_TAI_SAN,
                    GIA_TRI_HAO_MON = L_HAOMON.GIA_TRI_HAO_MON,
                    TONG_HAO_MON_LUY_KE = L_HAOMON.TONG_HAO_MON_LUY_KE,
                    TONG_GIA_TRI_CON_LAI = L_HAOMON.TONG_GIA_TRI_CON_LAI,
                    TY_LE_HAO_MON = L_HAOMON.TY_LE_HAO_MON,
                    TONG_NGUYEN_GIA = L_HAOMON.TONG_NGUYEN_GIA
                WHERE
                    ID = L_HAO_MON_ID;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    INSERT INTO KT_HAO_MON_TAI_SAN (
                        TAI_SAN_ID,
                        MA_TAI_SAN,
                        NAM_HAO_MON,
                        GIA_TRI_HAO_MON,
                        TONG_HAO_MON_LUY_KE,
                        TONG_GIA_TRI_CON_LAI,
                        TY_LE_HAO_MON,
                        TONG_NGUYEN_GIA
                    ) VALUES (
                        L_HAOMON.TAI_SAN_ID,
                        L_HAOMON.MA_TAI_SAN,
                        L_HAOMON.NAM_HAO_MON,
                        L_HAOMON.GIA_TRI_HAO_MON,
                        L_HAOMON.TONG_HAO_MON_LUY_KE,
                        L_HAOMON.TONG_GIA_TRI_CON_LAI,
                        L_HAOMON.TY_LE_HAO_MON,
                        L_HAOMON.TONG_NGUYEN_GIA
                    );

            END;

        END LOOP;
    ELSE
        SELECT
            TO_NUMBER(MIN(EXTRACT(YEAR FROM NGAY_BIEN_DONG)))
        INTO L_NAM_MIN
        FROM
            BD_BIEN_DONG
        WHERE
            TAI_SAN_ID = L_ID;

        L_NAM_MAX := TO_NUMBER(EXTRACT(YEAR FROM CURRENT_DATE));
--DBMS_OUTPUT.PUT_LINE(l_id);
--DBMS_OUTPUT.PUT_LINE(l_nam_min||'=>'||l_nam_max);
        IF L_NAM_MIN IS NOT NULL AND L_NAM_MIN > 0 THEN
            FOR INAM IN L_NAM_MIN..L_NAM_MAX LOOP
                BEGIN
                    SELECT
                        SUM(BD.NGUYEN_GIA)
                    INTO L_TONG_NGUYEN_GIA
                    FROM
                        BD_BIEN_DONG BD
                    WHERE
                        BD.TAI_SAN_ID = L_ID
                        AND EXTRACT(YEAR FROM BD.NGAY_BIEN_DONG) <= INAM;

                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        L_TONG_NGUYEN_GIA := 0;
                END;

                IF L_LOAI_HINH_TAI_SAN = 1 THEN
                    INSERT INTO KT_HAO_MON_TAI_SAN (
                        TAI_SAN_ID,
                        MA_TAI_SAN,
                        NAM_HAO_MON,
                        TONG_NGUYEN_GIA,
                        TONG_GIA_TRI_CON_LAI
                    ) VALUES (
                        L_ID,
                        L_TAI_SAN.MA,
                        INAM,
                        L_TONG_NGUYEN_GIA,
                        L_TONG_NGUYEN_GIA
                    );

                ELSE
                    INSERT INTO KT_HAO_MON_TAI_SAN (
                        TAI_SAN_ID,
                        MA_TAI_SAN,
                        NAM_HAO_MON,
                        TONG_NGUYEN_GIA
                    ) VALUES (
                        L_ID,
                        L_TAI_SAN.MA,
                        INAM,
                        L_TONG_NGUYEN_GIA
                    );

                END IF;

            END LOOP;
        END IF;

    END IF;

    UPDATE DB_TAI_SAN_NHAT_KY
    SET
        BD_IDS = (
            SELECT
                LISTAGG(ID, ',') WITHIN GROUP(
                    ORDER BY
                        1
                ) AS BD_IDS
            FROM
                BD_BIEN_DONG
            WHERE
                TAI_SAN_ID = L_ID
        )
    WHERE
        TAI_SAN_ID = L_ID;

    OPEN MSS_OUT FOR SELECT
                        JSON_OBJECT ( 'Code' VALUE '00', 'Message' VALUE 'Done', 'IdRecord' VALUE NULL, 'ObjectInfo' VALUE L_TAI_SAN.MA ) AS STRJSON
                    FROM
                        DUAL;

    RETURN;
END INSERT_TAI_SAN;