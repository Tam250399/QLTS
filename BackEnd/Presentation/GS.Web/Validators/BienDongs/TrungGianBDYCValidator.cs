using FluentValidation;
using GS.Web.Framework.Validators;
using GS.Web.Models.BienDongs;

namespace GS.Web.Validators.BienDongs
{
	public partial class TrungGianBDYCValidator : BaseGSValidator<TrungGianBDYCModel>
	{
		public TrungGianBDYCValidator()
		{
			//RuleFor(x => x.TrungGianBDYCChiTietModel.PHI_THU).Must((model, tienThanhLy) =>
			//{
			//	if (model.TrungGianBDYCChiTietModel.IS_BAN_THANH_LY && 
			//	(model.TrungGianBDYCChiTietModel.PHI_THU == null || model.TrungGianBDYCChiTietModel.PHI_THU == 0))
			//		return false;
			//	return true;
			//}).WithMessage("Tiền thanh lý phải có giá trị.");
			RuleFor(x => x.TrungGianBDYCChiTietModel.HINH_THUC_XU_LY_ID).Must((model, hinhthuc) =>
			{
				if (model.TrungGianBDYCChiTietModel.IS_BAN_THANH_LY && 
				(model.TrungGianBDYCChiTietModel.HINH_THUC_XU_LY_ID == null || model.TrungGianBDYCChiTietModel.HINH_THUC_XU_LY_ID == 0))
					return false;
				return true;
			}).WithMessage("Phải chọn hình thức xử lý.");
			RuleFor(x => x.TrungGianBDYCChiTietModel.NGAY_BAN_THANH_LY).Must((model, ngaybanthanhly) =>
			{
				if (model.TrungGianBDYCChiTietModel.IS_BAN_THANH_LY && 
				model.TrungGianBDYCChiTietModel.NGAY_BAN_THANH_LY == null )
					return false;
				return true;
			}).WithMessage("Phải nhập ngày bán thanh lý.");
		}
	}
}