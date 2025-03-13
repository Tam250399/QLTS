using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.DanhMuc
{
    public class DonViBoPhanModel : BaseGSApiModel
    {
        //[Required(ErrorMessage ="DB_ID null")]
        public Decimal? DB_ID { get; set; }
        [Required(ErrorMessage = "MA null")]
        public String MA { get; set; }
        [Required(ErrorMessage = "TEN null")]
        public String TEN { get; set; }
        public String DIA_CHI { get; set; }
        public String DIEN_THOAI { get; set; }
        public String FAX { get; set; }
        [Required(ErrorMessage = "DON_VI_ID null")]
        public Decimal? DON_VI_ID { get; set; }
        public Decimal? PARENT_ID { get; set; }
        public String TREE_NODE { get; set; }
        public Decimal? TREE_LEVEL { get; set; }
        //tạm thời thêm vào để đồng bộ tài sản nhà nước
        public String DB_GUID { get; set; }
        public string Error { get; set; }
        //add more

    }
}
