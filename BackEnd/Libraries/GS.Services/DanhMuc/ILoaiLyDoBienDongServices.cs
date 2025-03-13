//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 3/10/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.DM;
using System.Linq;

namespace GS.Services.DM
{
    public partial interface ILoaiLyDoBienDongService
    {
        #region LoaiLyDoBienDong
        IList<LoaiLyDoBienDong> GetAllLoaiLyDoBienDongs();
        IList<LoaiLyDoBienDong> GetAllLoaiLyDoBienDongsChuaDb();
        IPagedList<LoaiLyDoBienDong> SearchLoaiLyDoBienDongs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        LoaiLyDoBienDong GetLoaiLyDoBienDongById(decimal Id);
        LoaiLyDoBienDong GetLoaiLyDoBienDongByMa(string ma);

        IList<LoaiLyDoBienDong> GetLoaiLyDoBienDongByIds(decimal[] newsIds);
        void InsertLoaiLyDoBienDong(LoaiLyDoBienDong entity);
        void UpdateLoaiLyDoBienDong(LoaiLyDoBienDong entity);
        void DeleteLoaiLyDoBienDong(LoaiLyDoBienDong entity);
        IQueryable<LoaiLyDoBienDong> GetIQueryableLoaiLyDoBienDongByParent(decimal ParentId, string ParentTreeNode = "");
        #endregion
    }
}
