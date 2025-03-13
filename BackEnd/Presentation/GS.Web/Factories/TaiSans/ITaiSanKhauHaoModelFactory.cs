//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 28/5/2021
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.TaiSans;
using GS.Web.Models.TaiSans;
using System;

namespace GS.Web.Factories.TaiSans
{
    public partial interface ITaiSanKhauHaoModelFactory 
    {    
        #region TaiSanKhauHao
    
        TaiSanKhauHaoSearchModel PrepareTaiSanKhauHaoSearchModel(TaiSanKhauHaoSearchModel searchModel);
        
        TaiSanKhauHaoListModel PrepareTaiSanKhauHaoListModel(TaiSanKhauHaoSearchModel searchModel);
        TaiSanModel PrepareListTaiSanKhauHaobyTaiSanModel(TaiSanModel taiSanModel, TaiSanKhauHaoModel taiSanKhauHaoModel);
        
        TaiSanKhauHaoModel PrepareTaiSanKhauHaoModel(TaiSanKhauHaoModel model, TaiSanKhauHao item, bool excludeProperties = false);
        
        void PrepareTaiSanKhauHao(TaiSanKhauHaoModel model, TaiSanKhauHao item);
        void insertOrUpdateTaiSanKhauHao( TaiSanKhauHao taiSanKhauHao, TaiSanKhauHaoModel taiSanKhauHaoModel);
        void CreateOrUpdateTaiSanKhauHao(TaiSanKhauHaoModel model);
        #endregion
    }
}
