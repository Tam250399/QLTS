
using GS.Core.Configuration;
using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.NewAPI.Models.DanhMuc
{
    public class QuocGiaModel : BaseGSApiModel
    {
        public String MA { get; set; }
        public String TEN { get; set; }
        public String MO_TA { get; set; }
        public int? DB_ID { get; set; }
        public string TrangThaiDongBo { get; set; }
        public string Error { get; set; }
    }
}
