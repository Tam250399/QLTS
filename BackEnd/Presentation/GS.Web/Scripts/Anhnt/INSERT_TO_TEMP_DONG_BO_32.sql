create or replace PROCEDURE INSERT_TO_TEMP_DONG_BO_32 (
    PLOAI_HINH_TAI_SAN IN NUMBER DEFAULT 9,
    PNAM IN NUMBER DEFAULT 0,
    pCHE_DO_HAO_MON IN NUMBER DEFAULT 1,
    pMA_DON_VI IN VARCHAR2 DEFAULT NULL,
    pINSERT IN NUMBER DEFAULT 1
  ) AS totalRecORd NUMBER;
fn NUMBER;
TYPE TSCurTyp  IS REF CURSOR;
v_ts_cursor TSCurTyp;
ma_tai_san VARCHAR2(50);
v_stmt_str VARCHAR2(4000);
BEGIN DBMS_OUTPUT.PUT_LINE('pLOAI_HINH_TAI_SAN=' || pLOAI_HINH_TAI_SAN);
DBMS_OUTPUT.PUT_LINE(TO_CHAR(CURRENT_DATE, 'DD-MON-YYYY HH24:MI:SS'));
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
      nam,
      is_32,
      ngay_nhap
    )
  SELECT 
    ts.ma_tai_san,
    ts.ten_tai_san,
    ts.ma_don_vi,
    ts.ngay_duyet,
    ts.trang_thai,
    '||pLOAI_HINH_TAI_SAN||',
    1,
    (
      SELECT mn.ma_nhom
      FROM dm_nhom_donvi mn,
        dm_donvi dv
      WHERE rownum = 1
        AND mn.nhom_donvi_id = dv.nhom_donvi_id
    ),
    EXTRACT(
      year
      FROM ts.ngay_duyet
    ),
    1,
    '''||to_date('01-01-2015', 'DD-MM-YYYY')||'''
  FROM KT_TAISAN_31122014 ts32
    left join ts_tai_san ts on ts32.ma_tai_san = ts.ma_tai_san
  WHERE (ts.trang_thai is null or ts.trang_thai <> ''0'')
    AND ts.ma_tai_san not in (
      SELECT ma_tai_san
      FROM ts_duoi_500
    )
    AND ts.ma_tai_san not in (
      SELECT gs_temp_dong_bo_du_lieu.ma_tai_san
      FROM gs_temp_dong_bo_du_lieu
    )
    AND ts.loai_tai_san_id in (
      SELECT dmlts.loai_tai_san_id
      FROM dm_loaitaisan dmlts,
        GS_MAP_LOAI_TAI_SAN GSMAP
      WHERE dmlts.ma_loai_tai_san = GSMAP.MA_OLD
        AND dmlts.CHE_DO_HAO_MON_ID = 1
        AND GSMAP.CHE_DO_HAO_MON_ID = 1
        AND GSMAP.LOAI_HINH_TAI_SAN_ID = '||pLOAI_HINH_TAI_SAN||'
        '||
          (CASE
              WHEN pLOAI_HINH_TAI_SAN = 1 THEN 'AND 1 = (CASE WHEN ts.ma_tai_san in (
                                                          SELECT tsdat.ma_tai_san
                                                          FROM ts_dat tsdat
                                                          WHERE tsdat.LA_DAT_AO = ''1''
                                                        ) THEN 0 ELSE 1 END)' ELSE '' END)
          ||')';--AND rownum <= 10000

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
  COMMIT;
  v_stmt_str:='INSERT INTO gs_temp_dong_bo_du_lieu (
      ma_tai_san,
      ten_tai_san,
      ma_don_vi,
      ngay_duyet,
      trang_thai,
      loai_hinh_tai_san_id,
      che_do_hao_mon_id,
      MA_NHOM_DON_VI,
      nam,
      is_32
    )
  SELECT 
    TS.ma_tai_san,
    TS.ten_tai_san,
    TS.ma_don_vi,
    TS.ngay_duyet,
    TS.trang_thai,
    '||pLOAI_HINH_TAI_SAN||',
    1,
    (
      SELECT mn.ma_nhom
      FROM dm_nhom_donvi mn,
        dm_donvi dv
      WHERE rownum = 1
        AND mn.nhom_donvi_id = dv.nhom_donvi_id
    ),
    EXTRACT(
      year
      FROM TS.ngay_duyet
    ),
    1
  FROM ts_tai_san TS, DM_LOAITAISAN DMLTS, GS_MAP_LOAI_TAI_SAN GSMAP
  WHERE TS.LOAI_TAI_SAN_ID = DMLTS.LOAI_TAI_SAN_ID
    AND GSMAP.MA_OLD = DMLTS.MA_LOAI_TAI_SAN
    AND GSMAP.CHE_DO_HAO_MON_ID = 1
    AND DMLTS.CHE_DO_HAO_MON_ID = 1
    AND (TS.trang_thai is null or TS.trang_thai <> ''0'')
    AND TS.ma_tai_san not in (
      SELECT MA_TAI_SAN
      FROM KT_TAISAN_31122014
    )
    AND TS.ma_tai_san not in (
      SELECT ma_tai_san
      FROM ts_duoi_500
    )
    AND TS.ma_tai_san not in (
      SELECT gs_temp_dong_bo_du_lieu.ma_tai_san
      FROM gs_temp_dong_bo_du_lieu
    )
    AND GSMAP.LOAI_HINH_TAI_SAN_ID = '|| pLOAI_HINH_TAI_SAN ||
      (CASE
          WHEN pLOAI_HINH_TAI_SAN = 1 THEN 'AND 1 = (CASE WHEN TS.ma_tai_san in (
                                                      SELECT tsdat.ma_tai_san
                                                      FROM ts_dat tsdat
                                                      WHERE tsdat.LA_DAT_AO = ''1''
                                                    ) THEN 0 ELSE 1 END)' ELSE '' END);

  IF pMA_DON_VI IS NOT NULL THEN
    v_stmt_str:=v_stmt_str||' AND 1 = (CASE WHEN TS.ma_don_vi IN (SELECT MA_DON_VI
                                                                  FROM DM_DONVI
                                                                  WHERE MA_BO = '''||pMA_DON_VI||''') THEN 1
                                            ELSE 0 END)';
  END IF;

  IF pNam > 0 THEN 
    v_stmt_str:=v_stmt_str|| ' AND EXTRACT(YEAR FROM TS.ngay_duyet) = ' || pNam;
  END IF;
  EXECUTE IMMEDIATE v_stmt_str;
