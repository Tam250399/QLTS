update gs_temp_dong_bo_du_lieu
set LST_BIEN_DONG_JSON = REPLACE(LST_BIEN_DONG_JSON,'":.','":0.'),
LST_HIEN_TRANG_JSON = REPLACE(LST_HIEN_TRANG_JSON,'":.','":0.'),
LST_NGUON_VON_JSON = REPLACE(LST_NGUON_VON_JSON,'":.','":0.'),
TAI_SAN_JSON = REPLACE(TAI_SAN_JSON,'":.','":0.')
where LST_BIEN_DONG_JSON like '":.'
or LST_HIEN_TRANG_JSON like '":.'
or LST_NGUON_VON_JSON like '":.'
or TAI_SAN_JSON like '":.';

update gs_temp_dong_bo_du_lieu
set LST_BIEN_DONG_JSON = REPLACE(LST_BIEN_DONG_JSON,'":,','":null,'),
LST_HIEN_TRANG_JSON = REPLACE(LST_HIEN_TRANG_JSON,'":,','":null,'),
LST_NGUON_VON_JSON = REPLACE(LST_NGUON_VON_JSON,'":,','":null,'),
TAI_SAN_JSON = REPLACE(TAI_SAN_JSON,'":,','":null,')
where LST_BIEN_DONG_JSON like '":,'
or LST_HIEN_TRANG_JSON like '":,'
or LST_NGUON_VON_JSON like '":,'
or TAI_SAN_JSON like '":,';

update gs_temp_dong_bo_du_lieu
set LST_BIEN_DONG_JSON = REPLACE(LST_BIEN_DONG_JSON,'":-.','":-0.'),
LST_HIEN_TRANG_JSON = REPLACE(LST_HIEN_TRANG_JSON,'":-.','":-0.'),
LST_NGUON_VON_JSON = REPLACE(LST_NGUON_VON_JSON,'":-.','":-0.'),
TAI_SAN_JSON = REPLACE(TAI_SAN_JSON,'":-.','":-0.')
where LST_BIEN_DONG_JSON like '%":-.%'
or LST_HIEN_TRANG_JSON like '%":-.%'
or LST_NGUON_VON_JSON like '%":-.%'
or TAI_SAN_JSON like '%":-.%';


update gs_temp_dong_bo_du_lieu
set LST_BIEN_DONG_JSON = REPLACE(LST_BIEN_DONG_JSON,'":.','":0.'),
LST_HIEN_TRANG_JSON = REPLACE(LST_HIEN_TRANG_JSON,'":.','":0.'),
LST_NGUON_VON_JSON = REPLACE(LST_NGUON_VON_JSON,'":.','":0.'),
TAI_SAN_JSON = REPLACE(TAI_SAN_JSON,'":.','":0.')
where LST_BIEN_DONG_JSON like '%":.%'
or LST_HIEN_TRANG_JSON like '%":.%'
or LST_NGUON_VON_JSON like '%":.%'
or TAI_SAN_JSON like '%":.%';



update gs_temp_dong_bo_du_lieu
set LST_HIEN_TRANG_JSON = REPLACE(LST_HIEN_TRANG_JSON,'"{HIEN_TRANG_ID"','{"HIEN_TRANG_ID"')
where LST_HIEN_TRANG_JSON like '%"{HIEN_TRANG_ID"%';


update 
	ts_tai_san_nha tsnha
set 
	tsnha.tai_san_dat_id = (select
                                id
                            from
                                ts_tai_san
                            where 
								ma_db = tsnha.ma_db_dat)
where 
	tsnha.ma_db_dat is not null;
	
	
Select count(ma_tai_san) as total,TO_CHAR(CURRENT_DATE, 'DD-MON-YYYY HH24:MI:SS') as Current_Time
from gs_temp_dong_bo_du_lieu
where tai_san_json is null;


Select count(ma) as total,TO_CHAR(CURRENT_DATE, 'DD-MON-YYYY HH24:MI:SS') as Current_Time
from ts_tai_san
where ma_db is not null;




---- update ngay_bien_dong trung
	DECLARE
	L_EXST NUMBER(1);
	BEGIN
		FOR R_TABLE IN (
							SELECT 
								BD.ID, BD.TAI_SAN_ID, BD.NGAY_BIEN_DONG 
							FROM 
								BD_BIEN_DONG BD
							WHERE 
								EXISTS(
										SELECT 
											'X' 
										FROM 
											BD_BIEN_DONG BD1 
										WHERE 
											BD1.ID != BD.ID 
											AND BD1.NGAY_BIEN_DONG = BD.NGAY_BIEN_DONG 
											AND BD1.TAI_SAN_ID = BD.TAI_SAN_ID
											)
								AND TO_CHAR(BD.NGAY_BIEN_DONG, 'MMDD') != '0101' 
								-- AND TO_CHAR(BD.NGAY_BIEN_DONG, 'DD') != '01' 
								AND BD.LY_DO_BIEN_DONG_ID NOT IN (
																	SELECT
																		ID
																	FROM 
																		DM_LY_DO_BIEN_DONG
																	WHERE
																		LOAI_LY_DO_ID = 5)
		) LOOP

			SELECT 
				CASE 
					WHEN 
						EXISTS(
								SELECT 
									'X' 
								FROM 
									BD_BIEN_DONG BD1 
								WHERE 
									BD1.ID != R_TABLE.ID 
									AND BD1.NGAY_BIEN_DONG = R_TABLE.NGAY_BIEN_DONG 
									AND BD1.TAI_SAN_ID = R_TABLE.TAI_SAN_ID
								)
					THEN 1 
					ELSE 0 END 
				INTO L_EXST 
			FROM DUAL;
			
			IF L_EXST = 1 THEN
				UPDATE 
					BD_BIEN_DONG
				SET 
					NGAY_BIEN_DONG = NGAY_BIEN_DONG - 1
				WHERE 
					ID = R_TABLE.ID;
				COMMIT;
			END IF;
		END LOOP;


		FOR R_TABLE IN (
						SELECT 
							BD.ID, BD.TAI_SAN_ID, BD.NGAY_BIEN_DONG FROM BD_BIEN_DONG BD
						WHERE 
							EXISTS(	
									SELECT 
										'X' 
									FROM 
										BD_BIEN_DONG BD1 
									WHERE 
										BD1.ID != BD.ID 
										AND BD1.NGAY_BIEN_DONG = BD.NGAY_BIEN_DONG 
										AND BD1.TAI_SAN_ID = BD.TAI_SAN_ID
										)
							AND TO_CHAR(BD.NGAY_BIEN_DONG, 'MMDD') != '3112' 
							-- AND TO_CHAR(BD.NGAY_BIEN_DONG, 'DD') = '01' 
							AND BD.LOAI_BIEN_DONG_ID IN (
															SELECT
																ID
															FROM 
																DM_LY_DO_BIEN_DONG
															WHERE
																LOAI_LY_DO_ID = 5)
		) LOOP

			SELECT 
				CASE 
					WHEN 
						EXISTS(
								SELECT 
									'X' 
								FROM 
									BD_BIEN_DONG BD1 
								WHERE 
									BD1.ID != R_TABLE.ID 
									AND BD1.NGAY_BIEN_DONG = R_TABLE.NGAY_BIEN_DONG 
									AND BD1.TAI_SAN_ID = R_TABLE.TAI_SAN_ID
									)
						THEN 1 
					ELSE 0 
				END INTO L_EXST 
			FROM DUAL;
			
			IF L_EXST = 1 THEN
				UPDATE 
					BD_BIEN_DONG
				SET 
					NGAY_BIEN_DONG = NGAY_BIEN_DONG + 1
				WHERE 
					ID = R_TABLE.ID;
				COMMIT;
			END IF;
		END LOOP;
	END;


