//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Factories.DanhMuc;
using GS.Web.Framework.Validators;
using GS.Web.Models.DanhMuc;

namespace GS.Web.Validators.DanhMuc
{
    public partial class BaoCaoValidator : BaseGSValidator<BaoCaoModel>
    {
        public BaoCaoValidator(IBaoCaoModelFactory baoCaoModelFactory)
        {
            RuleFor(m => m.MA_BAO_CAO).NotEmpty().WithMessage("Mã không được để trống");
            RuleFor(m => m.MA_BAO_CAO).Must((model, code) =>
            {
                return baoCaoModelFactory.CheckMaBaoCao(id: model.ID, ma: model.MA_BAO_CAO);
            }).WithMessage("Mã đã tồn tại");
        }
    }
}

