//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using System;


namespace GS.Core.Domain.TaiSans
{
    public partial class TaiSanMayMoc : BaseEntity
    {
        public TaiSanMayMoc()
        {

        }
        public TaiSanMayMoc(TaiSanMayMoc obj)
        {
            this.TAI_SAN_ID = obj.TAI_SAN_ID;
            this.THONG_SO_KY_THUAT = obj.THONG_SO_KY_THUAT;
            this.THONG_SO_KY_HIEU = obj.THONG_SO_KY_HIEU;
            this.PHU_KIEN_JSON = obj.PHU_KIEN_JSON;
        }
        public String PHU_KIEN_JSON { get; set; }
        public Decimal TAI_SAN_ID { get; set; }
        public String THONG_SO_KY_THUAT { get; set; }
        public String THONG_SO_KY_HIEU { get; set; }
        public virtual TaiSan taisan { get; set; }
    }
}



