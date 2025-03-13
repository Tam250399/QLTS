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
    public partial interface IDMDC_ToChucNganSachService 
    {    
    #region DMDC_ToChucNganSach
        IList<DMDC_ToChucNganSach> GetAllDMDC_ToChucNganSachs();
        IPagedList <DMDC_ToChucNganSach> SearchDMDC_ToChucNganSachs(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        DMDC_ToChucNganSach GetDMDC_ToChucNganSachById(decimal Id);
        IList<DMDC_ToChucNganSach> GetDMDC_ToChucNganSachByIds(decimal[] newsIds);
        void InsertDMDC_ToChucNganSach(DMDC_ToChucNganSach entity);
        void UpdateDMDC_ToChucNganSach(DMDC_ToChucNganSach entity);
        void DeleteDMDC_ToChucNganSach(DMDC_ToChucNganSach entity);    
     #endregion
	}
}
