//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;

namespace GS.Core.Domain.TaiSans
{
	public partial class TaiSanVoHinh : BaseEntity
	{
		public TaiSanVoHinh()
		{

		}
		public TaiSanVoHinh(TaiSanVoHinh obj)
		{
			this.TAI_SAN_ID = obj.TAI_SAN_ID;
			this.THONG_SO_KY_THUAT = obj.THONG_SO_KY_THUAT;
		}

		public Decimal TAI_SAN_ID { get; set; }
		public String THONG_SO_KY_THUAT { get; set; }
		public virtual TaiSan taisan { get; set; }
	}
}



