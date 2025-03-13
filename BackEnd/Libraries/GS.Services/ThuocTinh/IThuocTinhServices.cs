//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.ThuocTinhs;

namespace GS.Services.ThuocTinhs
{
    public partial interface IThuocTinhService 
    {    
    #region ThuocTinh
        IList<ThuocTinh> GetAllThuocTinhs(string CauHinh = null);
        IPagedList <ThuocTinh> SearchThuocTinhs(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        ThuocTinh GetThuocTinhById(decimal Id);
        IList<ThuocTinh> GetThuocTinhByIds(decimal[] newsIds);
        void InsertThuocTinh(ThuocTinh entity);
        void UpdateThuocTinh(ThuocTinh entity);
        void DeleteThuocTinh(ThuocTinh entity);    
     #endregion
	}
}
