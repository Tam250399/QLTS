//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/3/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Factories.DanhMuc;
using GS.Web.Framework.Validators;
using GS.Web.Models.DanhMuc;

namespace GS.Web.Validators.DanhMuc
{
    public partial class DiaBanTestValidator : BaseGSValidator<DiaBanTestModel>
    {
        public DiaBanTestValidator(IDiaBanTestModelFactory diaBanTestModelFactory)
        {
            RuleFor(x => x.MA).NotEmpty().WithMessage("Mã không được để trống");
            RuleFor(x => x.TEN).NotEmpty().WithMessage("Tên không được để trống");
            RuleFor(x => x.MA).Must((model, code) =>{
                return diaBanTestModelFactory.CheckMaDiaBanTest(model.ID, model.MA);
            }).WithMessage("Mã địa bàn đã tồn tại");
        }
    }
}

