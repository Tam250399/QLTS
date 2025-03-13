create or replace PROCEDURE INSERT_TO_TEMP_DONG_BO (
    pLOAI_HINH_TAI_SAN IN NUMBER DEFAULT 9,
    pNAM IN NUMBER DEFAULT 0,
    pCHE_DO_HAO_MON IN NUMBER DEFAULT 2,
    pMA_DON_VI IN VARCHAR2 DEFAULT NULL,
    pINSERT IN NUMBER DEFAULT 1
  ) AS 
    totalRecord NUMBER;
    bienDongJson CLOB;
    nguonVonJson CLOB;
    hienTrangJson CLOB;
    dataJson CLOB;
    bienDongGuid VARCHAR2(36);
    nguyenGiaBanDau NUMBER;
    gsLyDoBD gs_map_ly_do_bien_dong %RowType;
    
    ma_nhan_xe varchar2(50);
    ma_chuc_danh varchar2(50);
    diaChi VARCHAR2(500);
    --
    fn NUMBER;
    TYPE TSCurTyp  IS REF CURSOR;
    v_ts_cursor TSCurTyp;
    ma_tai_san VARCHAR2(50);
    v_stmt_str VARCHAR2(4000);
BEGIN 
DBMS_OUTPUT.ENABLE (buffer_size => NULL);
DBMS_OUTPUT.PUT_LINE('pLOAI_HINH_TAI_SAN=' || pLOAI_HINH_TAI_SAN);
DBMS_OUTPUT.PUT_LINE(TO_CHAR(CURRENT_DATE, 'DD-MON-YYYY HH:MI:SS'));
--delete FROM gs_temp_dong_bo_du_lieu;
IF pINSERT = 1 THEN
    v_stmt_str:='INSERT INTO gs_temp_dong_bo_du_lieu (
        ma_tai_san,
        ten_tai_san,
        ma_don_vi,
        ngay_duyet,
        trang_thai,
        loai_hinh_tai_san_id,
        che_do_hao_mon_id,
        MA_NHOM_DON_VI,
        nam
      )
    SELECT ts.ma_tai_san,
      ts.ten_tai_san,
      ts.ma_don_vi,
      ts.ngay_duyet,
      ts.trang_thai,
      '||pLOAI_HINH_TAI_SAN||',
      2,
      (
        SELECT mn.ma_nhom
        FROM dm_nhom_donvi mn,
          dm_donvi dv
        WHERE rownum = 1
          AND mn.nhom_donvi_id = dv.nhom_donvi_id
          AND dv.ma_don_vi = ts.ma_don_vi
      ),
      EXTRACT(
        year
        FROM ts.ngay_duyet
      )
    FROM ts_tai_san ts
      LEFT JOIN dm_loaitaisan lts ON ts.loai_tai_san_id = lts.loai_tai_san_id
    WHERE ts.ma_tai_san NOT IN (
        SELECT ma_tai_san
        FROM ts_duoi_500
      )
      AND ts.ma_tai_san NOT IN (
        SELECT ma_tai_san
        FROM kt_taisan_31122014
      )
      AND ts.ma_tai_san NOT IN (
        SELECT gs_temp_dong_bo_du_lieu.ma_tai_san
        FROM gs_temp_dong_bo_du_lieu
      )
      AND (ts.trang_thai is null or ts.trang_thai <> ''0'')
      AND lts.che_do_hao_mon_id = 2
      AND lts.ma_loai_tai_san in (
      SELECT 
        GSMAP.MA_OLD
      FROM
        GS_MAP_LOAI_TAI_SAN GSMAP
      WHERE 
        GSMAP.CHE_DO_HAO_MON_ID = 2
        AND GSMAP.LOAI_HINH_TAI_SAN_ID = '||pLOAI_HINH_TAI_SAN||'
        '||
          (CASE
              WHEN pLOAI_HINH_TAI_SAN = 1 THEN ' AND 1 = (CASE WHEN ts.ma_tai_san in (
                                                          SELECT tsdat.ma_tai_san
                                                          FROM ts_dat tsdat
                                                          WHERE tsdat.LA_DAT_AO = ''1''
                                                        ) THEN 0 ELSE 1 END)' ELSE '' END)||')';
IF pMA_DON_VI IS NOT NULL THEN
    v_stmt_str:=v_stmt_str||' AND 1 = (CASE WHEN ts.ma_don_vi IN (SELECT MA_DON_VI
                                                                  FROM DM_DONVI
                                                                  WHERE MA_BO = '''||pMA_DON_VI||''') THEN 1
                                            ELSE 0 END)';
  END IF;

  IF pNam > 0 THEN 
    v_stmt_str:=v_stmt_str|| ' AND EXTRACT(YEAR FROM ts.ngay_duyet) = ' || pNam;
  END IF;
  --DBMS_OUTPUT.PUT_LINE(v_stmt_str);
  EXECUTE IMMEDIATE v_stmt_str;
    commit;
