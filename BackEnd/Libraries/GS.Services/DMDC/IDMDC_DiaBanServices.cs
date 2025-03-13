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
    public partial interface IDMDC_DiaBanService 
    {    
    #region DMDC_DiaBan
        IList<DMDC_DiaBan> GetAllDMDC_DiaBans();
        IPagedList <DMDC_DiaBan> SearchDMDC_DiaBans(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        DMDC_DiaBan GetDMDC_DiaBanById(decimal Id);
        IList<DMDC_DiaBan> GetDMDC_DiaBanByIds(decimal[] newsIds);
        void InsertDMDC_DiaBan(DMDC_DiaBan entity);
        void UpdateDMDC_DiaBan(DMDC_DiaBan entity);
        void DeleteDMDC_DiaBan(DMDC_DiaBan entity);
        DMDC_DiaBan GetDiaBanByMa(string Ma);
     #endregion
	}
}
