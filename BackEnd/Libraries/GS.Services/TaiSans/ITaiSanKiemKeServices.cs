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
using GS.Core.Domain.TaiSans;

namespace GS.Services.TaiSans
{
    public partial interface ITaiSanKiemKeService 
    {    
    #region TaiSanKiemKe
        IList<TaiSanKiemKe> GetAllTaiSanKiemKes();
        IPagedList <TaiSanKiemKe> SearchTaiSanKiemKes(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null, decimal DonViId = 0, DateTime? NgayKiemKeTu = null, DateTime? NgayKiemKeDen = null);
        TaiSanKiemKe GetTaiSanKiemKeById(decimal Id);
        IList<TaiSanKiemKe> GetTaiSanKiemKeByIds(decimal[] newsIds);
        TaiSanKiemKe GetMaxValueId();
        void InsertTaiSanKiemKe(TaiSanKiemKe entity);
        void UpdateTaiSanKiemKe(TaiSanKiemKe entity);
        void DeleteTaiSanKiemKe(TaiSanKiemKe entity);    
     #endregion
	}
}
