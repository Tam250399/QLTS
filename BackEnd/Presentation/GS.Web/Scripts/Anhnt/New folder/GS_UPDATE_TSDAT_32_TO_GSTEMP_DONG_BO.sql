create or replace FUNCTION GS_UPDATE_TSDAT_32_TO_GSTEMP_DONG_BO 
(
  PMA_TAI_SAN IN VARCHAR2,
  pLOAI_HINH_TAI_SAN IN NUMBER
) RETURN NUMBER AS 
    nguyenGiaBanDau NUMBER;
    bienDongJson CLOB;
    nguonVonJson CLOB;
    hienTrangJson CLOB;
    trangThai NUMBER;
    dataJson CLOB;
    bienDongGuid VARCHAR2(36);
    ts31122014 kt_taisan_31122014 %RowType;
    bdDatCuoi2014 bd_dat %RowType;
    bdDatCuoi bd_dat %RowType;
    gsLyDoBD gs_map_ly_do_bien_dong %RowType;
    gsMapLts gs_map_loai_tai_san %RowType;
    tsDat TS_DAT %ROWTYPE;
BEGIN
--
    BEGIN
        SELECT
            * INTO tsDat
        FROM
            TS_DAT
        WHERE
            MA_TAI_SAN = PMA_TAI_SAN;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
        RETURN 0;
    END;
