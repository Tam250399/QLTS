create or replace PROCEDURE INSERT_KIEM_KE_CCDC(
        P_JSON_STRING IN CLOB,
        MSS_OUT OUT SYS_REFCURSOR
    ) AS 
    L_JSON_OBJECT JSON_OBJECT_T;
    L_CCDC_KIEMKE_ARRAY JSON_ARRAY_T;
    L_HOI_DONG_ARRAY JSON_ARRAY_T;
    L_CCDC_THUA_ARRAY JSON_ARRAY_T;
    L_CCDC_OBJECT JSON_OBJECT_T;
    L_HOI_DONG_OBJECT JSON_OBJECT_T;
    L_CCDC_THUA_OBJECT JSON_OBJECT_T;

    L_KIEM_KE CCDC_KIEM_KE%ROWTYPE;
    L_HOI_DONG CCDC_KIEM_KE_HOI_DONG%ROWTYPE;


    L_KIEMKE_ID NUMBER;
    L_DON_VI_MA VARCHAR2(50);
    L_DON_VI_ID NUMBER;
    L_DON_VI_BO_PHAN_ID NUMBER;
    L_DON_VI_PHAN_BO_MA VARCHAR2(50);
    L_DON_VI_PHAN_BO_TEN VARCHAR2(2000);
    L_DON_VI_SU_DUNG_ID NUMBER;
    L_DON_VI_SU_DUNG_MA VARCHAR2(50);

    L_CCDC_ID NUMBER;
    L_CCDC_MA VARCHAR2(50);
    L_CCDC_DON_GIA NUMBER;
    L_CCDC_SO_LUONG NUMBER;
    L_CCDC_SO_LUONG_SO_SACH NUMBER;
    L_KIEM_KE_ID NUMBER;
    L_NHOM_CCDC_MA VARCHAR2(50);
    L_NHOM_CCDC_ID NUMBER;
    L_CCDC_TEN VARCHAR2(2000);
