using GS.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using GS.Web.Framework.Models;

namespace GS.NewAPI.Models
{
    public class TaiSanDatModel : BaseGSApiModel
    {
        public new decimal? ID { get; set; }
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
        //public IList<ObjHienTrang> lstHienTrang { get; set; }
        public bool cohoso { get; set; }
        public IList<TaiSanModel> ListTaiSanNhaTrenDat { get; set; } //use show in detail
        public TaiSanModel TaiSanModel { get; set; }
    }
}
