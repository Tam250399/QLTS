//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 5/3/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.TaiSans;
using GS.Web.Models.TaiSans;
using System;

namespace GS.Web.Factories.TaiSans
{
    public partial interface ITaiSanChoThueModelFactory 
    {    
        #region TaiSanChoThue
    
        TaiSanChoThueSearchModel PrepareTaiSanChoThueSearchModel(TaiSanChoThueSearchModel searchModel);
        
        TaiSanChoThueListModel PrepareTaiSanChoThueListModel(TaiSanChoThueSearchModel searchModel);
        
        TaiSanChoThueModel PrepareTaiSanChoThueModel(TaiSanChoThueModel model, TaiSanChoThue item, bool excludeProperties = false);
        
        void PrepareTaiSanChoThue(TaiSanChoThueModel model, TaiSanChoThue item);
        bool CheckTimeChoThue(decimal taiSanId, DateTime? NgayChoThue, decimal tsChoThueId);

        #endregion
    }
}
