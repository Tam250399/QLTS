using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.BaoCaos.TS_CDKT
{
    public class KiemKeTaiSanModel
    {
    }

    public class KiemKeTaiSanSearchModel
    {
        public KiemKeTaiSanSearchModel()
        {
            DDLDonViBoPhan = new List<SelectListItem>();
            DDLNhomTaiSan = new List<SelectListItem>();
        }
        [UIHint("Date")]
        public DateTime NgayBaoCaoFrom { get; set; }
        [UIHint("Date")]
        public DateTime NgayBaoCaoTo { get; set; }
        public Decimal DonViBoPhan { get; set; }
        public Decimal NhomTaiSan { get; set; }
        public List<SelectListItem> DDLDonViBoPhan { get; set; }
        public List<SelectListItem> DDLNhomTaiSan { get; set; }
    }
}
