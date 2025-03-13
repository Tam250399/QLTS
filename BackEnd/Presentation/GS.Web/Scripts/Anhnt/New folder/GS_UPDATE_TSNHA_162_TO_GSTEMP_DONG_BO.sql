create or replace FUNCTION GS_UPDATE_TSNHA_162_TO_GSTEMP_DONG_BO 
(
  PMA_TAI_SAN IN VARCHAR2 
, PLOAI_HINH_TAI_SAN IN NUMBER 
) RETURN NUMBER AS 
    biendongJsonJson CLOB;
    nguonVonJson CLOB;
    hienTrangJson CLOB;
    dataJson CLOB;
    biendongJsonGuid VARCHAR2(36);
    bdNhaCuoi bd_nha %RowType;
    TSNHA TS_NHA %ROWTYPE;
    nguyenGiaBanDau NUMBER;
    IS_DAT_AO VARCHAR(1);
    gsLyDoBD gs_map_ly_do_bien_dong %RowType;
    diaChi VARCHAR2(500);
    gsMapLts gs_map_loai_tai_san %RowType;
BEGIN
--
    BEGIN
        SELECT
            * INTO TSNHA
        FROM 
            TS_NHA
        WHERE
            MA_TAI_SAN = PMA_TAI_SAN;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            RETURN 0;
    END;
