//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/2/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Core;
using GS.Web.Framework.Validators;
using GS.Web.Models.CCDC;

namespace GS.Web.Validators.CCDC
{
    public partial class GiamHongmatValidator : BaseGSValidator<GiamHongmatModel>
    {
        public GiamHongmatValidator()
        {
            RuleFor(m => m.SO_LUONG).NotEmpty().WithMessage("Số lượng không được để trống");
            RuleFor(m => m.NGAY_LAP).NotEmpty().WithMessage("Ngày lập không được để trống");
            RuleFor(m => m.NGAY_LAP).Must((model, code) =>
            {
                if (model.NgayPhanBo <= model.NGAY_LAP)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("Ngày lập phải lớn hơn ngày phân bổ");
            RuleFor(m => m.SO_LUONG).Must((model, code) =>
            {
                if (model.SO_LUONG <= model.SoLuongText.ToNumber() && model.SO_LUONG >0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("Số lượng vượt quá cho phép");
        }
    }
}

