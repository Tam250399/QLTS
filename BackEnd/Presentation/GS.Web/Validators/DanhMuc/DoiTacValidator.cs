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
	public partial class DoiTacValidator : BaseGSValidator<DoiTacModel>
	{
		private readonly IDoiTacModelFactory _doiTacModelFactory;
		public DoiTacValidator(IDoiTacModelFactory doiTacModelFactory)
		{
			this._doiTacModelFactory = doiTacModelFactory;
			RuleFor(x => x.MA).Must((model, Ma) =>
			{
				return _doiTacModelFactory.CheckMaTrung(Ma, model.ID, model.DON_VI_ID);
			}).WithMessage("Mã không được trùng");
			RuleFor(x => x.MA).NotEmpty().WithMessage("Mã không được để trống");
			RuleFor(x => x.MA).MaximumLength(50).WithMessage("Mã đối tác không quá 50 ký tự");
			RuleFor(x => x.TEN).NotEmpty().WithMessage("Tên không được để trống");
			RuleFor(x => x.TEN).MaximumLength(255).WithMessage("Tên đối tác không quá 255 ký tự");
			//RuleFor(x => x.DON_VI_BO_PHAN_ID).NotEqual(0).WithMessage("Phải chọn phòng ban");
		}
	}
}

