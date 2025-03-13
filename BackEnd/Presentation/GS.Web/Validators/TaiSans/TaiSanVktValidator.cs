//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Factories.TaiSans;
using GS.Web.Framework.Validators;
using GS.Web.Models.TaiSans;

namespace GS.Web.Validators.TaiSans
{
	public partial class TaiSanVktValidator : BaseGSValidator<TaiSanVktModel>
	{
		public TaiSanVktValidator()
		{
			//RuleFor(x => x.VKT_DIEN_TICH).Must((model, dienTich) =>
			//{
			//	if (dienTich > 0 || model.THE_TICH > 0 || model.CHIEU_DAI > 0)
			//		return true;
			//	else
			//		return false;
			//}).WithMessage("Bạn phải nhập 1 trong 3 giá trị.");
		}
	}
}