ELSE
    SELECT count(*) INTO totalRecord
    FROM gs_temp_dong_bo_du_lieu gstemp
    WHERE gstemp.che_do_hao_mon_id = pCHE_DO_HAO_MON
      AND gstemp.loai_hinh_tai_san_id = pLOAI_HINH_TAI_SAN AND gstemp.tai_san_json is null
      AND is_32 = 0
      AND 1 = (
        CASE
          WHEN pMA_DON_VI is null
          or gstemp.ma_don_vi = pMA_DON_VI THEN 1
          ELSE 0
        END
      )
      AND 1 = (
        CASE
          WHEN pNam <= 0 THEN 1
          ELSE (
            CASE
              WHEN gstemp.nam = pNAM THEN 1
              ELSE 0
            END
          )
        END
      );
    DBMS_OUTPUT.PUT_LINE('totalRecord=' || totalRecord);
    DBMS_OUTPUT.PUT_LINE(TO_CHAR(CURRENT_DATE, 'DD-MON-YYYY HH:MI:SS'));
    v_stmt_str:='SELECT gstemp.ma_tai_san
      FROM gs_temp_dong_bo_du_lieu gstemp
      WHERE gstemp.che_do_hao_mon_id = '||pCHE_DO_HAO_MON||'
        AND gstemp.loai_hinh_tai_san_id = '||pLOAI_HINH_TAI_SAN||' AND gstemp.tai_san_json is null
        AND is_32 = 0';
    IF pMA_DON_VI IS NOT NULL THEN
        v_stmt_str:=v_stmt_str||' AND 1 = (CASE WHEN gstemp.MA_DON_VI IN (SELECT MA_DON_VI
                                                                  FROM DM_DONVI
                                                                  WHERE MA_BO = '''||pMA_DON_VI||''') THEN 1
                                            ELSE 0 END)';
    END IF;

    IF pNam > 0 THEN
    v_stmt_str:=v_stmt_str||' AND gstemp.nam = ' || pNam;
    END IF;
      
    IF pLOAI_HINH_TAI_SAN = 1 THEN --TsDat
        OPEN v_ts_cursor FOR v_stmt_str;
        LOOP
          FETCH v_ts_cursor INTO ma_tai_san;
          EXIT WHEN v_ts_cursor%NOTFOUND;
          fn:=GS_UPDATE_TSDAT_162_TO_GSTEMP_DONG_BO(ma_tai_san,pLOAI_HINH_TAI_SAN);
        END LOOP;
    ELSIF pLOAI_HINH_TAI_SAN = 2 THEN --TsNha
        OPEN v_ts_cursor FOR v_stmt_str;
        LOOP
          FETCH v_ts_cursor INTO ma_tai_san;
          EXIT WHEN v_ts_cursor%NOTFOUND;
          fn:=GS_UPDATE_TSNHA_162_TO_GSTEMP_DONG_BO(ma_tai_san,pLOAI_HINH_TAI_SAN);
        END LOOP;
    ELSIF pLOAI_HINH_TAI_SAN = 3 THEN --tsvkt
        OPEN v_ts_cursor FOR v_stmt_str;
            LOOP
              FETCH v_ts_cursor INTO ma_tai_san;
              EXIT WHEN v_ts_cursor%NOTFOUND;
              fn:=GS_UPDATE_TSVKT_162_TO_GSTEMP_DONG_BO(ma_tai_san,pLOAI_HINH_TAI_SAN);
              
        END LOOP;
    ELSIF pLOAI_HINH_TAI_SAN = 4 THEN --TsOto
        OPEN v_ts_cursor FOR v_stmt_str;
            LOOP
              FETCH v_ts_cursor INTO ma_tai_san;
              EXIT WHEN v_ts_cursor%NOTFOUND;
              fn:=GS_UPDATE_TSOTO_162_TO_GSTEMP_DONG_BO(ma_tai_san,pLOAI_HINH_TAI_SAN);
    END LOOP;
   
    ELSIF pLOAI_HINH_TAI_SAN = 5 THEN --TsPTK
        OPEN v_ts_cursor FOR v_stmt_str;
            LOOP
              FETCH v_ts_cursor INTO ma_tai_san;
              EXIT WHEN v_ts_cursor%NOTFOUND;
              fn:=GS_UPDATE_TSPTK_162_TO_GSTEMP_DONG_BO(ma_tai_san,pLOAI_HINH_TAI_SAN);
              
    END LOOP;
    ELSIF pLOAI_HINH_TAI_SAN = 7 THEN 
        OPEN v_ts_cursor FOR v_stmt_str;
            LOOP
              FETCH v_ts_cursor INTO ma_tai_san;
              EXIT WHEN v_ts_cursor%NOTFOUND;
              fn:=GS_UPDATE_TSCLN_162_TO_GSTEMP_DONG_BO(ma_tai_san,pLOAI_HINH_TAI_SAN);
        END LOOP;
    ELSE 
        OPEN v_ts_cursor FOR v_stmt_str;
            LOOP
              FETCH v_ts_cursor INTO ma_tai_san;
              EXIT WHEN v_ts_cursor%NOTFOUND;
              fn:=GS_UPDATE_TSKHAC_162_TO_GSTEMP_DONG_BO(ma_tai_san,pLOAI_HINH_TAI_SAN);
              
        END LOOP;
    END IF;
    DBMS_OUTPUT.PUT_LINE(TO_CHAR(CURRENT_DATE, 'DD-MON-YYYY HH:MI:SS'));
END IF;

END INSERT_TO_TEMP_DONG_BO;