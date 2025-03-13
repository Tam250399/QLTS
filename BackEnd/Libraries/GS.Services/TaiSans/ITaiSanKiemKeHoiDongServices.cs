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
    public partial interface ITaiSanKiemKeHoiDongService 
    {    
    #region TaiSanKiemKeHoiDong
        IList<TaiSanKiemKeHoiDong> GetAllTaiSanKiemKeHoiDongs();
        IPagedList <TaiSanKiemKeHoiDong> SearchTaiSanKiemKeHoiDongs(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null, int KiemKeId = 0);
        TaiSanKiemKeHoiDong GetTaiSanKiemKeHoiDongById(decimal Id);
        IList<TaiSanKiemKeHoiDong> GetTaiSanKiemKeHoiDongByIds(decimal[] newsIds);
        void InsertTaiSanKiemKeHoiDong(TaiSanKiemKeHoiDong entity);
        void UpdateTaiSanKiemKeHoiDong(TaiSanKiemKeHoiDong entity);
        void DeleteTaiSanKiemKeHoiDong(TaiSanKiemKeHoiDong entity);
        IList<TaiSanKiemKeHoiDong> GetTaiSanKiemKeHoiDongs(decimal KiemKeId = 0);
     #endregion
    }
}
