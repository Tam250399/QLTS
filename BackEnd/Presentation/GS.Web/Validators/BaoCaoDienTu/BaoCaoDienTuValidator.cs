//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/10/2021
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Factories.BaoCaoDienTus;
using GS.Web.Framework.Validators;
using GS.Web.Models.BaoCaoDienTu;

namespace GS.Web.Validators.BaoCaoDienTu
{
    public partial class BaoCaoDienTuValidator : BaseGSValidator<BaoCaoDienTuModel>
    {
        public BaoCaoDienTuValidator(IBaoCaoDienTuModelFactory BaoCaoDienTuModelFactory)
        {
            RuleFor(x => x.TEN).NotEmpty().WithMessage("Tên báo cáo không được để trống");
            RuleFor(x => x.NGAY_BAO_CAO).NotNull().WithMessage("Ngày báo cáo không được để trống");
        }
    }
}

