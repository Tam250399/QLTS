//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
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
    public partial interface IChoThueService 
    {    
    #region ChoThue
        IList<ChoThue> GetAllChoThues();
        IPagedList <ChoThue> SearchChoThues(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, Decimal CongCuId = 0, Decimal DonViBoPhanId = 0, DateTime? NgayChoThueTu = null, DateTime? NgayChoThueDen = null, Decimal DonViId = 0);
        ChoThue GetChoThueById(decimal Id);
        IList<ChoThue> GetChoThueByIds(decimal[] newsIds);
        void InsertChoThue(ChoThue entity);
        void UpdateChoThue(ChoThue entity);
        void DeleteChoThue(ChoThue entity);
        IList<ChoThue> GetChoThues(decimal XuatNhapId = 0);
     #endregion
    }
}
