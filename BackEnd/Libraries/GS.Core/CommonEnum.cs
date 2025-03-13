using System;

namespace GS.Core
{
    public static class enumTENCAUHINH
    {
        public static string DON_VI_IS_AUTO_DUYET = "donvicauhinh.isautoduyettaisanduoi500";
        public static string TS_KHOA_SO_HE_THONG = "cauhinhsotaisan.khoasohethong";
        public static string KeySearch = "SearchModel";
    }
    public static class enumColorCodeLoaiHinhTS
    {
        public static string DAT = "#C95520";
        public static string NHA = "#388e3c";
        public static string VKT = "#00838f";
        public static string OTO = "#d4e157";
        public static string PHUONGTIENKHAC = "#e57373";
        public static string CAYLAUNAM = "#7e57c2";
        public static string HUUHINH = "#ffeb3b";
        public static string VOHINH = "#546e7a";
        public static string DACTHU = "#ffd180";
        public static string MAYMOC = "#ff8a80";
        public static string KHAC2 = "#00e676";
        public static string KHAC3 = "#b71c1c";
        public static string KHAC4 = "#03a9f4";
        public static string KHAC5 = "#ffebee";
    }
    public enum enumDonViTien
    {
        Dong = 1,
        NghinDong = 1000,
        TrieuDong = 1000000,
        TyDong = 1000000000
    }

    public enum enumDonViDienTich
    {
        MetVuong = 1,
        NghinMetVuong = 1000,
        MuoiNghinMetVuong = 10000
    }

    public enum enumDonViKhoiLuong
    {
        Gram = 1,
        Kilogram = 1000,
    }
    public enum enumSoLuong
    {
        CAI_KHUON_VIEN = 1
    }

    public enum enumCompare
    {
        All = 0,
        SmallerOrEqual = 1,
        Smaller = 2,
        Equal = 3,
        Larger = 4,
        LargerOrEqual = 5,
        InRange = 6
    }
    public enum enumCapBaoCao
    {
        Cap_6 = 6,
        Cap_5 = 5,
        Cap_4 = 4,
        Cap_3 = 3,
        Cap_2 = 2,
        Cap_1 = 1,
    }
    public enum enumMucDichSuDung
    {
         ALL = 0,
        TRU_SO_LAM_VIEC = 72,
        HDSN_KHONG_KINH_DOANH = 73,
        HDSN_KINH_DOANH = 75,
        LAM_NHA_O = 181,
        CHO_THUE = 78,
        LDLK = 79,
        BO_TRONG = 182,
        BI_LAN_CHIEM = 183,
        SU_DUNG_KHAC = 208
    } 
    public enum enumCapDonVi
    {
        Cap_5 = 5,
        Cap_4 = 4,
        Cap_3 = 3,
        Cap_2 = 2,
        Cap_1 = 1,
    }
    public enum enumLyDoGiamBC
    {
        TatCa = 0,
        DieuChuyen = 1,
        ThanhLy = 2,
        BanChuyenNhuong = 3,
        BiThuHoi = 4,
        TieuHuy = 5,
        ChuyenGiao = 6,
        Khac = 7
    }
    public enum enumLyDoTangBC
    {
        TatCa = 0,
        MuaSam = 1,
        TiepNhap = 2,
        DangKyLanDau = 3,
        DiThue = 4,
        NhaNuocGiaoDat = 5,
        NhaNuocChoThueDat = 6
    }
    public enum enumLyDoBienDongBC
    {
        //TatCa = 0,
        //#region Tăng
        //MuaSam = 1,
        //TiepNhan = 2,
        //DangKyLanDau = 3,
        //NhaNuocGiaoDat = 4,
        //NhaNuocChoThueDat = 5,
        //DauTuXayDung = 6,
        //KiemKePhatHienThua = 7,
        //#endregion
        //#region Giảm
        //BanChuyenNhuong = 8,
        //BiThuHoi = 9,
        //ChuyenGiao = 10,
        //DieuChuyen = 11,
        //ThanhLy = 12,
        //TieuHuy = 13,
        //HuyHoai = 14,
        //Khac = 15
        //#endregion

