//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/6/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Framework.Validators;
using GS.Web.Models.DanhMuc;

namespace GS.Web.Validators.DanhMuc
{
    public partial class LoaiTaiSanKhauHaoValidator : BaseGSValidator<LoaiTaiSanKhauHaoModel>
    {
        public LoaiTaiSanKhauHaoValidator()
        {
            RuleFor(x => x.THOI_HAN_SU_DUNG).NotEmpty().WithMessage("Thời hạn sử dụng không được để trống");
        }
    }
}

