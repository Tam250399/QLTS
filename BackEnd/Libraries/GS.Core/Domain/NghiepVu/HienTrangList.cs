using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.NghiepVu
{
    public partial class ObjHienTrang_Entity
    {
        public decimal? HienTrangId { get; set; }
        public String GiaTriText { get; set; }
        public Decimal? GiaTriNumber { get; set; }
        public bool GiaTriCheckbox { get; set; }
        public string TenHienTrang { get; set; }
        public Decimal? KieuDuLieuId { get; set; }
        public Decimal? NhomHienTrangId { get; set; }
    }
    public partial class HienTrangList_Entity
    {
        public decimal? TaiSanId { get; set; }
        public IList<ObjHienTrang_Entity> lstObjHienTrang { get; set; }
    }
}
