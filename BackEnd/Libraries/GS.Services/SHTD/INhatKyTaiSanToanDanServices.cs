//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 7/12/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.SHTD;

namespace GS.Services.SHTD
{
    public partial interface INhatKyTaiSanToanDanService 
    {    
    #region NhatKyTaiSanToanDan
        IList<NhatKyTaiSanToanDan> GetAllNhatKyTaiSanToanDans();
        IPagedList <NhatKyTaiSanToanDan> SearchNhatKyTaiSanToanDans(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        NhatKyTaiSanToanDan GetNhatKyTaiSanToanDanById(decimal Id);
        IList<NhatKyTaiSanToanDan> GetNhatKyTaiSanToanDanByIds(decimal[] newsIds);
        void InsertNhatKyTaiSanToanDan(NhatKyTaiSanToanDan entity);
        void UpdateNhatKyTaiSanToanDan(NhatKyTaiSanToanDan entity);
        void DeleteNhatKyTaiSanToanDan(NhatKyTaiSanToanDan entity);    
     #endregion
	}
}
