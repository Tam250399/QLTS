//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.CCDC;
using GS.Web.Models.CCDC;
using System;

namespace GS.Web.Factories.CCDC
{
    public partial interface IChoThueModelFactory
    {    
        #region ChoThue
    
        ChoThueSearchModel PrepareChoThueSearchModel(ChoThueSearchModel searchModel);
        
        ChoThueListModel PrepareChoThueListModel(ChoThueSearchModel searchModel);
        
        void PrepareChoThue(ChoThueModel model, ChoThue item);

        ChoThueListModel PrepareChoThueModelForChonCongCu(ChoThueSearchModel searchModel);

        ChoThueModel PrepareChoThueModel(ChoThueModel model, Decimal MapId);

        #endregion
    }
}
