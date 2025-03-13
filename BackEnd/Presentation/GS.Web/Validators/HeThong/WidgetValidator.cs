//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 20/5/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Framework.Validators;
using GS.Web.Models.HeThong;

namespace GS.Web.Validators.HeThong
{
    public partial class WidgetValidator : BaseGSValidator<WidgetModel>
    {
        public WidgetValidator()
        {
            RuleFor(c => c.TEN).NotNull().NotEmpty().WithMessage("Tên không được để trống");
            RuleFor(c => c.TEN).MinimumLength(4).WithMessage("Tên không được ngắn hơn 3 ký tự");
        }
    }
}

