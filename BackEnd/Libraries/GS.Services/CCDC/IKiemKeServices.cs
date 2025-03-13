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
using GS.Core.Domain.Common;

namespace GS.Services.CCDC
{
    public partial interface IKiemKeService
    {
        #region KiemKe
        IList<KiemKe> GetAllKiemKes();
        IPagedList<KiemKe> SearchKiemKes(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, Decimal DonViId = 0, DateTime? TuNgay = null, DateTime? DenNgay = null, Decimal BoPhanId = 0);
        KiemKe GetKiemKeById(decimal Id);
        IList<KiemKe> GetKiemKeByIds(decimal[] newsIds);
        KiemKe GetKiemKeIdMax(decimal DonViId);
        void InsertKiemKe(KiemKe entity);
        void UpdateKiemKe(KiemKe entity);
        void DeleteKiemKe(KiemKe entity);
        #endregion
        MessageReturn DelelteKiemKeCCDCByStore(decimal KIEMKE_ID);

    }
}
