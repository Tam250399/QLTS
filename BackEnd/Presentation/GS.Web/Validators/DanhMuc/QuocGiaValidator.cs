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
    public partial class QuocGiaValidator : BaseGSValidator<QuocGiaModel>
    {
        public QuocGiaValidator(IQuocGiaModelFactory quocGiaModelFactory)
        {
            RuleFor(x => x.MA).NotEmpty().WithMessage("Mã quốc gia không được để trống");
            RuleFor(x => x.MA).MaximumLength(100).WithMessage("Mã quốc gia không quá 100 ký tự");
            RuleFor(x => x.TEN).NotEmpty().WithMessage("Tên quốc gia không được để trống");
            RuleFor(x => x.TEN).MaximumLength(255).WithMessage("Tên quốc gia không quá 255 ký tự");
            RuleFor(x => x.MA).Must((model, code) =>
            {
                return quocGiaModelFactory.CheckMaQuocGia(ma: model.MA, id: model.ID);
            }).WithMessage("Mã quốc gia đã tồn tại");
        }
    }
}

