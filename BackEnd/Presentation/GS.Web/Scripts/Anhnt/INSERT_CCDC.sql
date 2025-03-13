create or replace PROCEDURE INSERT_CCDC(
        P_JSON_STRING IN CLOB,
        MSS_OUT OUT SYS_REFCURSOR
    ) AS 
        L_JSON_OBJECT JSON_OBJECT_T;
        L_CCDC_OBJECT JSON_OBJECT_T;
        L_PHAN_BO_ARRAY JSON_ARRAY_T;
        L_PHAN_BO_OBJECT JSON_OBJECT_T;
        L_LUAN_CHUYEN_ARRAY JSON_ARRAY_T;
        L_GIAM_CCDC_ARRAY JSON_ARRAY_T;
        L_LUAN_CHUYEN_OBJECT JSON_OBJECT_T;
        L_GIAM_CCDC JSON_OBJECT_T;
        L_THONG_TIN_KHAC JSON_OBJECT_T;
        L_SUA_CHUA_ARRAY JSON_ARRAY_T;
        L_SUA_CHUA_OBJECT JSON_OBJECT_T;
        L_CHO_THUE_ARRAY JSON_ARRAY_T;
        L_CHO_THUE_OBJECT JSON_OBJECT_T;


        L_CCDC CCDC_CONG_CU %ROWTYPE;
        L_CCDC2 CCDC_CONG_CU %ROWTYPE;
        L_NHOM_CCDC DM_NHOM_CONG_CU %ROWTYPE;
        L_XUAT_NHAP CCDC_XUAT_NHAP %ROWTYPE;
        L_XUAT_KHO CCDC_XUAT_NHAP %ROWTYPE;
        L_NHAP_DVPB CCDC_XUAT_NHAP %ROWTYPE;
        L_NHAP_LO CCDC_NHAP_XUAT_CONG_CU %ROWTYPE;
        L_MAP_XUAT_KHO CCDC_NHAP_XUAT_CONG_CU %ROWTYPE;
        L_MAP_NHAP_DON_VI CCDC_NHAP_XUAT_CONG_CU %ROWTYPE;
        L_IMP_PHAN_BO CCDC_XUAT_NHAP %ROWTYPE;
        L_SUA_CHUA_BAO_DUONG CCDC_SUA_CHUA_BAO_DUONG %ROWTYPE;
        L_CHO_THUE CCDC_CHO_THUE %ROWTYPE;
        L_LC_NHAP CCDC_XUAT_NHAP %ROWTYPE;
        L_GIAM_XUAT_NHAP CCDC_XUAT_NHAP %ROWTYPE;
        L_GIAM_DC_NHAP CCDC_XUAT_NHAP %ROWTYPE;
        L_GIAM_NHAP_XUAT_CONG_CU CCDC_NHAP_XUAT_CONG_CU %ROWTYPE;
        L_GIAM_DC_MAP_NHAP CCDC_NHAP_XUAT_CONG_CU %ROWTYPE;
        L_GIAM_NHOM_CCDC DM_NHOM_CONG_CU %ROWTYPE;
        L_GIAM_HONG_MAT CCDC_GIAM_HONGMAT%ROWTYPE;
       

        L_MA_DB VARCHAR2(50);
        L_DON_VI_ID NUMBER;
        L_DON_VI_MA VARCHAR2(50);
        L_NHOM_CCDC_ID NUMBER;
        L_NHOM_CCDC_MA VARCHAR2(250);
        L_CCDC_ID NUMBER;
        L_CCDC2_ID NUMBER;
        L_XUAT_NHAP_LO_ID NUMBER;
        L_NHAP_LO_ID NUMBER;
        L_XUAT_KHO_ID NUMBER;
        L_NHAP_DVPB_ID NUMBER;
        L_DON_VI_PHAN_BO_MA VARCHAR2(50);
        L_DON_VI_PHAN_BO_ID NUMBER;
        L_STTXUAT_NHAP NUMBER;
        L_STTCCDC NUMBER;
        L_KHACH_HANG_ID NUMBER;
        L_KHACH_HANG_MA VARCHAR2(50);
        L_MA_PHAN_BO VARCHAR2(50);
        L_IMP_PHAN_BO_DON_GIA NUMBER;
        L_LC_XUAT_ID NUMBER;
        L_LC_SO_LUONG NUMBER;
        L_LC_NHAP_ID NUMBER;
        L_GIAM_DON_VI_MA VARCHAR2(50);
        L_GIAM_DON_VI_ID NUMBER;
        L_GIAM_NHOM_CCDC_ID NUMBER;
        L_GIAM_DC_NHAP_ID NUMBER;
        L_DON_VI_SU_DUNG_MA VARCHAR2(50);
        L_DON_VI_SU_SUNG_ID VARCHAR2(50);
        L_NHAP_KHO_ID NUMBER;
BEGIN
    L_JSON_OBJECT := JSON_OBJECT_T(P_JSON_STRING);
--    L_JSON_OBJECT := JSON_OBJECT_T('{"CONG_CU":{"MA":"004041001-501","TEN":"Máy chiếu tại gia","TRANG_THAI_ID":2,"SO_LUONG":20,"DON_GIA":5400000,"DON_VI_MA":"004041001","NGAY_XUAT_NHAP":"2017-04-01","LY_DO_ID":1,"CHUNG_TU_SO":"CT2017","CHUNG_TU_NGAY":"2017-04-01","GHI_CHU":null,"NHOM_CCDC_MA":"C001"},"PHAN_BO":[{"MA_PHAN_BO":"184d3c9b-9a30-4806-8542-061d5171bb64","DON_VI_PHAN_BO_MA":"P10","SO_LUONG":20,"NGAY_TANG":"2017-04-01","TRANG_THAI":2,"GHI_CHU":null}],"LUAN_CHUYEN":[{"MA_PHAN_BO":"184d3c9b-9a30-4806-8542-061d5171bb64","SO_QUYET_DINH":"QĐ2017","NGAY_QUYET_DINH":"2017-04-01","NGAY_LUAN_CHUYEN":"2018-04-01","DON_VI_NHAN_DIEU_CHUYEN_MA":"P12","SO_LUONG":20,"GHI_CHU":null}],"GIAM_CCDC":[{"DON_VI_TIEP_NHAN":"","MA_PHAN_BO":"184d3c9b-9a30-4806-8542-061d5171bb64","SO_QUYET_DINH":null,"NGAY_QUYET_DINH":"","NGAY_DIEU_CHUYEN":"2019-04-01","CHUNG_TU_SO":null,"CHUNG_TU_NGAY":"","LY_DO_GIAM_ID":6,"SO_LUONG":10,"GHI_CHU":null}],"THONG_TIN_KHAC":{"SUA_CHUA":[],"CHO_THUE":[]}}');
    L_CCDC_OBJECT := L_JSON_OBJECT.GET_OBJECT('CONG_CU');
    L_THONG_TIN_KHAC := L_JSON_OBJECT.GET_OBJECT('THONG_TIN_KHAC');
    L_PHAN_BO_ARRAY := L_JSON_OBJECT.GET_ARRAY('PHAN_BO');
    L_LUAN_CHUYEN_ARRAY := L_JSON_OBJECT.GET_ARRAY('LUAN_CHUYEN');
    L_GIAM_CCDC_ARRAY := L_JSON_OBJECT.GET_ARRAY('GIAM_CCDC');
    L_MA_DB := L_CCDC_OBJECT.GET_STRING('MA');
    DBMS_OUTPUT.PUT_LINE('L_MA_DB :'||L_MA_DB);
    IF L_CCDC_OBJECT IS NULL THEN
        OPEN MSS_OUT FOR
        SELECT JSON_OBJECT(
                'Code' VALUE '01',
                'Message' VALUE 'CONG_CU IS NULL',
                'IdRecord' VALUE null
                ) as STRJSON
        FROM dual;
        RETURN;
    END IF;
    
    L_DON_VI_MA := L_CCDC_OBJECT.GET_STRING('DON_VI_MA');
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
    IF L_CCDC_OBJECT.GET_DATE('NGAY_XUAT_NHAP') IS NULL THEN
        OPEN MSS_OUT FOR
        SELECT JSON_OBJECT(
                'Code' VALUE '01',
                'Message' VALUE 'NGAY_XUAT_NHAP IS NULL',
                'IdRecord' VALUE null
                ) as STRJSON
        FROM dual;
        RETURN;
    END IF;

    
    IF L_CCDC_OBJECT.GET_NUMBER('LY_DO_ID') IS NULL THEN
        OPEN MSS_OUT FOR
        SELECT JSON_OBJECT(
                'Code' VALUE '01',
                'Message' VALUE 'LY_DO_ID IS NULL',
                'IdRecord' VALUE null
                ) as STRJSON
        FROM dual;
        RETURN;
    END IF;
    
    IF L_MA_DB IS NULL THEN
        OPEN MSS_OUT FOR
        SELECT JSON_OBJECT(
                'Code' VALUE '01',
                'Message' VALUE 'CONG_CU - MA IS NULL',
                'IdRecord' VALUE null
                ) as STRJSON
        FROM dual;
        RETURN;
    ELSE
        BEGIN
            SELECT
                ID INTO L_CCDC_ID
            FROM 
                CCDC_CONG_CU
            WHERE 
                MA_DB = L_MA_DB
                AND DON_VI_ID = L_DON_VI_ID;
                
            OPEN MSS_OUT FOR
            SELECT JSON_OBJECT(
                    'Code' VALUE '00',
                    'Message' VALUE 'EXITS',
                    'IdRecord' VALUE null,
                    'ObjectInfo' VALUE L_CCDC_ID
                ) as STRJSON
            FROM dual;
            RETURN;
--            DELETE_CCDC(L_CCDC_ID,1);
--            L_CCDC_ID := NULL;
        EXCEPTION
            WHEN NO_DATA_FOUND THEN
            L_CCDC_ID := NULL;
        END;
    END IF;
    
    IF L_CCDC_OBJECT.GET_STRING('TEN') IS NULL THEN
        OPEN MSS_OUT FOR
        SELECT JSON_OBJECT(
                'Code' VALUE '01',
                'Message' VALUE 'CONG_CU - TEN IS NULL',
                'IdRecord' VALUE null
                ) as STRJSON
        FROM dual;
        RETURN;
    END IF;

    L_NHOM_CCDC_MA := L_CCDC_OBJECT.GET_STRING('NHOM_CCDC_MA');
    IF L_NHOM_CCDC_MA IS NULL THEN
        OPEN MSS_OUT FOR
        SELECT JSON_OBJECT(
                'Code' VALUE '01',
                'Message' VALUE 'CONG_CU - NHOM_CCDC_MA IS NULL',
                'IdRecord' VALUE null
                ) as STRJSON
        FROM dual;
        RETURN;
    ELSE
        BEGIN
            SELECT 
                * INTO L_NHOM_CCDC
            FROM
                DM_NHOM_CONG_CU
            WHERE
                MA = L_NHOM_CCDC_MA
                AND DON_VI_ID = L_DON_VI_ID;
            L_NHOM_CCDC_ID := L_NHOM_CCDC.ID;
        EXCEPTION
            WHEN NO_DATA_FOUND THEN
            OPEN MSS_OUT FOR
            SELECT JSON_OBJECT(
                    'Code' VALUE '01',
                    'Message' VALUE 'CONG_CU - NHOM_CCDC_MA '||L_NHOM_CCDC_MA||' NOT FOUND',
                    'IdRecord' VALUE null
                    ) as STRJSON
            FROM dual;
            RETURN;
        END;
    END IF;
