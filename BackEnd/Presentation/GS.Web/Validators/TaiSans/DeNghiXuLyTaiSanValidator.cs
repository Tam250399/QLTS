//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/11/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Framework.Validators;
using GS.Web.Models.TaiSans;

namespace GS.Web.Validators.TaiSans
{
    public partial class DeNghiXuLyTaiSanValidator : BaseGSValidator<DeNghiXuLyTaiSanModel>
    {
        public DeNghiXuLyTaiSanValidator()
        {
            RuleFor(x => x.MA_TAI_SAN).NotEmpty().WithMessage("Chưa chọn tài sản");
        }
    }
}

