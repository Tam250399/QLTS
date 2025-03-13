//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using System;


namespace GS.Core.Domain.DanhMuc
{
    public partial class DongXe : BaseEntity
    {
        public String MA { get; set; }
        public String TEN { get; set; }
        public String MO_TA { get; set; }
        public Decimal? NHAN_XE_ID { get; set; }
        public int? DB_ID { get; set; }
        public virtual NhanXe NhanXe { get; set; }
    }
}



