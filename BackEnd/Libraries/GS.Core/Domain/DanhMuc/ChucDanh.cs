//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using System;


namespace GS.Core.Domain.DanhMuc
{
    public partial class ChucDanh : BaseEntity
    {
        public String TEN_CHUC_DANH { get; set; }
        public String MO_TA { get; set; }
        public String MA_CHUC_DANH { get; set; }
        public Decimal? KHOI_DON_VI_ID { get; set; }
        public Decimal? DINH_MUC { get; set; }
        public decimal? DB_ID { get; set; }
    }
    public enum KhoiDonViEnum
    {
        All = 0,
        BoCoQuanTrungUong = 1,
        Tinh = 2
    }
}



