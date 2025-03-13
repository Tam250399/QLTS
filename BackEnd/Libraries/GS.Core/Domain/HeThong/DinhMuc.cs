//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 18/6/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.HeThong
{
	public partial class DinhMuc : BaseEntity
	{
		public String MA {get;set;}
		public String QUYET_DINH_SO {get;set;}
		public DateTime QUYET_DINH_NGAY {get;set;}
		public DateTime TU_NGAY {get;set;}
		public DateTime? DEN_NGAY {get;set;}
		public String THONG_TU {get;set;}
		public Decimal? DON_VI_ID {get;set;}
		
	}
	public class KetQuaDinhMuc : BaseViewEntity
	{
		public decimal? SoLuongVuotQua { get; set; }
		public decimal? SoTienVuotQua { get; set; }
		public decimal? SoLuongDinhMuc { get; set; }
		public decimal? NguyenGiaDinhMuc { get; set; }
		public decimal? SoLuongDangCo { get; set; }
	}
	
}



