//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 22/5/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.KT
{
	public partial class HaoMonTaiSanLog : BaseEntity
	{
		public Decimal? TAI_SAN_ID {get;set;}
		public DateTime? NGAY_TINH {get;set;}
		public Decimal? NAM_TINH {get;set;}
		
	}
	public partial class KTGiaTriTaiSan
	{
		//
		public Decimal? DON_VI_CHE_DO_HACH_TOAN_ID { get; set; }

		public Decimal? LoaiTaiSanId { get; set; }
		public Decimal? LoaiTaiSanDonViId { get; set; }
		public Decimal? LoaiHinhTaiSanId { get; set; }
		public DateTime? TS_NgayKetThucTinh { get; set; }   //ngày nhập
		public DateTime? TS_NgayBatDauTinh { get; set; }    //ngày đưa vào sử dụng
		public DateTime? KH_NgayBatDau { get; set; } //ngày bắt đầu tính kh 
		public Decimal? TS_NguyenGia { get; set; }
		public Decimal? TS_GiaTriHienTai { get; set; }
		public Decimal? TS_TyLeNguyenGiaTrichKH { get; set; }
		public Decimal? TS_TyLeNguyenGiaTinhHM { get; set; }
		public Decimal? TS_NguyenGiaTinhKH { get; set; }
		public Decimal? TS_NguyenGiaTinhHM { get; set; } //nguyên giá trừ nguyên giá tình hao mòn
		public Decimal? TS_NguyenGiaBienDong { get; set; } //nguyên giá biến động (nếu có)
														 // bien dong
		public Decimal? TaiSanId { get; set; }
		public DateTime? TS_NgayBienDong { get; set; }
		public Decimal? TS_NguyenGiaTang { get; set; }
		public Decimal? TS_GTCLTruocBD { get; set; }//dùng các biến động tăng, giảm, điều chuyển
		public Decimal? TS_GTCLSauBD { get; set; } //dùng các biến động tăng, giảm, điều chuyển
		public Decimal? LoaiLyDoBienDong { get; set; }
		//Hao mon
		public Decimal? HM_GiaTriTinh { get; set; }
		public Decimal? HM_GiaTriConLai { get; set; }
		public Decimal? HM_NamConLai { get; set; }
		public Decimal? HM_NamSuDung { get; set; }
		public Decimal? HM_NamTheoQD { get; set; }
		public Decimal? HM_TyLe { get; set; }
		public Decimal? HM_LuyKe { get; set; }
		public Decimal? HM_GiaTriHaoMonMotNam { get; set; }

		//Khau Hao
		public Decimal? KH_GiaTriTinh { get; set; }
		public Decimal? KH_GiaTriConLai { get; set; }
		public Decimal? KH_ThangConLai { get; set; }
		public Decimal? KH_ThangSuDung { get; set; }
		public Decimal? KH_ThangTheoQD { get; set; }
		public Decimal? KH_TyLe { get; set; }
		public Decimal? KH_LuyKe { get; set; }
		public Decimal? KH_GiaTriTrichMotThang { get; set; }

	}
}



