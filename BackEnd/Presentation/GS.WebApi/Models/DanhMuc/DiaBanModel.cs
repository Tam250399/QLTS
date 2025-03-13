using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.DanhMuc
{
    public class DiaBanModel : BaseGSApiModel
    {
        public String MA { get; set; }
        public String TEN { get; set; }
        public String MA_CHA { get; set; }
        public String MO_TA { get; set; }
        public decimal? PARENT_ID { get; set; }
        public decimal? TREE_LEVEL { get; set; }
        public decimal? QUOC_GIA_ID { get; set; }
        public decimal? TRANG_THAI_ID { get; set; }
        public decimal? LOAI_DIA_BAN_ID { get; set; }
        public decimal? DB_ID { get; set; }
        public string Error { get; set; }
    }
    public enum LoaiDiaBan
    {
        tinh=1,
        Huyen=2,
        xa=3
    }
}