--
    hienTrangJson := '';
    bienDongJson := '';
    nguonVonJson := '';
    dataJson := '';

    BEGIN
        select * into ts31122014
        from kt_taisan_31122014
        where PMA_TAI_SAN = kt_taisan_31122014.ma_tai_san;
        nguyenGiaBanDau := ts31122014.NGUYEN_GIA_NS + ts31122014.NGUYEN_GIA_KHAC + ts31122014.NGUYEN_GIA_ODA + ts31122014.NGUYEN_GIA_VIEN_TRO;
        BEGIN
            select * into bdDatCuoi2014
            from bd_dat
            where EXTRACT(
                year
                from NGAY_BIEN_DONG
              ) < 2015
              and rownum = 1
              and DUYET_BIEN_DONG = 2
              AND MA_TAI_SAN = PMA_TAI_SAN
            order by NGAY_BIEN_DONG desc;
            
            BEGIN
                SELECT
                    * INTO gsMapLts
                FROM 
                    gs_map_loai_tai_san
                WHERE 
                    MA_OLD = bdDatCuoi2014.MA_LOAI_TAI_SAN
                    AND CHE_DO_HAO_MON_ID = 1;
            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    gsMapLts := NULL;
            END;
            bienDongGuid := '00000000-0000-0000-0000-000000000000';
            bienDongJson := bienDongJson ||
                '{"GUID":"' || bienDongGuid || '",
                "NGUYEN_GIA":' || (
                  ts31122014.NGUYEN_GIA_NS + ts31122014.NGUYEN_GIA_KHAC + ts31122014.NGUYEN_GIA_ODA + ts31122014.NGUYEN_GIA_VIEN_TRO
                )||',
                "NGAY_BIEN_DONG":"' || to_date('01-01-2015', 'DD-MM-YYYY')|| '",
                "NGAY_SU_DUNG":"' || to_date('01-01-2008', 'DD-MM-YYYY')|| '",
                "LOAI_BIEN_DONG_ID":1,
                "LOAI_TAI_SAN_MA":"'||gsMapLts.ma_new||'",
                "LY_DO_BIEN_DONG_MA":"001",
                "LY_DO_BIEN_DONG_TEN":"Đăng ký lần đầu",
                "DON_VI_MA":"' || ts31122014.MA_DON_VI|| '",
                "DIA_BAN_MA":"'|| bdDatCuoi2014.MA_DIA_BAN || '",
                "TRANG_THAI":2,
                "DAT_TONG_DIEN_TICH":' || ts31122014.DIEN_TICH || ',
                "NGAY_DUYET":"' || to_date('01-01-2015', 'DD-MM-YYYY')|| '",
                "HS_CNQSD_SO":"' || GS_STRING_TO_STRINGJSON(bdDatCuoi2014.CN_QUYEN_SD_DAT_SO)|| '",
                "HS_CNQSD_NGAY":"' || bdDatCuoi2014.CN_QUYEN_SD_DAT_NGAY|| '",
                "HS_QUYET_DINH_GIAO_SO":"' || GS_STRING_TO_STRINGJSON(bdDatCuoi2014.QUYET_DINH_GIAO_DAT_SO)|| '",
                "HS_QUYET_DINH_GIAO_NGAY":"' || bdDatCuoi2014.QUYET_DINH_GIAO_DAT_NGAY|| '",
                "HS_CHUYEN_NHUONG_SD_SO":"' || GS_STRING_TO_STRINGJSON(bdDatCuoi2014.HD_CHUYEN_NHUONG_SD_SO)|| '",
                "HS_CHUYEN_NHUONG_SD_NGAY":"' || bdDatCuoi2014.HD_CHUYEN_NHUONG_SD_NGAY|| '",
                "HS_QUYET_DINH_CHO_THUE_SO":"' || GS_STRING_TO_STRINGJSON(bdDatCuoi2014.HD_THUE_DAT_SO)|| '",
                "HS_QUYET_DINH_CHO_THUE_NGAY":"' || bdDatCuoi2014.HD_THUE_DAT_NGAY|| '",
                "HS_KHAC":"' || GS_STRING_TO_STRINGJSON(bdDatCuoi2014.GIAY_TO_KHAC) || '"},';
            nguonVonJson := nguonVonJson ||
                '{"NGUON_VON_ID":1,
                "GIA_TRI":' || rtrim(to_char((
                  ts31122014.NGUYEN_GIA_NS + ts31122014.NGUYEN_GIA_KHAC + ts31122014.NGUYEN_GIA_ODA + ts31122014.NGUYEN_GIA_VIEN_TRO
                ), 'FM9999999999999999999999990.99'), '.')||',
                "BIEN_DONG_GUID":"' || bienDongGuid || '"},';
            hienTrangJson := hienTrangJson ||
                '{"HIEN_TRANG_ID":72,
                "TEN_HIEN_TRANG":"Trụ sở làm việc",
                "KIEU_DU_LIEU_ID":1,
                "GIA_TRI_NUMBER":' || rtrim(to_char(ts31122014.LAM_TRU_SO_NN, 'FM9999999999999999999999990.99'), '.')||',
                "BIEN_DONG_GUID":"' || bienDongGuid || '"},
              
                {"HIEN_TRANG_ID":73,
                "TEN_HIEN_TRANG":"HĐSN-Không KD",
                "KIEU_DU_LIEU_ID":1,
                "GIA_TRI_NUMBER":' || rtrim(to_char(ts31122014.HD_SN, 'FM9999999999999999999999990.99'), '.')||',
                "BIEN_DONG_GUID":"' || bienDongGuid || '"},
              
                {"HIEN_TRANG_ID":75,
                "TEN_HIEN_TRANG":"HĐSN-KD",
                "KIEU_DU_LIEU_ID":1,
                "GIA_TRI_NUMBER":0,
                "BIEN_DONG_GUID":"' || bienDongGuid || '"},
              
                {"HIEN_TRANG_ID":78,
                "TEN_HIEN_TRANG":"HĐSN-Cho thuê",
                "KIEU_DU_LIEU_ID":1,
                "GIA_TRI_NUMBER":' || rtrim(to_char(ts31122014.CHO_THUE, 'FM9999999999999999999999990.99'), '.')||',
                "BIEN_DONG_GUID":"' || bienDongGuid || '"},
              
                {"HIEN_TRANG_ID":79,
                "TEN_HIEN_TRANG":"HĐSN-LDLK",
                "KIEU_DU_LIEU_ID":1,
                "GIA_TRI_NUMBER":0,
                "BIEN_DONG_GUID":"' || bienDongGuid || '"},
              
                {"HIEN_TRANG_ID":81,
                "TEN_HIEN_TRANG":"SD hỗn hợp",
                "KIEU_DU_LIEU_ID":1,
                "GIA_TRI_NUMBER":0,
                "BIEN_DONG_GUID":"' || bienDongGuid || '"},
              
                {"HIEN_TRANG_ID":132,
                "TEN_HIEN_TRANG":"Khác",
                "KIEU_DU_LIEU_ID":1,
                "GIA_TRI_NUMBER":' || rtrim(to_char((
                  ts31122014.DE_O + ts31122014.SD_KHAC + ts31122014.SX_KDDV + ts31122014.SX_KDDV_SAIPHEP
                ), 'FM9999999999999999999999990.99'), '.')||',
                "BIEN_DONG_GUID":"' || bienDongGuid || '"},';
        EXCEPTION 
            WHEN NO_DATA_FOUND THEN
            bdDatCuoi2014:=NULL;
        END;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
        ts31122014:=NULL;
        BEGIN
            SELECT bd_dat.GIA_TRI INTO nguyenGiaBanDau
            FROM bd_dat
            WHERE bd_dat.ma_tai_san = PMA_TAI_SAN
              AND rownum = 1
            order by bd_dat.NGAY_BIEN_DONG;
        EXCEPTION
            WHEN NO_DATA_FOUND THEN nguyenGiaBanDau := 0;
        END;
    END;
    
    

