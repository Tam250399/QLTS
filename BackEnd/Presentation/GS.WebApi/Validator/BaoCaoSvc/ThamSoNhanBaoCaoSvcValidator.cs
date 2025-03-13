using FluentValidation;
using GS.Web.Framework.Validators;
using GS.WebApi.Factories;
using GS.WebApi.Models.BaoCaoSvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Validator.BaoCaoSvc
{
	public class ThamSoNhanBaoCaoSvcValidator : BaseGSValidator<ThamSoNhanBaoCaoSvcSearchModel>
	{
		public ThamSoNhanBaoCaoSvcValidator(IValidateFactory validateFactory)
		{
			RuleFor(p => p.LIST_DON_VI_ID).Must((model,don_vi_id)=> {
				if (don_vi_id== null || don_vi_id.Count==0)
					return false;
				return true;

			}).WithMessage("Phải có it nhất một đơn vị");
			RuleFor(p => p.MA_BAO_CAO).NotEmpty().WithMessage("Phải nhập mã báo cáo");
			RuleFor(p => p.LOAI_DATA_TRA_VE).NotNull().WithMessage("Phải nhập loại dữ liệu trả về");
        }
	}
}
