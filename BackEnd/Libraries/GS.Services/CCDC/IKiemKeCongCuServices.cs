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
    public partial interface IKiemKeCongCuService 
    {    
    #region KiemKeCongCu
        IList<KiemKeCongCu> GetAllKiemKeCongCus();
        IPagedList <KiemKeCongCu> SearchKiemKeCongCus(bool isPhatHienThua, int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null, Decimal KiemKeId = 0);
        KiemKeCongCu GetKiemKeCongCuById(decimal Id);
        IList<KiemKeCongCu> GetKiemKeCongCuByIds(decimal[] newsIds);
        KiemKeCongCu GetKiemKeCongCu(Decimal KiemKeId, Decimal CongCuId, Decimal NhapXuatId);
        IList<KiemKeCongCu> GetKiemKeCongCus(Decimal? KiemKeId = 0, Decimal XuatNhapId = 0);
        void InsertKiemKeCongCu(KiemKeCongCu entity);
        void UpdateKiemKeCongCu(KiemKeCongCu entity);
        void DeleteKiemKeCongCu(KiemKeCongCu entity);    
     #endregion
	}
}
