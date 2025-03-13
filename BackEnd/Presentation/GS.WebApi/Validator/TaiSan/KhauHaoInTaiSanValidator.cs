using FluentValidation;
using GS.Web.Framework.Validators;
using GS.WebApi.Factories;
using GS.WebApi.Models.TaiSan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Validator.TaiSan
{

    public class KhauHaoInTaiSanValidator : BaseGSValidator<KhauHaoInTaiSanDBModel> 
    {
        public KhauHaoInTaiSanValidator(IValidateFactory validateFactory)
        {
            RuleFor(m => m).Custom((model, context) =>
            {
                var result = validateFactory.CheckChiTietKhauHaoInTaiSan(model);
                if (result.Count > 0)
                {
                    context.AddFailure(string.Join("; ", result));
                }
            });
            RuleFor(m => m.THANG_KHAU_HAO).Must((model, code) =>
            {
                if(!(model.THANG_KHAU_HAO>=1 && (model.THANG_KHAU_HAO <= 12)))
                    {
                    return false;
                }
                else
                {
                    return true;
                }
            }).WithMessage("THANG_KHAU_HAO Không tồn tại");            
        }
    }
}
