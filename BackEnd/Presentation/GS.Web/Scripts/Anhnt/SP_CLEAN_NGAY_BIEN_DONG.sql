create or replace PROCEDURE SP_CLEAN_NGAY_BIEN_DONG AS 
    L_EXST              NUMBER(1);
    L_COUNT_EXST        NUMBER;
    L_COUNT             NUMBER;
    L_NGAY_BIEN_DONG    DATE;
BEGIN

    SELECT 
        COUNT(BD.ID) INTO L_COUNT
    FROM 
        BD_BIEN_DONG BD
    WHERE 
        EXISTS(
                SELECT 
                    'X' 
                FROM 
                    BD_BIEN_DONG BD1 
                WHERE 
                    BD1.ID != BD.ID 
                    AND BD1.NGAY_BIEN_DONG = BD.NGAY_BIEN_DONG 
                    AND BD1.TAI_SAN_ID = BD.TAI_SAN_ID
                    AND BD1.TRANG_THAI_ID <> 6
                    )
        AND BD.TRANG_THAI_ID <> 6
        ORDER BY BD.NGAY_BIEN_DONG;
    DBMS_OUTPUT.PUT_LINE(L_COUNT ||' BIEN DONG TRUNG NGAY BIEN DONG');
    IF L_COUNT > 0 THEN
        --Ngày biến động = 31/12
        --lý do tăng mới
        FOR R_TABLE IN (
                            SELECT 
                                BD.ID, BD.TAI_SAN_ID, BD.NGAY_BIEN_DONG 
                            FROM 
                                BD_BIEN_DONG BD
                            WHERE 
                                EXISTS(
                                        SELECT 
                                            'X' 
                                        FROM 
                                            BD_BIEN_DONG BD1 
                                        WHERE 
                                            BD1.ID != BD.ID 
                                            AND BD1.NGAY_BIEN_DONG = BD.NGAY_BIEN_DONG 
                                            AND BD1.TAI_SAN_ID = BD.TAI_SAN_ID
                                            AND BD1.TRANG_THAI_ID <> 6
                                            )
                                AND TO_CHAR(BD.NGAY_BIEN_DONG, 'MMDD') = '3112' 
                                AND BD.TRANG_THAI_ID <> 6
                                AND BD.LY_DO_BIEN_DONG_ID IN (
                                                                    SELECT
                                                                        ID
                                                                    FROM 
                                                                        DM_LY_DO_BIEN_DONG
                                                                    WHERE
                                                                        LOAI_LY_DO_ID IN (1,12))
                                ORDER BY BD.NGAY_BIEN_DONG
        ) LOOP
--        DBMS_OUTPUT.PUT_LINE('1: R_TABLE.ID = ' || R_TABLE.ID);
            SELECT 
                COUNT(*) INTO L_COUNT_EXST
            FROM 
                BD_BIEN_DONG BD1 
            WHERE 
                BD1.ID != R_TABLE.ID 
                AND BD1.NGAY_BIEN_DONG = R_TABLE.NGAY_BIEN_DONG 
                AND BD1.TAI_SAN_ID = R_TABLE.TAI_SAN_ID
                AND BD1.TRANG_THAI_ID <> 6;
            IF L_COUNT_EXST > 0 THEN
                UPDATE 
                    BD_BIEN_DONG
                SET 
                    NGAY_BIEN_DONG = NGAY_BIEN_DONG - L_COUNT_EXST
                WHERE 
                    ID = R_TABLE.ID;
                COMMIT;
            END IF;
        END LOOP;
        
        --Ngày biến động = 01/01
        --lý do giảm toàn bộ
        FOR R_TABLE IN (
                            SELECT 
                                BD.ID, BD.TAI_SAN_ID, BD.NGAY_BIEN_DONG 
                            FROM 
                                BD_BIEN_DONG BD
                            WHERE 
                                EXISTS(
                                        SELECT 
                                            'X' 
                                        FROM 
                                            BD_BIEN_DONG BD1 
                                        WHERE 
                                            BD1.ID != BD.ID 
                                            AND BD1.NGAY_BIEN_DONG = BD.NGAY_BIEN_DONG 
                                            AND BD1.TAI_SAN_ID = BD.TAI_SAN_ID
                                            AND BD1.TRANG_THAI_ID <> 6
                                            )
                                AND TO_CHAR(BD.NGAY_BIEN_DONG, 'MMDD') = '0101' 
                                -- AND TO_CHAR(BD.NGAY_BIEN_DONG, 'DD') != '01' 
                                AND BD.LY_DO_BIEN_DONG_ID IN (
                                                                    SELECT
                                                                        ID
                                                                    FROM 
                                                                        DM_LY_DO_BIEN_DONG
                                                                    WHERE
                                                                        LOAI_LY_DO_ID = 5)
                                AND BD.TRANG_THAI_ID <> 6
                                ORDER BY BD.NGAY_BIEN_DONG
        ) LOOP
    