        TatCa = 0,
        #region Tăng
        DangKyLanDau = 001,
        NhaNuocGiaoDat = 002,
        NhaNuocChoThueDat = 003,
        DauTuXayDung = 004,
        TiepNhan = 005,
        MuaSam = 006,
        KiemKePhatHienThua = 007,
        #endregion
        #region Giảm
        BanChuyenNhuong = 008,
        BiThuHoi = 009,
        ChuyenGiao = 010,
        DieuChuyen = 011,
        ThanhLy = 012,
        TieuHuy = 013,
        HuyHoai = 014,
        Khac = 015
        #endregion
    }
    public static class enumLyDoBienDongBCTest
    {
        public static string TatCa = "000";
        public static string DangKyLanDau = "001";
        public static string NhaNuocGiaoDat = "002";
        public static string NhaNuocChoThueDat = "003";
        public static string DauTuXayDung = "004";
        public static string TiepNhan = "005";
        public static string MuaSam = "006";
        public static string KiemKePhatHienThua = "007";
        public static string BanChuyenNhuong = "008";
        public static string BiThuHoi = "009";
        public static string ChuyenGiao = "0010";
        public static string DieuChuyen = "011";
        public static string ThanhLy = "012";
        public static string TieuHuy = "013";
        public static string HuyHoai = "014";
        public static string Khac = "015";
    }
    public enum enumTinhHuyenXa
    {
        Tinh = 1,
        Huyen = 2,
        Xa = 3
    }
    public enum enumTinhHuyenXaTrungUong
    {
        TrungUong = 0,
        Tinh = 1,
        Huyen = 2,
        Xa = 3
    }
    public enum enumHanhDongListDuyetTaiSan
    {
        DUYET = 1,
        BO_DUYET = 2
    }
    public enum enumHanhDongListDuyetBaoCao
    {
        DUYET = 1,
        BO_DUYET = 2
    }
    public enum enumHeThong
    {
        QLTSC_DKTS = 1,
        DKTS = 2,
        CTNS = 3,
        HTDB = 4,
        KHO = 5,
        QLTSNN = 6,
        QLTSC_QLTSNN = 7
    }
    public enum enumPhanBaoCao
    {
        TONG_HOP_CHUNG = 0,
        LOAI_HINH_DON_VI = 1,
        DON_VI_TRUC_THUOC = 2,
        BAO_CAO_1A = 3
    }
    public enum enumNamBaoCao
    {
        NAM_2018 = 0,
        NAM_2019 = 1,
        NAM_2020 = 2
    }
    public enum enumTrangThaiBaoCao
    {
        TAT_CA = -1,
        MOI_TAO = 0,
        CHO_DUYET = 1,
        TU_CHOI = 2,
        DA_DUYET = 3,
        DA_GUI_CAP_TREN = 4,
        CAP_TREN_DA_DUYET = 5
    }
    public static class enumMA_BAO_CAO
    {
        #region Công cụ dụng cụ

        public static string CCDC_KiemKe = "CCDC_KiemKe";
        public static string CCDC_KiemKeBoPhan = "CCDC_KiemKeBoPhan";
        public static string CCDC_TongHopKiemKe = "CCDC_TongHopKiemKe";
        public static string CCDC_LietKe = "CCDC_LietKe";
        public static string CCDC_LietKeBoPhan = "CCDC_LietKeBoPhan";
        public static string CCDC_TangGiamCongCu = "CCDC_TangGiamCongCu";
        public static string CCDC_TangCongCu = "CCDC_TangCongCu";
        public static string CCDC_GiamCongCu = "CCDC_GiamCongCu";
        public static string CCDC_SuaChuaCongCu = "CCDC_SuaChuaCongCu";
        public static string CCDC_BaoHongMat = "CCDC_BaoHongMat";
        public static string CCDC_PhanBoCongCu = "CCDC_PhanBoCongCu";
        public static string CCDC_BienBanKiemKe = "CCDC_BienBanKiemKe";
        public static string CCDC_TongHopTon = "CCDC_TongHopTon";
        public static string CCDC_TongHop = "CCDC_TongHop";

        #endregion Công cụ dụng cụ
        #region Báo cáo Chế độ kế toán

        public static string CDKT_KiemKeTaiSan = "CDKT_KiemKeTaiSan";
        public static string CDKT_KiemKeTaiSanPhongBan = "CDKT_KiemKeTaiSanPhongBan";
        public static string CDKT_BS03_MS_B04H_BC_THTANGGIAM_TSCD = "CDKT_BS03_MS_B04H_BC_THTANGGIAM_TSCD";
        public static string CDKT_BS04_MS_C55A_HD_BANG_TINH_HAO_MON = "CDKT_BS04_MS_C55A_HD_BANG_TINH_HAO_MON";
        public static string CDKT_BS05_BANG_TINH_KHAU_HAO_TSCD = "CDKT_BS05_BANG_TINH_KHAU_HAO_TSCD";
        public static string CDKT_BS06_SOGHITANG_TSCD = "CDKT_BS06_SOGHITANG_TSCD";
        public static string CDKT_BS07_SOGHIGIAM_TSCD = "CDKT_BS07_SOGHIGIAM_TSCD";
        public static string CDKT_BS08_MS_S31H_SO_TSCD = "CDKT_BS08_MS_S31H_SO_TSCD";
        public static string CDKT_B09_S32H_SO_TS = "CDKT_B09_S32H_SO_TS";
        public static string CDKT_B10_S32H_CCDC = "CDKT_B10_S32H_CCDC";
        public static string CDKT_B11_S32H_SO_TS_CCDC = "CDKT_B11_S32H_SO_TS_CCDC";
        public static string CDKT_B08_S24H_SO_TS = "CDKT_B08_S24H_SO_TS";
        public static string CDKT_B11_S24H_SO_TS_CCDC = "CDKT_B11_S24H_SO_TS_CCDC";
        public static string CDKT_BienBanKiemKe = "CDKT_BienBanKiemKe";
        #endregion Báo cáo Chế độ kế toán
        #region Tài sản toàn dân