----
	select 
		count(ID),
		ts.don_vi_id,
		(
			select 
				tree_node 
			from 
				dm_don_vi 
			where 
				id = ts.don_vi_id
		) as tree_node
	from 
		ts_tai_san ts 
	where 
		ts.trang_thai_id = 4 
		and ts.loai_hinh_tai_san_id = 4
	GROUP BY 
		ts.don_vi_id;
    
	
	
	--delete all tai san
	DELETE ts_tai_san_nguon_von;
    DELETE TS_TAI_SAN_HIEN_TRANG_SU_DUNG;
    DELETE bd_bien_dong_chi_tiet;  
    DELETE bd_bien_dong;
    DELETE nv_yeu_cau_nhat_ky;
	DELETE nv_yeu_cau_chi_tiet;
    DELETE nv_yeu_cau;
    DELETE ts_tai_san_cho_thue;
    DELETE ts_tai_san_kiem_ke_hoi_dong;
    DELETE ts_tai_san_kiem_ke_tai_san;
    DELETE ts_tai_san_kiem_ke;
    DELETE ts_tai_san_vo_hinh;
    DELETE ts_tai_san_cln;
    DELETE ts_tai_san_dat;
    DELETE ts_tai_san_dat;
    DELETE ts_tai_san_nha;
    DELETE ts_tai_san_oto;
    DELETE ts_tai_san_vkt;
    DELETE ts_tai_san_may_moc;
    DELETE KT_HAO_MON_TAI_SAN_LOG;
    DELETE KT_HAO_MON_TAI_SAN;
    DELETE KT_KHAU_HAO_TAI_SAN;
    DELETE DB_TAI_SAN_NHAT_KY;
    DELETE ts_tai_san;
    -----------------------------
	
	
	insert into db_tai_san_nhat_ky(TAI_SAN_ID,
	LOAI_HINH_TAI_SAN_ID,
	TRANG_THAI_ID,
	NGAY_CAP_NHAT)
	select
		ts.id,
		ts.loai_hinh_tai_san_id,
		0,
		ts.ngay_tao
	from
		ts_tai_san ts
	where
		ts.id not in (select tai_san_id from db_tai_san_nhat_ky)
		and ts.trang_thai_id <> 1;
	------------------------------------
	update db_tai_san_nhat_ky dbtsnk
	set dbtsnk.bd_ids = (select LISTAGG(bd.ID, ',') WITHIN GROUP (ORDER BY bd.ID) AS description
					from bd_bien_dong bd
					where bd.tai_san_id = dbtsnk.tai_san_id
					and bd.trang_thai_id = 3);
	------------------------------------
	--delete gs_temp_dong_bo_du_lieu
	BEGIN
		FOR RECORD IN (SELECT MA_TAI_SAN
						FROM gs_temp_dong_bo_du_lieu)
		LOOP
			DELETE gs_temp_dong_bo_du_lieu
			WHERE MA_TAI_SAN = RECORD.MA_TAI_SAN;
			COMMIT;
		END LOOP;
	END;
	------------------------------------
	-- CREATE TABLE KT_TAISAN_31122014
	CREATE TABLE KT_TAISAN_31122014
   (	"MA_DON_VI" VARCHAR2(50 CHAR), 
	"TEN_DON_VI" VARCHAR2(255 CHAR), 
	"MA_TAI_SAN" VARCHAR2(50 CHAR), 
	"TEN_TAI_SAN" VARCHAR2(500 CHAR), 
	"DIEN_TICH" NUMBER(18,3), 
	"SO_LUONG" NUMBER(18,3), 
	"NGUYEN_GIA_NS" NUMBER(18,3), 
	"NGUYEN_GIA_KHAC" NUMBER(18,3), 
	"NGUYEN_GIA_ODA" NUMBER(18,3), 
	"NGUYEN_GIA_VIEN_TRO" NUMBER(18,3), 
	"GIA_TRI_CON_LAI" NUMBER(18,3), 
	"LAM_TRU_SO_NN" NUMBER(18,3), 
	"HD_SN" NUMBER(18,3), 
	"SX_KDDV" NUMBER(18,3), 
	"CHO_THUE" NUMBER(18,3), 
	"SX_KDDV_SAIPHEP" NUMBER(18,3), 
	"DE_O" NUMBER(18,3), 
	"SD_KHAC" NUMBER(18,3)
   )
   ------------------------------------

	create or replace FUNCTION GS_DEL_GS_TEMP_TSDB (
	p_MA_DB VARCHAR2
	)RETURN number AS 

	BEGIN
	  FOR r_product IN (
			SELECT * FROM gs_temp_dong_bo_du_lieu
		) 
	  LOOP
		DELETE gs_temp_dong_bo_du_lieu
		WHERE r_product.MA_TAI_SAN=MA_TAI_SAN;
		commit;
	  END LOOP;
	  RETURN 1;
	END GS_DEL_GS_TEMP_TSDB;
	------------------------------------- update hm_gia_tri_con_lai biến động chi tiết
	declare
		l_year NUMBER;
		l_giatri NUMBER;
	BEGIN
	DBMS_OUTPUT.PUT_LINE('RUN');
		FOR RECORD IN (SELECT 
							bd.ID AS BIEN_DONG_ID,
							ts.ID AS TAI_SAN_ID,
							bd.NGAY_BIEN_DONG AS NGAY_BIEN_DONG
						FROM 
							bd_bien_dong_chi_tiet bdct
						JOIN
							bd_bien_dong bd
							ON bdct.bien_dong_id = bd.id
						JOIN 
							ts_tai_san ts
							ON ts.id = bd.tai_san_id
						WHERE 
							bd.LOAI_BIEN_DONG_ID = 5
							AND nvl(bdct.hm_gia_tri_con_lai,0) = 0
							AND ts.MA_QLDKTS40 IS NOT NULL)
		LOOP
			l_year:= EXTRACT(   YEAR 
							FROM   
								RECORD.NGAY_BIEN_DONG);
			IF TO_CHAR(RECORD.NGAY_BIEN_DONG, 'DD-MON-YYYY') <> '31-12-'||l_year THEN
				l_year:=l_year-1;
			END IF;
			
			BEGIN
				SELECT
					TONG_GIA_TRI_CON_LAI
					INTO l_giatri
				FROM
					kt_hao_mon_tai_san
				WHERE
					TAI_SAN_ID = RECORD.TAI_SAN_ID
					AND NAM_HAO_MON = l_year
					AND ROWNUM = 1;
			EXCEPTION
				WHEN NO_DATA_FOUND THEN
				l_giatri:=0;
			END;
			
			IF l_giatri > 0 THEN
				UPDATE
					bd_bien_dong_chi_tiet
				SET
					hm_gia_tri_con_lai = l_giatri
				WHERE 
					BIEN_DONG_ID = RECORD.BIEN_DONG_ID;
			END IF;
			COMMIT;
		END LOOP;
	END;
	-------------------------------------
	select 
		donvi.ma_bo,
		donvi.ten_don_vi,
		ts.ma_tai_san,
		ts.ten_tai_san,
		tsct.thoi_gian_su_dung as nam_su_dung,
		(nvl(tsct.nguyen_gia_ns,0) + nvl(tsct.nguyen_gia_khac,0) + nvl(tsct.nguyen_gia_oda,0) + nvl(tsct.nguyen_gia_vien_tro,0))as nguyen_gia
	from
		ts_nha tsct
	join ts_tai_san ts on ts.ma_tai_san = tsct.ma_tai_san
	join dm_donvi donvi on ts.ma_don_vi=donvi.ma_don_vi
	where donvi.loai_don_vi = 3
	order by donvi.ma_bo,donvi.ma_don_vi;
	------------------------------
	select 
		donvi.ma_bo,
		donvi.ten_don_vi,
		ts.ma_tai_san,
		ts.ten_tai_san,
		null as nam_su_dung,
		null as nguyen_gia
	from
		ts_dat tsct
	join ts_tai_san ts on ts.ma_tai_san = tsct.ma_tai_san
	join dm_donvi donvi on ts.ma_don_vi=donvi.ma_don_vi
	where donvi.loai_don_vi = 3
	order by donvi.ma_bo,donvi.ma_don_vi;
	
	
	
	-------------------- update loai_hinh_tai_san_id cho table gs_map_loai_tai_san
	update gs_map_loai_tai_san gsmap
	set gsmap.loai_hinh_tai_san_id =   (select 
											loai_hinh_tai_san_id
										from
											gs_dm_loai_tai_san
										where 
											che_do_hao_mon_id = 23
											and ma = gsmap.ma_new);

	-------------select count ts lệch loại tài sản và loại hình tài sản
	
	select ts.ma_tai_san,ts.ten_tai_san,lts.ma_loai_tai_san,lts.ten_loai_tai_san,donvi.ma_don_vi,donvi.TEN_DON_VI --count(ts.ma_tai_san)
	from ts_nha tsct
	join ts_tai_san ts on ts.ma_tai_san = tsct.ma_tai_san
	join dm_loaitaisan lts on ts.loai_tai_san_id = lts.loai_tai_san_id
	join dm_donvi donvi on ts.ma_don_vi = donvi.ma_don_vi
	where lts.ma_loai_tai_san not like '2%';
									
	select ts.ma_tai_san,ts.ten_tai_san,lts.ma_loai_tai_san,lts.ten_loai_tai_san,donvi.ma_don_vi,donvi.TEN_DON_VI --count(ts.ma_tai_san)
	from ts_dat tsct
	join ts_tai_san ts on ts.ma_tai_san = tsct.ma_tai_san
	join dm_loaitaisan lts on ts.loai_tai_san_id = lts.loai_tai_san_id
	join dm_donvi donvi on ts.ma_don_vi = donvi.ma_don_vi
	where lts.ma_loai_tai_san not like '1%';
									
									
									
	select ts.ma_tai_san,ts.ten_tai_san,lts.ma_loai_tai_san,lts.ten_loai_tai_san,donvi.ma_don_vi,donvi.TEN_DON_VI --count(ts.ma_tai_san)
	from ts_oto tsct
	join ts_tai_san ts on ts.ma_tai_san = tsct.ma_tai_san
	join dm_loaitaisan lts on ts.loai_tai_san_id = lts.loai_tai_san_id
	join dm_donvi donvi on ts.ma_don_vi = donvi.ma_don_vi
	where lts.ma_loai_tai_san not like '3%';
									
									
									
	select ts.ma_tai_san,ts.ten_tai_san,lts.ma_loai_tai_san,lts.ten_loai_tai_san,donvi.ma_don_vi,donvi.TEN_DON_VI --count(ts.ma_tai_san)
	from ts_khac tsct
	join ts_tai_san ts on ts.ma_tai_san = tsct.ma_tai_san
	join dm_loaitaisan lts on ts.loai_tai_san_id = lts.loai_tai_san_id
	join dm_donvi donvi on ts.ma_don_vi = donvi.ma_don_vi
	where lts.ma_loai_tai_san like '1%'
		  or lts.ma_loai_tai_san like '2%'
		  or lts.ma_loai_tai_san like '3%';


	-----update map lý do biến động
	update gs_map_ly_do_bien_dong gsmap
	set gsmap.ten_new = (select
							ten
						from 
							gs_dm_ly_do_bien_dong
						where
							ma = gsmap.ma_new),
		gsmap.LOAI_LY_DO_NEW = (select
									LOAI_LY_DO_ID
								from 
									gs_dm_ly_do_bien_dong
								where
									ma = gsmap.ma_new);
	
	-----------update tree node
	create or replace PROCEDURE UP_DATE_TREE_NODE_DIA_BAN AS
	BEGIN
		FOR record in (Select Level
							,diaban.tree_level
							,diaban.id
							,diaban.parent_id
							,diaban.tree_node
							,Prior diaban.tree_node as parent_tree_node
						From dm_dia_ban diaban
						Connect By Prior diaban.id = diaban.parent_id
						Start  With diaban.parent_id Is Null)
		LOOP
			update dm_dia_ban 
			set tree_node =   (CASE WHEN record.parent_tree_node IS NULL
									THEN LPAD(ID, 8, '0')
									ELSE record.parent_tree_node||'-'||LPAD(ID, 8, '0') END),
			tree_level = record.Level
			where id=record.id;
			commit;
		END LOOP;
	END UP_DATE_TREE_NODE_DIA_BAN;

