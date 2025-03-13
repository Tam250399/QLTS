//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.HeThong;
using GS.Web.Models.HeThong;

namespace GS.Web.Factories.HeThong
{
    public partial interface INguoiDungModelFactory
    {
        #region NguoiDung
        ResultAction DuyetTaiKhoan(decimal Id, NguoiDung nguoiDung = null);
        ResultAction HuyDuyetTaiKhoan(decimal Id, NguoiDung nguoiDung = null);
        NguoiDungSearchModel PrepareNguoiDungSearchModel(NguoiDungSearchModel searchModel);
        NguoiDungSearchModel PrepareNguoiDungSearchModelDuToanCap2(NguoiDungSearchModel searchModel);

        NguoiDungListModel PrepareNguoiDungListModel(NguoiDungSearchModel searchModel);
        NguoiDungListModel PrepareDuyetNguoiDungListModel(NguoiDungSearchModel searchModel);

        NguoiDungModel PrepareNguoiDungModel(NguoiDungModel model, NguoiDung item, bool excludeProperties = false);
        NguoiDungModel PrepareNguoiDungModelDuToanCap2(NguoiDungModel model, NguoiDung item, bool excludeProperties = false);

        void PrepareNguoiDung(NguoiDungModel model, NguoiDung item);
        bool CheckTenDangNhap(string TEN_DANG_NHAP, decimal id = 0);
        bool CheckMaCanbo(string MA_CAN_BO, decimal id = 0);
        bool CheckResetPassword(string MAT_KHAU_CU, bool isResetmk = true);
        #endregion
    }
}
