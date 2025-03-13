//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Framework.Validators;
using GS.Web.Models.CCDC;

namespace GS.Web.Validators.CCDC
{
    public partial class KiemKeValidator : BaseGSValidator<KiemKeModel>
    {
        public KiemKeValidator()
        {
            RuleFor(m => m.NGAY_KIEM_KE).NotEmpty().WithMessage("Ngày kiểm kê không được để trống");
        }
    }
}