BEGIN
    L_JSON_OBJECT := JSON_OBJECT_T(P_JSON_STRING);
    L_CCDC_KIEMKE_ARRAY := L_JSON_OBJECT.GET_ARRAY('CCDC_KIEMKE');
    L_HOI_DONG_ARRAY := L_JSON_OBJECT.GET_ARRAY('THANH_VIEN_HOI_DONG');
    L_CCDC_THUA_ARRAY := L_JSON_OBJECT.GET_ARRAY('CCDC_THUA');
    L_DON_VI_MA := L_JSON_OBJECT.GET_STRING('DON_VI_MA');


    -- check
    IF L_DON_VI_MA IS NULL THEN
        OPEN MSS_OUT FOR
        SELECT JSON_OBJECT(
                'Code' VALUE '01',
                'Message' VALUE 'DON_VI_MA IS NULL',
                'IdRecord' VALUE null
                ) as STRJSON
        FROM dual;
        RETURN;
    ELSE   
        BEGIN
            SELECT 
                ID INTO L_DON_VI_ID
            FROM
                DM_DON_VI
            WHERE
                MA = L_DON_VI_MA;
        EXCEPTION
            WHEN NO_DATA_FOUND THEN
            OPEN MSS_OUT FOR
            SELECT JSON_OBJECT(
                    'Code' VALUE '01',
                    'Message' VALUE 'DON_VI_MA '||L_DON_VI_MA||' NOT FOUND',
                    'IdRecord' VALUE null
                    ) as STRJSON
            FROM dual;
            RETURN;
        END;
    END IF;

    IF L_JSON_OBJECT.GET_DATE('NGAY_KIEM_KE') IS NULL THEN
        OPEN MSS_OUT FOR
        SELECT JSON_OBJECT(
                'Code' VALUE '01',
                'Message' VALUE 'NGAY_KIEM_KE IS NULL',
                'IdRecord' VALUE null
                ) as STRJSON
        FROM dual;
        RETURN;
    END IF;

    --BEGIN KIEM KE
    L_DON_VI_PHAN_BO_MA := L_JSON_OBJECT.GET_STRING('PHONG_BAN_MA');
    L_DON_VI_BO_PHAN_ID := NULL;
    IF L_DON_VI_PHAN_BO_MA IS NOT NULL THEN
        BEGIN
            SELECT 
                ID,TEN INTO L_DON_VI_BO_PHAN_ID,L_DON_VI_PHAN_BO_TEN
            FROM
                DM_DON_VI_BO_PHAN
            WHERE
                MA = L_DON_VI_PHAN_BO_MA
                AND DON_VI_ID = L_DON_VI_ID;
        EXCEPTION
            WHEN NO_DATA_FOUND THEN
            OPEN MSS_OUT FOR
            SELECT JSON_OBJECT(
                    'Code' VALUE '01',
                    'Message' VALUE 'PHONG_BAN_MA '||L_DON_VI_PHAN_BO_MA||' NOT FOUND',
                    'IdRecord' VALUE null
                    ) as STRJSON
            FROM dual;
            RETURN;
        END;
    END IF;

    L_KIEM_KE.SO_KIEM_KE := '00001';
    L_KIEM_KE.DON_VI_ID := L_DON_VI_ID;
    L_KIEM_KE.DON_VI_BO_PHAN_ID := L_DON_VI_BO_PHAN_ID;
    L_KIEM_KE.NGAY_KIEM_KE := L_JSON_OBJECT.GET_DATE('NGAY_KIEM_KE');
    L_KIEM_KE.DAI_DIEN_BO_PHAN := L_JSON_OBJECT.GET_STRING('DAI_DIEN_BO_PHAN');
    L_KIEM_KE.NGUOI_LAP_BIEU := L_JSON_OBJECT.GET_STRING('NGUOI_LAP_BIEU');
    L_KIEM_KE.TRUONG_BAN := L_JSON_OBJECT.GET_STRING('TRUONG_BAN');
    L_KIEM_KE.KE_TOAN := L_JSON_OBJECT.GET_STRING('KE_TOAN');
    
    INSERT INTO CCDC_KIEM_KE
    (
        SO_KIEM_KE,
        DON_VI_ID,
        DON_VI_BO_PHAN_ID,
        NGAY_KIEM_KE,
        DAI_DIEN_BO_PHAN,
        NGUOI_LAP_BIEU,
        TRUONG_BAN,
        NGAY_TAO,
        KE_TOAN
    )
    VALUES
    (
        L_KIEM_KE.SO_KIEM_KE,
        L_KIEM_KE.DON_VI_ID,
        L_KIEM_KE.DON_VI_BO_PHAN_ID,
        L_KIEM_KE.NGAY_KIEM_KE,
        L_KIEM_KE.DAI_DIEN_BO_PHAN,
        L_KIEM_KE.NGUOI_LAP_BIEU,
        L_KIEM_KE.TRUONG_BAN,
        CURRENT_DATE,
        L_KIEM_KE.KE_TOAN
    )RETURNING ID INTO L_KIEM_KE_ID;

    UPDATE CCDC_KIEM_KE
    SET SO_KIEM_KE = LPAD(L_KIEM_KE_ID,5,'0')
    WHERE ID = L_KIEM_KE_ID;
    --END KIEM KE

    IF L_CCDC_KIEMKE_ARRAY IS NOT NULL AND L_CCDC_KIEMKE_ARRAY.GET_SIZE > 0 THEN
        FOR i IN 0..L_CCDC_KIEMKE_ARRAY.GET_SIZE -1 LOOP
            L_CCDC_OBJECT := TREAT(L_CCDC_KIEMKE_ARRAY.GET(i) AS JSON_OBJECT_T);
            L_CCDC_MA := L_CCDC_OBJECT.GET_STRING('MA');
            IF L_CCDC_OBJECT.GET_STRING('MA') IS NULL THEN
                OPEN MSS_OUT FOR
                    SELECT JSON_OBJECT(
                            'Code' VALUE '01',
                                    'Message' VALUE 'CCDC_KIEMKE - MA IS NULL',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                RETURN;
            END IF;
            L_DON_VI_SU_DUNG_MA := L_CCDC_OBJECT.GET_STRING('DON_VI_SU_DUNG_MA');
            IF L_DON_VI_SU_DUNG_MA IS NOT NULL THEN
                BEGIN
                    SELECT 
                        ID INTO L_DON_VI_SU_DUNG_ID
                    FROM
                        DM_DON_VI_BO_PHAN
                    WHERE
                        MA = L_DON_VI_SU_DUNG_MA
                        AND DON_VI_ID = L_DON_VI_ID;
                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                    OPEN MSS_OUT FOR
                    SELECT JSON_OBJECT(
                            'Code' VALUE '01',
                            'Message' VALUE 'CCDC_KIEMKE - DON_VI_SU_DUNG_MA '||L_DON_VI_SU_DUNG_MA||' NOT FOUND',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                    RETURN;
                END;
            ELSE
                OPEN MSS_OUT FOR
                    SELECT JSON_OBJECT(
                            'Code' VALUE '01',
                                    'Message' VALUE 'CCDC_KIEMKE - DON_VI_SU_DUNG_MA IS NULL',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                RETURN;
            END IF;
            IF L_CCDC_OBJECT.GET_NUMBER('SO_LUONG') IS NULL THEN
                OPEN MSS_OUT FOR
                SELECT JSON_OBJECT(
                        'Code' VALUE '01',
                                'Message' VALUE 'CCDC_KIEMKE - SO_LUONG IS NULL',
                        'IdRecord' VALUE null
                        ) as STRJSON
                FROM dual;
                RETURN;
            END IF;
            BEGIN
                SELECT
                    ID INTO L_CCDC_ID
                FROM
                    CCDC_CONG_CU
                WHERE
                    MA_DB = L_CCDC_MA
                    AND DON_VI_ID = L_DON_VI_ID;
                
            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                OPEN MSS_OUT FOR
                SELECT JSON_OBJECT(
                        'Code' VALUE '01',
                                    'Message' VALUE 'CCDC_KIEMKE - MA '||L_CCDC_MA||' NOT FOUND',
                        'IdRecord' VALUE null
                        ) as STRJSON
                FROM dual;
                RETURN;
            END;
            
            SELECT 
                DON_GIA INTO L_CCDC_DON_GIA
            FROM
                CCDC_NHAP_XUAT_CONG_CU
            WHERE   
                CONG_CU_ID = L_CCDC_ID
            ORDER BY ID
            FETCH FIRST 1 ROW ONLY;
            L_CCDC_SO_LUONG_SO_SACH := NVL(L_CCDC_OBJECT.GET_NUMBER('SO_LUONG_SO_SACH'),0);
            IF L_CCDC_SO_LUONG_SO_SACH = 0 THEN
                L_CCDC_SO_LUONG_SO_SACH:=L_CCDC_SO_LUONG;
            END IF;
            INSERT INTO CCDC_KIEM_KE_CONG_CU
            (
                CONG_CU_ID,
                IS_PHAT_HIEN_THUA,
                KIEM_KE_ID,
                SO_LUONG_KIEM_KE,
                SO_LUONG_SO_SACH,
                DON_GIA,
                DON_VI_BO_PHAN_ID
            )
            VALUES
            (
                L_CCDC_ID,
                0,
                L_KIEM_KE_ID,
                L_CCDC_SO_LUONG,
                L_CCDC_SO_LUONG_SO_SACH,
                L_CCDC_DON_GIA,
                L_DON_VI_SU_DUNG_ID
            );
        END LOOP;
    END IF;

    IF L_HOI_DONG_ARRAY IS NOT NULL AND L_HOI_DONG_ARRAY.GET_SIZE > 0 THEN
        FOR i IN 0..L_HOI_DONG_ARRAY.GET_SIZE -1 LOOP
            L_HOI_DONG_OBJECT := TREAT(L_HOI_DONG_ARRAY.GET(i) AS JSON_OBJECT_T);
            IF L_HOI_DONG_OBJECT.GET_STRING('HO_TEN') IS NULL THEN
                OPEN MSS_OUT FOR
                SELECT JSON_OBJECT(
                        'Code' VALUE '01',
                                'Message' VALUE 'THANH_VIEN_HOI_DONG - HO_TEN IS NULL',
                        'IdRecord' VALUE null
                        ) as STRJSON
                FROM dual;
                RETURN;
            END IF;
            
            IF L_HOI_DONG_OBJECT.GET_STRING('CHUC_VU') IS NULL THEN
                OPEN MSS_OUT FOR
                SELECT JSON_OBJECT(
                        'Code' VALUE '01',
                                'Message' VALUE 'THANH_VIEN_HOI_DONG - CHUC_VU IS NULL',
                        'IdRecord' VALUE null
                        ) as STRJSON
                FROM dual;
                RETURN;
            END IF;
            L_HOI_DONG.HO_TEN := L_HOI_DONG_OBJECT.GET_STRING('HO_TEN');
            L_HOI_DONG.CHUC_VU := L_HOI_DONG_OBJECT.GET_STRING('CHUC_VU');
            L_HOI_DONG.DAI_DIEN := L_HOI_DONG_OBJECT.GET_STRING('DAI_DIEN');
            L_HOI_DONG.VI_TRI_ID := NVL(L_HOI_DONG_OBJECT.GET_NUMBER('VI_TRI'),1);
            L_HOI_DONG.KIEM_KE_ID := L_KIEM_KE_ID;
            INSERT INTO CCDC_KIEM_KE_HOI_DONG
            (
                HO_TEN,
                CHUC_VU,
                DAI_DIEN,
                VI_TRI_ID,
                KIEM_KE_ID
            )
            VALUES
            (
                L_HOI_DONG.HO_TEN,
                L_HOI_DONG.CHUC_VU,
                L_HOI_DONG.DAI_DIEN,
                L_HOI_DONG.VI_TRI_ID,
                L_HOI_DONG.KIEM_KE_ID
            );
        END LOOP;
    END IF;        

    IF L_CCDC_THUA_ARRAY IS NOT NULL AND L_CCDC_THUA_ARRAY.GET_SIZE > 0 THEN
        FOR i IN 0..L_CCDC_THUA_ARRAY.GET_SIZE -1 LOOP
            L_CCDC_THUA_OBJECT := TREAT(L_CCDC_THUA_ARRAY.GET(i) AS JSON_OBJECT_T);   
            L_CCDC_TEN := L_CCDC_THUA_OBJECT.GET_STRING('TEN');
            IF L_CCDC_TEN IS NULL THEN
                OPEN MSS_OUT FOR
                    SELECT JSON_OBJECT(
                            'Code' VALUE '01',
                                    'Message' VALUE 'CCDC_THUA - TEN IS NULL',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                RETURN;
            END IF;
            L_DON_VI_SU_DUNG_MA := L_CCDC_OBJECT.GET_STRING('DON_VI_SU_DUNG_MA');
            IF L_DON_VI_SU_DUNG_MA IS NOT NULL THEN
                BEGIN
                    SELECT 
                        ID INTO L_DON_VI_SU_DUNG_ID
                    FROM
                        DM_DON_VI_BO_PHAN
                    WHERE
                        MA = L_DON_VI_SU_DUNG_MA
                        AND DON_VI_ID = L_DON_VI_ID;
                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                    OPEN MSS_OUT FOR
                    SELECT JSON_OBJECT(
                            'Code' VALUE '01',
                            'Message' VALUE 'CCDC_THUA - DON_VI_SU_DUNG_MA '||L_DON_VI_SU_DUNG_MA||' NOT FOUND',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                    RETURN;
                END;
            ELSE
                OPEN MSS_OUT FOR
                    SELECT JSON_OBJECT(
                            'Code' VALUE '01',
                                    'Message' VALUE 'CCDC_THUA - DON_VI_SU_DUNG_MA IS NULL',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                RETURN;
            END IF;
            IF L_CCDC_THUA_OBJECT.GET_STRING('NHOM_CCDC_MA') IS NULL THEN
                OPEN MSS_OUT FOR
                    SELECT JSON_OBJECT(
                            'Code' VALUE '01',
                                    'Message' VALUE 'CCDC_THUA - NHOM_CCDC_MA IS NULL',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                RETURN;
            ELSE
                BEGIN
                    L_NHOM_CCDC_MA := L_CCDC_THUA_OBJECT.GET_STRING('NHOM_CCDC_MA');
                    SELECT
                        ID INTO L_NHOM_CCDC_ID
                    FROM 
                        DM_NHOM_CONG_CU
                    WHERE
                        DON_VI_ID = L_DON_VI_ID
                        AND MA = L_NHOM_CCDC_MA;
                EXCEPTION 
                    WHEN NO_DATA_FOUND THEN
                    OPEN MSS_OUT FOR
                        SELECT JSON_OBJECT(
                                'Code' VALUE '01',
                                            'Message' VALUE 'CCDC_THUA - NHOM_CCDC_MA '||L_NHOM_CCDC_MA||' NOT FOUND',
                                'IdRecord' VALUE null
                                ) as STRJSON
                        FROM dual;
                    RETURN;
                END;
            END IF;

            L_CCDC_DON_GIA := NVL(L_CCDC_THUA_OBJECT.GET_NUMBER('DON_GIA'),0);
            IF L_CCDC_DON_GIA = 0 THEN
                OPEN MSS_OUT FOR
                    SELECT JSON_OBJECT(
                            'Code' VALUE '01',
                                    'Message' VALUE 'CCDC_THUA - DON_GIA < 0',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                RETURN;
            END IF;
            L_CCDC_SO_LUONG := NVL(L_CCDC_THUA_OBJECT.GET_NUMBER('SO_LUONG'),0);
            IF L_CCDC_SO_LUONG = 0 THEN
                OPEN MSS_OUT FOR
                    SELECT JSON_OBJECT(
                            'Code' VALUE '01',
                                    'Message' VALUE 'CCDC_THUA - SO_LUONG < 0',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                RETURN;
            END IF;
            
            INSERT INTO CCDC_KIEM_KE_CONG_CU
            (
                TEN_CONG_CU_THUA,
                NHOM_CONG_CU_ID,
                IS_PHAT_HIEN_THUA,
                KIEM_KE_ID,
                SO_LUONG_KIEM_KE,
                DON_VI_BO_PHAN_ID,
                DON_GIA
            )
            VALUES
            (
                L_CCDC_TEN,
                L_NHOM_CCDC_ID,
                1,
                L_KIEM_KE_ID,
                L_CCDC_SO_LUONG,
                L_DON_VI_SU_DUNG_ID,
                L_CCDC_DON_GIA
            );
        END LOOP;
    END IF;    
    OPEN MSS_OUT FOR
    SELECT JSON_OBJECT(
            'Code' VALUE '00',
            'Message' VALUE 'Done',
            'IdRecord' VALUE null,
            'ObjectInfo' VALUE L_KIEMKE_ID
        ) as STRJSON
    FROM dual;
    RETURN;
END INSERT_KIEM_KE_CCDC;