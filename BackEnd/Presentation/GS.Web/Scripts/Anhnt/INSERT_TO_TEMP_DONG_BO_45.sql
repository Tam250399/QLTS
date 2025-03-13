create or replace PROCEDURE INSERT_TO_TEMP_DONG_BO_45 (
    pLOAI_HINH_TAI_SAN IN NUMBER DEFAULT 1,
    pNAM IN NUMBER DEFAULT 0,
    pMA_DON_VI IN VARCHAR2 DEFAULT NULL,
    pINSERT IN NUMBER DEFAULT 1
  ) AS 
    totalRecord NUMBER;
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
    v_stmt_str:='
    INSERT INTO GS_TEMP_DONG_BO_DU_LIEU (
        MA_TAI_SAN,
        TEN_TAI_SAN,
        MA_DU_AN,
        MA_DON_VI,
        NGAY_DUYET,
        TRANG_THAI,
        LOAI_HINH_TAI_SAN_ID,
        MA_NHOM_DON_VI,
        NAM,
        CHE_DO_HAO_MON_ID,
        CHE_DO_HAO_MON_ID_OLD,
        HM_TY_LE
      )
    SELECT TS.MA_TAI_SAN,
      TS.TEN_TAI_SAN,
      (
        SELECT
          MA_DU_AN
        FROM
          DM_DUAN
        WHERE
          DU_AN_ID = TS.DU_AN_ID
          AND ROWNUM = 1
      ),
      TS.MA_DON_VI,
      TS.NGAY_DUYET,
      TS.TRANG_THAI,
      '||pLOAI_HINH_TAI_SAN||',
      (
        SELECT MN.MA_NHOM
        FROM DM_NHOM_DONVI MN,
          DM_DONVI DV
        WHERE ROWNUM = 1
          AND MN.NHOM_DONVI_ID = DV.NHOM_DONVI_ID
          AND DV.MA_DON_VI = TS.MA_DON_VI
      ),
      EXTRACT(
        YEAR
        FROM TS.NGAY_DUYET
      ),
      23,
      LTS.CHE_DO_HAO_MON_ID,
      '||
      CASE 
          WHEN pLOAI_HINH_TAI_SAN = 2 THEN
                '(
                  SELECT TY_LE_HAO_MON
                  FROM TS_NHA
                  WHERE MA_TAI_SAN = TS.MA_TAI_SAN
                )'
          WHEN pLOAI_HINH_TAI_SAN = 4 THEN
                '(
                  SELECT TY_LE_HAO_MON
                  FROM TS_OTO
                  WHERE MA_TAI_SAN = TS.MA_TAI_SAN
                )'
          WHEN pLOAI_HINH_TAI_SAN IN (3,5,6,7,8,9,10) THEN
                '(
                  SELECT TY_LE_HAO_MON
                  FROM TS_KHAC
                  WHERE MA_TAI_SAN = TS.MA_TAI_SAN
                )'
          ELSE 'NULL'
      END
      ||'
    FROM TS_TAI_SAN TS
      LEFT JOIN DM_LOAITAISAN LTS ON TS.LOAI_TAI_SAN_ID = LTS.LOAI_TAI_SAN_ID
    WHERE TS.MA_TAI_SAN NOT IN (
        SELECT MA_TAI_SAN
        FROM TS_DUOI_500
      )
      AND TS.MA_TAI_SAN NOT IN (
        SELECT GS_TEMP_DONG_BO_DU_LIEU.MA_TAI_SAN
        FROM GS_TEMP_DONG_BO_DU_LIEU
      )
      AND (TS.TRANG_THAI IS NULL OR TS.TRANG_THAI <> ''0'')
      AND LTS.MA_LOAI_TAI_SAN IN (
                                  SELECT 
                                    GSMAP.MA_OLD
                                  FROM
                                    GS_MAP_LOAI_TAI_SAN GSMAP
                                  WHERE 
                                    GSMAP.LOAI_HINH_TAI_SAN_ID = '||pLOAI_HINH_TAI_SAN||'
                                    AND GSMAP.CHE_DO_HAO_MON_OLD = LTS.CHE_DO_HAO_MON_ID)
        '||
          (CASE
              WHEN pLOAI_HINH_TAI_SAN = 1 
              THEN ' AND TS.MA_TAI_SAN NOT IN (
                                                SELECT TSDAT.MA_TAI_SAN
                                                FROM TS_DAT TSDAT
                                                WHERE TSDAT.LA_DAT_AO = ''1''
                                              )' ELSE '' END);
IF pMA_DON_VI IS NOT NULL THEN
    v_stmt_str:=v_stmt_str||' AND 1 = (CASE WHEN TS.MA_DON_VI IN (SELECT MA_DON_VI
                                                                  FROM DM_DONVI
                                                                  WHERE MA_BO = '''||pMA_DON_VI||''') THEN 1
                                            ELSE 0 END)';
  END IF;

  IF pNam > 0 THEN 
    v_stmt_str:=v_stmt_str|| ' AND EXTRACT(YEAR FROM TS.NGAY_DUYET) = ' || pNam;
  END IF;
  --DBMS_OUTPUT.PUT_LINE(v_stmt_str);
  EXECUTE IMMEDIATE v_stmt_str;
    commit;
