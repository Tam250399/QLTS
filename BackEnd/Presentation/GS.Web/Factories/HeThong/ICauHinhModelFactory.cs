//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 27/9/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.HeThong;
using GS.Web.Models.HeThong;
namespace GS.Web.Factories.HeThong
{
    public partial interface ICauHinhModelFactory
    {
        #region CauHinh

        CauHinhSearchModel PrepareCauHinhSearchModel(CauHinhSearchModel searchModel);

        CauHinhListModel PrepareCauHinhListModel(CauHinhSearchModel searchModel);

        CauHinhModel PrepareCauHinhModel(CauHinhModel model, CauHinh item, bool excludeProperties = false);
        CauHinhChungModel PrepareCauHinhChungModel();

        void PrepareCauHinh(CauHinhModel model, CauHinh item);
        XacThucChungThuSomodel PrepareXacThucChungThuSomodel();
        ThongTinKetXuatDuLieuModel PrepareLichKetXuatDuLieuModel(ThongTinKetXuatDuLieuModel model);
        CauHinhSoTaiSanModel PrepareListCauHinhSoTaiSan(CauHinhSoTaiSanModel model, decimal DonViId = 0);
		TrangThaiNamModel PrepareTrangThaiNamTaiSanModel(TrangThaiNamModel model, decimal Year, decimal DonViId = 0);
		TrangThaiNam PrepareTrangThaiNamTaiSan(TrangThaiNamModel model, TrangThaiNam item);
        CauHinhTuDongDuyetModel PrepareCauHinhTuDongDuyetModel(CauHinhTuDongDuyetModel model);
        ThongTinKetXuatDuLieuModel PrepareLichKetXuatDuLieu(ThongTinKetXuatDuLieuModel model);

        DinhMucChucDanhSearchModel PrepareDinhMucChucDanhSearchModel(DinhMucChucDanhSearchModel searchModel);
        DinhMucChucDanhListModel PrepareDinhMucChucDanhListModel(DinhMucChucDanhSearchModel searchModel);
        DinhMucChucDanhModel PrepareDinhMucChucDanhModel(DinhMucChucDanhModel model);
        CauHinhThreadBaoCaoModel PrepareCauHinhThreadBaoCaoModel();
		/// <summary>
		/// Kiểm tra đã khóa sổ tài sản hay chưa
		/// </summary>
		/// <param name="Year">Năm truyền vào</param>
		/// <returns>true: đã khóa, false: chưa khóa</returns>
        bool CheckYearIsLockSoTaiSan(decimal Year);
		TrangThaiNam GetSoTaiSanByYear(decimal Year);
        void PrePareModelBySession(object model, object session);
        #endregion
    }
}
