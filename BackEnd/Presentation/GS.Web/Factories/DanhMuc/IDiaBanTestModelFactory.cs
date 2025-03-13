//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/3/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.DanhMuc;
namespace GS.Web.Factories.DanhMuc
{
    public partial interface IDiaBanTestModelFactory 
    {    
        #region DiaBanTest
    
        DiaBanTestSearchModel PrepareDiaBanTestSearchModel(DiaBanTestSearchModel searchModel);
        
        DiaBanTestListModel PrepareDiaBanTestListModel(DiaBanTestSearchModel searchModel);
        
        DiaBanTestModel PrepareDiaBanTestModel(DiaBanTestModel model, DiaBanTest item, bool excludeProperties = false);
        
        void PrepareDiaBanTest(DiaBanTestModel model, DiaBanTest item);

        bool CheckMaDiaBanTest(decimal id, string Ma);
        
        #endregion        
	}
}
