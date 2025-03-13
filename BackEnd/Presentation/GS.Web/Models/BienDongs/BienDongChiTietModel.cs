//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Models.NghiepVu;
using GS.Web.Validators.BienDongs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.BienDongs
{
    [Validator(typeof(BienDongChiTietValidator))]
    public class BienDongChiTietModel : BaseGSEntityModel
    {
        public BienDongChiTietModel()
        {
            lstHienTrang = new List<ObjHienTrang>();
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
        public Decimal? NHA_SO_TANG { get; set; }
        public Decimal? NHA_NAM_XAY_DUNG { get; set; }
        public Decimal? NHA_DIEN_TICH_XD { get; set; }
        public Decimal? NHA_TONG_DIEN_TICH_XD { get; set; }
        public Decimal? VKT_DIEN_TICH { get; set; }
        public Decimal? VKT_THE_TICH { get; set; }
        public Decimal? VKT_CHIEU_DAI { get; set; }
        public String OTO_BIEN_KIEM_SOAT { get; set; }
        public Decimal? OTO_SO_CHO_NGOI { get; set; }
        public Decimal? OTO_SO_CAU_XE { get; set; }
        public decimal? OTO_CHUC_DANH_ID { get; set; }
        //Thêm vào để xử lý yêu cầu chọn nhiều chức danh 1 lúc -- hungnt
        public string OTO_LIST_CHUC_DANH_ID { get; set; }
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
        public Decimal? HINH_THUC_XU_LY_ID { get; set; }
        public Boolean? IS_BAN_THANH_LY { get; set; }
        public Boolean? DIEU_CHUYEN_KEM_THEO { get; set; }
        public DateTime? NGAY_BAN_THANH_LY { get; set; }
        //Ho so, giay to
        public String HS_QUYET_DINH_BAN_GIAO { get; set; }
		[UIHint("DateNullable")]
		public DateTime? HS_QUYET_DINH_BAN_GIAO_NGAY { get; set; }
		public String HS_BIEN_BAN_NGHIEM_THU { get; set; }
		[UIHint("DateNullable")]
		public DateTime? HS_BIEN_BAN_NGHIEM_THU_NGAY { get; set; }
		public String HS_PHAP_LY_KHAC { get; set; }
		[UIHint("DateNullable")]
		public DateTime? HS_PHAP_LY_KHAC_NGAY { get; set; }
		public String HS_CNQSD_SO { get; set; }
		[UIHint("DateNullable")]
		public DateTime? HS_CNQSD_NGAY { get; set; }
		public String HS_QUYET_DINH_GIAO_SO { get; set; }
		[UIHint("DateNullable")]
		public DateTime? HS_QUYET_DINH_GIAO_NGAY { get; set; }
		public String HS_CHUYEN_NHUONG_SD_SO { get; set; }
		[UIHint("DateNullable")]
		public DateTime? HS_CHUYEN_NHUONG_SD_NGAY { get; set; }
		public String HS_QUYET_DINH_CHO_THUE_SO { get; set; }
		[UIHint("DateNullable")]
		public DateTime? HS_QUYET_DINH_CHO_THUE_NGAY { get; set; }
		public String HS_KHAC { get; set; }
		public String DIA_CHI { get; set; }
        public string NHA_DIA_CHI { get; set; }
        public Decimal? KH_TY_LE_NGUYEN_GIA_KHAU_HAO { get; set; }
        public IList<ObjHienTrang> lstHienTrang { get; set; }
        public Decimal? DON_VI_NHAN_DIEU_CHUYEN_ID { get; set; }
    }
    public partial class BienDongChiTietSearchModel : BaseSearchModel
    {
        public BienDongChiTietSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class BienDongChiTietListModel : BasePagedListModel<BienDongChiTietModel>
    {

    }
}

