using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.DanhMuc
{
    public partial class NguonTaiSan : BaseEntity
    {
        public String TEN { get; set; }
        public decimal? NGUOI_DUNG_ID { get; set; }
        public decimal? TRANG_THAI_ID { get; set; }
        public bool IS_REPORT { get;set; }
        public bool IS_SHOW { get;set; }
    }
}
