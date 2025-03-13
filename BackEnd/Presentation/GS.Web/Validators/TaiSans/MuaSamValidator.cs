//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 21/7/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Services.TaiSans;
using GS.Web.Framework.Validators;
using GS.Web.Models.TaiSans;
using System.Linq;

namespace GS.Web.Validators.TaiSans
{
    public partial class MuaSamValidator : BaseGSValidator<MuaSamModel>
    {
        public MuaSamValidator(IMuaSamChiTietService _muaSamChiTietService)
        {
            RuleFor(c => c.NGAY_DANG_KY).Must((model, c) =>
            {
                var Ngay_MSCT_Min = _muaSamChiTietService.GetMapByMuaSamId(model.ID).Min(x => x.THOI_GIAN_DU_KIEN);
                if (Ngay_MSCT_Min != null && Ngay_MSCT_Min < model.NGAY_DANG_KY)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }).WithMessage("Ngày đăng ký phải nhỏ hơn Thời gian dự kiến");
        }
    }
}