--        DBMS_OUTPUT.PUT_LINE('2: R_TABLE.ID = ' || R_TABLE.ID);
            SELECT 
                COUNT(*) INTO L_COUNT_EXST
            FROM 
                BD_BIEN_DONG BD1 
            WHERE 
                BD1.ID != R_TABLE.ID 
                AND BD1.NGAY_BIEN_DONG = R_TABLE.NGAY_BIEN_DONG 
                AND BD1.TAI_SAN_ID = R_TABLE.TAI_SAN_ID
                AND BD1.TRANG_THAI_ID <> 6;
    
            IF L_COUNT_EXST > 0 THEN
                UPDATE 
                    BD_BIEN_DONG
                SET 
                    NGAY_BIEN_DONG = NGAY_BIEN_DONG + L_COUNT_EXST
                WHERE 
                    ID = R_TABLE.ID;
                COMMIT;
            END IF;
        END LOOP;
        
         
        --Ngày biến động = 31/12
        --lý do khác giảm toàn bộ
        FOR R_TABLE IN (
                        SELECT 
                            BD.ID, BD.TAI_SAN_ID, BD.NGAY_BIEN_DONG FROM BD_BIEN_DONG BD
                        WHERE 
                            EXISTS(	
                                    SELECT 
                                        'X' 
                                    FROM 
                                        BD_BIEN_DONG BD1 
                                    WHERE 
                                        BD1.ID != BD.ID 
                                        AND BD1.NGAY_BIEN_DONG = BD.NGAY_BIEN_DONG 
                                        AND BD1.TAI_SAN_ID = BD.TAI_SAN_ID
                                        AND BD1.TRANG_THAI_ID <> 6
                                        )
                            AND TO_CHAR(BD.NGAY_BIEN_DONG, 'MMDD') = '3112' 
                            -- AND TO_CHAR(BD.NGAY_BIEN_DONG, 'DD') = '01' 
                            AND BD.LOAI_BIEN_DONG_ID NOT IN (
                                                            SELECT
                                                                ID
                                                            FROM 
                                                                DM_LY_DO_BIEN_DONG
                                                            WHERE
                                                                LOAI_LY_DO_ID = 5)
                            AND BD.TRANG_THAI_ID <> 6
                            ORDER BY BD.NGAY_BIEN_DONG
        ) LOOP
    
