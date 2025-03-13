using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Core
{
    public static class CommonDanhMuc
    {
        public static string RequestDanhMuc => "aprserver/category/";
        public static string QuocGia => "countries/";
        public static string NguonGocTaiSan => "assetSources/";
        public static string LoaiTaiSan => "assetTypes/";
        public static string MucDichSuDung => "assetUsagePurposes/";
        public static string HienTrangSuDung => "assetUsageStates/";
        public static string LyDoBienDong => "assetMutationCause/";
        public static string NguoiDung => "users/";
        public static string ResetPassword => "service/api/users/";
        public static string DonVi => "unit/";
        public static string LoaiDonVi => "unitTypes/";
        public static string CayPhanLoaiTaiSan => "assetTypeTrees/";
        public static string TinhThanh_DiaBan => "region/";
        public static string DonViBoPhan => "departments/";
        public static string DuAn => "projects/";
        public static string LoaiLyDoBienDong => "assetMutationCauseType/";
        public static string NhanHieu => "brands/";
        public static string DongSanPham => "productLines/";
        public static string ChucDanh => "userTitles/";
        public static string HinhThucMuaSam => "procurementForms/";
    }
    public static class CommonTaiSan
    {
        public static string RequestTaiSan => "aprserver/asset/";
        public static string RequestTaiSan1 => "asset/";
        public static string TangMoi => "assetIncreasementController";
        public static string TangNguyenGia => "assetIncreaseOriginalValueController";
        public static string GiamNguyenGia => "assetDecreaseOriginalValueController";
        public static string ThayDoiThongTin => "changeAssetInformationController";
        public static string GiamTaiSan => "assetDecreaseController";
        public static string DieuChuyenCungHeThong => "assetTransferController";
        public static string GetMaTaiSan => "getSynchronizedData";
        public static string CheckBienDong => "getSynchronizedDataAssetMutations";
        public static string GetHaoMonKho => "assetAmortizations/getData";
        public static string KhaiThac_QuyetDinh => "assetExploitationDecisions/";
        public static string KhaiThac_KinhDoanh => "business";
        public static string KhaiThac_LienDoanh => "venture";
        public static string KhaiThac_ChoThue => "lease";
        public static string KhaiThac_SoTien => "amount";
        public static string HaoMonTaiSan => "assetAmortizations";
        public static string KhauHaoTaiSan => "assetDepreciations";
        public static string XoaBienDong => "assets/deleteMutations";
    }
    public static class CommonLog
    {
        public static string RequestLog => "searchLogs/";
        public static string SearchLog => "searchingLogsForRequestBody";
        public static string LogTaiSan => "logs-assets";
        public static string LogDanhMuc => "logs-ms-category";
        public static string LogCleaner = "Cleaner";
        public static string LogSaving = "Saving";
    }
    public static class CommonAction
    {
        public static string DongBo => "sync";
        public static string ChiTiet => "detail";
        public static string TatCa => "all";
        public static string Xoa => "delete";
        public static string LayThongTinDongBo => "getBySyncId";
        public static string ChangePassword => "change-password";
        public static string ResetPassword => "reset-password";
        public static string LayThongTinTSTD => "getNationalPublicAssetsBySyncId";
        public static string LayThongTinTaiSanXuLy => "nationalPublicAssetHandlingDecisionPlanAssetsBySyncId";
        public static string LayThongTinXuLy => "nationalPublicAssetHandlingDecisionPlansBySyncId";
        public static string LayThongTinKetQua => "getNationalPublicAssetHandlingDecisionResultAssetsBySyncId";
        public static string LayThongTinThuChi => "getNationalPublicAssetHandlingDecisionAccountingsBySyncId";
        public static string resetPassWord => "getNationalPublicAssetHandlingDecisionAccountingsBySyncId";

    }

    public static class CommonParam
    {
        public static string SyncId => "syncId";

    }
    public static class CommonTSXacLap
    {
        public static string QuyenDinh => "nationalPublicAssetDecisions/";
        public static string TaiSanTd => "nationalPublicAssets/";
        public static string PhuongAnXuLy => "nationalPublicAssetHandlingDecisionPlans/";
        public static string TaiSanXuLy => "nationalPublicAssetHandlingDecisionPlanAssets/";
        public static string KetQuaXuLy => "nationalPublicAssetHandlingDecisionResultAssets/";
        public static string SoSachThuChi => "nationalPublicAssetHandlingDecisionAccountings/";
    }
    public static class CommonActionReport
    {
        public static string RequestReport = "aprserver/report/";
        public static string RPT_08A_DK_TSC_01 = "rpt08adktsc01/sync";//Service đồng bộ báo cáo mẫu 8a-ĐK/TSC - Phần 01
        public static string RPT_08A_DK_TSC_02 = "rpt08adktsc02/sync";//Service đồng bộ báo cáo mẫu 8a-ĐK/TSC - Phần 02
        public static string RPT_08A_DK_TSC_03 = "rpt08adktsc03/sync";//Service đồng bộ báo cáo mẫu 8a-ĐK/TSC - Phần 03
        public static string RPT_08A_DK_TSKCHT_01 = "rpt08adktskcht01/sync";//Service đồng bộ báo cáo mẫu 08a-ĐK/TSKCHT - Phần 01
        public static string RPT_08A_DK_TSKCHT_02 = "rpt08adktskcht02/sync";//Service đồng bộ báo cáo mẫu 08a-ĐK/TSKCHT - Phần 02
        public static string RPT_08A_DK_TSKCHT_03 = "rpt08adktskcht03/sync";//Service đồng bộ báo cáo mẫu 08a-ĐK/TSKCHT - Phần 03
        public static string RPT_08B_DK_TSC_01 = "rpt08bdktsc01/sync";//Service đồng bộ báo cáo mẫu 8b-ĐK/TSC - Phần 01
        public static string RPT_08B_DK_TSC_02 = "rpt08bdktsc02/sync";//Service đồng bộ báo cáo mẫu 8b-ĐK/TSC - Phần 02
        public static string RPT_08B_DK_TSC_03 = "rpt08bdktsc03/sync";//Service đồng bộ báo cáo mẫu 8b-ĐK/TSC - Phần 03
        public static string RPT_08B_DK_TSKCHT = "rpt08bdktskcht01/sync";//Service đồng bộ báo cáo mẫu 08b-ĐK/TSKCHT - Phần 01
        public static string RPT_08B_DK_TSKCHT_02 = "rpt08bdktskcht02/sync";//Service đồng bộ báo cáo mẫu 08b-ĐK/TSKCHT - Phần 02
        public static string RPT_08B_DK_TSKCHT_03 = "//sync";//Service đồng bộ báo cáo mẫu 08b-ĐK/TSKCHT - Phần 03
        public static string RPT_08C_DK_TSKCHT_01 = "rpt08cdktskcht01/sync";//Service đồng bộ báo cáo mẫu 08c -ĐK/TSKCHT - Phần 01
        public static string RPT_08C_DK_TSKCHT_02 = "rpt08cdktskcht02/sync";//Service đồng bộ báo cáo mẫu 08c -ĐK/TSKCHT - Phần 02
        public static string RPT_08D_DK_TSKCHT = "rpt08ddktskcht/sync";//Service đồng bộ báo cáo mẫu 8d -ĐK/TSKCHT
        public static string RPT_09A_CK_TSC = "rpt09acktsc/sync";//Service đồng bộ báo cáo mẫu 09a-CK/TSC
        public static string RPT_09B_CK_TSC = "rpt09bcktsc/sync";//Service đồng bộ báo cáo mẫu 09b-CK/TSC
        public static string RPT_09C_CK_TSC = "rpt09ccktsc/sync";//Service đồng bộ báo cáo mẫu 09c-CK/TSC
        public static string RPT_09D_CK_TSC = "rpt09dcktsc/sync";//Service đồng bộ báo cáo mẫu 09d-CK/TSC
        public static string RPT_09DD_CK_TSC = "rpt09ddcktsc/sync";//Service đồng bộ báo cáo mẫu 09dd-CK/TSC
        public static string RPT_10A_CK_TSC = "rpt10acktsc/sync";//Service đồng bộ báo cáo mẫu 10a-CK/TSC
        public static string RPT_10B_CK_TSC = "rpt10bcktsc/sync";//Service đồng bộ báo cáo mẫu 10b-CK/TSC
        public static string RPT_10C_CK_TSC = "rpt10ccktsc/sync";//Service đồng bộ báo cáo mẫu 10c-CK/TSC
        public static string RPT_10D_CK_TSC = "rpt10dcktsc/sync";//Service đồng bộ báo cáo mẫu 10d-CK/TSC
        public static string RPT_11A_CK_TSC = "rpt11acktsc/sync";//Service đồng bộ báo cáo mẫu 11a-CK/TSC
        public static string RPT_11B_CK_TSC = "rpt11bcktsc/sync";//Service đồng bộ báo cáo mẫu 11b-CK/TSC
        public static string RPT_11C_CK_TSC = "rpt11ccktsc/sync";//Service đồng bộ báo cáo mẫu 11c-CK/TSC
        public static string RPT_11D_CK_TSC = "rpt11dcktsc/sync";//Service đồng bộ báo cáo mẫu 11d-CK/TSC
        public static string RPT_BCCP_01 = "rptbccp01/sync";//Service đồng bộ báo cáo Chính phủ - Phụ lục I
        public static string RPT_BCCP_02 = "rptbccp02/sync";//Service đồng bộ báo cáo Chính phủ - Phụ lục II
        public static string RPT_BCCP_03 = "rptbccp03/sync";//Service đồng bộ báo cáo Chính phủ - Phụ lục III
        public static string RPT_BCCP_04 = "rptbccp04/sync";//Service đồng bộ báo cáo Chính phủ - Phụ lục IV
        public static string RPT_BCCP_05 = "rptbccp05/sync";//Service đồng bộ báo cáo Chính phủ - Phụ lục V
        public static string RPT_BCCP_06 = "rptbccp06/sync";//Service đồng bộ báo cáo Chính phủ - Phụ lục VI
        public static string RPT_BCCP_07 = "rptbccp07/sync";//Service đồng bộ báo cáo Chính phủ - Phụ lục VII
        public static string RPT_BCCP_08 = "rptbccp08/sync";//Service đồng bộ báo cáo Chính phủ - Phụ lục VIII
        public static string RPT_BCCP_09 = "rptbccp09/sync";//Service đồng bộ báo cáo Chính phủ - Phụ lục IX
        public static string RPT_BCCP_10 = "rptbccp10/sync";//Service đồng bộ báo cáo Chính phủ - Phụ lục X
        public static string RPT_BCCP_11A = "rptbccp11a/sync";//Service đồng bộ báo cáo Chính phủ - Phụ lục Xia
        public static string RPT_BCCP_11B = "rptbccp11b/sync";//Service đồng bộ báo cáo Chính phủ - Phụ lục Xib
        public static string RPT_BCTSCTKVHV = "rptbctsctkvhv/sync";//Service đồng bộ báo cáo tài sản thống kê hiện vật
        public static string RPT_B03_CCTT_A = "rptb03cctta/sync";//Service đồng bộ báo cáo B03/CCTT - Tài sản cố định hữu hình trang bị cho đơn vị 
        public static string RPT_B03_CCTT_B = "rptb03ccttb/sync";//Service đồng bộ báo cáo B03/CCTT - Tài sản cố định vô hình trang bị cho đơn vị
        public static string RPT_XLSHTD_01 = "rptxlshtd01/sync";//Service đồng bộ báo cáo RPT_XLSHTD_01 - Kê khai thông tin tài sản được xác lập quyền sở hữu toàn dân phần I
        public static string RPT_XLSHTD_02_P1 = "rptxlshtd02p1/sync";//Service đồng bộ báo cáo RPT_XLSHTD_02_P1 - Phần 1: Tổng hợp chung
        public static string RPT_XLSHTD_02_P2 = "rptxlshtd02p2/sync";//Service đồng bộ báo cáo RPT_XLSHTD_02_P2 - Phần 2: Chi tiết từng đơn vị
        public static string RPT_XLSHTD_03 = "rptxlshtd03/sync";//Service đồng bộ báo cáo RPT_XLSHTD_03 - Dữ liệu báo cáo tổng hợp tình hình tăng tài sản được xác lập sở hữu toàn dân
        public static string RPT_XLSHTD_04 = "rptxlshtd04/sync";//Service đồng bộ báo cáo RPT_XLSHTD_04 - Dữ liệu báo cáo tổng hợp tình hình giảm tài sản được xác lập sở hữu toàn dân
        public static string RPT_XLSHTD_05 = "rptxlshtd05/sync";//Service đồng bộ báo cáo RPT_XLSHTD_05 - Dữ liệu báo cáo tổng hợp số tiền thu được từ xử lý tài sản được xác lập sở hữu toàn dân
        public static string RPT_XLSHTD_06 = "rptxlshtd06/sync";//Service đồng bộ báo cáo RPT_XLSHTD_06 - Dữ liệu báo cáo tổng hợp tình hình giảm tài sản được xác lập sở hữu toàn dân
        public static string RPT_XLSHTD_07 = "rptbccp07/sync";//Service đồng bộ báo cáo RPT_XLSHTD_07 - Báo cáo kê khai thông tin phương án xử lý tài sản được xác lập quyền sở hữu toàn dân
        public static string RPT_XLSHTD_08 = "rptbccp08/sync";//Service đồng bộ báo cáo RPT_XLSHTD_08 - Báo cáo kết quả xử lý tài sản được xác lập quyền sở hữu toàn dân


    }
    public enum enumLoaiDongBo
    {
        ThemMoi = 1,
        CapNhat = 2
    }
    public enum enumKho_Loai_Bien_Dong
    {
        TangMoi = 1,
        TangNguyenGia = 2,
        GiamNguyenGia = 3,
        ThayDoiThongTin = 4,
        Giam = 5,
        DieuChuyen = 6
    }
    public static class CommonFormat
    {
        public static string FormatDate => "dd-MM-yyyy";
    }
    public enum enumLyDoBienDong
    {
        //tăng nguyên giá
        DangKyLanDau = 604,
        TangGiaDat = 619,
        TangDienTichDat = 620,
        DanhGiaLaiNguyenGiaNha = 624,
        NangCapMoRongNha = 622,
        //giảm nguyên giá
        GiamGiaDat = 626,
        GiamDienTichDat = 627,
        Giam_DanhGiaLaiNguyenGiaNha = 630,
        CaiTaoThuHepDienTichNha = 628,
        // giảm tài sản 
        GiamTaiSan_DieuChuyen = 614,
        ChuyenNhuong = 611,
        Ban = 611,
        ThanhLy = 615,
        BiThuHoi = 612,
        ChuyenGiaoVeDiaPhuong = 613,
        TieuHuy = 616,
        BiMat = 617,
        Giam_Khac = 618
    }

    public static class enumDMDC_TranCode
    {
        public static string DonViNganSach = "0016";
        public static string QuocGia = "0038";
        public static string DiaBan = "0049";
    }
}
