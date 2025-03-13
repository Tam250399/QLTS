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
	/// <summary>
	/// nhãn hiển thị lên báo cáo
	/// </summary>
	public enum enumNHAN_HIEN_THI_LOAI_HINH_TSTD
	{
		ALL = 0,
		DAT = 1,
		NHA = 2,
		OTO = 3,
		TAI_SAN_KHAC = 4
	}
	public enum enumLOAI_HINH_TAI_SAN_TOAN_DAN
	{
		ALL = 0,
		DAT = 1,
		NHA = 2,
		OTO = 3,
		TAI_SAN_KHAC = 4
	}
	public enum enumTRANGTHAITSTD {
		XOA = 0,
		TONTAI =1,
		NHAP = 3
	}
	public partial class TaiSanTd : BaseEntity
	{
        public TaiSanTd()
        {
            GUID = Guid.NewGuid();
        }
		public Guid GUID {get;set;}
		public String TEN {get;set;}
		public String TEN_LOAI_TAI_SAN { get; set; }
		public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
		public Decimal? LOAI_TAI_SAN_ID {get;set;}
		public Decimal? QUYET_DINH_TICH_THU_ID { get; set; }
		public Decimal? SO_LUONG {get;set;}
		public Decimal? GIA_TRI_XAC_LAP { get; set; }
		public Decimal? GIA_TRI_TINH { get; set; }
		public String DON_VI_TINH { get; set; }
		public String GHI_CHU { get; set; }
		public Decimal? TRANG_THAI_ID { get; set; }
		public DateTime? NGAY_SU_DUNG { get; set; }
		public String BIEN_KIEM_SOAT { get; set; }
		public Decimal? NHAN_XE_ID { get; set; }
		public Decimal? SO_CHO_NGOI { get; set; }
		public Decimal? TAI_TRONG { get; set; }
		public Decimal? TAI_SAN_DAT_ID { get; set; }
		public String DIA_CHI { get; set; }
		public Decimal? SO_CAU_XE { get; set; }
		public string DB_ID { get; set; }
		public string DB_QUYET_DINH_TICH_THU_ID { get; set; }
		public virtual QuyetDinhTichThu quyetdinh { get; set; }
		public virtual LoaiTaiSan loaitaisan { get; set; }
	}  
}



