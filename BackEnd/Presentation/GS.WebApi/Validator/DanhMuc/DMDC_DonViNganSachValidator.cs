using FluentValidation;
using GS.Web.Framework.Validators;
using GS.WebApi.Models.DMDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Validator.DanhMuc
{

    public class DMDC_DonViNganSachValidator : BaseGSValidator<DMDC_DonViNganSachModel>
    {
        public DMDC_DonViNganSachValidator()
        {
            RuleFor(m => m.ID).Must((model, code) =>
            {
                return (model.ID > 0);
            }).WithMessage("ID null");

        }
    }

}
