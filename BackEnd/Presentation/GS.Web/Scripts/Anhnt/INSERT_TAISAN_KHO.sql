create or replace PROCEDURE INSERT_TAISAN_KHO (
    P_JSON_STRING   IN    CLOB,
    MSS_OUT         OUT   SYS_REFCURSOR
) AS

    L_TAI_SAN                TS_TAI_SAN%ROWTYPE;
    L_BIEN_DONG_ID           NUMBER;
    L_JSON_OBJECT            JSON_OBJECT_T;
    L_BIEN_DONG_ARR          JSON_ARRAY_T;
    L_BIEN_DONG_OBJECT       JSON_OBJECT_T;
    L_HIEN_TRANG_ARR         JSON_ARRAY_T;
    L_HIEN_TRANG_OBJECT      JSON_OBJECT_T;
    L_NGUON_VON_ARR          JSON_ARRAY_T;
    L_NGUON_VON_OBJECT       JSON_OBJECT_T;
    L_KHAU_HAO_ARR           JSON_ARRAY_T;
    L_KHAU_HAO_OBJECT        JSON_OBJECT_T;
    L_HAO_MON_ARR            JSON_ARRAY_T;
    L_HAO_MON_OBJECT         JSON_OBJECT_T;
--object tài sản
    L_TS_DAT                 TS_TAI_SAN_DAT%ROWTYPE;
    L_TS_NHA                 TS_TAI_SAN_NHA%ROWTYPE;
    L_TS_VKT                 TS_TAI_SAN_VKT%ROWTYPE;
    L_TS_OTO                 TS_TAI_SAN_OTO%ROWTYPE;
    L_TS_PTK                 TS_TAI_SAN_OTO%ROWTYPE;
    L_TS_CLN                 TS_TAI_SAN_CLN%ROWTYPE;
    L_TS_MAYMOC              TS_TAI_SAN_MAY_MOC%ROWTYPE;
    L_TS_HUUHINH             TS_TAI_SAN_MAY_MOC%ROWTYPE;
    L_TS_VOHINH              TS_TAI_SAN_VO_HINH%ROWTYPE;
    L_TS_DACTHU              TS_TAI_SAN_MAY_MOC%ROWTYPE;
    L_BD                     BD_BIEN_DONG%ROWTYPE;
    L_BD_CT                  BD_BIEN_DONG_CHI_TIET%ROWTYPE;
    L_TS_HIEN_TRANG          TS_TAI_SAN_HIEN_TRANG_SU_DUNG%ROWTYPE;
    L_TEN_HIEN_TRANG         VARCHAR(1000);
    L_TS_NGUON_VON           TS_TAI_SAN_NGUON_VON%ROWTYPE;
    L_HAO_MON                KT_HAO_MON_TAI_SAN%ROWTYPE;
    L_KHAU_HAO               KT_KHAU_HAO_TAI_SAN%ROWTYPE; 
-- object tài sản json
    L_TS_DAT_OBJECT          JSON_OBJECT_T;
    L_TS_NHA_OBJECT          JSON_OBJECT_T;
    L_TS_VKT_OBJECT          JSON_OBJECT_T;
    L_TS_OTO_OBJECT          JSON_OBJECT_T;
    L_TS_PTK_OBJECT          JSON_OBJECT_T;
    L_TS_CLN_OBJECT          JSON_OBJECT_T;
    L_TS_MAYMOC_OBJECT       JSON_OBJECT_T;
    L_TS_HUUHINH_OBJECT      JSON_OBJECT_T;
    L_TS_VOHINH_OBJECT       JSON_OBJECT_T;
    L_TS_DACTHU_OBJECT       JSON_OBJECT_T;
--tai san
    L_EXC_FN                 NUMBER;
    L_ID                     NUMBER;
    L_BD_ID                  NUMBER;
    L_MA_DB                  VARCHAR(50);
    L_MA_TSDAT               VARCHAR(50);
    L_DONVI_MA               VARCHAR(50);
    L_DONVI_ID               NUMBER;
    L_NSX_MA                 VARCHAR(50);
    L_LOAI_HINH_TAI_SAN      NUMBER;
    L_LYDO_ID                NUMBER;
    L_NHANXE_MA              VARCHAR(50);
    L_NHANXE_ID              NUMBER;
    L_CHUCDANH_MA            VARCHAR(50);
    L_CHUCDANH_ID            NUMBER;
    L_LOAITAISAN_ID          NUMBER;
    L_HINH_THUC_MUA_SAM_ID   NUMBER;
    L_HINH_THUC_MUA_SAM_MA   VARCHAR(50);
    L_HINH_THUC_XU_LY_ID     NUMBER;
    L_HINH_THUC_XU_LY_MA     VARCHAR(50);
    L_MUC_DICH_SU_DUNG_ID    NUMBER;
    L_DONVI_DIEU_CHUYEN_ID   NUMBER;
    L_DONVI_DIEU_CHUYEN_MA   VARCHAR(50);
    L_BD_HTSD_JSON           CLOB;
    L_BD_NV_JSON             CLOB;
    L_HAO_MON_ID             NUMBER;
    L_KHAU_HAO_ID            NUMBER;
    L_NGUON_TAI_SAN_ID       NUMBER;
    L_NAM                    NUMBER;
    L_TSDAT_TENDIABAN        VARCHAR(4000);
