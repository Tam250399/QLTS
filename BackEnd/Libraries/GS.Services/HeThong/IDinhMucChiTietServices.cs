//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 18/6/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.HeThong;

namespace GS.Services.HeThong
{
    public partial interface IDinhMucChiTietService 
    {    
    #region DinhMucChiTiet
        IList<DinhMucChiTiet> GetAllDinhMucChiTiets();
        IPagedList <DinhMucChiTiet> SearchDinhMucChiTiets(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        DinhMucChiTiet GetDinhMucChiTietById(decimal Id);
        IList<DinhMucChiTiet> GetDinhMucChiTietByIds(decimal[] newsIds);
        IList<DinhMucChiTiet> GetDinhMucChiTietByDinhMucId(decimal? Id);
        DinhMucChiTiet GetDinhMucChiTietByDinhMucIdChucDanhIdAndLoaiTaiSanId(decimal DinhMucId, decimal ChucDanhId, decimal LoaiTaiSanId);
        void InsertDinhMucChiTiet(DinhMucChiTiet entity);
        void UpdateDinhMucChiTiet(DinhMucChiTiet entity);
        void DeleteDinhMucChiTiet(DinhMucChiTiet entity);    
     #endregion
	}
}
