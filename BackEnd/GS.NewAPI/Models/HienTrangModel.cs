using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace GS.NewAPI.Models
{
    public class HienTrangModel
    {
    }

    public partial class ObjHienTrang
    {
        public decimal? HienTrangId { get; set; }
        public String GiaTriText { get; set; }
        [UIHint("InputAddon")]
        public Decimal? GiaTriNumber { get; set; }
        public Boolean? GiaTriCheckbox { get; set; }
        public string TenHienTrang { get; set; }
        public Decimal? KieuDuLieuId { get; set; }
        public Decimal? NhomHienTrangId { get; set; }
        public decimal? DonViId { get; set; }
        public bool IsOpenAll { get; set; }
    }

    public partial class HienTrangList
    {
        public decimal? TaiSanId { get; set; }
        public IList<ObjHienTrang> lstObjHienTrang { get; set; }
    }
}