--------insert tổng nguyên giá
DECLARE
l_nam_min NUMBER;
l_nam_max NUMBER;
L_TONG_NGUYEN_GIA NUMBER;
L_ID NUMBER;
BEGIN
	FOR RECORD IN (
					SELECT
						DISTINCT(TAI_SAN_ID) AS ID
					FROM
						BD_BIEN_DONG
					WHERE
						LOAI_HINH_TAI_SAN_ID = 1
					)
	LOOP
		SELECT
			MIN(EXTRACT(YEAR FROM NGAY_BIEN_DONG))
			INTO l_nam_min
		FROM
			BD_BIEN_DONG
		WHERE
			TAI_SAN_ID = RECORD.ID;

		l_nam_max:=EXTRACT(YEAR FROM CURRENT_DATE);

		FOR INAM IN l_nam_min .. l_nam_max
		LOOP
			BEGIN
	            SELECT
	                SUM
	                (
	                    BD.NGUYEN_GIA
	                )
	                INTO L_TONG_NGUYEN_GIA
	            FROM 
	                BD_BIEN_DONG BD 
	            WHERE
	                BD.TAI_SAN_ID = RECORD.ID
	                AND EXTRACT(YEAR FROM BD.NGAY_BIEN_DONG) <= INAM;
	        EXCEPTION
	            WHEN NO_DATA_FOUND THEN
	            L_TONG_NGUYEN_GIA := 0;
	        END;

			BEGIN
				SELECT ID INTO L_ID
				FROM KT_HAO_MON_TAI_SAN WHERE TAI_SAN_ID = RECORD.ID AND NAM_HAO_MON = INAM;

				UPDATE KT_HAO_MON_TAI_SAN 
				SET	TONG_NGUYEN_GIA = L_TONG_NGUYEN_GIA,
					TONG_GIA_TRI_CON_LAI = L_TONG_NGUYEN_GIA
				WHERE ID = L_ID;
			EXCEPTION
				WHEN NO_DATA_FOUND THEN
				INSERT INTO KT_HAO_MON_TAI_SAN (
							TAI_SAN_ID,
							MA_TAI_SAN,
							NAM_HAO_MON,
							TONG_NGUYEN_GIA,
							TONG_GIA_TRI_CON_LAI
						)
				VALUES (
							RECORD.ID,
							(
								SELECT
									MA
								FROM
									TS_TAI_SAN
								WHERE
									ID = RECORD.ID),
							INAM,
							L_TONG_NGUYEN_GIA,
							L_TONG_NGUYEN_GIA
						);
			END;
			

		END LOOP;
	END LOOP;
