using GS.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.BaoCaos.CCDC
{
    public class CCDCLietKeReport
    {
        
    }
    public class LietKeCCDCModelSearchModel
    {
        public LietKeCCDCModelSearchModel()
        {
            DDLDonViBoPhan = new List<SelectListItem>();
            DDLNhomCongCu = new List<SelectListItem>();
            ListCongCu = new List<int>();
            ListDonViBoPhan = new List<int>();
        }
        [UIHint("DateNullable")]
        public DateTime? NgayBaoCao { get; set; }
        public Decimal DonViBoPhan { get; set; }
        public Decimal NhomCongCu { get; set; }
        public IList<int> ListDonViBoPhan { get; set; }
        public IList<int> ListCongCu { get; set; }
        public string ListStringDonVi { get; set; }
        public string ListStringCongCu { get; set; }
        public List<SelectListItem> DDLDonViBoPhan { get; set; }
        public List<SelectListItem> DDLNhomCongCu { get; set; }
    }
}
