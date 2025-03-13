//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.CCDC;
using GS.Web.Models.CCDC;
namespace GS.Web.Factories.CCDC
{
    public partial interface INhapXuatCongCuModelFactory 
    {    
        #region NhapXuatCongCu
    
        NhapXuatCongCuSearchModel PrepareNhapXuatCongCuSearchModel(NhapXuatCongCuSearchModel searchModel);
        
        NhapXuatCongCuListModel PrepareNhapXuatCongCuListModel(NhapXuatCongCuSearchModel searchModel);
        
        NhapXuatCongCuModel PrepareNhapXuatCongCuModel(NhapXuatCongCuModel model, NhapXuatCongCu item, bool excludeProperties = false);
        
        void PrepareNhapXuatCongCu(NhapXuatCongCuModel model, NhapXuatCongCu item);

        NhapXuatCongCuListModel PrepareForPhanBo(CongCuSearchModel searchModel);

        #endregion
    }
}
