//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/2/2020
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
    public partial interface ISuaChuaBaoDuongService 
    {    
    #region SuaChuaBaoDuong
        IList<SuaChuaBaoDuong> GetAllSuaChuaBaoDuongs();
        IList<SuaChuaBaoDuong> GetSuaChuaBaoDuongs(decimal XuatNhapId = 0);
        IPagedList <SuaChuaBaoDuong> SearchSuaChuaBaoDuongs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, Decimal CongCuId = 0, Decimal BoPhanId = 0, DateTime? NgayDen = null, DateTime? NgayTu = null, decimal? donViId =0);
        SuaChuaBaoDuong GetSuaChuaBaoDuongById(decimal Id);
        IList<SuaChuaBaoDuong> GetSuaChuaBaoDuongByIds(decimal[] newsIds);
        void InsertSuaChuaBaoDuong(SuaChuaBaoDuong entity);
        void UpdateSuaChuaBaoDuong(SuaChuaBaoDuong entity);
        void DeleteSuaChuaBaoDuong(SuaChuaBaoDuong entity);    
     #endregion
	}
}