--        DBMS_OUTPUT.PUT_LINE('3: R_TABLE.ID = ' || R_TABLE.ID);
            SELECT 
                CASE 
                    WHEN 
                        EXISTS(
                                SELECT 
                                    'X' 
                                FROM 
                                    BD_BIEN_DONG BD1 
                                WHERE 
                                    BD1.ID != R_TABLE.ID 
                                    AND BD1.NGAY_BIEN_DONG = R_TABLE.NGAY_BIEN_DONG 
                                    AND BD1.TAI_SAN_ID = R_TABLE.TAI_SAN_ID
                                    )
                        THEN 1 
                    ELSE 0 
                END INTO L_EXST 
            FROM DUAL;
    
            IF L_EXST = 1 THEN
                UPDATE 
                    BD_BIEN_DONG
                SET 
                    NGAY_BIEN_DONG = NGAY_BIEN_DONG - 1
                WHERE 
                    ID = R_TABLE.ID;
                COMMIT;
            END IF;
        END LOOP;
        
        --Ngày biến động = 01/01
        --lý do khác tăng mới, giảm toàn bộ
        FOR R_TABLE IN (
                            SELECT 
                                BD.ID, BD.TAI_SAN_ID, BD.NGAY_BIEN_DONG 
                            FROM 
                                BD_BIEN_DONG BD
                            WHERE 
                                EXISTS(
                                        SELECT 
                                            'X' 
                                        FROM 
                                            BD_BIEN_DONG BD1 
                                        WHERE 
                                            BD1.ID != BD.ID 
                                            AND BD1.NGAY_BIEN_DONG = BD.NGAY_BIEN_DONG 
                                            AND BD1.TAI_SAN_ID = BD.TAI_SAN_ID
                                            AND BD1.TRANG_THAI_ID <> 6
                                            )
                                AND TO_CHAR(BD.NGAY_BIEN_DONG, 'MMDD') = '0101' 
                                -- AND TO_CHAR(BD.NGAY_BIEN_DONG, 'DD') != '01' 
                                AND BD.LY_DO_BIEN_DONG_ID NOT IN (
                                                                    SELECT
                                                                        ID
                                                                    FROM 
                                                                        DM_LY_DO_BIEN_DONG
                                                                    WHERE
                                                                        LOAI_LY_DO_ID IN (1,12,5))
                                AND BD.TRANG_THAI_ID <> 6
                                ORDER BY BD.NGAY_BIEN_DONG
        ) LOOP
    
--        DBMS_OUTPUT.PUT_LINE('4: R_TABLE.ID = ' || R_TABLE.ID);
            SELECT 
                CASE 
                    WHEN 
                        EXISTS(
                                SELECT 
                                    'X' 
                                FROM 
                                    BD_BIEN_DONG BD1 
                                WHERE 
                                    BD1.ID != R_TABLE.ID 
                                    AND BD1.NGAY_BIEN_DONG = R_TABLE.NGAY_BIEN_DONG 
                                    AND BD1.TAI_SAN_ID = R_TABLE.TAI_SAN_ID
                                    AND BD1.TRANG_THAI_ID <> 6
                                    )
                        THEN 1 
                    ELSE 0 
                END INTO L_EXST 
            FROM DUAL;
    
            IF L_EXST = 1 THEN
--            DBMS_OUTPUT.PUT_LINE('4: L_EXST = ' || L_EXST);
                UPDATE 
                    BD_BIEN_DONG
                SET 
                    NGAY_BIEN_DONG = NGAY_BIEN_DONG + 1
                WHERE 
                    ID = R_TABLE.ID;
                
                COMMIT;
            END IF;
        END LOOP;
        
        
        --Ngày biến động != 01/01
        --lý do khác giảm toàn bộ
        FOR R_TABLE IN (
                            SELECT 
                                BD.ID, BD.TAI_SAN_ID, BD.NGAY_BIEN_DONG 
                            FROM 
                                BD_BIEN_DONG BD
                            WHERE 
                                EXISTS(
                                        SELECT 
                                            'X' 
                                        FROM 
                                            BD_BIEN_DONG BD1 
                                        WHERE 
                                            BD1.ID != BD.ID 
                                            AND BD1.NGAY_BIEN_DONG = BD.NGAY_BIEN_DONG 
                                            AND BD1.TAI_SAN_ID = BD.TAI_SAN_ID
                                            AND BD1.TRANG_THAI_ID <> 6
                                            )
                                AND TO_CHAR(BD.NGAY_BIEN_DONG, 'MMDD') != '0101' 
                                -- AND TO_CHAR(BD.NGAY_BIEN_DONG, 'DD') != '01' 
                                AND BD.LY_DO_BIEN_DONG_ID NOT IN (
                                                                    SELECT
                                                                        ID
                                                                    FROM 
                                                                        DM_LY_DO_BIEN_DONG
                                                                    WHERE
                                                                        LOAI_LY_DO_ID = 5)
                                AND BD.TRANG_THAI_ID <> 6
                                ORDER BY BD.NGAY_BIEN_DONG
        ) LOOP
    
