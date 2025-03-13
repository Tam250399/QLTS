using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.DanhMuc
{
    public class DongXeModel: BaseGSApiModel
    {
        public String MA { get; set; }
        public String TEN { get; set; }
        public String MO_TA { get; set; }
        public String MA_NHAN_XE { get; set; }
        public int? DB_ID { get; set; }
        public decimal? NHAN_XE_ID { get; set; }
    }
}
