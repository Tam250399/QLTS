using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.DanhMuc
{
    public class ChucDanhModel : BaseGSApiModel
    {
       [Required(ErrorMessage ="TEN_CHUC_DANH null")]
        public String TEN_CHUC_DANH { get; set; }
        public String MO_TA { get; set; }
        [Required(ErrorMessage = "MA_CHUC_DANH null")]
        public String MA_CHUC_DANH { get; set; }
        [Required(ErrorMessage = "KHOI_DON_VI_ID null")]
        public Decimal KHOI_DON_VI_ID { get; set; }
        public Decimal? DINH_MUC { get; set; }
        [Required(ErrorMessage = "DB_ID null")]
        public decimal? DB_ID { get; set; }
    }
}
