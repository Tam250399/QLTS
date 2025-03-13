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
    public class HaoMonValidator : BaseGSValidator<HaoMonDBModel>
    {
        public HaoMonValidator(IValidateFactory validateFactory)
        {
            RuleFor(m => m).Custom((model, context) =>
            {
                var result = validateFactory.CheckChiTietHaoMonTaiSan(model, false);
                if (result.Count > 0)
                {
                    context.AddFailure(string.Join("; ", result));
                }
            });
        }
    }
}
