//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.DanhMuc;
using System;

namespace GS.Core.Domain.TaiSans
{
    public partial class TaiSanNguonVon : BaseEntity
    {
        public TaiSanNguonVon()
        {

        }
        public TaiSanNguonVon(TaiSanNguonVon obj)
        {
            this.TAI_SAN_ID = obj.TAI_SAN_ID;
            this.NGUON_VON_ID = obj.NGUON_VON_ID;
            this.GIA_TRI = obj.GIA_TRI;
            this.BIEN_DONG_ID = obj.BIEN_DONG_ID;
        }
		public TaiSanNguonVon(NguonVonModelCore obj, decimal TaiSanId)
		{
            this.TAI_SAN_ID = TaiSanId;
            this.NGUON_VON_ID = obj.ID;
            this.GIA_TRI = obj.GiaTri??0;
        }
        public Decimal TAI_SAN_ID { get; set; }
        public Decimal NGUON_VON_ID { get; set; }
        public Decimal GIA_TRI { get; set; }
        public Decimal BIEN_DONG_ID { get; set; }
        public virtual TaiSan taisan { get; set; }
        public virtual NguonVon nguonvon { get; set; }
        public virtual BienDong biendong { get; set; }
    }
}



