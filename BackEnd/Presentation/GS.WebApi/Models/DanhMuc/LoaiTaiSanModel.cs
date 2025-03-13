using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.DanhMuc
{

    public class LoaiTaiSanModel : BaseGSApiModel
    {
        public int? DB_ID { get; set; }
        public String MA { get; set; }
        public String TEN { get; set; }
        public String DON_VI { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }      
        public String MO_TA { get; set; }
        public int? DB_CHE_DO_HAO_MON_ID { get; set; }
        public int? DB_PARENT_ID { get; set; }
        public decimal? PARENT_ID { get; set; }    
        public String MA_NHOM_TAI_SAN { get; set; }
        public string TrangThaiDongBo { get; set; }
        public decimal? CHE_DO_HAO_MON_ID { get; set; }
        public string Error { get; set; }
        public string DON_VI_TINH { get; set; }
        public Decimal? HM_TY_LE { get; set; }
        public Decimal? HM_THOI_HAN_SU_DUNG { get; set; }
    }

}
