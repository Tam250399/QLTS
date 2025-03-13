//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/2/2020
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
    public partial interface IGiamHongmatService 
    {    
    #region GiamHongmat
        IList<GiamHongmat> GetAllGiamHongmats();
        IPagedList <GiamHongmat> SearchGiamHongmats(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        GiamHongmat GetGiamHongmatByNhapXuatIdAndCongCuId(decimal NhapXuatId = 0, decimal CongCuId = 0);
        GiamHongmat GetGiamHongmatById(decimal Id);
        IList<GiamHongmat> GetGiamHongmatByIds(decimal[] newsIds);
        void InsertGiamHongmat(GiamHongmat entity);
        void UpdateGiamHongmat(GiamHongmat entity);
        void DeleteGiamHongmat(GiamHongmat entity);    
     #endregion
	}
}