--    DBMS_OUTPUT.PUT_LINE('CONG_CU - TRANG_THAI_ID = '||L_CCDC_OBJECT.GET_NUMBER('TRANG_TA);
    IF L_CCDC_OBJECT.GET_NUMBER('TRANG_THAI_ID') IS NULL THEN
        OPEN MSS_OUT FOR
        SELECT JSON_OBJECT(
                'Code' VALUE '01',
                'Message' VALUE 'CONG_CU - TRANG_THAI_ID IS NULL',
                'IdRecord' VALUE null
                ) as STRJSON
        FROM dual;
        RETURN;
    END IF;
    
    IF NVL(L_CCDC_OBJECT.GET_NUMBER('SO_LUONG'),0) = 0 THEN
        OPEN MSS_OUT FOR
        SELECT JSON_OBJECT(
                'Code' VALUE '01',
                'Message' VALUE 'CONG_CU - SO_LUONG IS NULL',
                'IdRecord' VALUE null
                ) as STRJSON
        FROM dual;
        RETURN;
    END IF;
    
    IF L_CCDC_OBJECT.GET_NUMBER('DON_GIA') IS NULL THEN
        OPEN MSS_OUT FOR
        SELECT JSON_OBJECT(
                'Code' VALUE '01',
                'Message' VALUE 'CONG_CU - DON_GIA IS NULL',
                'IdRecord' VALUE null
                ) as STRJSON
        FROM dual;
        RETURN;
    END IF;


    DBMS_OUTPUT.PUT_LINE('L_THONG_TIN_KHAC '||case when L_THONG_TIN_KHAC is null then 1 else 0 end);
    IF L_THONG_TIN_KHAC IS NOT NULL THEN
    --sửa chữa
        L_SUA_CHUA_ARRAY := L_THONG_TIN_KHAC.GET_ARRAY('SUA_CHUA');
        IF L_SUA_CHUA_ARRAY IS NOT NULL AND L_SUA_CHUA_ARRAY.GET_SIZE > 0 THEN 
            FOR i IN 0..L_SUA_CHUA_ARRAY.GET_SIZE -1 LOOP 
                L_SUA_CHUA_OBJECT := TREAT(L_SUA_CHUA_ARRAY.GET(i) AS JSON_OBJECT_T);

                IF L_SUA_CHUA_OBJECT.GET_STRING('DON_VI_SU_DUNG_MA') IS NULL THEN
                    OPEN MSS_OUT FOR
                    SELECT JSON_OBJECT(
                            'Code' VALUE '01',
                            'Message' VALUE 'SUA_CHUA - DON_VI_SU_DUNG_MA IS NULL',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                    RETURN;
                END IF;

                IF L_SUA_CHUA_OBJECT.GET_DATE('NGAY_SUA_CHUA_TU') IS NULL THEN
                    OPEN MSS_OUT FOR
                    SELECT JSON_OBJECT(
                            'Code' VALUE '01',
                            'Message' VALUE 'SUA_CHUA - NGAY_SUA_CHUA_TU IS NULL',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                    RETURN;
                END IF;
                IF L_SUA_CHUA_OBJECT.GET_DATE('NGAY_SUA_CHUA_DEN') IS NULL THEN
                    OPEN MSS_OUT FOR
                    SELECT JSON_OBJECT(
                            'Code' VALUE '01',
                            'Message' VALUE 'SUA_CHUA - NGAY_SUA_CHUA_DEN IS NULL',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                    RETURN;
                END IF;
            
                IF NVL(L_SUA_CHUA_OBJECT.GET_NUMBER('SO_LUONG'),0) = 0  THEN
                    OPEN MSS_OUT FOR
                    SELECT JSON_OBJECT(
                            'Code' VALUE '01',
                            'Message' VALUE 'SUA_CHUA - SO_LUONG IS NULL',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                    RETURN;
                END IF;
            
                IF NVL(L_SUA_CHUA_OBJECT.GET_NUMBER('GIA_TRI'),0) = 0  THEN
                    OPEN MSS_OUT FOR
                    SELECT JSON_OBJECT(
                            'Code' VALUE '01',
                            'Message' VALUE 'SUA_CHUA - GIA_TRI IS NULL',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                    RETURN;
                END IF;
            END LOOP;
        END IF;

        --cho thuê
        L_CHO_THUE_ARRAY := L_THONG_TIN_KHAC.GET_ARRAY('CHO_THUE');
        IF L_CHO_THUE_ARRAY IS NOT NULL AND L_CHO_THUE_ARRAY.GET_SIZE > 0 THEN 
            FOR i IN 0..L_CHO_THUE_ARRAY.GET_SIZE -1 LOOP 
                L_CHO_THUE_OBJECT := TREAT(L_CHO_THUE_ARRAY.GET(i) AS JSON_OBJECT_T);
                IF L_CHO_THUE_OBJECT.GET_STRING('DON_VI_SU_DUNG_MA') IS NULL THEN
                    OPEN MSS_OUT FOR
                    SELECT JSON_OBJECT(
                            'Code' VALUE '01',
                            'Message' VALUE 'CHO_THUE - DON_VI_SU_DUNG_MA IS NULL',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                    RETURN;
                END IF;
                IF L_CHO_THUE_OBJECT.GET_DATE('NGAY_CHO_THUE_TU') IS NULL THEN
                    OPEN MSS_OUT FOR
                    SELECT JSON_OBJECT(
                            'Code' VALUE '01',
                            'Message' VALUE 'CHO_THUE - NGAY_CHO_THUE_TU IS NULL',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                    RETURN;
                END IF;
                IF L_CHO_THUE_OBJECT.GET_DATE('NGAY_CHO_THUE_DEN') IS NULL THEN
                    OPEN MSS_OUT FOR
                    SELECT JSON_OBJECT(
                            'Code' VALUE '01',
                            'Message' VALUE 'CHO_THUE - NGAY_CHO_THUE_DEN IS NULL',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                    RETURN;
                END IF;
            
                IF NVL(L_CHO_THUE_OBJECT.GET_NUMBER('SO_LUONG'),0) = 0  THEN
                    OPEN MSS_OUT FOR
                    SELECT JSON_OBJECT(
                            'Code' VALUE '01',
                            'Message' VALUE 'CHO_THUE - SO_LUONG IS NULL',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                    RETURN;
                END IF;
            
                IF NVL(L_CHO_THUE_OBJECT.GET_NUMBER('GIA_TRI'),0) = 0  THEN
                    OPEN MSS_OUT FOR
                    SELECT JSON_OBJECT(
                            'Code' VALUE '01',
                            'Message' VALUE 'CHO_THUE - GIA_TRI IS NULL',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                    RETURN;
                END IF;
            END LOOP;
        END IF;
    END IF;
    --luân chuyển
    IF L_LUAN_CHUYEN_ARRAY IS NOT NULL AND L_LUAN_CHUYEN_ARRAY.GET_SIZE > 0 THEN 
        FOR i IN 0..L_LUAN_CHUYEN_ARRAY.GET_SIZE -1 LOOP 
            L_LUAN_CHUYEN_OBJECT := TREAT(L_LUAN_CHUYEN_ARRAY.GET(i) AS JSON_OBJECT_T);
            IF L_LUAN_CHUYEN_OBJECT.GET_STRING('DON_VI_SU_DUNG_MA') IS NULL THEN
                OPEN MSS_OUT FOR
                SELECT JSON_OBJECT(
                        'Code' VALUE '01',
                        'Message' VALUE 'LUAN_CHUYEN - DON_VI_SU_DUNG_MA IS NULL',
                        'IdRecord' VALUE null
                        ) as STRJSON
                FROM dual;
                RETURN;
            END IF;

            IF L_LUAN_CHUYEN_OBJECT.GET_DATE('NGAY_LUAN_CHUYEN') IS NULL THEN
                OPEN MSS_OUT FOR
                SELECT JSON_OBJECT(
                        'Code' VALUE '01',
                        'Message' VALUE 'LUAN_CHUYEN - NGAY_LUAN_CHUYEN IS NULL',
                        'IdRecord' VALUE null
                        ) as STRJSON
                FROM dual;
                RETURN;
            END IF;
            IF NVL(L_LUAN_CHUYEN_OBJECT.GET_NUMBER('SO_LUONG'),0) = 0  THEN
                OPEN MSS_OUT FOR
                SELECT JSON_OBJECT(
                        'Code' VALUE '01',
                        'Message' VALUE 'LUAN_CHUYEN - SO_LUONG IS NULL',
                        'IdRecord' VALUE null
                        ) as STRJSON
                FROM dual;
                RETURN;
            END IF;
        
            IF L_LUAN_CHUYEN_OBJECT.GET_STRING('DON_VI_NHAN_DIEU_CHUYEN_MA') IS NULL THEN
                OPEN MSS_OUT FOR
                SELECT JSON_OBJECT(
                        'Code' VALUE '01',
                        'Message' VALUE 'LUAN_CHUYEN - DON_VI_NHAN_DIEU_CHUYEN_MA IS NULL',
                        'IdRecord' VALUE null
                        ) as STRJSON
                FROM dual;
                RETURN;
            END IF;
        END LOOP;
    END IF;
    --giảm công cụ
    IF L_GIAM_CCDC_ARRAY IS NOT NULL AND L_GIAM_CCDC_ARRAY.GET_SIZE > 0 THEN 
        FOR i IN 0..L_GIAM_CCDC_ARRAY.GET_SIZE -1 LOOP 
            L_GIAM_CCDC := TREAT(L_GIAM_CCDC_ARRAY.GET(i) AS JSON_OBJECT_T);
            IF L_GIAM_CCDC.GET_STRING('DON_VI_SU_DUNG_MA') IS NULL THEN
                OPEN MSS_OUT FOR
                SELECT JSON_OBJECT(
                        'Code' VALUE '01',
                        'Message' VALUE 'GIAM_CCDC - DON_VI_SU_DUNG_MA IS NULL',
                        'IdRecord' VALUE null
                        ) as STRJSON
                FROM dual;
                RETURN;
            END IF;

            IF L_GIAM_CCDC.GET_DATE('NGAY_DIEU_CHUYEN') IS NULL THEN
                OPEN MSS_OUT FOR
                SELECT JSON_OBJECT(
                        'Code' VALUE '01',
                        'Message' VALUE 'GIAM_CCDC - NGAY_DIEU_CHUYEN IS NULL',
                        'IdRecord' VALUE null
                        ) as STRJSON
                FROM dual;
                RETURN;
            END IF;
        
            IF L_GIAM_CCDC.GET_STRING('DON_VI_TIEP_NHAN') IS NULL AND L_GIAM_CCDC.GET_NUMBER('LY_DO_GIAM_ID') = 4 THEN
                OPEN MSS_OUT FOR
                SELECT JSON_OBJECT(
                        'Code' VALUE '01',
                        'Message' VALUE 'GIAM_CCDC - DON_VI_TIEP_NHAN IS NULL',
                        'IdRecord' VALUE null
                        ) as STRJSON
                FROM dual;
                RETURN;
            END IF;
        END LOOP;
    END IF;
    
    --prepare
    L_XUAT_NHAP.TEN := L_CCDC_OBJECT.GET_STRING('TEN');
    L_XUAT_NHAP.DON_VI_ID := L_DON_VI_ID;
    L_XUAT_NHAP.IS_XUAT := 0;
    L_XUAT_NHAP.NGAY_XUAT_NHAP := L_CCDC_OBJECT.GET_DATE('NGAY_XUAT_NHAP');
    L_XUAT_NHAP.MA :=  L_DON_VI_MA;
    L_XUAT_NHAP.MA_LIEN_QUAN := L_DON_VI_MA;
    L_XUAT_NHAP.GHI_CHU := L_CCDC_OBJECT.GET_STRING('GHI_CHU');
    L_XUAT_NHAP.MUC_DICH_XUAT_NHAP_ID := L_CCDC_OBJECT.GET_NUMBER('LY_DO_ID');
    L_XUAT_NHAP.LOAI_XUAT_NHAP_ID := 1;

    INSERT INTO CCDC_XUAT_NHAP
    (
        TEN,
        DON_VI_ID,
        IS_XUAT,
        NGAY_XUAT_NHAP,
        MA,
        MA_LIEN_QUAN,
        GHI_CHU,
        MUC_DICH_XUAT_NHAP_ID,
        LOAI_XUAT_NHAP_ID,
        NGAY_TAO,
        GUID
    )
    VALUES
    (
        L_XUAT_NHAP.TEN,
        L_XUAT_NHAP.DON_VI_ID,
        L_XUAT_NHAP.IS_XUAT,
        L_XUAT_NHAP.NGAY_XUAT_NHAP,
        L_XUAT_NHAP.MA,
        L_XUAT_NHAP.MA_LIEN_QUAN,
        L_XUAT_NHAP.GHI_CHU,
        L_XUAT_NHAP.MUC_DICH_XUAT_NHAP_ID,
        L_XUAT_NHAP.LOAI_XUAT_NHAP_ID,
        SYSDATE,
        SYS_GUID()
    ) RETURNING ID INTO L_XUAT_NHAP_LO_ID;
    L_XUAT_NHAP.MA := L_DON_VI_MA || '-' || L_XUAT_NHAP_LO_ID;
    L_XUAT_NHAP.MA_LIEN_QUAN := L_DON_VI_MA || '-' || L_XUAT_NHAP_LO_ID;
    UPDATE CCDC_XUAT_NHAP
    SET MA = L_DON_VI_MA || '-' || L_XUAT_NHAP_LO_ID,
        MA_LIEN_QUAN = L_DON_VI_MA || '-' || L_XUAT_NHAP_LO_ID
    WHERE ID = L_XUAT_NHAP_LO_ID;
               
    --công cụ

    L_CCDC.DON_VI_ID := L_DON_VI_ID;
    L_CCDC.TEN := L_CCDC_OBJECT.GET_STRING('TEN');
    L_CCDC.NHOM_CONG_CU_ID := L_NHOM_CCDC_ID;
    L_CCDC.MA := L_DON_VI_MA || '-' || L_NHOM_CCDC_ID;
    L_CCDC.MA_DB := L_MA_DB;

    INSERT INTO CCDC_CONG_CU
    (
        DON_VI_ID,
        TEN,
        NHOM_CONG_CU_ID,
        MA,
        MA_DB,
        NGAY_TAO,
        GUID
    )
    VALUES
    (
        L_CCDC.DON_VI_ID,
        L_CCDC.TEN,
        L_CCDC.NHOM_CONG_CU_ID,
        L_CCDC.MA,
        L_CCDC.MA_DB,
        SYSDATE,
        SYS_GUID()
    ) RETURNING ID INTO L_CCDC_ID;
    L_CCDC.MA := L_DON_VI_MA || '-' || L_NHOM_CCDC_ID || '-' || L_CCDC_ID;
    UPDATE CCDC_CONG_CU
    SET MA = L_DON_VI_MA || '-' || L_NHOM_CCDC_ID || '-' || L_CCDC_ID
    WHERE ID = L_CCDC_ID;

    L_NHAP_LO.NHAP_XUAT_ID := L_XUAT_NHAP_LO_ID;
    L_NHAP_LO.CONG_CU_ID := L_CCDC_ID;
    L_NHAP_LO.CHUNG_TU_SO := L_CCDC_OBJECT.GET_STRING('CHUNG_TU_SO');
    L_NHAP_LO.CHUNG_TU_NGAY := L_CCDC_OBJECT.GET_DATE('CHUNG_TU_NGAY');
    L_NHAP_LO.DON_GIA := NVL(L_CCDC_OBJECT.GET_NUMBER('DON_GIA'),0);
    L_NHAP_LO.SO_LUONG := L_CCDC_OBJECT.GET_NUMBER('SO_LUONG');
    L_NHAP_LO.TRANG_THAI_ID := L_CCDC_OBJECT.GET_NUMBER('TRANG_THAI_ID');
    L_NHAP_LO.THANH_TIEN := NVL(L_CCDC_OBJECT.GET_NUMBER('DON_GIA'),0)  * NVL(L_CCDC_OBJECT.GET_NUMBER('SO_LUONG'),0);

    INSERT INTO CCDC_NHAP_XUAT_CONG_CU
    (
        NHAP_XUAT_ID,
        CONG_CU_ID,
        CHUNG_TU_SO,
        CHUNG_TU_NGAY,
        DON_GIA,
        SO_LUONG,
        TRANG_THAI_ID,
        THANH_TIEN
    )
    VALUES
    (
        L_NHAP_LO.NHAP_XUAT_ID,
        L_NHAP_LO.CONG_CU_ID,
        L_NHAP_LO.CHUNG_TU_SO,
        L_NHAP_LO.CHUNG_TU_NGAY,
        L_NHAP_LO.DON_GIA,
        L_NHAP_LO.SO_LUONG,
        L_NHAP_LO.TRANG_THAI_ID,
        L_NHAP_LO.THANH_TIEN
    ) RETURNING ID INTO L_NHAP_LO_ID;
    --phân bổ
    IF L_PHAN_BO_ARRAY IS NOT NULL AND L_PHAN_BO_ARRAY.GET_SIZE > 0 THEN 
        FOR i IN 0..L_PHAN_BO_ARRAY.GET_SIZE -1 LOOP 
            L_PHAN_BO_OBJECT := TREAT(L_PHAN_BO_ARRAY.GET(i) AS JSON_OBJECT_T);
            IF L_PHAN_BO_OBJECT.GET_DATE('NGAY_TANG') IS NULL THEN
                OPEN MSS_OUT FOR
                SELECT JSON_OBJECT(
                        'Code' VALUE '01',
                        'Message' VALUE 'PHAN_BO - NGAY_TANG IS NULL',
                        'IdRecord' VALUE null
                        ) as STRJSON
                FROM dual;
                DELETE_CCDC(L_CCDC_ID,1);
                RETURN;
            END IF;
            
            IF NVL(L_PHAN_BO_OBJECT.GET_NUMBER('TRANG_THAI'),0) = 0  THEN
                OPEN MSS_OUT FOR
                SELECT JSON_OBJECT(
                        'Code' VALUE '01',
                        'Message' VALUE 'PHAN_BO - TRANG_THAI IS NULL',
                        'IdRecord' VALUE null
                        ) as STRJSON
                FROM dual;
                DELETE_CCDC(L_CCDC_ID,1);
                RETURN;
            END IF;
            -- đơn vị phân bổ
            L_DON_VI_PHAN_BO_MA := L_PHAN_BO_OBJECT.GET_STRING('DON_VI_PHAN_BO_MA');
            IF L_DON_VI_PHAN_BO_MA IS NULL THEN
                OPEN MSS_OUT FOR
                SELECT JSON_OBJECT(
                        'Code' VALUE '01',
                        'Message' VALUE 'PHAN_BO - DON_VI_PHAN_BO_MA IS NULL',
                        'IdRecord' VALUE null
                        ) as STRJSON
                FROM dual;
                DELETE_CCDC(L_CCDC_ID,1);
                RETURN;
            ELSE   
                BEGIN
                    SELECT 
                        ID INTO L_DON_VI_PHAN_BO_ID
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
                            'Message' VALUE 'PHAN_BO - DON_VI_PHAN_BO_MA '||L_DON_VI_PHAN_BO_MA||' NOT FOUND',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                	DELETE_CCDC(L_CCDC_ID,1);
                    RETURN;
                END;
            END IF;


            L_XUAT_KHO.FROM_XUAT_NHAP_ID := L_XUAT_NHAP_LO_ID;
            L_XUAT_KHO.TEN := L_CCDC.TEN;
            L_XUAT_KHO.DON_VI_ID := L_DON_VI_ID;
            L_XUAT_KHO.IS_XUAT := 1;
            L_XUAT_KHO.NGAY_XUAT_NHAP := L_PHAN_BO_OBJECT.GET_DATE('NGAY_TANG');
            L_XUAT_KHO.MA := L_DON_VI_MA;
            L_XUAT_KHO.MA_LIEN_QUAN := L_XUAT_NHAP.MA_LIEN_QUAN;
            L_XUAT_KHO.GHI_CHU := L_PHAN_BO_OBJECT.GET_STRING('GHI_CHU');
            L_XUAT_KHO.LOAI_XUAT_NHAP_ID := 2;
            L_XUAT_KHO.IS_PHAN_BO_FIRST := (CASE WHEN i = 0 THEN 1
                                                ELSE 0 END);

            INSERT INTO CCDC_XUAT_NHAP
            (
                FROM_XUAT_NHAP_ID,
                TEN,
                DON_VI_ID,
                IS_XUAT,
                NGAY_XUAT_NHAP,
                MA,
                MA_LIEN_QUAN,
                GHI_CHU,
                LOAI_XUAT_NHAP_ID,
                IS_PHAN_BO_FIRST,
                NGAY_TAO,
                GUID
            )
            VALUES
            (
                L_XUAT_KHO.FROM_XUAT_NHAP_ID,
                L_XUAT_KHO.TEN,
                L_XUAT_KHO.DON_VI_ID,
                L_XUAT_KHO.IS_XUAT,
                L_XUAT_KHO.NGAY_XUAT_NHAP,
                L_XUAT_KHO.MA,
                L_XUAT_KHO.MA_LIEN_QUAN,
                L_XUAT_KHO.GHI_CHU,
                L_XUAT_KHO.LOAI_XUAT_NHAP_ID,
                L_XUAT_KHO.IS_PHAN_BO_FIRST,
                SYSDATE,
                SYS_GUID()
            ) RETURNING ID INTO L_XUAT_KHO_ID;
            L_XUAT_KHO.MA := L_DON_VI_MA || '-' || L_XUAT_KHO_ID;
            UPDATE CCDC_XUAT_NHAP
            SET MA = L_DON_VI_MA || '-' || L_XUAT_KHO_ID
            WHERE ID = L_XUAT_KHO_ID;
            -- nhập đơn vị phân bổ

            L_NHAP_DVPB.FROM_XUAT_NHAP_ID := L_XUAT_KHO_ID;
            L_NHAP_DVPB.TEN := L_CCDC.TEN;
            L_NHAP_DVPB.DON_VI_BO_PHAN_ID := L_DON_VI_PHAN_BO_ID;
            L_NHAP_DVPB.DON_VI_ID := L_DON_VI_ID;
            L_NHAP_DVPB.IS_XUAT := 0;
            L_NHAP_DVPB.NGAY_XUAT_NHAP := L_PHAN_BO_OBJECT.GET_DATE('NGAY_TANG');
            L_NHAP_DVPB.MA := L_DON_VI_MA;
            L_NHAP_DVPB.MA_LIEN_QUAN := L_XUAT_NHAP.MA_LIEN_QUAN;
            L_NHAP_DVPB.GHI_CHU := L_PHAN_BO_OBJECT.GET_STRING('GHI_CHU');
            L_NHAP_DVPB.LOAI_XUAT_NHAP_ID := 2;
            L_NHAP_DVPB.MA_DB := L_PHAN_BO_OBJECT.GET_STRING('MA_PHAN_BO');
            INSERT INTO CCDC_XUAT_NHAP
            (
                FROM_XUAT_NHAP_ID,
                TEN,
                DON_VI_BO_PHAN_ID,
                DON_VI_ID,
                IS_XUAT,
                NGAY_XUAT_NHAP,
                MA,
                MA_LIEN_QUAN,
                GHI_CHU,
                LOAI_XUAT_NHAP_ID,
                MA_DB,
                NGAY_TAO,
                GUID
            )
            VALUES
            (
                L_NHAP_DVPB.FROM_XUAT_NHAP_ID,
                L_NHAP_DVPB.TEN,
                L_NHAP_DVPB.DON_VI_BO_PHAN_ID,
                L_NHAP_DVPB.DON_VI_ID,
                L_NHAP_DVPB.IS_XUAT,
                L_NHAP_DVPB.NGAY_XUAT_NHAP,
                L_NHAP_DVPB.MA,
                L_NHAP_DVPB.MA_LIEN_QUAN,
                L_NHAP_DVPB.GHI_CHU,
                L_NHAP_DVPB.LOAI_XUAT_NHAP_ID,
                L_NHAP_DVPB.MA_DB,
                SYSDATE,
                SYS_GUID()
            ) RETURNING ID INTO L_NHAP_DVPB_ID;
            L_NHAP_DVPB.MA := L_DON_VI_MA || '-' || L_NHAP_DVPB_ID;
            UPDATE CCDC_XUAT_NHAP
            SET MA = L_DON_VI_MA || '-' || L_NHAP_DVPB_ID
            WHERE ID = L_NHAP_DVPB_ID;
            -- insert xuất/nhập đơn vị bộ phận
            -- mapping
            L_MAP_XUAT_KHO.CONG_CU_ID := L_CCDC_ID;
            L_MAP_XUAT_KHO.NHAP_XUAT_ID := L_XUAT_KHO_ID;
            L_MAP_XUAT_KHO.SO_LUONG := L_PHAN_BO_OBJECT.GET_NUMBER('SO_LUONG');
            L_MAP_XUAT_KHO.DON_GIA := L_CCDC_OBJECT.GET_NUMBER('DON_GIA');
            L_MAP_XUAT_KHO.TRANG_THAI_ID := L_PHAN_BO_OBJECT.GET_NUMBER('TRANG_THAI');
            L_MAP_XUAT_KHO.NHAP_KHO_ID := L_NHAP_LO_ID;
            INSERT INTO CCDC_NHAP_XUAT_CONG_CU
            (
                CONG_CU_ID,
                NHAP_XUAT_ID,
                SO_LUONG,
                DON_GIA,
                TRANG_THAI_ID,
                NHAP_KHO_ID
            )
            VALUES
            (
                L_MAP_XUAT_KHO.CONG_CU_ID,
                L_MAP_XUAT_KHO.NHAP_XUAT_ID,
                L_MAP_XUAT_KHO.SO_LUONG,
                L_MAP_XUAT_KHO.DON_GIA,
                L_MAP_XUAT_KHO.TRANG_THAI_ID,
                L_MAP_XUAT_KHO.NHAP_KHO_ID
            );

            L_MAP_NHAP_DON_VI.CONG_CU_ID := L_CCDC_ID;
            L_MAP_NHAP_DON_VI.NHAP_XUAT_ID := L_NHAP_DVPB_ID;
            L_MAP_NHAP_DON_VI.SO_LUONG := L_PHAN_BO_OBJECT.GET_NUMBER('SO_LUONG');
            L_MAP_NHAP_DON_VI.DON_GIA := L_CCDC_OBJECT.GET_NUMBER('DON_GIA');
            L_MAP_NHAP_DON_VI.TRANG_THAI_ID := L_PHAN_BO_OBJECT.GET_NUMBER('TRANG_THAI');
            INSERT INTO CCDC_NHAP_XUAT_CONG_CU
            (
                CONG_CU_ID,
                NHAP_XUAT_ID,
                SO_LUONG,
                DON_GIA,
                TRANG_THAI_ID
            )
            VALUES
            (
                L_MAP_NHAP_DON_VI.CONG_CU_ID,
                L_MAP_NHAP_DON_VI.NHAP_XUAT_ID,
                L_MAP_NHAP_DON_VI.SO_LUONG,
                L_MAP_NHAP_DON_VI.DON_GIA,
                L_MAP_NHAP_DON_VI.TRANG_THAI_ID
            );
            -- phanBo.nhapXuatMap := mappingNhapDonVi;
        END LOOP;
        IF L_THONG_TIN_KHAC IS NOT NULL THEN
        --sửa chữa
            DBMS_OUTPUT.PUT_LINE('BEGIN SUA_CHUA');
            L_SUA_CHUA_ARRAY := L_THONG_TIN_KHAC.GET_ARRAY('SUA_CHUA');
            IF L_SUA_CHUA_ARRAY IS NOT NULL AND L_SUA_CHUA_ARRAY.GET_SIZE > 0 THEN 
                FOR i IN 0..L_SUA_CHUA_ARRAY.GET_SIZE -1 LOOP 
                    L_SUA_CHUA_OBJECT := TREAT(L_SUA_CHUA_ARRAY.GET(i) AS JSON_OBJECT_T);
                    L_DON_VI_SU_DUNG_MA := L_SUA_CHUA_OBJECT.GET_STRING('DON_VI_SU_DUNG_MA');
                    BEGIN
                        SELECT 
                            ID INTO L_DON_VI_SU_SUNG_ID
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
                                        'Message' VALUE 'THONG_TIN_KHAC - SUA_CHUA - DON_VI_SU_DUNG_MA NOT FOUND',
                                'IdRecord' VALUE null
                                ) as STRJSON
                        FROM dual;
                		DELETE_CCDC(L_CCDC_ID,1);
                        RETURN;
                    END;
                    L_MA_PHAN_BO := L_SUA_CHUA_OBJECT.GET_STRING('MA_PHAN_BO');
                    BEGIN
                        SELECT
                            * INTO L_IMP_PHAN_BO
                        FROM
                            CCDC_XUAT_NHAP
                        WHERE
                            MA_DB = L_MA_PHAN_BO;

                        IF L_IMP_PHAN_BO.DON_VI_BO_PHAN_ID != L_DON_VI_SU_SUNG_ID THEN
                            BEGIN
                                SELECT
                                    * INTO L_IMP_PHAN_BO
                                FROM
                                    CCDC_XUAT_NHAP
                                WHERE
                                    DON_VI_BO_PHAN_ID = L_DON_VI_SU_SUNG_ID
                                    AND IS_XUAT = 0
                                    AND ID IN ( SELECT 
                                                    NHAP_XUAT_ID
                                                FROM 
                                                    CCDC_NHAP_XUAT_CONG_CU
                                                WHERE
                                                    CONG_CU_ID = L_CCDC_ID)
                                ORDER BY ID DESC
                                FETCH FIRST 1 ROW ONLY;
                            EXCEPTION
                                WHEN NO_DATA_FOUND THEN
                                    SELECT
                                        * INTO L_IMP_PHAN_BO
                                    FROM
                                        CCDC_XUAT_NHAP
                                    WHERE
                                        MA_DB = L_MA_PHAN_BO;
                            END;
                        END IF;
                        
                    EXCEPTION
                        WHEN NO_DATA_FOUND THEN
                        OPEN MSS_OUT FOR
                        SELECT JSON_OBJECT(
                                'Code' VALUE '01',
                                        'Message' VALUE 'THONG_TIN_KHAC - SUA_CHUA - MA_PHAN_BO NOT FOUND IN ARRAY PHAN_BO',
                                'IdRecord' VALUE null
                                ) as STRJSON
                        FROM dual;
                		DELETE_CCDC(L_CCDC_ID,1);
                        RETURN;
                    END;
                    
                    BEGIN
                        SELECT
                            DON_GIA INTO L_IMP_PHAN_BO_DON_GIA
                        FROM
                            CCDC_NHAP_XUAT_CONG_CU
                        WHERE
                            NHAP_XUAT_ID = L_IMP_PHAN_BO.ID
                            AND CONG_CU_ID = L_CCDC_ID;
                    EXCEPTION
                        WHEN NO_DATA_FOUND THEN
                            L_IMP_PHAN_BO_DON_GIA:=0;
                    END;
                    L_KHACH_HANG_MA := L_SUA_CHUA_OBJECT.GET_STRING('MA_DOI_TAC');
                    IF L_KHACH_HANG_MA IS NOT NULL THEN
                        BEGIN
                            SELECT 
                                ID INTO L_KHACH_HANG_ID
                            FROM
                                DM_DOI_TAC
                            WHERE
                                MA = L_KHACH_HANG_MA
                                AND DON_VI_ID = L_DON_VI_ID
                                AND (LOAI_DOI_TAC_ID = 0 OR LOAI_DOI_TAC_ID IS NULL);
                        EXCEPTION
                            WHEN NO_DATA_FOUND THEN
                            OPEN MSS_OUT FOR
                            SELECT JSON_OBJECT(
                                    'Code' VALUE '01',
                                                'Message' VALUE 'THONG_TIN_KHAC - SUA_CHUA - MA_DOI_TAC '||L_KHACH_HANG_MA||' NOT FOUND',
                                    'IdRecord' VALUE null
                                    ) as STRJSON
                            FROM dual;
                			DELETE_CCDC(L_CCDC_ID,1);
                            RETURN;
                        END;
                    ELSE
                        L_KHACH_HANG_ID:=NULL;
                    END IF;
                    L_SUA_CHUA_BAO_DUONG.CAP_QUYET_DINH := L_SUA_CHUA_OBJECT.GET_STRING('CAP_QUYET_DINH');
                    L_SUA_CHUA_BAO_DUONG.CHUNG_TU_NGAY := L_SUA_CHUA_OBJECT.GET_DATE('CHUNG_TU_NGAY');
                    L_SUA_CHUA_BAO_DUONG.CHUNG_TU_SO := L_SUA_CHUA_OBJECT.GET_STRING('CHUNG_TU_SO');
                    L_SUA_CHUA_BAO_DUONG.CONG_CU_ID := L_CCDC_ID;
                    L_SUA_CHUA_BAO_DUONG.GHI_CHU := L_SUA_CHUA_OBJECT.GET_STRING('GHI_CHU');
                    L_SUA_CHUA_BAO_DUONG.NGAY_QUYET_DINH := L_SUA_CHUA_OBJECT.GET_DATE('NGAY_QUYET_DINH');
                    L_SUA_CHUA_BAO_DUONG.NGAY_SUA_CHUA_FROM := NVL(L_SUA_CHUA_OBJECT.GET_DATE('NGAY_SUA_CHUA_TU'),SYSDATE);
                    L_SUA_CHUA_BAO_DUONG.NGAY_SUA_CHUA_TO := NVL(L_SUA_CHUA_OBJECT.GET_DATE('NGAY_SUA_CHUA_DEN'),SYSDATE);
                    L_SUA_CHUA_BAO_DUONG.SO_QUYET_DINH := L_SUA_CHUA_OBJECT.GET_STRING('SO_QUYET_DINH');
                    L_SUA_CHUA_BAO_DUONG.GIA_TRI_SUA_CHUA := NVL(L_SUA_CHUA_OBJECT.GET_NUMBER('GIA_TRI'),0);
                    L_SUA_CHUA_BAO_DUONG.SO_LUONG_SUA_CHUA := NVL(L_SUA_CHUA_OBJECT.GET_NUMBER('SO_LUONG'),0);
                    L_SUA_CHUA_BAO_DUONG.NHAP_XUAT_ID := L_IMP_PHAN_BO.ID;
                    L_SUA_CHUA_BAO_DUONG.KHACH_HANG_ID := L_KHACH_HANG_ID;
                    INSERT INTO CCDC_SUA_CHUA_BAO_DUONG
                    (
                        CAP_QUYET_DINH,
                        CHUNG_TU_NGAY,
                        CHUNG_TU_SO,
                        CONG_CU_ID,
                        GHI_CHU,
                        NGAY_QUYET_DINH,
                        NGAY_SUA_CHUA_FROM,
                        NGAY_SUA_CHUA_TO,
                        SO_QUYET_DINH,
                        GIA_TRI_SUA_CHUA,
                        SO_LUONG_SUA_CHUA,
                        NHAP_XUAT_ID,
                        KHACH_HANG_ID
                    )
                    VALUES
                    (
                        L_SUA_CHUA_BAO_DUONG.CAP_QUYET_DINH,
                        L_SUA_CHUA_BAO_DUONG.CHUNG_TU_NGAY,
                        L_SUA_CHUA_BAO_DUONG.CHUNG_TU_SO,
                        L_SUA_CHUA_BAO_DUONG.CONG_CU_ID,
                        L_SUA_CHUA_BAO_DUONG.GHI_CHU,
                        L_SUA_CHUA_BAO_DUONG.NGAY_QUYET_DINH,
                        L_SUA_CHUA_BAO_DUONG.NGAY_SUA_CHUA_FROM,
                        L_SUA_CHUA_BAO_DUONG.NGAY_SUA_CHUA_TO,
                        L_SUA_CHUA_BAO_DUONG.SO_QUYET_DINH,
                        L_SUA_CHUA_BAO_DUONG.GIA_TRI_SUA_CHUA,
                        L_SUA_CHUA_BAO_DUONG.SO_LUONG_SUA_CHUA,
                        L_SUA_CHUA_BAO_DUONG.NHAP_XUAT_ID,
                        L_SUA_CHUA_BAO_DUONG.KHACH_HANG_ID
                    );
                END LOOP;
            END IF;
            --cho thuê
            L_CHO_THUE_ARRAY := L_THONG_TIN_KHAC.GET_ARRAY('CHO_THUE');
            IF L_CHO_THUE_ARRAY IS NOT NULL AND L_CHO_THUE_ARRAY.GET_SIZE > 0 THEN 
                FOR i IN 0..L_CHO_THUE_ARRAY.GET_SIZE -1 LOOP 
                    L_CHO_THUE_OBJECT := TREAT(L_CHO_THUE_ARRAY.GET(i) AS JSON_OBJECT_T);
                    
                    L_DON_VI_SU_DUNG_MA := L_CHO_THUE_OBJECT.GET_STRING('DON_VI_SU_DUNG_MA');
                    BEGIN
                        SELECT 
                            ID INTO L_DON_VI_SU_SUNG_ID
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
                                        'Message' VALUE 'THONG_TIN_KHAC - CHO_THUE - DON_VI_SU_DUNG_MA NOT FOUND',
                                'IdRecord' VALUE null
                                ) as STRJSON
                        FROM dual;
                		DELETE_CCDC(L_CCDC_ID,1);
                        RETURN;
                    END;
                    L_MA_PHAN_BO := L_CHO_THUE_OBJECT.GET_STRING('MA_PHAN_BO');
                    BEGIN
                        SELECT
                            * INTO L_IMP_PHAN_BO
                        FROM
                            CCDC_XUAT_NHAP
                        WHERE
                            MA_DB = L_MA_PHAN_BO;

                        IF L_IMP_PHAN_BO.DON_VI_BO_PHAN_ID != L_DON_VI_SU_SUNG_ID THEN
                            BEGIN
                                SELECT
                                    * INTO L_IMP_PHAN_BO
                                FROM
                                    CCDC_XUAT_NHAP
                                WHERE
                                    DON_VI_BO_PHAN_ID = L_DON_VI_SU_SUNG_ID
                                    AND IS_XUAT = 0
                                    AND ID IN ( SELECT 
                                                    NHAP_XUAT_ID
                                                FROM 
                                                    CCDC_NHAP_XUAT_CONG_CU
                                                WHERE
                                                    CONG_CU_ID = L_CCDC_ID)
                                ORDER BY ID DESC
                                FETCH FIRST 1 ROW ONLY;
                            EXCEPTION
                                WHEN NO_DATA_FOUND THEN
                                    SELECT
                                        * INTO L_IMP_PHAN_BO
                                    FROM
                                        CCDC_XUAT_NHAP
                                    WHERE
                                        MA_DB = L_MA_PHAN_BO;
                            END;
                        END IF;
                    EXCEPTION
                        WHEN NO_DATA_FOUND THEN
                        OPEN MSS_OUT FOR
                        SELECT JSON_OBJECT(
                                'Code' VALUE '01',
                                        'Message' VALUE 'THONG_TIN_KHAC - CHO_THUE - MA_PHAN_BO NOT FOUND IN ARRAY PHAN_BO',
                                'IdRecord' VALUE null
                                ) as STRJSON
                        FROM dual;
                		DELETE_CCDC(L_CCDC_ID,1);
                        RETURN;
                    END;
                    BEGIN
                        SELECT
                            DON_GIA INTO L_IMP_PHAN_BO_DON_GIA
                        FROM
                            CCDC_NHAP_XUAT_CONG_CU
                        WHERE
                            NHAP_XUAT_ID = L_IMP_PHAN_BO.ID
                            AND CONG_CU_ID = L_CCDC_ID;
                    EXCEPTION
                        WHEN NO_DATA_FOUND THEN
                            L_IMP_PHAN_BO_DON_GIA:=0;
                    END;
                    L_KHACH_HANG_MA := L_CHO_THUE_OBJECT.GET_STRING('MA_DOI_TAC');
                    BEGIN
                        SELECT 
                            ID INTO L_KHACH_HANG_ID
                        FROM
                            DM_DOI_TAC
                        WHERE
                            MA = L_KHACH_HANG_MA
                            AND DON_VI_ID = L_DON_VI_ID
                            AND (LOAI_DOI_TAC_ID = 0 OR LOAI_DOI_TAC_ID IS NULL);
                    EXCEPTION
                        WHEN NO_DATA_FOUND THEN
                        OPEN MSS_OUT FOR
                        SELECT JSON_OBJECT(
                                'Code' VALUE '01',
                                            'Message' VALUE 'THONG_TIN_KHAC - CHO_THUE - MA_DOI_TAC '||L_KHACH_HANG_MA||' NOT FOUND',
                                'IdRecord' VALUE null
                                ) as STRJSON
                        FROM dual;
                		DELETE_CCDC(L_CCDC_ID,1);
                        RETURN;
                    END;
                    
                    L_CHO_THUE.QUYET_DINH_SO := L_CHO_THUE_OBJECT.GET_STRING('SO_QUYET_DINH');
                    L_CHO_THUE.QUYET_DINH_NGAY := L_CHO_THUE_OBJECT.GET_DATE('NGAY_QUYET_DINH');
                    L_CHO_THUE.CAP_QUYET_DINH := L_CHO_THUE_OBJECT.GET_STRING('CAP_QUYET_DINH');
                    L_CHO_THUE.NGAY_CHO_THUE_FROM := L_CHO_THUE_OBJECT.GET_DATE('NGAY_CHO_THUE_TU');
                    L_CHO_THUE.NGAY_CHO_THUE_TO := L_CHO_THUE_OBJECT.GET_DATE('NGAY_CHO_THUE_DEN');
                    L_CHO_THUE.SO_TIEN_THU_DUOC := L_CHO_THUE_OBJECT.GET_STRING('GIA_TRI');
                    L_CHO_THUE.KHACH_HANG_ID := L_KHACH_HANG_ID;
                    L_CHO_THUE.HOP_DONG_SO := L_CHO_THUE_OBJECT.GET_STRING('HOP_DONG_SO');
                    L_CHO_THUE.HOP_DONG_NGAY := L_CHO_THUE_OBJECT.GET_DATE('HOP_DONG_NGAY');
                    L_CHO_THUE.SO_LUONG := L_CHO_THUE_OBJECT.GET_STRING('SO_LUONG');
                    L_CHO_THUE.GHI_CHU := L_CHO_THUE_OBJECT.GET_STRING('GHI_CHU');
                    L_CHO_THUE.CONG_CU_ID := L_CCDC_ID;
                    L_CHO_THUE.NHAP_XUAT_ID := L_IMP_PHAN_BO.ID;
                    INSERT INTO CCDC_CHO_THUE
                    (
                        QUYET_DINH_SO,
                        QUYET_DINH_NGAY,
                        CAP_QUYET_DINH,
                        NGAY_CHO_THUE_TO,
                        NGAY_CHO_THUE_FROM,
                        SO_TIEN_THU_DUOC,
                        KHACH_HANG_ID,
                        HOP_DONG_SO,
                        HOP_DONG_NGAY,
                        SO_LUONG,
                        GHI_CHU,
                        CONG_CU_ID,
                        NHAP_XUAT_ID    
                    )
                    VALUES
                    (
                        L_CHO_THUE.QUYET_DINH_SO,
                        L_CHO_THUE.QUYET_DINH_NGAY,
                        L_CHO_THUE.CAP_QUYET_DINH,
                        L_CHO_THUE.NGAY_CHO_THUE_TO,
                        L_CHO_THUE.NGAY_CHO_THUE_FROM,
                        L_CHO_THUE.SO_TIEN_THU_DUOC,
                        L_CHO_THUE.KHACH_HANG_ID,
                        L_CHO_THUE.HOP_DONG_SO,
                        L_CHO_THUE.HOP_DONG_NGAY,
                        L_CHO_THUE.SO_LUONG,
                        L_CHO_THUE.GHI_CHU,
                        L_CHO_THUE.CONG_CU_ID,
                        L_CHO_THUE.NHAP_XUAT_ID    
                    );
                END LOOP;
            END IF;
            DBMS_OUTPUT.PUT_LINE('END CHO THUE');
        END IF;

        -- luân chuyển
        IF L_LUAN_CHUYEN_ARRAY IS NOT NULL AND L_LUAN_CHUYEN_ARRAY.GET_SIZE > 0 THEN 
            FOR i IN 0..L_LUAN_CHUYEN_ARRAY.GET_SIZE -1 LOOP 
                L_LUAN_CHUYEN_OBJECT := TREAT(L_LUAN_CHUYEN_ARRAY.GET(i) AS JSON_OBJECT_T);
                L_DON_VI_SU_DUNG_MA := L_LUAN_CHUYEN_OBJECT.GET_STRING('DON_VI_SU_DUNG_MA');
                BEGIN
                    SELECT 
                        ID INTO L_DON_VI_SU_SUNG_ID
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
                                    'Message' VALUE 'LUAN_CHUYEN - DON_VI_SU_DUNG_MA NOT FOUND',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                	DELETE_CCDC(L_CCDC_ID,1);
                    RETURN;
                END;
                L_MA_PHAN_BO := L_LUAN_CHUYEN_OBJECT.GET_STRING('MA_PHAN_BO');
                BEGIN
                    SELECT
                        * INTO L_IMP_PHAN_BO
                    FROM
                        CCDC_XUAT_NHAP
                    WHERE
                        MA_DB = L_MA_PHAN_BO;

                    IF L_IMP_PHAN_BO.DON_VI_BO_PHAN_ID != L_DON_VI_SU_SUNG_ID THEN
                        BEGIN
                            SELECT
                                * INTO L_IMP_PHAN_BO
                            FROM
                                CCDC_XUAT_NHAP
                            WHERE
                                DON_VI_BO_PHAN_ID = L_DON_VI_SU_SUNG_ID
                                AND IS_XUAT = 0
                                AND ID IN ( SELECT 
                                                NHAP_XUAT_ID
                                            FROM 
                                                CCDC_NHAP_XUAT_CONG_CU
                                            WHERE
                                                CONG_CU_ID = L_CCDC_ID)
                            ORDER BY ID DESC
                            FETCH FIRST 1 ROW ONLY;
                        EXCEPTION
                            WHEN NO_DATA_FOUND THEN
                                SELECT
                                    * INTO L_IMP_PHAN_BO
                                FROM
                                    CCDC_XUAT_NHAP
                                WHERE
                                    MA_DB = L_MA_PHAN_BO;
                        END;
                    END IF;
                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                    OPEN MSS_OUT FOR
                    SELECT JSON_OBJECT(
                            'Code' VALUE '01',
                                    'Message' VALUE 'LUAN_CHUYEN - MA_PHAN_BO NOT FOUND IN ARRAY PHAN_BO',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                	DELETE_CCDC(L_CCDC_ID,1);
                    RETURN;
                END;
                BEGIN
                    SELECT
                        DON_GIA,ID INTO L_IMP_PHAN_BO_DON_GIA,L_NHAP_KHO_ID
                    FROM
                        CCDC_NHAP_XUAT_CONG_CU
                    WHERE
                        NHAP_XUAT_ID = L_IMP_PHAN_BO.ID
                        AND CONG_CU_ID = L_CCDC_ID;
                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        L_IMP_PHAN_BO_DON_GIA:=0;
                END;
					
                L_LC_NHAP.NGAY_XUAT_NHAP := L_LUAN_CHUYEN_OBJECT.GET_DATE('NGAY_LUAN_CHUYEN');
                INSERT INTO CCDC_XUAT_NHAP
                (
                    FROM_XUAT_NHAP_ID,
                    DON_VI_ID,
                    MA,
                    MA_LIEN_QUAN,
                    IS_XUAT,
                    DON_VI_BO_PHAN_ID,
                    LOAI_XUAT_NHAP_ID,
                    NGAY_TAO,
                    GUID,
                    NGAY_XUAT_NHAP
                )
                VALUES
                (
                    L_IMP_PHAN_BO.ID,
                    L_DON_VI_ID,
                    L_DON_VI_MA,
                    L_IMP_PHAN_BO.MA_LIEN_QUAN,
                    1,
                    L_IMP_PHAN_BO.DON_VI_BO_PHAN_ID,
                    3,
                    SYSDATE,
                    SYS_GUID(),
                    L_LC_NHAP.NGAY_XUAT_NHAP
                )RETURNING ID INTO L_LC_XUAT_ID;
                
                UPDATE CCDC_XUAT_NHAP
                SET MA = L_DON_VI_MA || '-' || L_LC_XUAT_ID
                WHERE ID = L_LC_XUAT_ID;

                L_LC_SO_LUONG := L_LUAN_CHUYEN_OBJECT.GET_NUMBER('SO_LUONG');
                -- mapping xuat
                INSERT INTO CCDC_NHAP_XUAT_CONG_CU
                (
                    CONG_CU_ID,
                    NHAP_XUAT_ID,
                    SO_LUONG,
                    DON_GIA,
                    TRANG_THAI_ID,
                    NHAP_KHO_ID
                )
                VALUES
                (
                    L_CCDC_ID,
                    L_LC_XUAT_ID,
                    L_LC_SO_LUONG,
                    L_IMP_PHAN_BO_DON_GIA,
                    L_IMP_PHAN_BO.TRANG_THAI_ID,
					L_NHAP_KHO_ID
                );
                
                -- nhap
                L_DON_VI_PHAN_BO_MA := L_LUAN_CHUYEN_OBJECT.GET_STRING('DON_VI_NHAN_DIEU_CHUYEN_MA');

                BEGIN
                    SELECT 
                        ID INTO L_DON_VI_PHAN_BO_ID
                    FROM
                        DM_DON_VI_BO_PHAN
                    WHERE
                        MA = L_DON_VI_PHAN_BO_MA
                        AND DON_VI_ID = L_DON_VI_ID;
                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                    L_DON_VI_PHAN_BO_ID := NULL;
                END;
                L_LC_NHAP.FROM_XUAT_NHAP_ID := L_LC_XUAT_ID;
                L_LC_NHAP.DON_VI_ID := L_DON_VI_ID;
                L_LC_NHAP.MA := L_DON_VI_MA;
                L_LC_NHAP.MA_LIEN_QUAN := L_IMP_PHAN_BO.MA_LIEN_QUAN;
                L_LC_NHAP.IS_XUAT := 0;
                L_LC_NHAP.LOAI_XUAT_NHAP_ID := 3;
                L_LC_NHAP.DON_VI_BO_PHAN_ID := L_DON_VI_PHAN_BO_ID;
                L_LC_NHAP.GHI_CHU := L_LUAN_CHUYEN_OBJECT.GET_STRING('GHI_CHU');
                L_LC_NHAP.QUYET_DINH_NGAY := L_LUAN_CHUYEN_OBJECT.GET_DATE('NGAY_QUYET_DINH');
                L_LC_NHAP.QUYET_DINH_SO := L_LUAN_CHUYEN_OBJECT.GET_STRING('SO_QUYET_DINH');
                L_LC_NHAP.NGAY_XUAT_NHAP := L_LUAN_CHUYEN_OBJECT.GET_DATE('NGAY_LUAN_CHUYEN');
                INSERT INTO CCDC_XUAT_NHAP
                (
                    FROM_XUAT_NHAP_ID,
                    DON_VI_ID,
                    MA,
                    MA_LIEN_QUAN,
                    IS_XUAT,
                    LOAI_XUAT_NHAP_ID,
                    DON_VI_BO_PHAN_ID,
                    GHI_CHU,
                    QUYET_DINH_NGAY,
                    QUYET_DINH_SO,
                    NGAY_XUAT_NHAP,
                    NGAY_TAO,
                    GUID
                )
                VALUES 
                (
                    L_LC_NHAP.FROM_XUAT_NHAP_ID,
                    L_LC_NHAP.DON_VI_ID,
                    L_LC_NHAP.MA,
                    L_LC_NHAP.MA_LIEN_QUAN,
                    L_LC_NHAP.IS_XUAT,
                    L_LC_NHAP.LOAI_XUAT_NHAP_ID,
                    L_LC_NHAP.DON_VI_BO_PHAN_ID,
                    L_LC_NHAP.GHI_CHU,
                    L_LC_NHAP.QUYET_DINH_NGAY,
                    L_LC_NHAP.QUYET_DINH_SO,
                    L_LC_NHAP.NGAY_XUAT_NHAP,
                    SYSDATE,
                    SYS_GUID()
                ) RETURNING ID INTO L_LC_NHAP_ID;
                L_LC_NHAP.MA := L_DON_VI_MA || '-' || L_LC_XUAT_ID;
                UPDATE CCDC_XUAT_NHAP
                SET MA = L_DON_VI_MA || '-' || L_LC_XUAT_ID
                WHERE ID = L_LC_NHAP_ID;
                -- mapping nhap
                INSERT INTO CCDC_NHAP_XUAT_CONG_CU
                (
                    SO_LUONG,
                    NHAP_XUAT_ID,
                    CONG_CU_ID,
                    DON_GIA,
                    TRANG_THAI_ID
                )
                VALUES
                (
                    L_LC_SO_LUONG,
                    L_LC_NHAP_ID,
                    L_CCDC_ID,
                    L_IMP_PHAN_BO_DON_GIA,
                    L_IMP_PHAN_BO.TRANG_THAI_ID
                );
            END LOOP;
        END IF;
                    
        -- giảm ccdc
        IF L_GIAM_CCDC_ARRAY IS NOT NULL AND L_GIAM_CCDC_ARRAY.GET_SIZE > 0 THEN 
            FOR i IN 0..L_GIAM_CCDC_ARRAY.GET_SIZE -1 LOOP 
                L_GIAM_CCDC := TREAT(L_GIAM_CCDC_ARRAY.GET(i) AS JSON_OBJECT_T);
                
                SELECT NVL(MAX(ID),1) INTO L_STTXUAT_NHAP
                FROM CCDC_XUAT_NHAP;
                
                L_DON_VI_SU_DUNG_MA := L_GIAM_CCDC.GET_STRING('DON_VI_SU_DUNG_MA');
                BEGIN
                    SELECT 
                        ID INTO L_DON_VI_SU_SUNG_ID
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
                                    'Message' VALUE 'GIAM_CCDC - DON_VI_SU_DUNG_MA NOT FOUND',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                	DELETE_CCDC(L_CCDC_ID,1);
                    RETURN;
                END;
                L_MA_PHAN_BO := L_GIAM_CCDC.GET_STRING('MA_PHAN_BO');
                BEGIN
                    SELECT
                        * INTO L_IMP_PHAN_BO
                    FROM
                        CCDC_XUAT_NHAP
                    WHERE
                        MA_DB = L_MA_PHAN_BO;

                    IF L_IMP_PHAN_BO.DON_VI_BO_PHAN_ID != L_DON_VI_SU_SUNG_ID THEN
                        BEGIN
                            SELECT
                                * INTO L_IMP_PHAN_BO
                            FROM
                                CCDC_XUAT_NHAP
                            WHERE
                                DON_VI_BO_PHAN_ID = L_DON_VI_SU_SUNG_ID
                                AND IS_XUAT = 0
                                AND ID IN ( SELECT 
                                                NHAP_XUAT_ID
                                            FROM 
                                                CCDC_NHAP_XUAT_CONG_CU
                                            WHERE
                                                CONG_CU_ID = L_CCDC_ID)
                            ORDER BY ID DESC
                            FETCH FIRST 1 ROW ONLY;
                        EXCEPTION
                            WHEN NO_DATA_FOUND THEN
                                SELECT
                                    * INTO L_IMP_PHAN_BO
                                FROM
                                    CCDC_XUAT_NHAP
                                WHERE
                                    MA_DB = L_MA_PHAN_BO;
                        END;
                    END IF;
                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                    OPEN MSS_OUT FOR
                    SELECT JSON_OBJECT(
                            'Code' VALUE '01',
                                    'Message' VALUE 'GIAM_CCDC - MA_PHAN_BO NOT FOUND IN ARRAY PHAN_BO',
                            'IdRecord' VALUE null
                            ) as STRJSON
                    FROM dual;
                	DELETE_CCDC(L_CCDC_ID,1);
                    RETURN;
                END;
                BEGIN
                    SELECT
                        DON_GIA,ID INTO L_IMP_PHAN_BO_DON_GIA,L_NHAP_KHO_ID
                    FROM
                        CCDC_NHAP_XUAT_CONG_CU
                    WHERE
                        NHAP_XUAT_ID = L_IMP_PHAN_BO.ID
                        AND CONG_CU_ID = L_CCDC_ID;
                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        L_IMP_PHAN_BO_DON_GIA:=0;
                END;

                L_GIAM_XUAT_NHAP.DON_VI_BO_PHAN_ID := L_IMP_PHAN_BO.DON_VI_BO_PHAN_ID;
                L_GIAM_XUAT_NHAP.DON_VI_ID := L_IMP_PHAN_BO.DON_VI_ID;
                L_GIAM_XUAT_NHAP.FROM_XUAT_NHAP_ID := L_IMP_PHAN_BO.ID;
                L_GIAM_XUAT_NHAP.GHI_CHU := L_GIAM_CCDC.GET_STRING('GHI_CHU');
                L_GIAM_XUAT_NHAP.IS_XUAT := 1;
                L_GIAM_XUAT_NHAP.MA_LIEN_QUAN := L_IMP_PHAN_BO.MA_LIEN_QUAN;
                L_GIAM_XUAT_NHAP.QUYET_DINH_NGAY := L_GIAM_CCDC.GET_DATE('NGAY_QUYET_DINH');
                L_GIAM_XUAT_NHAP.QUYET_DINH_SO := L_GIAM_CCDC.GET_STRING('SO_QUYET_DINH');
                --                L_GIAM_XUAT_NHAP.MA := (
                SELECT 
                    MA || '-' || L_STTXUAT_NHAP 
                    INTO L_GIAM_XUAT_NHAP.MA
                FROM
                    DM_DON_VI
                WHERE
                    ID = L_IMP_PHAN_BO.DON_VI_ID
                    AND ROWNUM = 1;
                L_GIAM_XUAT_NHAP.NGAY_XUAT_NHAP := L_GIAM_CCDC.GET_DATE('NGAY_DIEU_CHUYEN');
                L_GIAM_XUAT_NHAP.LOAI_XUAT_NHAP_ID := L_GIAM_CCDC.GET_NUMBER('LY_DO_GIAM_ID');
                INSERT INTO CCDC_XUAT_NHAP
                (
                    DON_VI_BO_PHAN_ID,
                    DON_VI_ID,
                    FROM_XUAT_NHAP_ID,
                    GHI_CHU,
                    IS_XUAT,
                    MA_LIEN_QUAN,
                    QUYET_DINH_NGAY,
                    QUYET_DINH_SO,
                    MA,
                    NGAY_XUAT_NHAP,
                    LOAI_XUAT_NHAP_ID,
                    NGAY_TAO,
                    GUID
                )
                VALUES 
                (
                    L_GIAM_XUAT_NHAP.DON_VI_BO_PHAN_ID,
                    L_GIAM_XUAT_NHAP.DON_VI_ID,
                    L_GIAM_XUAT_NHAP.FROM_XUAT_NHAP_ID,
                    L_GIAM_XUAT_NHAP.GHI_CHU,
                    L_GIAM_XUAT_NHAP.IS_XUAT,
                    L_GIAM_XUAT_NHAP.MA_LIEN_QUAN,
                    L_GIAM_XUAT_NHAP.QUYET_DINH_NGAY,
                    L_GIAM_XUAT_NHAP.QUYET_DINH_SO,
                    L_GIAM_XUAT_NHAP.MA,
                    L_GIAM_XUAT_NHAP.NGAY_XUAT_NHAP,
                    L_GIAM_XUAT_NHAP.LOAI_XUAT_NHAP_ID,
                    SYSDATE,
                    SYS_GUID()
                )RETURNING ID INTO L_LC_XUAT_ID;

                -- mapping xuất
                L_GIAM_NHAP_XUAT_CONG_CU.CHUNG_TU_NGAY := L_GIAM_CCDC.GET_DATE('CHUNG_TU_NGAY');
                L_GIAM_NHAP_XUAT_CONG_CU.CHUNG_TU_SO := L_GIAM_CCDC.GET_STRING('CHUNG_TU_SO');
                L_GIAM_NHAP_XUAT_CONG_CU.CONG_CU_ID := L_CCDC_ID;
                L_GIAM_NHAP_XUAT_CONG_CU.DON_GIA := L_IMP_PHAN_BO_DON_GIA;
                L_GIAM_NHAP_XUAT_CONG_CU.NHAP_XUAT_ID := L_LC_XUAT_ID;
                DBMS_OUTPUT.PUT_LINE('mapping xuất : '||L_LC_XUAT_ID);
                L_GIAM_NHAP_XUAT_CONG_CU.SO_LUONG := NVL(L_GIAM_CCDC.GET_NUMBER('SO_LUONG'),0);
                L_GIAM_NHAP_XUAT_CONG_CU.TRANG_THAI_ID := L_IMP_PHAN_BO.TRANG_THAI_ID;
                L_GIAM_NHAP_XUAT_CONG_CU.NHAP_KHO_ID := L_NHAP_KHO_ID;
                L_GIAM_NHAP_XUAT_CONG_CU.THANH_TIEN := L_IMP_PHAN_BO_DON_GIA * NVL(L_GIAM_CCDC.GET_NUMBER('SO_LUONG'),0);
                INSERT INTO CCDC_NHAP_XUAT_CONG_CU
                (
                    CHUNG_TU_NGAY,
                    CHUNG_TU_SO,
                    CONG_CU_ID,
                    DON_GIA,
                    NHAP_XUAT_ID,
                    SO_LUONG,
                    TRANG_THAI_ID,
                    NHAP_KHO_ID,
                    THANH_TIEN
                )
                VALUES 
                (
                    L_GIAM_NHAP_XUAT_CONG_CU.CHUNG_TU_NGAY,
                    L_GIAM_NHAP_XUAT_CONG_CU.CHUNG_TU_SO,
                    L_GIAM_NHAP_XUAT_CONG_CU.CONG_CU_ID,
                    L_GIAM_NHAP_XUAT_CONG_CU.DON_GIA,
                    L_GIAM_NHAP_XUAT_CONG_CU.NHAP_XUAT_ID,
                    L_GIAM_NHAP_XUAT_CONG_CU.SO_LUONG,
                    L_GIAM_NHAP_XUAT_CONG_CU.TRANG_THAI_ID,
                    L_GIAM_NHAP_XUAT_CONG_CU.NHAP_KHO_ID,
                    L_GIAM_NHAP_XUAT_CONG_CU.THANH_TIEN
                );

                IF L_GIAM_CCDC.GET_NUMBER('LY_DO_GIAM_ID') = 4 THEN
                    --thêm mới nhóm và công cụ cho đơn vị ngoài
                    L_GIAM_DON_VI_MA := L_GIAM_CCDC.GET_STRING('DON_VI_TIEP_NHAN');
                    BEGIN
                        SELECT 
                            ID INTO L_GIAM_DON_VI_ID
                        FROM
                            DM_DON_VI
                        WHERE
                            MA = L_GIAM_DON_VI_MA;
                    EXCEPTION
                        WHEN NO_DATA_FOUND THEN
                        OPEN MSS_OUT FOR
                        SELECT JSON_OBJECT(
                                'Code' VALUE '01',
                                            'Message' VALUE 'GIAM_CCDC - DON_VI_TIEP_NHAN '||L_GIAM_DON_VI_MA||' NOT FOUND',
                                'IdRecord' VALUE null
                                ) as STRJSON
                        FROM dual;
                		DELETE_CCDC(L_CCDC_ID,1);
                        RETURN;
                    END;
                        
                    BEGIN
                        SELECT
                            ID INTO L_GIAM_NHOM_CCDC_ID
                        FROM
                            DM_NHOM_CONG_CU
                        WHERE
                            MA = L_NHOM_CCDC_MA
                            AND DON_VI_ID = L_GIAM_DON_VI_ID;
                    EXCEPTION
                        WHEN NO_DATA_FOUND THEN
                        L_GIAM_NHOM_CCDC.DON_VI_TINH_ID := L_NHOM_CCDC.DON_VI_TINH_ID;
                        L_GIAM_NHOM_CCDC.TEN := L_NHOM_CCDC.TEN;
                        L_GIAM_NHOM_CCDC.THOI_HAN_PHAN_BO := L_NHOM_CCDC.THOI_HAN_PHAN_BO;
                        L_GIAM_NHOM_CCDC.DON_VI_ID := L_GIAM_DON_VI_ID;
                        
                        INSERT INTO DM_NHOM_CONG_CU
                        (
                            DON_VI_TINH_ID,
                            TEN,
                            THOI_HAN_PHAN_BO,
                            DON_VI_ID
                        )VALUES
                        (
                            L_GIAM_NHOM_CCDC.DON_VI_TINH_ID,
                            L_GIAM_NHOM_CCDC.TEN,
                            L_GIAM_NHOM_CCDC.THOI_HAN_PHAN_BO,
                            L_GIAM_NHOM_CCDC.DON_VI_ID
                        )RETURNING ID INTO L_GIAM_NHOM_CCDC_ID;

                        UPDATE DM_NHOM_CONG_CU
                        SET TREE_NODE = LPAD(L_GIAM_NHOM_CCDC_ID, 8, '0')
                        WHERE ID = L_GIAM_NHOM_CCDC_ID;
                    END;

                    
                    L_CCDC2.TEN := L_CCDC.TEN;
                    L_CCDC2.DON_VI_ID := L_GIAM_DON_VI_ID;
                    L_CCDC2.NHOM_CONG_CU_ID := L_GIAM_NHOM_CCDC_ID;
                    L_CCDC2.MA := L_GIAM_DON_VI_MA || '-' || L_GIAM_NHOM_CCDC_ID;
                    
                    INSERT INTO CCDC_CONG_CU
                    (
                        TEN,
                        DON_VI_ID,
                        NHOM_CONG_CU_ID,
                        MA,
                        NGAY_TAO,
                        GUID
                    )
                    VALUES
                    (
                        L_CCDC2.TEN,
                        L_CCDC2.DON_VI_ID,
                        L_CCDC2.NHOM_CONG_CU_ID,
                        L_CCDC2.MA,
                        SYSDATE,
                        SYS_GUID()
                    )RETURNING ID INTO L_CCDC2_ID;
                    L_CCDC2.MA := L_GIAM_DON_VI_MA || '-' || L_GIAM_NHOM_CCDC_ID || '-' || L_CCDC2_ID;
                    UPDATE CCDC_CONG_CU
                    SET MA = L_GIAM_DON_VI_MA || '-' || L_GIAM_NHOM_CCDC_ID || '-' || L_CCDC2_ID
                    WHERE ID = L_CCDC2_ID;
                    
                    -- nhập
                    L_GIAM_DC_NHAP.DON_VI_ID := L_GIAM_DON_VI_ID;
                    L_GIAM_DC_NHAP.FROM_XUAT_NHAP_ID := L_LC_XUAT_ID;
                    L_GIAM_DC_NHAP.GHI_CHU := L_GIAM_CCDC.GET_STRING('GHI_CHU');
                    L_GIAM_DC_NHAP.IS_XUAT := 0;
                    L_GIAM_DC_NHAP.LOAI_XUAT_NHAP_ID := 4;
                    L_GIAM_DC_NHAP.MA_LIEN_QUAN := L_IMP_PHAN_BO.MA_LIEN_QUAN;
                    L_GIAM_DC_NHAP.QUYET_DINH_NGAY := L_GIAM_CCDC.GET_DATE('NGAY_QUYET_DINH');
                    L_GIAM_DC_NHAP.QUYET_DINH_SO := L_GIAM_CCDC.GET_STRING('SO_QUYET_DINH');
                    L_GIAM_DC_NHAP.MA := L_GIAM_DON_VI_MA;
                    L_GIAM_DC_NHAP.NGAY_XUAT_NHAP := L_GIAM_CCDC.GET_DATE('NGAY_DIEU_CHUYEN');
                    INSERT INTO CCDC_XUAT_NHAP
                    (
                        DON_VI_ID,
                        FROM_XUAT_NHAP_ID,
                        GHI_CHU,
                        IS_XUAT,
                        LOAI_XUAT_NHAP_ID,
                        MA_LIEN_QUAN,
                        QUYET_DINH_NGAY,
                        QUYET_DINH_SO,
                        MA,
                        NGAY_XUAT_NHAP,
                        NGAY_TAO,
                        GUID
                    )VALUES
                    (
                        L_GIAM_DC_NHAP.DON_VI_ID,
                        L_GIAM_DC_NHAP.FROM_XUAT_NHAP_ID,
                        L_GIAM_DC_NHAP.GHI_CHU,
                        L_GIAM_DC_NHAP.IS_XUAT,
                        L_GIAM_DC_NHAP.LOAI_XUAT_NHAP_ID,
                        L_GIAM_DC_NHAP.MA_LIEN_QUAN,
                        L_GIAM_DC_NHAP.QUYET_DINH_NGAY,
                        L_GIAM_DC_NHAP.QUYET_DINH_SO,
                        L_GIAM_DC_NHAP.MA,
                        L_GIAM_DC_NHAP.NGAY_XUAT_NHAP,
                        SYSDATE,
                        SYS_GUID()
                    )RETURNING ID INTO L_GIAM_DC_NHAP_ID;
                    L_GIAM_DC_NHAP.MA := L_DON_VI_MA || '-' || L_GIAM_DC_NHAP_ID;
                    UPDATE CCDC_XUAT_NHAP
                    SET MA = L_DON_VI_MA || '-' || L_GIAM_DC_NHAP_ID
                    WHERE ID = L_GIAM_DC_NHAP_ID;

                    -- mapping nhập
                    L_GIAM_DC_MAP_NHAP.CHUNG_TU_NGAY := L_GIAM_CCDC.GET_DATE('CHUNG_TU_NGAY');
                    L_GIAM_DC_MAP_NHAP.CHUNG_TU_SO := L_GIAM_CCDC.GET_STRING('CHUNG_TU_SO');
                    L_GIAM_DC_MAP_NHAP.CONG_CU_ID := L_CCDC2_ID;
                    L_GIAM_DC_MAP_NHAP.DON_GIA := L_IMP_PHAN_BO_DON_GIA;
                    L_GIAM_DC_MAP_NHAP.NHAP_XUAT_ID := L_GIAM_DC_NHAP_ID;
                    L_GIAM_DC_MAP_NHAP.SO_LUONG := NVL(L_GIAM_CCDC.GET_NUMBER('SO_LUONG'),0);
                    L_GIAM_DC_MAP_NHAP.TRANG_THAI_ID := L_IMP_PHAN_BO.TRANG_THAI_ID;
                    L_GIAM_DC_MAP_NHAP.THANH_TIEN := L_IMP_PHAN_BO_DON_GIA * NVL(L_GIAM_CCDC.GET_NUMBER('SO_LUONG'),0);

                    INSERT INTO CCDC_NHAP_XUAT_CONG_CU
                    (
                        CHUNG_TU_NGAY,
                        CHUNG_TU_SO,
                        CONG_CU_ID,
                        DON_GIA,
                        NHAP_XUAT_ID,
                        SO_LUONG,
                        TRANG_THAI_ID,
                        THANH_TIEN
                    )
                    VALUES
                    (
                        L_GIAM_DC_MAP_NHAP.CHUNG_TU_NGAY,
                        L_GIAM_DC_MAP_NHAP.CHUNG_TU_SO,
                        L_GIAM_DC_MAP_NHAP.CONG_CU_ID,
                        L_GIAM_DC_MAP_NHAP.DON_GIA,
                        L_GIAM_DC_MAP_NHAP.NHAP_XUAT_ID,
                        L_GIAM_DC_MAP_NHAP.SO_LUONG,
                        L_GIAM_DC_MAP_NHAP.TRANG_THAI_ID,
                        L_GIAM_DC_MAP_NHAP.THANH_TIEN
                    );
                END IF;
                --giấy báo hỏng mất
                IF L_GIAM_CCDC.GET_NUMBER('LY_DO_GIAM_ID') = 6 THEN
                    L_GIAM_HONG_MAT.DON_VI_ID := L_DON_VI_ID;
                    L_GIAM_HONG_MAT.CONG_CU_ID := L_CCDC_ID;
                    L_GIAM_HONG_MAT.NHAP_XUAT_ID := L_LC_XUAT_ID;
                    L_GIAM_HONG_MAT.DON_VI_BO_PHAN_ID := L_IMP_PHAN_BO.DON_VI_BO_PHAN_ID;
                    L_GIAM_HONG_MAT.NGAY_LAP := NVL(L_GIAM_CCDC.GET_DATE('NGAY_DIEU_CHUYEN'),SYSDATE);
                    L_GIAM_HONG_MAT.SO_LUONG := NVL(L_GIAM_CCDC.GET_NUMBER('SO_LUONG'),0);
                    L_GIAM_HONG_MAT.NGAY_TAO := SYSDATE;
                    
                    INSERT INTO CCDC_GIAM_HONGMAT
                    (
                        DON_VI_ID,
                        CONG_CU_ID,
                        NHAP_XUAT_ID,
                        DON_VI_BO_PHAN_ID,
                        NGAY_LAP,
                        SO_LUONG,
                        NGAY_TAO
                    )
                    VALUES
                    (
                        L_GIAM_HONG_MAT.DON_VI_ID,
                        L_GIAM_HONG_MAT.CONG_CU_ID,
                        L_GIAM_HONG_MAT.NHAP_XUAT_ID,
                        L_GIAM_HONG_MAT.DON_VI_BO_PHAN_ID,
                        L_GIAM_HONG_MAT.NGAY_LAP,
                        L_GIAM_HONG_MAT.SO_LUONG,
                        L_GIAM_HONG_MAT.NGAY_TAO
                    );

                END IF;
            END LOOP;
        END IF;
                    
    END IF;


    OPEN MSS_OUT FOR
    SELECT JSON_OBJECT(
            'Code' VALUE '00',
            'Message' VALUE 'Done',
            'IdRecord' VALUE null,
            'ObjectInfo' VALUE L_CCDC_ID
        ) as STRJSON
    FROM dual;
    DBMS_OUTPUT.PUT_LINE(L_CCDC_ID||','||L_DON_VI_ID||','||L_NHAP_KHO_ID);
    RETURN;
END INSERT_CCDC;