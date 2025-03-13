//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using System;


namespace GS.Core.Domain.TaiSans
{
    public partial class TaiSanCln : BaseEntity
    {
        public TaiSanCln()
        {

        }
        public TaiSanCln(TaiSanCln obj)
        {
            this.TAI_SAN_ID = obj.TAI_SAN_ID;
            this.NAM_SINH = obj.NAM_SINH;
        }
        public Decimal TAI_SAN_ID { get; set; }
        public Decimal? NAM_SINH { get; set; }
        public virtual TaiSan taisan { get; set; }

    }
}



