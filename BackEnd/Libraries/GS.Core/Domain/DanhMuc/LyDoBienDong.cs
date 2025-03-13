//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Core.Domain.DanhMuc
{
	public enum enumLyDoTangGiamForBaoCao
	{
		TAT_CA = 0,
		DANG_KY_LAN_DAU = 1,
		DAT_DUOC_TIEP_NHAN = 2,
		NHAN_CHUYEN_NHUONG = 3,
		MUA_SAM = 4,
		DAT_DI_THUE = 5,
		DAT_DUOC_GIAO_MOI = 6,
		XAY_DUNG_MOI = 7,
		TIEP_NHAN = 8,
		TIEP_NHAN_TU_DIEU_CHUYEN = 9,
		MUA_MOI = 10,
		GIAO_VON_CHO_DON_VI_CHU_TAI_CHINH = 11,
		BAN = 12,
		CHUYEN_NHUONG = 13,
		TIEU_HUY = 14,
		DIEU_CHUYEN_NGOAI_HE_THONG = 15,
		THANH_LY = 16,
		DIEU_CHUYEN = 17,
		BI_THU_HOI = 18,
		KHAC = 19
	}
	/// <summary>
	/// LOAI_BIEN_DONG_ID
	/// </summary>
	public enum enumLOAI_LY_DO_TANG_GIAM
	{
		TANG_TOAN_BO = 1,//tang toan bo
		BIEN_DONG_TANG_GIA_TRI = 2,// tăng
		BIEN_DONG_GIAM_GIA_TRI = 3,// giảm
		GIAM_TOAN_BO = 5,// giảm
		GIAM_DIEU_CHUYEN_MOT_PHAN = 6,// giảm
									  //THAY_DOI_KHAC = 4,
									  //GIAM_CAP_NHAT_TIEN_BAN_THANH_LY = 7,// giảm
									  //GIAM_DIEU_CHUYEN_NOI_BO = 8,//giảm
									  //GIAM_DIEU_CHUYEN_KHAC = 9,//giảm
									  //GIAM_DIEU_CHUYEN_NGOAI_HE_THONG = 10,// giảm
		THAY_DOI_THONG_TIN = 11,
		NHAP_SO_DU_DAU_KY = 12
	}
	public static class enum_LOAI_LY_DO_BIEN_DONG
	{
		public static string LY_DO_TANG = "001";
		public static string LY_DO_GIAM = "002";
		public static string LY_DO_TANG_1_PHAN = "003";
		public static string LY_DO_GIAM_1_PHAN = "004";
		public static string THAY_DOI_THONG_TIN = "005";
		public static string DIEU_CHUYEN_1_PHAN = "041";
	}
	public static class enum_LY_DO_BIEN_DONG
	{
		public static string DANG_KY_LAN_DAU = "001";
		public static string NHA_NUOC_GIAO_DAT = "002";
		public static string NHA_NUOC_CHO_THUE_DAT = "003";
		public static string DAU_TU_XAY_DUNG = "004";
		public static string TIEP_NHAN = "005";
		public static string MUA_SAM = "006";
		public static string KIEM_KE_PHAT_HIEN_THUA = "007";
		public static string BAN_CHUYEN_NHUONG = "008";
		public static string BI_THU_HOI = "009";
		public static string CHUYEN_GIAO_VE_DIA_PHUONG = "010";
		public static string DIEU_CHUYEN = "011";
		public static string THANH_LY = "012";
		public static string TIEU_HUY = "013";
		public static string GIAM_DO_BI_MAT_HUY_HOAI = "014";
		public static string KHAC = "015";
		public static string TANG_GIA_DAT = "016";
		public static string TANG_DIEN_TICH_DAT = "017";
		public static string TANG_NANG_CAP_MO_RONG_SUA_CHUA = "018";
		public static string TANG_NANG_CAP_SUA_CHUA_LON = "019";
		public static string TANG_LAP_DAP_THEM_BO_PHAN = "020";
		public static string TANG_DANH_GIA_LAI_GIA_TRI = "021";
		public static string TANG_THAY_DOI_TINH_TRANG_PHE_DUYET_QUYET_TOAN = "022";
		public static string GIAM_GIA_DAT = "023";
		public static string GIAM_DIEN_TICH_DAT = "024";
		public static string GIAM_CAI_TAO_THU_HEP_DIEN_TICH = "025";
		public static string GIAM_CAI_TAO_THAO_DO_MOT_PHAN = "026";
		public static string GIAM_DANH_GIA_LAI_GIA_TRI_TAI_SAN = "027";
		public static string GIAM_THAY_DOI_TINH_TRANG_PHE_DUYET_QUYET_TOAN = "028";
		public static string THAY_DOI_THONG_TIN = "030";
		public static string DIEU_CHUYEN_MOT_PHAN = "031";
		//thêm lý do giảm ngày 8/1/2022
		public static string NHA_NUOC_THU_HOI_MOT_PHAN = "040";
		public static string CHUYEN_GIAO_VE_DIA_PHUONG_MOT_PHAN = "041";
		public static string LY_DO_KHAC = "042";
	}
	public enum enumHINH_THUC_XU_LY_TSC
	{
		CHON_HINH_THUC = 0,
		PHA_DO_HUY_BO = 1,
		BAN_DAU_GIA = 2,
		BAN_CHI_DINH = 3,
		BAN_NIEM_YET_GIA = 4
	}
	public enum enumLY_DO_GIAM_TOAN_BO
	{
		DIEU_CHUYEN = 614,
		BAN_CHUYEN_NHUONG = 611,
		THANH_LY = 615,     //phương tiện khác
	}
	public enum enumLY_DO_GIAM_THU_TIEN
	{
		BAN_CHUYEN_NHUONG = 611,
		THANH_LY = 615,
	}
	public enum enumLY_DO_TANG_GIAM_NGUYEN_GIA
	{
		TANG_GIA_DAT = 619,
		DANH_GIA_LAI_NGUYEN_GIA_TANG = 624,
		DANH_GIA_LAI_NGUYEN_GIA_GIAM = 630,
		GIAM_GIA_DAT = 626,
		DIEU_CHINH_GIA_TRI_TS_GIAM = 630,
		THAY_DOI_TINH_TRANG_PHE_DUYET_QUYET_TOAN_TANG = 625,
		THAY_DOI_TINH_TRANG_PHE_DUYET_QUYET_TOAN_GIAM = 631,
		GIAM_DIEN_TICH_DAT = 627,
		CAI_TAO_THU_HIEP_DIEN_TICH = 628,
		TANG_DIEN_TICH_DAT = 620,
		NANG_CAP_MO_RONG_DIEN_TICH_DAT = 622
	}
	public enum enumLY_DO_THAY_DOI_TINH_TRANG_PHE_DUYET_QUYET_TOAN
	{
		THAY_DOI_TINH_TRANG_PHE_DUYET_QUYET_TOAN_BIEN_DONG_TANG_GIA_TRI = 625,
		THAY_DOI_TINH_TRANG_PHE_DUYET_QUYET_TOAN_BIEN_DONG_GIAM_GIA_TRI = 631
	}
	public enum enumLY_DO_KIEM_KE_THUA
	{
		//ALL_KIEM_KE_PHAT_HIEN_THUA = 618 //id db dev
		ALL_KIEM_KE_PHAT_HIEN_THUA = 610 //id db test
	}
	public partial class LyDoBienDong : BaseEntity
	{

		public String MA { get; set; }
		public String TEN { get; set; }
		public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
		public Decimal? LOAI_LY_DO_ID { get; set; }
		public Decimal? LOAI_LY_DO_BIEN_DONG_ID { get; set; }
		public Decimal? TRUONG_SAP_XEP { get; set; }
		public string LOAI_DON_VI { get; set; }
		public enumLOAI_LY_DO_TANG_GIAM loaiLyDoTangGiam
		{
			get => (enumLOAI_LY_DO_TANG_GIAM)LOAI_LY_DO_ID.GetValueOrDefault();
			set => LOAI_LY_DO_ID = (int)value;
		}
		public Decimal? DB_ID { get; set; }
		public string LOAI_HINH_TAI_SAN_AP_DUNG_ID { get; set; }
		public virtual LoaiLyDoBienDong LoaiLyDoBienDong { get; set; }
	}
}



