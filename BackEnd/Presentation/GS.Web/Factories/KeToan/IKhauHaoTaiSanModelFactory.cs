//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 5/6/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.Common;
using GS.Core.Domain.KT;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Web.Models.DongBoTaiSan;
using GS.Web.Models.ImportTaiSan;
using GS.Web.Models.KeToan;
using System.Collections.Generic;

namespace GS.Web.Factories.KeToan
{
    public partial interface IKhauHaoTaiSanModelFactory 
    {    
        #region KhauHaoTaiSan
    
        KhauHaoTaiSanSearchModel PrepareKhauHaoTaiSanSearchModel(KhauHaoTaiSanSearchModel searchModel);
        
        KhauHaoTaiSanListModel PrepareKhauHaoTaiSanListModel(KhauHaoTaiSanSearchModel searchModel);
        
        KhauHaoTaiSanModel PrepareKhauHaoTaiSanModel(KhauHaoTaiSanModel model, KhauHaoTaiSan item, bool excludeProperties = false);
        
        void PrepareKhauHaoTaiSan(KhauHaoTaiSanModel model, KhauHaoTaiSan item);
        MessageReturn PrepareInSertKhauHaoDongBo(KhauHaoDBModel model, int nguonId);
        void PrepareKhauHaoTaiSanFromHaoMonDB(KhauHaoDBModel model, KhauHaoTaiSan item);
        void InsertUpdateKhauHaoTaiSanModelFromTangMoiTS(YeuCau yeuCau, YeuCauChiTiet yeuCauChiTiet, KhauHaoTaiSanModel khauHaoTaiSanModel);
        KhauHaoTaiSanModel PrepareKhauHaoTaiSanModelFromYeuCau(YeuCau yeuCau, YeuCauChiTiet yeuCauChiTiet, KhauHaoTaiSanModel khauHaoTaiSanModel);
        KhauHaoTaiSan InsertToKhauHaoTaiSan(ImportKhTaiSanModel item, TaiSan taiSan);
        List<KhauHaoExport> PrepareKhauHaoTaiSanExport(KhauHaoTaiSanSearchModel searchModel);
        #endregion
    }
}