        public static string TSTD_ThongTinTaiSan = "TSTD_ThongTinTaiSan";
        public static string TSTD_PhuongAnXuLy = "TSTD_PhuongAnXuLy";
        public static string TSTD_KetQuaXuLy = "TSTD_KetQuaXuLy";
        public static string TSTD_TinhHinhXuLy = "TSTD_TinhHinhXuLy";
        public static string TSTD_TongHop = "TSTD_TongHop";
        public static string TSTD_MAU_01_BC_XLSHTD = "TSTD_MAU_01_BC_XLSHTD";
        public static string TSTD_MAU_02_BC_XLSHTD = "TSTD_MAU_02_BC_XLSHTD";
        public static string TSTD_MAU_03_BC_XLSHTD = "TSTD_MAU_03_BC_XLSHTD";
        public static string TSTD_MAU_04_BC_XLSHTD = "TSTD_MAU_04_BC_XLSHTD";
        public static string TSTD_MAU_05_BC_XLSHTD = "TSTD_MAU_05_BC_XLSHTD";
        public static string TSTD_MAU_06_BC_XLSHTD = "TSTD_MAU_06_BC_XLSHTD";

        #endregion Tài sản toàn dân
        #region Báo cáo đối chiếu dữ liệu
        public static string TS_DOICHIEU_DATA_02A_DK_TSNN = "TS_DOICHIEU_DATA_02A_DK_TSNN";
        public static string TS_DOICHIEU_DATA_02A_DK_TSNN_p1 = "TS_DOICHIEU_DATA_02A_DK_TSNN_p1";
        public static string TS_DOICHIEU_DATA_02A_DK_TSNN_p2 = "TS_DOICHIEU_DATA_02A_DK_TSNN_p2";
        public static string TS_DOICHIEU_DATA_02A_DK_TSNN_p3 = "TS_DOICHIEU_DATA_02A_DK_TSNN_p3";
        public static string TS_DOICHIEU_DATA_02B_DK_TSNN = "TS_DOICHIEU_DATA_02B_DK_TSNN";
        public static string TS_DOICHIEU_DATA_02B_DK_TSNN_p1 = "TS_DOICHIEU_DATA_02B_DK_TSNN_p1";
        public static string TS_DOICHIEU_DATA_02B_DK_TSNN_p2 = "TS_DOICHIEU_DATA_02B_DK_TSNN_p2";
        public static string TS_DOICHIEU_DATA_02B_DK_TSNN_p3 = "TS_DOICHIEU_DATA_02B_DK_TSNN_p3";
        #endregion
        #region Báo cáo tài sản chi tiết
        public static string TS_BCCT_1A_DK_TSNN_DV_SD = "TS_BCCT_1A_DK_TSNN_DV_SD";
        public static string TS_BCCT_1B_HienTrangSuDungDatNha = "TS_BCCT_1B_HienTrangSuDungDatNha";
        public static string BCCT_01C_DK_TSNN_TangGiamTSNNDonViSuDung = "BCCT_01C_DK_TSNN_TangGiamTSNNDonViSuDung";
        public static string BCCT_01D_DK_TSNN_BCTangTSNNDonViSuDung = "BCCT_01D_DK_TSNN_BCTangTSNNDonViSuDung";
        public static string BCCT_01E_DK_TSNN_BCGiamTSNNDonViSuDung = "BCCT_01E_DK_TSNN_BCGiamTSNNDonViSuDung";
        public static string BCCT_01F_DK_TSNN_BCKhauHaoHaoMonDonViSuDung = "BCCT_01F_DK_TSNN_BCKhauHaoHaoMonDonViSuDung";
        public static string BCCT_01G_DK_TSNN_BCBanThanhLyTSNN = "BCCT_01G_DK_TSNN_BCBanThanhLyTSNN";
        public static string BCCT_01H_DK_TSNN_BCBanThanhLyTSNN = "BCCT_01H_DK_TSNN_BCBanThanhLyTSNN";
        public static string BCCT_B03_CCTT_HUUHINH = "BCCT_B03_CCTT_HUUHINH";
        public static string BCCT_B03_CCTT_VOHINH = "BCCT_B03_CCTT_VOHINH";
        public static string TS_BCCT_BCTDXNTS_DKTS = "TS_BCCT_BCTDXNTS_DKTS";
        #endregion
        #region Báo cáo tài sản tổng hợp
        public static string TS_BCTH_02A_DK_TSNN = "TS_BCTH_02A_DK_TSNN";
        public static string TS_BCTH_02A_DK_TSNN_p1 = "TS_BCTH_02A_DK_TSNN_p1";
        public static string TS_BCTH_02A_DK_TSNN_p2 = "TS_BCTH_02A_DK_TSNN_p2";
        public static string TS_BCTH_02A_DK_TSNN_p3 = "TS_BCTH_02A_DK_TSNN_p3";

