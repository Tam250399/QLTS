using GS.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.BaoCaos.TSTD
{
    public enum enumDonViTienTSTD
    {
        NghinDong = 1,
        TrieuDong = 2,
        TyDong = 3
    }
    public enum enumDonViDienTichTSTD
    {
        Met = 1,
        NghinMet = 2,
        MuoiNghinMet = 3
    }
    public class BaoCaoThongTinTSTDModel : BaseGSEntityModel
    {
        public int LOAI_TAI_SAN { get; set; }
        public string SO_QUYET_DINH_TT { get; set; }
        public DateTime NGAY_QUYET_DINH_TT { get; set; }
        public string DON_VI_TT { get; set; }
        public string SO_QUYET_DINH_XL { get; set; }
        public DateTime NGAY_QUYET_DINH_XL { get; set; }
        public string DON_VI_XL { get; set; }
        public int SO_LUONG { get; set; }
        public int NGUYEN_GIA { get; set; }
        public int GIA_TRI_CON_LAI { get; set; }
        public int PHUONG_AN_XU_LY { get; set; }
        public string GHI_CHU { get; set; }
    }
    public class BaoCaoThongTinTSTDSearchModel
    {
        public BaoCaoThongTinTSTDSearchModel()
        {
            DDLDonVi = new List<SelectListItem>();
            DDLLoaiTaiSan = new List<SelectListItem>();
            DDLDonViTien = new List<SelectListItem>();
            DDLDonViDienTich = new List<SelectListItem>();
        }
        [UIHint("DateNullable")]
        public DateTime? NgayBatDau { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NgayKetThuc { get; set; }
        public Decimal DonVi { get; set; }
        public Decimal LoaiTaiSan { get; set; }
        public int DonViTien { get; set; }
        public int DonViDienTich { get; set; }
        public List<SelectListItem> DDLDonVi { get; set; }
        public List<SelectListItem> DDLLoaiTaiSan { get; set; }
        public List<SelectListItem> DDLDonViTien { get; set; }
        public List<SelectListItem> DDLDonViDienTich { get; set; }
    }
}