--
    
    --lay bien dong + nguon von + hien trang
    hienTrangJson := '';
    biendongJsonJson := '';
    nguonVonJson := '';
    dataJson := '';
    FOR r_bd_nha IN (
      SELECT *
      FROM bd_nha
      WHERE bd_nha.ma_tai_san = PMA_TAI_SAN
      order by bd_nha.bd_nha_id desc
    ) LOOP biendongJsonGuid := raw_to_guid(LPAD(r_bd_nha.BD_NHA_ID, 32, '0'));
    BEGIN
    SELECT gstg.* INTO gsLyDoBD
    FROM dm_lydo_tanggiam dmtg,
      gs_map_ly_do_bien_dong gstg
    WHERE rownum = 1
      AND dmtg.MA_LY_DO = r_bd_nha.MA_LY_DO
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
    biendongJsonJson := biendongJsonJson || 
        '{"GUID":"' || biendongJsonGuid || '",
        "NGUYEN_GIA":' || rtrim(to_char(r_bd_nha.NGUYEN_GIA_NS + r_bd_nha.NGUYEN_GIA_KHAC, 'FM9999999999999999999999990.99'),'.') || ',
        "NGAY_BIEN_DONG":"'|| r_bd_nha.NGAY_BIEN_DONG || '",
        "NGAY_SU_DUNG":"' || r_bd_nha.THOI_GIAN_SU_DUNG || '",
        "DON_VI_NHAN_DIEU_CHUYEN_MA":"' || r_bd_nha.MA_DONVI_NHAN_DIEU_CHUYEN || '",
        "LOAI_BIEN_DONG_ID":' || r_bd_nha.LOAI_BIEN_DONG || ',
        "LY_DO_BIEN_DONG_MA":"' || nvl(gsLyDoBD.MA_NEW , '041') || '",
        "LY_DO_BIEN_DONG_TEN":"' || GS_STRING_TO_STRINGJSON(nvl(gsLyDoBD.TEN_NEW , 'Khác')) || '",
        "DON_VI_MA":"' || r_bd_nha.MA_DON_VI || '",
        "NHA_SO_TANG":' || r_bd_nha.SO_TANG || ',
        "NHA_TONG_DIEN_TICH_XD":' || rtrim(to_char(r_bd_nha.TONG_DIEN_TICH_SAN, 'FM9999999999999999999999990.99'), '.') || ',
        "NHA_NAM_XAY_DUNG":' || r_bd_nha.NAM_XAY_DUNG || ',
        "NGAY_DUYET":"' || r_bd_nha.NGAY_DUYET_BIEN_DONG || '",
        "TRANG_THAI":' || nvl(r_bd_nha.DUYET_BIEN_DONG, 1) || ',
        "GIA_TRI_CON_LAI":' || rtrim(to_char(r_bd_nha.GIA_TRI_CON_LAI, 'FM9999999999999999999999990.99'), '.') || ',
        "KH_GIA_TINH_KHAU_HAO":' || rtrim(to_char((
          r_bd_nha.KH_NGUYEN_GIA_NS + r_bd_nha.KH_NGUYEN_GIA_KHAC + r_bd_nha.KH_NGUYEN_GIA_ODA + r_bd_nha.KH_NGUYEN_GIA_VIEN_TRO
        ), 'FM9999999999999999999999990.99'), '.') || ',
        "KH_GIA_TRI_CON_LAI":' || r_bd_nha.KH_GIA_TRI_CON_LAI || ',
        "KH_THANG_CON_LAI":' || r_bd_nha.SO_THANG_CON_SU_DUNG || ',
        "HS_KHAC":"' || GS_STRING_TO_STRINGJSON(r_bd_nha.SO_QD) || '"},';
    nguonVonJson := nguonVonJson||
        '{"NGUON_VON_ID":1, "GIA_TRI":' || r_bd_nha.NGUYEN_GIA_NS || ', "BIEN_DONG_GUID":"' || biendongJsonGuid || '"},
        {"NGUON_VON_ID":3, "GIA_TRI":' || r_bd_nha.NGUYEN_GIA_KHAC|| ', "BIEN_DONG_GUID":"' || biendongJsonGuid|| '"},';
    hienTrangJson := hienTrangJson ||
        '{"HIEN_TRANG_ID":82,
        "TEN_HIEN_TRANG":"Trụ sở làm việc",
        "KIEU_DU_LIEU_ID":1,
        "GIA_TRI_NUMBER":' || rtrim(to_char(r_bd_nha.LAM_TRU_SO_LV, 'FM9999999999999999999999990.99'), '.') || ',
        "BIEN_DONG_GUID":"' || biendongJsonGuid || '"},
        {"HIEN_TRANG_ID": 83,
        "TEN_HIEN_TRANG":"HĐSN-Không KD",
        "KIEU_DU_LIEU_ID":1,
        "GIA_TRI_NUMBER":' ||  rtrim(to_char(r_bd_nha.HD_SN, 'FM9999999999999999999999990.99'), '.') || ',
        "BIEN_DONG_GUID":"' || biendongJsonGuid || '"},
        {"HIEN_TRANG_ID": 85,
        "TEN_HIEN_TRANG":"HĐSN-Cho thuê",
        "KIEU_DU_LIEU_ID":1,
        "GIA_TRI_NUMBER":' ||  rtrim(to_char(r_bd_nha.CHO_THUE, 'FM9999999999999999999999990.99'), '.') || ',
        "BIEN_DONG_GUID":"' || biendongJsonGuid || '"},
        {"HIEN_TRANG_ID": 88,
        "TEN_HIEN_TRANG":"Khác",
        "KIEU_DU_LIEU_ID":1,
        "GIA_TRI_NUMBER":' ||  rtrim(to_char((r_bd_nha.DE_O + r_bd_nha.SD_KHAC), 'FM9999999999999999999999990.99'), '.') || ',
        "BIEN_DONG_GUID":"' || biendongJsonGuid || '"},
        {"HIEN_TRANG_ID": 84,
        "TEN_HIEN_TRANG":"HĐSN-KD",
        "KIEU_DU_LIEU_ID":1,
        "GIA_TRI_NUMBER": 0,
        "BIEN_DONG_GUID":"' || biendongJsonGuid || '"},
        {"HIEN_TRANG_ID": 86,
        "TEN_HIEN_TRANG":"HĐSN-LDLK",
        "KIEU_DU_LIEU_ID":1,
        "GIA_TRI_NUMBER": 0,
        "BIEN_DONG_GUID":"' || biendongJsonGuid || '"},
        {"HIEN_TRANG_ID": 87,
        "TEN_HIEN_TRANG":"SD hỗn hợp",
        "KIEU_DU_LIEU_ID":1,
        "GIA_TRI_NUMBER": 0,
        "BIEN_DONG_GUID":"' || biendongJsonGuid || '"},';
    END LOOP;
    --lay du lieu json cua tai san
    BEGIN
        SELECT tsdat.DIA_CHI INTO diaChi
        FROM ts_dat tsdat
        WHERE tsdat.ma_tai_san = PMA_TAI_SAN;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
        BEGIN
            SELECT dv.DIA_CHI INTO diaChi
            FROM dm_donvi dv,
              gs_temp_dong_bo_du_lieu gstemp
            WHERE dv.ma_don_vi = gstemp.ma_don_vi
              AND gstemp.ma_tai_san = PMA_TAI_SAN;
        EXCEPTION
            WHEN NO_DATA_FOUND THEN
            diaChi := NULL;
        END;
    END;
    BEGIN
        SELECT
            * INTO bdNhaCuoi
        FROM
            BD_NHA
        WHERE
            ROWNUM = 1
            AND MA_TAI_SAN = PMA_TAI_SAN
            AND DUYET_BIEN_DONG = 2
        ORDER BY NGAY_BIEN_DONG DESC;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN 
        bdNhaCuoi:=NULL;
    END;
    BEGIN
        BEGIN
            SELECT tsdat.LA_DAT_AO INTO IS_DAT_AO
              FROM ts_dat tsdat,TS_NHA TS
              WHERE tsdat.ma_tai_san = TS.MA_TAI_SAN_DAT
              AND TS.MA_TAI_SAN = PMA_TAI_SAN
              AND tsdat.LA_DAT_AO = '1';
        EXCEPTION
            WHEN NO_DATA_FOUND THEN
            IS_DAT_AO:='0';
        END;
        SELECT TO_CLOB(
            '{"TAI_SAN_DAT_MA":"' || (CASE WHEN IS_DAT_AO='1' THEN '' ELSE NVL(bdNhaCuoi.MA_TAI_SAN_DAT,ts.MA_TAI_SAN_DAT) END) || '",
            "DIA_CHI":"' || GS_STRING_TO_STRINGJSON(diaChi)|| '",
            "NHA_SO_TANG":' || NVL(bdNhaCuoi.SO_TANG,ts.SO_TANG) || ',
            "NAM_XAY_DUNG":' || NVL(bdNhaCuoi.NAM_XAY_DUNG,ts.NAM_XAY_DUNG) || ',
            "DIEN_TICH_SAN_XAY_DUNG":' || rtrim(to_char(NVL(bdNhaCuoi.TONG_DIEN_TICH_SAN,ts.TONG_DIEN_TICH_SAN) , 'FM9999999999999999999999990.99'), '.')|| ',
            "NGAY_SU_DUNG":"' || NVL(bdNhaCuoi.THOI_GIAN_SU_DUNG,ts.THOI_GIAN_SU_DUNG) || '"}'
          ) INTO dataJson
        FROM ts_nha ts
        WHERE ts.ma_tai_san = PMA_TAI_SAN;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN 
        dataJson := NULL;
    END;
    biendongJsonJson := REPLACE('[' || biendongJsonJson || ']','":,','":null,');
    nguonVonJson := REPLACE('[' || nguonVonJson || ']','":,','":null,');
    hienTrangJson := REPLACE('[' || hienTrangJson || ']','":,','":null,');
    --UPDATE lại temp đồng bộ

        
    BEGIN
        SELECT (
            bd_nha.NGUYEN_GIA_NS + bd_nha.NGUYEN_GIA_KHAC + bd_nha.NGUYEN_GIA_ODA + bd_nha.NGUYEN_GIA_VIEN_TRO
          ) INTO nguyenGiaBanDau
        FROM bd_nha
        WHERE bd_nha.ma_tai_san = PMA_TAI_SAN
          AND rownum = 1
        order by bd_nha.NGAY_BIEN_DONG;
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
      gstemp.lst_bien_dong_json = biendongJsonJson,
      gstemp.lst_nguon_von_json = nguonVonJson,
      gstemp.lst_hien_trang_json = hienTrangJson,
      gstemp.nguyen_gia_ban_dau = nguyenGiaBanDau,
      gstemp.ngay_su_dung = NVL(bdNhaCuoi.thoi_gian_su_dung,TSNHA.thoi_gian_su_dung),
      gstemp.ma_loai_tai_san = gsMapLts.ma_new,
        gstemp.loai_tai_san_ten = gsMapLts.ten_new
    WHERE gstemp.ma_tai_san = PMA_TAI_SAN;
    commit;
  RETURN 0;
END GS_UPDATE_TSNHA_162_TO_GSTEMP_DONG_BO;