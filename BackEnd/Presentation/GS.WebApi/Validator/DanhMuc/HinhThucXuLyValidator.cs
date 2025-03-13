using FluentValidation;
using GS.Web.Framework.Validators;
using GS.WebApi.Factories;
using GS.WebApi.Models.DanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Validator.DanhMuc
{
    public class HinhThucXuLyValidator :  BaseGSValidator<HinhThucXuLyModel>
    {
        public HinhThucXuLyValidator(IValidateFactory validateFactory)
        {
            RuleFor(x => x.PHUONG_AN_XU_LY_ID).Must((model,code) => {
                return validateFactory.CheckPhuongAnXuLy(model.PHUONG_AN_XU_LY_ID);
            }).WithMessage("PHUONG_AN_XU_LY_ID không tồn tại");
        }
      
    }
}
