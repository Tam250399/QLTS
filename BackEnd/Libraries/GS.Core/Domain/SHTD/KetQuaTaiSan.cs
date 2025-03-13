using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.SHTD
{
    public partial class KetQuaTaiSan:BaseEntity
    {
		public Decimal? TAI_SAN_TD_ID { get; set; }
		public Decimal? XU_LY_KET_QUA_ID { get; set; }
		public Decimal? SO_LUONG { get; set; }
		public Decimal? SO_TIEN { get; set; }
		public Decimal? TAI_SAN_TD_XU_LY_ID { get; set; }
		public virtual XuLyKetQua xulyketqua { get; set; }
		public virtual TaiSanTd taisantd { get; set; }
		public virtual TaiSanTdXuLy taisantdxuly { get; set; }
	}
}
