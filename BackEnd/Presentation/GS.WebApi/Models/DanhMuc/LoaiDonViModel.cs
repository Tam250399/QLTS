using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.DanhMuc
{
    public class LoaiDonViModel: BaseGSApiModel
    {
        public String MA { get; set; }
        public String TEN { get; set; }    
        public String MA_CHA { get; set; }
        public Decimal? CHE_DO_HACH_TOAN_ID { get; set; }
        public int? TYPE { get; set; }
        public int? DB_ID { get; set; }
        public int? DB_PARENT_ID { get; set; }
        public string Error { get; set; }
        public string TrangThaiDongBo { get; set; }
        public decimal? PARENT_ID { get; set; }
        public decimal? SAP_XEP { get; set; }
    }
}
