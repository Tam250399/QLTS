using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.DanhMuc
{
    public class NguonGocTaiSanModel : BaseGSApiModel
    {
        public String MA { get; set; }
        public String TEN { get; set; }
        public int? DB_ID { get; set; }
        public Decimal? PARENT_ID { get; set; }
        public int? DB_PARENT_ID { get; set; }
        public string TrangThaiDongBo { get; set; }
        public string Error { get; set; }
    }
}
