using FluentValidation;
using GS.Web.Framework.Validators;
using GS.Web.Models.SHTD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Validators.SHTD
{
    public partial class XuLyKetQuaValidator:BaseGSValidator<XuLyKetQuaModel>
    {
        public XuLyKetQuaValidator()
        {
            RuleFor(c => c.CHUNG_TU_NOP_TIEN_NGAY).NotEmpty().WithMessage("Chứng từ ngày không được để trống");
            RuleFor(c => c.CHUNG_TU_NOP_TIEN_SO).NotEmpty().WithMessage("Chứng từ số không được để trống");
            RuleFor(c => c.NGAY_NOP_TIEN).NotEmpty().WithMessage("Tài sản không được để trống");
            RuleFor(c => c.SO_TIEN).NotEmpty().WithMessage("Số tiền không được để trống");
            RuleFor(c => c.CHUNG_TU_NOP_TIEN_NGAY).Must((model, code) =>
                {

                    if (model.CHUNG_TU_NOP_TIEN_NGAY != null && model.CHUNG_TU_NOP_TIEN_NGAY <= DateTime.Now)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }).WithMessage("Chứng từ ngày phải nhỏ hơn ngày hiện tại");
            RuleFor(c => c.NGAY_NOP_TIEN).Must((model, code) =>
            {

                if (model.NGAY_NOP_TIEN != null && model.NGAY_NOP_TIEN <= DateTime.Now)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }).WithMessage("Ngày nộp tiền phải nhỏ hơn ngày hiện tại");
        }
    }
}
