//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 6/10/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.DMDC;

namespace GS.Services.DMDC
{
    public partial interface IDMDC_DuAnService 
    {    
    #region DMDC_DuAn
        IList<DMDC_DuAn> GetAllDMDC_DuAns();
        IPagedList <DMDC_DuAn> SearchDMDC_DuAns(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        DMDC_DuAn GetDMDC_DuAnById(decimal Id);
        IList<DMDC_DuAn> GetDMDC_DuAnByIds(decimal[] newsIds);
        void InsertDMDC_DuAn(DMDC_DuAn entity);
        void UpdateDMDC_DuAn(DMDC_DuAn entity);
        void DeleteDMDC_DuAn(DMDC_DuAn entity);    
     #endregion
	}
}