END;
----------------------------


---------insert dm_don_vi
begin
	for record in (select dv.*
					from QLDKTS_25_01_2021.DM_DONVI dv
					where dv.ma_don_vi not in (select ma
                            from dm_don_vi)
					order by dv.ma_don_vi)
		loop
		insert into dm_don_vi
			(
				MA,
				TEN,
				MA_BO,
				MA_DIA_BAN,
				MA_DVQHNS,
				DIA_CHI,
				DIEN_THOAI,
				FAX,
				MA_TINH,
				NHOM_DON_VI_ID,
				CAP_DON_VI_ID,
				MA_HUYEN,
				CQTC_MA,
				CHE_DO_HACH_TOAN_ID,
				SO_QUYET_DINH,
				NGAY_QUYET_DINH,
				SO_QUYET_DINH_GIAO_VON,
				NGAY_QUYET_DINH_GIAO_VON,
				PARENT_ID,
				LOAI_DON_VI_ID,
				DIA_BAN_ID,
				TRANG_THAI_ID,
				DON_VI_HIEN_THI,
				LA_DON_VI_NHAP_LIEU,
				TRANG_THAI_THAY_DOI_ID,
				CO_TAI_SAN,
				KHONG_CHUYEN_MA,
				LA_BAN_QL_DU_AN,
				LA_DON_VI_TU_CHU_TAI_CHINH,
				DA_CO_QUYET_DINH_GIAO_VON,
				NGAY_TAO,
				NGAY_CAP_NHAT
			)
		values
		(
			record.MA_DON_VI,
			record.TEN_DON_VI,
			record.MA_BO,
			record.MA_DIA_BAN,
			record.MA_DON_VI_CU,
			record.DIA_CHI,
			record.DIEN_THOAI,
			record.FAX,
			record.MA_TINH,
			record.NHOM_DONVI_ID,
			record.CAP,
			record.MA_HUYEN,
			record.CQTC_MA,
			record.CHE_DO_HACH_TOAN,
			record.SO_QUYET_DINH,
			record.NGAY_QUYET_DINH,
			record.SO_QUYET_DINH_GIAO_VON,
			record.NGAY_QUYET_DINH_GIAO_VON,
			(select id
			from dm_don_vi
			where ma = record.MA_DON_VI_CHA),
			(case when record.LOAI_DON_VI is not null then
					(select id 
					from dm_loai_don_vi
					where ma = record.LOAI_DON_VI)
				else null end),
			(select id
			from dm_dia_ban
			where ma = record.MA_DIA_BAN
			order by trang_thai_id
			FETCH first row only),
			record.TRANG_THAI,
			record.DON_VI_HIEN_THI,
			record.LA_DON_VI_NHAP_LIEU,
			record.TRANG_THAI_THAY_DOI,
			record.CO_TAI_SAN,
			record.KHONG_CHUYEN_MA,
			record.LA_BAN_QL_DU_AN,
			record.LA_DON_VI_TU_CHU_TAI_CHINH,
			record.DA_CO_QUYET_DINH_GIAO_VON,
			CURRENT_DATE,
			CURRENT_DATE
			);
		commit;
	end loop;
end;
------------------------------------
---------UPDATE TÊN TÀI SẢN ĐẤT
DECLARE
L_TEN_TAI_SAN VARCHAR2(2000);
l_tsdat_tendiaban VARCHAR2(2000);
BEGIN
	--[TENTRUSO],[DIACHI],[XAPHUONG], [QUANHUYEN], [TINHTHANH]
    FOR RECORD IN (
                    SELECT TSDAT.DIA_CHI,TSDAT.DIA_BAN_ID,TSDAT.TAI_SAN_ID,TS.MA_QLDKTS40
                    FROM TS_TAI_SAN_DAT TSDAT
                    JOIN TS_TAI_SAN TS ON TS.ID = TSDAT.TAI_SAN_ID)
    LOOP
        BEGIN
            SELECT TEN_TAI_SAN INTO L_TEN_TAI_SAN
            FROM qldkts_17_02_2020.TS_TAI_SAN
            WHERE MA_TAI_SAN = RECORD.MA_QLDKTS40;
        EXCEPTION
            WHEN NO_DATA_FOUND THEN
            CONTINUE;
        END;
        IF RECORD.DIA_CHI IS NOT NULL THEN
            L_TEN_TAI_SAN := L_TEN_TAI_SAN || ',' || RECORD.DIA_CHI;
        END IF;
    
        IF RECORD.DIA_BAN_ID IS NOT NULL THEN
            SELECT 
                LISTAGG(dm_dia_ban.TEN, ', ') WITHIN GROUP (ORDER BY dm_dia_ban.tree_node desc) AS description INTO l_tsdat_tendiaban
            FROM  
                dm_dia_ban
            START WITH id = RECORD.DIA_BAN_ID
            CONNECT BY prior parent_id = id;
            L_TEN_TAI_SAN := L_TEN_TAI_SAN || ',' || l_tsdat_tendiaban;
        END IF;
        UPDATE TS_TAI_SAN
        SET TEN = L_TEN_TAI_SAN
        WHERE ID = RECORD.TAI_SAN_ID;
        
        UPDATE BD_BIEN_DONG
        SET TAI_SAN_TEN = L_TEN_TAI_SAN
        WHERE TAI_SAN_ID = RECORD.TAI_SAN_ID;
        
        UPDATE NV_YEU_CAU
        SET TAI_SAN_TEN = L_TEN_TAI_SAN
        WHERE TAI_SAN_ID = RECORD.TAI_SAN_ID;
        COMMIT;
    END LOOP;
END;
------UPDATE TRANG THAI TS_TAI_SAN DKTS4.0

DECLARE
    trangThai NUMBER;
