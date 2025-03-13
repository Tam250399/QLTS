//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.NghiepVu;
using System;


namespace GS.Core.Domain.BienDongs
{
    public partial class BienDongChiTiet : BaseEntity
    {
        public BienDongChiTiet()
        {

        }
        public BienDongChiTiet(BienDongChiTiet obj)
        {
            this.BIEN_DONG_ID = obj.BIEN_DONG_ID;
            this.HINH_THUC_MUA_SAM_ID = obj.HINH_THUC_MUA_SAM_ID;
            this.MUC_DICH_SU_DUNG_ID = obj.MUC_DICH_SU_DUNG_ID;
            this.NHAN_HIEU = obj.NHAN_HIEU;
            this.SO_HIEU = obj.SO_HIEU;
            this.DIA_BAN_ID = obj.DIA_BAN_ID;
            this.DATA_JSON = obj.DATA_JSON;
            this.HTSD_JSON = obj.HTSD_JSON;
            this.NGUYEN_GIA = obj.NGUYEN_GIA;
            this.DAT_TONG_DIEN_TICH = obj.DAT_TONG_DIEN_TICH;
            this.HTSD_QUAN_LY_NHA_NUOC = obj.HTSD_QUAN_LY_NHA_NUOC;
            this.HTSD_HDSN_KINH_DOANH_KHONG = obj.HTSD_HDSN_KINH_DOANH_KHONG;
            this.HTSD_HDSN_KINH_DOANH = obj.HTSD_HDSN_KINH_DOANH;
            this.HTSD_CHO_THUE = obj.HTSD_CHO_THUE;
            this.HTSD_LIEN_DOANH = obj.HTSD_LIEN_DOANH;
            this.HTSD_SU_DUNG_HON_HOP = obj.HTSD_SU_DUNG_HON_HOP;
            this.HTSD_SU_DUNG_KHAC = obj.HTSD_SU_DUNG_KHAC;
            this.HM_SO_NAM_CON_LAI = obj.HM_SO_NAM_CON_LAI;
            this.HM_TY_LE_HAO_MON = obj.HM_TY_LE_HAO_MON;
            this.HM_LUY_KE = obj.HM_LUY_KE;
            this.HM_GIA_TRI_CON_LAI = obj.HM_GIA_TRI_CON_LAI;
            this.KH_NGAY_BAT_DAU = obj.KH_NGAY_BAT_DAU;
            this.KH_THANG_CON_LAI = obj.KH_THANG_CON_LAI;
            this.KH_GIA_TINH_KHAU_HAO = obj.KH_GIA_TINH_KHAU_HAO;
            this.KH_GIA_TRI_TRICH_THANG = obj.KH_GIA_TRI_TRICH_THANG;
            this.KH_LUY_KE = obj.KH_LUY_KE;
            this.KH_CON_LAI = obj.KH_CON_LAI;
            this.NHA_SO_TANG = obj.NHA_SO_TANG;
            this.NHA_NAM_XAY_DUNG = obj.NHA_NAM_XAY_DUNG;
            this.NHA_DIEN_TICH_XD = obj.NHA_DIEN_TICH_XD;
            this.NHA_TONG_DIEN_TICH_XD = obj.NHA_TONG_DIEN_TICH_XD;
            this.VKT_DIEN_TICH = obj.VKT_DIEN_TICH;
            this.VKT_THE_TICH = obj.VKT_THE_TICH;
            this.VKT_CHIEU_DAI = obj.VKT_CHIEU_DAI;
            this.OTO_BIEN_KIEM_SOAT = obj.OTO_BIEN_KIEM_SOAT;
            this.OTO_SO_CHO_NGOI = obj.OTO_SO_CHO_NGOI;
            this.OTO_SO_CAU_XE = obj.OTO_SO_CAU_XE;
            this.OTO_CHUC_DANH_ID = obj.OTO_CHUC_DANH_ID;
            this.OTO_LIST_CHUC_DANH_ID = obj.OTO_LIST_CHUC_DANH_ID;
            this.OTO_NHAN_XE_ID = obj.OTO_NHAN_XE_ID;
            this.OTO_TAI_TRONG = obj.OTO_TAI_TRONG;
            this.OTO_CONG_XUAT = obj.OTO_CONG_XUAT;
            this.OTO_XI_LANH = obj.OTO_XI_LANH;
            this.OTO_SO_KHUNG = obj.OTO_SO_KHUNG;
            this.OTO_SO_MAY = obj.OTO_SO_MAY;
            this.THONG_SO_KY_THUAT = obj.THONG_SO_KY_THUAT;
            this.CLN_SO_NAM = obj.CLN_SO_NAM;
            this.PHI_THU = obj.PHI_THU;
            this.PHI_BU_DAP = obj.PHI_BU_DAP;
            this.PHI_NOP_NGAN_SACH = obj.PHI_NOP_NGAN_SACH;
            this.PHI_KHAC = obj.PHI_KHAC;
            this.DON_VI_NHAN_DIEU_CHUYEN_ID = obj.DON_VI_NHAN_DIEU_CHUYEN_ID;
            this.HINH_THUC_XU_LY_ID = obj.HINH_THUC_XU_LY_ID;
            this.IS_BAN_THANH_LY = obj.IS_BAN_THANH_LY;
            this.HS_CNQSD_SO = obj.HS_CNQSD_SO;
            this.HS_CNQSD_NGAY = obj.HS_CNQSD_NGAY;
            this.HS_QUYET_DINH_GIAO_SO = obj.HS_QUYET_DINH_GIAO_SO;
            this.HS_QUYET_DINH_GIAO_NGAY = obj.HS_QUYET_DINH_GIAO_NGAY;
            this.HS_CHUYEN_NHUONG_SD_SO = obj.HS_CHUYEN_NHUONG_SD_SO;
            this.HS_CHUYEN_NHUONG_SD_NGAY = obj.HS_CHUYEN_NHUONG_SD_NGAY;
            this.HS_QUYET_DINH_CHO_THUE_SO = obj.HS_QUYET_DINH_CHO_THUE_SO;
            this.HS_QUYET_DINH_CHO_THUE_NGAY = obj.HS_QUYET_DINH_CHO_THUE_NGAY;
            this.HS_HOP_DONG_CHO_THUE_SO = obj.HS_HOP_DONG_CHO_THUE_SO;
            this.HS_HOP_DONG_CHO_THUE_NGAY = obj.HS_HOP_DONG_CHO_THUE_NGAY;
            this.HS_KHAC = obj.HS_KHAC;
            this.HS_QUYET_DINH_BAN_GIAO = obj.HS_QUYET_DINH_BAN_GIAO;
            this.HS_QUYET_DINH_BAN_GIAO_NGAY = obj.HS_QUYET_DINH_BAN_GIAO_NGAY;
            this.HS_BIEN_BAN_NGHIEM_THU = obj.HS_BIEN_BAN_NGHIEM_THU;
            this.HS_BIEN_BAN_NGHIEM_THU_NGAY = obj.HS_BIEN_BAN_NGHIEM_THU_NGAY;
            this.HS_PHAP_LY_KHAC = obj.HS_PHAP_LY_KHAC;
            this.HS_PHAP_LY_KHAC_NGAY = obj.HS_PHAP_LY_KHAC_NGAY;
            this.DIA_CHI = obj.DIA_CHI;
            this.NGAY_BAN_THANH_LY = NGAY_BAN_THANH_LY;
            this.TAI_SAN_TRUOC_DIEU_CHUYEN_ID = obj.TAI_SAN_TRUOC_DIEU_CHUYEN_ID;
            this.KH_TY_LE_NGUYEN_GIA_KHAU_HAO = obj.KH_TY_LE_NGUYEN_GIA_KHAU_HAO;
            this.DAT_GIA_TRI_QUYEN_SD_DAT = obj.DAT_GIA_TRI_QUYEN_SD_DAT;
            this.NHA_DIA_CHI = obj.NHA_DIA_CHI;
            
        }

