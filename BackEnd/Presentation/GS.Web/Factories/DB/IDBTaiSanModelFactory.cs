//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/10/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DB;
using GS.Web.Models.DB;
namespace GS.Web.Factories.DB
{
    public partial interface IDBTaiSanModelFactory 
    {    
        #region DBTaiSan
    
        DBTaiSanSearchModel PrepareDBTaiSanSearchModel(DBTaiSanSearchModel searchModel);
        
        DBTaiSanListModel PrepareDBTaiSanListModel(DBTaiSanSearchModel searchModel);
        
        DBTaiSanModel PrepareDBTaiSanModel(DBTaiSanModel model, DBTaiSan item, bool excludeProperties = false);
        
        void PrepareDBTaiSan(DBTaiSanModel model, DBTaiSan item);
        
        #endregion        
	}
}
