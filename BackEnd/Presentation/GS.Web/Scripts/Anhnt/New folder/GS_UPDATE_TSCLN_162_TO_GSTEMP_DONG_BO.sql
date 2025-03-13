create or replace FUNCTION GS_UPDATE_TSCLN_162_TO_GSTEMP_DONG_BO 
(
  PMA_TAI_SAN IN VARCHAR2 
, PLOAI_HINH_TAI_SAN IN NUMBER DEFAULT 7
) RETURN NUMBER AS 
    bienDongJson CLOB;
    nguonVonJson CLOB;
    hienTrangJson CLOB;
    dataJson CLOB;
    bienDongGuid VARCHAR2(36);
    nguyenGiaBANDau NUMBER;
    bdKhacCuoi bd_khac %RowType;
    gsLyDoBD gs_map_ly_do_bien_dong %RowType;
    tsKhac ts_khac %RowType;
    gsMapLts gs_map_loai_tai_san %RowType;
    GSTEMPDONGBO GS_TEMP_DONG_BO_DU_LIEU %ROWTYPE;
BEGIN
    --
    BEGIN
        SELECT 
            * INTO tsKhac
        FROM 
            ts_khac
        WHERE 
            MA_TAI_SAN = PMA_TAI_SAN;
    EXCEPTION WHEN NO_DATA_FOUND
        THEN --DBMS_OUTPUT.PUT_LINE('NO_DATA_FOUND ' || PMA_TAI_SAN);
        RETURN 0;
    END;
    --
    hienTrangJson := '';
    bienDongJson := '';
    nguonVonJson := '';
    dataJson := '';
    FOR r_bd_cln IN (
      SELECT *
      FROM bd_khac
      WHERE bd_khac.ma_tai_san = PMA_TAI_SAN
      order by bd_khac.BD_KHAC_ID desc
    ) LOOP bienDongGuid := raw_to_guid(LPAD(r_bd_cln.BD_KHAC_ID, 32, '0'));
    BEGIN
    SELECT gstg.* INTO gsLyDoBD
    FROM dm_lydo_tanggiam dmtg,
      gs_map_ly_do_bien_dong gstg
    WHERE rownum = 1
      AND dmtg.MA_LY_DO = r_bd_cln.MA_LY_DO
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
        "NGUYEN_GIA":' || rtrim(to_char(r_bd_cln.NGUYEN_GIA_NS + r_bd_cln.NGUYEN_GIA_KHAC, 'FM9999999999999999999999990.99'), '.') || ',
        "NGAY_BIEN_DONG":"'|| r_bd_cln.NGAY_BIEN_DONG || '",
        "DON_VI_NHAN_DIEU_CHUYEN_MA":"' || r_bd_cln.MA_DONVI_NHAN_DIEU_CHUYEN || '",
        "LOAI_BIEN_DONG_ID":' || r_bd_cln.LOAI_BIEN_DONG || ',
        "LY_DO_BIEN_DONG_MA":"' || nvl(gsLyDoBD.MA_NEW, '973') || '",
        "LY_DO_BIEN_DONG_TEN":"' || GS_STRING_TO_STRINGJSON(nvl(gsLyDoBD.TEN_NEW, 'Khác')) || '",
        "DON_VI_MA":"' || r_bd_cln.MA_DON_VI || '",
        "NGAY_SU_DUNG":"' || r_bd_cln.THOI_GIAN_SU_DUNG || '",
        "THONG_SO_KY_THUAT":"' ||  GS_STRING_TO_STRINGJSON(r_bd_cln.THONG_SO_KT) || '",
        "TRANG_THAI":' || nvl(r_bd_cln.DUYET_BIEN_DONG, 1) || ',
        "NGAY_DUYET":"' || r_bd_cln.NGAY_DUYET_BIEN_DONG || '",
        "GIA_TRI_CON_LAI":' || rtrim(to_char(r_bd_cln.GIA_TRI_CON_LAI, 'FM9999999999999999999999990.99'), '.') || ',
        "KH_GIA_TINH_KHAU_HAO":' || rtrim(to_char(r_bd_cln.KH_NGUYEN_GIA_NS + r_bd_cln.KH_NGUYEN_GIA_KHAC + r_bd_cln.KH_NGUYEN_GIA_ODA + r_bd_cln.KH_NGUYEN_GIA_VIEN_TRO, 'FM9999999999999999999999990.99'), '.') || ',
        "KH_GIA_TRI_CON_LAI":' || rtrim(to_char(r_bd_cln.KH_GIA_TRI_CON_LAI, 'FM9999999999999999999999990.99'), '.') || ',
        "KH_THANG_CON_LAI":' || r_bd_cln.SO_THANG_CON_SU_DUNG || ',
        "HS_KHAC":"' || GS_STRING_TO_STRINGJSON(r_bd_cln.SO_QD) || '"},';
    nguonVonJson := nguonVonJson ||
        '{"NGUON_VON_ID":1, "GIA_TRI":' || r_bd_cln.NGUYEN_GIA_NS || ', "BIEN_DONG_GUID":"' || bienDongGuid ||'"},
        {"NGUON_VON_ID":3, "GIA_TRI":' || r_bd_cln.NGUYEN_GIA_KHAC || ', "BIEN_DONG_GUID":"' || bienDongGuid ||'"},';
    hienTrangJson := hienTrangJson ||
        '{"HIEN_TRANG_ID":172,
        "TEN_HIEN_TRANG":"Quản lý nhà nước",
        "KIEU_DU_LIEU_ID":2,
        "GIA_TRI_CHECKBOX":' || (
          CASE
            WHEN r_bd_cln.HIEN_TRANG_SU_DUNG = '4' THEN 'true'
            ELSE 'false'
          END
        ) || ',
        "BIEN_DONG_GUID":"' || bienDongGuid ||'"},
      
        {"HIEN_TRANG_ID":173,
        "TEN_HIEN_TRANG":"HĐSN-Không KD",
        "KIEU_DU_LIEU_ID":2,
        "NHOM_HIEN_TRANG_ID":1,
        "GIA_TRI_CHECKBOX":' || (
          CASE
            WHEN r_bd_cln.HIEN_TRANG_SU_DUNG = '4' THEN 'true'
            ELSE 'false'
          END
        ) || ',
        "BIEN_DONG_GUID":"' || bienDongGuid ||'"},
      
        {"HIEN_TRANG_ID":174,
        "TEN_HIEN_TRANG":"HĐSN-Kinh doanh",
        "KIEU_DU_LIEU_ID":2,
        "NHOM_HIEN_TRANG_ID":1,
        "GIA_TRI_CHECKBOX":' || (
          CASE
            WHEN r_bd_cln.HIEN_TRANG_SU_DUNG = '4' THEN 'true'
            ELSE 'false'
          END
        ) || ',
        "BIEN_DONG_GUID":"' || bienDongGuid ||'"},
      
        {"HIEN_TRANG_ID":175,
        "TEN_HIEN_TRANG":"Hoạt động khác",
        "KIEU_DU_LIEU_ID":2,
        "GIA_TRI_CHECKBOX":' || (
          CASE
            WHEN r_bd_cln.HIEN_TRANG_SU_DUNG = '4' THEN 'true'
            ELSE 'false'
          END
        ) || ',
        "BIEN_DONG_GUID":"' || bienDongGuid ||'"},
      
        {"HIEN_TRANG_ID":176,
        "TEN_HIEN_TRANG":"Liên doanh, liên kết",
        "KIEU_DU_LIEU_ID":2,
        "GIA_TRI_CHECKBOX": false,
        "BIEN_DONG_GUID":"' || bienDongGuid ||'"},
      
        {"HIEN_TRANG_ID":177,
        "TEN_HIEN_TRANG":"Cho thuê",
        "KIEU_DU_LIEU_ID":2,
        "GIA_TRI_CHECKBOX": false,
        "BIEN_DONG_GUID":"' || bienDongGuid ||'"},';
    END LOOP;
    --lay du lieu json cua tai san
    
    bienDongJson := REPLACE('[' || bienDongJson || ']','":,','":null,');
    nguonVonJson := REPLACE('[' || nguonVonJson || ']','":,','":null,');
    hienTrangJson := REPLACE('[' || hienTrangJson || ']','":,','":null,');
    --UPDATE lại temp đồng bộ
    BEGIN
        SELECT (
            bd_khac.NGUYEN_GIA_NS + bd_khac.NGUYEN_GIA_KHAC + bd_khac.NGUYEN_GIA_ODA + bd_khac.NGUYEN_GIA_VIEN_TRO
          ) INTO nguyenGiaBANDau
        FROM bd_khac
        WHERE bd_khac.ma_tai_san = PMA_TAI_SAN
          AND rownum = 1
        order by bd_khac.NGAY_BIEN_DONG;
        EXCEPTION
        WHEN NO_DATA_FOUND THEN nguyenGiaBANDau := NULL;
    END;
    
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
        SELECT TO_CLOB('{"NAM_SINH":' || (CASE WHEN NVL(bdKhacCuoi.NAM_SX,ts.NAM_SX) IS NULL THEN 'null' ELSE NVL(bdKhacCuoi.NAM_SX,ts.NAM_SX) ||'' END) || '}') INTO dataJson
        FROM ts_khac ts
        WHERE ts.ma_tai_san = PMA_TAI_SAN;
        EXCEPTION
        WHEN NO_DATA_FOUND THEN dataJson := NULL;
    END;
    
    SELECT 
        gsmap.* INTO gsMapLts
    FROM
        gs_map_loai_tai_san gsmap, ts_tai_san ts,dm_loaitaisan dmlts
    WHERE
        dmlts.loai_tai_san_id = ts.loai_tai_san_id
        AND gsmap.ma_old = dmlts.ma_loai_tai_san
        AND ts.ma_tai_san = PMA_TAI_SAN
        AND gsmap.LOAI_HINH_TAI_SAN_ID = PLOAI_HINH_TAI_SAN
        AND gsmap.CHE_DO_HAO_MON_ID = 2;
        
    UPDATE gs_temp_dong_bo_du_lieu gstemp
    SET gstemp.tai_san_json = REPLACE(dataJson,'":,','":null,'),
        gstemp.lst_bien_dong_json = bienDongJson,
        gstemp.lst_nguon_von_json = nguonVonJson,
        gstemp.lst_hien_trang_json = hienTrangJson,
        gstemp.NUOC_SAN_XUAT_MA =(
                                case
                                  when NVL(bdKhacCuoi.MA_QUOC_GIA,tsKhac.MA_QUOC_GIA) = '999' then 'VN'
                                  when NVL(bdKhacCuoi.MA_QUOC_GIA,tsKhac.MA_QUOC_GIA) not in (SELECT MA_QUOC_GIA FROM dm_quocgia) then 'VN'
                                  else NVL(bdKhacCuoi.MA_QUOC_GIA,tsKhac.MA_QUOC_GIA)
                                end
                              ),
        gstemp.NAM_SAN_XUAT = NVL(bdKhacCuoi.NAM_SX,tsKhac.NAM_SX),
        gstemp.ngay_su_dung = NVL(bdKhacCuoi.thoi_gian_su_dung,tsKhac.thoi_gian_su_dung),
        gstemp.NGUYEN_GIA_BAN_DAU = nguyenGiaBANDau,
        gstemp.ma_loai_tai_san = gsMapLts.ma_new,
        gstemp.loai_tai_san_ten = gsMapLts.ten_new
    WHERE gstemp.ma_tai_san = PMA_TAI_SAN;
   
    commit;
    
  RETURN 1;
END GS_UPDATE_TSCLN_162_TO_GSTEMP_DONG_BO;