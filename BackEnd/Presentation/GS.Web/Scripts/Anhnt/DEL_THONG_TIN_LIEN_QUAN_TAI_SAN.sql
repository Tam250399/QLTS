create or replace FUNCTION DEL_THONG_TIN_LIEN_QUAN_TAI_SAN 
(
  PID IN NUMBER 
) RETURN NUMBER AS 
BEGIN
    DELETE 
      ts_tai_san_nguon_von 
    where 
      tai_san_id = PID
      or BIEN_DONG_ID in (select 
                            id 
                          from 
                            bd_bien_dong 
                          where 
                            tai_san_id = PID);
    DELETE TS_TAI_SAN_HIEN_TRANG_SU_DUNG where tai_san_id = PID
    or BIEN_DONG_ID in (select id from bd_bien_dong where tai_san_id = PID);
    COMMIT;
    DELETE bd_bien_dong_chi_tiet where  bien_dong_id in (SELECT bd.id from bd_bien_dong bd where bd.tai_san_id = PID); 
    COMMIT;
    DELETE bd_bien_dong where tai_san_id = PID;
    COMMIT;
    DELETE nv_yeu_cau_nhat_ky where  yeu_cau_id in (SELECT nv.id from nv_yeu_cau nv where nv.tai_san_id = PID);
    COMMIT;
	  DELETE nv_yeu_cau_chi_tiet where  yeu_cau_id in (SELECT nv.id from nv_yeu_cau nv where nv.tai_san_id = PID);
    COMMIT;
    DELETE nv_yeu_cau where tai_san_id = PID;
    COMMIT;
    DELETE KT_HAO_MON_TAI_SAN_LOG where TAI_SAN_ID = PID;
    COMMIT;
    DELETE KT_HAO_MON_TAI_SAN where tai_san_id = PID;
    COMMIT;
    DELETE KT_KHAU_HAO_TAI_SAN where tai_san_id = PID;
    COMMIT;
     DELETE ts_tai_san_vo_hinh where tai_san_id =PID;
    COMMIT;
    DELETE ts_tai_san_cln where tai_san_id =PID;
    COMMIT;
    DELETE ts_tai_san_dat where tai_san_id =PID;
    COMMIT;
    DELETE ts_tai_san_nha where tai_san_id =PID;
    COMMIT;
    DELETE ts_tai_san_oto where tai_san_id =PID;
    COMMIT;
    DELETE ts_tai_san_vkt where tai_san_id =PID;
    COMMIT;
    DELETE ts_tai_san_may_moc where tai_san_id =PID;
    COMMIT;
  RETURN 1;
END DEL_THONG_TIN_LIEN_QUAN_TAI_SAN;