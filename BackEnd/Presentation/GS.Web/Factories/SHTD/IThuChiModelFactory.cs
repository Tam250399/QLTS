//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 7/12/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.SHTD;
using GS.Web.Models.SHTD;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GS.Web.Factories.SHTD
{
    public partial interface IThuChiModelFactory 
    {
        #region ThuChi
        string PrepareDanhSachQuyetDinhXuLy(string ListXuLyID);
        void CheckValidNgayThuDauTien(ModelStateDictionary ModelState, ThuChiModel model);
        void UpdateListXuLyIdWhenCreate(string ListThuChiFirst, string ListThuChi);
        ThuChiSearchModel PrepareThuChiSearchModel(ThuChiSearchModel searchModel);
        ThuChiModel PrepareThuChiModelForCreateOrUpdate(string ListXuLyIdString, decimal Id);
        void TinhSoTienThuChiTiepTheo(ThuChiModel model);
        ThuChiListModel PrepareThuChiListModel(ThuChiSearchModel searchModel);
        
        ThuChiModel PrepareThuChiModel(ThuChiModel model, ThuChi item, bool excludeProperties = false);
        ThuChiListModel PrepareThuChiKetQuaListModel(ThuChiSearchModel searchModel);


        void PrepareThuChi(ThuChiModel model, ThuChi item);
        bool CheckValidChiPhiDauTien(ThuChiModel model);

        #endregion
    }
}
