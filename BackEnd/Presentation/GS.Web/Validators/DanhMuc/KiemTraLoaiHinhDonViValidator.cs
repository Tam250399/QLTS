using FluentValidation;
using GS.Web.Framework.Validators;
using GS.Web.Models.DanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Validators.DanhMuc
{
	public class KiemTraLoaiHinhDonViValidator : BaseGSValidator<KiemTraLoaiHinhDonViModel>
	{
		public KiemTraLoaiHinhDonViValidator()
		{
			RuleFor(m => m.LOAI_DON_VI_ID).NotEmpty().WithMessage("Loại đơn vị chưa được chọn");
			RuleFor(m => m.LOAI_CAP_DON_VI_ID).NotEmpty().WithMessage("Cấp đơn vị chưa được chọn");
			RuleFor(m => m.CAP_DON_VI_ID).NotEmpty().WithMessage("Cấp đơn vị chưa được chọn");
		}
		
	}
}
