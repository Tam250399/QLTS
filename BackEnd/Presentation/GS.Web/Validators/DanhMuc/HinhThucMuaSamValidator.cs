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
    public partial class HinhThucMuaSamValidator : BaseGSValidator<HinhThucMuaSamModel>
    {
        public HinhThucMuaSamValidator(IHinhThucMuaSamModelFactory hinhThucMuaSamModelFactory)
        {
            RuleFor(x => x.MA).NotEmpty().WithMessage("Mã không được để trống");
            RuleFor(x => x.TEN).NotEmpty().WithMessage("Tên không được để trống");
            RuleFor(m => m.MA).Must((model, code) =>
            {
                return hinhThucMuaSamModelFactory.CheckMaLoaiHinhThucMuaSam(id: model.ID, ma: model.MA);
            }).WithMessage("Mã đã tồn tại");
        }
    }
    }


