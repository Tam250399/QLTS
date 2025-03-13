//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 18/5/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace GS.Core.Domain.Api
{
    public class QuyetDinhTichThuApi : BaseEntity
    {
        public QuyetDinhTichThuApi()
        {
            ListTaiSanToanDanModels = new List<TaiSanToanDanApi>();
        }
        public String QUYET_DINH_SO { get; set; }
        public DateTime? QUYET_DINH_NGAY { get; set; }
        public String TEN { get; set; }
        public String GHI_CHU { get; set; }
        public String MA_DON_VI { get; set; }
        public IList<TaiSanToanDanApi> ListTaiSanToanDanModels { get; set; }
    }
}
