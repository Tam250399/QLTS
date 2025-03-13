//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.NghiepVu;
using System;
using System.Collections.Generic;

namespace GS.Core.Domain.TaiSans
{
	//sử dụng làm nhãn cho báo cáo
	public enum enumNHAN_HIEN_THI_LOAI_HINH_TS
	{
		DAT = 1,
		NHA = 2,
		OTO = 4,
		TAI_SAN_CO_DINH_KHAC = 20
	}

	// thêm Phương thức mua sắm tập trung và phân tán
	public enum enumPHUONG_THUC_MUA_SAM
	{
		TapTrung = 1,
		PhanTan = 2,
		Khac = 3
	}

	//Sử dụng cho phần tài sản, khai thác tài sản, mua sắm tài sản
	public enum enumTRANG_THAI_TAI_SAN
	{
		ALL = 0,
		NHAP = 1,
		NHAP_LIEU = 2,
		CHO_DUYET = 3,
		DA_DUYET = 4,
		TRA_LAI = 5,
		XOA = 6,

		/// <summary>
		/// Tài sản nhận từ kho đã insert để lấy mã. cần chờ để cập nhật thêm thông tin
		/// </summary>
		CHO_DONG_BO = 7,

		DA_DUYET_GIAM_TOAN_BO = 8
	}


	//phan loai tai san theo nguyen gia
	public enum enumPHAN_LOAI_TAI_SAN
    {
		DAT_NHA_OTO = 0,
		TS_DUOI_500TR = 1,
		TS_TREN_500TR = 2
    }

	public enum enumTS_NGUYEN_GIA
	{
		TAT_CA = 0,
		TREN_500_TRIEU = 1,
		DUOI_500_TRIEU = 2,
	}

	public enum enumIsMienThue
	{
		MienThue = 1,
		KhongMienThue = 2,
	}

	public enum enumNguoiTaoTaiSan
	{
		KhongNguoiQuanLy = -1
	}

	public partial class TaiSan : BaseEntity
	{
		public TaiSan()
		{
		}

		public TaiSan(TaiSan _main)
		{
			this.CHUNG_TU_NGAY = _main.CHUNG_TU_NGAY;
			this.CHUNG_TU_SO = _main.CHUNG_TU_SO;
			this.DOI_TAC_ID = _main.DOI_TAC_ID;
			this.DON_VI_BO_PHAN_ID = _main.DON_VI_BO_PHAN_ID;
			this.DON_VI_ID = _main.DON_VI_ID;
			this.DU_AN_ID = _main.DU_AN_ID;
			this.GHI_CHU = _main.GHI_CHU;
			this.GUID = _main.GUID;
			this.ID = _main.ID;
			this.LOAI_HINH_TAI_SAN_ID = _main.LOAI_HINH_TAI_SAN_ID;
			this.LOAI_TAI_SAN_ID = _main.LOAI_TAI_SAN_ID;
			this.LOAI_TAI_SAN_DON_VI_ID = _main.LOAI_TAI_SAN_DON_VI_ID;
			this.LY_DO_BIEN_DONG_ID = _main.LY_DO_BIEN_DONG_ID;
			this.MA = _main.MA;
			this.NAM_SAN_XUAT = _main.NAM_SAN_XUAT;
			this.NGAY_DUYET = _main.NGAY_DUYET;
			this.NGAY_NHAP = _main.NGAY_NHAP;
			this.NGAY_SU_DUNG = _main.NGAY_SU_DUNG;
			this.NGAY_TAO = _main.NGAY_TAO;
			this.NGUOI_TAO_ID = _main.NGUOI_TAO_ID;
			this.NUOC_SAN_XUAT_ID = _main.NUOC_SAN_XUAT_ID;
			this.QUYET_DINH_NGAY = _main.QUYET_DINH_NGAY;
			this.QUYET_DINH_NGUOI_ID = _main.QUYET_DINH_NGUOI_ID;
			this.QUYET_DINH_SO = _main.QUYET_DINH_SO;
			this.TEN = _main.TEN;
			this.TRANG_THAI_ID = _main.TRANG_THAI_ID;
			this.NGUYEN_GIA_BAN_DAU = _main.NGUYEN_GIA_BAN_DAU;
			this.GIA_MUA_TIEP_NHAN = _main.GIA_MUA_TIEP_NHAN;
			this.IS_XAC_NHAN = _main.IS_XAC_NHAN;
			this.NGAY_XAC_NHAN = _main.NGAY_XAC_NHAN;
			this.IS_MIEN_THUE = _main.IS_MIEN_THUE;
			this.GIA_HOA_DON = _main.GIA_HOA_DON;
			this.MIEN_THUE_SO_TIEN = _main.MIEN_THUE_SO_TIEN;
			this.HM_SO_NAM_CON_LAI = _main.HM_SO_NAM_CON_LAI;
			this.HM_TY_LE = _main.HM_TY_LE;
			this.IS_DUYET = _main.IS_DUYET;
			this.NGUON_TAI_SAN_ID = _main.NGUON_TAI_SAN_ID;
			this.PHUONG_THUC_MUA_SAM_ID = _main.PHUONG_THUC_MUA_SAM_ID;
			this.DON_VI_MUA_SAM_TAP_TRUNG_ID = _main.DON_VI_MUA_SAM_TAP_TRUNG_ID;
			this.PHAN_LOAI_TAI_SAN = _main.PHAN_LOAI_TAI_SAN;
			this.LA_TS_HET_HM_CO_THAY_DOI_NG = _main.LA_TS_HET_HM_CO_THAY_DOI_NG;
			this.SO_NAM_SD_CON_LAI = _main.SO_NAM_SD_CON_LAI;
	}