--lay bien dong + nguon von + hien trang
FOR r_bd_dat IN (
  select *
  from bd_dat
  where bd_dat.ma_tai_san = PMA_TAI_SAN
    and 1 = (CASE
                WHEN ts31122014.MA_TAI_SAN IS NULL
                      OR (ts31122014.MA_TAI_SAN IS NOT NULL 
                            AND EXTRACT(
                                          year
                                          from bd_dat.NGAY_BIEN_DONG
                                        ) > 2014) THEN 1
                ELSE 0 END)  -- TO_CHAR(bd_dat.NGAY_BIEN_DONG, 'YYYYMMDD') >= '20150101'
  order by bd_dat.bd_dat_id desc
) LOOP 
bienDongGuid := raw_to_guid(LPAD(r_bd_dat.BD_DAT_ID, 32, '0'));
BEGIN
select gstg.* into gsLyDoBD
from dm_lydo_tanggiam dmtg,
  gs_map_ly_do_bien_dong gstg
where rownum = 1
  and dmtg.MA_LY_DO = r_bd_dat.MA_LY_DO
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
BEGIN
    SELECT
        * INTO gsMapLts
    FROM 
        gs_map_loai_tai_san
    WHERE 
        MA_OLD = r_bd_dat.MA_LOAI_TAI_SAN
        AND CHE_DO_HAO_MON_ID = 1;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        gsMapLts := NULL;
END;
bienDongJson := bienDongJson ||
    '{"GUID":"' || bienDongGuid || '",
    "NGUYEN_GIA":' || rtrim(to_char(r_bd_dat.GIA_TRI, 'FM9999999999999999999999990.99'), '.')||',
    "NGAY_BIEN_DONG":"' || r_bd_dat.NGAY_BIEN_DONG || '",
    "NGAY_SU_DUNG":"' || to_date('01-01-2008', 'DD-MM-YYYY') ||'",
    "LOAI_BIEN_DONG_ID":' || r_bd_dat.LOAI_BIEN_DONG ||',
    "LOAI_TAI_SAN_MA":"'||gsMapLts.ma_new||'",
    "LY_DO_BIEN_DONG_MA":"' || gsLyDoBD.MA_NEW || '",
    "LY_DO_BIEN_DONG_TEN":"' || GS_STRING_TO_STRINGJSON(gsLyDoBD.TEN_NEW) || '",
    "DON_VI_MA":"' || r_bd_dat.MA_DON_VI || '",
    "DIA_BAN_MA":"'|| r_bd_dat.MA_DIA_BAN || '",
    "DON_VI_NHAN_DIEU_CHUYEN_MA":"' || r_bd_dat.MA_DONVI_NHAN_DIEU_CHUYEN || '",
    "QUYET_DINH_SO":"' || GS_STRING_TO_STRINGJSON(r_bd_dat.QUYET_DINH_GIAO_DAT_SO) || '",
    "QUYET_DINH_NGAY":"' || r_bd_dat.QUYET_DINH_GIAO_DAT_NGAY || '",
    "TRANG_THAI":' || nvl(r_bd_dat.DUYET_BIEN_DONG, 1) || ',
    "DAT_TONG_DIEN_TICH":' || rtrim(to_char(r_bd_dat.DIEN_TICH, 'FM9999999999999999999999990.99'), '.')||',
    "NGAY_DUYET":"' || r_bd_dat.NGAY_DUYET_BIEN_DONG || '",
    "HS_CNQSD_SO":"' || GS_STRING_TO_STRINGJSON(r_bd_dat.CN_QUYEN_SD_DAT_SO) || '",
    "HS_CNQSD_NGAY":"' || r_bd_dat.CN_QUYEN_SD_DAT_NGAY || '",
    "HS_QUYET_DINH_GIAO_SO":"' || GS_STRING_TO_STRINGJSON(r_bd_dat.QUYET_DINH_GIAO_DAT_SO) || '",
    "HS_QUYET_DINH_GIAO_NGAY":"' || r_bd_dat.QUYET_DINH_GIAO_DAT_NGAY || '",
    "HS_CHUYEN_NHUONG_SD_SO":"' || GS_STRING_TO_STRINGJSON(r_bd_dat.HD_CHUYEN_NHUONG_SD_SO) || '",
    "HS_CHUYEN_NHUONG_SD_NGAY":"' || r_bd_dat.HD_CHUYEN_NHUONG_SD_NGAY || '",
    "HS_QUYET_DINH_CHO_THUE_SO":"' || GS_STRING_TO_STRINGJSON(r_bd_dat.HD_THUE_DAT_SO) || '",
    "HS_QUYET_DINH_CHO_THUE_NGAY":"' || r_bd_dat.HD_THUE_DAT_NGAY || '",
    "HS_KHAC":"' || GS_STRING_TO_STRINGJSON(r_bd_dat.GIAY_TO_KHAC) || '"},';
