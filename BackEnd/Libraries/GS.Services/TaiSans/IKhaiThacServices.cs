//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/7/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.TaiSans;

namespace GS.Services.TaiSans
{
    public partial interface IKhaiThacService
    {
        #region KhaiThac
        IList<KhaiThac> GetAllKhaiThacs();
        IPagedList <KhaiThac> SearchKhaiThacs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, string QUYET_DINH_SO = null, decimal LOAI_HINH_KHAI_THAC_ID = 0, DateTime? QUYET_DINH_NGAY = null, string HOP_DONG_SO = null, DateTime? HOP_DONG_NGAY = null, DateTime? KHAI_THAC_NGAY_TU = null, DateTime? KHAI_THAC_NGAY_DEN = null, decimal donviId = 0);
        IPagedList<KhaiThac> GetKhaiThacsKhacNgay(int pageIndex = 0, int pageSize = int.MaxValue, DateTime? KHAI_THAC_NGAY_TU = null, DateTime? KHAI_THAC_NGAY_DEN = null, decimal donviId = 0);
        KhaiThac GetKhaiThacById(decimal Id);
        IList<KhaiThac> GetKhaiThacByIds(decimal[] newsIds);
        void InsertKhaiThac(KhaiThac entity);
        void UpdateKhaiThac(KhaiThac entity);
        void DeleteKhaiThac(KhaiThac entity);
        KhaiThac GetKhaiThacByMa_DB(string ma_db);
        #endregion
    }
}