		public String MA { get; set; }
		public String TEN { get; set; }
		public Decimal? LOAI_TAI_SAN_ID { get; set; }
		public Decimal? DU_AN_ID { get; set; }
		public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
		public Decimal? LOAI_TAI_SAN_DON_VI_ID { get; set; }
		public Decimal? TRANG_THAI_ID { get; set; }
		public String QUYET_DINH_SO { get; set; }
		public DateTime? QUYET_DINH_NGAY { get; set; }
		public Decimal? QUYET_DINH_NGUOI_ID { get; set; }
		public Decimal? NUOC_SAN_XUAT_ID { get; set; }
		public Decimal? LY_DO_BIEN_DONG_ID { get; set; }
		public Decimal? DOI_TAC_ID { get; set; }
		public DateTime? NGAY_DUYET { get; set; }
		public Decimal? NAM_SAN_XUAT { get; set; }
		public DateTime? NGAY_NHAP { get; set; }
		public DateTime? NGAY_SU_DUNG { get; set; }
		public String GHI_CHU { get; set; }
		public Decimal? DON_VI_BO_PHAN_ID { get; set; }
		public Decimal? DON_VI_ID { get; set; }
		public DateTime? NGAY_TAO { get; set; }
		public Decimal? NGUOI_TAO_ID { get; set; }
		public Guid GUID { get; set; }
		public String CHUNG_TU_SO { get; set; }
		public DateTime? CHUNG_TU_NGAY { get; set; }
		public decimal? NGUYEN_GIA_BAN_DAU { get; set; }
		public decimal? PHAN_LOAI_TAI_SAN { get; set; }
		public bool? LA_TS_HET_HM_CO_THAY_DOI_NG { get; set; }
		public decimal? SO_NAM_SD_CON_LAI { get; set; }
		public decimal? GIA_MUA_TIEP_NHAN { get; set; }
		public DateTime? NGAY_CAP_NHAT { get; set; }
		public String MA_DB { get; set; }
		public bool? IS_XAC_NHAN { get; set; }
		public DateTime? NGAY_XAC_NHAN { get; set; }
		public bool? IS_MIEN_THUE { get; set; }
		public decimal? GIA_HOA_DON { get; set; }
		public decimal? MIEN_THUE_SO_TIEN { get; set; }
		public decimal? HM_SO_NAM_CON_LAI { get; set; }
		public decimal? HM_TY_LE { get; set; }
		public bool? IS_DUYET { get; set; }
		public String MA_BO { get; set; }
		//add more
		public virtual ICollection<YeuCau> YeuCaus { get; set; }
		public virtual ICollection<BienDong> BienDongs { get; set; }
		public virtual LoaiTaiSan loaitaisan { get; set; }
		public virtual LoaiTaiSanDonVi loaitaisandonvi { get; set; }
		public virtual NguoiDung nguoidung { get; set; }
		public virtual DonVi donvi { get; set; }
		public virtual LyDoBienDong lydobiendong { get; set; }
		public virtual QuocGia nuocsanxuat { get; set; }
		public virtual DonViBoPhan donvibophan { get; set; }
        //Thêm Phương Thức mua sắm
        public decimal? PHUONG_THUC_MUA_SAM_ID { get; set; }
        public decimal? DON_VI_MUA_SAM_TAP_TRUNG_ID { get; set; }
        public enumTRANG_THAI_TAI_SAN TrangThaiTaiSan
		{
			get => (enumTRANG_THAI_TAI_SAN)TRANG_THAI_ID;
			set => TRANG_THAI_ID = (int)value;
		}

		public enumLOAI_HINH_TAI_SAN enumLoaiHinhTaiSan
		{
			get => (enumLOAI_HINH_TAI_SAN)LOAI_HINH_TAI_SAN_ID;
			set => LOAI_HINH_TAI_SAN_ID = (int)value;
		}

		public Decimal NGUON_TAI_SAN_ID { get; set; }
		public string MA_QLDKTS40 { get; set; }
		public int? SO_LUONG_HAO_MON_TAI_SAN { get; set; }

    }

	public enum enumNguonTaiSan
	{
		TAT_CA = -1,
		QLTSC = 0,
		DKTS40 = 1,
		CSDLQGTSC = 2,
		KHAC = 3,
		QLTSNN = 4,
		CTNS = 5,
		HTGTDB = 6,
		KTNN = 7,
		TSNoiNganh = 8,
	}
    public partial class CountView : BaseViewEntity
	{
		public CountView()
		{
		}
        public int COUNT_ROW { get; set; }
    }
}