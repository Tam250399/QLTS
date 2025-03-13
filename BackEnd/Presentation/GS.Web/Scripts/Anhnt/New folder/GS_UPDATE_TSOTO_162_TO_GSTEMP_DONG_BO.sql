create or replace FUNCTION GS_UPDATE_TSOTO_162_TO_GSTEMP_DONG_BO 
(
  PMA_TAI_SAN IN VARCHAR2 
, PLOAI_HINH_TAI_SAN IN NUMBER DEFAULT 4 
) RETURN NUMBER AS 
    bienDongJson CLOB;
    nguonVonJson CLOB;
    hienTrangJson CLOB;
    dataJson CLOB;
    bienDongGuid VARCHAR2(36);
    bdOtoCuoi bd_oto %RowType;
    ma_nhan_xe varchar2(50);
    ma_chuc_danh varchar2(50);
    gsLyDoBD gs_map_ly_do_bien_dong %RowType;
    tsOto ts_oto %RowType;
    gsMapLts gs_map_loai_tai_san %RowType;
    nguyenGiaBanDau NUMBER;
BEGIN
--
    BEGIN
        select 
            * into tsOto
        from 
            ts_oto
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
    FOR r_bd_oto IN (
      SELECT *
      FROM bd_oto
      WHERE bd_oto.ma_tai_san = PMA_TAI_SAN
      order by bd_oto.bd_oto_id desc
    ) LOOP BEGIN
    SELECT dm_nhan_xe.ma_nhan_xe INTO ma_nhan_xe
    FROM dm_nhan_xe
    WHERE ROWNUM = 1
      AND dm_nhan_xe.ten_nhan_xe = r_bd_oto.nhan_hieu;
    EXCEPTION
    WHEN NO_DATA_FOUND THEN ma_nhan_xe := '';
    END;
    BEGIN
    SELECT dm_chuc_danh.ma_chuc_danh INTO ma_chuc_danh
    FROM dm_chuc_danh
    WHERE dm_chuc_danh.id = r_bd_oto.CHUC_DANH_ID;
    EXCEPTION
    WHEN NO_DATA_FOUND THEN ma_chuc_danh := '';
    END;
    BEGIN
    SELECT gstg.* INTO gsLyDoBD
    FROM dm_lydo_tanggiam dmtg,
      gs_map_ly_do_bien_dong gstg
    WHERE rownum = 1
      AND dmtg.MA_LY_DO = r_bd_oto.MA_LY_DO
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
    bienDongGuid := raw_to_guid(LPAD(r_bd_oto.BD_OTO_ID, 32, '0'));
    bienDongJson := bienDongJson||
        '{"GUID":"' || bienDongGuid || '",
        "NGUYEN_GIA":' || rtrim(to_char( r_bd_oto.NGUYEN_GIA_NS + r_bd_oto.NGUYEN_GIA_KHAC ,'FM9999999999999999999999990.99'), '.') ||',
        "NGAY_BIEN_DONG":"'|| r_bd_oto.NGAY_BIEN_DONG || '",
        "DON_VI_NHAN_DIEU_CHUYEN_MA":"' || r_bd_oto.MA_DONVI_NHAN_DIEU_CHUYEN || '",
        "LOAI_BIEN_DONG_ID":' || r_bd_oto.LOAI_BIEN_DONG || ',
        "LY_DO_BIEN_DONG_MA":"' || nvl(gsLyDoBD.MA_NEW, '049') || '",
        "LY_DO_BIEN_DONG_TEN":"' || GS_STRING_TO_STRINGJSON(nvl(gsLyDoBD.TEN_NEW, 'Khác')) || '",
        "DON_VI_MA":"' || r_bd_oto.MA_DON_VI || '",
        "NGAY_SU_DUNG":"' || r_bd_oto.THOI_GIAN_SU_DUNG || '",
        "NHAN_HIEU":"' || r_bd_oto.NHAN_HIEU || '",
        "TRANG_THAI":' || nvl(r_bd_oto.DUYET_BIEN_DONG, 1) || ',
        "OTO_BIEN_KIEM_SOAT":"' || GS_STRING_TO_STRINGJSON(r_bd_oto.BIEN_KIEM_SOAT) || '",
        "OTO_SO_CHO_NGOI":' || r_bd_oto.SO_CHO_NGOI || ',
        "OTO_CHUC_DANH_MA":"' || ma_chuc_danh || '",
        "OTO_NHAN_XE_MA":"' || ma_nhan_xe || '",
        "OTO_TAI_TRONG":' || rtrim(to_char(r_bd_oto.TAI_TRONG,'FM9999999999999999999999990.99'), '.') || ',
        "OTO_XI_LANH":' || rtrim(to_char(r_bd_oto.DUNGTICH_XILANH,'FM9999999999999999999999990.99'), '.') || ',
        "NGAY_DUYET":"' || r_bd_oto.NGAY_DUYET_BIEN_DONG || '",
        "GIA_TRI_CON_LAI":' || rtrim(to_char(r_bd_oto.GIA_TRI_CON_LAI,'FM9999999999999999999999990.99'), '.') ||',
        "KH_GIA_TINH_KHAU_HAO":' || rtrim(to_char( r_bd_oto.KH_NGUYEN_GIA_NS + r_bd_oto.KH_NGUYEN_GIA_KHAC + r_bd_oto.KH_NGUYEN_GIA_ODA + r_bd_oto.KH_NGUYEN_GIA_VIEN_TRO,'FM9999999999999999999999990.99'), '.') ||',
        "KH_GIA_TRI_CON_LAI":' || r_bd_oto.KH_GIA_TRI_CON_LAI || ',
        "KH_THANG_CON_LAI":' || r_bd_oto.SO_THANG_CON_SU_DUNG || ',
        "HS_KHAC":"' || GS_STRING_TO_STRINGJSON(r_bd_oto.SO_QD) || '"},';
    nguonVonJson := nguonVonJson||
        '{"NGUON_VON_ID":1, "GIA_TRI":' || r_bd_oto.NGUYEN_GIA_NS ||', "BIEN_DONG_GUID":"' || bienDongGuid ||'"},
        {"NGUON_VON_ID":3, "GIA_TRI":' || r_bd_oto.NGUYEN_GIA_KHAC || ', "BIEN_DONG_GUID":"' || bienDongGuid ||'"},';
    hienTrangJson := hienTrangJson||
        '{"HIEN_TRANG_ID":99,
        "TEN_HIEN_TRANG":"Quản lý nhà nước",
        "KIEU_DU_LIEU_ID":2,
        "GIA_TRI_CHECKBOX":' || (
          CASE
            WHEN r_bd_oto.HIEN_TRANG_SU_DUNG = '1' THEN 'true'
            ELSE 'false'
          END
        )||',
        "BIEN_DONG_GUID":"' || bienDongGuid ||'"},
      
        {"HIEN_TRANG_ID":100,
        "TEN_HIEN_TRANG":"HĐSN-Không KD",
        "KIEU_DU_LIEU_ID":2,
        "NHOM_HIEN_TRANG_ID":1,
        "GIA_TRI_CHECKBOX":' || (
          CASE
            WHEN r_bd_oto.HIEN_TRANG_SU_DUNG = '2' THEN 'true'
            ELSE 'false'
          END
        )||',
        "BIEN_DONG_GUID":"' || bienDongGuid ||'"},
      
        {"HIEN_TRANG_ID":101,
        "TEN_HIEN_TRANG":"HĐSN-Kinh doanh",
        "KIEU_DU_LIEU_ID":2,
        "NHOM_HIEN_TRANG_ID":1,
        "GIA_TRI_CHECKBOX":' || (
          CASE
            WHEN r_bd_oto.HIEN_TRANG_SU_DUNG = '3' THEN 'true'
            ELSE 'false'
          END
        )||',
        "BIEN_DONG_GUID":"' || bienDongGuid ||'"},
      
        {"HIEN_TRANG_ID":102,
        "TEN_HIEN_TRANG":"Hoạt động khác",
        "KIEU_DU_LIEU_ID":2,
        "GIA_TRI_CHECKBOX":' || (
          CASE
            WHEN r_bd_oto.HIEN_TRANG_SU_DUNG = '4' THEN 'true'
            ELSE 'false'
          END
        )||',
        "BIEN_DONG_GUID":"' || bienDongGuid ||'"},
      
        {"HIEN_TRANG_ID":103,
        "TEN_HIEN_TRANG":"HĐSN-LDLK",
        "KIEU_DU_LIEU_ID":2,
        "GIA_TRI_CHECKBOX": false,
        "BIEN_DONG_GUID":"' || bienDongGuid ||'"},
      
        {"HIEN_TRANG_ID":104,
        "TEN_HIEN_TRANG":"Cho thuê",
        "KIEU_DU_LIEU_ID":2,
        "GIA_TRI_CHECKBOX": false,
        "BIEN_DONG_GUID":"' || bienDongGuid ||'"},';
    END LOOP;
   
    bienDongJson := REPLACE('[' || bienDongJson || ']','":,','":null,');
    nguonVonJson := REPLACE('[' || nguonVonJson || ']','":,','":null,');
    hienTrangJson := REPLACE('[' || hienTrangJson || ']','":,','":null,');
    --UPDATE lại temp đồng bộ
    BEGIN
        SELECT (
            bd_oto.NGUYEN_GIA_NS + bd_oto.NGUYEN_GIA_KHAC + bd_oto.NGUYEN_GIA_ODA + bd_oto.NGUYEN_GIA_VIEN_TRO
          ) INTO nguyenGiaBanDau
        FROM bd_oto
        WHERE bd_oto.ma_tai_san = PMA_TAI_SAN
          AND rownum = 1
        order by bd_oto.NGAY_BIEN_DONG;
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
    BEGIN 
        SELECT
            * INTO bdOtoCuoi
        FROM
            BD_OTO
        WHERE
            ROWNUM = 1
            AND DUYET_BIEN_DONG = 2
            AND MA_TAI_SAN = PMA_TAI_SAN
        ORDER BY NGAY_BIEN_DONG DESC;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
       bdOtoCuoi:=NULL;
    END;
    
     --lay du lieu json cua tai san
     BEGIN
        SELECT TO_CLOB(
            '{"BIEN_KIEM_SOAT":"' || NVL(bdOtoCuoi.BIEN_KIEM_SOAT,ts.BIEN_KIEM_SOAT) || '",
            "SO_CHO_NGOI":' ||(CASE WHEN bdOtoCuoi.SO_CHO_NGOI > 0 THEN bdOtoCuoi.SO_CHO_NGOI ELSE ts.SO_CHO_NGOI END) || ',
            "DUNG_TICH":' || rtrim(to_char(NVL(bdOtoCuoi.DUNGTICH_XILANH,ts.DUNGTICH_XILANH),'FM9999999999999999999999990.99'), '.') || ',
            "CHUC_DANH_MA":"' || (
              SELECT dm_chuc_danh.ma_chuc_danh
              FROM dm_chuc_danh
              WHERE rownum = 1
                AND dm_chuc_danh.id = NVL(bdOtoCuoi.CHUC_DANH_ID,ts.CHUC_DANH_ID)
            ) || '",
            "TAI_TRONG":' || rtrim(to_char((CASE WHEN bdOtoCuoi.TAI_TRONG > 0 THEN bdOtoCuoi.TAI_TRONG ELSE ts.TAI_TRONG END),'FM9999999999999999999999990.99'), '.') || ',
            "NHAN_XE_MA":"' || (
              SELECT dm_nhan_xe.ma_nhan_xe
              FROM dm_nhan_xe
              WHERE ROWNUM = 1
                AND dm_nhan_xe.ten_nhan_xe = NVL(bdOtoCuoi.nhan_hieu,ts.nhan_hieu)
            ) || '",
            "TRANG_THAI_KH":' || (CASE WHEN NVL(bdOtoCuoi.tinh_khau_hao,ts.tinh_khau_hao) IS NULL THEN 'null' ELSE NVL(bdOtoCuoi.tinh_khau_hao,ts.tinh_khau_hao)||'' END ) || '}'
          ) INTO dataJson
        FROM ts_oto ts
        WHERE ts.ma_tai_san = PMA_TAI_SAN;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN dataJson := NULL;
        dataJson:=NULL;
    END;
    
    UPDATE gs_temp_dong_bo_du_lieu gstemp
    SET gstemp.tai_san_json = REPLACE(dataJson,'":,','":null,'),
        gstemp.lst_bien_dong_json = bienDongJson,
        gstemp.lst_nguon_von_json = nguonVonJson,
        gstemp.lst_hien_trang_json = hienTrangJson,
        gstemp.NUOC_SAN_XUAT_MA = (
                                    case
                                    when NVL(bdOtoCuoi.NUOC_SX,tsOto.NUOC_SX) = '999' then 'VN'
                                    when NVL(bdOtoCuoi.NUOC_SX,tsOto.NUOC_SX) not in (select MA_QUOC_GIA from dm_quocgia) then 'VN'
                                    else NVL(bdOtoCuoi.NUOC_SX,tsOto.NUOC_SX)
                                    end
        ),
        gstemp.ngay_su_dung = NVL(bdOtoCuoi.thoi_gian_su_dung,tsOto.thoi_gian_su_dung),
        gstemp.NAM_SAN_XUAT = NVL(bdOtoCuoi.NAM_SX,tsOto.NAM_SX),
        gstemp.NGUYEN_GIA_BAN_DAU = nguyenGiaBanDau,
        gstemp.ma_loai_tai_san = gsMapLts.ma_new,
        gstemp.loai_tai_san_ten = gsMapLts.ten_new
    WHERE gstemp.ma_tai_san = PMA_TAI_SAN;
    commit;
  RETURN 0;
END GS_UPDATE_TSOTO_162_TO_GSTEMP_DONG_BO;