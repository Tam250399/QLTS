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
    public partial class CheDoHaoMonValidator : BaseGSValidator<CheDoHaoMonModel>
    {
        public CheDoHaoMonValidator(ICheDoHaoMonModelFactory cheDoHaoMonModelFactory)
        {
            RuleFor(x => x.MA_CHE_DO).NotEmpty().WithMessage("Mã không được để trống");
            RuleFor(x => x.TEN_CHE_DO).NotEmpty().WithMessage("Tên không được để trống");
            RuleFor(m => m.MA_CHE_DO).Must((model, code) =>
            {
                return cheDoHaoMonModelFactory.CheckMaCheDoHaoMon(id: model.ID, ma: model.MA_CHE_DO);
            }).WithMessage("Mã đã tồn tại");

        }
    }
}