ELSE
  SELECT 
    count(*) into totalRecORd
  FROM 
    gs_temp_dong_bo_du_lieu gstemp
  WHERE 
    gstemp.loai_hinh_tai_san_id = pLOAI_HINH_TAI_SAN
    AND is_32 = 1
    -- AND lst_bien_dong_json is not json
    AND tai_san_json IS NULL
    --AND rownum <= 10000
    AND 1 = (
      CASE
        WHEN pMA_DON_VI IS NULL
        OR gstemp.ma_don_vi = pMA_DON_VI THEN 1
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
  DBMS_OUTPUT.PUT_LINE('totalRecORd=' || totalRecORd);
  v_stmt_str:='SELECT 
                  gstemp.ma_tai_san
                FROM 
                  gs_temp_dong_bo_du_lieu gstemp
                WHERE 
                  gstemp.loai_hinh_tai_san_id = '||pLOAI_HINH_TAI_SAN||'
                  AND is_32 = 1
                  AND tai_san_json IS NULL';
      --AND lst_bien_dong_json is not json
      --AND tai_san_json IS NULL
      --AND rownum <= 10000
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
      fn:=GS_UPDATE_TSDAT_32_TO_GSTEMP_DONG_BO(ma_tai_san,pLOAI_HINH_TAI_SAN);
  END LOOP;
  ELSIF pLOAI_HINH_TAI_SAN = 2 THEN --TsNha
  OPEN v_ts_cursor FOR v_stmt_str;
  LOOP
      FETCH v_ts_cursor INTO ma_tai_san;
      EXIT WHEN v_ts_cursor%NOTFOUND;
  fn:=GS_UPDATE_TSNHA_32_TO_GSTEMP_DONG_BO(ma_tai_san,pLOAI_HINH_TAI_SAN);
  END LOOP;
  ELSIF pLOAI_HINH_TAI_SAN = 3 THEN --tsvkt
  OPEN v_ts_cursor FOR v_stmt_str;
  LOOP
      FETCH v_ts_cursor INTO ma_tai_san;
      EXIT WHEN v_ts_cursor%NOTFOUND;
      fn:=GS_UPDATE_TSVKT_32_TO_GSTEMP_DONG_BO(ma_tai_san,pLOAI_HINH_TAI_SAN);
  END LOOP;
  COMMIT;
  ELSIF pLOAI_HINH_TAI_SAN = 4 THEN --TsOto
  OPEN v_ts_cursor FOR v_stmt_str;
  LOOP
      FETCH v_ts_cursor INTO ma_tai_san;
      EXIT WHEN v_ts_cursor%NOTFOUND;
      fn:=GS_UPDATE_TSOTO_32_TO_GSTEMP_DONG_BO(ma_tai_san,pLOAI_HINH_TAI_SAN);
  END LOOP;
  COMMIT;
  ELSIF pLOAI_HINH_TAI_SAN = 5 THEN --TsPTK
  OPEN v_ts_cursor FOR v_stmt_str;
  LOOP
      FETCH v_ts_cursor INTO ma_tai_san;
      EXIT WHEN v_ts_cursor%NOTFOUND;
      fn:=GS_UPDATE_TSPTK_32_TO_GSTEMP_DONG_BO(ma_tai_san,pLOAI_HINH_TAI_SAN);
  END LOOP;
  COMMIT;
  ELSIF pLOAI_HINH_TAI_SAN = 7 THEN 
  OPEN v_ts_cursor FOR v_stmt_str;
  LOOP
      FETCH v_ts_cursor INTO ma_tai_san;
      EXIT WHEN v_ts_cursor%NOTFOUND;
    fn:=GS_UPDATE_TSCLN_32_TO_GSTEMP_DONG_BO(ma_tai_san,pLOAI_HINH_TAI_SAN);
  END LOOP;
  COMMIT;
  ELSE 
  OPEN v_ts_cursor FOR v_stmt_str;
  LOOP
      FETCH v_ts_cursor INTO ma_tai_san;
      EXIT WHEN v_ts_cursor%NOTFOUND;
  fn:=GS_UPDATE_TSKHAC_32_TO_GSTEMP_DONG_BO(ma_tai_san,pLOAI_HINH_TAI_SAN);
  END LOOP;
  END IF;
END IF;
DBMS_OUTPUT.PUT_LINE(TO_CHAR(CURRENT_DATE, 'DD-MON-YYYY HH24:MI:SS'));
END INSERT_TO_TEMP_DONG_BO_32;