//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/6/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.DanhMuc;
namespace GS.Web.Factories.DanhMuc
{
    public partial interface ILoaiTaiSanKhauHaoModelFactory 
    {    
        #region LoaiTaiSanKhauHao
    
        LoaiTaiSanKhauHaoSearchModel PrepareLoaiTaiSanKhauHaoSearchModel(LoaiTaiSanKhauHaoSearchModel searchModel);
        
        //LoaiTaiSanKhauHaoListModel PrepareLoaiTaiSanKhauHaoListModel(LoaiTaiSanKhauHaoSearchModel searchModel);
        LoaiTaiSanListModel PrepareLoaiTaiSanKhauHaoListModel(LoaiTaiSanKhauHaoSearchModel searchModel);
        
        LoaiTaiSanKhauHaoModel PrepareLoaiTaiSanKhauHaoModel(LoaiTaiSanKhauHaoModel model, LoaiTaiSanKhauHao item, bool excludeProperties = false);
        
        void PrepareLoaiTaiSanKhauHao(LoaiTaiSanKhauHaoModel model, LoaiTaiSanKhauHao item);
        
        #endregion        
	}
}
