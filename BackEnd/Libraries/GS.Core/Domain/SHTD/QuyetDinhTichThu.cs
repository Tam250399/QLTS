//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core.Domain.DanhMuc;

namespace GS.Core.Domain.SHTD
{
	public partial class QuyetDinhTichThu : BaseEntity
	{
		public enum enumTRANGTHAI_QUYETDINH_TSTD
		{
			XOA = 0,
			CHO_DUYET = 1,
			NHAP = 2,
			DUYET = 3,
			TU_CHOI = 4
		}
		public QuyetDinhTichThu()
        {
            GUID = Guid.NewGuid();
        }
		public Guid GUID {get;set;}
		public String QUYET_DINH_SO {get;set;}
		public DateTime? QUYET_DINH_NGAY {get;set;}
		public Decimal? TONG_GIA_TRI { get; set; }
		public String TEN {get;set;}
		public String GHI_CHU {get;set;}
		public Decimal? NGUOI_TAO_ID { get; set; }
		public DateTime? NGAY_TAO { get; set; }
		public Decimal? DON_VI_ID { get; set; }
		public Decimal? TRANG_THAI_ID { get; set; }
		public Decimal? CO_QUAN_BAN_HANH_ID { get; set; }
		public String NGUOI_QUYET_DINH { get; set; }
		public Decimal? NGUON_GOC_ID { get; set; }
		public String DB_ID { get; set; }
		public virtual DonVi DonVi { get; set; }
		public virtual NguonGocTaiSan NguonGocTaiSan { get; set; }
		public virtual DonVi BoNganhTinh { get; set; }


	}
}



