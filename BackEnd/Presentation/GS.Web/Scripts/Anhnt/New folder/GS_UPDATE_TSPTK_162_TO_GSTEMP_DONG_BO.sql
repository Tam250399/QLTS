create or replace FUNCTION GS_UPDATE_TSPTK_162_TO_GSTEMP_DONG_BO 
(
  PMA_TAI_SAN IN VARCHAR2 
, PLOAI_HINH_TAI_SAN IN NUMBER 
) RETURN NUMBER AS 
    bienDongJson CLOB;
    nguonVonJson CLOB;
    hienTrangJson CLOB;
    dataJson CLOB;
    bienDongGuid VARCHAR2(36);
    bdKhacCuoi2014 bd_khac %RowType;
    bdKhacCuoi bd_khac %RowType;
    ts31122014 kt_taisan_31122014 %RowType;
    gsLyDoBD gs_map_ly_do_bien_dong %RowType;
    tsKhac ts_khac %RowType;
    gsMapLts gs_map_loai_tai_san %RowType;
    nguyenGiaBanDau NUMBER;
BEGIN
BEGIN
--
    BEGIN
        select 
            * into tsKhac
        from 
            ts_khac
        where 
            MA_TAI_SAN = PMA_TAI_SAN;
    EXCEPTION WHEN NO_DATA_FOUND
        THEN --DBMS_OUTPUT.PUT_LINE('NO_DATA_FOUND ' || PMA_TAI_SAN);
        return 0;
    END;
