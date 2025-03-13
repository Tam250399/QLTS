using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.DanhMuc
{
    public class MucDichSuDungModel: BaseGSApiModel
    {
        public String MA { get; set; }
        public String TEN { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }      
        public int? DB_ID { get; set; }
        public string TrangThaiDongBo { get; set; }       
        public string Error { get; set; }
    }
}
