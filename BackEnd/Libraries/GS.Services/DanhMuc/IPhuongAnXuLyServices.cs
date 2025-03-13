//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.DanhMuc;

namespace GS.Services.DanhMuc
{
    public partial interface IPhuongAnXuLyService 
    {    
    #region PhuongAnXuLy
        IList<PhuongAnXuLy> GetAllPhuongAnXuLys();
        IList<PhuongAnXuLy> GetAllPhuongAnXuLysChuaDb();
        IPagedList <PhuongAnXuLy> SearchPhuongAnXuLys(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        PhuongAnXuLy GetPhuongAnXuLyById(decimal Id);
        IList<PhuongAnXuLy> GetPhuongAnXuLyByIds(decimal[] newsIds);
        void InsertPhuongAnXuLy(PhuongAnXuLy entity);
        void UpdatePhuongAnXuLy(PhuongAnXuLy entity);
        void DeletePhuongAnXuLy(PhuongAnXuLy entity);
        IList<PhuongAnXuLy> GetPhuongAnXuLys(string Ma = null);
        PhuongAnXuLy GetPhuongAnXuLyByMa(string Ma = null);
        void UpdateListPhuongAnXuLy(List<PhuongAnXuLy> entities);
        void InsertListPhuongAnXuLy(List<PhuongAnXuLy> entities);
     #endregion
    }
}
