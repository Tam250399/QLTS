//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/3/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Factories.DanhMuc;
using GS.Web.Framework.Validators;
using GS.Web.Models.DanhMuc;

namespace GS.Web.Validators.DanhMuc
{
    public partial class LoaiTaiSanVoHinhValidator : BaseGSValidator<LoaiTaiSanVoHinhModel>
    {
        public LoaiTaiSanVoHinhValidator(ILoaiTaiSanVoHinhModelFactory loaiTaiSanVoHinhModelFactory)
        {
            RuleFor(x => x.MA).NotEmpty().WithMessage("Mã không được để trống");
            RuleFor(x => x.TEN).NotEmpty().WithMessage("Tên không được để trống");
            RuleFor(x => x.MA).Must((model, code) =>
            {
                return loaiTaiSanVoHinhModelFactory.CheckMaLoaiTaiSanDonVi(model.ID, model.MA);
            }).WithMessage("Mã loại tài sản vô hình đã tồn tại");
            RuleFor(x => x.PARENT_ID).NotEmpty().WithMessage("Loại tài sản cha không được để trống");
            RuleFor(x => x.MA).MaximumLength(50).WithMessage("Mã đối tác không quá 50 ký tự");
            RuleFor(x => x.TEN).MaximumLength(255).WithMessage("Tên đối tác không quá 255 ký tự");
        }
    }
}