BEGIN
	--DAT
    FOR RECORD IN (
                    SELECT TSDAT.MA_TAI_SAN AS MA_TAI_SAN
                    FROM TS_DAT TSDAT
                    INNER JOIN TS_TAI_SAN TS ON TS.MA_TAI_SAN = TSDAT.MA_TAI_SAN
                    WHERE TS.TRANG_THAI IS NULL
                    AND (TS.MA_DON_VI LIKE '018%'
                        OR TS.MA_DON_VI LIKE 'T01%'
                        OR TS.MA_DON_VI LIKE 'T03%'
                        OR TS.MA_DON_VI LIKE 'T05%'
                        OR TS.MA_DON_VI LIKE 'T07%'))
    LOOP
        BEGIN
            SELECT
                (CASE 
                    WHEN LOAI_BIEN_DONG = 5 AND DUYET_BIEN_DONG = 2 THEN 4
                    ELSE TO_NUMBER(DUYET_BIEN_DONG) END) INTO trangThai
            FROM
                BD_DAT
            WHERE
                DUYET_BIEN_DONG <> 0
                AND MA_TAI_SAN = RECORD.MA_TAI_SAN
            ORDER BY 
                NGAY_BIEN_DONG DESC
            FETCH 
                FIRST 1 ROW ONLY;

            IF trangThai IN (1,3) THEN
            SELECT
                (CASE 
                    WHEN COUNT(*) >= 1 THEN 2 END) INTO trangThai
            FROM
                BD_DAT
            WHERE
                DUYET_BIEN_DONG <> 2
                AND MA_TAI_SAN = RECORD.MA_TAI_SAN;
            END IF;
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
          BEGIN 
            SELECT
                2 INTO trangThai
            FROM
                TS_DULIEU_TRAODOI
            WHERE
                MA_TAI_SAN_PHAT_SINH = RECORD.MA_TAI_SAN
                AND ROWNUM = 1;
          EXCEPTION
            WHEN NO_DATA_FOUND THEN
            trangThai:=NULL;
          END;
        END;
        UPDATE TS_TAI_SAN
        SET TRANG_THAI = trangThai
        WHERE MA_TAI_SAN = RECORD.MA_TAI_SAN;
        COMMIT;
    END LOOP;
	--NHA
    FOR RECORD IN (
                    SELECT TSNHA.MA_TAI_SAN AS MA_TAI_SAN
                    FROM TS_NHA TSNHA
                    INNER JOIN TS_TAI_SAN TS ON TS.MA_TAI_SAN = TSNHA.MA_TAI_SAN
                    WHERE TS.TRANG_THAI IS NULL
                    AND (TS.MA_DON_VI LIKE '018%'
                        OR TS.MA_DON_VI LIKE 'T01%'
                        OR TS.MA_DON_VI LIKE 'T03%'
                        OR TS.MA_DON_VI LIKE 'T05%'
                        OR TS.MA_DON_VI LIKE 'T07%'))
    LOOP
        BEGIN
            SELECT
                (CASE 
                    WHEN LOAI_BIEN_DONG = 5 AND DUYET_BIEN_DONG = 2 THEN 4
                    ELSE TO_NUMBER(DUYET_BIEN_DONG) END) INTO trangThai
            FROM
                BD_NHA
            WHERE
                DUYET_BIEN_DONG <> 0
                AND MA_TAI_SAN = RECORD.MA_TAI_SAN
            ORDER BY 
                NGAY_BIEN_DONG DESC
            FETCH 
                FIRST 1 ROW ONLY;

            IF trangThai IN (1,3) THEN
            SELECT
                (CASE 
                    WHEN COUNT(*) >= 1 THEN 2 END) INTO trangThai
            FROM
                BD_NHA
            WHERE
                DUYET_BIEN_DONG <> 2
                AND MA_TAI_SAN = RECORD.MA_TAI_SAN;
            END IF;
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
          BEGIN 
            SELECT
                2 INTO trangThai
            FROM
                TS_DULIEU_TRAODOI
            WHERE
                MA_TAI_SAN_PHAT_SINH = RECORD.MA_TAI_SAN
                AND ROWNUM = 1;
          EXCEPTION
            WHEN NO_DATA_FOUND THEN
            trangThai:=NULL;
          END;
        END;
        UPDATE TS_TAI_SAN
        SET TRANG_THAI = trangThai
        WHERE MA_TAI_SAN = RECORD.MA_TAI_SAN;
        COMMIT;
    END LOOP;
	--OTO
    FOR RECORD IN (
                    SELECT TSOTO.MA_TAI_SAN AS MA_TAI_SAN
                    FROM TS_OTO TSOTO
                    INNER JOIN TS_TAI_SAN TS ON TS.MA_TAI_SAN = TSOTO.MA_TAI_SAN
                    WHERE TS.TRANG_THAI IS NULL
                    AND (TS.MA_DON_VI LIKE '018%'
                        OR TS.MA_DON_VI LIKE 'T01%'
                        OR TS.MA_DON_VI LIKE 'T03%'
                        OR TS.MA_DON_VI LIKE 'T05%'
                        OR TS.MA_DON_VI LIKE 'T07%'))
    LOOP
        BEGIN
            SELECT
                (CASE 
                    WHEN LOAI_BIEN_DONG = 5 AND DUYET_BIEN_DONG = 2 THEN 4
                    ELSE TO_NUMBER(DUYET_BIEN_DONG) END) INTO trangThai
            FROM
                BD_OTO
            WHERE
                DUYET_BIEN_DONG <> 0
                AND MA_TAI_SAN = RECORD.MA_TAI_SAN
            ORDER BY 
                NGAY_BIEN_DONG DESC
            FETCH 
                FIRST 1 ROW ONLY;

            IF trangThai IN (1,3) THEN
            SELECT
                (CASE 
                    WHEN COUNT(*) >= 1 THEN 2 END) INTO trangThai
            FROM
                BD_OTO
            WHERE
                DUYET_BIEN_DONG <> 2
                AND MA_TAI_SAN = RECORD.MA_TAI_SAN;
            END IF;
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
          BEGIN 
            SELECT
                2 INTO trangThai
            FROM
                TS_DULIEU_TRAODOI
            WHERE
                MA_TAI_SAN_PHAT_SINH = RECORD.MA_TAI_SAN
                AND ROWNUM = 1;
          EXCEPTION
            WHEN NO_DATA_FOUND THEN
            trangThai:=NULL;
          END;
        END;
        UPDATE TS_TAI_SAN
        SET TRANG_THAI = trangThai
        WHERE MA_TAI_SAN = RECORD.MA_TAI_SAN;
        COMMIT;
    END LOOP;
	--KHAC
	FOR RECORD IN (
                    SELECT TSKHAC.MA_TAI_SAN AS MA_TAI_SAN
                    FROM TS_KHAC TSKHAC
                    INNER JOIN TS_TAI_SAN TS ON TS.MA_TAI_SAN = TSKHAC.MA_TAI_SAN
                    WHERE TS.TRANG_THAI IS NULL
                    AND (TS.MA_DON_VI LIKE '018%'
                        OR TS.MA_DON_VI LIKE 'T01%'
                        OR TS.MA_DON_VI LIKE 'T03%'
                        OR TS.MA_DON_VI LIKE 'T05%'
                        OR TS.MA_DON_VI LIKE 'T07%'))
    LOOP
        BEGIN
            SELECT
                (CASE 
                    WHEN LOAI_BIEN_DONG = 5 AND DUYET_BIEN_DONG = 2 THEN 4
                    ELSE TO_NUMBER(DUYET_BIEN_DONG) END) INTO trangThai
            FROM
                BD_KHAC
            WHERE
                DUYET_BIEN_DONG <> 0
                AND MA_TAI_SAN = RECORD.MA_TAI_SAN
            ORDER BY 
                NGAY_BIEN_DONG DESC
            FETCH 
                FIRST 1 ROW ONLY;

            IF trangThai IN (1,3) THEN
            SELECT
                (CASE 
                    WHEN COUNT(*) >= 1 THEN 2 END) INTO trangThai
            FROM
                BD_KHAC
            WHERE
                DUYET_BIEN_DONG <> 2
                AND MA_TAI_SAN = RECORD.MA_TAI_SAN;
            END IF;
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
          BEGIN 
            SELECT
                2 INTO trangThai
            FROM
                TS_DULIEU_TRAODOI
            WHERE
                MA_TAI_SAN_PHAT_SINH = RECORD.MA_TAI_SAN
                AND ROWNUM = 1;
          EXCEPTION
            WHEN NO_DATA_FOUND THEN
            trangThai:=NULL;
          END;
        END;
        UPDATE TS_TAI_SAN
        SET TRANG_THAI = trangThai
        WHERE MA_TAI_SAN = RECORD.MA_TAI_SAN;
        COMMIT;
    END LOOP;
END;


