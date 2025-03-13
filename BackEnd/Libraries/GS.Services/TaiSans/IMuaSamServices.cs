//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 21/7/2020
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
    public partial interface IMuaSamService 
    {    
    #region MuaSam
        IList<MuaSam> GetAllMuaSams();
        IPagedList <MuaSam> SearchMuaSams(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, DateTime? Tu_ngay = null, DateTime? Den_ngay = null, decimal? trangThaiId = 0, Decimal? donViSD_ID = 0, Decimal? donViQL_ID = 0, Decimal? LoaiTaiSanId = 0, decimal? LoaiHinhTaiSanId = 0);
        MuaSam GetMuaSamById(decimal Id);
        IList<MuaSam> GetMuaSamByIds(decimal[] newsIds);
        void InsertMuaSam(MuaSam entity);
        void UpdateMuaSam(MuaSam entity);
        void DeleteMuaSam(MuaSam entity);    
     #endregion
	}
}
