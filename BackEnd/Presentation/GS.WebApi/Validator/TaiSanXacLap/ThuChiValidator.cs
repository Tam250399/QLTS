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
    public class ThuChiValidator : BaseGSValidator<ThuChiModel>
    {
        public ThuChiValidator()
        {
            RuleFor(c => c.SO_TIEN_PHAI_THU).NotEmpty().WithMessage("SO_TIEN_PHAI_THU null");
            RuleFor(c => c.SO_TIEN_CON_PHAI_THU).NotEmpty().WithMessage("SO_TIEN_CON_PHAI_THU null");
            RuleFor(c => c.SO_TIEN_THU).NotEmpty().WithMessage("SO_TIEN_THU null");
            RuleFor(c => c.NGAY_THU).NotEmpty().WithMessage("NGAY_THU null");
            RuleFor(c => c.CHI_PHI).NotEmpty().WithMessage("CHI_PHI null");
            RuleFor(c => c.SO_TIEN_THU).Must((model, code) =>
            {
                if (model.SO_TIEN_PHAI_THU != null && model.SO_TIEN_THU != null && model.SO_TIEN_THU <= model.SO_TIEN_PHAI_THU)
                    return true;
                else
                    return false;
            }).WithMessage("SO_TIEN_PHAI_THU phải lớn hơn hoặc bằng SO_TIEN_THU");
            RuleFor(c => c.SO_TIEN_CON_PHAI_THU).Must((model, code) =>
            {
                if (model.SO_TIEN_PHAI_THU != null && model.SO_TIEN_CON_PHAI_THU != null && model.SO_TIEN_CON_PHAI_THU <= model.SO_TIEN_PHAI_THU)
                    return true;
                else
                    return false;
            }).WithMessage("SO_TIEN_PHAI_THU phải lớn hơn hoặc bằng SO_TIEN_CON_PHAI_THU");
            RuleFor(c => c.NGAY_THU).Must((model, code) =>
            {
                if (model.NGAY_THU != null && model.NGAY_THU <= DateTime.Now)
                    return true;
                else
                    return false;
            }).WithMessage("NGAY_THU phải nhỏ hơn hoặc bằng ngày hiện tại");
            RuleFor(c => c.TG_NGAY_NOP).Must((model, code) =>
            {
                if (model.TG_NGAY_NOP != null && model.TG_NGAY_NOP <= DateTime.Now)
                    return true;
                else if (model.TG_NGAY_NOP == null)
                    return true;
                else
                    return false;
            }).WithMessage("TG_NGAY_NOP phải nhỏ hơn hoặc bằng ngày hiện tại");
        }
    }
}
