using FluentValidation;
using GS.Web.Framework.Validators;
using GS.WebApi.Models;
using GS.WebApi.Models.TaiSan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Validator.TaiSan
{
    public partial class TestValidator : BaseGSValidator<TestAPIModel>
    {
        public TestValidator()
        {
            RuleFor(x => x.NGAY_KIEM_KE).NotEmpty().WithMessage("Ngày kiểm kê không được để trống");
        }
    }
}