BEGIN
    L_JSON_OBJECT := JSON_OBJECT_T(P_JSON_STRING);
    --l_JSON_OBJECT := JSON_OBJECT_T('{"TEN_TAI_SAN":"123, Xã Đa Phước, Huyện An Phú, Tỉnh An Giang","MA_TAI_SAN":"T11000-101-27181","LOAI_TAI_SAN_TEN":null,"LOAI_TAI_SAN_ID":558.0,"LOAI_TAI_SAN_DON_VI_ID":null,"NGAY_NHAP":null,"NGAY_DUYET":null,"NGAY_SU_DUNG":"2020-12-01T00:00:00","LY_DO_BIEN_DONG_MA":null,"LY_DO_BIEN_DONG_TEN":null,"MA_LOAI_TAI_SAN":"101","LOAI_HINH_TAI_SAN_ID":1.0,"NGUYEN_GIA_BAN_DAU":null,"MA_DON_VI":"T11000","TRANG_THAI":2.0,"GHI_CHU":null,"DON_VI_BO_PHAN_MA":null,"DON_VI_BO_PHAN_ID":null,"NUOC_SAN_XUAT_MA":null,"NAM_SAN_XUAT":null,"GIA_MUA_TIEP_NHAN":null,"GIA_HOA_DON":null,"QUYET_DINH_SO":null,"QUYET_DINH_NGAY":null,"CHUNG_TU_SO":null,"CHUNG_TU_NGAY":null,"MA_DU_AN":null,"LST_BIEN_DONG_JSON":null,"LST_HIEN_TRANG_JSON":null,"LST_NGUON_VON_JSON":null,"TAI_SAN_JSON":null,"CHE_DO_HAO_MON_ID":0.0,"MA_NHOM_DON_VI":null,"NAM":null,"DB_MA":"T11000-101-99999","CHE_DO_HAO_MON_ID_OLD":0.0,"TS_CLN":null,"TS_DAT":{"DIA_CHI":"123","DIA_BAN_MA":null,"DIA_BAN_ID":56617.0,"DIEN_TICH":150.0,"HS_CNQSD_SO":null,"HS_CNQSD_NGAY":null,"HS_QUYET_DINH_GIAO_SO":null,"HS_QUYET_DINH_GIAO_NGAY":null,"HS_CHUYEN_NHUONG_SD_SO":null,"HS_CHUYEN_NHUONG_SD_NGAY":null,"HS_QUYET_DINH_CHO_THUE_SO":null,"HS_QUYET_DINH_CHO_THUE_NGAY":null},"TS_NHA":null,"TS_VKT":null,"TS_OTO":null,"TS_PTK":null,"TS_MAY_MOC":null,"TS_DAC_THU":null,"TS_HUU_HINH_KHAC":null,"TS_VO_HINH":null,"LST_BIEN_DONG":[{"ID":null,"ID_DB":"17","LOAI_TAI_SAN_ID":558.0,"LOAI_TAI_SAN_DON_VI_ID":null,"TEN_TAI_SAN":"123, Xã Đa Phước, Huyện An Phú, Tỉnh An Giang","NGUYEN_GIA":100000000000.0,"DON_VI_BO_PHAN_ID":null,"DON_VI_BO_PHAN_MA":null,"CHUNG_TU_SO":null,"CHUNG_TU_NGAY":null,"NGAY_BIEN_DONG":"2020-12-01T00:00:00","NGAY_SU_DUNG":null,"LOAI_BIEN_DONG_ID":1.0,"LY_DO_BIEN_DONG_MA":"002","LY_DO_BIEN_DONG_ID":605.0,"LY_DO_BIEN_DONG_TEN":null,"LOAI_TAI_SAN_MA":null,"TRANG_THAI_ID":3.0,"NGAY_TAO":"2021-02-07T18:55:38.1441325+07:00","GUID":"00000000-0000-0000-0000-000000000000","GHI_CHU":null,"QUYET_DINH_NGAY":null,"NGAY_DUYET":"2020-12-13T00:00:00","QUYET_DINH_SO":null,"IS_BIENDONG_CUOI":null,"TRANG_THAI":null,"DON_VI_MA":"T11000","DATA_JSON":"{\"HS_CNQSD_SO\":null,\"HS_CNQSD_NGAY\":null,\"HS_QUYET_DINH_GIAO_SO\":null,\"HS_QUYET_DINH_GIAO_NGAY\":null,\"HS_CHUYEN_NHUONG_SD_SO\":null,\"HS_CHUYEN_NHUONG_SD_NGAY\":null,\"HS_QUYET_DINH_CHO_THUE_SO\":null,\"HS_QUYET_DINH_CHO_THUE_NGAY\":null,\"HS_KHAC\":null,\"HS_QUYET_DINH_BAN_GIAO\":null,\"HS_QUYET_DINH_BAN_GIAO_NGAY\":null,\"HS_HOP_DONG_CHO_THUE_SO\":null,\"HS_HOP_DONG_CHO_THUE_NGAY\":null,\"HS_PHAP_LY_KHAC\":null,\"HS_PHAP_LY_KHAC_NGAY\":null}","HTSD_JSON":"{\"TaiSanId\":null,\"lstObjHienTrang\":[{\"HienTrangId\":186.0,\"GiaTriText\":null,\"GiaTriNumber\":150.0,\"GiaTriCheckbox\":false,\"TenHienTrang\":\"Quản lý dự án\",\"KieuDuLieuId\":1.0,\"NhomHienTrangId\":null},{\"HienTrangId\":72.0,\"GiaTriText\":null,\"GiaTriNumber\":null,\"GiaTriCheckbox\":false,\"TenHienTrang\":\"Diện tích làm việc\",\"KieuDuLieuId\":1.0,\"NhomHienTrangId\":null},{\"HienTrangId\":73.0,\"GiaTriText\":null,\"GiaTriNumber\":null,\"GiaTriCheckbox\":false,\"TenHienTrang\":\"HĐSN-Không KD\",\"KieuDuLieuId\":1.0,\"NhomHienTrangId\":null},{\"HienTrangId\":75.0,\"GiaTriText\":null,\"GiaTriNumber\":null,\"GiaTriCheckbox\":false,\"TenHienTrang\":\"HĐSN-Kinh doanh\",\"KieuDuLieuId\":1.0,\"NhomHienTrangId\":null},{\"HienTrangId\":78.0,\"GiaTriText\":null,\"GiaTriNumber\":null,\"GiaTriCheckbox\":false,\"TenHienTrang\":\"HĐSN-Cho thuê\",\"KieuDuLieuId\":1.0,\"NhomHienTrangId\":null},{\"HienTrangId\":79.0,\"GiaTriText\":null,\"GiaTriNumber\":null,\"GiaTriCheckbox\":false,\"TenHienTrang\":\"HĐSN-LDLK\",\"KieuDuLieuId\":1.0,\"NhomHienTrangId\":null},{\"HienTrangId\":181.0,\"GiaTriText\":null,\"GiaTriNumber\":null,\"GiaTriCheckbox\":false,\"TenHienTrang\":\"Để ở\",\"KieuDuLieuId\":1.0,\"NhomHienTrangId\":null},{\"HienTrangId\":182.0,\"GiaTriText\":null,\"GiaTriNumber\":null,\"GiaTriCheckbox\":false,\"TenHienTrang\":\"Bỏ trống\",\"KieuDuLieuId\":1.0,\"NhomHienTrangId\":null},{\"HienTrangId\":183.0,\"GiaTriText\":null,\"GiaTriNumber\":null,\"GiaTriCheckbox\":false,\"TenHienTrang\":\"Bị lấn chiếm\",\"KieuDuLieuId\":1.0,\"NhomHienTrangId\":null},{\"HienTrangId\":81.0,\"GiaTriText\":null,\"GiaTriNumber\":null,\"GiaTriCheckbox\":false,\"TenHienTrang\":\"SD hỗn hợp\",\"KieuDuLieuId\":1.0,\"NhomHienTrangId\":null}]}","NGUON_VON_BD":[{"TEN_NGUON_VON":"Nguồn ngân sách","NGUON_VON_ID":1.0,"GIA_TRI":100000000000.0,"BIEN_DONG_GUID":"00000000-0000-0000-0000-000000000000"}],"HIEN_TRANG_BD":[{"HIEN_TRANG_ID":186.0,"BIEN_DONG_GUID":"00000000-0000-0000-0000-000000000000","NHOM_HIEN_TRANG_ID":null,"KIEU_DU_LIEU_ID":1.0,"TEN_HIEN_TRANG":"Quản lý dự án","GIA_TRI_TEXT":null,"GIA_TRI_NUMBER":150.0,"GIA_TRI_CHECKBOX":false},{"HIEN_TRANG_ID":72.0,"BIEN_DONG_GUID":"00000000-0000-0000-0000-000000000000","NHOM_HIEN_TRANG_ID":null,"KIEU_DU_LIEU_ID":1.0,"TEN_HIEN_TRANG":"Diện tích làm việc","GIA_TRI_TEXT":null,"GIA_TRI_NUMBER":null,"GIA_TRI_CHECKBOX":null},{"HIEN_TRANG_ID":73.0,"BIEN_DONG_GUID":"00000000-0000-0000-0000-000000000000","NHOM_HIEN_TRANG_ID":null,"KIEU_DU_LIEU_ID":1.0,"TEN_HIEN_TRANG":"HĐSN-Không KD","GIA_TRI_TEXT":null,"GIA_TRI_NUMBER":null,"GIA_TRI_CHECKBOX":null},{"HIEN_TRANG_ID":75.0,"BIEN_DONG_GUID":"00000000-0000-0000-0000-000000000000","NHOM_HIEN_TRANG_ID":null,"KIEU_DU_LIEU_ID":1.0,"TEN_HIEN_TRANG":"HĐSN-Kinh doanh","GIA_TRI_TEXT":null,"GIA_TRI_NUMBER":null,"GIA_TRI_CHECKBOX":null},{"HIEN_TRANG_ID":78.0,"BIEN_DONG_GUID":"00000000-0000-0000-0000-000000000000","NHOM_HIEN_TRANG_ID":null,"KIEU_DU_LIEU_ID":1.0,"TEN_HIEN_TRANG":"HĐSN-Cho thuê","GIA_TRI_TEXT":null,"GIA_TRI_NUMBER":null,"GIA_TRI_CHECKBOX":null},{"HIEN_TRANG_ID":79.0,"BIEN_DONG_GUID":"00000000-0000-0000-0000-000000000000","NHOM_HIEN_TRANG_ID":null,"KIEU_DU_LIEU_ID":1.0,"TEN_HIEN_TRANG":"HĐSN-LDLK","GIA_TRI_TEXT":null,"GIA_TRI_NUMBER":null,"GIA_TRI_CHECKBOX":null},{"HIEN_TRANG_ID":181.0,"BIEN_DONG_GUID":"00000000-0000-0000-0000-000000000000","NHOM_HIEN_TRANG_ID":null,"KIEU_DU_LIEU_ID":1.0,"TEN_HIEN_TRANG":"Để ở","GIA_TRI_TEXT":null,"GIA_TRI_NUMBER":null,"GIA_TRI_CHECKBOX":null},{"HIEN_TRANG_ID":182.0,"BIEN_DONG_GUID":"00000000-0000-0000-0000-000000000000","NHOM_HIEN_TRANG_ID":null,"KIEU_DU_LIEU_ID":1.0,"TEN_HIEN_TRANG":"Bỏ trống","GIA_TRI_TEXT":null,"GIA_TRI_NUMBER":null,"GIA_TRI_CHECKBOX":null},{"HIEN_TRANG_ID":183.0,"BIEN_DONG_GUID":"00000000-0000-0000-0000-000000000000","NHOM_HIEN_TRANG_ID":null,"KIEU_DU_LIEU_ID":1.0,"TEN_HIEN_TRANG":"Bị lấn chiếm","GIA_TRI_TEXT":null,"GIA_TRI_NUMBER":null,"GIA_TRI_CHECKBOX":null},{"HIEN_TRANG_ID":81.0,"BIEN_DONG_GUID":"00000000-0000-0000-0000-000000000000","NHOM_HIEN_TRANG_ID":null,"KIEU_DU_LIEU_ID":1.0,"TEN_HIEN_TRANG":"SD hỗn hợp","GIA_TRI_TEXT":null,"GIA_TRI_NUMBER":null,"GIA_TRI_CHECKBOX":null}],"HINH_THUC_MUA_SAM_MA":null,"MUC_DICH_SU_DUNG_MA":null,"MUC_DICH_SU_DUNG_ID":null,"NHAN_HIEU":null,"SO_HIEU":null,"DIA_BAN_MA":null,"DIA_BAN_ID":null,"DAT_TONG_DIEN_TICH":150.0,"HTSD_QUAN_LY_NHA_NUOC":null,"HTSD_HDSN_KINH_DOANH_KHONG":null,"HTSD_HDSN_KINH_DOANH":null,"HTSD_CHO_THUE":null,"HTSD_LIEN_DOANH":null,"HTSD_SU_DUNG_HON_HOP":null,"HTSD_SU_DUNG_KHAC":null,"HM_SO_NAM_CON_LAI":null,"HM_TY_LE_HAO_MON":null,"HM_LUY_KE":null,"GIA_TRI_CON_LAI":100000000000.0,"KH_NGAY_BAT_DAU":null,"KH_THANG_CON_LAI":null,"KH_GIA_TINH_KHAU_HAO":null,"KH_GIA_TRI_TRICH_THANG":null,"KH_LUY_KE":null,"KH_CON_LAI":null,"NHA_SO_TANG":null,"NHA_NAM_XAY_DUNG":null,"NHA_DIEN_TICH_XD":null,"NHA_TONG_DIEN_TICH_XD":null,"VKT_DIEN_TICH":150.0,"VKT_THE_TICH":null,"VKT_CHIEU_DAI":null,"OTO_BIEN_KIEM_SOAT":null,"OTO_SO_CHO_NGOI":null,"OTO_CHUC_DANH_MA":null,"OTO_NHAN_XE_MA":null,"OTO_TAI_TRONG":null,"OTO_CONG_XUAT":null,"OTO_XI_LANH":null,"OTO_SO_KHUNG":null,"OTO_SO_MAY":null,"THONG_SO_KY_THUAT":null,"CLN_SO_NAM":null,"PHI_THU":null,"PHI_BU_DAP":null,"PHI_NOP_NGAN_SACH":null,"PHI_KHAC":null,"DON_VI_NHAN_DIEU_CHUYEN_MA":null,"HINH_THUC_XU_LY_MA":null,"IS_BAN_THANH_LY":false,"DIEU_CHUYEN_KEM_THEO":null,"HS_CNQSD_SO":null,"HS_CNQSD_NGAY":null,"HS_QUYET_DINH_GIAO_SO":null,"HS_QUYET_DINH_GIAO_NGAY":null,"HS_CHUYEN_NHUONG_SD_SO":null,"HS_CHUYEN_NHUONG_SD_NGAY":null,"HS_QUYET_DINH_CHO_THUE_SO":null,"HS_QUYET_DINH_CHO_THUE_NGAY":null,"HS_KHAC":null,"HS_QUYET_DINH_BAN_GIAO":null,"HS_QUYET_DINH_BAN_GIAO_NGAY":null,"HS_HOP_DONG_CHO_THUE_SO":null,"HS_HOP_DONG_CHO_THUE_NGAY":null,"HS_PHAP_LY_KHAC":null,"HS_PHAP_LY_KHAC_NGAY":null,"DIA_CHI":"123","DAT_GIA_TRI_QUYEN_SD_DAT":null}],"LST_NGUON_VON":[],"LST_HIEN_TRANG":[],"LST_HAO_MON":null,"ID":27181.0}');
    L_MA_DB := L_JSON_OBJECT.GET_STRING('DB_MA');
    L_NGUON_TAI_SAN_ID := NVL(L_JSON_OBJECT.GET_NUMBER('NGUON_TAI_SAN_ID'), 2);
    ----DBMS_OUTPUT.PUT_LINE('l_ma_db=' || l_ma_db);  
    L_DONVI_MA := L_JSON_OBJECT.GET_STRING('MA_DON_VI');
    L_NSX_MA := L_JSON_OBJECT.GET_STRING('NUOC_SAN_XUAT_MA');
    L_LOAI_HINH_TAI_SAN := L_JSON_OBJECT.GET_NUMBER('LOAI_HINH_TAI_SAN_ID');
    L_LOAITAISAN_ID := L_JSON_OBJECT.GET_NUMBER('LOAI_TAI_SAN_ID');
    L_LYDO_ID := L_JSON_OBJECT.GET_NUMBER('LY_DO_BIEN_DONG_ID');
    BEGIN
        IF L_NGUON_TAI_SAN_ID = 2 THEN
            SELECT
                *
            INTO L_TAI_SAN
            FROM
                TS_TAI_SAN
            WHERE
                MA_DB = L_MA_DB
                AND NGUON_TAI_SAN_ID = 2;

        ELSE
            SELECT
                *
            INTO L_TAI_SAN
            FROM
                TS_TAI_SAN
            WHERE
                MA_QLDKTS40 = L_MA_DB
                AND NGUON_TAI_SAN_ID = L_NGUON_TAI_SAN_ID;

        END IF;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            L_TAI_SAN := NULL;
    END;

    IF L_TAI_SAN.ID IS NULL THEN
        L_TAI_SAN.GUID := SYS_GUID();
    ELSIF L_TAI_SAN.TRANG_THAI_ID <> 7 THEN
        L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_TAI_SAN.ID);
    END IF;
--BEGIN prepare TAI_SAN
--check loaiHinhTaiSan
   
--check biENDong
--l_bien_dong_arr :=JSON_ARRAY_T(REPLACE(l_JSON_OBJECT.get_Array('LST_BIEN_DONG').stringIFy,'\\\"','\"'));

    L_BIEN_DONG_ARR := L_JSON_OBJECT.GET_ARRAY('LST_BIEN_DONG');
--check donVi
    IF L_DONVI_MA IS NULL THEN
        OPEN MSS_OUT FOR SELECT
                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'MA_DON_VI IS NULL' ) AS STRJSON
                         FROM
                             DUAL;

        RETURN;
    ELSE
        BEGIN
            SELECT
                ID,
                MA
            INTO
                L_DONVI_ID,
                L_DONVI_MA
            FROM
                DM_DON_VI
            WHERE
                MA = L_DONVI_MA;

        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                OPEN MSS_OUT FOR SELECT
                                     JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'MA_DON_VI('
                                                                                      || L_DONVI_MA
                                                                                      || ') NOT FOUND' ) AS STRJSON
                                 FROM
                                     DUAL;

                RETURN;
        END;
    END IF;

    L_TAI_SAN.DON_VI_ID := L_DONVI_ID;
--trạng thái
    L_TAI_SAN.TEN := L_JSON_OBJECT.GET_STRING('TEN_TAI_SAN');
    L_TAI_SAN.MA_DB := L_MA_DB;
    IF L_JSON_OBJECT.GET_STRING('TRANG_THAI_ID') IS NULL THEN
        OPEN MSS_OUT FOR SELECT
                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TRANG_THAI_ID IS NULL' ) AS STRJSON
                         FROM
                             DUAL;

        RETURN;
    END IF;    
--

    L_TAI_SAN.QUYET_DINH_SO := L_JSON_OBJECT.GET_STRING('QUYET_DINH_SO');
    L_TAI_SAN.QUYET_DINH_NGAY := ( CASE
        WHEN L_TAI_SAN.QUYET_DINH_SO IS NULL THEN
            NULL
        ELSE L_JSON_OBJECT.GET_DATE('QUYET_DINH_NGAY')
    END );

    L_TAI_SAN.GHI_CHU := L_JSON_OBJECT.GET_STRING('GHI_CHU');
    L_TAI_SAN.CHUNG_TU_NGAY := L_JSON_OBJECT.GET_DATE('CHUNG_TU_NGAY');
    L_TAI_SAN.CHUNG_TU_SO := L_JSON_OBJECT.GET_STRING('CHUNG_TU_SO');
    L_TAI_SAN.NGUYEN_GIA_BAN_DAU := NVL(L_JSON_OBJECT.GET_NUMBER('NGUYEN_GIA_BAN_DAU'), 0);
    L_TAI_SAN.NAM_SAN_XUAT := L_JSON_OBJECT.GET_NUMBER('NAM_SAN_XUAT');
    L_TAI_SAN.LOAI_HINH_TAI_SAN_ID := L_JSON_OBJECT.GET_NUMBER('LOAI_HINH_TAI_SAN_ID');
    L_TAI_SAN.NGAY_SU_DUNG := L_JSON_OBJECT.GET_DATE('NGAY_SU_DUNG');
    L_TAI_SAN.NGAY_NHAP := L_JSON_OBJECT.GET_DATE('NGAY_NHAP');
    L_TAI_SAN.NGAY_DUYET := L_JSON_OBJECT.GET_DATE('NGAY_DUYET');
    L_TAI_SAN.GIA_MUA_TIEP_NHAN := L_JSON_OBJECT.GET_NUMBER('GIA_MUA_TIEP_NHAN');
    L_TAI_SAN.DON_VI_BO_PHAN_ID := L_JSON_OBJECT.GET_NUMBER('DON_VI_BO_PHAN_ID');
    L_TAI_SAN.LOAI_TAI_SAN_ID := ( CASE
        WHEN L_JSON_OBJECT.GET_NUMBER('LOAI_TAI_SAN_ID') = 0 THEN
            NULL
        ELSE L_JSON_OBJECT.GET_NUMBER('LOAI_TAI_SAN_ID')
    END );

    L_TAI_SAN.LOAI_TAI_SAN_DON_VI_ID := ( CASE
        WHEN L_JSON_OBJECT.GET_NUMBER('LOAI_TAI_SAN_DON_VI_ID') = 0 THEN
            NULL
        ELSE L_JSON_OBJECT.GET_NUMBER('LOAI_TAI_SAN_DON_VI_ID')
    END );

    L_TAI_SAN.GIA_HOA_DON := L_JSON_OBJECT.GET_NUMBER('GIA_HOA_DON');
    L_TAI_SAN.NUOC_SAN_XUAT_ID := L_JSON_OBJECT.GET_NUMBER('NUOC_SAN_XUAT_ID');
    L_TAI_SAN.MIEN_THUE_SO_TIEN := L_JSON_OBJECT.GET_NUMBER('MIEN_THUE_SO_TIEN');
    L_TAI_SAN.TRANG_THAI_ID := L_JSON_OBJECT.GET_NUMBER('TRANG_THAI_ID');
    IF L_TAI_SAN.MIEN_THUE_SO_TIEN IS NOT NULL THEN
        L_TAI_SAN.IS_MIEN_THUE := 1;
    ELSE
        L_TAI_SAN.IS_MIEN_THUE := 0;
    END IF;
