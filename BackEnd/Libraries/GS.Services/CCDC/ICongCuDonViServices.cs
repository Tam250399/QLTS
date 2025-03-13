//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.CCDC;

namespace GS.Services.CCDC
{
    public partial interface ICongCuDonViService 
    {    
    #region CongCuDonVi
        IList<CongCuDonVi> GetAllCongCuDonVis();
        IPagedList <CongCuDonVi> SearchCongCuDonVis(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        CongCuDonVi GetCongCuDonViById(decimal Id);
        IList<CongCuDonVi> GetCongCuDonViByIds(decimal[] newsIds);
        void InsertCongCuDonVi(CongCuDonVi entity);
        void UpdateCongCuDonVi(CongCuDonVi entity);
        void DeleteCongCuDonVi(CongCuDonVi entity);    
     #endregion
	}
}