nguonVonJson := nguonVonJson ||
    '{"NGUON_VON_ID":1,
    "GIA_TRI":' || rtrim(to_char(r_bd_dat.GIA_TRI, 'FM9999999999999999999999990.99'), '.')||',
    "BIEN_DONG_GUID":"' || bienDongGuid || '"},';
hienTrangJson := hienTrangJson ||
    '{"HIEN_TRANG_ID":72,
    "TEN_HIEN_TRANG":"Trụ sở làm việc",
    "KIEU_DU_LIEU_ID":1,
    "GIA_TRI_NUMBER":' || rtrim(to_char(r_bd_dat.LAM_TRU_SO_NN, 'FM9999999999999999999999990.99'), '.')||',
    "BIEN_DONG_GUID":"' || bienDongGuid || '"},
  
    {"HIEN_TRANG_ID":73,
    "TEN_HIEN_TRANG":"HĐSN-Không KD",
    "KIEU_DU_LIEU_ID":1,
    "GIA_TRI_NUMBER":' || rtrim(to_char(r_bd_dat.LAM_CO_SO_HD_SN, 'FM9999999999999999999999990.99'), '.')||',
    "BIEN_DONG_GUID":"' || bienDongGuid || '"},
  
    {"HIEN_TRANG_ID":75,
    "TEN_HIEN_TRANG":"HĐSN-KD",
    "KIEU_DU_LIEU_ID":1,
    "GIA_TRI_NUMBER":0,
    "BIEN_DONG_GUID":"' || bienDongGuid || '"},
  
    {"HIEN_TRANG_ID":78,
    "TEN_HIEN_TRANG":"HĐSN-Cho thuê",
    "KIEU_DU_LIEU_ID":1,
    "GIA_TRI_NUMBER":' || rtrim(to_char(r_bd_dat.KHAC_CHO_THUE, 'FM9999999999999999999999990.99'), '.')||',
    "BIEN_DONG_GUID":"' || bienDongGuid || '"},
  
    {"HIEN_TRANG_ID":79,
    "TEN_HIEN_TRANG":"HĐSN-LDLK",
    "KIEU_DU_LIEU_ID":1,
    "GIA_TRI_NUMBER":0,
    "BIEN_DONG_GUID":"' || bienDongGuid || '"},
  
    {"HIEN_TRANG_ID":81,
    "TEN_HIEN_TRANG":"SD hỗn hợp",
    "KIEU_DU_LIEU_ID":1,
    "GIA_TRI_NUMBER":0,
    "BIEN_DONG_GUID":"' || bienDongGuid || '"},
  
    {"HIEN_TRANG_ID":132,
    "TEN_HIEN_TRANG":"Khác",
    "KIEU_DU_LIEU_ID":1,
    "GIA_TRI_NUMBER":' || rtrim(to_char((
      r_bd_dat.KHAC_LAM_NHA_O + r_bd_dat.KHAC_BI_LAN_CHIEM + r_bd_dat.KHAC_BO_TRONG + r_bd_dat.KHAC
    ), 'FM9999999999999999999999990.99'), '.')||',
    "BIEN_DONG_GUID":"' || bienDongGuid || '"},';
