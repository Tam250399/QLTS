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
	public partial class DuAnValidator : BaseGSValidator<DuAnModel>
	{
		public DuAnValidator(IDuAnModelFactory duAnModelFactory)
		{
			RuleFor(m => m.MA).NotEmpty().WithMessage("Mã dự án không được để trống");
			RuleFor(m => m.TEN).NotEmpty().WithMessage("Tên dự án không được để trống");
			RuleFor(x => x.MA).MaximumLength(50).WithMessage("Mã đối tác không quá 50 ký tự");
			RuleFor(x => x.TEN).MaximumLength(255).WithMessage("Tên đối tác không quá 255 ký tự");
			RuleFor(m => m.MA).Must((model, code) =>
			{
				return duAnModelFactory.CheckMaDuAn(id: model.ID, ma: model.MA,donViID:model.DON_VI_ID);
			}).WithMessage("Mã đã tồn tại");
			//RuleFor(m => m.NGAY_BAT_DAU).NotEmpty().WithMessage("Ngày bắt đầu không được để trống");
			//RuleFor(m => m.NGAY_KET_THUC).NotEmpty().WithMessage("Ngày kết thúc không được để trống");
			RuleFor(m => m.NGAY_KET_THUC).Must((model, code) =>
			{
				if (model.NGAY_BAT_DAU.HasValue && model.NGAY_KET_THUC.HasValue)
				{
					return (model.NGAY_KET_THUC >= model.NGAY_BAT_DAU);
				}
				return true;
			}).WithMessage("Ngày kết thúc không được nhỏ hơn ngày bắt đầu");
			//RuleFor(m => m.TONG_KINH_PHI).Must((model, code) =>
			//{
			//	return (model.TONG_KINH_PHI > 0);
			//}).WithMessage("Tổng kinh phí phải lớn hơn 0");
			RuleFor(m => m.TONG_KINH_PHI).Must((model, code) =>
			{
				var tongNguonVon = (model.NGUON_NS??0) + (model.NGUON_VIEN_TRO??0) + (model.NGUON_ODA??0) + (model.NGUON_KHAC??0);
				return ((model.TONG_KINH_PHI??0) == tongNguonVon);
			}).WithMessage("Tổng các nguồn vốn phải bằng tổng kinh phí");

		}
	}
}