--END prepare TAI_SAN

    IF L_TAI_SAN.ID IS NULL THEN
        --INSERT tài sản
        INSERT INTO TS_TAI_SAN (
            TEN,
            LOAI_TAI_SAN_ID,
            DU_AN_ID,
            LOAI_HINH_TAI_SAN_ID,
            TRANG_THAI_ID,
            QUYET_DINH_SO,
            QUYET_DINH_NGAY,
            NUOC_SAN_XUAT_ID,
            LY_DO_BIEN_DONG_ID,
            DOI_TAC_ID,
            NGAY_DUYET,
            NAM_SAN_XUAT,
            NGAY_NHAP,
            NGAY_SU_DUNG,
            GHI_CHU,
            DON_VI_BO_PHAN_ID,
            DON_VI_ID,
            NGAY_TAO,
            GUID,
            CHUNG_TU_SO,
            CHUNG_TU_NGAY,
            NGUYEN_GIA_BAN_DAU,
            GIA_MUA_TIEP_NHAN,
            LOAI_TAI_SAN_DON_VI_ID,
            MA_DB,
            MA_QLDKTS40,
            MIEN_THUE_SO_TIEN,
            IS_MIEN_THUE,
            NGUON_TAI_SAN_ID
        ) VALUES (
            L_TAI_SAN.TEN,
            L_TAI_SAN.LOAI_TAI_SAN_ID,
            L_TAI_SAN.DU_AN_ID,
            L_TAI_SAN.LOAI_HINH_TAI_SAN_ID,
            L_TAI_SAN.TRANG_THAI_ID,
            L_TAI_SAN.QUYET_DINH_SO,
            L_TAI_SAN.QUYET_DINH_NGAY,
            L_TAI_SAN.NUOC_SAN_XUAT_ID,
            L_TAI_SAN.LY_DO_BIEN_DONG_ID,
            L_TAI_SAN.DOI_TAC_ID,
            L_TAI_SAN.NGAY_DUYET,
            L_TAI_SAN.NAM_SAN_XUAT,
            L_TAI_SAN.NGAY_NHAP,
            L_TAI_SAN.NGAY_SU_DUNG,
            L_TAI_SAN.GHI_CHU,
            L_TAI_SAN.DON_VI_BO_PHAN_ID,
            L_TAI_SAN.DON_VI_ID,
            CURRENT_DATE,
            SYS_GUID(),
            L_TAI_SAN.CHUNG_TU_SO,
            L_TAI_SAN.CHUNG_TU_NGAY,
            L_TAI_SAN.NGUYEN_GIA_BAN_DAU,
            L_TAI_SAN.GIA_MUA_TIEP_NHAN,
            L_TAI_SAN.LOAI_TAI_SAN_DON_VI_ID,
            (
                CASE
                    WHEN L_NGUON_TAI_SAN_ID = 2 THEN
                        L_TAI_SAN.MA_DB
                    ELSE
                        NULL
                END
            ),
            (
                CASE
                    WHEN L_NGUON_TAI_SAN_ID <> 2 THEN
                        L_TAI_SAN.MA_DB
                    ELSE
                        NULL
                END
            ),
            L_TAI_SAN.MIEN_THUE_SO_TIEN,
            L_TAI_SAN.IS_MIEN_THUE,
            L_NGUON_TAI_SAN_ID
        ) RETURNING ID INTO L_ID;

        IF L_TAI_SAN.LOAI_TAI_SAN_ID IS NOT NULL THEN
            SELECT
                L_DONVI_MA
                || '-'
                || MA
                || '-'
                || L_ID
            INTO L_TAI_SAN.MA
            FROM
                DM_LOAI_TAI_SAN
            WHERE
                ID = L_TAI_SAN.LOAI_TAI_SAN_ID;

            UPDATE TS_TAI_SAN
            SET
                MA = L_TAI_SAN.MA
            WHERE
                ID = L_ID;

        ELSIF L_TAI_SAN.LOAI_TAI_SAN_DON_VI_ID IS NOT NULL THEN
            SELECT
                L_DONVI_MA
                || '-'
                || MA
                || '-'
                || L_ID
            INTO L_TAI_SAN.MA
            FROM
                DM_LOAI_TAI_SAN_DON_VI
            WHERE
                ID = L_TAI_SAN.LOAI_TAI_SAN_DON_VI_ID;

            UPDATE TS_TAI_SAN
            SET
                MA = L_TAI_SAN.MA
            WHERE
                ID = L_ID;

        END IF;

    ELSE
        L_ID := L_TAI_SAN.ID;
        UPDATE TS_TAI_SAN
        SET
            LOAI_TAI_SAN_ID = L_TAI_SAN.LOAI_TAI_SAN_ID,
            LOAI_HINH_TAI_SAN_ID = L_TAI_SAN.LOAI_HINH_TAI_SAN_ID,
            TRANG_THAI_ID = L_TAI_SAN.TRANG_THAI_ID,
            QUYET_DINH_SO = L_TAI_SAN.QUYET_DINH_SO,
            QUYET_DINH_NGAY = L_TAI_SAN.QUYET_DINH_NGAY,
            NUOC_SAN_XUAT_ID = L_TAI_SAN.NUOC_SAN_XUAT_ID,
            LY_DO_BIEN_DONG_ID = L_TAI_SAN.LY_DO_BIEN_DONG_ID,
            NGAY_DUYET = L_TAI_SAN.NGAY_DUYET,
            NAM_SAN_XUAT = L_TAI_SAN.NAM_SAN_XUAT,
            NGAY_NHAP = L_TAI_SAN.NGAY_NHAP,
            NGAY_SU_DUNG = L_TAI_SAN.NGAY_SU_DUNG,
            GHI_CHU = L_TAI_SAN.GHI_CHU,
            DON_VI_BO_PHAN_ID = L_TAI_SAN.DON_VI_BO_PHAN_ID,
            DON_VI_ID = L_TAI_SAN.DON_VI_ID,
            CHUNG_TU_SO = L_TAI_SAN.CHUNG_TU_SO,
            CHUNG_TU_NGAY = L_TAI_SAN.CHUNG_TU_NGAY,
            NGUYEN_GIA_BAN_DAU = L_TAI_SAN.NGUYEN_GIA_BAN_DAU,
            GIA_MUA_TIEP_NHAN = L_TAI_SAN.GIA_MUA_TIEP_NHAN,
            LOAI_TAI_SAN_DON_VI_ID = L_TAI_SAN.LOAI_TAI_SAN_DON_VI_ID,
            -- MA_DB = l_ma_db,
            GIA_HOA_DON = L_TAI_SAN.GIA_HOA_DON,
            MIEN_THUE_SO_TIEN = L_TAI_SAN.MIEN_THUE_SO_TIEN,
            IS_MIEN_THUE = L_TAI_SAN.IS_MIEN_THUE,
            MA_DB = (
                CASE
                    WHEN L_NGUON_TAI_SAN_ID = 2 THEN
                        L_TAI_SAN.MA_DB
                    ELSE
                        NULL
                END
            ),
            MA_QLDKTS40=(
                CASE
                    WHEN L_NGUON_TAI_SAN_ID != 2 THEN
                        L_TAI_SAN.MA_DB
                    ELSE
                        NULL
                END
            ),
        WHERE
            ID = L_ID;

    END IF;

    COMMIT;
    ----DBMS_OUTPUT.PUT_LINE('l_id=' || l_id);
--INSERT tài sản nhật ký
    IF L_NGUON_TAI_SAN_ID <> 2 THEN
        INSERT INTO DB_TAI_SAN_NHAT_KY (
            TAI_SAN_ID,
            LOAI_HINH_TAI_SAN_ID,
            TRANG_THAI_ID,
            NGAY_CAP_NHAT,
            NGAY_DONG_BO,
            IS_TAI_SAN_TOAN_DAN
        ) VALUES (
            L_ID,
            L_LOAI_HINH_TAI_SAN,
            1,
            CURRENT_DATE,
            CURRENT_DATE,
            0
        );

    END IF;
    
    