        public Decimal BIEN_DONG_ID { get; set; }
        public Decimal? HINH_THUC_MUA_SAM_ID { get; set; }
        public Decimal? MUC_DICH_SU_DUNG_ID { get; set; }
        public String NHAN_HIEU { get; set; }
        public String SO_HIEU { get; set; }
        public Decimal? DIA_BAN_ID { get; set; }
        public String DATA_JSON { get; set; }
        public String HTSD_JSON { get; set; }
        public Decimal? NGUYEN_GIA { get; set; }
        public Decimal? DAT_TONG_DIEN_TICH { get; set; }
        public Decimal? HTSD_QUAN_LY_NHA_NUOC { get; set; }
        public Decimal? HTSD_HDSN_KINH_DOANH_KHONG { get; set; }
        public Decimal? HTSD_HDSN_KINH_DOANH { get; set; }
        public Decimal? HTSD_CHO_THUE { get; set; }
        public Decimal? HTSD_LIEN_DOANH { get; set; }
        public Decimal? HTSD_SU_DUNG_HON_HOP { get; set; }
        public Decimal? HTSD_SU_DUNG_KHAC { get; set; }
        public Decimal? HM_SO_NAM_CON_LAI { get; set; }
        public Decimal? HM_TY_LE_HAO_MON { get; set; }
        public Decimal? HM_LUY_KE { get; set; }
        public Decimal? HM_GIA_TRI_CON_LAI { get; set; }
        public DateTime? KH_NGAY_BAT_DAU { get; set; }
        public Decimal? KH_THANG_CON_LAI { get; set; }
        public Decimal? KH_GIA_TINH_KHAU_HAO { get; set; }
        public Decimal? KH_GIA_TRI_TRICH_THANG { get; set; }
        public Decimal? KH_LUY_KE { get; set; }
        public Decimal? KH_CON_LAI { get; set; }
        public Decimal? KH_TY_LE_NGUYEN_GIA_KHAU_HAO { get; set; }
        public Decimal? NHA_SO_TANG { get; set; }
        public Decimal? NHA_NAM_XAY_DUNG { get; set; }
        public Decimal? NHA_DIEN_TICH_XD { get; set; }
        public Decimal? NHA_TONG_DIEN_TICH_XD { get; set; }
        public Decimal? VKT_DIEN_TICH { get; set; }
        public Decimal? VKT_THE_TICH { get; set; }
        public Decimal? VKT_CHIEU_DAI { get; set; }
        public String OTO_BIEN_KIEM_SOAT { get; set; }
        public Decimal? OTO_SO_CHO_NGOI { get; set; }
		public decimal? OTO_SO_CAU_XE { get; set; }
		public Decimal? OTO_CHUC_DANH_ID { get; set; }
        //Thêm vào để xử lý yêu cầu chọn nhiều chức danh 1 lúc -- hungnt
        public string  OTO_LIST_CHUC_DANH_ID { get; set; }
        public Decimal? OTO_NHAN_XE_ID { get; set; }
        public Decimal? OTO_TAI_TRONG { get; set; }
        public Decimal? OTO_CONG_XUAT { get; set; }
        public Decimal? OTO_XI_LANH { get; set; }
        public String OTO_SO_KHUNG { get; set; }
        public String OTO_SO_MAY { get; set; }
        public String THONG_SO_KY_THUAT { get; set; }
        public Decimal? CLN_SO_NAM { get; set; }
        public Decimal? PHI_THU { get; set; }
        public Decimal? PHI_BU_DAP { get; set; }
        public Decimal? PHI_NOP_NGAN_SACH { get; set; }
        public Decimal? PHI_KHAC { get; set; }
        public Decimal? DON_VI_NHAN_DIEU_CHUYEN_ID { get; set; }
        public Decimal? HINH_THUC_XU_LY_ID { get; set; }
        public Boolean? IS_BAN_THANH_LY { get; set; }
        public Boolean? DIEU_CHUYEN_KEM_THEO { get; set; }
		public String HS_CNQSD_SO { get; set; }
		public DateTime? HS_CNQSD_NGAY { get; set; }
		public String HS_QUYET_DINH_GIAO_SO { get; set; }
		public DateTime? HS_QUYET_DINH_GIAO_NGAY { get; set; }
		public String HS_CHUYEN_NHUONG_SD_SO { get; set; }
		public DateTime? HS_CHUYEN_NHUONG_SD_NGAY { get; set; }
		public String HS_QUYET_DINH_CHO_THUE_SO { get; set; }
		public DateTime? HS_QUYET_DINH_CHO_THUE_NGAY { get; set; }
        public String HS_HOP_DONG_CHO_THUE_SO { get; set; }
        public DateTime? HS_HOP_DONG_CHO_THUE_NGAY { get; set; }
        public String HS_KHAC { get; set; }
		public String HS_QUYET_DINH_BAN_GIAO { get; set; }
		public DateTime? HS_QUYET_DINH_BAN_GIAO_NGAY { get; set; }
		public String HS_BIEN_BAN_NGHIEM_THU { get; set; }
		public DateTime? HS_BIEN_BAN_NGHIEM_THU_NGAY { get; set; }
		public String HS_PHAP_LY_KHAC { get; set; }
		public DateTime? HS_PHAP_LY_KHAC_NGAY { get; set; }
        public DateTime? NGAY_BAN_THANH_LY { get; set; }
        public String DIA_CHI { get; set; }
        public string NHA_DIA_CHI { get; set; }
        public Decimal? TAI_SAN_TRUOC_DIEU_CHUYEN_ID { get; set; }
        public Decimal? DAT_GIA_TRI_QUYEN_SD_DAT { get; set; }
        public virtual BienDong biendong { get; set; }
        public virtual HinhThucMuaSam hinhthucmuasam { get; set; }
        public virtual MucDichSuDung mucdichsudung { get; set; }
        public virtual DonVi donvinhandieuchuyen { get; set; }
    }
}



