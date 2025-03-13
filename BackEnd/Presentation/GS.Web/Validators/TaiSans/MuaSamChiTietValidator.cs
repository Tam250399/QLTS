//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 21/7/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Services.TaiSans;
using GS.Web.Framework.Validators;
using GS.Web.Models.TaiSans;

namespace GS.Web.Validators.TaiSans
{
    public partial class MuaSamChiTietValidator : BaseGSValidator<MuaSamChiTietModel>
    {
        public MuaSamChiTietValidator(IMuaSamService _muaSamService)
        {
            RuleFor(x => x.TEN_TAI_SAN).NotEmpty().WithMessage("Tên không được để trống");
            RuleFor(x => x.HINH_THUC_MUA_SAM_ID).NotEmpty().WithMessage("Bạn chưa chọn hình thức mua sắm.");
			RuleFor(x => x.DU_TOAN_DUOC_DUYET).NotEmpty().WithMessage("Dự toán được phê duyệt không được để trống.");
			RuleFor(x => x.SO_LUONG).NotEmpty().WithMessage("Bạn chưa nhập số lượng.");
            RuleFor(x => x.THOI_GIAN_DU_KIEN).NotEmpty().WithMessage("Bạn chưa nhập thời gian dự kiến.");
			RuleFor(x => x.LOAI_TAI_SAN_ID).Must((model, loaiTSId) =>
			{
				if (!loaiTSId.HasValue)
				{
					return false;
				}
				else
					return true;
			}).WithMessage("Bạn chưa chọn loại tài sản.");
			RuleFor(x => x.DU_TOAN_DUOC_DUYET).Must((model, duToan) =>
			{
				if (duToan != null && duToan<1)
				{
					return false;
				}
				else
					return true;
			}).WithMessage("Dự toán được phê duyệt phải lớn hơn 0.");
			RuleFor(x => x.DU_TOAN_DUOC_DUYET).Must((model, duToan) =>
			{
				if (duToan>0 && model.THANH_TIEN >0 && duToan<model.THANH_TIEN)
				{
					return false;
				}
				else
					return true;
			}).WithMessage("Số dự toán phải lớn hơn hoặc bằng thành tiền.");
			RuleFor(x => x.THOI_GIAN_DU_KIEN).Must((model, tg) =>
			{
				if (tg !=null)
				{
					var ms = _muaSamService.GetMuaSamById(model.MUA_SAM_ID);
					if (ms!=null && tg.Value < ms.NGAY_DANG_KY)
						return false;
					else
						return true;
				}
				else
					return true;
			}).WithMessage("Dự kiến thời gian giao nhận phải lớn hơn hoặc bằng ngày đăng ký.");
			RuleFor(x => x.HINH_THUC_MUA_SAM_ID).Must((model, hinhThucMuaSamId) =>

			{
				if (!hinhThucMuaSamId.HasValue)
				{
					return false;
				}
				else
					return true;
			}).WithMessage("Bạn chưa chọn hình thức mua sắm.");

		}
    }
}

