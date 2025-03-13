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
    public partial class KiemKeHoiDongValidator : BaseGSValidator<KiemKeHoiDongModel>
    {
        public KiemKeHoiDongValidator()
        {
            RuleFor(c => c.HO_TEN).NotNull().WithMessage("Họ tên không được để trống");
            RuleFor(c => c.CHUC_VU).NotNull().WithMessage("Chức vụ không được để trống");
            //RuleFor(c => c.DAI_DIEN).NotNull().WithMessage("Đại diện không được để trống");
            //RuleFor(c => c.VI_TRI_ID).Must((model,code)=>
            //{
            //    if (model.VI_TRI_ID > 0)
            //        return true;
            //    else return false;
            //}).WithMessage("Chọn vị trí");
        }
    }
}