---Thống kê
select 
	sum(case when ts.ma_tai_san like '038%' then 1 else 0 end) as ts038,
	sum(case when ts.ma_tai_san like '026%' then 1 else 0 end) as ts026,
	sum(case when ts.ma_tai_san like '014%' then 1 else 0 end) as ts014,
	sum(case when ts.ma_tai_san like 'T06%' then 1 else 0 end) as tsT06,
	sum(case when ts.ma_tai_san like 'T07%' then 1 else 0 end) as tsT07,
	sum(case when ts.ma_tai_san like 'T17%' then 1 else 0 end) as tsT17,
	sum(case when ts.ma_tai_san like 'T30%' then 1 else 0 end) as tsT30,
	sum(case when ts.ma_tai_san like 'T33%' then 1 else 0 end) as tsT33,
	sum(case when ts.ma_tai_san like 'T48%' then 1 else 0 end) as tsT48,
	sum(case when ts.ma_tai_san like 'T52%' then 1 else 0 end) as tsT52,
	sum(case when ts.ma_tai_san like 'T58%' then 1 else 0 end) as tsT58,
	sum(case when ts.ma_tai_san like 'T55%' then 1 else 0 end) as tsT55,
	sum(case when ts.ma_tai_san like 'T05%' then 1 else 0 end) as tsT05,
	sum(case when ts.ma_tai_san like 'T04%' then 1 else 0 end) as tsT04,
	sum(case when ts.ma_tai_san like 'T03%' then 1 else 0 end) as tsT03,
	sum(case when ts.ma_tai_san like '004%' then 1 else 0 end) as ts004
from ts_nha nha
left join ts_tai_san ts on ts.ma_tai_san = nha.ma_tai_san
where ts.trang_thai <> '0';



select					
	sum(case when ts.ma_tai_san like '021%' then 1 else 0 end) as ts021	,
	sum(case when ts.ma_tai_san like '018%' then 1 else 0 end) as ts018	,
	sum(case when ts.ma_tai_san like '025%' then 1 else 0 end) as ts025	,
	sum(case when ts.ma_tai_san like '023%' then 1 else 0 end) as ts023	,
	sum(case when ts.ma_tai_san like '044%' then 1 else 0 end) as ts044	,
	sum(case when ts.ma_tai_san like '050%' then 1 else 0 end) as ts050	,
	sum(case when ts.ma_tai_san like 'T14%' then 1 else 0 end) as tsT14	,
	sum(case when ts.ma_tai_san like 'T13%' then 1 else 0 end) as tsT13	,
	sum(case when ts.ma_tai_san like 'T16%' then 1 else 0 end) as tsT16	,
	sum(case when ts.ma_tai_san like 'T29%' then 1 else 0 end) as tsT29	,
	sum(case when ts.ma_tai_san like 'T42%' then 1 else 0 end) as tsT42	,
	sum(case when ts.ma_tai_san like 'T44%' then 1 else 0 end) as tsT44	,
	sum(case when ts.ma_tai_san like 'T49%' then 1 else 0 end) as tsT49	,
	sum(case when ts.ma_tai_san like 'T59%' then 1 else 0 end) as tsT59	,
	sum(case when ts.ma_tai_san like '005%' then 1 else 0 end) as ts005	,
	sum(case when ts.ma_tai_san like '030%' then 1 else 0 end) as ts030	
from ts_nha nha					
left join ts_tai_san ts on ts.ma_tai_san = nha.ma_tai_san					





select 
	sum(case when ts.ma_tai_san like '021%' then dat.gia_tri else 0 end) as ts021,
	sum(case when ts.ma_tai_san like '018%' then dat.gia_tri else 0 end) as ts018,
	sum(case when ts.ma_tai_san like '025%' then dat.gia_tri else 0 end) as ts025,
	sum(case when ts.ma_tai_san like '023%' then dat.gia_tri else 0 end) as ts023,
	sum(case when ts.ma_tai_san like '044%' then dat.gia_tri else 0 end) as ts044,
	sum(case when ts.ma_tai_san like '050%' then dat.gia_tri else 0 end) as ts050,
	sum(case when ts.ma_tai_san like 'T14%' then dat.gia_tri else 0 end) as tsT14,
	sum(case when ts.ma_tai_san like 'T13%' then dat.gia_tri else 0 end) as tsT13,
	sum(case when ts.ma_tai_san like 'T16%' then dat.gia_tri else 0 end) as tsT16,
	sum(case when ts.ma_tai_san like 'T29%' then dat.gia_tri else 0 end) as tsT29,
	sum(case when ts.ma_tai_san like 'T42%' then dat.gia_tri else 0 end) as tsT42,
	sum(case when ts.ma_tai_san like 'T44%' then dat.gia_tri else 0 end) as tsT44,
	sum(case when ts.ma_tai_san like 'T49%' then dat.gia_tri else 0 end) as tsT49,
	sum(case when ts.ma_tai_san like 'T59%' then dat.gia_tri else 0 end) as tsT59,
	sum(case when ts.ma_tai_san like '005%' then dat.gia_tri else 0 end) as ts005,
	sum(case when ts.ma_tai_san like '030%' then dat.gia_tri else 0 end) as ts030
from ts_dat dat
left join ts_tai_san ts on ts.ma_tai_san = dat.ma_tai_san
where ts.trang_thai <> '0';


select 
	sum(case when ts.ma_tai_san like '021%' then nha.NGUYEN_GIA_NS + nha.NGUYEN_GIA_KHAC else 0 end) as ts021,
	sum(case when ts.ma_tai_san like '018%' then nha.NGUYEN_GIA_NS + nha.NGUYEN_GIA_KHAC else 0 end) as ts018,
	sum(case when ts.ma_tai_san like '025%' then nha.NGUYEN_GIA_NS + nha.NGUYEN_GIA_KHAC else 0 end) as ts025,
	sum(case when ts.ma_tai_san like '023%' then nha.NGUYEN_GIA_NS + nha.NGUYEN_GIA_KHAC else 0 end) as ts023,
	sum(case when ts.ma_tai_san like '044%' then nha.NGUYEN_GIA_NS + nha.NGUYEN_GIA_KHAC else 0 end) as ts044,
	sum(case when ts.ma_tai_san like '050%' then nha.NGUYEN_GIA_NS + nha.NGUYEN_GIA_KHAC else 0 end) as ts050,
	sum(case when ts.ma_tai_san like 'T14%' then nha.NGUYEN_GIA_NS + nha.NGUYEN_GIA_KHAC else 0 end) as tsT14,
	sum(case when ts.ma_tai_san like 'T13%' then nha.NGUYEN_GIA_NS + nha.NGUYEN_GIA_KHAC else 0 end) as tsT13,
	sum(case when ts.ma_tai_san like 'T16%' then nha.NGUYEN_GIA_NS + nha.NGUYEN_GIA_KHAC else 0 end) as tsT16,
	sum(case when ts.ma_tai_san like 'T29%' then nha.NGUYEN_GIA_NS + nha.NGUYEN_GIA_KHAC else 0 end) as tsT29,
	sum(case when ts.ma_tai_san like 'T42%' then nha.NGUYEN_GIA_NS + nha.NGUYEN_GIA_KHAC else 0 end) as tsT42,
	sum(case when ts.ma_tai_san like 'T44%' then nha.NGUYEN_GIA_NS + nha.NGUYEN_GIA_KHAC else 0 end) as tsT44,
	sum(case when ts.ma_tai_san like 'T49%' then nha.NGUYEN_GIA_NS + nha.NGUYEN_GIA_KHAC else 0 end) as tsT49,
	sum(case when ts.ma_tai_san like 'T59%' then nha.NGUYEN_GIA_NS + nha.NGUYEN_GIA_KHAC else 0 end) as tsT59,
	sum(case when ts.ma_tai_san like '005%' then nha.NGUYEN_GIA_NS + nha.NGUYEN_GIA_KHAC else 0 end) as ts005,
	sum(case when ts.ma_tai_san like '030%' then nha.NGUYEN_GIA_NS + nha.NGUYEN_GIA_KHAC else 0 end) as ts030
