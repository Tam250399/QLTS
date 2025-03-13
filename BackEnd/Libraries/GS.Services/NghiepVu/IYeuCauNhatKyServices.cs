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
    public partial interface IYeuCauNhatKyService
    {
        #region YeuCauNhatKy
        IList<YeuCauNhatKy> GetAllYeuCauNhatKys();
        IPagedList<YeuCauNhatKy> SearchYeuCauNhatKys(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        YeuCauNhatKy GetYeuCauNhatKyById(decimal Id);
        IList<YeuCauNhatKy> GetYeuCauNhatKyByIds(decimal[] newsIds);
        void InsertYeuCauNhatKy(YeuCauNhatKy entity);
        void UpdateYeuCauNhatKy(YeuCauNhatKy entity);
        void DeleteYeuCauNhatKy(YeuCauNhatKy entity);
        #endregion
    }
}
