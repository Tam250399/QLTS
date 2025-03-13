using GS.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.BaoCaos.TSTD
{
    public class BaoCaoHinhThucXuLyTSTDModel : BaseGSEntityModel
    {
    }
    public class BaoCaoHinhThucXuLyTSTDSearchModel
    {
        public BaoCaoHinhThucXuLyTSTDSearchModel()
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
        public int DonVi { get; set; }
        public int LoaiXuLyId { get; set; }
        public int DonViTien { get; set; }
        public int DonViDienTich { get; set; }
        public List<SelectListItem> DDLDonVi { get; set; }
        public List<SelectListItem> DDLLoaiTaiSan { get; set; }
        public List<SelectListItem> DDLDonViTien { get; set; }
        public List<SelectListItem> DDLDonViDienTich { get; set; }
    }
}
