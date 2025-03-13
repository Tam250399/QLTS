//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 25/3/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.DanhMuc;
namespace GS.Web.Factories.DanhMuc
{
    public partial interface IDonViChuyenDoiModelFactory 
    {    
        #region DonViChuyenDoi
    
        DonViChuyenDoiSearchModel PrepareDonViChuyenDoiSearchModel(DonViChuyenDoiSearchModel searchModel);
        
        DonViChuyenDoiListModel PrepareDonViChuyenDoiListModel(DonViChuyenDoiSearchModel searchModel);
        
        DonViChuyenDoiModel PrepareDonViChuyenDoiModel(DonViChuyenDoiModel model, DonViChuyenDoi item, bool excludeProperties = false);
        
        void PrepareDonViChuyenDoi(DonViChuyenDoiModel model, DonViChuyenDoi item);
        
        #endregion        
	}
}
