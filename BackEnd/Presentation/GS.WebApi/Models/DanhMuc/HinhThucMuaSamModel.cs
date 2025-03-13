using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.DanhMuc
{
    public class HinhThucMuaSamModel:BaseGSApiModel
    {
        [Required(ErrorMessage ="MA null")]
        public String MA { get; set; }
        [Required(ErrorMessage = "TEN null")]
        public String TEN { get; set; }
        [Required(ErrorMessage = "DB_ID null")]
        public int? DB_ID { get; set; }
    }
}
