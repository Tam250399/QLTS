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
	public partial class DonViValidator : BaseGSValidator<DonViModel>
	{
		public DonViValidator(IDonViModelFactory donViModelFactory)
		{
			RuleFor(m => m.MA).NotEmpty().WithMessage("Mã đơn vị không được để trống");
            RuleFor(m => m.MA).Length(3).WithMessage("Mã đơn vị chỉ được 3 kí tự");
            RuleFor(m => m.TEN).NotEmpty().WithMessage("Tên đơn vị không được để trống");
			//RuleFor(m => m.MA_DIA_BAN).NotEmpty().WithMessage("Mã địa bàn không được để trống");
			//RuleFor(m => m.DIA_BAN_ID).NotEmpty().WithMessage("Tên địa bàn không được để trống");
			RuleFor(m => m.DIA_CHI).NotEmpty().WithMessage("Địa chỉ không được để trống");
			RuleFor(m => m.LOAI_DON_VI_ID).NotEmpty().WithMessage("Loại đơn vị chưa được chọn");
			RuleFor(m => m.CHE_DO_HACH_TOAN_ID).Must((model, cheDoHachToan) =>
			{
				if (cheDoHachToan > 0)
					return true;
				return false;
			}).WithMessage("Chọn chế độ quản lý tài sản cố định");
			// nếu check có mã qhns thì mqhns không được bỏ trống

			RuleFor(m => m.MA_DVQHNS).Must((model, MA_DVQHNS) =>
			{
				if (model.IsMaQHNS && string.IsNullOrEmpty(MA_DVQHNS) )
				{
					return false;
				}
				return true;
				
			}).WithMessage("Mã QHNS không được bỏ trống");
			RuleFor(m => m.MA).Must((model, code) =>
			{
				string Ma = model.MA_DON_VI_CHA + model.MA;
				return donViModelFactory.CheckMaDonVi(id: model.ID, ma: Ma);
			}).WithMessage("Mã đã tồn tại");
			RuleFor(m => m.SO_QUYET_DINH_GIAO_VON).Must((model, code) =>
			{
				return !(model.LA_DON_VI_TU_CHU_TAI_CHINH.Equals(true) && model.DA_CO_QUYET_DINH_GIAO_VON.Equals(true) && string.IsNullOrEmpty(model.SO_QUYET_DINH_GIAO_VON));
			}).WithMessage("Số quyết định không được để trống");
			RuleFor(m => m.NGAY_QUYET_DINH_GIAO_VON).Must((model, code) =>
			{
				return !(model.LA_DON_VI_TU_CHU_TAI_CHINH.Equals(true) && model.DA_CO_QUYET_DINH_GIAO_VON.Equals(true) && model.NGAY_QUYET_DINH_GIAO_VON == null);
			}).WithMessage("Ngày quyết định không được để trống");

			RuleFor(m => m.SO_QUYET_DINH).Must((model, code) =>
			{
				return !(model.LA_DON_VI_TU_CHU_TAI_CHINH.Equals(true) && model.DA_CO_QUYET_DINH_GIAO_VON.Equals(false) && string.IsNullOrEmpty(model.SO_QUYET_DINH));
			}).WithMessage("Số quyết định không được để trống");
			RuleFor(m => m.NGAY_QUYET_DINH).Must((model, code) =>
			{
				return !(model.LA_DON_VI_TU_CHU_TAI_CHINH.Equals(true) && model.DA_CO_QUYET_DINH_GIAO_VON.Equals(false) && model.NGAY_QUYET_DINH == null);
			}).WithMessage("Ngày quyết định không được để trống");

		}
	}
}

