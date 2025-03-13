using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.WebApi.Validator.TaiSan;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.TaiSan
{
    [Validator(typeof(LogsDongBoCsdlqgValidator))]
    public class LogsDongBoCsdlqgModel : BaseGSEntityModel
    {
        [Required(ErrorMessage = ("UUID is null"))]
        public String UUID { get; set; }
        [Required(ErrorMessage = ("MA_QLTSC is null"))]
        public String MA_QLTSC { get; set; }
        public String MA_CSDLQG { get; set; }
        public String MO_TA { get; set; }
    }
}
