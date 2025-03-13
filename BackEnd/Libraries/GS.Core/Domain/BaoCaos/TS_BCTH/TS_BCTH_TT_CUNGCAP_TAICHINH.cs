namespace GS.Core.Domain.BaoCaos.TS_BCTH
{
	public class TS_BCTH_TT_CUNGCAP_TAICHINH : BaseViewEntity
	{
		public decimal? DON_VI_ID { get; set; }
		public string DON_VI_TEN { get; set; }
		public string MA_DON_VI { get; set; }
		public string DV_1 { get; set; }
		public string DV_2 { get; set; }
		public string DV_TEN_1 { get; set; }
		public string DV_TEN_2 { get; set; }

		#region Nguyên giá

		// Nguyên giá số đầu năm
		public decimal? NG_DAU_NAM_QUYEN_SD_DAT { get; set; }

		public decimal? NG_DAU_NAM_CTRINH_PHANMEM { get; set; }
		public decimal? NG_DAU_NAM_GIATRI_THUONGHIEU { get; set; }
		public decimal? NG_DAU_NAM_KHAC { get; set; }
		public decimal? NG_DAU_NAM_BAN_QUYEN { get; set; }

		// Nguyên giá tăng trong năm
		public decimal? NG_TANG_TRONG_NAM_QUYEN_SD_DAT { get; set; }

		public decimal? NG_TANG_TRONG_NAM_CTRINH_PHANMEM { get; set; }
		public decimal? NG_TANG_TRONG_NAM_GIATRI_THUONGHIEU { get; set; }
		public decimal? NG_TANG_TRONG_NAM_KHAC { get; set; }
		public decimal? NG_TANG_TRONG_NAM_BAN_QUYEN { get; set; }

		// Nguyên giá giảm trong năm
		public decimal? NG_GIAM_TRONG_NAM_QUYEN_SD_DAT { get; set; }

		public decimal? NG_GIAM_TRONG_NAM_CTRINH_PHANMEM { get; set; }
		public decimal? NG_GIAM_TRONG_NAM_GIATRI_THUONGHIEU { get; set; }
		public decimal? NG_GIAM_TRONG_NAM_KHAC { get; set; }
		public decimal? NG_GIAM_TRONG_NAM_BAN_QUYEN { get; set; }

		// Nguyên giá số cuối năm
		public decimal? NG_CUOI_NAM_QUYEN_SD_DAT { get; set; }

		public decimal? NG_CUOI_NAM_CTRINH_PHANMEM { get; set; }
		public decimal? NG_CUOI_NAM_GIATRI_THUONGHIEU { get; set; }
		public decimal? NG_CUOI_NAM_KHAC { get; set; }
		public decimal? NG_CUOI_NAM_BAN_QUYEN { get; set; }

		#endregion Nguyên giá

		#region Khấu hao, hao mòn lũy kế

		// Khấu hao, hao mòn lũy kế số đầu năm
		public decimal? KHHM_DAU_NAM_QUYEN_SD_DAT { get; set; }

		public decimal? KHHM_DAU_NAM_CTRINH_PHANMEM { get; set; }
		public decimal? KHHM_DAU_NAM_GIATRI_THUONGHIEU { get; set; }
		public decimal? KHHM_DAU_NAM_KHAC { get; set; }
		public decimal? KHHM_DAU_NAM_BAN_QUYEN { get; set; }

		// Khấu hao, hao mòn lũy kế tăng trong năm
		public decimal? KHHM_TANG_TRONG_NAM_QUYEN_SD_DAT { get; set; }

		public decimal? KHHM_TANG_TRONG_NAM_CTRINH_PHANMEM { get; set; }
		public decimal? KHHM_TANG_TRONG_NAM_GIATRI_THUONGHIEU { get; set; }
		public decimal? KHHM_TANG_TRONG_NAM_KHAC { get; set; }
		public decimal? KHHM_TANG_TRONG_NAM_BAN_QUYEN { get; set; }

		// Khấu hao, hao mòn lũy kế giảm trong năm
		public decimal? KHHM_GIAM_TRONG_NAM_QUYEN_SD_DAT { get; set; }

		public decimal? KHHM_GIAM_TRONG_NAM_CTRINH_PHANMEM { get; set; }
		public decimal? KHHM_GIAM_TRONG_NAM_GIATRI_THUONGHIEU { get; set; }
		public decimal? KHHM_GIAM_TRONG_NAM_KHAC { get; set; }
		public decimal? KHHM_GIAM_TRONG_NAM_BAN_QUYEN { get; set; }

		// Khấu hao, hao mòn lũy kế số cuối năm
		public decimal? KHHM_CUOI_NAM_QUYEN_SD_DAT { get; set; }

		public decimal? KHHM_CUOI_NAM_CTRINH_PHANMEM { get; set; }
		public decimal? KHHM_CUOI_NAM_GIATRI_THUONGHIEU { get; set; }
		public decimal? KHHM_CUOI_NAM_KHAC { get; set; }
		public decimal? KHHM_CUOI_NAM_BAN_QUYEN { get; set; }

		#endregion Khấu hao, hao mòn lũy kế

		#region Giá trị còn lại

		// Giá trị còn lại số đầu năm
		public decimal? GTCL_DAU_NAM_QUYEN_SD_DAT { get; set; }

		public decimal? GTCL_DAU_NAM_CTRINH_PHANMEM { get; set; }
		public decimal? GTCL_DAU_NAM_GIATRI_THUONGHIEU { get; set; }
		public decimal? GTCL_DAU_NAM_KHAC { get; set; }
		public decimal? GTCL_DAU_NAM_BAN_QUYEN { get; set; }

		// Giá trị còn lại số cuối năm
		public decimal? GTCL_CUOI_NAM_QUYEN_SD_DAT { get; set; }

		public decimal? GTCL_CUOI_NAM_CTRINH_PHANMEM { get; set; }
		public decimal? GTCL_CUOI_NAM_GIATRI_THUONGHIEU { get; set; }
		public decimal? GTCL_CUOI_NAM_KHAC { get; set; }
		public decimal? GTCL_CUOI_NAM_BAN_QUYEN { get; set; }

		#endregion Giá trị còn lại
	}
}