//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Services.DanhMuc;
using GS.Web.Framework.Validators;
using GS.Web.Models.DanhMuc;
using System.Linq;

namespace GS.Web.Validators.DanhMuc
{
    public partial class PhuongAnXuLyValidator : BaseGSValidator<PhuongAnXuLyModel>
    {
        public PhuongAnXuLyValidator(IPhuongAnXuLyService _phuongAnXuLyService)
        {
            RuleFor(m=>m.TEN).NotEmpty().WithMessage("Tên không được để trống");
            RuleFor(m => m.MA).NotEmpty().WithMessage("Mã không được để trống");
            RuleFor(m => m.MA).Must((model, code) =>
            {
                var htxl = _phuongAnXuLyService.GetPhuongAnXuLys(Ma: model.MA).FirstOrDefault();
                if (htxl != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }).WithMessage("Mã không được trùng");
        }
    }
}

