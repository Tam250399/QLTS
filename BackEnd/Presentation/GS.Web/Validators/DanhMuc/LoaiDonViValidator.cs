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
    public partial class LoaiDonViValidator : BaseGSValidator<LoaiDonViModel>
    {
        public LoaiDonViValidator(ILoaiDonViModelFactory loaiDonViModelFactory)
        {
            RuleFor(x => x.MA).NotEmpty().WithMessage("Mã không được để trống");
            RuleFor(x => x.TEN).NotEmpty().WithMessage("Tên không được để trống");
            //RuleFor(x => x.PARENT_ID).NotEmpty().WithMessage("Chọn loại hình đơn vị cha");
            RuleFor(m => m.MA).Must((model, code) =>
            {
                return loaiDonViModelFactory.CheckMaLoaiDonVi(id: model.ID, ma: model.MA);
            }).WithMessage("Mã đã tồn tại");
        }
    }
}

