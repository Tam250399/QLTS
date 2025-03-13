using FluentValidation;
using GS.Web.Framework.Validators;
using GS.Web.Models.SHTD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Validators.SHTD
{
    public partial class KetQuaTaiSanValidator : BaseGSValidator<KetQuaTaiSanModel>
    {
        public KetQuaTaiSanValidator()
        {            
            RuleFor(c => c.TAI_SAN_TD_XU_LY_ID).NotEmpty().WithMessage("Tài sản không được để trống");
            RuleFor(c => c.SO_LUONG).NotEmpty().WithMessage("Số lượng không được để trống");
            RuleFor(c => c.SO_TIEN).NotEmpty().WithMessage("Số tiền không được để trống");
            RuleFor(c => c.TAI_SAN_TD_XU_LY_ID).Must((model, code) =>
            {

                if (model.TAI_SAN_TD_XU_LY_ID != null && model.TAI_SAN_TD_XU_LY_ID >0)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }).WithMessage("Tài sản không được để trống");
            RuleFor(c => c.SO_LUONG).Must((model, code) =>
            {

                if (model.SO_LUONG != null && model.SO_LUONG <= model.SoLuongCoTheXuLy)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }).WithMessage("Số lượng vượt quá giới hạn");           
        }
    }
}
