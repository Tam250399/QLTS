using GS.Core.Domain.BienDongs;
using GS.Core.Domain.DanhMuc;
using GS.Web.Framework.Models;
using GS.Web.Models.NghiepVu;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.BienDongs
{
    public class TrungGianBDYCChiTietModel : BaseGSEntityModel
    {
        public TrungGianBDYCChiTietModel()
        {
            HinhThucMuaSamAvailable = new List<SelectListItem>();
            MucDichDuocGiaoAvailable = new List<SelectListItem>();
            BoPhanSuDungAvailable = new List<SelectListItem>();
            lstHienTrang = new List<ObjHienTrang>();
        }
        public enumBDorYC EnumBDorYC
        {
            get => (enumBDorYC)BDorYC;
        }
        public int BDorYC { get; set; }
        public Decimal YEU_CAU_ID { get; set; }
        public Decimal? HINH_THUC_MUA_SAM_ID { get; set; }
        public Decimal? MUC_DICH_SU_DUNG_ID { get; set; }
        public String NHAN_HIEU { get; set; }
        public String SO_HIEU { get; set; }
        public Decimal? DIA_BAN_ID { get; set; }
        public String DATA_JSON { get; set; }
        [UIHint("InputAddon")]
        public Decimal? NGUYEN_GIA { get; set; }
        [UIHint("InputAddon")]
        public Decimal? DAT_TONG_DIEN_TICH { get; set; }
        public Decimal? DAT_GIA_TRI_QUYEN_SD_DAT { get; set; }
        public Decimal? HTSD_QUAN_LY_NHA_NUOC { get; set; }
        public Decimal? HTSD_HDSN_KINH_DOANH_KHONG { get; set; }
        public Decimal? HTSD_HDSN_KINH_DOANH { get; set; }
        public Decimal? HTSD_CHO_THUE { get; set; }
        public Decimal? HTSD_LIEN_DOANH { get; set; }
        public Decimal? HTSD_SU_DUNG_HON_HOP { get; set; }
        public Decimal? HTSD_SU_DUNG_KHAC { get; set; }
        [UIHint("InputAddon")]
        public Decimal? HM_SO_NAM_CON_LAI { get; set; }
        [UIHint("InputAddon")]
        public Decimal? HM_TY_LE_HAO_MON { get; set; }
        [UIHint("InputAddon")]
        public Decimal? HM_LUY_KE { get; set; }
        [UIHint("InputAddon")]
        public Decimal? HM_GIA_TRI_CON_LAI { get; set; }
        [UIHint("DateNullable")]
        public DateTime? KH_NGAY_BAT_DAU { get; set; }
        [UIHint("InputAddon")]
        public Decimal KH_THANG_THEO_QD { get; set; }
        [UIHint("InputAddon")]
        public Decimal? KH_THANG_CON_LAI { get; set; }
        [UIHint("InputAddon")]
        public Decimal? KH_GIA_TINH_KHAU_HAO { get; set; }
        [UIHint("InputAddon")]
        public Decimal? KH_GIA_TRI_TRICH_THANG { get; set; }
        [UIHint("InputAddon")]
        public Decimal? KH_LUY_KE { get; set; }
        [UIHint("InputAddon")]
        public Decimal? KH_CON_LAI { get; set; }
        [UIHint("InputAddon")]
        public Decimal? NHA_SO_TANG { get; set; }
        [UIHint("InputYear")]
        public Decimal? NHA_NAM_XAY_DUNG { get; set; }
        [UIHint("InputAddon")]
        public Decimal? NHA_DIEN_TICH_XD { get; set; }
        [UIHint("InputAddon")]
        public Decimal? NHA_TONG_DIEN_TICH_XD { get; set; }
        [UIHint("InputAddon")]
        public Decimal? VKT_DIEN_TICH { get; set; }
        public Decimal? VKT_THE_TICH { get; set; }
        public Decimal? VKT_CHIEU_DAI { get; set; }
        public String OTO_BIEN_KIEM_SOAT { get; set; }
        public Decimal? OTO_SO_CHO_NGOI { get; set; }
        public Decimal? OTO_SO_CAU_XE { get; set; }
        public decimal? OTO_CHUC_DANH_ID { get; set; }
        public Decimal? OTO_NHAN_XE_ID { get; set; }
        public Decimal? OTO_TAI_TRONG { get; set; }
        public Decimal? OTO_CONG_XUAT { get; set; }
        public Decimal? OTO_XI_LANH { get; set; }
        public String OTO_SO_KHUNG { get; set; }
        public String OTO_SO_MAY { get; set; }
        public String THONG_SO_KY_THUAT { get; set; }
        public Decimal? CLN_SO_NAM { get; set; }
        public String HTSD_JSON { get; set; }
        [UIHint("InputAddon")]
        public Decimal? PHI_THU { get; set; }
        [UIHint("InputAddon")]
        public Decimal? PHI_BU_DAP { get; set; }
        [UIHint("InputAddon")]
        public Decimal? PHI_NOP_NGAN_SACH { get; set; }
        [UIHint("InputAddon")]
        public Decimal? KH_TY_LE_NGUYEN_GIA_KHAU_HAO { get; set; }
        [UIHint("InputAddon")]
        public Decimal? PHI_KHAC { get; set; }
        public Decimal? DON_VI_NHAN_DIEU_CHUYEN_ID { get; set; }
        public Decimal? HINH_THUC_XU_LY_ID { get; set; }
        public Boolean IS_BAN_THANH_LY { get; set; }
        public Boolean DIEU_CHUYEN_KEM_THEO { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_BAN_THANH_LY { get; set; }
        //add more 
        public IList<SelectListItem> HinhThucMuaSamAvailable { get; set; }
        public IList<SelectListItem> MucDichDuocGiaoAvailable { get; set; }
        public IList<SelectListItem> BoPhanSuDungAvailable { get; set; }
        public Decimal? HM_GIA_TRI_TINH { get; set; }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        [UIHint("InputAddon")]
        public Decimal? TS_GIA_TRI_HIEN_TAI { get; set; }
        public enumPHUONG_PHAP_TINH_KHAU_HAO enumPhuongPhapTinhKhauHao { get; set; }
        public SelectList PhuongPhapTinhKhauHaoAvailable { get; set; }

        public Decimal? PHUONG_PHAP_TINH_KHAU_HAO_ID { get; set; }
        public IList<ObjHienTrang> lstHienTrang { get; set; }
        public Decimal? HM_THOI_HAN_SU_DUNG { get; set; }
        public Decimal? KH_THOI_HAN_SU_DUNG { get; set; }
        public SelectList ddlPhuongAnXuLy { get; set; }
        public enumHINH_THUC_XU_LY_TSC enumPhuongAnXuLy { get; set; }
        //properties phuc vu bien dong
        [UIHint("InputAddon")]
        public Decimal? NGUYEN_GIA_SAU_BD { get; set; }
        [UIHint("InputAddon")]
        public Decimal? NHA_DIEN_TICH_XD_SAU_BD { get; set; }
        [UIHint("InputAddon")]
        public Decimal? NHA_TONG_DIEN_TICH_XD_SAU_BD { get; set; }
        [UIHint("InputAddon")]
        public Decimal? DAT_TONG_DIEN_TICH_SAU_BD { get; set; }
        [UIHint("InputAddon")]
        public Decimal? VKT_CHIEU_DAI_CU { get; set; }
        [UIHint("InputAddon")]
        public Decimal? VKT_DIEN_TICH_CU { get; set; }
        [UIHint("InputAddon")]
        public Decimal? VKT_THE_TICH_CU { get; set; }
        [UIHint("InputAddon")]
        public Decimal? VKT_CHIEU_DAI_SAU_BD { get; set; }
        [UIHint("InputAddon")]
        public Decimal? VKT_DIEN_TICH_SAU_BD { get; set; }
        [UIHint("InputAddon")]
        public Decimal? VKT_THE_TICH_SAU_BD { get; set; }
        [UIHint("InputAddon")]
        public Decimal? NGUYEN_GIA_CU { get; set; }
        [UIHint("InputAddon")]
        public Decimal? HM_GIA_TRI_CON_LAI_CU { get; set; }
        [UIHint("InputAddon")]
        public Decimal? NHA_DIEN_TICH_XD_CU { get; set; }
        [UIHint("InputAddon")]
        public Decimal? NHA_TONG_DIEN_TICH_XD_CU { get; set; }
        [UIHint("InputAddon")]
        public Decimal? DAT_TONG_DIEN_TICH_CU { get; set; }
        //
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

        public String HS_HOP_DONG_CHO_THUE_SO { get; set; }
        [UIHint("DateNullable")]
        public DateTime? HS_HOP_DONG_CHO_THUE_NGAY { get; set; }
        public String HS_KHAC { get; set; }

        public String DIA_CHI { get; set; }
        public string DonViNhanDieuChuyenTen { get; set; }
        public string OtoTenChucDanh { get; set; }


    }
    public partial class TrungGianBDYCChiTietSearchModel : BaseSearchModel
    {
        public TrungGianBDYCChiTietSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class TrungGianBDYCChiTietListModel : BasePagedListModel<TrungGianBDYCChiTietModel>
    {

    }
}
