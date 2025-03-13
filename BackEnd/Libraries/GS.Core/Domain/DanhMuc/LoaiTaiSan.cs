//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace GS.Core.Domain.DanhMuc
{
	public enum enumLoaiXe
	{
		TatCa = 0,
		XeTai = 1,
		XeChucDanh = 2,
		Khac = 3,
	}
	public static class enumLoaiTaiSan_Dat
	{
		public const string LTS_DAT_TRU_SO = "101";
		public const string LTS_DAT_SU_NGHIEP = "102";
	}
	public enum enumLoaiTaiSan_DatCQNN
	{
		 LTS_DAT_CONG_CONG = 10208,
		 //LTS_DAT_VAN_HOA = 10203,
		 //LTS_DAT_KHAC = 10209, 
		 //LTS_DAT_THE_THAO = 10204
	}
	public static class enumLoaiTaiSan_Khac
    {
        public const string TS_DE_HONG_DE_VO = "801";
    }
    public partial class LoaiTaiSan : BaseEntity
	{
		public String MA { get; set; }
		public String TEN { get; set; }
		public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
		public Decimal? HM_THOI_HAN_SU_DUNG { get; set; }
		public Decimal? HM_TY_LE { get; set; }
		public String MO_TA { get; set; }
		public Decimal? CHE_DO_HAO_MON_ID { get; set; }
		public Decimal? PARENT_ID { get; set; }
		public String TREE_NODE { get; set; }
		public Decimal? TREE_LEVEL { get; set; }
		public string DON_VI_TINH { get; set; }
		public decimal? SO_THU_TU { get; set; }



		public enumLOAI_HINH_TAI_SAN LoaiHinhTaiSan
		{
			get => (enumLOAI_HINH_TAI_SAN)LOAI_HINH_TAI_SAN_ID;
			set => LOAI_HINH_TAI_SAN_ID = (int)value;
		}

		public virtual CheDoHaoMon CheDoHaoMon { get; set; }
		public virtual LoaiTaiSan LoaiTaiSanCha { get; set; }
		public virtual ICollection<LoaiTaiSan> ListLoaiTaiSanCon { get; set; }
		

		/// <summary>
		/// Mã tài sản ở cơ quan tổ chức đơn vị KHO
		/// </summary>
		public String DB_ID_JSON { get; set; }

		public Decimal? OTO_CHO_NGOI_TU { get; set; }
		public Decimal? OTO_CHO_NGOI_DEN { get; set; }
		public decimal? OTO_LOAI_XE_ID { get; set; }
        public Decimal? DB_ID { get; set; }
	}	
	public enum enumCHE_DO_HACH_TOAN
	{
		HAO_MON = 1,
		KHAU_HAO = 2,
		HAO_MON_VA_KHAU_HAO = 3,
	}
	public enum enumLOAI_HINH_TAI_SAN
	{
		ALL = 0,
		DAT = 1,
		NHA = 2,
		TAI_SAN_VAT_KIEN_TRUC = 3,
		OTO = 4,
		PHUONG_TIEN_KHAC = 5,
		TAI_SAN_MAY_MOC_THIET_BI = 6,
		TAI_SAN_CAY_LAU_NAM_SVLV = 7,
		HUU_HINH_KHAC = 8,
		VO_HINH = 9,
		DAC_THU = 10,
		TAI_SAN_TOAN_DAN_KHAC = 11,
		TAI_SAN_TOAN_DAN_DAT = 12,
		TAI_SAN_TOAN_DAN_NHA = 13,
		TAI_SAN_TOAN_DAN_OTO = 14,
		TAI_SAN_TOAN_DAN_TAI_SAN_KHAC = 15,
		//TAI_SAN_CONG_TRINH_NUOC_SACH = 12,
		//TAI_SAN_HA_TANG_NONG_THON = 13,
		KHAC = 30,
		TSCD_KHAC =16,
		TAI_SAN_QUAN_LY_NHU_TSCD =17
	}

	public enum enumLOAI_HINH_TAI_SAN_BAO_CAO
	{
		ALL = 0,
		DAT = 1,
		NHA = 2,
		TAI_SAN_VAT_KIEN_TRUC = 3,
		OTO = 4,
		PHUONG_TIEN_KHAC = 5,
		TAI_SAN_MAY_MOC_THIET_BI = 6,
		TAI_SAN_CAY_LAU_NAM_SVLV = 7,
		HUU_HINH_KHAC = 8,
		VO_HINH = 9,
		DAC_THU = 10,
		TAI_SAN_TOAN_DAN_KHAC = 11,

		//TAI_SAN_CONG_TRINH_NUOC_SACH = 12,
		//TAI_SAN_HA_TANG_NONG_THON = 13,
		KHAC = 30,

		TREN_500 = 500,
		DUOI_500 = 501,
	}
	public enum enumLoaiTaiSanDefault
	{
		TS_CO_DINH_DAC_THU = 1116,
		TS_HUU_HINH_KHAC = 686,      
	}
	public enum enumPHUONG_PHAP_TINH_KHAU_HAO
	{
		DUONG_THANG,
		SO_DU_DIEU_CHINH,
		SO_LUONG_KHOI_LUONG_SP,
	}
	public enum enumLoaiTaiSanOto
	{
		CHUYEN_DUNG = 588,
		OTO_CHUC_DANH = 401
	}
	/// <summary>
	/// Value: mã loại tài sản
	/// </summary>

}