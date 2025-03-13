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
    public class LyDoBienDongValidator : BaseGSValidator<LyDoBienDongModel>
    {
        public LyDoBienDongValidator(IValidateFactory validateFactory)
        {
            RuleFor(m => m).Custom((model, context) =>
              {
                  string Result = validateFactory.CheckChiTietLyDoBienDong(model);
                  if(!string.IsNullOrEmpty(Result))
                  {
                      context.AddFailure(Result);
                  }
                  
              });
        }
    }
}
