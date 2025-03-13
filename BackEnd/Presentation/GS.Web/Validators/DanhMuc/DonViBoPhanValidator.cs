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
    public partial class DonViBoPhanValidator : BaseGSValidator<DonViBoPhanModel>
    {
        public DonViBoPhanValidator(IDonViBoPhanModelFactory donViBoPhanModelFactory)
        {
            RuleFor(m => m.TEN).Must((model, ten) =>
            {
                return donViBoPhanModelFactory.CheckTenDonViBoPhan(id: model.ID, ten: model.TEN);
            }).WithMessage("Tên đơn vị đã tồn tại");
            RuleFor(m => m.TEN).NotEmpty().WithMessage("Tên không được để trống");
        }
    }
}

