//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Framework.Validators;
using GS.Web.Models.TaiSans;

namespace GS.Web.Validators.TaiSans
{
    public partial class TaiSanKiemKeValidator : BaseGSValidator<TaiSanKiemKeModel>
    {
        public TaiSanKiemKeValidator()
        {
            RuleFor(x => x.NGAY_KIEM_KE).NotEmpty().WithMessage("Ngày kiểm kê không được để trống");
        }
    }
}