from ts_nha nha
left join ts_tai_san ts on ts.ma_tai_san = nha.ma_tai_san
where ts.trang_thai <> '0';




select 
	sum(case when ts.ma_tai_san like '021%' then oto.NGUYEN_GIA_NS + oto.NGUYEN_GIA_KHAC else 0 end) as ts021,
	sum(case when ts.ma_tai_san like '018%' then oto.NGUYEN_GIA_NS + oto.NGUYEN_GIA_KHAC else 0 end) as ts018,
	sum(case when ts.ma_tai_san like '025%' then oto.NGUYEN_GIA_NS + oto.NGUYEN_GIA_KHAC else 0 end) as ts025,
	sum(case when ts.ma_tai_san like '023%' then oto.NGUYEN_GIA_NS + oto.NGUYEN_GIA_KHAC else 0 end) as ts023,
	sum(case when ts.ma_tai_san like '044%' then oto.NGUYEN_GIA_NS + oto.NGUYEN_GIA_KHAC else 0 end) as ts044,
	sum(case when ts.ma_tai_san like '050%' then oto.NGUYEN_GIA_NS + oto.NGUYEN_GIA_KHAC else 0 end) as ts050,
	sum(case when ts.ma_tai_san like 'T14%' then oto.NGUYEN_GIA_NS + oto.NGUYEN_GIA_KHAC else 0 end) as tsT14,
	sum(case when ts.ma_tai_san like 'T13%' then oto.NGUYEN_GIA_NS + oto.NGUYEN_GIA_KHAC else 0 end) as tsT13,
	sum(case when ts.ma_tai_san like 'T16%' then oto.NGUYEN_GIA_NS + oto.NGUYEN_GIA_KHAC else 0 end) as tsT16,
	sum(case when ts.ma_tai_san like 'T29%' then oto.NGUYEN_GIA_NS + oto.NGUYEN_GIA_KHAC else 0 end) as tsT29,
	sum(case when ts.ma_tai_san like 'T42%' then oto.NGUYEN_GIA_NS + oto.NGUYEN_GIA_KHAC else 0 end) as tsT42,
	sum(case when ts.ma_tai_san like 'T44%' then oto.NGUYEN_GIA_NS + oto.NGUYEN_GIA_KHAC else 0 end) as tsT44,
	sum(case when ts.ma_tai_san like 'T49%' then oto.NGUYEN_GIA_NS + oto.NGUYEN_GIA_KHAC else 0 end) as tsT49,
	sum(case when ts.ma_tai_san like 'T59%' then oto.NGUYEN_GIA_NS + oto.NGUYEN_GIA_KHAC else 0 end) as tsT59,
	sum(case when ts.ma_tai_san like '005%' then oto.NGUYEN_GIA_NS + oto.NGUYEN_GIA_KHAC else 0 end) as ts005,
	sum(case when ts.ma_tai_san like '030%' then oto.NGUYEN_GIA_NS + oto.NGUYEN_GIA_KHAC else 0 end) as ts030
from ts_oto oto
left join ts_tai_san ts on ts.ma_tai_san = oto.ma_tai_san
where ts.trang_thai <> '0';



select 
	sum(case when ts.ma_tai_san like '021%' then khac.NGUYEN_GIA_NS + khac.NGUYEN_GIA_KHAC else 0 end) as ts021,
	sum(case when ts.ma_tai_san like '018%' then khac.NGUYEN_GIA_NS + khac.NGUYEN_GIA_KHAC else 0 end) as ts018,
	sum(case when ts.ma_tai_san like '025%' then khac.NGUYEN_GIA_NS + khac.NGUYEN_GIA_KHAC else 0 end) as ts025,
	sum(case when ts.ma_tai_san like '023%' then khac.NGUYEN_GIA_NS + khac.NGUYEN_GIA_KHAC else 0 end) as ts023,
	sum(case when ts.ma_tai_san like '044%' then khac.NGUYEN_GIA_NS + khac.NGUYEN_GIA_KHAC else 0 end) as ts044,
	sum(case when ts.ma_tai_san like '050%' then khac.NGUYEN_GIA_NS + khac.NGUYEN_GIA_KHAC else 0 end) as ts050,
	sum(case when ts.ma_tai_san like 'T14%' then khac.NGUYEN_GIA_NS + khac.NGUYEN_GIA_KHAC else 0 end) as tsT14,
	sum(case when ts.ma_tai_san like 'T13%' then khac.NGUYEN_GIA_NS + khac.NGUYEN_GIA_KHAC else 0 end) as tsT13,
	sum(case when ts.ma_tai_san like 'T16%' then khac.NGUYEN_GIA_NS + khac.NGUYEN_GIA_KHAC else 0 end) as tsT16,
	sum(case when ts.ma_tai_san like 'T29%' then khac.NGUYEN_GIA_NS + khac.NGUYEN_GIA_KHAC else 0 end) as tsT29,
	sum(case when ts.ma_tai_san like 'T42%' then khac.NGUYEN_GIA_NS + khac.NGUYEN_GIA_KHAC else 0 end) as tsT42,
	sum(case when ts.ma_tai_san like 'T44%' then khac.NGUYEN_GIA_NS + khac.NGUYEN_GIA_KHAC else 0 end) as tsT44,
	sum(case when ts.ma_tai_san like 'T49%' then khac.NGUYEN_GIA_NS + khac.NGUYEN_GIA_KHAC else 0 end) as tsT49,
	sum(case when ts.ma_tai_san like 'T59%' then khac.NGUYEN_GIA_NS + khac.NGUYEN_GIA_KHAC else 0 end) as tsT59,
	sum(case when ts.ma_tai_san like '005%' then khac.NGUYEN_GIA_NS + khac.NGUYEN_GIA_KHAC else 0 end) as ts005,
	sum(case when ts.ma_tai_san like '030%' then khac.NGUYEN_GIA_NS + khac.NGUYEN_GIA_KHAC else 0 end) as ts030
from ts_khac khac
left join ts_tai_san ts on ts.ma_tai_san = khac.ma_tai_san
where ts.trang_thai <> '0';


