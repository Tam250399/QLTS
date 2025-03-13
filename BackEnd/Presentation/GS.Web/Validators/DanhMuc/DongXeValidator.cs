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
    public partial class DongXeValidator : BaseGSValidator<DongXeModel>
    {
        public DongXeValidator(IDongXeModelFactory dongXeModelFactory)
        {
            RuleFor(x => x.MA).NotEmpty().WithMessage("Mã không được để trống");
            RuleFor(x => x.MA).MaximumLength(20).WithMessage("Mã dòng xe không quá 20 ký tự");
            RuleFor(x => x.TEN).NotEmpty().WithMessage("Tên không được để trống");
            RuleFor(x => x.MA).MaximumLength(255).WithMessage("Tên dòng xe không quá 255 ký tự");
            RuleFor(x => x.NHAN_XE_ID).NotEmpty().WithMessage("Nhãn xe chưa được chọn");
            RuleFor(x => x.MA).Must((model, code) =>
            {
                return dongXeModelFactory.CheckMaDongXe(ma: model.MA, id: model.ID);
            }).WithMessage("Mã đã tồn tại");
        }
    }
}

