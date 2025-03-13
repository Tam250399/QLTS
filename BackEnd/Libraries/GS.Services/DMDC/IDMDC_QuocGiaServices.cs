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
    public partial interface IDMDC_QuocGiaService 
    {    
    #region DMDC_QuocGia
        IList<DMDC_QuocGia> GetAllDMDC_QuocGias();
        IPagedList <DMDC_QuocGia> SearchDMDC_QuocGias(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        DMDC_QuocGia GetDMDC_QuocGiaById(decimal Id);
        DMDC_QuocGia GetDMDC_QuocGiaByMa(string Ma);
        IList<DMDC_QuocGia> GetDMDC_QuocGiaByIds(decimal[] newsIds);
        void InsertDMDC_QuocGia(DMDC_QuocGia entity);
        void UpdateDMDC_QuocGia(DMDC_QuocGia entity);
        void DeleteDMDC_QuocGia(DMDC_QuocGia entity);    
     #endregion
	}
}
