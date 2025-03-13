//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.NghiepVu;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.NghiepVu;
using GS.Web.Models.TaiSans;
using System;

namespace GS.Web.Factories.NghiepVu
{
    public partial interface IYeuCauChiTietModelFactory
    {
        #region YeuCauChiTiet
        YeuCauChiTietSearchModel PrepareYeuCauChiTietSearchModel(YeuCauChiTietSearchModel searchModel);
        /// <summary>
        /// Description: Prepare entity yeu cau chi tiet tu entity bien dong chi tiet
        /// </summary>
        /// <param name="_ycct">entity yeu cau chi tiet</param>
        /// <param name="_bdct">entity bien dong chi tiet</param>
        /// <returns></returns>
        YeuCauChiTiet PrepareYCCTFromBDCT(BienDongChiTiet _bdct, YeuCauChiTiet _ycct, bool isCopy = false);
        YeuCauChiTietListModel PrepareYeuCauChiTietListModel(YeuCauChiTietSearchModel searchModel);

        YeuCauChiTietModel PrepareYeuCauChiTietModel(YeuCauChiTietModel model, YeuCauChiTiet item, bool excludeProperties = false);

        YeuCauChiTiet PrepareYeuCauChiTiet(YeuCauChiTiet item, TaiSanModel taisanModel);
		/// <summary>
		/// Description: Lấy thông tin giá trị tài sản trước biến động
		/// </summary>
		/// <param name="biendongtruoc">yêu cầu chi tiết biến động trước </param>
		/// <param name="model">Yêu cầu chi tiết hiện tại</param>
		/// <returns></returns>
		YeuCauModel PrepareYeuCauChiTietTruocBD(YeuCauModel model);
        YeuCauChiTietModel PrepareYeuCauChiTietTruocBDByYCTT(YeuCauChiTiet YCTTtruoc, YeuCauChiTietModel model);
        /// <summary>
        /// Description: Lấy thông tin của loại tài sản.
        /// </summary>
        /// <param name="loaiTaiSan">loại tài sản model</param>
        /// <param name="nvYeuCauChiTiet"></param>
        /// <returns></returns>
        YeuCauChiTietModel PrepareValueFromLoaiTStoYeuCauCT(LoaiTaiSanModel loaiTaiSan, YeuCauChiTietModel nvYeuCauChiTiet);

        /// <summary>
        /// Description: Prepare Yeu cau chi tiet khi edit bien dong
        /// </summary>
        /// <param name="model"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        YeuCauChiTiet PrepareYCCTForBDEdit(YeuCauChiTietModel model, YeuCauChiTiet item);
        
        #endregion

        #region GiamTaiSan
        YeuCauChiTiet PrepareYeuCauChiTietGiamTaiSan(YeuCauChiTietModel model, YeuCauChiTiet item, YeuCau yeucau);
        #endregion
        #region Func
        /// <summary>
        /// Tính giá trị còn lại theo theo tỉ lệ điều chuyển một phần
        /// </summary>
        /// <param name="TaiSanId"></param>
        /// <param name="NgayBienDong"></param>
        /// <param name="OLD_GTCL"></param>
        /// <param name="NguyenGiaDieuChuyen"></param>
        /// <returns></returns>
        decimal CalculateGTCLDieuChuyenMotPhan(decimal TaiSanId, DateTime? NgayBienDong, decimal OLD_GTCL, decimal NguyenGiaDieuChuyen);
        #endregion
    }
}
