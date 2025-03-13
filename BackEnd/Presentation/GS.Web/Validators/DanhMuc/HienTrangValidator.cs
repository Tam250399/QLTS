//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Framework.Validators;
using GS.Web.Models.DanhMuc;

namespace GS.Web.Validators.DanhMuc
{
    public partial class HienTrangValidator : BaseGSValidator<HienTrangModel>
    {
        public HienTrangValidator()
        {
            RuleFor(x => x.TEN_HIEN_TRANG).NotEmpty().WithMessage("Tên không được để trống");
            RuleFor(x => x.SelectedLoaiDonViIds).NotEmpty().WithMessage("Loại đơn vị áp dụng không được để trống");
        }
    }
}