        public static string TS_BCTH_02B_DK_TSNN = "TS_BCTH_02B_DK_TSNN";
        public static string TS_BCTH_02B_DK_TSNN_p1 = "TS_BCTH_02B_DK_TSNN_p1";
        public static string TS_BCTH_02B_DK_TSNN_p2 = "TS_BCTH_02B_DK_TSNN_p2";
        public static string TS_BCTH_02B_DK_TSNN_p3 = "TS_BCTH_02B_DK_TSNN_p3";

        public static string TS_BCTH_02C_DK_TSNN = "TS_BCTH_02C_DK_TSNN";
        public static string TS_BCTH_02C_DK_TSNN_p1 = "TS_BCTH_02C_DK_TSNN_p1";
        public static string TS_BCTH_02C_DK_TSNN_p2 = "TS_BCTH_02C_DK_TSNN_p2";
        public static string TS_BCTH_02C_DK_TSNN_p3 = "TS_BCTH_02C_DK_TSNN_p3";

        public static string TS_BCTH_02D_DK_TSNN = "TS_BCTH_02D_DK_TSNN";
        public static string TS_BCTH_02D_DK_TSNN_p1 = "TS_BCTH_02D_DK_TSNN_p1";
        public static string TS_BCTH_02D_DK_TSNN_p2 = "TS_BCTH_02D_DK_TSNN_p2";
        public static string TS_BCTH_02D_DK_TSNN_p3 = "TS_BCTH_02D_DK_TSNN_p3";

        public static string TS_BCTH_02E_DK_TSNN = "TS_BCTH_02E_DK_TSNN";
        public static string TS_BCTH_02E_DK_TSNN_p1 = "TS_BCTH_02E_DK_TSNN_p1";
        public static string TS_BCTH_02E_DK_TSNN_p2 = "TS_BCTH_02E_DK_TSNN_p2";
        public static string TS_BCTH_02E_DK_TSNN_p3 = "TS_BCTH_02E_DK_TSNN_p3";

        public static string TS_BCTH_02F_DK_TSNN = "TS_BCTH_02F_DK_TSNN";
        public static string TS_BCTH_02F_DK_TSNN_p1 = "TS_BCTH_02F_DK_TSNN_p1";
        public static string TS_BCTH_02F_DK_TSNN_p2 = "TS_BCTH_02F_DK_TSNN_p2";
        public static string TS_BCTH_02F_DK_TSNN_p3 = "TS_BCTH_02F_DK_TSNN_p3";

        public static string TS_BCTH_02G_DK_TSNN = "TS_BCTH_02G_DK_TSNN";
        public static string TS_BCTH_02G_DK_TSNN_p1 = "TS_BCTH_02G_DK_TSNN_p1";
        public static string TS_BCTH_02G_DK_TSNN_p2 = "TS_BCTH_02G_DK_TSNN_p2";
        public static string TS_BCTH_02G_DK_TSNN_p3 = "TS_BCTH_02G_DK_TSNN_p3";

        public static string TS_BCTH_02H_DK_TSNN = "TS_BCTH_02H_DK_TSNN";
        public static string TS_BCTH_02H_DK_TSNN_p1 = "TS_BCTH_02H_DK_TSNN_p1";
        public static string TS_BCTH_02H_DK_TSNN_p2 = "TS_BCTH_02H_DK_TSNN_p2";
        public static string TS_BCTH_02H_DK_TSNN_p3 = "TS_BCTH_02H_DK_TSNN_p3";

        public static string TS_BCTH_B03_CCTT_HUUHINH = "TS_BCTH_B03_CCTT_HUUHINH";
        public static string TS_BCTH_B03_CCTT_VOHINH = "TS_BCTH_B03_CCTT_VOHINH";

        public static string TS_BCTH_08A_DK_TSC_P1 = "TS_BCTH_08A_DK_TSC_P1";
        public static string TS_BCTH_08A_DK_TSC_P2 = "TS_BCTH_08A_DK_TSC_P2";
        public static string TS_BCTH_08A_DK_TSC_P3 = "TS_BCTH_08A_DK_TSC_P3";