--        DBMS_OUTPUT.PUT_LINE('5: R_TABLE.ID = ' || R_TABLE.ID);
            SELECT 
                COUNT(*) INTO L_COUNT_EXST
            FROM 
                BD_BIEN_DONG BD1 
            WHERE 
                BD1.ID != R_TABLE.ID 
                AND BD1.NGAY_BIEN_DONG = R_TABLE.NGAY_BIEN_DONG 
                AND BD1.TAI_SAN_ID = R_TABLE.TAI_SAN_ID
                AND BD1.TRANG_THAI_ID <> 6;
    
            IF L_COUNT_EXST >= 1 THEN
                BEGIN
                    SELECT BD2.NGAY_BIEN_DONG INTO L_NGAY_BIEN_DONG
                    FROM BD_BIEN_DONG BD2
                    WHERE BD2.TAI_SAN_ID = R_TABLE.TAI_SAN_ID
                        AND BD2.ID != R_TABLE.ID
                        AND BD2.LY_DO_BIEN_DONG_ID IN (
                                                        SELECT
                                                            ID
                                                        FROM 
                                                            DM_LY_DO_BIEN_DONG
                                                        WHERE
                                                            LOAI_LY_DO_ID IN (1,12))
                        AND BD2.TRANG_THAI_ID <> 6
                    ORDER BY BD2.LY_DO_BIEN_DONG_ID
                    FETCH FIRST 1 ROW ONLY;
                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                    L_NGAY_BIEN_DONG:=NULL;
                END;
                                                            
                UPDATE 
                    BD_BIEN_DONG
                SET 
                    NGAY_BIEN_DONG = (CASE WHEN L_NGAY_BIEN_DONG = NGAY_BIEN_DONG - 1 THEN NGAY_BIEN_DONG + 1
                                            ELSE NGAY_BIEN_DONG - 1 END)
                WHERE 
                    ID = R_TABLE.ID;
                COMMIT;
            END IF;
        END LOOP;
    
    
        --Ngày biến động != 31/12
        --lý do giảm toàn bộ
        FOR R_TABLE IN (
                        SELECT 
                            BD.ID, BD.TAI_SAN_ID, BD.NGAY_BIEN_DONG FROM BD_BIEN_DONG BD
                        WHERE 
                            EXISTS(	
                                    SELECT 
                                        'X' 
                                    FROM 
                                        BD_BIEN_DONG BD1 
                                    WHERE 
                                        BD1.ID != BD.ID 
                                        AND BD1.NGAY_BIEN_DONG = BD.NGAY_BIEN_DONG 
                                        AND BD1.TAI_SAN_ID = BD.TAI_SAN_ID
                                        AND BD1.TRANG_THAI_ID <> 6
                                        )
                            AND TO_CHAR(BD.NGAY_BIEN_DONG, 'MMDD') != '3112' 
                            -- AND TO_CHAR(BD.NGAY_BIEN_DONG, 'DD') = '01' 
                            AND BD.LOAI_BIEN_DONG_ID IN (
                                                            SELECT
                                                                ID
                                                            FROM 
                                                                DM_LY_DO_BIEN_DONG
                                                            WHERE
                                                                LOAI_LY_DO_ID = 5)
                            AND BD.TRANG_THAI_ID <> 6
                            ORDER BY BD.NGAY_BIEN_DONG
        ) LOOP
    
--        DBMS_OUTPUT.PUT_LINE('7: R_TABLE.ID = ' || R_TABLE.ID);
            SELECT 
                CASE 
                    WHEN 
                        EXISTS(
                                SELECT 
                                    'X' 
                                FROM 
                                    BD_BIEN_DONG BD1 
                                WHERE 
                                    BD1.ID != R_TABLE.ID 
                                    AND BD1.NGAY_BIEN_DONG = R_TABLE.NGAY_BIEN_DONG 
                                    AND BD1.TAI_SAN_ID = R_TABLE.TAI_SAN_ID
                                    AND BD1.TRANG_THAI_ID <> 6
                                    )
                        THEN 1 
                    ELSE 0 
                END INTO L_EXST 
            FROM DUAL;
    
            IF L_EXST = 1 THEN
                UPDATE 
                    BD_BIEN_DONG
                SET 
                    NGAY_BIEN_DONG = NGAY_BIEN_DONG + 1
                WHERE 
                    ID = R_TABLE.ID;
                COMMIT;
            END IF;
        END LOOP;
    END IF;
   
END SP_CLEAN_NGAY_BIEN_DONG;