using FluentValidation;
using GS.Core;
using GS.Core.Domain.CauHinh;
using GS.Web.Factories.HeThong;
using GS.Web.Framework.Validators;
using GS.Web.Models.HeThong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Validators.CauHinh
{
	public class TrangThaiNamValidator : BaseGSValidator<TrangThaiNamModel>
	{
		public TrangThaiNamValidator(ICauHinhModelFactory cauHinhModelFactory)
		{
			RuleFor(p => p.NgayKhoaSo).NotEmpty().WithMessage("Ngày khóa sổ không được trống");
			RuleFor(x => x.Nam).NotEmpty().WithMessage("Năm không được trống");
			RuleFor(x => x.TrangThai).NotNull().WithMessage("Trạng thái không được trống");
			RuleFor(x => x.NgayKhoaSo).Must((model, NgayKhoaSo) =>
			{
				if (NgayKhoaSo.HasValue && NgayKhoaSo.Value.Year >= model.Nam) return true;
				return false;
			}).WithMessage("Năm khóa sổ không thể nhỏ hơn năm của sổ");
			RuleFor(x => x.NgayKhoaSo).Must((model, NgayKhoaSo, propertyValidatorContext) =>
			{
				var lastYear = cauHinhModelFactory.GetSoTaiSanByYear(model.Nam-1);
				propertyValidatorContext.MessageFormatter.AppendArgument("lastYearLockDate", lastYear?.NgayKhoaSo.toDateVNString());
				if (lastYear != null && lastYear.NgayKhoaSo >= NgayKhoaSo) return false;
				return true;
			}).WithMessage("Ngày khóa sổ không được nhỏ hơn ngày khóa sổ năm trước :{lastYearLockDate}");
			/// muốn thay đổi thì phải mở sổ năm trước sau đó.
			/// VD: update sổ năm 2019 thì sổ 2020 phải mở
			RuleFor(x => x.Nam).Must((model, Year, propertyValidatorContext) =>
			  {
				  var nextYear = Year + 1;
				  propertyValidatorContext.MessageFormatter.AppendArgument("nextYear", nextYear);
				  var soNamSau = cauHinhModelFactory.GetSoTaiSanByYear(nextYear);
				  if (soNamSau != null && soNamSau.TrangThai != (int)enumTrangThaiNamTaiSan.OPEN) return false;
				  return true;
			  }).WithMessage("Phải mở khóa sổ năm {nextYear}");
			/// muốn thay đổi thì phải khóa sổ năm trước đó.
			/// VD: update sổ năm 2019 thì sổ 2018 phải đóng
			RuleFor(x => x.Nam).Must((model, Year, propertyValidatorContext) =>
			  {
				  var lastYear = Year - 1;
				  propertyValidatorContext.MessageFormatter.AppendArgument("lastYear", lastYear);
				  var soNamTruoc = cauHinhModelFactory.GetSoTaiSanByYear(lastYear);
				  if (soNamTruoc != null && soNamTruoc.TrangThai != (int)enumTrangThaiNamTaiSan.CLOSE) return false;
				  return true;
			  }).WithMessage("Phải khóa sổ năm {lastYear}");


		}
	}
}
