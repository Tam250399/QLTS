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
    public partial class TaiSanKiemKeHoiDongValidator : BaseGSValidator<TaiSanKiemKeHoiDongModel>
    {
        public TaiSanKiemKeHoiDongValidator()
        {
            RuleFor(c => c.HO_TEN).NotNull().WithMessage("Họ tên không được để trống");
            RuleFor(c => c.CHUC_VU).NotNull().WithMessage("Chức vụ không được để trống");
        }
    }
}

