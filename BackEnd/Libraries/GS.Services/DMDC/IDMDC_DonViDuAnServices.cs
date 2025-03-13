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
    public partial interface IDMDC_DonViDuAnService 
    {    
    #region DMDC_DonViDuAn
        IList<DMDC_DonViDuAn> GetAllDMDC_DonViDuAns();
        IPagedList <DMDC_DonViDuAn> SearchDMDC_DonViDuAns(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        DMDC_DonViDuAn GetDMDC_DonViDuAnById(decimal Id);
        IList<DMDC_DonViDuAn> GetDMDC_DonViDuAnByIds(decimal[] newsIds);
        void InsertDMDC_DonViDuAn(DMDC_DonViDuAn entity);
        void UpdateDMDC_DonViDuAn(DMDC_DonViDuAn entity);
        void DeleteDMDC_DonViDuAn(DMDC_DonViDuAn entity);    
     #endregion
	}
}
