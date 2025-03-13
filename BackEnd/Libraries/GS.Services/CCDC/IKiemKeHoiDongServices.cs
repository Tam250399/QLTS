//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.CCDC;

namespace GS.Services.CCDC
{
    public partial interface IKiemKeHoiDongService 
    {    
    #region KiemKeHoiDong
        IList<KiemKeHoiDong> GetAllKiemKeHoiDongs();
        IPagedList <KiemKeHoiDong> SearchKiemKeHoiDongs(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        IPagedList<KiemKeHoiDong> SearchKiemKeHoiDongsForKiemKe(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, int KiemKeId = 0);
        KiemKeHoiDong GetKiemKeHoiDongById(decimal Id);
        IList<KiemKeHoiDong> GetKiemKeHoiDongByIds(decimal[] newsIds);
        IList<KiemKeHoiDong> GetKiemKeHoiDongs(Decimal KiemKeId = 0);
        void InsertKiemKeHoiDong(KiemKeHoiDong entity);
        void UpdateKiemKeHoiDong(KiemKeHoiDong entity);
        void DeleteKiemKeHoiDong(KiemKeHoiDong entity);    
     #endregion
	}
}
