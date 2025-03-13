//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Services.HeThong;
using GS.Web.Factories.HeThong;
using GS.Web.Framework.Validators;
using GS.Web.Models.HeThong;

namespace GS.Web.Validators.HeThong
{
    public partial class NhanHienThiValidator : BaseGSValidator<NhanHienThiModel>
    {
        public NhanHienThiValidator(INhanHienThiService _nhanHienThiService, INhanHienThiModelFactory nhanHienThiModelFactory)
        {
            RuleFor(x => x.MA).NotEmpty().WithMessage(_nhanHienThiService.GetGiaTri("Validator.HeThong.NhanHienThi.Ma.NotNull"));
            RuleFor(x => x.MA).Must((model, code) =>
            {
                return nhanHienThiModelFactory.CheckTrungMa(model.MA, model.ID);
            }).WithMessage(_nhanHienThiService.GetGiaTri("Validator.HeThong.NhanHienThi.Ma.Trung"));
        }
    }
}

