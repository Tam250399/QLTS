//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Factories.DanhMuc;
using GS.Web.Framework.Validators;
using GS.Web.Models.DanhMuc;

namespace GS.Web.Validators.DanhMuc
{
    public partial class DiaBanValidator : BaseGSValidator<DiaBanModel>
    {
        public DiaBanValidator(IDiaBanModelFactory diaBanModelFactory)
        {
            
            RuleFor(x => x.MA).NotEmpty().WithMessage("Mã không được để trống");
            RuleFor(x => x.MA).MaximumLength(15).WithMessage("Mã quốc gia không quá 15 ký tự");
            RuleFor(x => x.TEN).NotEmpty().WithMessage("Tên không được để trống");
            RuleFor(x => x.TEN).MaximumLength(255).WithMessage("Tên quốc gia không quá 255 ký tự");

            RuleFor(x => x.MA).Must((model, code) =>
            {
                return diaBanModelFactory.CheckMaDiaBan(model.ID, model.MA);
            }).WithMessage("Mã địa bàn đã tồn tại");
        }
    }
}

