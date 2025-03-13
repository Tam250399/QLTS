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
    public class DanhMucDuAnValidator : BaseGSValidator<DuAnModel>
    {
        public DanhMucDuAnValidator(IValidateFactory validateFactory)
        {
            RuleFor(m => m.DON_VI_ID).Must((model, code) =>
            {
                if (model.DON_VI_ID >= 0)
                {
                    return validateFactory.CheckTonTaiDonVi(DonViId: model.DON_VI_ID.Value);
                }
                return true;
            }).WithMessage("DON_VI_ID Không tồn tại");
            RuleFor(m => m).Custom((model, context) =>
            {
                var result = validateFactory.CheckDuAn(model);
                if (!string.IsNullOrEmpty(result))
                {
                    context.AddFailure(result);
                }              
            });
            //RuleFor(m => m.MA).Must((model, Code) =>
            //  {
            //      return validateFactory.CheckTonTaiMaDuAn(model.MA);
            //  }).WithMessage("MA đã tồn tại");

        }
    }
}