        public static string TS_BCTH_08B_DK_TSC_P1 = "TS_BCTH_08B_DK_TSC_P1";
        public static string TS_BCTH_08B_DK_TSC_P2 = "TS_BCTH_08B_DK_TSC_P2";
        public static string TS_BCTH_08B_DK_TSC_P3 = "TS_BCTH_08B_DK_TSC_P3";
        #endregion
        #region Báo cáo tài sản quốc hội
        public static string TS_BCQH_MAU01_THTSNN = "TS_BCQH_MAU01_THTSNN";
        public static string TS_BCQH_MAU02_CCTSNN = "TS_BCQH_MAU02_CCTSNN";
        public static string TS_BCQH_MAU03_QUYDAT_MDSD = "TS_BCQH_MAU03_QUYDAT_MDSD";
        public static string TS_BCQH_MAU04_TS_LTS = "TS_BCQH_MAU04_TS_LTS";
        public static string TS_BCQH_MAU05_TS_CQ_TC_DV = "TS_BCQH_MAU05_TS_CQ_TC_DV";
        public static string TS_BCQH_MAU06_TS_CAPQL = "TS_BCQH_MAU06_TS_CAPQL";
        public static string TS_BCQH_MAU07_OTO_SD = "TS_BCQH_MAU07_OTO_SD";
        public static string TS_BCQH_MAU08_OTO_VSD = "TS_BCQH_MAU08_OTO_VSD";
        public static string TS_BCQH_MAU09_DAT_SD = "TS_BCQH_MAU09_DAT_SD";
        public static string TS_BCQH_MAU10_TS_TREN500_MDSD = "TS_BCQH_MAU10_TS_TREN500_MDSD";
        public static string TS_BCQH_MAU11A_THTSNN = "TS_BCQH_MAU11A_THTSNN";
        public static string TS_BCQH_MAU11B_THTSNN = "TS_BCQH_MAU11B_THTSNN";
        public static string TS_BCQH_CT_OTOCD = "TS_BCQH_CT_OTOCD";
        public static string TS_BCQH_TH_OTOCD_THC = "TS_BCQH_TH_OTOCD_THC";
        public static string TS_BCQH_TH_OTOCD_DV = "TS_BCQH_TH_OTOCD_DV";
        public static string TS_BCQH_QLSD_DAT_TSC = "TS_BCQH_QLSD_KVD_TSC";
        public static string TS_BCQH_QLSD_NHA_TSC = "TS_BCQH_QLSD_NHA_TSC";
        public static string TS_BCQH_QLSD_OTO_TSC = "TS_BCQH_QLSD_OTO_TSC";
        public static string TS_BCQH_TANG_GIAM_PL02_TSNN = "TS_BCQH_TANG_GIAM_PL02_TSNN";
        public static string TS_BCQH_TANG_GIAM_PL03_TSNN = "TS_BCQH_TANG_GIAM_PL03_TSNN";
        public static string TS_BCQH_TANG_GIAM_PL05_TSNN = "TS_BCQH_TANG_GIAM_PL05_TSNN";
        public static string TS_BCQH_TANG_GIAM_PL06_TSNN = "TS_BCQH_TANG_GIAM_PL06_TSNN";
        public static string TS_BCQH_TANG_GIAM_PL07_TSNN = "TS_BCQH_TANG_GIAM_PL07_TSNN";
        public static string TS_BCQH_TANG_GIAM_PL08_SD_OTO = "TS_BCQH_TANG_GIAM_PL08_SD_OTO";
        public static string TS_BCQH_TANG_GIAM_PL09_NG500 = "TS_BCQH_TANG_GIAM_PL09_NG500";
        public static string TS_BCQH_TANG_GIAM_PL10_TSNN = "TS_BCQH_TANG_GIAM_PL10_TSNN";
        public static string TS_BCQH_TANG_GIAM_PL12_DV = "TS_BCQH_TANG_GIAM_PL12_DV";
        public static string TS_BCQH_TANG_GIAM_PL13_HD = "TS_BCQH_TANG_GIAM_PL13_HD";

        #endregion
        #region Báo cáo tài sản công khai
        public static string TS_BCCK_M01_KHMS_TS = "TS_BCCK_M01_KHMS_TS";
        public static string TS_BCCK_M02_KQMS_TS = "TS_BCCK_M02_KQMS_TS";
        public static string TS_BCCK_M03_QLNHA_DAT = "TS_BCCK_M03_QLNHA_DAT";
        public static string TS_BCCK_M04_QLOTO_KHAC = "TS_BCCK_M04_QLOTO_KHAC";
        public static string TS_BCCK_M05_THUE_TS = "TS_BCCK_M05_THUE_TS";
        public static string TS_BCCK_M06_CHUYENDOI_SOHUU = "TS_BCCK_M06_CHUYENDOI_SOHUU";
        public static string TS_BCCK_M07_KQ_MUASAM = "TS_BCCK_M07_KQ_MUASAM";
        public static string TS_BCCK_9a_CK_TSC_TINHHINH_DAUTU = "TS_BCCK_9a_CK_TSC_TINHHINH_DAUTU";
        public static string TS_BCCK_9b_CK_TSC_QL_SD_TRU_SO = "TS_BCCK_9b_CK_TSC_QL_SD_TRU_SO";
        public static string TS_BCCK_9c_CK_TSC_QL_SD_OTO_KHAC = "TS_BCCK_9c_CK_TSC_QL_SD_OTO_KHAC";

        public static string TS_BCCK_9d_CK_TSC_XULY_TS = "TS_BCCK_9d_CK_TSC_XULY_TS";
        public static string TS_BCCK_9dd_CK_TSC_KHAITHAC_TC = "TS_BCCK_9dd_CK_TSC_KHAITHAC_TC";