--BEGIN prepare từng loại tài sản

    IF L_LOAI_HINH_TAI_SAN = 1 THEN --enumLOAI_HINH_TAI_SAN.DAT
        L_TS_DAT_OBJECT := L_JSON_OBJECT.GET_OBJECT('TS_DAT');
        IF L_TS_DAT_OBJECT IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_DAT IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
            RETURN;
        END IF;

        L_TS_DAT.DIA_BAN_ID := L_TS_DAT_OBJECT.GET_NUMBER('DIA_BAN_ID');
        L_TS_DAT.DIA_CHI := L_TS_DAT_OBJECT.GET_STRING('DIA_CHI');
        --[TENTRUSO],[DIACHI],[XAPHUONG], [QUANHUYEN], [TINHTHANH]
        IF L_TS_DAT.DIA_CHI IS NOT NULL THEN
            L_TAI_SAN.TEN := L_TAI_SAN.TEN
                             || ','
                             || L_TS_DAT.DIA_CHI;
        END IF;

        IF L_TS_DAT.DIA_BAN_ID IS NOT NULL THEN
            SELECT
                LISTAGG(DM_DIA_BAN.TEN, ', ') WITHIN GROUP(
                    ORDER BY
                        DM_DIA_BAN.TREE_NODE DESC
                ) AS DESCRIPTION
            INTO L_TSDAT_TENDIABAN
            FROM
                DM_DIA_BAN
            START WITH
                ID = L_TS_DAT.DIA_BAN_ID
            CONNECT BY
                PRIOR PARENT_ID = ID;

            L_TAI_SAN.TEN := L_TAI_SAN.TEN
                             || ','
                             || L_TSDAT_TENDIABAN;
        END IF;

        L_TS_DAT.DIEN_TICH := L_TS_DAT_OBJECT.GET_NUMBER('DIEN_TICH');
        L_TS_DAT.TAI_SAN_ID := L_ID;
        
    --INSERT tai san dat
        INSERT INTO TS_TAI_SAN_DAT (
            TAI_SAN_ID,
            DIA_CHI,
            DIA_BAN_ID,
            DIEN_TICH
        ) VALUES (
            L_TS_DAT.TAI_SAN_ID,
            L_TS_DAT.DIA_CHI,
            L_TS_DAT.DIA_BAN_ID,
            L_TS_DAT.DIEN_TICH
        );

    ELSIF L_LOAI_HINH_TAI_SAN = 7 THEN --enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV
        L_TS_CLN_OBJECT := L_JSON_OBJECT.GET_OBJECT('TS_CLN');
        IF L_TS_CLN_OBJECT IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_CLN IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
            UPDATE TS_TAI_SAN
            SET
                TRANG_THAI_ID = 7
            WHERE
                ID = L_ID;

            RETURN;
        END IF;

        IF L_TS_CLN_OBJECT.GET_NUMBER('NAM_SINH') IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_CLN-NAM_SINH IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
            UPDATE TS_TAI_SAN
            SET
                TRANG_THAI_ID = 7
            WHERE
                ID = L_ID;

            RETURN;
        END IF;

        L_TS_CLN.TAI_SAN_ID := L_ID;
        L_TS_CLN.NAM_SINH := L_TS_CLN_OBJECT.GET_NUMBER('NAM_SINH');
        --INSERT cay lau nam
        INSERT INTO TS_TAI_SAN_CLN (
            TAI_SAN_ID,
            NAM_SINH
        ) VALUES (
            L_TS_CLN.TAI_SAN_ID,
            L_TS_CLN.NAM_SINH
        );

    ELSIF L_LOAI_HINH_TAI_SAN = 2 THEN --enumLOAI_HINH_TAI_SAN.NHA
        L_TS_NHA_OBJECT := L_JSON_OBJECT.GET_OBJECT('TS_NHA');
        IF L_TS_NHA_OBJECT IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_NHA IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
            UPDATE TS_TAI_SAN
            SET
                TRANG_THAI_ID = 7
            WHERE
                ID = L_ID;

            RETURN;
        END IF;

        IF L_TS_NHA_OBJECT.GET_NUMBER('DIEN_TICH_SAN_XAY_DUNG') IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_NHA-DIEN_TICH_SAN_XAY_DUNG IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
            UPDATE TS_TAI_SAN
            SET
                TRANG_THAI_ID = 7
            WHERE
                ID = L_ID;

            RETURN;
        END IF;

        L_TS_NHA.DIEN_TICH_SAN_XAY_DUNG := L_TS_NHA_OBJECT.GET_NUMBER('DIEN_TICH_SAN_XAY_DUNG');
        IF L_TS_NHA_OBJECT.GET_NUMBER('NAM_XAY_DUNG') IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_NHA-NAM_XAY_DUNG IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
            UPDATE TS_TAI_SAN
            SET
                TRANG_THAI_ID = 7
            WHERE
                ID = L_ID;

            RETURN;
        END IF;

        L_TS_NHA.NAM_XAY_DUNG := L_TS_NHA_OBJECT.GET_NUMBER('NAM_XAY_DUNG');
        L_MA_TSDAT := L_TS_NHA_OBJECT.GET_STRING('TAI_SAN_DAT_MA');
        L_TS_DAT.TAI_SAN_ID := L_TS_NHA_OBJECT.GET_NUMBER('TAI_SAN_DAT_ID');
        --Lấy tài sản đất id
        IF L_TS_DAT.TAI_SAN_ID = 0 THEN
            BEGIN
                IF L_MA_TSDAT IS NOT NULL THEN
                    BEGIN
                        IF L_NGUON_TAI_SAN_ID = 2 THEN
                            SELECT
                                TS.ID
                            INTO L_TS_DAT.TAI_SAN_ID
                            FROM
                                TS_TAI_SAN_DAT   TSDAT,
                                TS_TAI_SAN       TS
                            WHERE
                                TSDAT.TAI_SAN_ID = TS.ID
                                AND TS.MA_DB = L_MA_TSDAT
                                AND NGUON_TAI_SAN_ID = 2
                                AND ROWNUM = 1;

                        ELSE
                            SELECT
                                TS.ID
                            INTO L_TS_DAT.TAI_SAN_ID
                            FROM
                                TS_TAI_SAN_DAT   TSDAT,
                                TS_TAI_SAN       TS
                            WHERE
                                TSDAT.TAI_SAN_ID = TS.ID
                                AND TS.MA_QLDKTS40 = L_MA_TSDAT
                                AND NGUON_TAI_SAN_ID = 2
                                AND ROWNUM = 1
                                AND NGUON_TAI_SAN_ID = L_NGUON_TAI_SAN_ID;

                        END IF;

                    EXCEPTION
                        WHEN NO_DATA_FOUND THEN
                            L_TS_DAT.TAI_SAN_ID := NULL;
                    END;

                END IF;

            END;
        END IF;

        L_TS_NHA.TAI_SAN_DAT_ID := L_TS_DAT.TAI_SAN_ID;
        L_TS_NHA.DIA_CHI := NVL(L_TS_NHA_OBJECT.GET_STRING('DIA_CHI'), NVL(L_TS_DAT.DIA_CHI, 'Việt Nam'));

        L_TS_NHA.NGAY_SU_DUNG := L_TS_NHA_OBJECT.GET_DATE('NGAY_SU_DUNG');
        L_TS_NHA.TAI_SAN_ID := L_ID;
        --INSERT tai san nha
        INSERT INTO TS_TAI_SAN_NHA (
            TAI_SAN_ID,
            TAI_SAN_DAT_ID,
            DIEN_TICH_SAN_XAY_DUNG,
            NAM_XAY_DUNG,
            NGAY_SU_DUNG,
            DIA_CHI,
            MA_DB_DAT
        ) VALUES (
            L_TS_NHA.TAI_SAN_ID,
            L_TS_NHA.TAI_SAN_DAT_ID,
            L_TS_NHA.DIEN_TICH_SAN_XAY_DUNG,
            L_TS_NHA.NAM_XAY_DUNG,
            L_TS_NHA.NGAY_SU_DUNG,
            L_TS_NHA.DIA_CHI,
            L_MA_TSDAT
        );

    ELSIF L_LOAI_HINH_TAI_SAN = 4 THEN --enumLOAI_HINH_TAI_SAN.OTO
        L_TS_OTO_OBJECT := L_JSON_OBJECT.GET_OBJECT('TS_OTO');
        IF L_TS_OTO_OBJECT IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_OTO IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
            UPDATE TS_TAI_SAN
            SET
                TRANG_THAI_ID = 7
            WHERE
                ID = L_ID;

            RETURN;
        END IF;

        IF L_TS_OTO_OBJECT.GET_STRING('BIEN_KIEM_SOAT') IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_OTO-BIEN_KIEM_SOAT IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
            RETURN;
        END IF;

        L_TS_OTO.BIEN_KIEM_SOAT := L_TS_OTO_OBJECT.GET_STRING('BIEN_KIEM_SOAT');
        IF L_TS_OTO_OBJECT.GET_NUMBER('TAI_TRONG') IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_OTO-TAI_TRONG IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
            UPDATE TS_TAI_SAN
            SET
                TRANG_THAI_ID = 7
            WHERE
                ID = L_ID;

            RETURN;
        END IF;

        L_TS_OTO.TAI_TRONG := L_TS_OTO_OBJECT.GET_NUMBER('TAI_TRONG');
        IF L_TS_OTO_OBJECT.GET_NUMBER('SO_CHO_NGOI') IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_OTO-SO_CHO_NGOI IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
            UPDATE TS_TAI_SAN
            SET
                TRANG_THAI_ID = 7
            WHERE
                ID = L_ID;

            RETURN;
        END IF;

        L_TS_OTO.SO_CHO_NGOI := L_TS_OTO_OBJECT.GET_NUMBER('SO_CHO_NGOI');
        L_TS_OTO.CONG_XUAT := L_TS_OTO_OBJECT.GET_NUMBER('CONG_XUAT');
        L_TS_OTO.SO_KHUNG := L_TS_OTO_OBJECT.GET_STRING('SO_KHUNG');
        L_TS_OTO.SO_MAY := L_TS_OTO_OBJECT.GET_STRING('SO_MAY');
        L_NHANXE_MA := L_TS_OTO_OBJECT.GET_STRING('NHAN_XE_MA');
        IF L_NHANXE_MA IS NOT NULL THEN
            BEGIN
                SELECT
                    ID
                INTO L_NHANXE_ID
                FROM
                    DM_NHAN_XE
                WHERE
                    MA = L_NHANXE_MA
                    AND ROWNUM = 1;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    L_NHANXE_ID := NULL;
            END;

            L_TS_OTO.NHAN_XE_ID := L_NHANXE_ID;
        END IF;

        L_CHUCDANH_MA := L_TS_OTO_OBJECT.GET_STRING('CHUC_DANH_MA');
        ----DBMS_OUTPUT.PUT_LINE('CHUC_DANH_MA=' || l_chucdanh_ma);
        IF L_CHUCDANH_MA IS NOT NULL THEN
            BEGIN
                SELECT
                    ID
                INTO L_CHUCDANH_ID
                FROM
                    DM_CHUC_DANH
                WHERE
                    MA_CHUC_DANH = L_CHUCDANH_MA
                    AND ROWNUM = 1;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    L_CHUCDANH_ID := NULL;
            END;

            L_TS_OTO.CHUC_DANH_ID := L_CHUCDANH_ID;
        END IF;
        ----DBMS_OUTPUT.PUT_LINE('l_chucdanh_id=' || l_chucdanh_id);
        ----DBMS_OUTPUT.PUT_LINE('l_ts_oto.CHUC_DANH_ID=' || l_ts_oto.CHUC_DANH_ID);

        L_TS_OTO.TAI_SAN_ID := L_ID;
        --INSERT oto
        INSERT INTO TS_TAI_SAN_OTO (
            TAI_SAN_ID,
            BIEN_KIEM_SOAT,
            SO_CHO_NGOI,
            DUNG_TICH,
            CHUC_DANH_ID,
            TAI_TRONG,
            SO_KHUNG,
            NHAN_XE_ID,
            CONG_XUAT,
            SO_MAY
        ) VALUES (
            L_TS_OTO.TAI_SAN_ID,
            L_TS_OTO.BIEN_KIEM_SOAT,
            L_TS_OTO.SO_CHO_NGOI,
            L_TS_OTO.DUNG_TICH,
            L_TS_OTO.CHUC_DANH_ID,
            L_TS_OTO.TAI_TRONG,
            L_TS_OTO.SO_KHUNG,
            L_TS_OTO.NHAN_XE_ID,
            L_TS_OTO.CONG_XUAT,
            L_TS_OTO.SO_MAY
        );

    ELSIF L_LOAI_HINH_TAI_SAN = 5 THEN --enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC
        L_TS_PTK_OBJECT := L_JSON_OBJECT.GET_OBJECT('TS_PTK');
        IF L_TS_PTK_OBJECT IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_PTK IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
            UPDATE TS_TAI_SAN
            SET
                TRANG_THAI_ID = 7
            WHERE
                ID = L_ID;

            RETURN;
        END IF;

        IF L_TS_PTK_OBJECT.GET_NUMBER('TAI_TRONG') IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_OTO-TAI_TRONG IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
            UPDATE TS_TAI_SAN
            SET
                TRANG_THAI_ID = 7
            WHERE
                ID = L_ID;

            RETURN;
        END IF;

        L_TS_PTK.TAI_TRONG := L_TS_PTK_OBJECT.GET_NUMBER('TAI_TRONG');
        IF L_TS_PTK_OBJECT.GET_NUMBER('SO_CHO_NGOI') IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_OTO-SO_CHO_NGOI IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
            UPDATE TS_TAI_SAN
            SET
                TRANG_THAI_ID = 7
            WHERE
                ID = L_ID;

            RETURN;
        END IF;

        L_TS_PTK.SO_CHO_NGOI := L_TS_PTK_OBJECT.GET_NUMBER('SO_CHO_NGOI');
        L_TS_PTK.CONG_XUAT := L_TS_PTK_OBJECT.GET_NUMBER('CONG_XUAT');
        L_TS_PTK.SO_KHUNG := L_TS_PTK_OBJECT.GET_STRING('SO_KHUNG');
        L_TS_PTK.SO_MAY := L_TS_PTK_OBJECT.GET_STRING('SO_MAY');
        L_NHANXE_MA := L_TS_PTK_OBJECT.GET_STRING('NHAN_XE_MA');
        IF L_NHANXE_MA IS NOT NULL THEN
            BEGIN
                SELECT
                    ID
                INTO L_NHANXE_ID
                FROM
                    DM_NHAN_XE
                WHERE
                    MA = L_NHANXE_MA
                    AND ROWNUM = 1;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    L_NHANXE_ID := NULL;
            END;

            L_TS_PTK.NHAN_XE_ID := L_NHANXE_ID;
        END IF;

        L_CHUCDANH_MA := L_TS_PTK_OBJECT.GET_STRING('CHUC_DANH_MA');
        IF L_CHUCDANH_MA IS NOT NULL THEN
            BEGIN
                SELECT
                    ID
                INTO L_CHUCDANH_ID
                FROM
                    DM_CHUC_DANH
                WHERE
                    MA_CHUC_DANH = L_CHUCDANH_MA
                    AND ROWNUM = 1;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    L_CHUCDANH_ID := NULL;
            END;

            L_TS_PTK.CHUC_DANH_ID := L_CHUCDANH_ID;
        END IF;

        L_TS_PTK.TAI_SAN_ID := L_ID;
        --INSERT ptk
        INSERT INTO TS_TAI_SAN_OTO (
            TAI_SAN_ID,
            BIEN_KIEM_SOAT,
            SO_CHO_NGOI,
            DUNG_TICH,
            CHUC_DANH_ID,
            TAI_TRONG,
            SO_KHUNG,
            NHAN_XE_ID,
            CONG_XUAT,
            SO_MAY
        ) VALUES (
            L_TS_PTK.TAI_SAN_ID,
            NULL,
            L_TS_PTK.SO_CHO_NGOI,
            L_TS_PTK.DUNG_TICH,
            L_TS_PTK.CHUC_DANH_ID,
            L_TS_PTK.TAI_TRONG,
            L_TS_PTK.SO_KHUNG,
            L_TS_PTK.NHAN_XE_ID,
            L_TS_PTK.CONG_XUAT,
            L_TS_PTK.SO_MAY
        );

    ELSIF L_LOAI_HINH_TAI_SAN = 3 THEN --enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC
        L_TS_VKT_OBJECT := L_JSON_OBJECT.GET_OBJECT('TS_VKT');
        IF L_TS_VKT_OBJECT IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_VKT IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
            UPDATE TS_TAI_SAN
            SET
                TRANG_THAI_ID = 7
            WHERE
                ID = L_ID;

            RETURN;
        END IF;

        L_TS_VKT.CHIEU_DAI := NVL(L_TS_VKT_OBJECT.GET_NUMBER('CHIEU_DAI'), 0);
        L_TS_VKT.THE_TICH := NVL(L_TS_VKT_OBJECT.GET_NUMBER('THE_TICH'), 0);
        L_TS_VKT.DIEN_TICH := NVL(L_TS_VKT_OBJECT.GET_NUMBER('DIEN_TICH'), 0);
        L_TS_VKT.TAI_SAN_ID := L_ID;
        --INSERT vkt
        INSERT INTO TS_TAI_SAN_VKT (
            TAI_SAN_ID,
            DIEN_TICH,
            THE_TICH,
            CHIEU_DAI
        ) VALUES (
            L_TS_VKT.TAI_SAN_ID,
            L_TS_VKT.DIEN_TICH,
            L_TS_VKT.THE_TICH,
            L_TS_VKT.CHIEU_DAI
        );

    ELSIF L_LOAI_HINH_TAI_SAN = 10 THEN --enumLOAI_HINH_TAI_SAN.DAC_THU
        L_TS_DACTHU_OBJECT := L_JSON_OBJECT.GET_OBJECT('TS_DAC_THU');
        IF L_TS_DACTHU_OBJECT IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_DAC_THU IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
            UPDATE TS_TAI_SAN
            SET
                TRANG_THAI_ID = 7
            WHERE
                ID = L_ID;

            RETURN;
        END IF;

        L_TS_DACTHU.TAI_SAN_ID := L_ID;
        L_TS_DACTHU.THONG_SO_KY_THUAT := L_TS_DACTHU_OBJECT.GET_STRING('THONG_SO_KY_THUAT');
        L_TS_DACTHU.PHU_KIEN_JSON := L_TS_DACTHU_OBJECT.GET_STRING('PHU_KIEN_JSON');
        --INSERT dac thu
        INSERT INTO TS_TAI_SAN_MAY_MOC (
            TAI_SAN_ID,
            THONG_SO_KY_THUAT,
            PHU_KIEN_JSON,
            THONG_SO_KY_HIEU
        ) VALUES (
            L_ID,
            L_TS_DACTHU.THONG_SO_KY_THUAT,
            L_TS_DACTHU.PHU_KIEN_JSON,
            L_TS_DACTHU.THONG_SO_KY_HIEU
        );

    ELSIF L_LOAI_HINH_TAI_SAN = 8 THEN --enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC
        L_TS_HUUHINH_OBJECT := L_JSON_OBJECT.GET_OBJECT('TS_HUU_HINH_KHAC');
        IF L_TS_HUUHINH_OBJECT IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_HUU_HINH_KHAC IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
            UPDATE TS_TAI_SAN
            SET
                TRANG_THAI_ID = 7
            WHERE
                ID = L_ID;

            RETURN;
        END IF;

        L_TS_HUUHINH.TAI_SAN_ID := L_ID;
        L_TS_HUUHINH.THONG_SO_KY_THUAT := L_TS_HUUHINH_OBJECT.GET_STRING('THONG_SO_KY_THUAT');
        L_TS_HUUHINH.PHU_KIEN_JSON := L_TS_HUUHINH_OBJECT.GET_STRING('PHU_KIEN_JSON');
         --INSERT huu hinh
        INSERT INTO TS_TAI_SAN_MAY_MOC (
            TAI_SAN_ID,
            THONG_SO_KY_THUAT,
            PHU_KIEN_JSON,
            THONG_SO_KY_HIEU
        ) VALUES (
            L_TS_HUUHINH.TAI_SAN_ID,
            L_TS_HUUHINH.THONG_SO_KY_THUAT,
            L_TS_HUUHINH.PHU_KIEN_JSON,
            L_TS_HUUHINH.THONG_SO_KY_HIEU
        );

    ELSIF L_LOAI_HINH_TAI_SAN = 6 THEN --enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI
        L_TS_MAYMOC_OBJECT := L_JSON_OBJECT.GET_OBJECT('TS_MAY_MOC');
        IF L_TS_MAYMOC_OBJECT IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_MAY_MOC IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
            UPDATE TS_TAI_SAN
            SET
                TRANG_THAI_ID = 7
            WHERE
                ID = L_ID;

            RETURN;
        END IF;

        L_TS_MAYMOC.TAI_SAN_ID := L_ID;
        L_TS_MAYMOC.THONG_SO_KY_THUAT := L_TS_MAYMOC_OBJECT.GET_STRING('THONG_SO_KY_THUAT');
        L_TS_MAYMOC.PHU_KIEN_JSON := L_TS_MAYMOC_OBJECT.GET_STRING('PHU_KIEN_JSON');
        --INSERT may moc
        INSERT INTO TS_TAI_SAN_MAY_MOC (
            TAI_SAN_ID,
            THONG_SO_KY_THUAT,
            PHU_KIEN_JSON,
            THONG_SO_KY_HIEU
        ) VALUES (
            L_TS_MAYMOC.TAI_SAN_ID,
            L_TS_MAYMOC.THONG_SO_KY_THUAT,
            L_TS_MAYMOC.PHU_KIEN_JSON,
            L_TS_MAYMOC.THONG_SO_KY_HIEU
        );

    ELSIF L_LOAI_HINH_TAI_SAN = 9 THEN --enumLOAI_HINH_TAI_SAN.VO_HINH
        L_TS_VOHINH_OBJECT := L_JSON_OBJECT.GET_OBJECT('TS_VO_HINH');
        IF L_TS_VOHINH_OBJECT IS NULL THEN
            OPEN MSS_OUT FOR SELECT
                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'TS_VO_HINH IS NULL' ) AS STRJSON
                             FROM
                                 DUAL;

            L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
            UPDATE TS_TAI_SAN
            SET
                TRANG_THAI_ID = 7
            WHERE
                ID = L_ID;

            RETURN;
        END IF;

        L_TS_VOHINH.TAI_SAN_ID := L_ID;
        L_TS_VOHINH.THONG_SO_KY_THUAT := L_TS_VOHINH_OBJECT.GET_STRING('THONG_SO_KY_THUAT');
        --INSERT vo hinh
        INSERT INTO TS_TAI_SAN_VO_HINH (
            TAI_SAN_ID,
            THONG_SO_KY_THUAT
        ) VALUES (
            L_TS_VOHINH.TAI_SAN_ID,
            L_TS_VOHINH.THONG_SO_KY_THUAT
        );

    END IF;
    --END prepare từng loại tài sản

    L_BD_HTSD_JSON := '';
    FOR I IN 0..L_BIEN_DONG_ARR.GET_SIZE - 1 LOOP
        L_BIEN_DONG_OBJECT := TREAT(L_BIEN_DONG_ARR.GET(I) AS JSON_OBJECT_T);
        L_HIEN_TRANG_ARR := L_BIEN_DONG_OBJECT.GET_ARRAY('HIEN_TRANG_BD');
        L_NGUON_VON_ARR := L_BIEN_DONG_OBJECT.GET_ARRAY('NGUON_VON_BD');
        L_BD_HTSD_JSON := '';
        L_BD_HTSD_JSON := '';
        -- IF l_loai_hinh_tai_san = 1
        -- AND l_bien_dong_object.GET_NUMBER('LOAI_BIEN_DONG_ID') = 3 THEN 
        --     FOR ht IN 0..l_hien_trang_arr.get_size -1 LOOP l_hien_trang_object := TREAT(l_hien_trang_arr.get(ht) AS JSON_OBJECT_T);
        --     l_bd_HTSD_JSON := l_bd_HTSD_JSON||'{"HienTrangId":' || l_hien_trang_object.GET_NUMBER('HIEN_TRANG_ID') || ',"GiaTriText":"' || l_hien_trang_object.GET_NUMBER('GIA_TRI_TEXT') || '","GiaTriNumber":' || - ABS(nvl(l_hien_trang_object.GET_NUMBER('GIA_TRI_NUMBER'),0)) || ',"GiaTriCheckbox":' || l_hien_trang_object.GET_STRING('GIA_TRI_CHECKBOX') || ',"NhomHienTrangId":' || l_hien_trang_object.GET_NUMBER('NHOM_HIEN_TRANG_ID') || ',"TenHienTrang":"' || l_hien_trang_object.GET_STRING('TEN_HIEN_TRANG') || '","KieuDuLieuId":' || l_hien_trang_object.GET_NUMBER('KIEU_DU_LIEU_ID') || '},';
        --     END LOOP;
        -- ELSE 
        FOR HT IN 0..L_HIEN_TRANG_ARR.GET_SIZE - 1 LOOP
            L_HIEN_TRANG_OBJECT := TREAT(L_HIEN_TRANG_ARR.GET(HT) AS JSON_OBJECT_T);
            L_BD_HTSD_JSON := L_BD_HTSD_JSON
                              || '{"HienTrangId":'
                              || L_HIEN_TRANG_OBJECT.GET_NUMBER('HIEN_TRANG_ID')
                              || ',"GiaTriText":"'
                              || L_HIEN_TRANG_OBJECT.GET_NUMBER('GIA_TRI_TEXT')
                              || '","GiaTriNumber":'
                              || ABS(NVL(L_HIEN_TRANG_OBJECT.GET_NUMBER('GIA_TRI_NUMBER'), 0))
                              || ',"GiaTriCheckbox":'
                              || L_HIEN_TRANG_OBJECT.GET_STRING('GIA_TRI_CHECKBOX')
                              || ',"NhomHienTrangId":'
                              || L_HIEN_TRANG_OBJECT.GET_NUMBER('NHOM_HIEN_TRANG_ID')
                              || ',"TenHienTrang":"'
                              || L_HIEN_TRANG_OBJECT.GET_STRING('TEN_HIEN_TRANG')
                              || '","KieuDuLieuId":'
                              || L_HIEN_TRANG_OBJECT.GET_NUMBER('KIEU_DU_LIEU_ID')
                              || '},';

        END LOOP;
        --END IF;

        L_BD_HTSD_JSON := '{"TaiSanId":'
                          || L_ID
                          || ',
                                    "lstObjHienTrang":['
                          || REPLACE(L_BD_HTSD_JSON, '":,', '":null,')
                          || ']}';

        L_BD_HTSD_JSON := REPLACE(L_BD_HTSD_JSON, '},]', '}]');
        IF L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') <> 5 THEN
            FOR NV IN 0..L_NGUON_VON_ARR.GET_SIZE - 1 LOOP
                L_NGUON_VON_OBJECT := TREAT(L_NGUON_VON_ARR.GET(NV) AS JSON_OBJECT_T);
                L_BD_NV_JSON := L_BD_NV_JSON
                                || '{
                                        "ID":'
                                || L_NGUON_VON_OBJECT.GET_NUMBER('NGUON_VON_ID')
                                || ',
                                        "TEN":"'
                                || L_NGUON_VON_OBJECT.GET_NUMBER('TEN_NGUON_VON')
                                || '",
                                        "LoaiHinhTaiSanId":'
                                || L_LOAI_HINH_TAI_SAN
                                || ',
                                        "GiaTri":'
                                || (
                    CASE
                        WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') = 3 THEN
                            -ABS(NVL(L_NGUON_VON_OBJECT.GET_NUMBER('GIA_TRI'), 0))
                        ELSE ABS(NVL(L_NGUON_VON_OBJECT.GET_NUMBER('GIA_TRI'), 0))
                    END
                )
                                || '},';

            END LOOP;
        END IF;

        L_BD_NV_JSON := '['
                        || REPLACE(L_BD_NV_JSON, '":,', '":null,')
                        || ']';
        L_BD_NV_JSON := REPLACE(L_BD_NV_JSON, '},]', '}]');
        IF L_BIEN_DONG_OBJECT.GET_NUMBER('TRANG_THAI_ID') = 3 THEN --prepare biENDong
            L_BD.TAI_SAN_ID := L_ID;
            L_BD.TAI_SAN_MA := L_TAI_SAN.MA;
            L_BD.TAI_SAN_TEN := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_STRING('TEN_TAI_SAN') IS NOT NULL THEN
                    L_BIEN_DONG_OBJECT.GET_STRING('TEN_TAI_SAN')
                ELSE L_TAI_SAN.TEN
            END );

            L_BD.LOAI_HINH_TAI_SAN_ID := L_LOAI_HINH_TAI_SAN;
            L_BD.LOAI_TAI_SAN_DON_VI_ID := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_TAI_SAN_DON_VI_ID') IS NOT NULL THEN
                    L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_TAI_SAN_DON_VI_ID')
                ELSE L_TAI_SAN.LOAI_TAI_SAN_DON_VI_ID
            END );

            L_BD.DON_VI_ID := L_TAI_SAN.DON_VI_ID;
            L_BD.LOAI_TAI_SAN_ID := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_TAI_SAN_ID') IS NOT NULL THEN
                    L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_TAI_SAN_ID')
                ELSE L_TAI_SAN.LOAI_TAI_SAN_ID
            END );

            L_BD.NGUYEN_GIA := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') = 3 THEN
                    -ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('NGUYEN_GIA'))
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') = 5 THEN
                    NULL
                ELSE ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('NGUYEN_GIA'))
            END );

            L_BD.CHUNG_TU_SO := L_BIEN_DONG_OBJECT.GET_STRING('CHUNG_TU_SO');
            L_BD.CHUNG_TU_NGAY := L_BIEN_DONG_OBJECT.GET_DATE('CHUNG_TU_NGAY');
            L_BD.NGAY_BIEN_DONG := L_BIEN_DONG_OBJECT.GET_DATE('NGAY_BIEN_DONG');
            L_BD.NGAY_DUYET := L_BIEN_DONG_OBJECT.GET_DATE('NGAY_DUYET');
            L_BD.NGAY_SU_DUNG := L_TAI_SAN.NGAY_SU_DUNG;
            L_BD.LOAI_BIEN_DONG_ID := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') = 4 THEN
                    11
                ELSE L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID')
            END );

            L_BD.TRANG_THAI_ID := 3; --trang thai da duyet
            L_BD.NGAY_TAO := NVL(L_BIEN_DONG_OBJECT.GET_DATE('NGAY_TAO'), CURRENT_DATE);
            L_BD.GHI_CHU := L_BIEN_DONG_OBJECT.GET_STRING('GHI_CHU');
            L_BD.QUYET_DINH_SO := L_BIEN_DONG_OBJECT.GET_STRING('QUYET_DINH_SO');
            L_BD.QUYET_DINH_NGAY := ( CASE
                WHEN L_BD.QUYET_DINH_SO IS NULL THEN
                    NULL
                ELSE L_BIEN_DONG_OBJECT.GET_DATE('QUYET_DINH_NGAY')
            END );

            L_BD.IS_BIENDONG_CUOI := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_STRING('IS_BIENDONG_CUOI') = 'true' THEN
                    1
                ELSE 0
            END );

            L_BD.ID_DB := L_BIEN_DONG_OBJECT.GET_STRING('ID_DB');
            L_BD.LY_DO_BIEN_DONG_ID := L_BIEN_DONG_OBJECT.GET_NUMBER('LY_DO_BIEN_DONG_ID');
            L_BD.DON_VI_BO_PHAN_ID := L_BIEN_DONG_OBJECT.GET_NUMBER('DON_VI_BO_PHAN_ID');
            L_BD.GUID := STRING_GUID_TO_RAW(L_BIEN_DONG_OBJECT.GET_STRING('GUID'));
            --INSERT bien dong
            INSERT INTO BD_BIEN_DONG (
                TAI_SAN_ID,
                TAI_SAN_MA,
                TAI_SAN_TEN,
                LOAI_TAI_SAN_ID,
                LOAI_HINH_TAI_SAN_ID,
                NGUYEN_GIA,
                DON_VI_BO_PHAN_ID,
                CHUNG_TU_SO,
                CHUNG_TU_NGAY,
                NGAY_BIEN_DONG,
                NGAY_SU_DUNG,
                LOAI_BIEN_DONG_ID,
                LY_DO_BIEN_DONG_ID,
                TRANG_THAI_ID,
                DON_VI_ID,
                NGAY_TAO,
                GUID,
                GHI_CHU,
                QUYET_DINH_SO,
                QUYET_DINH_NGAY,
                IS_BIENDONG_CUOI,
                LOAI_TAI_SAN_DON_VI_ID,
                NGAY_DUYET,
                NGUOI_DUYET_ID,
                ID_DB
            ) VALUES (
                L_ID,
                L_BD.TAI_SAN_MA,
                L_BD.TAI_SAN_TEN,
                L_BD.LOAI_TAI_SAN_ID,
                L_BD.LOAI_HINH_TAI_SAN_ID,
                L_BD.NGUYEN_GIA,
                L_BD.DON_VI_BO_PHAN_ID,
                L_BD.CHUNG_TU_SO,
                L_BD.CHUNG_TU_NGAY,
                L_BD.NGAY_BIEN_DONG,
                L_BD.NGAY_SU_DUNG,
                L_BD.LOAI_BIEN_DONG_ID,
                L_BD.LY_DO_BIEN_DONG_ID,
                3,
                L_BD.DON_VI_ID,
                L_BD.NGAY_TAO,
                L_BD.GUID,
                L_BD.GHI_CHU,
                L_BD.QUYET_DINH_SO,
                L_BD.QUYET_DINH_NGAY,
                L_BD.IS_BIENDONG_CUOI,
                L_BD.LOAI_TAI_SAN_DON_VI_ID,
                L_BD.NGAY_DUYET,
                L_BD.NGUOI_DUYET_ID,
                L_BD.ID_DB
            ) RETURNING ID INTO L_BD_ID;

            COMMIT;
            --prepare bien dong chi tiet
            L_BD_CT.BIEN_DONG_ID := L_BD_ID;
            L_BD_CT.HTSD_JSON := L_BD_HTSD_JSON;
            IF L_LOAI_HINH_TAI_SAN = 1 THEN --enumLOAI_HINH_TAI_SAN.DAT
                L_BD_CT.DIA_BAN_ID := L_BIEN_DONG_OBJECT.GET_NUMBER('DIA_BAN_ID');
                L_BD_CT.DIA_CHI := L_BIEN_DONG_OBJECT.GET_STRING('DIA_CHI');
                L_BD_CT.DAT_TONG_DIEN_TICH := ( CASE
                    WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') = 3 THEN
                        -ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('DAT_TONG_DIEN_TICH'))
                    WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') = 5 THEN
                        NULL
                    ELSE ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('DAT_TONG_DIEN_TICH'))
                END );

            ELSIF L_LOAI_HINH_TAI_SAN = 2 THEN --enumLOAI_HINH_TAI_SAN.NHA
                L_BD_CT.DIA_BAN_ID := L_BIEN_DONG_OBJECT.GET_NUMBER('DIA_BAN_ID');
                L_BD_CT.DIA_CHI := L_BIEN_DONG_OBJECT.GET_STRING('DIA_CHI');
                L_BD_CT.NHA_SO_TANG := L_BIEN_DONG_OBJECT.GET_NUMBER('NHA_SO_TANG');
                L_BD_CT.NHA_NAM_XAY_DUNG := L_BIEN_DONG_OBJECT.GET_NUMBER('NHA_NAM_XAY_DUNG');
                L_BD_CT.NHA_DIEN_TICH_XD := ( CASE
                    WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') = 3 THEN
                        -ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('NHA_DIEN_TICH_XD'))
                    WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') = 5 THEN
                        NULL
                    ELSE ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('NHA_DIEN_TICH_XD'))
                END );

                L_BD_CT.NHA_TONG_DIEN_TICH_XD := ( CASE
                    WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') = 3 THEN
                        -ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('NHA_TONG_DIEN_TICH_XD'))
                    WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') = 5 THEN
                        NULL
                    ELSE ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('NHA_TONG_DIEN_TICH_XD'))
                END );

            ELSIF L_LOAI_HINH_TAI_SAN = 3 THEN --enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC
                L_BD_CT.VKT_DIEN_TICH := L_BIEN_DONG_OBJECT.GET_NUMBER('VKT_DIEN_TICH');
                L_BD_CT.VKT_THE_TICH := L_BIEN_DONG_OBJECT.GET_NUMBER('VKT_THE_TICH');
                L_BD_CT.VKT_CHIEU_DAI := L_BIEN_DONG_OBJECT.GET_NUMBER('VKT_CHIEU_DAI');
            ELSIF L_LOAI_HINH_TAI_SAN = 4 OR L_LOAI_HINH_TAI_SAN = 5 THEN --enumLOAI_HINH_TAI_SAN.OTO + enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC
                L_BD_CT.OTO_BIEN_KIEM_SOAT := L_BIEN_DONG_OBJECT.GET_STRING('OTO_BIEN_KIEM_SOAT');
                L_BD_CT.OTO_SO_CHO_NGOI := L_BIEN_DONG_OBJECT.GET_NUMBER('OTO_SO_CHO_NGOI');
                L_BD_CT.OTO_TAI_TRONG := L_BIEN_DONG_OBJECT.GET_NUMBER('OTO_TAI_TRONG');
                L_BD_CT.OTO_CONG_XUAT := L_BIEN_DONG_OBJECT.GET_NUMBER('OTO_CONG_XUAT');
                L_BD_CT.OTO_XI_LANH := L_BIEN_DONG_OBJECT.GET_NUMBER('OTO_XI_LANH');
                L_BD_CT.OTO_SO_KHUNG := L_BIEN_DONG_OBJECT.GET_STRING('OTO_SO_KHUNG');
                L_BD_CT.OTO_SO_MAY := L_BIEN_DONG_OBJECT.GET_STRING('OTO_SO_MAY');
                L_CHUCDANH_MA := L_BIEN_DONG_OBJECT.GET_STRING('OTO_CHUC_DANH_MA');
                IF L_CHUCDANH_MA IS NOT NULL THEN
                    BEGIN
                        SELECT
                            ID
                        INTO L_CHUCDANH_ID
                        FROM
                            DM_CHUC_DANH
                        WHERE
                            MA_CHUC_DANH = L_CHUCDANH_MA
                            AND ROWNUM = 1;

                    EXCEPTION
                        WHEN NO_DATA_FOUND THEN
                            OPEN MSS_OUT FOR SELECT
                                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-OTO_CHUC_DANH_MA('
                                                                                                  || L_CHUCDANH_MA
                                                                                                  || ') NOT FOUND' ) AS STRJSON
                                             FROM
                                                 DUAL;

                            L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
                            UPDATE TS_TAI_SAN
                            SET
                                TRANG_THAI_ID = 7
                            WHERE
                                ID = L_ID;

                            RETURN;
                    END;

                    L_BD_CT.OTO_CHUC_DANH_ID := L_CHUCDANH_ID;
                END IF;

                L_NHANXE_MA := L_BIEN_DONG_OBJECT.GET_STRING('OTO_NHAN_XE_MA');
                IF L_NHANXE_MA IS NOT NULL THEN
                    BEGIN
                        SELECT
                            ID
                        INTO L_NHANXE_ID
                        FROM
                            DM_NHAN_XE
                        WHERE
                            MA = L_NHANXE_MA
                            AND ROWNUM = 1;

                    EXCEPTION
                        WHEN NO_DATA_FOUND THEN
                            OPEN MSS_OUT FOR SELECT
                                                 JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-OTO_NHAN_XE_MA('
                                                                                                  || L_NHANXE_MA
                                                                                                  || ') NOT FOUND' ) AS STRJSON
                                             FROM
                                                 DUAL;

                            L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
                            UPDATE TS_TAI_SAN
                            SET
                                TRANG_THAI_ID = 7
                            WHERE
                                ID = L_ID;

                            RETURN;
                    END;

                    L_BD_CT.OTO_NHAN_XE_ID := L_NHANXE_ID;
                END IF;

            ELSIF L_LOAI_HINH_TAI_SAN = 7 THEN --enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV
                L_BD_CT.CLN_SO_NAM := L_BIEN_DONG_OBJECT.GET_NUMBER('CLN_SO_NAM');
            ELSIF L_LOAI_HINH_TAI_SAN = 6 OR L_LOAI_HINH_TAI_SAN = 8 OR L_LOAI_HINH_TAI_SAN = 9 OR L_LOAI_HINH_TAI_SAN = 10 THEN -- máy móc, hữu hình, vô hình, đặc thù
                L_BD_CT.THONG_SO_KY_THUAT := L_BIEN_DONG_OBJECT.GET_STRING('THONG_SO_KY_THUAT');
            END IF;

            L_HINH_THUC_MUA_SAM_MA := L_BIEN_DONG_OBJECT.GET_STRING('HINH_THUC_MUA_SAM_MA');
            IF L_HINH_THUC_MUA_SAM_MA IS NOT NULL THEN
                BEGIN
                    SELECT
                        ID
                    INTO L_HINH_THUC_MUA_SAM_ID
                    FROM
                        DM_HINH_THUC_MUA_SAM
                    WHERE
                        MA = L_HINH_THUC_MUA_SAM_MA
                        AND ROWNUM = 1;

                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        OPEN MSS_OUT FOR SELECT
                                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-HINH_THUC_MUA_SAM_MA('
                                                                                              || L_HINH_THUC_MUA_SAM_MA
                                                                                              || ') NOT FOUND' ) AS STRJSON
                                         FROM
                                             DUAL;

                        L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
                        UPDATE TS_TAI_SAN
                        SET
                            TRANG_THAI_ID = 7
                        WHERE
                            ID = L_ID;

                        RETURN;
                END;

                L_BD_CT.HINH_THUC_MUA_SAM_ID := L_HINH_THUC_MUA_SAM_ID;
            END IF;
            -- l_muc_dich_su_dung_id := l_bien_dong_object.GET_STRING('MUC_DICH_SU_DUNG_ID');           

            L_DONVI_DIEU_CHUYEN_MA := L_BIEN_DONG_OBJECT.GET_STRING('DON_VI_NHAN_DIEU_CHUYEN_MA');
            IF L_DONVI_DIEU_CHUYEN_MA IS NOT NULL THEN
                BEGIN
                    SELECT
                        ID
                    INTO L_DONVI_DIEU_CHUYEN_ID
                    FROM
                        DM_DON_VI
                    WHERE
                        MA = L_DONVI_DIEU_CHUYEN_MA
                        AND ROWNUM = 1;

                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        OPEN MSS_OUT FOR SELECT
                                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-DON_VI_NHAN_DIEU_CHUYEN_MA('
                                                                                              || L_DONVI_DIEU_CHUYEN_MA
                                                                                              || ') NOT FOUND' ) AS STRJSON
                                         FROM
                                             DUAL;

                        L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
                        UPDATE TS_TAI_SAN
                        SET
                            TRANG_THAI_ID = 7
                        WHERE
                            ID = L_ID;

                        RETURN;
                END;

                L_BD_CT.DON_VI_NHAN_DIEU_CHUYEN_ID := L_DONVI_DIEU_CHUYEN_ID;
            END IF;

            L_HINH_THUC_XU_LY_MA := L_BIEN_DONG_OBJECT.GET_STRING('HINH_THUC_XU_LY_MA');
            IF L_HINH_THUC_XU_LY_MA IS NOT NULL THEN
                BEGIN
                    SELECT
                        ID
                    INTO L_HINH_THUC_XU_LY_ID
                    FROM
                        DM_DON_VI
                    WHERE
                        MA = L_HINH_THUC_XU_LY_MA
                        AND ROWNUM = 1;

                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        OPEN MSS_OUT FOR SELECT
                                             JSON_OBJECT ( 'Code' VALUE '01', 'Message' VALUE 'BIEN_DONG-HINH_THUC_XU_LY_MA('
                                                                                              || L_NHANXE_MA
                                                                                              || ') NOT FOUND' ) AS STRJSON
                                         FROM
                                             DUAL;

                        L_EXC_FN := DEL_THONG_TIN_LIEN_QUAN_TAI_SAN(L_ID);
                        UPDATE TS_TAI_SAN
                        SET
                            TRANG_THAI_ID = 7
                        WHERE
                            ID = L_ID;

                        RETURN;
                END;

                L_BD_CT.HINH_THUC_XU_LY_ID := L_HINH_THUC_XU_LY_ID;
            END IF;

            L_BD_CT.NHAN_HIEU := L_BIEN_DONG_OBJECT.GET_STRING('NHAN_HIEU');
            L_BD_CT.SO_HIEU := L_BIEN_DONG_OBJECT.GET_STRING('SO_HIEU');
            L_BD_CT.DATA_JSON := REPLACE(L_BIEN_DONG_OBJECT.GET_STRING('DATA_JSON'), '\"', '"');

            L_BD_CT.NGUYEN_GIA := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') = 3 THEN
                    -ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('NGUYEN_GIA'))
                WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') = 5 THEN
                    NULL
                ELSE ABS(L_BIEN_DONG_OBJECT.GET_NUMBER('NGUYEN_GIA'))
            END );

            L_BD_CT.HM_SO_NAM_CON_LAI := L_BIEN_DONG_OBJECT.GET_NUMBER('HM_SO_NAM_CON_LAI');
            L_BD_CT.HM_TY_LE_HAO_MON := L_BIEN_DONG_OBJECT.GET_NUMBER('HM_TY_LE_HAO_MON');
            L_BD_CT.HM_LUY_KE := L_BIEN_DONG_OBJECT.GET_NUMBER('HM_LUY_KE');
            L_BD_CT.HM_GIA_TRI_CON_LAI := L_BIEN_DONG_OBJECT.GET_NUMBER('GIA_TRI_CON_LAI');
            L_BD_CT.KH_NGAY_BAT_DAU := L_BIEN_DONG_OBJECT.GET_DATE('KH_NGAY_BAT_DAU');
            L_BD_CT.KH_THANG_CON_LAI := L_BIEN_DONG_OBJECT.GET_NUMBER('KH_THANG_CON_LAI');
            L_BD_CT.KH_GIA_TINH_KHAU_HAO := L_BIEN_DONG_OBJECT.GET_NUMBER('KH_GIA_TINH_KHAU_HAO');
            L_BD_CT.KH_GIA_TRI_TRICH_THANG := L_BIEN_DONG_OBJECT.GET_NUMBER('KH_GIA_TRI_TRICH_THANG');
            L_BD_CT.KH_LUY_KE := L_BIEN_DONG_OBJECT.GET_NUMBER('KH_LUY_KE');
            L_BD_CT.KH_CON_LAI := L_BIEN_DONG_OBJECT.GET_NUMBER('KH_CON_LAI');
            L_BD_CT.PHI_THU := L_BIEN_DONG_OBJECT.GET_NUMBER('PHI_THU');
            L_BD_CT.PHI_BU_DAP := L_BIEN_DONG_OBJECT.GET_NUMBER('PHI_BU_DAP');
            L_BD_CT.PHI_NOP_NGAN_SACH := L_BIEN_DONG_OBJECT.GET_NUMBER('PHI_NOP_NGAN_SACH');
            L_BD_CT.PHI_KHAC := L_BIEN_DONG_OBJECT.GET_NUMBER('PHI_KHAC');
            L_BD_CT.IS_BAN_THANH_LY := ( CASE
                WHEN L_BIEN_DONG_OBJECT.GET_STRING('IS_BAN_THANH_LY') = 'true' THEN
                    1
                ELSE 0
            END );
            --HTSD_JSON
            --INSERT bien dong chi tiet

            INSERT INTO BD_BIEN_DONG_CHI_TIET (
                BIEN_DONG_ID,
                HINH_THUC_MUA_SAM_ID,
                MUC_DICH_SU_DUNG_ID,
                NHAN_HIEU,
                SO_HIEU,
                DIA_BAN_ID,
                DATA_JSON,
                NGUYEN_GIA,
                DAT_TONG_DIEN_TICH,
                HM_SO_NAM_CON_LAI,
                HM_TY_LE_HAO_MON,
                HM_LUY_KE,
                HM_GIA_TRI_CON_LAI,
                KH_NGAY_BAT_DAU,
                KH_THANG_CON_LAI,
                KH_GIA_TINH_KHAU_HAO,
                KH_GIA_TRI_TRICH_THANG,
                KH_LUY_KE,
                KH_CON_LAI,
                NHA_SO_TANG,
                NHA_NAM_XAY_DUNG,
                NHA_DIEN_TICH_XD,
                NHA_TONG_DIEN_TICH_XD,
                VKT_DIEN_TICH,
                VKT_THE_TICH,
                VKT_CHIEU_DAI,
                OTO_BIEN_KIEM_SOAT,
                OTO_SO_CHO_NGOI,
                OTO_CHUC_DANH_ID,
                OTO_NHAN_XE_ID,
                OTO_TAI_TRONG,
                OTO_CONG_XUAT,
                OTO_XI_LANH,
                OTO_SO_KHUNG,
                OTO_SO_MAY,
                THONG_SO_KY_THUAT,
                CLN_SO_NAM,
                PHI_THU,
                PHI_BU_DAP,
                PHI_NOP_NGAN_SACH,
                PHI_KHAC,
                DON_VI_NHAN_DIEU_CHUYEN_ID,
                HTSD_JSON,
                HINH_THUC_XU_LY_ID,
                IS_BAN_THANH_LY,
                DIA_CHI,
                DIEU_CHUYEN_KEM_THEO
            ) VALUES (
                L_BD_ID,
                L_BD_CT.HINH_THUC_MUA_SAM_ID,
                L_BD_CT.MUC_DICH_SU_DUNG_ID,
                L_BD_CT.NHAN_HIEU,
                L_BD_CT.SO_HIEU,
                L_BD_CT.DIA_BAN_ID,
                L_BD_CT.DATA_JSON,
                L_BD_CT.NGUYEN_GIA,
                L_BD_CT.DAT_TONG_DIEN_TICH,
                L_BD_CT.HM_SO_NAM_CON_LAI,
                L_BD_CT.HM_TY_LE_HAO_MON,
                L_BD_CT.HM_LUY_KE,
                L_BD_CT.HM_GIA_TRI_CON_LAI,
                L_BD_CT.KH_NGAY_BAT_DAU,
                L_BD_CT.KH_THANG_CON_LAI,
                L_BD_CT.KH_GIA_TINH_KHAU_HAO,
                L_BD_CT.KH_GIA_TRI_TRICH_THANG,
                L_BD_CT.KH_LUY_KE,
                L_BD_CT.KH_CON_LAI,
                L_BD_CT.NHA_SO_TANG,
                L_BD_CT.NHA_NAM_XAY_DUNG,
                L_BD_CT.NHA_DIEN_TICH_XD,
                L_BD_CT.NHA_TONG_DIEN_TICH_XD,
                L_BD_CT.VKT_DIEN_TICH,
                L_BD_CT.VKT_THE_TICH,
                L_BD_CT.VKT_CHIEU_DAI,
                L_BD_CT.OTO_BIEN_KIEM_SOAT,
                L_BD_CT.OTO_SO_CHO_NGOI,
                L_BD_CT.OTO_CHUC_DANH_ID,
                L_BD_CT.OTO_NHAN_XE_ID,
                L_BD_CT.OTO_TAI_TRONG,
                L_BD_CT.OTO_CONG_XUAT,
                L_BD_CT.OTO_XI_LANH,
                L_BD_CT.OTO_SO_KHUNG,
                L_BD_CT.OTO_SO_MAY,
                L_BD_CT.THONG_SO_KY_THUAT,
                L_BD_CT.CLN_SO_NAM,
                L_BD_CT.PHI_THU,
                L_BD_CT.PHI_BU_DAP,
                L_BD_CT.PHI_NOP_NGAN_SACH,
                L_BD_CT.PHI_KHAC,
                L_BD_CT.DON_VI_NHAN_DIEU_CHUYEN_ID,
                L_BD_CT.HTSD_JSON,
                L_BD_CT.HINH_THUC_XU_LY_ID,
                L_BD_CT.IS_BAN_THANH_LY,
                L_BD_CT.DIA_CHI,
                L_BD_CT.DIEU_CHUYEN_KEM_THEO
            );

            FOR I IN 0..L_HIEN_TRANG_ARR.GET_SIZE - 1 LOOP
                L_HIEN_TRANG_OBJECT := TREAT(L_HIEN_TRANG_ARR.GET(I) AS JSON_OBJECT_T);
                L_TS_HIEN_TRANG.HIEN_TRANG_ID := L_HIEN_TRANG_OBJECT.GET_NUMBER('HIEN_TRANG_ID');
                --DBMS_OUTPUT.PUT_LINE(l_hien_trang_object.GET_STRING('TEN_HIEN_TRANG'));
                BEGIN
                    SELECT
                        TEN_HIEN_TRANG
                    INTO L_TEN_HIEN_TRANG
                    FROM
                        DM_HIEN_TRANG
                    WHERE
                        ID = L_TS_HIEN_TRANG.HIEN_TRANG_ID;
                    --DBMS_OUTPUT.PUT_LINE(l_ts_hien_trang.HIEN_TRANG_ID);

                    L_TS_HIEN_TRANG.NHOM_HIEN_TRANG_ID := L_HIEN_TRANG_OBJECT.GET_NUMBER('NHOM_HIEN_TRANG_ID');
                    L_TS_HIEN_TRANG.KIEU_DU_LIEU_ID := L_HIEN_TRANG_OBJECT.GET_NUMBER('KIEU_DU_LIEU_ID');
                    L_TS_HIEN_TRANG.TEN_HIEN_TRANG := NVL(L_TEN_HIEN_TRANG, L_HIEN_TRANG_OBJECT.GET_STRING('TEN_HIEN_TRANG'));
                    L_TS_HIEN_TRANG.GIA_TRI_TEXT := L_HIEN_TRANG_OBJECT.GET_NUMBER('GIA_TRI_TEXT');
                    L_TS_HIEN_TRANG.GIA_TRI_NUMBER := ABS(NVL(L_HIEN_TRANG_OBJECT.GET_NUMBER('GIA_TRI_NUMBER'), 0));

                    L_TS_HIEN_TRANG.GIA_TRI_CHECKBOX := ( CASE
                        WHEN L_HIEN_TRANG_OBJECT.GET_STRING('GIA_TRI_CHECKBOX') = 'true' THEN
                            1
                        WHEN L_HIEN_TRANG_OBJECT.GET_STRING('GIA_TRI_CHECKBOX') = 'false' THEN
                            0
                        ELSE NULL
                    END );

                    INSERT INTO TS_TAI_SAN_HIEN_TRANG_SU_DUNG (
                        TAI_SAN_ID,
                        BIEN_DONG_ID,
                        NHOM_HIEN_TRANG_ID,
                        KIEU_DU_LIEU_ID,
                        TEN_HIEN_TRANG,
                        GIA_TRI_TEXT,
                        GIA_TRI_NUMBER,
                        GIA_TRI_CHECKBOX,
                        HIEN_TRANG_ID
                    ) VALUES (
                        L_ID,
                        L_BD_ID,
                        L_TS_HIEN_TRANG.NHOM_HIEN_TRANG_ID,
                        L_TS_HIEN_TRANG.KIEU_DU_LIEU_ID,
                        L_TS_HIEN_TRANG.TEN_HIEN_TRANG,
                        L_TS_HIEN_TRANG.GIA_TRI_TEXT,
                        L_TS_HIEN_TRANG.GIA_TRI_NUMBER,
                        L_TS_HIEN_TRANG.GIA_TRI_CHECKBOX,
                        L_TS_HIEN_TRANG.HIEN_TRANG_ID
                    );

                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                    --DBMS_OUTPUT.PUT_LINE('NOT FOUND ID '||l_hien_trang_object.GET_NUMBER('HIEN_TRANG_ID'));
                        L_TEN_HIEN_TRANG := NULL;
                END;

            END LOOP;
            -- END IF;

            IF L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') <> 5 THEN
                FOR NV IN 0..L_NGUON_VON_ARR.GET_SIZE - 1 LOOP
                    L_NGUON_VON_OBJECT := TREAT(L_NGUON_VON_ARR.GET(NV) AS JSON_OBJECT_T);
                    L_TS_NGUON_VON.NGUON_VON_ID := L_NGUON_VON_OBJECT.GET_NUMBER('NGUON_VON_ID');
                    L_TS_NGUON_VON.GIA_TRI := ( CASE
                        WHEN L_BIEN_DONG_OBJECT.GET_NUMBER('LOAI_BIEN_DONG_ID') = 3 THEN
                            -ABS(NVL(L_NGUON_VON_OBJECT.GET_NUMBER('GIA_TRI'), 0))
                        ELSE ABS(NVL(L_NGUON_VON_OBJECT.GET_NUMBER('GIA_TRI'), 0))
                    END );

                    INSERT INTO TS_TAI_SAN_NGUON_VON (
                        TAI_SAN_ID,
                        NGUON_VON_ID,
                        GIA_TRI,
                        BIEN_DONG_ID
                    ) VALUES (
                        L_ID,
                        L_TS_NGUON_VON.NGUON_VON_ID,
                        L_TS_NGUON_VON.GIA_TRI,
                        L_BD_ID
                    );

                END LOOP;
            END IF;

        END IF;

    END LOOP;

    COMMIT;
    IF L_NGUON_TAI_SAN_ID != 2 THEN
        UPDATE DB_TAI_SAN_NHAT_KY
        SET
            BD_IDS = (
                SELECT
                    LISTAGG(ID, ',') WITHIN GROUP(
                        ORDER BY
                            1
                    ) AS BD_IDS
                FROM
                    BD_BIEN_DONG
                WHERE
                    TAI_SAN_ID = L_ID
            )
        WHERE
            TAI_SAN_ID = L_ID;

    END IF;
    -- cập nhật trường biến động cuối

    SELECT
        ID
    INTO L_BD_ID
    FROM
        BD_BIEN_DONG
    WHERE
        TAI_SAN_ID = L_ID
    ORDER BY
        NGAY_BIEN_DONG DESC
    FETCH FIRST 1 ROWS ONLY;

    UPDATE BD_BIEN_DONG
    SET
        IS_BIENDONG_CUOI = (
            CASE
                WHEN ID = L_BD_ID THEN
                    1
                ELSE
                    0
            END
        )
    WHERE
        TAI_SAN_ID = L_ID; 
    --insert kt hao mon

    L_HAO_MON_ARR := L_JSON_OBJECT.GET_ARRAY('LST_HAO_MON');
    IF L_HAO_MON_ARR IS NOT NULL AND L_HAO_MON_ARR.GET_SIZE > 0 THEN
        FOR I IN 0..L_HAO_MON_ARR.GET_SIZE - 1 LOOP
            L_HAO_MON_OBJECT := TREAT(L_HAO_MON_ARR.GET(I) AS JSON_OBJECT_T);
           
           -- l_hao_mon_id := l_hao_mon_object.GET_NUMBER('ID');
           -- IF l_hao_mon_id > 0 THEN
           --     BEGIN
           --         SELECT 
           --             * INTO L_HAO_MON
           --         FROM
           --             KT_HAO_MON_TAI_SAN
           --         WHERE 
           --             ID = l_hao_mon_id;
           --         --
           --         L_HAO_MON.NAM_HAO_MON := l_hao_mon_object.GET_NUMBER('NAM_HAO_MON');
           --         L_HAO_MON.GIA_TRI_HAO_MON := l_hao_mon_object.GET_NUMBER('GIA_TRI_HAO_MON');
           --         L_HAO_MON.TONG_HAO_MON_LUY_KE := l_hao_mon_object.GET_NUMBER('TONG_HAO_MON_LUY_KE');
           --         L_HAO_MON.TONG_GIA_TRI_CON_LAI := l_hao_mon_object.GET_NUMBER('TONG_GIA_TRI_CON_LAI');
           --         L_HAO_MON.TY_LE_HAO_MON := l_hao_mon_object.GET_NUMBER('TY_LE_HAO_MON');
           --         L_HAO_MON.TONG_NGUYEN_GIA := l_hao_mon_object.GET_NUMBER('TONG_NGUYEN_GIA');

           --         UPDATE KT_HAO_MON_TAI_SAN
           --         SET 
           --             NAM_HAO_MON = L_HAO_MON.NAM_HAO_MON,
           --             GIA_TRI_HAO_MON = L_HAO_MON.GIA_TRI_HAO_MON,
           --             TONG_HAO_MON_LUY_KE = L_HAO_MON.TONG_HAO_MON_LUY_KE,
           --             TONG_GIA_TRI_CON_LAI = L_HAO_MON.TONG_GIA_TRI_CON_LAI,
           --             TY_LE_HAO_MON = L_HAO_MON.TY_LE_HAO_MON,
           --             TONG_NGUYEN_GIA = L_HAO_MON.TONG_NGUYEN_GIA
           --         WHERE ID = l_hao_mon_id;
           --     EXCEPTION
           --         WHEN NO_DATA_FOUND THEN
           --         L_HAO_MON:=NULL;
           --     END;
           -- ELSE
            BEGIN
                L_NAM := L_HAO_MON_OBJECT.GET_NUMBER('NAM_HAO_MON');
                SELECT
                    *
                INTO L_HAO_MON
                FROM
                    KT_HAO_MON_TAI_SAN
                WHERE
                    TAI_SAN_ID = L_ID
                    AND NAM_HAO_MON = L_NAM;
                --

                L_HAO_MON.NAM_HAO_MON := L_HAO_MON_OBJECT.GET_NUMBER('NAM_HAO_MON');
                L_HAO_MON.GIA_TRI_HAO_MON := L_HAO_MON_OBJECT.GET_NUMBER('GIA_TRI_HAO_MON');
                L_HAO_MON.TONG_HAO_MON_LUY_KE := L_HAO_MON_OBJECT.GET_NUMBER('TONG_HAO_MON_LUY_KE');
                L_HAO_MON.TONG_GIA_TRI_CON_LAI := L_HAO_MON_OBJECT.GET_NUMBER('TONG_GIA_TRI_CON_LAI');
                L_HAO_MON.TY_LE_HAO_MON := L_HAO_MON_OBJECT.GET_NUMBER('TY_LE_HAO_MON');
                L_HAO_MON.TONG_NGUYEN_GIA := L_HAO_MON_OBJECT.GET_NUMBER('TONG_NGUYEN_GIA');
                UPDATE KT_HAO_MON_TAI_SAN
                SET
                    NAM_HAO_MON = L_HAO_MON.NAM_HAO_MON,
                    GIA_TRI_HAO_MON = L_HAO_MON.GIA_TRI_HAO_MON,
                    TONG_HAO_MON_LUY_KE = L_HAO_MON.TONG_HAO_MON_LUY_KE,
                    TONG_GIA_TRI_CON_LAI = L_HAO_MON.TONG_GIA_TRI_CON_LAI,
                    TY_LE_HAO_MON = L_HAO_MON.TY_LE_HAO_MON,
                    TONG_NGUYEN_GIA = L_HAO_MON.TONG_NGUYEN_GIA
                WHERE
                    ID = L_HAO_MON_ID;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    L_HAO_MON.TAI_SAN_ID := L_ID;
                    L_HAO_MON.MA_TAI_SAN := L_TAI_SAN.MA;
                    L_HAO_MON.NAM_HAO_MON := L_HAO_MON_OBJECT.GET_NUMBER('NAM_HAO_MON');
                    L_HAO_MON.GIA_TRI_HAO_MON := L_HAO_MON_OBJECT.GET_NUMBER('GIA_TRI_HAO_MON');
                    L_HAO_MON.TONG_HAO_MON_LUY_KE := L_HAO_MON_OBJECT.GET_NUMBER('TONG_HAO_MON_LUY_KE');
                    L_HAO_MON.TONG_GIA_TRI_CON_LAI := L_HAO_MON_OBJECT.GET_NUMBER('TONG_GIA_TRI_CON_LAI');
                    L_HAO_MON.TY_LE_HAO_MON := L_HAO_MON_OBJECT.GET_NUMBER('TY_LE_HAO_MON');
                    L_HAO_MON.TONG_NGUYEN_GIA := L_HAO_MON_OBJECT.GET_NUMBER('TONG_NGUYEN_GIA');
                    L_HAO_MON.DB_ID := L_HAO_MON_OBJECT.GET_NUMBER('DB_ID');
                    INSERT INTO KT_HAO_MON_TAI_SAN (
                        TAI_SAN_ID,
                        MA_TAI_SAN,
                        NAM_HAO_MON,
                        GIA_TRI_HAO_MON,
                        TONG_HAO_MON_LUY_KE,
                        TONG_GIA_TRI_CON_LAI,
                        TY_LE_HAO_MON,
                        TONG_NGUYEN_GIA,
                        DB_ID
                    ) VALUES (
                        L_ID,
                        L_TAI_SAN.MA,
                        L_HAO_MON.NAM_HAO_MON,
                        L_HAO_MON.GIA_TRI_HAO_MON,
                        L_HAO_MON.TONG_HAO_MON_LUY_KE,
                        L_HAO_MON.TONG_GIA_TRI_CON_LAI,
                        L_HAO_MON.TY_LE_HAO_MON,
                        L_HAO_MON.TONG_NGUYEN_GIA,
                        L_HAO_MON.DB_ID
                    );

            END;
               
           -- END IF;

        END LOOP;
    END IF;

