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
	public partial class LoaiTaiSanValidator : BaseGSValidator<LoaiTaiSanModel>
	{
		public LoaiTaiSanValidator(ILoaiTaiSanModelFactory loaiTaiSanModelFactory)
		{
			RuleFor(x => x.MA).NotEmpty().WithMessage("Mã không được để trống");
			RuleFor(x => x.TEN).NotEmpty().WithMessage("Tên không được để trống");
			RuleFor(x => x.LOAI_HINH_TAI_SAN_ID).NotEmpty().WithMessage("Chọn loại hình tài sản");
			RuleFor(x => x.PARENT_ID).NotEmpty().WithMessage("Chọn loại tài sản cha");
			RuleFor(x => x.MA).Must((model, code) =>
			{
				return loaiTaiSanModelFactory.CheckMaLoaiTaiSan(model.ID, model.MA, model.CHE_DO_HAO_MON_ID.Value);
			}).WithMessage("Mã Loại tài sản đã tồn tại");
			RuleFor(x => x.OTO_CHO_NGOI_DEN).Must((model, code) =>
			{
				if (model.OTO_CHO_NGOI_TU > 0 && model.OTO_CHO_NGOI_DEN > 0 && model.OTO_CHO_NGOI_DEN < model.OTO_CHO_NGOI_TU)
					return false;
				return true;
			}).WithMessage("Số chỗ ngồi tối đa phải lớn hơn số chỗ ngồi tối thiểu!");
		}
	}
}