        public static string TS_BCCK_10a_CK_TSC_DAUTU_XD_THUE = "TS_BCCK_10a_CK_TSC_DAUTU_XD_THUE";
        public static string TS_BCCK_10b_CK_TSC_QLSD_TS = "TS_BCCK_10b_CK_TSC_QLSD_TS";
        public static string TS_BCCK_10c_CK_TSC_XL_TS = "TS_BCCK_10c_CK_TSC_XL_TS";
        public static string TS_BCCK_10d_CK_TSC_KTNL_TS = "TS_BCCK_10d_CK_TSC_KTNL_TS";

        public static string TS_BCCK_11a_CK_TSC_NGUONLUC_TC_TS = "TS_BCCK_11a_CK_TSC_NGUONLUC_TC_TS";
        public static string TS_BCCK_11b_CK_TSC_QL_SD_TS = "TS_BCCK_11b_CK_TSC_QL_SD_TS";
        public static string TS_BCCK_11c_CK_TSC_XL_TS = "TS_BCCK_11c_CK_TSC_XL_TS";
        public static string TS_BCCK_11d_CK_TSC_KTNL_TS = "TS_BCCK_11d_CK_TSC_KTNL_TS";



        public static string TS_BCCK_TDMS = "TS_BCCK_TDMS";
        public static string TS_BCCK_THMS = "TS_BCCK_THMS";
        public static string TS_BCCK_DM_TS_DIEU_CHUYEN_BAN_GIAO = "TS_BCCK_DM_TS_DIEU_CHUYEN_BAN_GIAO";

        #endregion
        #region Báo cáo kê khai tài sản 
        public static string TS_BCKK_01_DKTSNN_KEKHAITRUSO = "TS_BCKK_01_DKTSNN_KEKHAITRUSO";
        public static string TS_BCKK_02_DKTSNN_KEKHAIOTO = "TS_BCKK_02_DKTSNN_KEKHAIOTO";
        public static string TS_BCKK_03_DKTSNN_KEKHAITS500 = "TS_BCKK_03_DKTSNN_KEKHAITS500";
        public static string TS_BCKK_04a_DKTSC_KEKHAITRUSO = "TS_BCKK_04a_DKTSC_KEKHAITRUSO";
        public static string TS_BCKK_04b_DKTSC_KEKHAIOTO = "TS_BCKK_04b_DKTSC_KEKHAIOTO";
        public static string TS_BCKK_04c_DKTSNN_KEKHAITSKHAC = "TS_BCKK_04c_DKTSNN_KEKHAITSKHAC";
        public static string TS_BCKK_05a_DKTSDA_KEKHAITRUSO = "TS_BCKK_05a_DKTSDA_KEKHAITRUSO";
        public static string TS_BCKK_05b_DKTSDA_KEKHAIOTO = "TS_BCKK_05b_DKTSDA_KEKHAIOTO";
        public static string TS_BCKK_05c_DKTSDA_KEKHAIKHAC = "TS_BCKK_05c_DKTSDA_KEKHAIKHAC";
        public static string TS_BCKK_06a_DKTSC_DVTDTTDON_VI = "TS_BCKK_06a_DKTSC_DVTDTTDON_VI";
        public static string TS_BCKK_06b_DKTSC_DVTDTTNHA_DAT = "TS_BCKK_06b_DKTSC_DVTDTTNHA_DAT";
        public static string TS_BCKK_06c_DKTSC_DVTDTTOTO = "TS_BCKK_06c_DKTSC_DVTDTTOTO";
        public static string TS_BCKK_06d_DKTSC_DVTDTTKHAC = "TS_BCKK_06d_DKTSC_DVTDTTKHAC";
        public static string TS_BCKK_07_DKTSC_XOATS_CSDL = "TS_BCKK_07_DKTSC_XOATS_CSDL";
        public static string TS_BCKK_01_DMTSNN_GIAM_NHA_DAT = "TS_BCKK_01_DMTSNN_GIAM_NHA_DAT";
        public static string TS_BCKK_02_DMTSNN_GIAM_OTO = "TS_BCKK_02_DMTSNN_GIAM_OTO";
        public static string TS_BCKK_03_DMTSNN_GIAM_KHAC = "TS_BCKK_03_DMTSNN_GIAM_KHAC";