--insert kt khau hao

    L_KHAU_HAO_ARR := L_JSON_OBJECT.GET_ARRAY('LST_KHAU_HAO');
    IF L_KHAU_HAO_ARR IS NOT NULL AND L_KHAU_HAO_ARR.GET_SIZE > 0 THEN
        FOR I IN 0..L_KHAU_HAO_ARR.GET_SIZE - 1 LOOP
            L_KHAU_HAO_OBJECT := TREAT(L_KHAU_HAO_ARR.GET(I) AS JSON_OBJECT_T);
           -- l_khau_hao_id := l_khau_hao_object.GET_NUMBER('ID');
           -- IF l_khau_hao_id > 0 THEN
           --     BEGIN
           --         SELECT 
           --             * INTO L_KHAU_HAO
           --         FROM
           --             KT_KHAU_HAO_TAI_SAN
           --         WHERE 
           --             ID = l_khau_hao_id;
           --         --
           --         L_KHAU_HAO.NAM_KHAU_HAO := l_khau_hao_object.GET_NUMBER('NAM_KHAU_HAO');
           --         L_KHAU_HAO.GIA_TRI_KHAU_HAO := l_khau_hao_object.GET_NUMBER('GIA_TRI_KHAU_HAO');
           --         L_KHAU_HAO.TONG_KHAU_HAO_LUY_KE := l_khau_hao_object.GET_NUMBER('TONG_KHAU_HAO_LUY_KE');
           --         L_KHAU_HAO.TONG_GIA_TRI_CON_LAI := l_khau_hao_object.GET_NUMBER('TONG_GIA_TRI_CON_LAI');
           --         L_KHAU_HAO.TY_LE_KHAU_HAO := l_khau_hao_object.GET_NUMBER('TY_LE_KHAU_HAO');
           --         L_KHAU_HAO.TONG_NGUYEN_GIA := l_khau_hao_object.GET_NUMBER('TONG_NGUYEN_GIA');
           --         L_KHAU_HAO.THANG_KHAU_HAO := l_khau_hao_object.GET_NUMBER('THANG_KHAU_HAO');

           --         UPDATE KT_KHAU_HAO_TAI_SAN
           --         SET 
           --             NAM_KHAU_HAO = L_KHAU_HAO.NAM_KHAU_HAO,
           --             GIA_TRI_KHAU_HAO = L_KHAU_HAO.GIA_TRI_KHAU_HAO,
           --             TONG_KHAU_HAO_LUY_KE = L_KHAU_HAO.TONG_KHAU_HAO_LUY_KE,
           --             TONG_GIA_TRI_CON_LAI = L_KHAU_HAO.TONG_GIA_TRI_CON_LAI,
           --             TY_LE_KHAU_HAO = L_KHAU_HAO.TY_LE_KHAU_HAO,
           --             TONG_NGUYEN_GIA = L_KHAU_HAO.TONG_NGUYEN_GIA,
           --             THANG_KHAU_HAO = L_KHAU_HAO.THANG_KHAU_HAO
           --         WHERE ID = l_khau_hao_id;
           --     EXCEPTION
           --         WHEN NO_DATA_FOUND THEN
           --         L_HAO_MON:=NULL;
           --     END;
           -- ELSE
            BEGIN
                L_NAM := L_KHAU_HAO_OBJECT.GET_NUMBER('NAM_KHAU_HAO');
                SELECT
                    *
                INTO L_KHAU_HAO
                FROM
                    KT_KHAU_HAO_TAI_SAN
                WHERE
                    TAI_SAN_ID = L_ID
                    AND NAM_KHAU_HAO = L_NAM;
                --

                L_KHAU_HAO.NAM_KHAU_HAO := L_KHAU_HAO_OBJECT.GET_NUMBER('NAM_KHAU_HAO');
                L_KHAU_HAO.GIA_TRI_KHAU_HAO := L_KHAU_HAO_OBJECT.GET_NUMBER('GIA_TRI_KHAU_HAO');
                L_KHAU_HAO.TONG_KHAU_HAO_LUY_KE := L_KHAU_HAO_OBJECT.GET_NUMBER('TONG_KHAU_HAO_LUY_KE');
                L_KHAU_HAO.TONG_GIA_TRI_CON_LAI := L_KHAU_HAO_OBJECT.GET_NUMBER('TONG_GIA_TRI_CON_LAI');
                L_KHAU_HAO.TY_LE_KHAU_HAO := L_KHAU_HAO_OBJECT.GET_NUMBER('TY_LE_KHAU_HAO');
                L_KHAU_HAO.TONG_NGUYEN_GIA := L_KHAU_HAO_OBJECT.GET_NUMBER('TONG_NGUYEN_GIA');
                L_KHAU_HAO.THANG_KHAU_HAO := L_KHAU_HAO_OBJECT.GET_NUMBER('THANG_KHAU_HAO');
                UPDATE KT_KHAU_HAO_TAI_SAN
                SET
                    NAM_KHAU_HAO = L_KHAU_HAO.NAM_KHAU_HAO,
                    GIA_TRI_KHAU_HAO = L_KHAU_HAO.GIA_TRI_KHAU_HAO,
                    TONG_KHAU_HAO_LUY_KE = L_KHAU_HAO.TONG_KHAU_HAO_LUY_KE,
                    TONG_GIA_TRI_CON_LAI = L_KHAU_HAO.TONG_GIA_TRI_CON_LAI,
                    TY_LE_KHAU_HAO = L_KHAU_HAO.TY_LE_KHAU_HAO,
                    TONG_NGUYEN_GIA = L_KHAU_HAO.TONG_NGUYEN_GIA,
                    THANG_KHAU_HAO = L_KHAU_HAO.THANG_KHAU_HAO
                WHERE
                    ID = L_KHAU_HAO_ID;

            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    L_KHAU_HAO.TAI_SAN_ID := L_ID;
                    L_KHAU_HAO.MA_TAI_SAN := L_TAI_SAN.MA;
                    L_KHAU_HAO.NAM_KHAU_HAO := L_KHAU_HAO_OBJECT.GET_NUMBER('NAM_KHAU_HAO');
                    L_KHAU_HAO.GIA_TRI_KHAU_HAO := L_KHAU_HAO_OBJECT.GET_NUMBER('GIA_TRI_KHAU_HAO');
                    L_KHAU_HAO.TONG_KHAU_HAO_LUY_KE := L_KHAU_HAO_OBJECT.GET_NUMBER('TONG_KHAU_HAO_LUY_KE');
                    L_KHAU_HAO.TONG_GIA_TRI_CON_LAI := L_KHAU_HAO_OBJECT.GET_NUMBER('TONG_GIA_TRI_CON_LAI');
                    L_KHAU_HAO.TY_LE_KHAU_HAO := L_KHAU_HAO_OBJECT.GET_NUMBER('TY_LE_KHAU_HAO');
                    L_KHAU_HAO.TONG_NGUYEN_GIA := L_KHAU_HAO_OBJECT.GET_NUMBER('TONG_NGUYEN_GIA');
                    L_KHAU_HAO.THANG_KHAU_HAO := L_KHAU_HAO_OBJECT.GET_NUMBER('THANG_KHAU_HAO');
                    INSERT INTO KT_KHAU_HAO_TAI_SAN (
                        TAI_SAN_ID,
                        MA_TAI_SAN,
                        NAM_KHAU_HAO,
                        GIA_TRI_KHAU_HAO,
                        TONG_KHAU_HAO_LUY_KE,
                        TONG_GIA_TRI_CON_LAI,
                        TY_LE_KHAU_HAO,
                        TONG_NGUYEN_GIA,
                        THANG_KHAU_HAO
                    ) VALUES (
                        L_ID,
                        L_TAI_SAN.MA,
                        L_KHAU_HAO.NAM_KHAU_HAO,
                        L_KHAU_HAO.GIA_TRI_KHAU_HAO,
                        L_KHAU_HAO.TONG_KHAU_HAO_LUY_KE,
                        L_KHAU_HAO.TONG_GIA_TRI_CON_LAI,
                        L_KHAU_HAO.TY_LE_KHAU_HAO,
                        L_KHAU_HAO.TONG_NGUYEN_GIA,
                        L_KHAU_HAO.THANG_KHAU_HAO
                    );

            END;
               
           -- END IF;

        END LOOP;
    END IF;


-- chay chot hao mon

    OPEN MSS_OUT FOR SELECT
                        JSON_OBJECT ( 'Code' VALUE '00', 'Message' VALUE 'Done', 'IdRecord' VALUE NULL, 'ObjectInfo' VALUE L_TAI_SAN.MA ) AS STRJSON
                    FROM
                        DUAL;

    INSERT INTO QL_HOAT_DONG (
        IP_TRUY_CAP,
        MO_TA,
        DOI_TUONG_ID
    ) VALUES (
        '127.0.0',
        'inser tai san bang store',
        L_TAI_SAN.ID
    );

    RETURN;
END INSERT_TAISAN_KHO;