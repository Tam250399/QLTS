using FluentValidation;
using GS.Web.Framework.Validators;
using GS.WebApi.Factories;
using GS.WebApi.Models.TaiSanXacLap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Validator.TaiSanXacLap
{
    public class KetQuaXuLyTaiSanValidator : BaseGSValidator<KetQuaXuLyTaiSanModel>
    {
        public KetQuaXuLyTaiSanValidator()
        {
            //RuleFor(c => c.TAI_SAN_TD_XU_LY_ID).NotNull().WithMessage("TAI_SAN_TD_XU_LY_ID null");
            RuleFor(c => c.HINH_THUC_XU_LY_ID).NotNull().WithMessage("HINH_THUC_XU_LY_ID null");
        }
    }
}
