//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.NghiepVu;
using GS.Web.Validators.TaiSans;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.TaiSans
{
    [Validator(typeof(TaiSanNhaValidator))]
    public class TaiSanNhaModel : BaseGSEntityModel
    {
        public TaiSanNhaModel()
        {
            //TaiSanDatModel = new TaiSanDatModel();
            AvailableQuocGia = new List<SelectListItem>();
            AvailableTinh = new List<SelectListItem>();
            AvailableHuyen = new List<SelectListItem>();
            AvailableXa = new List<SelectListItem>();
            AvailableLoaiTaiSan = new List<SelectListItem>();
            ListHienTrangModel = new List<HienTrangModel>();
            lstHienTrang = new List<ObjHienTrang>();
        }
        public Decimal TAI_SAN_ID { get; set; }
        [UIHint("InputAddon")]
        public Decimal? TAI_SAN_DAT_ID { get; set; }
        [UIHint("InputAddon")]
        public Decimal? DIEN_TICH_XAY_DUNG { get; set; }
        [UIHint("InputAddon")]
        public Decimal? DIEN_TICH_SAN_XAY_DUNG { get; set; }
        [UIHint("InputYear")]
        public Decimal? NAM_XAY_DUNG { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_SU_DUNG { get; set; }
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
        //add more
        // truong chua cap nha khi them nha kem dat
        public decimal? NHA_LOAI_TAI_SAN_ID { get; set; }
        public int NHA_QuocGiaId { get; set; }
        public decimal? NHA_TinhId { get; set; }
        public decimal? NHA_HuyenId { get; set; }
        public decimal? NHA_XaId { get; set; }
        public string TinhString { get; set; }
        public string HuyenString { get; set; }
        public string XaString { get; set; }
        public bool isQuanLyDat { get; set; }
        public String NHA_DIA_CHI { get; set; }
        public String DIA_CHI { get; set; }
        // add thêm thông tin mã địa bàn  để lưu địa chỉ tỉnh, huyện, xã
        public Decimal? DIA_BAN_ID { get; set; }
        //add more
        //thông tin tài sản đất dc gán
        public String MaTaiSanDat { get; set; }
		public String TenTaiSanDat { get; set; }
		public String MaTaiSan { get; set; }
		public String TenTaiSan { get; set; }
		public String TenLoaiTaiSan { get; set; }
		public String strNguyenGiaVN { get; set; }
		public String strDienTichSanVN { get; set; }
		public Decimal? TRANG_THAI_TAI_SAN_ID { get; set; }
		[UIHint("InputAddon")]
        public decimal? NHA_SO_TANG { get; set; }
        public IList<ObjHienTrang> lstHienTrang { get; set; }
        public decimal CHE_DO_HACH_TOAN_ID { get; set; }
        public bool TRANG_THAI_KH { get; set; }
        public TaiSanModel TaiSanModel { get; set; }
        public YeuCauChiTietModel NVYeuCauChiTietModel { get; set; }
        public LoaiTaiSanModel LoaiTaiSanModel { get; set; }
        public TaiSanDatModel TaiSanDatModel { get; set; }
        public IList<HienTrangModel> ListHienTrangModel { get; set; }
        public IList<SelectListItem> AvailableLoaiTaiSan { get; set; }
        public IList<SelectListItem> AvailableQuocGia { get; set; }
        public IList<SelectListItem> AvailableTinh { get; set; }
        public IList<SelectListItem> AvailableHuyen { get; set; }
        public IList<SelectListItem> AvailableXa { get; set; }
        // Để ẩn hiện một số trường khi thay đổi địa bàn
        public bool IsEditDiaBan { get; set; }

    }
    public partial class TaiSanNhaSearchModel : BaseSearchModel
    {
        public TaiSanNhaSearchModel()
        {
        }
        public string KeySearch { get; set; }
		public Decimal? TAI_SAN_DAT_ID { get; set; }
		public bool? isTangMoi { get; set; }
	}
    public partial class TaiSanNhaListModel : BasePagedListModel<TaiSanNhaModel>
    {

    }
}