END LOOP;
--lay du lieu json cua tai san
    BEGIN
        SELECT
            * INTO bdDatCuoi
        FROM
            BD_DAT
        WHERE
            ROWNUM = 1
            AND MA_TAI_SAN = PMA_TAI_SAN
            AND DUYET_BIEN_DONG = 2
        ORDER BY NGAY_BIEN_DONG DESC;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
        bdDatCuoi:=NULL;
    END;
    
    BEGIN
        SELECT 
            TO_CLOB(
            '{"DIA_CHI":"' || GS_STRING_TO_STRINGJSON(NVL(bdDatCuoi.dia_chi,TS.dia_chi)) || '",
            "DIA_BAN_MA":"' || NVL(bdDatCuoi.ma_dia_ban,TS.ma_dia_ban) || '",
            "DIEN_TICH":' || rtrim(to_char((CASE WHEN bdDatCuoi.dien_tich > 0 THEN bdDatCuoi.dien_tich ELSE TS.dien_tich END), 'FM9999999999999999999999990.99'), '.') || ',
            "HS_QUYET_DINH_GIAO_SO":"' || GS_STRING_TO_STRINGJSON(NVL(bdDatCuoi.QUYET_DINH_GIAO_DAT_SO,TS.QUYET_DINH_GIAO_DAT_SO)) || '",
            "HS_QUYET_DINH_GIAO_NGAY":"' || NVL(bdDatCuoi.QUYET_DINH_GIAO_DAT_NGAY,TS.QUYET_DINH_GIAO_DAT_NGAY) || '",
            "HS_QUYET_DINH_CHO_THUE_SO":"' || GS_STRING_TO_STRINGJSON(NVL(bdDatCuoi.HD_THUE_DAT_SO,TS.HD_THUE_DAT_SO)) || '",
            "HS_QUYET_DINH_CHO_THUE_NGAY":"' || NVL(bdDatCuoi.HD_THUE_DAT_NGAY,TS.HD_THUE_DAT_NGAY) || '",
            "HS_CHUYEN_NHUONG_SD_SO":"' || GS_STRING_TO_STRINGJSON(NVL(bdDatCuoi.HD_CHUYEN_NHUONG_SD_SO,TS.HD_CHUYEN_NHUONG_SD_SO)) || '",
            "HS_CHUYEN_NHUONG_SD_NGAY":"' || NVL(bdDatCuoi.HD_CHUYEN_NHUONG_SD_NGAY,TS.HD_CHUYEN_NHUONG_SD_NGAY) || '",
            "HS_KHAC":"' || GS_STRING_TO_STRINGJSON(NVL(bdDatCuoi.KHAC,TS.KHAC)) || '",
            "HS_CNQSD_SO":"' || GS_STRING_TO_STRINGJSON(NVL(bdDatCuoi.CN_QUYEN_SD_DAT_SO,TS.CN_QUYEN_SD_DAT_SO)) || '",
            "HS_CNQSD_NGAY":"' || NVL(bdDatCuoi.CN_QUYEN_SD_DAT_NGAY,TS.CN_QUYEN_SD_DAT_NGAY) || '"}') INTO dataJson
        FROM
            TS_DAT TS
        WHERE
            MA_TAI_SAN = PMA_TAI_SAN;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN dataJson := NULL;
    END;
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
    --DBMS_OUTPUT.PUT_LINE(PMA_TAI_SAN);    
    BEGIN 
      SELECT
          (CASE 
              WHEN LOAI_BIEN_DONG = 5 AND DUYET_BIEN_DONG = 2 THEN 4
              ELSE TO_NUMBER(DUYET_BIEN_DONG) END) INTO trangThai
      FROM
          BD_DAT
      WHERE
          DUYET_BIEN_DONG <> 0
          AND MA_TAI_SAN = PMA_TAI_SAN
          AND ROWNUM = 1
      ORDER BY NGAY_BIEN_DONG DESC;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
      BEGIN 
        SELECT
            2 INTO trangThai
        FROM
            TS_DULIEU_TRAODOI
        WHERE
            MA_TAI_SAN_PHAT_SINH = PMA_TAI_SAN
            AND ROWNUM = 1;
      EXCEPTION
        WHEN NO_DATA_FOUND THEN
        trangThai:=NULL;
      END;
    END;
   
    update gs_temp_dong_bo_du_lieu gstemp
    set gstemp.tai_san_json = replace(dataJson,'":,','":null,'),
      gstemp.lst_bien_dong_json = bienDongJson,
      gstemp.lst_nguon_von_json = nguonVonJson,
      gstemp.lst_hien_trang_json = hienTrangJson,
      gstemp.NGAY_SU_DUNG = to_date('01-01-2008', 'DD-MM-YYYY'),
      gstemp.NGUYEN_GIA_BAN_DAU = nguyenGiaBanDau,
      gstemp.ma_loai_tai_san = gsMapLts.ma_new,
      gstemp.loai_tai_san_ten = gsMapLts.ten_new,
        gstemp.trang_thai = NVL(trangThai,gstemp.trang_thai)
    where gstemp.ma_tai_san = PMA_TAI_SAN;
    commit;
  RETURN 1;
END GS_UPDATE_TSDAT_32_TO_GSTEMP_DONG_BO;