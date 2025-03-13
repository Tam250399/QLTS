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
    public partial interface IDinhMucService 
    {    
    #region DinhMuc
        IList<DinhMuc> GetAllDinhMucs();
        //IPagedList <DinhMuc> SearchDinhMucs(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        IPagedList<DinhMuc> SearchDinhMucs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null,
            decimal? donViID = 0, DateTime? TuNgay = null, DateTime? DenNgay = null, string SoQuyetDinh = null, DateTime? NgayQuyetDinh = null);
        DinhMuc GetDinhMucById(decimal Id);
        IList<DinhMuc> GetDinhMucByIds(decimal[] newsIds);
        IList<DinhMuc> GetListDinhMucByDonViIds(decimal? donviId = 0);
        void InsertDinhMuc(DinhMuc entity);
        void UpdateDinhMuc(DinhMuc entity);
        void DeleteDinhMuc(DinhMuc entity);
        DinhMuc CheckMaDinhMuc(string Ma);
        DinhMuc CheckSoQuyetDinhDinhMuc(string Ma);
        void GenMa(DinhMuc entity);
     #endregion
    }
}