select 
	sum(case when ts.ma_tai_san like '021%' then duoi_500.NGUYEN_GIA_NS + duoi_500.NGUYEN_GIA_KHAC else 0 end) as ts021,
	sum(case when ts.ma_tai_san like '018%' then duoi_500.NGUYEN_GIA_NS + duoi_500.NGUYEN_GIA_KHAC else 0 end) as ts018,
	sum(case when ts.ma_tai_san like '025%' then duoi_500.NGUYEN_GIA_NS + duoi_500.NGUYEN_GIA_KHAC else 0 end) as ts025,
	sum(case when ts.ma_tai_san like '023%' then duoi_500.NGUYEN_GIA_NS + duoi_500.NGUYEN_GIA_KHAC else 0 end) as ts023,
	sum(case when ts.ma_tai_san like '044%' then duoi_500.NGUYEN_GIA_NS + duoi_500.NGUYEN_GIA_KHAC else 0 end) as ts044,
	sum(case when ts.ma_tai_san like '050%' then duoi_500.NGUYEN_GIA_NS + duoi_500.NGUYEN_GIA_KHAC else 0 end) as ts050,
	sum(case when ts.ma_tai_san like 'T14%' then duoi_500.NGUYEN_GIA_NS + duoi_500.NGUYEN_GIA_KHAC else 0 end) as tsT14,
	sum(case when ts.ma_tai_san like 'T13%' then duoi_500.NGUYEN_GIA_NS + duoi_500.NGUYEN_GIA_KHAC else 0 end) as tsT13,
	sum(case when ts.ma_tai_san like 'T16%' then duoi_500.NGUYEN_GIA_NS + duoi_500.NGUYEN_GIA_KHAC else 0 end) as tsT16,
	sum(case when ts.ma_tai_san like 'T29%' then duoi_500.NGUYEN_GIA_NS + duoi_500.NGUYEN_GIA_KHAC else 0 end) as tsT29,
	sum(case when ts.ma_tai_san like 'T42%' then duoi_500.NGUYEN_GIA_NS + duoi_500.NGUYEN_GIA_KHAC else 0 end) as tsT42,
	sum(case when ts.ma_tai_san like 'T44%' then duoi_500.NGUYEN_GIA_NS + duoi_500.NGUYEN_GIA_KHAC else 0 end) as tsT44,
	sum(case when ts.ma_tai_san like 'T49%' then duoi_500.NGUYEN_GIA_NS + duoi_500.NGUYEN_GIA_KHAC else 0 end) as tsT49,
	sum(case when ts.ma_tai_san like 'T59%' then duoi_500.NGUYEN_GIA_NS + duoi_500.NGUYEN_GIA_KHAC else 0 end) as tsT59,
	sum(case when ts.ma_tai_san like '005%' then duoi_500.NGUYEN_GIA_NS + duoi_500.NGUYEN_GIA_KHAC else 0 end) as ts005,
	sum(case when ts.ma_tai_san like '030%' then duoi_500.NGUYEN_GIA_NS + duoi_500.NGUYEN_GIA_KHAC else 0 end) as ts030
from ts_duoi_500 duoi_500
left join ts_tai_san ts on ts.ma_tai_san = duoi_500.ma_tai_san
where ts.trang_thai <> '0';



---------------------
SELECT
	MA_DON_VI AS MA,
	TEN_DON_VI AS TEN,
	MA_BO AS MA_BO,
	MA_DIA_BAN AS MA_DIA_BAN,
	MA_DON_VI_CU AS MA_DVQHNS,
	DIA_CHI AS DIA_CHI,
	DIEN_THOAI AS DIEN_THOAI,
	FAX AS FAX,
	MA_TINH AS MA_TINH,
	NHOM_DONVI_ID AS NHOM_DON_VI_ID,
	CAP AS CAP_DON_VI_ID,
	MA_HUYEN AS MA_HUYEN,
	CQTC_MA AS CQTC_MA,
	CHE_DO_HACH_TOAN AS CHE_DO_HACH_TOAN_ID,
	SO_QUYET_DINH AS SO_QUYET_DINH,
	NGAY_QUYET_DINH AS NGAY_QUYET_DINH,
	SO_QUYET_DINH_GIAO_VON AS SO_QUYET_DINH_GIAO_VON,
	NGAY_QUYET_DINH_GIAO_VON AS NGAY_QUYET_DINH_GIAO_VON,
	(case when MA_DON_VI_CHA is not null then
    'BEGIN_(select id
	from dm_don_vi
	where ma = '''||MA_DON_VI_CHA||''')_END'else null end) AS PARENT_ID,
	(case when LOAI_DON_VI is not null then
    'BEGIN_(select id 
	  from dm_loai_don_vi
	  where ma ='''|| LOAI_DON_VI||''')_END'else null end) AS LOAI_DON_VI_ID,
	(case when MA_DIA_BAN is not null then
    'BEGIN_(select id
	from dm_dia_ban
	where ma = '''||MA_DIA_BAN||'''
	order by trang_thai_id
	FETCH first row only)_END'else null end) AS DIA_BAN_ID,
	TRANG_THAI AS TRANG_THAI_ID,
	DON_VI_HIEN_THI AS DON_VI_HIEN_THI,
	LA_DON_VI_NHAP_LIEU AS LA_DON_VI_NHAP_LIEU,
	TRANG_THAI_THAY_DOI AS TRANG_THAI_THAY_DOI_ID,
	CO_TAI_SAN AS CO_TAI_SAN,
	KHONG_CHUYEN_MA AS KHONG_CHUYEN_MA,
	LA_BAN_QL_DU_AN AS LA_BAN_QL_DU_AN,
	LA_DON_VI_TU_CHU_TAI_CHINH AS LA_DON_VI_TU_CHU_TAI_CHINH,
	DA_CO_QUYET_DINH_GIAO_VON AS DA_CO_QUYET_DINH_GIAO_VON
FROM 
	DM_DONVI
WHERE
	MA_DON_VI NOT IN (SELECT MA_DON_VI FROM GS_TEMP);



----
select 'INSERT INTO GS_TEMP_TAI_SAN VALUES ('''||MA_QLDKTS40||''');'
from TS_TAI_SAN
where MA_QLDKTS40 IS NOT NULL;



-----
select 
'ALTER TABLE '||A.table_name||'
ADD CONSTRAINT '||A.constraint_name||' FOREIGN KEY
(
  '||uc.column_name||' 
)
REFERENCES '||c.table_name||'
(
  ID 
)
ENABLE;' as al
from all_constraints A 
JOIN all_constraints C ON A.owner = C.owner
                        AND A.r_constraint_name = C.constraint_name
left join user_cons_columns uc on uc.constraint_name = a.constraint_name
where A.table_name in ('BD_BIEN_DONG','BD_BIEN_DONG_CHI_TIET') AND A.CONSTRAINT_TYPE = 'R';

------
select 'ALTER TABLE TS_TAI_SAN DROP CONSTRAINT '||constraint_name||';' 
from all_constraints where table_name='TS_TAI_SAN' and CONSTRAINT_TYPE = 'R';






BEGIN
   FOR cur_rec IN (SELECT object_name, object_type
                     FROM user_objects
                    WHERE object_type IN
                             (
                              'PACKAGE',
                              'PROCEDURE',
                              'FUNCTION'
                             ))
   LOOP
      BEGIN
		EXECUTE IMMEDIATE    'DROP '
						  || cur_rec.object_type
						  || ' "'
						  || cur_rec.object_name
						  || '"';
      EXCEPTION
         WHEN OTHERS
         THEN
            DBMS_OUTPUT.put_line (   'FAILED: DROP '
                                  || cur_rec.object_type
                                  || ' "'
                                  || cur_rec.object_name
                                  || '"'
                                 );
      END;
   END LOOP;
END;


BEGIN
   FOR cur_rec IN (SELECT object_name, object_type
                     FROM user_objects
                    WHERE object_type IN
                             (
                              'PACKAGE',
                              'PROCEDURE',
                              'FUNCTION'
                             ))
   LOOP
      BEGIN
		EXECUTE IMMEDIATE    'ALTER '
						  || cur_rec.object_type
						  || ' "'
						  || cur_rec.object_name
						  || '" COMPILE';
      EXCEPTION
         WHEN OTHERS
         THEN
            DBMS_OUTPUT.put_line (   'FAILED: DROP '
                                  || cur_rec.object_type
                                  || ' "'
                                  || cur_rec.object_name
                                  || '"'
                                 );
      END;
   END LOOP;
END;