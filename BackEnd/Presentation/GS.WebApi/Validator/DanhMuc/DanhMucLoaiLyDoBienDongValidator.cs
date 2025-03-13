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
    public class DanhMucLoaiLyDoBienDongValidator:BaseGSValidator<LoaiLyDoBienDongModel>
    {
        public DanhMucLoaiLyDoBienDongValidator(IValidateFactory validateFactory)
        {
            RuleFor(m => m).Custom((model, context) =>
            {
                var result = validateFactory.CheckLoaiLyDoBienDong(model);
                if (!string.IsNullOrEmpty(result))
                {
                    context.AddFailure(result);
                }
            });
        }
    }
}