ELSE
    SELECT count(*) INTO totalRecord
    FROM gs_temp_dong_bo_du_lieu gstemp
    WHERE 
        gstemp.loai_hinh_tai_san_id = pLOAI_HINH_TAI_SAN AND gstemp.tai_san_json is null

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
    v_stmt_str:='SELECT GSTEMP.MA_TAI_SAN
      FROM GS_TEMP_DONG_BO_DU_LIEU GSTEMP
      WHERE GSTEMP.LOAI_HINH_TAI_SAN_ID = '||pLOAI_HINH_TAI_SAN||' AND GSTEMP.TAI_SAN_JSON IS NULL
        ';
    IF pMA_DON_VI IS NOT NULL THEN
        v_stmt_str:=v_stmt_str||' AND 1 = (CASE WHEN GSTEMP.MA_DON_VI IN (SELECT MA_DON_VI
                                                                  FROM DM_DONVI
                                                                  WHERE MA_BO = '''||pMA_DON_VI||''') THEN 1
                                            ELSE 0 END)';
    END IF;

    IF pNam > 0 THEN
    v_stmt_str:=v_stmt_str||' AND GSTEMP.NAM = ' || pNam;
    END IF;
    DBMS_OUTPUT.PUT_LINE(v_stmt_str);
    IF pLOAI_HINH_TAI_SAN = 1 THEN --TsDat
        OPEN v_ts_cursor FOR v_stmt_str;
        LOOP
          FETCH v_ts_cursor INTO ma_tai_san;
          EXIT WHEN v_ts_cursor%NOTFOUND;
          fn:=GS_UPDATE_TSDAT_45_TO_GSTEMP_DONG_BO(ma_tai_san,pLOAI_HINH_TAI_SAN);
        END LOOP;
    ELSIF pLOAI_HINH_TAI_SAN = 2 THEN --TsNha
        OPEN v_ts_cursor FOR v_stmt_str;
        LOOP
          FETCH v_ts_cursor INTO ma_tai_san;
          EXIT WHEN v_ts_cursor%NOTFOUND;
          fn:=GS_UPDATE_TSNHA_45_TO_GSTEMP_DONG_BO(ma_tai_san,pLOAI_HINH_TAI_SAN);
        END LOOP;
    ELSIF pLOAI_HINH_TAI_SAN = 3 THEN --tsvkt
        OPEN v_ts_cursor FOR v_stmt_str;
            LOOP
              FETCH v_ts_cursor INTO ma_tai_san;
              EXIT WHEN v_ts_cursor%NOTFOUND;
              fn:=GS_UPDATE_TSVKT_45_TO_GSTEMP_DONG_BO(ma_tai_san,pLOAI_HINH_TAI_SAN);

        END LOOP;
    ELSIF pLOAI_HINH_TAI_SAN = 4 THEN --TsOto
        OPEN v_ts_cursor FOR v_stmt_str;
            LOOP
              FETCH v_ts_cursor INTO ma_tai_san;
              EXIT WHEN v_ts_cursor%NOTFOUND;
              fn:=GS_UPDATE_TSOTO_45_TO_GSTEMP_DONG_BO(ma_tai_san,pLOAI_HINH_TAI_SAN);
    END LOOP;

    ELSIF pLOAI_HINH_TAI_SAN = 5 THEN --TsPTK
        OPEN v_ts_cursor FOR v_stmt_str;
            LOOP
              FETCH v_ts_cursor INTO ma_tai_san;
              EXIT WHEN v_ts_cursor%NOTFOUND;
              fn:=GS_UPDATE_TSPTK_45_TO_GSTEMP_DONG_BO(ma_tai_san,pLOAI_HINH_TAI_SAN);

    END LOOP;
    ELSIF pLOAI_HINH_TAI_SAN = 7 THEN 
        OPEN v_ts_cursor FOR v_stmt_str;
            LOOP
              FETCH v_ts_cursor INTO ma_tai_san;
              EXIT WHEN v_ts_cursor%NOTFOUND;
              fn:=GS_UPDATE_TSCLN_45_TO_GSTEMP_DONG_BO(ma_tai_san,pLOAI_HINH_TAI_SAN);
        END LOOP;
    ELSE 
        OPEN v_ts_cursor FOR v_stmt_str;
            LOOP
              FETCH v_ts_cursor INTO ma_tai_san;
              EXIT WHEN v_ts_cursor%NOTFOUND;
              fn:=GS_UPDATE_TSKHAC_45_TO_GSTEMP_DONG_BO(ma_tai_san,pLOAI_HINH_TAI_SAN);

        END LOOP;
    END IF;
    DBMS_OUTPUT.PUT_LINE(TO_CHAR(CURRENT_DATE, 'DD-MON-YYYY HH:MI:SS'));
END IF;

END INSERT_TO_TEMP_DONG_BO_45;