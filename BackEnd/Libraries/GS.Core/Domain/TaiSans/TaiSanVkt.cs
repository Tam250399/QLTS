//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using System;


namespace GS.Core.Domain.TaiSans
{
    public partial class TaiSanVkt : BaseEntity
    {
        public TaiSanVkt()
        {

        }
        public TaiSanVkt(TaiSanVkt obj)
        {
            this.TAI_SAN_ID = obj.TAI_SAN_ID;
            this.DIEN_TICH = obj.DIEN_TICH;
            this.THE_TICH = obj.THE_TICH;
            this.CHIEU_DAI = obj.CHIEU_DAI;
        }
        public Decimal TAI_SAN_ID { get; set; }
        public Decimal? DIEN_TICH { get; set; }
        public Decimal? THE_TICH { get; set; }
        public Decimal? CHIEU_DAI { get; set; }
        public virtual TaiSan taisan { get; set; }
    }
}



