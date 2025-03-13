create or replace FUNCTION GS_UPDATE_TSCLN_32_TO_GSTEMP_DONG_BO 
(
  PMA_TAI_SAN IN VARCHAR2 
, PLOAI_HINH_TAI_SAN IN NUMBER DEFAULT 7
) RETURN NUMBER AS
    nguyenGiaBanDau NUMBER;
    bienDongJson CLOB;
    nguonVonJson CLOB;
    hienTrangJson CLOB;
    dataJson CLOB;
    trangThai NUMBER;
    bienDongGuid VARCHAR2(36);
    bdKhacCuoi2014 bd_khac %RowType;
    bdKhacCuoi bd_khac %RowType;
    ts31122014 kt_taisan_31122014 %RowType;
    gsLyDoBD gs_map_ly_do_bien_dong %RowType;
    tsKhac ts_khac %RowType;
    gsMapLts gs_map_loai_tai_san %RowType;
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
    hienTrangJson := '';
    bienDongJson := '';
    nguonVonJson := '';
    dataJson := '';
    

    select * into ts31122014
    from kt_taisan_31122014
    where PMA_TAI_SAN = kt_taisan_31122014.ma_tai_san;
    
    BEGIN
        select 
            * into bdKhacCuoi2014
        from 
            bd_khac
        where 
            EXTRACT(
                year
                from NGAY_BIEN_DONG
              ) < 2015
            and rownum = 1
            and DUYET_BIEN_DONG = 2
            AND MA_TAI_SAN = PMA_TAI_SAN
        order by NGAY_BIEN_DONG desc;
        bienDongGuid := raw_to_guid(LPAD(bdKhacCuoi2014.BD_KHAC_ID, 32, '0'));
        bienDongGuid:=REPLACE(bienDongGuid,'"GUID":"----"','"GUID":"00000000-0000-0000-0000-000000000000"');
        bienDongJson := bienDongJson ||
            '{"GUID":"' || bienDongGuid || '",
            "NGUYEN_GIA":' || rtrim(to_char((
              ts31122014.NGUYEN_GIA_NS + ts31122014.NGUYEN_GIA_KHAC
            ), 'FM9999999999999999999999990.99'), '.')||',
            "NGAY_BIEN_DONG":"' || to_date('01-01-2015', 'DD-MM-YYYY') || '",
            "DON_VI_NHAN_DIEU_CHUYEN_MA":"' || GS_STRING_TO_STRINGJSON(bdKhacCuoi2014.MA_DONVI_NHAN_DIEU_CHUYEN) || '",
            "LOAI_BIEN_DONG_ID":1,
            "LY_DO_BIEN_DONG_MA":"001",
            "LY_DO_BIEN_DONG_TEN":"Đăng ký lần đầu",
            "DON_VI_MA":"' || GS_STRING_TO_STRINGJSON(ts31122014.MA_DON_VI) || '",
            "NGAY_SU_DUNG":"' || bdKhacCuoi2014.THOI_GIAN_SU_DUNG || '",
            "THONG_SO_KY_THUAT":"' || GS_STRING_TO_STRINGJSON(bdKhacCuoi2014.THONG_SO_KT) || '",
            "TRANG_THAI":2,
            "NGAY_DUYET":"' || to_date('01-01-2015', 'DD-MM-YYYY') || '",
            "GIA_TRI_CON_LAI":' || rtrim(to_char(ts31122014.GIA_TRI_CON_LAI, 'FM9999999999999999999999990.99'), '.')||',
            "HS_KHAC":"' || GS_STRING_TO_STRINGJSON(bdKhacCuoi2014.SO_QD) || '"}';
        nguonVonJson := nguonVonJson ||
            '{"NGUON_VON_ID":1,
            "GIA_TRI":' || rtrim(to_char(ts31122014.NGUYEN_GIA_NS, 'FM9999999999999999999999990.99'), '.')||',
            "BIEN_DONG_GUID":"' || bienDongGuid || '"},
          
            {"NGUON_VON_ID":3,
            "GIA_TRI":' || rtrim(to_char(ts31122014.NGUYEN_GIA_KHAC, 'FM9999999999999999999999990.99'), '.')||',
            "BIEN_DONG_GUID":"' || bienDongGuid || '"},';
        hienTrangJson := hienTrangJson ||
            '{"HIEN_TRANG_ID":172,
            "TEN_HIEN_TRANG":"Quản lý nhà nước",
            "KIEU_DU_LIEU_ID":2,
            "GIA_TRI_CHECKBOX":' || (
              CASE
                WHEN bdKhacCuoi2014.HIEN_TRANG_SU_DUNG = '4' THEN 'true'
                ELSE 'false'
              end
            ) || ',
            "BIEN_DONG_GUID":"' || bienDongGuid || '"},
          
            {"HIEN_TRANG_ID":173,
            "TEN_HIEN_TRANG":"HĐSN-Không KD",
            "KIEU_DU_LIEU_ID":2,
            "NHOM_HIEN_TRANG_ID":1,
            "GIA_TRI_CHECKBOX":' || (
              CASE
                WHEN bdKhacCuoi2014.HIEN_TRANG_SU_DUNG = '4' THEN 'true'
                ELSE 'false'
              end
            ) || ',
            "BIEN_DONG_GUID":"' || bienDongGuid || '"},
          
            {"HIEN_TRANG_ID":174,
            "TEN_HIEN_TRANG":"HĐSN-Kinh doanh",
            "KIEU_DU_LIEU_ID":2,
            "NHOM_HIEN_TRANG_ID":1,
            "GIA_TRI_CHECKBOX":' || (
              CASE
                WHEN bdKhacCuoi2014.HIEN_TRANG_SU_DUNG = '4' THEN 'true'
                ELSE 'false'
              end
            ) || ',
            "BIEN_DONG_GUID":"' || bienDongGuid || '"},
          
            {"HIEN_TRANG_ID":175,
            "TEN_HIEN_TRANG":"Hoạt động khác",
            "KIEU_DU_LIEU_ID":2,
            "GIA_TRI_CHECKBOX":' || (
              CASE
                WHEN bdKhacCuoi2014.HIEN_TRANG_SU_DUNG = '4' THEN 'true'
                ELSE 'false'
              end
            ) || ',
            "BIEN_DONG_GUID":"' || bienDongGuid || '"},
          
            {"HIEN_TRANG_ID":176,
            "TEN_HIEN_TRANG":"Liên doanh, liên kết",
            "KIEU_DU_LIEU_ID":2,
            "GIA_TRI_CHECKBOX":false,
            "BIEN_DONG_GUID":"' || bienDongGuid || '"},
          
            {"HIEN_TRANG_ID":177,
            "TEN_HIEN_TRANG":"Cho thuê",
            "KIEU_DU_LIEU_ID":2,
            "GIA_TRI_CHECKBOX":false,
            "BIEN_DONG_GUID":"' || bienDongGuid || '"},';
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
        bdKhacCuoi2014:=NULL;
    END;

    --lay bien dong + nguon von + hien trang
    FOR r_bd_cln IN (
      select *
      from bd_khac
      where bd_khac.ma_tai_san = PMA_TAI_SAN
        and  EXTRACT(
          year
          from bd_khac.NGAY_BIEN_DONG
        ) > 2014 --TO_CHAR(bd_khac.NGAY_BIEN_DONG, 'YYYYMMDD') >= '20150101'
      order by bd_khac.BD_KHAC_ID desc
    ) LOOP bienDongGuid := raw_to_guid(LPAD(r_bd_cln.BD_KHAC_ID, 32, '0'));
    BEGIN
    select gstg.* into gsLyDoBD
    from dm_lydo_tanggiam dmtg,
      gs_map_ly_do_bien_dong gstg
    where rownum = 1
      and dmtg.MA_LY_DO = r_bd_cln.MA_LY_DO
      and (
        gstg.MA_OLD = dmtg.MA_LY_DO
        or dmtg.TEN_LY_DO = gstg.TEN_OLD
      )
      and (
        gstg.LOAI_HINH_TAI_SAN_NEW = pLOAI_HINH_TAI_SAN
        or gstg.LOAI_HINH_TAI_SAN_NEW = 0
      );
    EXCEPTION
    WHEN NO_DATA_FOUND THEN gsLyDoBD := null;
    END;
    bienDongJson := bienDongJson ||
        '{"GUID":"' || bienDongGuid || '",
        "NGUYEN_GIA":' || rtrim(to_char((
          r_bd_cln.NGUYEN_GIA_NS + r_bd_cln.NGUYEN_GIA_KHAC
        ), 'FM9999999999999999999999990.99'), '.')||',
        "NGAY_BIEN_DONG":"' || r_bd_cln.NGAY_BIEN_DONG || '",
        "DON_VI_NHAN_DIEU_CHUYEN_MA":"' || r_bd_cln.MA_DONVI_NHAN_DIEU_CHUYEN || '",
        "LOAI_BIEN_DONG_ID":' || r_bd_cln.LOAI_BIEN_DONG ||',
        "LY_DO_BIEN_DONG_MA":"' || nvl(gsLyDoBD.MA_NEW, '973') || '",
        "LY_DO_BIEN_DONG_TEN":"' || nvl(gsLyDoBD.TEN_NEW, 'Khác') || '",
        "DON_VI_MA":"' || r_bd_cln.MA_DON_VI || '",
        "NGAY_SU_DUNG":"' || r_bd_cln.THOI_GIAN_SU_DUNG || '",
        "THONG_SO_KY_THUAT":"' || GS_STRING_TO_STRINGJSON(r_bd_cln.THONG_SO_KT) || '",
        "TRANG_THAI":' || nvl(r_bd_cln.DUYET_BIEN_DONG, 1) || ',
        "NGAY_DUYET":"' || r_bd_cln.NGAY_DUYET_BIEN_DONG || '",
        "GIA_TRI_CON_LAI":' || rtrim(to_char(r_bd_cln.GIA_TRI_CON_LAI, 'FM9999999999999999999999990.99'), '.')||',
        "HS_KHAC":"' || r_bd_cln.SO_QD || '"}';
    nguonVonJson := nguonVonJson ||
        '{"NGUON_VON_ID":1,
        "GIA_TRI":' || rtrim(to_char(r_bd_cln.NGUYEN_GIA_NS, 'FM9999999999999999999999990.99'), '.')||',
        "BIEN_DONG_GUID":"' || bienDongGuid || '"},
      
        {"NGUON_VON_ID":3,
        "GIA_TRI":' || rtrim(to_char(r_bd_cln.NGUYEN_GIA_KHAC, 'FM9999999999999999999999990.99'), '.')||',
        "BIEN_DONG_GUID":"' || bienDongGuid || '"},';
    hienTrangJson := hienTrangJson ||
        '{"HIEN_TRANG_ID":172,
        "TEN_HIEN_TRANG":"Quản lý nhà nước",
        "KIEU_DU_LIEU_ID":2,
        "GIA_TRI_CHECKBOX":' || (
          CASE
            WHEN r_bd_cln.HIEN_TRANG_SU_DUNG = '4' THEN 'true'
            ELSE 'false'
          end
        ) || ',
        "BIEN_DONG_GUID":"' || bienDongGuid || '"},
      
        {"HIEN_TRANG_ID":173,
        "TEN_HIEN_TRANG":"HĐSN-Không KD",
        "KIEU_DU_LIEU_ID":2,
        "NHOM_HIEN_TRANG_ID":1,
        "GIA_TRI_CHECKBOX":' || (
          CASE
            WHEN r_bd_cln.HIEN_TRANG_SU_DUNG = '4' THEN 'true'
            ELSE 'false'
          end
        ) || ',
        "BIEN_DONG_GUID":"' || bienDongGuid || '"},
      
        {"HIEN_TRANG_ID":174,
        "TEN_HIEN_TRANG":"HĐSN-Kinh doanh",
        "KIEU_DU_LIEU_ID":2,
        "NHOM_HIEN_TRANG_ID":1,
        "GIA_TRI_CHECKBOX":' || (
          CASE
            WHEN r_bd_cln.HIEN_TRANG_SU_DUNG = '4' THEN 'true'
            ELSE 'false'
          end
        ) || ',
        "BIEN_DONG_GUID":"' || bienDongGuid || '"},
      
        {"HIEN_TRANG_ID":175,
        "TEN_HIEN_TRANG":"Hoạt động khác",
        "KIEU_DU_LIEU_ID":2,
        "GIA_TRI_CHECKBOX":' || (
          CASE
            WHEN r_bd_cln.HIEN_TRANG_SU_DUNG = '4' THEN 'true'
            ELSE 'false'
          end
        ) || ',
        "BIEN_DONG_GUID":"' || bienDongGuid || '"},
      
        {"HIEN_TRANG_ID":176,
        "TEN_HIEN_TRANG":"Liên doanh, liên kết",
        "KIEU_DU_LIEU_ID":2,
        "GIA_TRI_CHECKBOX":false,
        "BIEN_DONG_GUID":"' || bienDongGuid || '"},
      
        {"HIEN_TRANG_ID":177,
        "TEN_HIEN_TRANG":"Cho thuê",
        "KIEU_DU_LIEU_ID":2,
        "GIA_TRI_CHECKBOX":false,
        "BIEN_DONG_GUID":"' || bienDongGuid || '"},';
    END LOOP;
    
    bienDongJson := replace('[' || bienDongJson || ']','":,','":null,');
    nguonVonJson := replace('[' || nguonVonJson || ']','":,','":null,');
    hienTrangJson := replace('[' || hienTrangJson || ']','":,','":null,');
