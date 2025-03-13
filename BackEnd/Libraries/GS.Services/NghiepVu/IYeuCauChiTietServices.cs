//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.NghiepVu;
using System.Collections.Generic;

namespace GS.Services.NghiepVu
{
    public partial interface IYeuCauChiTietService
    {
        #region YeuCauChiTiet
        IList<YeuCauChiTiet> GetAllYeuCauChiTiets();
        IPagedList<YeuCauChiTiet> SearchYeuCauChiTiets(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        YeuCauChiTiet GetYeuCauChiTietById(decimal Id);
        IList<YeuCauChiTiet> GetYeuCauChiTietByIds(decimal[] newsIds);
        void InsertYeuCauChiTiet(YeuCauChiTiet entity);
        void UpdateYeuCauChiTiet(YeuCauChiTiet entity);
        void DeleteYeuCauChiTiet(YeuCauChiTiet entity);
        YeuCauChiTiet GetYeuCauChiTietByYeuCauId(decimal YeuCauId);
        IList<YeuCauChiTiet> GetYeuCauChiTietByYeuCauIds(IList<decimal> YeuCauIds);
        #endregion
    }
}
