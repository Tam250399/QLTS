create or replace FUNCTION DEL_TAI_SAN_DONG_BO (
p_MA_DB VARCHAR2
)RETURN number AS 

BEGIN
  FOR r_product IN (
        SELECT * FROM TS_TAI_SAN where MA_DB = p_MA_DB
    ) 
  LOOP
    DELETE ts_tai_san_nguon_von where tai_san_id = r_product.ID;
    DELETE TS_TAI_SAN_HIEN_TRANG_SU_DUNG where tai_san_id = r_product.ID;
    DELETE bd_bien_dong_chi_tiet where  bien_dong_id in (SELECT bd.id from bd_bien_dong bd where bd.tai_san_id = r_product.ID);  
    DELETE bd_bien_dong where tai_san_id = r_product.ID;
    DELETE nv_yeu_cau_nhat_ky where  yeu_cau_id in (SELECT nv.id from nv_yeu_cau nv where nv.tai_san_id = r_product.ID);
	  DELETE nv_yeu_cau_chi_tiet where  yeu_cau_id in (SELECT nv.id from nv_yeu_cau nv where nv.tai_san_id = r_product.ID);
    DELETE nv_yeu_cau where tai_san_id = r_product.ID;
    DELETE ts_tai_san_cho_thue where tai_san_id = r_product.ID;
    DELETE ts_tai_san_kiem_ke_hoi_dong where kiem_ke_id in (select id from ts_tai_san_kiem_ke);
    DELETE ts_tai_san_kiem_ke_tai_san where tai_san_id = r_product.ID;
    DELETE ts_tai_san_kiem_ke where id in (select  kkts.kiem_ke_id from ts_tai_san_kiem_ke_tai_san kkts where kkts.tai_san_id = r_product.ID);
    DELETE ts_tai_san_vo_hinh where tai_san_id = r_product.ID;
    DELETE ts_tai_san_cln where tai_san_id = r_product.ID;
    DELETE ts_tai_san_dat where tai_san_id = r_product.ID;
    DELETE ts_tai_san_dat where tai_san_id = r_product.ID;
    DELETE ts_tai_san_nha where tai_san_id = r_product.ID;
    DELETE ts_tai_san_oto where tai_san_id = r_product.ID;
    DELETE ts_tai_san_vkt where tai_san_id = r_product.ID;
    DELETE ts_tai_san_may_moc where tai_san_id = r_product.ID;
    DELETE KT_HAO_MON_TAI_SAN_LOG where TAI_SAN_ID = r_product.ID;
    DELETE KT_HAO_MON_TAI_SAN where tai_san_id = r_product.ID;
    DELETE KT_KHAU_HAO_TAI_SAN where tai_san_id = r_product.ID;
    DELETE ts_tai_san where id = r_product.ID;
    --dbms_output.put_line('DELETE: ' || r_product.ID); 
  END LOOP;
  commit;
  RETURN 1;
END DEL_TAI_SAN_DONG_BO;