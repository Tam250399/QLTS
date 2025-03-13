using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.DanhMuc
{
    public class CheDoHaoMonModel: BaseGSApiModel
    {
        public string MA_CHE_DO { get; set; }
        public string TEN_CHE_DO { get; set; }
        public string TU_NGAY { get; set; }
        public string DEN_NGAY { get; set; }
        public int? DB_ID { get; set; }
        public string Error { get; set; }
        public string TrangThaiDongBo { get; set; }
    }
}