        #endregion
        #region Báo cáo tra cứu số liệu
        public static string TS_BCTC_03_DK_TSNN = "TS_BCTC_03_DK_TSNN";
        public static string TS_BCTC_04_DK_TSNN = "TS_BCTC_04_DK_TSNN";
        #endregion
        #region CongCuKiemTra
        public static string CCKTTaiSanSoLieuDaNhap_PL02 = "CCKTTaiSanSoLieuDaNhap_PL02";
        public static string CCKTTaiSanSoLieuDaNhap_PL03 = "CCKTTaiSanSoLieuDaNhap_PL03";
        #endregion
        #region Báo cáo ban quản lý dự án
        public static string TS_BCDA_01C_DK_TSDA = "TS_BCDA_01C_DK_TSDA";
        public static string TS_BCDA_02A_CTDV_TSDA = "TS_BCDA_02A_CTDV_TSDA";
        public static string TS_BCDA_02A_THC_TSDA = "TS_BCDA_02A_THC_TSDA";
        public static string TS_BCDA_02B_TS_TSDA = "TS_BCDA_02B_TS_TSDA";
        public static string TS_BCDA_02C_OT_TSDA = "TS_BCDA_02C_OT_TSDA";
        public static string TS_BCDA_02D_TSK_TSDA = "TS_BCDA_02D_TSK_TSDA";
        public static string TS_BCDA_02E1_KT_TSDA = "TS_BCDA_02E1_KT_TSDA";
        public static string TS_BCDA_02E2_KT_TSDA = "TS_BCDA_02E2_KT_TSDA";
        public static string TS_BCDA_02F1_KT_TSDA = "TS_BCDA_02F1_KT_TSDA";
        public static string TS_BCDA_02F2_KT_TSDA = "TS_BCDA_02F2_KT_TSDA";
        public static string TS_BCDA_02F3_KT_TSDA = "TS_BCDA_02E2_KT_TSDA";
        public static string TS_BCDA_02G_KT_TSDA = "TS_BCDA_02G_KT_TSDA";
        public static string TS_BCDA_02I_KT_TSDA = "TS_BCDA_02I_KT_TSDA";
        public static string TS_BCDA_02K_TS_TSDA = "TS_BCDA_02K_TS_TSDA";
        public static string TS_BCDA_02K1_TS_TSDA = "TS_BCDA_02K1_TS_TSDA";
        public static string TS_BCDA_02H_TS_TSDA = "TS_BCDA_02H_TS_TSDA";
        public static string TS_BCDA_02H1_TS_TSDA = "TS_BCDA_02H1_TS_TSDA";
        public static string TS_BCDA_04_TSDA = "TS_BCDA_04_TSDA";
        public static string TS_BCDA_05_TSDA = "TS_BCDA_05_TSDA";
        #endregion
        #region Báo cáo ban quản lý dự án
        public static string TS_BCDA_01BC_TSDA = "TS_BCDA_01BC_TSDA";
        public static string TS_BCDA_02BC_TSDA = "TS_BCDA_02BC_TSDA";
        public static string TS_BCDA_03BC_TSDA = "TS_BCDA_03BC_TSDA";
        public static string TS_BCDA_04BC_HTSD_KHAC = "TS_BCDA_04BC_HTSD_KHAC";
        public static string TS_BCDA_05BC_TANG_GIAM_TSDA = "TS_BCDA_05BC_TANG_GIAM_TSDA";
        public static string TS_BCDA_04A_TRUSODENGHIXULY = "TS_BCDA_04A_TRUSODENGHIXULY";
        public static string TS_BCDA_04B_OTODENGHIXULY = "TS_BCDA_04B_OTODENGHIXULY";
        public static string TS_BCDA_04C_KHACDENGHIXULY = "TS_BCDA_04C_KHACDENGHIXULY";
        public static string TS_BCDA_01A_DK_TSDA = "TS_BCDA_01A_DK_TSDA";
        public static string TS_BCDA_01B_DK_TSDA = "TS_BCDA_01B_DK_TSDA";
        #endregion
        #region In thẻ tài sản
        public static string TS_IN_THE_TSCD_S25H_TT107 = "TS_IN_THE_TSCD_S25H_TT107";
        public static string TS_IN_THE_TSCD_S11_DNN_TT133 = "TS_IN_THE_TSCD_S11_DNN_TT133";
        public static string TS_IN_THE_KIEM_KE_TAI_SAN = "TS_IN_THE_KIEM_KE_TAI_SAN";
        #endregion In thẻ tài sản
        #region Phiếu xác nhận thông tin tài sản
        public static string TS_PXNTT_MAU_01 = "TS_PXNTT_MAU_01";
        public static string TS_PXNTT_MAU_02 = "TS_PXNTT_MAU_02";
        public static string TS_PXNTT_MAU_03 = "TS_PXNTT_MAU_03";
        #endregion
    }
    public static class enumCSDLQG_MA_NHOM_TAI_SAN
    {
        public const string CoQuanToChuc = "1";
        public const string XacLapToanDan = "2";
        public const string KetCauHaTang = "3";
        /// <summary>
        /// Đất đai
        /// </summary>
        public const string DatDai = "4";
        public const string TaiNguyen = "5";
        public const string TaiSanCongTaiDoanhNghiep = "6";
    }
    #region enum đồng bộ
    /// <summary>
    /// enum loại lý do biến động kho
    /// </summary>
    public enum enumLOAI_LY_DO_TANG_GIAM_KHO
    {
        TANG_TOAN_BO = 1,//	Tăng toàn bộ
        GIAM_TOAN_BO = 2,//	Giảm toàn bộ
        GIAM_DIEU_CHUYEN_MOT_PHAN = 3,//	Giảm một phần
        THAY_DOI_THONG_TIN = 4,//	Thay đổi khác
        GIAM_TOAN_BO5 = 5,//	Giảm toàn bộ
        GIAM_DIEU_CHUYEN_MOT_PHAN6 = 6,//	Giảm điều chuyển một phần
        GIAM_TOAN_BO7 = 7,//	Cập nhật tiền bán thanh lý
        GIAM_TOAN_BO8 = 8,//	Giảm điều chuyển nội bộ
        GIAM_TOAN_BO9 = 9,//	Giảm điều chuyển khác
        GIAM_TOAN_BO10 = 10,//	Điều chuyển ngoài hệ thống
        NHAP_SO_DU_DAU_KY = 11,//	Nhập số dư đầu kỳ

    }
    /// <summary>
    /// enum loại hình tài sản kho
    /// </summary>
    public enum enumLOAI_HINH_TAI_SAN_KHO
    {
        DAT = 7,
        NHA = 8,
        TAI_SAN_VAT_KIEN_TRUC = 9,
        OTO = 10,
        PHUONG_TIEN_KHAC = 11,
        TAI_SAN_CAY_LAU_NAM_SVLV = 13,
        TAI_SAN_MAY_MOC_THIET_BI = 12,
        VO_HINH = 15,
        HUU_HINH_KHAC = 14,
        DAC_THU = 16,
        //Toàn Dân
        TAI_SAN_TOAN_DAN_KHAC = 20,
        TAI_SAN_TOAN_DAN_DAT = 17,
        TAI_SAN_TOAN_DAN_NHA = 18,
        TAI_SAN_TOAN_DAN_OTO = 19,
        TAI_SAN_TOAN_DAN_TAI_SAN_KHAC = 20,
    }
    /// <summary>
    /// enum Hiện trạng sử dung dùng cho đồng bộ tài sản
    /// </summary>
    public enum enumIdNhomTaiSanKho
    {
        CoQuanToChuc = 1,
        XacLapToanDan = 2
    }
    #endregion

