using FluentValidation;
using GS.Web.Factories.BaoCaos;
using GS.Web.Framework.Validators;
using GS.Web.Models.BaoCaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Validators.BaoCaos
{
    public class CauHinhBaoCaoValidator : BaseGSValidator<CauHinhBaoCaoModel>
    {
        public CauHinhBaoCaoValidator(IReportModelFactory reportModelFactory)
		{
			RuleFor(m => m.MaBaoCao).NotEmpty().WithMessage("Phải nhập mã");
			RuleFor(m => m.MaBaoCao).Must((model, code) =>
            {
                return reportModelFactory.CheckMaCauHinhBaoCao(model.MaBaoCao);
            }).WithMessage("Mã đã tồn tại");
        }
    }
    public class CauHinhBaoCaoDBValidator : BaseGSValidator<CauHinhBaoCaoDBModel>
    {
        public CauHinhBaoCaoDBValidator(IReportModelFactory reportModelFactory)
		{
			RuleFor(m => m.MaBaoCao).NotEmpty().WithMessage("Phải nhập mã báo cáo");
			RuleFor(m => m.MaBaoCao).Must((model, code) =>
            {
                return reportModelFactory.CheckMaCauHinhBaoCaoDB(model.MaBaoCao);
            }).WithMessage("Mã báo cáo đã tồn tại");
        }
    }
    public class CauHinhMapBaoCaoDongBoValidator : BaseGSValidator<CauHinhMapBaoCaoDongBoModel>
    {
        public CauHinhMapBaoCaoDongBoValidator(IReportModelFactory reportModelFactory)
		{
			RuleFor(m => m.MaBaoCao).NotEmpty().WithMessage("Phải nhập mã báo cáo");
			RuleFor(m => m.MaBaoCao).Must((model, code) =>
            {
                return reportModelFactory.CheckMaCauHinhBaoCaoDB(model.MaBaoCao);
            }).WithMessage("Mã báo cáo đã tồn tại");
        }
    }
}
