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
    public partial class NhanXeValidator : BaseGSValidator<NhanXeModel>
    {
        public NhanXeValidator(INhanXeModelFactory nhanXeModelFactory)
        {
            RuleFor(x => x.MA).NotEmpty().WithMessage("Mã không được để trống");
            RuleFor(x => x.TEN).NotEmpty().WithMessage("Tên không được để trống");
            RuleFor(x => x.MA).Must((model, code) =>
            {
                return nhanXeModelFactory.CheckMaNhanXe(ma: model.MA, id: model.ID);
            }).WithMessage("Mã đã tồn tại");
        }
    }
}