    public enum enumNguonVon
    {
        NguonNganSach = 1,
        NguonKhac = 3,
        NguonHoatDongSuNghiep = 2,
        NguonVienTro = 4
    }
    public enum enumDongBoBienDong
    {
        CHO = 0,
        DA_DONG_BO = 2,
        DANG_DONG_BO = 1,
        THAT_BAI = 3,
        DANG_CHAY_JOB = 4
    }
    public static class enumHoatDong
    {
        public static string TaoMoi = "taomoi";
        public static string CapNhat = "CapNhat";
        public static string Xoa = "Xoa";
        public static string DangNhap = "dangnhap";
        public static string Khoa = "Khoa";
        public static string TrinhDuyet = "TrinhDuyet";
        public static string Duyet = "Duyet";
        public static string HuyDuyet = "HuyDuyet";
        public static string DbThanhCong = "DbThanhCong";
        public static string DbThatBai = "DbThatBai";
        public static string DbCho = "DbCho";
        public static string CallWebApi = "CallWebApi";
        public static string CalledWebApi = "CalledWebApi";
        public static string GuiDuLieuLoi = "GuiDuLieuLoi";
        public static string DaGuiDuLieu = "DaGuiDuLieu";
        public static string XoaTaiSanTheoDonVi = "XoaTaiSanTheoDonVi";
        public static string DongBoTaiSan = "DongBoTaiSan";
        public static string RenderReport = "RenderReport";
        public static string ResponseApi = "ResponseApi";
        public static string DbBaoCao = "DbBaoCao";
        public static string DbHaoMon = "DbHaoMon";
    }
    public static class enumSendRequest
    {
        public static string DongBoDonViThuCong = "ConsumingDanhMuc/UpdateDonViThuCong";
        public static string DongBoBaoCao = "ConsumingBaoCao/DongBoBaoCao";
        public static string DongBoXoaBienDong = "ConsumingTaiSan/XoaBienDong";
        public static string DongBoBienDongThuCong = "ConsumingTaiSan/DongBoBienDongThuCong";
        public static string DongBoBienDongKhacTangMoi = "ConsumingTaiSan/DongBoBienDongKhacTangMoi";
        public static string DongBoBienDongTaiSanTangMoi = "ConsumingTaiSan/DongBoBienDongTaiSanTangMoi";
        public static string GetJsonBienDongTangMoi = "ConsumingTaiSan/GetJsonBienDongTangMoi";
        public static string GetJsonBienDongKhacTangMoi = "ConsumingTaiSan/GetJsonBienDongKhacTangMoi";
        public static string CheckLogBienDongKho = "ConsumingTaiSan/CheckLogBienDongKho";
        public static string CheckLogHaoMonKho = "ConsumingTaiSan/CheckLogHaoMonKho";
        public static string DongBoHaoMonTaiSan = "ConsumingTaiSan/DongBoHaoMonTaiSan";
        public static string CapNhatMaTaiSanKho = "ConsumingTaiSan/CapNhatMaTaiSanKho";
        public static string CapNhatMaTaiSanKho2 = "ConsumingTaiSan/CapNhatMaTaiSanKho2";
        public static string DongBoKhaiThacTaiSan = "ConsumingTaiSan/DongBoKhaiThacTaiSan";
        public static string DongBoListTaiSan = "TaiSanSvc/UpdateListTaiSan";
    }



}