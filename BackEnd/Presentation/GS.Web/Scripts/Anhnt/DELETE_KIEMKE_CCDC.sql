CREATE OR REPLACE PROCEDURE DELETE_KIEMKE_CCDC 
(
  P_KIEMKE_ID IN NUMBER 
) AS 
BEGIN
    DELETE 
        ccdc_kiem_ke_hoi_dong
    WHERE
        kiem_ke_id = P_KIEMKE_ID;
        
    DELETE 
        ccdc_kiem_ke_cong_cu
    WHERE
        kiem_ke_id = P_KIEMKE_ID;
        
    DELETE 
        ccdc_kiem_ke
    WHERE
        ID = P_KIEMKE_ID;
    
    COMMIT;
  
END DELETE_KIEMKE_CCDC;