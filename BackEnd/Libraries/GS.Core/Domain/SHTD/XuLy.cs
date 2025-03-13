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


namespace GS.Core.Domain.SHTD
{
	public enum enumLoaiXuLy
	{
		DeXuat = 1,
		KetQua = 2,
		PhuongAn = 3
		
	}
	public enum enumTrangThaiXuLy
	{
		Nhap = 1,
		TonTai = 2

	}
	public partial class XuLy : BaseEntity
	{
        public XuLy()
        {
            this.GUID = Guid.NewGuid();
        }
		public Guid? GUID {get;set;}
		public String QUYET_DINH_SO {get;set;}
		public DateTime? QUYET_DINH_NGAY {get;set;}
		public Decimal? DON_VI_ID {get;set;}
		public String GHI_CHU {get;set;}
		public DateTime? NGAY_TAO { get; set; }
		public String NGUOI_QUYET_DINH { get; set; }
		public Decimal? CO_QUAN_BAN_HANH_ID { get; set; }
		public Decimal? TRANG_THAI_ID { get; set; }
		public string DB_ID { get; set; }

	}
}



