//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Models.NghiepVu;
using GS.Web.Validators.TaiSans;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.TaiSans
{
    [Validator(typeof(TaiSanDatValidator))]
    public class TaiSanDatModel : BaseGSEntityModel
    {
        public TaiSanDatModel()
        {
            AvailableQuocGia = new List<SelectListItem>();
            AvailableTinh = new List<SelectListItem>();
            AvailableHuyen = new List<SelectListItem>();
            AvailableXa = new List<SelectListItem>();
            lstHienTrang = new List<ObjHienTrang>();
        }
        public Decimal TAI_SAN_ID { get; set; }
        public String DIA_CHI { get; set; }
        public Decimal? DIA_BAN_ID { get; set; }
        [UIHint("InputAddon")]
        public Decimal DIEN_TICH { get; set; }
        [UIHint("InputAddon")]
        public Decimal? DIEN_TICH_XAY_NHA { get; set; }
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
        public String HS_PHAP_LY_KHAC { get; set; }
        [UIHint("DateNullable")]
        public DateTime? HS_PHAP_LY_KHAC_NGAY { get; set; }
		public String HS_KHAC { get; set; }
        // add more 
        public string TaiSanMa { get; set; }
        public string TenDat { get; set; }
        public int QuocGiaId { get; set; }
        public int TinhId { get; set; }
        public int HuyenId { get; set; }
        public int XaId { get; set; }
        public IList<SelectListItem> AvailableQuocGia { get; set; }
        public IList<SelectListItem> AvailableTinh { get; set; }
        public IList<SelectListItem> AvailableHuyen { get; set; }
        public IList<SelectListItem> AvailableXa { get; set; }
        [UIHint("InputAddon")]
        public Decimal? DAT_DIEN_TICH { get; set; }
        public IList<ObjHienTrang> lstHienTrang { get; set; }
        public bool cohoso { get; set; }
		public IList<TaiSanModel> ListTaiSanNhaTrenDat { get; set; } //use show in detail
        public TaiSanModel TaiSanModel { get; set; }
        public YeuCauChiTietModel YeuCauChiTietModel { get; set; }
        public YeuCauModel  YeuCauModel { get; set; }
	}
    public partial class TaiSanDatSearchModel : BaseSearchModel
    {
        public TaiSanDatSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class TaiSanDatListModel : BasePagedListModel<TaiSanDatModel>
    {

    }
}

