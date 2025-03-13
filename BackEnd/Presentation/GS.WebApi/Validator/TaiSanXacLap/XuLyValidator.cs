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
    public class XuLyValidator : BaseGSValidator<XuLyModel>
    {
        public XuLyValidator(IValidateFactory validateFactory)
        {
            RuleFor(c => c.QUYET_DINH_SO).NotEmpty().WithMessage("QUYET_DINH_SO null");
            RuleFor(c => c.QUYET_DINH_NGAY).NotEmpty().WithMessage("QUYET_DINH_NGAY null");
            RuleFor(c => c.QUYET_DINH_NGAY).Must((model, code) =>
            {
                if (model.QUYET_DINH_NGAY != null && model.QUYET_DINH_NGAY <= DateTime.Now)
                    return true;
                else
                    return false;
            }).WithMessage("QUYET_DINH_NGAY phải nhỏ hơn hoặc bằng ngày hiện tại");
            //RuleFor(c => c.LST_TS_XU_LY).Must((model, code) =>
            //{
            //    if (model.LST_TS_XU_LY != null && model.LST_TS_XU_LY.Count() > 0)
            //        return true;
            //    else
            //        return false;
            //}).WithMessage("LST_TS_XU_LY null");
        }
    }
}
