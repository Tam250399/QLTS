//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.HeThong;
using System;
using System.Collections.Generic;

namespace GS.Services.HeThong
{
    public partial interface INhatKyService
    {
        #region NhatKy
        IList<NhatKy> GetAllNhatKys();
        IPagedList<NhatKy> SearchNhatKys(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, string Username = null, int CAP_DO = 0, DateTime? Fromdate = null, DateTime? Todate = null);
        NhatKy GetNhatKyById(decimal Id);
        void InsertNhatKy(NhatKy entity);
        void UpdateNhatKy(NhatKy entity);
        void DeleteNhatKy(NhatKy entity);
        #endregion
    }
}
