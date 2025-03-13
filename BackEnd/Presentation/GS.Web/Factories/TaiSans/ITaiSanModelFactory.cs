//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;
using GS.Web.Models.TaiSans;
using System.Collections.Generic;
using System.Threading.Tasks;
using static GS.Web.Models.TaiSans.TSLogicSoLieuDauKyModel;

namespace GS.Web.Factories.TaiSans
{
    public partial interface ITaiSanModelFactory
    {
        #region TaiSan

        TaiSanSearchModel PrepareTaiSanSearchModel(TaiSanSearchModel searchModel, bool isDuyetBienDong = false, bool isExcutedTSDT = false);
		/// <summary>
		/// Lấy danh sách của cả đơn vị cha và đơn vị con
		/// </summary>
		/// <param name="searchModel"></param>
		/// <returns></returns>
        TaiSanListModel PrepareTaiSanListModel(TaiSanSearchModel searchModel);
		/// <summary>
		/// Lấy danh sách tài sản duy nhất 1 đơn vị
		/// </summary>
		/// <param name="searchModel"></param>
		/// <returns></returns>
        TaiSanListModel PrepareDanhSachTaiSan(TaiSanSearchModel searchModel);
        TSLogicSoLieuDauKyListModel LogicDanhSachTaiSan(BaoCaoTaiSanChiTietSearchModel searchModel);

        TaiSanModel PrepareTaiSanModel(TaiSanModel model, TaiSan item, bool excludeProperties = false, List<decimal> lstTaiSanChon = null, bool? isCreateTSDA = false, bool? isCreateTaiSan = false, bool? tsQLNTSCD = false);

        /// <summary>
        ///  Description: Hàm lấy thông tin tài sản phục vụ view thông tin tài sản
        /// </summary>
        /// <param name="model"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        TaiSanModel PrepareTaiSanModelView(TaiSanModel model, TaiSan item);
        TaiSanModel PrepareTaiSanModelChon(TaiSanModel model, TaiSan item, bool excludeProperties = false, List<decimal> lstTaiSanChon = null, decimal idKhaiThac = 0);
        void PrepareTaiSan(TaiSanModel model, TaiSan item);
        bool CheckMaTaiSan(string Ma, decimal? id = 0);
        bool CheckTenTaiSan(string ten, decimal? id = 0, decimal? donViId = 0);
        TaiSanListModel PrepareTaiSanchonListModel(TaiSanSearchModel searchModel);
        #endregion
        #region Tinh gia tri tai san
        TinhGiaTriTSModel TinhToanGiaTriTS(TinhGiaTriTSModel model);
        TinhGiaTriTSModel TinhHaoMon(TinhGiaTriTSModel model);
        TinhGiaTriTSModel TrichKhauHao(TinhGiaTriTSModel model);
        /// <summary>
        /// Kiểm tra loại hình tài sản này và với nguyên giá này ở đơn vị hiện tại có tự đông duyệt không
        /// </summary>
        /// <param name="LoaiHinhTaiSan">Loại hình của tài sản</param>
        /// <param name="NguyenGia">Nguyên giá của tài sản</param>
        /// <returns>true: tự động duyệt - false: duyệt tay</returns>
        bool CheckAutoDuyet(decimal? LoaiHinhTaiSan, decimal? NguyenGia);
        #endregion
        #region Bien dong tang giam nguyen gia
        /// <summary>
        /// Description: Tính giá trị tài sản tại thời điểm biến động tăng, giảm nguyên giá
        /// </summary>
        /// <returns></returns>
        TinhGiaTriTSModel TinhGiaTriBDNguyenGia(TinhGiaTriTSModel model);
        #endregion
        #region Duyet dang ky tai san

        #endregion
        /// <summary>
        /// Clone tất cả những gì liên quan đến tài sản sang đơn vị mới. Bao gồm biến động, yêu cầu, chi tiết tài sản.
        /// </summary>
        /// <param name="TaiSanID">Tài sản Id</param>
        /// <param name="bienDong">biến động giảm tài sản (Điều chuyển)</param>
        void DieuChuyenTaiSan(int TaiSanID, BienDongChiTiet bienDongChiTiet);
        void DieuChuyenMotPhan(YeuCau yeuCau, BienDongChiTiet yeuCauChiTiet);

        string LoadMaTaiSan(decimal? DonViId = 0, decimal? TaiSanId = 0, decimal? LoaiTaiSanId = 0, decimal? loaiHinhTaiSanId = 0);
        /// <summary>
        /// tính ra hao mòn kh của tài sản khi lưu
        /// </summary>
        /// <param name="yeuCauChiTiet">đầu ra</param>
        /// <param name="model">đầu vào</param>
        void PrepareSaveHMKHTaiSan(YeuCauChiTiet yeuCauChiTiet, TaiSanModel model);
       
        TaiSanListModel PrepareTaiSanKhaiThacListModel(TaiSanSearchModel searchModel);
        TaiSanSearchModel PrepareKiemTraTaiSanSearchModel(TaiSanSearchModel searchModel);
        TaiSanListModel PrepareKiemTraTaiSanListModel(TaiSanSearchModel searchModel);
        List<TaiSanExport> PrepareExportTaiSanKiemTra(TaiSanSearchModel searchModel);

        #region Cấu hình cảnh báo 
        Task<TaiSanListModel> PrepareDanhSachTaiSanThayDoiDiaBan(TaiSanSearchModel searchModel);
        Task<TaiSanListModel> PrepareDanhSachTaiSanSaiHienTrang(TaiSanSearchModel searchModel);
        Task<TaiSanListModel> PrepareDanhSachTaiSanSaiDuoi10Trieu(TaiSanSearchModel searchModel);
        Task<TaiSanListModel> PrepareDanhSachOtoSaiChoNgoi(TaiSanSearchModel searchModel);
        Task<TaiSanListModel> PrepareDanhSachTaiSanChuaTinhHaoMon(TaiSanSearchModel searchModel);
        Task<TaiSanListModel> PrepareDanhSachTaiSanNhanDieuChuyen(TaiSanSearchModel searchModel);

        #endregion

        #region Chuyển quyền xử lý tài sản
        TaiSanSearchModel PrepareQuyenXuLyTaiSanSearchModel(TaiSanSearchModel searchModel, bool isDuyetBienDong = false);
        #endregion
        #region KiemTraLogicSoLieuDauKy
        TaiSanListModel PrepareCCKTListTaiSan(BaoCaoTaiSanChiTietSearchModel searchModel);
        List<TaiSanExport> PrepareExportTaiSan(TaiSanSearchModel searchModel);
        #endregion KiemTraLogicSoLieuDauKy
        #region Kiểm tra logic tài sản
        bool CheckDonViSuNghiepKhongNhapXeChucDanh(TaiSanModel model);
        #endregion
    }
}
