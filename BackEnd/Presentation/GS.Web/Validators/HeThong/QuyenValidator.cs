//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Factories.HeThong;
using GS.Web.Framework.Validators;
using GS.Web.Models.HeThong;

namespace GS.Web.Validators.HeThong
{
    public partial class QuyenValidator : BaseGSValidator<QuyenModel>
    {
        public QuyenValidator(IQuyenModelFactory quyenModelFactory)
        {
            RuleFor(x => x.MA).NotEmpty().WithMessage("Mã quyền không được để trống");
            RuleFor(x => x.MA).Must((model, code) =>
            {
                return quyenModelFactory.CheckMaQuyen(model.MA, model.ID);
            }).WithMessage("Mã nhập vào đã trùng");
        }
    }
}

