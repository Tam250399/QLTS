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
    public partial class HinhThucXuLyValidator : BaseGSValidator<HinhThucXuLyModel>
    {
        public HinhThucXuLyValidator(IHinhThucXuLyService hinhThucXuLyService)
        {
            RuleFor(model => model.MA).NotEmpty().WithMessage("GS.Web.Models.DanhMuc.HinhThucXuLyModel.MA.NotEmpty");
            RuleFor(model => model.TEN).NotEmpty().WithMessage("GS.Web.Models.DanhMuc.HinhThucXuLyModel.TEN.NotEmpty");
            RuleFor(model => model.MA).Must((model, code) =>
            {
                var ngts = hinhThucXuLyService.GetHinhThucXuLys(Ma: model.MA).FirstOrDefault();
                if (ngts != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }).WithMessage("GS.Web.Models.DanhMuc.HinhThucXuLyModel.MA.NotTrung");
        }
    }
}

