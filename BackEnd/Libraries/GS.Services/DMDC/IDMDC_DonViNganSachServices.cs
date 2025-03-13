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
    public partial interface IDMDC_DonViNganSachService 
    {    
    #region DMDC_DonViNganSach
        IList<DMDC_DonViNganSach> GetAllDMDC_DonViNganSachs();
        IPagedList <DMDC_DonViNganSach> SearchDMDC_DonViNganSachs(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        DMDC_DonViNganSach GetDMDC_DonViNganSachById(decimal Id);
        DMDC_DonViNganSach GetDMDC_DonViNganSachByMa(string ma);
        IList<DMDC_DonViNganSach> GetDMDC_DonViNganSachByIds(decimal[] newsIds);
        void InsertDMDC_DonViNganSach(DMDC_DonViNganSach entity);
        void UpdateDMDC_DonViNganSach(DMDC_DonViNganSach entity);
        void DeleteDMDC_DonViNganSach(DMDC_DonViNganSach entity);    
     #endregion
	}
}