--update lại temp đồng bộ
    

    
    select 
        gsmap.* into gsMapLts
    from
        gs_map_loai_tai_san gsmap, ts_tai_san ts,dm_loaitaisan dmlts
    where
        dmlts.loai_tai_san_id = ts.loai_tai_san_id
        and gsmap.ma_old = dmlts.ma_loai_tai_san
    and ts.ma_tai_san = PMA_TAI_SAN
        and gsmap.LOAI_HINH_TAI_SAN_ID = PLOAI_HINH_TAI_SAN
        and gsmap.CHE_DO_HAO_MON_ID = 1;
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
        (CASE 
            WHEN LOAI_BIEN_DONG = 5 AND DUYET_BIEN_DONG = 2 THEN 4
            ELSE TO_NUMBER(DUYET_BIEN_DONG) END) INTO trangThai
    FROM
        BD_KHAC
    WHERE
        DUYET_BIEN_DONG <> 0
        AND MA_TAI_SAN = PMA_TAI_SAN
        AND ROWNUM = 1
    ORDER BY NGAY_BIEN_DONG DESC;
    
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
        gstemp.NGUYEN_GIA_BAN_DAU = ( ts31122014.NGUYEN_GIA_NS + ts31122014.NGUYEN_GIA_KHAC + ts31122014.NGUYEN_GIA_ODA + ts31122014.NGUYEN_GIA_VIEN_TRO ),
        gstemp.ma_loai_tai_san = gsMapLts.ma_new,
        gstemp.loai_tai_san_ten = gsMapLts.ten_new,
        gstemp.trang_thai = trangThai
    WHERE gstemp.ma_tai_san = PMA_TAI_SAN;
    commit;
  RETURN 1;
END GS_UPDATE_TSCLN_32_TO_GSTEMP_DONG_BO;