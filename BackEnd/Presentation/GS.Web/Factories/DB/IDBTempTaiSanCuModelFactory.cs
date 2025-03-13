//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2021
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DB;
using GS.Web.Models.DB;
namespace GS.Web.Factories.DB
{
    public partial interface IDBTempTaiSanCuModelFactory 
    {    
        #region DBTempTaiSanCu
    
        DBTempTaiSanCuSearchModel PrepareDBTempTaiSanCuSearchModel(DBTempTaiSanCuSearchModel searchModel);
        
        DBTempTaiSanCuListModel PrepareDBTempTaiSanCuListModel(DBTempTaiSanCuSearchModel searchModel);
        
        DBTempTaiSanCuModel PrepareDBTempTaiSanCuModel(DBTempTaiSanCuModel model, DBTempTaiSanCu item, bool excludeProperties = false);
        
        void PrepareDBTempTaiSanCu(DBTempTaiSanCuModel model, DBTempTaiSanCu item);
        
        #endregion        
	}
}
