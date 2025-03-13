using FluentValidation;
using GS.Web.Framework.Validators;
using GS.WebApi.Models.DMDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Validator.DanhMuc
{

    public class DMDC_DuAnValidator : BaseGSValidator<DMDC_DuAnModel>
    {
        public DMDC_DuAnValidator()
        {
            RuleFor(m => m.ID).Must((model, code) =>
            {
                return model.ID > 0;
            }).WithMessage("ID null");

        }
    }

}
