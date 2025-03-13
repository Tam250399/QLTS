//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 5/3/2020
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
    public partial interface ITaiSanChoThueService 
    {    
    #region TaiSanChoThue
        IList<TaiSanChoThue> GetAllTaiSanChoThues();
        IPagedList <TaiSanChoThue> SearchTaiSanChoThues(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null, decimal donViId = 0);
        TaiSanChoThue GetTaiSanChoThueById(decimal Id);
        IList<TaiSanChoThue> GetTaiSanChoThueByIds(decimal[] newsIds);
        void InsertTaiSanChoThue(TaiSanChoThue entity);
        void UpdateTaiSanChoThue(TaiSanChoThue entity);
        void DeleteTaiSanChoThue(TaiSanChoThue entity);
        TaiSanChoThue GetTaiSanChoThueByTaiSanIdandNgaychoThue(decimal taiSanId, DateTime? NgayChoThue);
        bool CheckLoaiDonViToChuc(decimal LoaiDonViId);
     #endregion
    }
}
