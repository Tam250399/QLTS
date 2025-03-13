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


namespace GS.Core.Domain.TaiSans
{
	public partial class KhaiThacTaiSan : BaseEntity
	{
		public decimal KHAI_THAC_ID {get;set;}
		public decimal? TAI_SAN_ID {get;set;}
		public decimal KHAI_THAC_CHI_TIET_ID { get; set; }
		public Decimal? DIEN_TICH_KHAI_THAC {get;set;}
		public virtual TaiSan taiSan { get; set; }
		public virtual KhaiThac khaiThac { get; set; }
        public decimal? SO_TIEN { get; set; }
        public string GHI_CHU { get; set; }
        public DateTime? TU_NGAY { get; set; }
		public DateTime? DEN_NGAY { get; set; }
	}
}