--
    --lay bien dong + nguon von + hien trang
    hienTrangJson := '';
    bienDongJson := '';
    nguonVonJson := '';
    dataJson := '';
    FOR r_bd_ptk IN (
      SELECT *
      FROM bd_khac
      WHERE bd_khac.ma_tai_san = PMA_TAI_SAN
      order by bd_khac.BD_KHAC_ID desc
    ) LOOP bienDongGuid := raw_to_guid(LPAD(r_bd_ptk.BD_KHAC_ID, 32, '0'));
    BEGIN
    SELECT gstg.* INTO gsLyDoBD
    FROM dm_lydo_tanggiam dmtg,
      gs_map_ly_do_bien_dong gstg
    WHERE rownum = 1
      AND dmtg.MA_LY_DO = r_bd_ptk.MA_LY_DO
      AND (
        gstg.MA_OLD = dmtg.MA_LY_DO
        or dmtg.TEN_LY_DO = gstg.TEN_OLD
      )
      AND (
        gstg.LOAI_HINH_TAI_SAN_NEW = pLOAI_HINH_TAI_SAN
        or gstg.LOAI_HINH_TAI_SAN_NEW = 0
      );
    EXCEPTION
    WHEN NO_DATA_FOUND THEN gsLyDoBD := null;
    END;
    bienDongJson := bienDongJson||
        '{"GUID":"' || bienDongGuid || '",
        "NGUYEN_GIA":' || rtrim(to_char( r_bd_ptk.NGUYEN_GIA_NS + r_bd_ptk.NGUYEN_GIA_KHAC, 'FM9999999999999999999999990.99'), '.') ||',
        "NGAY_BIEN_DONG":"'|| r_bd_ptk.NGAY_BIEN_DONG || '",
        "DON_VI_NHAN_DIEU_CHUYEN_MA":"' || r_bd_ptk.MA_DONVI_NHAN_DIEU_CHUYEN || '",
        "LOAI_BIEN_DONG_ID":' || r_bd_ptk.LOAI_BIEN_DONG || ',
        "LY_DO_BIEN_DONG_MA":"' || nvl(gsLyDoBD.MA_NEW, '981')|| '",
        "LY_DO_BIEN_DONG_TEN":"' || GS_STRING_TO_STRINGJSON(nvl(gsLyDoBD.TEN_NEW, 'Khác')) || '",
        "DON_VI_MA":"' || r_bd_ptk.MA_DON_VI|| '",
        "NGAY_SU_DUNG":"' || r_bd_ptk.THOI_GIAN_SU_DUNG|| '",
        "TRANG_THAI":' || nvl(r_bd_ptk.DUYET_BIEN_DONG, 1) || ',
        "OTO_BIEN_KIEM_SOAT":"' || GS_STRING_TO_STRINGJSON(r_bd_ptk.KY_HIEU)|| '",
        "NGAY_DUYET":"' || r_bd_ptk.NGAY_DUYET_BIEN_DONG|| '",
        "GIA_TRI_CON_LAI":' || rtrim(to_char(r_bd_ptk.GIA_TRI_CON_LAI, 'FM9999999999999999999999990.99'), '.') ||',
        "KH_GIA_TINH_KHAU_HAO":' || rtrim(to_char(r_bd_ptk.KH_NGUYEN_GIA_NS + r_bd_ptk.KH_NGUYEN_GIA_KHAC + r_bd_ptk.KH_NGUYEN_GIA_ODA + r_bd_ptk.KH_NGUYEN_GIA_VIEN_TRO, 'FM9999999999999999999999990.99'), '.') ||',
        "KH_GIA_TRI_CON_LAI":' || rtrim(to_char(r_bd_ptk.KH_GIA_TRI_CON_LAI, 'FM9999999999999999999999990.99'), '.') ||',
        "KH_THANG_CON_LAI":' || rtrim(to_char(r_bd_ptk.SO_THANG_CON_SU_DUNG, 'FM9999999999999999999999990.99'), '.') ||',
        "HS_KHAC":"' || GS_STRING_TO_STRINGJSON(r_bd_ptk.SO_QD) || '"},';
    nguonVonJson := nguonVonJson ||
        '{"NGUON_VON_ID":1, "GIA_TRI":' || rtrim(to_char(r_bd_ptk.NGUYEN_GIA_NS, 'FM9999999999999999999999990.99'), '.') || ', "BIEN_DONG_GUID":"' || bienDongGuid ||'"},
        {"NGUON_VON_ID":3, "GIA_TRI":' || rtrim(to_char(r_bd_ptk.NGUYEN_GIA_KHAC, 'FM9999999999999999999999990.99'), '.') || ',"BIEN_DONG_GUID":"' || bienDongGuid ||'"},';
    hienTrangJson := hienTrangJson ||
        '{"HIEN_TRANG_ID":109,
        "TEN_HIEN_TRANG":"Quản lý nhà nước",
        "KIEU_DU_LIEU_ID":2,
        "GIA_TRI_CHECKBOX":' || (
          CASE
            WHEN r_bd_ptk.HIEN_TRANG_SU_DUNG = '1' THEN 'true'
            ELSE 'false'
          END
        ) || ',
        "BIEN_DONG_GUID":"' || bienDongGuid ||'"},
      
        {"HIEN_TRANG_ID":110,
        "TEN_HIEN_TRANG":"HĐSN-Không KD",
        "KIEU_DU_LIEU_ID":2,
        "NHOM_HIEN_TRANG_ID":1,
        "GIA_TRI_CHECKBOX":' || (
          CASE
            WHEN r_bd_ptk.HIEN_TRANG_SU_DUNG = '2' THEN 'true'
            ELSE 'false'
          END
        ) || ',
        "BIEN_DONG_GUID":"' || bienDongGuid ||'"},
      
        {"HIEN_TRANG_ID":111,
        "TEN_HIEN_TRANG":"HĐSN-Kinh doanh",
        "KIEU_DU_LIEU_ID":2,
        "NHOM_HIEN_TRANG_ID":1,
        "GIA_TRI_CHECKBOX":' || (
          CASE
            WHEN r_bd_ptk.HIEN_TRANG_SU_DUNG = '3' THEN 'true'
            ELSE 'false'
          END
        ) || ',
        "BIEN_DONG_GUID":"' || bienDongGuid ||'"},
      
        {"HIEN_TRANG_ID":112,
        "TEN_HIEN_TRANG":"Hoạt động khác",
        "KIEU_DU_LIEU_ID":2,
        "GIA_TRI_CHECKBOX":' || (
          CASE
            WHEN r_bd_ptk.HIEN_TRANG_SU_DUNG = '4' THEN 'true'
            ELSE 'false'
          END
        ) || ',
        "BIEN_DONG_GUID":"' || bienDongGuid ||'"},
      
        {"HIEN_TRANG_ID":114,
        "TEN_HIEN_TRANG":"HĐSN-LDLK",
        "KIEU_DU_LIEU_ID":2,
        "GIA_TRI_CHECKBOX": false,
        "BIEN_DONG_GUID":"' || bienDongGuid ||'"},
      
        {"HIEN_TRANG_ID":113,
        "TEN_HIEN_TRANG":"Cho thuê",
        "KIEU_DU_LIEU_ID":2,
        "GIA_TRI_CHECKBOX": false,
        "BIEN_DONG_GUID":"' || bienDongGuid ||'"},';
    END LOOP;
    --lay du lieu json cua tai san
    BEGIN
        SELECT
            * INTO bdKhacCuoi
        FROM
            BD_KHAC
        WHERE
            ROWNUM = 1
            AND MA_TAI_SAN = PMA_TAI_SAN
            AND DUYET_BIEN_DONG = 2
        ORDER BY NGAY_BIEN_DONG DESC;
    EXCEPTION 
        WHEN NO_DATA_FOUND THEN
        bdKhacCuoi:=NULL;
    END;
    BEGIN
        SELECT TO_CLOB(
            '{"BIEN_KIEM_SOAT":"' || GS_STRING_TO_STRINGJSON(NVL(bdKhacCuoi.KY_HIEU,TS.KY_HIEU)) || '",
            "TRANG_THAI_KH":' || NVL(bdKhacCuoi.tinh_khau_hao,TS.tinh_khau_hao) || ',
            "SO_CHO_NGOI":0,
            "TAI_TRONG": 0}'
          ) INTO dataJson
          FROM TS_KHAC TS
          WHERE MA_TAI_SAN = PMA_TAI_SAN;
    EXCEPTION 
        WHEN NO_DATA_FOUND THEN
        dataJson:=NULL;
    END;
    
    bienDongJson := REPLACE('[' || bienDongJson || ']','":,','":null,');
    nguonVonJson := REPLACE('[' || nguonVonJson || ']','":,','":null,');
    hienTrangJson := REPLACE('[' || hienTrangJson || ']','":,','":null,');
    --UPDATE lại temp đồng bộ
    SELECT (
        bd_khac.NGUYEN_GIA_NS + bd_khac.NGUYEN_GIA_KHAC + bd_khac.NGUYEN_GIA_ODA + bd_khac.NGUYEN_GIA_VIEN_TRO
      ) INTO nguyenGiaBanDau
    FROM bd_khac
    WHERE bd_khac.ma_tai_san = PMA_TAI_SAN
      AND rownum = 1
    order by bd_khac.NGAY_BIEN_DONG;
    EXCEPTION
    WHEN NO_DATA_FOUND THEN nguyenGiaBanDau := 0;
    END;


        
    select 
        gsmap.* into gsMapLts
    from
        gs_map_loai_tai_san gsmap, ts_tai_san ts,dm_loaitaisan dmlts
    where
        dmlts.loai_tai_san_id = ts.loai_tai_san_id
        and gsmap.ma_old = dmlts.ma_loai_tai_san
        and ts.ma_tai_san = PMA_TAI_SAN
        and gsmap.LOAI_HINH_TAI_SAN_ID = PLOAI_HINH_TAI_SAN
        and gsmap.CHE_DO_HAO_MON_ID = 2;
   
    UPDATE gs_temp_dong_bo_du_lieu gstemp
    SET gstemp.tai_san_json = REPLACE(dataJson,'":,','":null,'),
        gstemp.lst_bien_dong_json = bienDongJson,
        gstemp.lst_nguon_von_json = nguonVonJson,
        gstemp.lst_hien_trang_json = hienTrangJson,
        gstemp.NUOC_SAN_XUAT_MA =(
                                case
                                  when NVL(bdKhacCuoi.MA_QUOC_GIA,tsKhac.MA_QUOC_GIA) = '999' then 'VN'
                                  when NVL(bdKhacCuoi.MA_QUOC_GIA,tsKhac.MA_QUOC_GIA) not in (select MA_QUOC_GIA from dm_quocgia) then 'VN'
                                  else NVL(bdKhacCuoi.MA_QUOC_GIA,tsKhac.MA_QUOC_GIA)
                                end
                              ),
        gstemp.NAM_SAN_XUAT = NVL(bdKhacCuoi.NAM_SX,tsKhac.NAM_SX),
        gstemp.ngay_su_dung = NVL(bdKhacCuoi.thoi_gian_su_dung,tsKhac.thoi_gian_su_dung),
        gstemp.NGUYEN_GIA_BAN_DAU =nguyenGiaBanDau,
        gstemp.ma_loai_tai_san = gsMapLts.ma_new,
        gstemp.loai_tai_san_ten = gsMapLts.ten_new
    WHERE gstemp.ma_tai_san = PMA_TAI_SAN;
    commit;
  RETURN 0;
END GS_UPDATE_TSPTK_162_TO_GSTEMP_DONG_BO;