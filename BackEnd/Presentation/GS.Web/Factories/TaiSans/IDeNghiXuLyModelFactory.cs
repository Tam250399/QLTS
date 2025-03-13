//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/11/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.TaiSans;
using GS.Web.Models.TaiSans;
using System;

namespace GS.Web.Factories.TaiSans
{
    public partial interface IDeNghiXuLyModelFactory 
    {    
        #region DeNghiXuLy
    
        DeNghiXuLySearchModel PrepareDeNghiXuLySearchModel(DeNghiXuLySearchModel searchModel);
        
        DeNghiXuLyListModel PrepareDeNghiXuLyListModel(DeNghiXuLySearchModel searchModel);
        
        DeNghiXuLyModel PrepareDeNghiXuLyModel(DeNghiXuLyModel model, DeNghiXuLy item, bool excludeProperties = false);
        
        void PrepareDeNghiXuLy(DeNghiXuLyModel model, DeNghiXuLy item);
        bool CheckTrungSoPhieuTrongNgay(Decimal ID,String soPhieu, DateTime ngay, Decimal donViId);

        #endregion
    }
}
