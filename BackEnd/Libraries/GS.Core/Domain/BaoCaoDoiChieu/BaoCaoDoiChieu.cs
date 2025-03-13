//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/10/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core.Domain.DanhMuc;

namespace GS.Core.Domain.BaoCaoDoiChieus
{
	public partial class BaoCaoDoiChieu : BaseEntity
	{
		public Decimal? DON_VI_ID { get; set; }
		public Decimal BAO_CAO_ID { get; set; }
		public Decimal NAM_BAO_CAO { get; set; }
		public Decimal HE_THONG_ID { get; set; }
		public DateTime? NGAY_TAO { get; set; }
		public DateTime? NGAY_CAP_NHAT { get; set; }
		public Decimal FILE_ID { get; set; }
		//public String TenFile { get; set; }
		//public Object FILE_CONTENT { get; set; }
		public enumHeThong PhanMem
		{
			get => (enumHeThong)HE_THONG_ID;
			set => HE_THONG_ID = (int)value;
		}
		public enumPhanBaoCao PhanBaoCao
		{
			get => (enumPhanBaoCao)BAO_CAO_ID;
			set => BAO_CAO_ID = (int)value;
		}

	}
}
