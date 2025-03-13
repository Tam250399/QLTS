using FluentValidation;
using GS.Web.Framework.Validators;
using GS.WebApi.Factories;
using GS.WebApi.Models.DanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Validator.DanhMuc
{
    public class LoaiTaiSanDonViValidator : BaseGSValidator<LoaiTaiSanDonViModel>
    {
        public LoaiTaiSanDonViValidator(IValidateFactory validateFactory)
        {           
            RuleFor(m => m.PARENT_ID).Custom((model, context) =>
            {
                if(model>=0)
                {
                    string result = validateFactory.CheckTonTaiLoaiTaiSanDonViCha(model.Value);
                    if (!string.IsNullOrEmpty(result))
                    {
                        context.AddFailure(result);
                    }
                }                
            });
            RuleFor(m => m.DON_VI_ID).Must((model, code) =>
            {
                if(model.DON_VI_ID>=0)
                {
                    return validateFactory.CheckTonTaiDonVi(DonViId: model.DON_VI_ID.Value);
                }
                return true;
            }).WithMessage("DON_VI_ID Không tồn tại");
            RuleFor(m => m.CHE_DO_HAO_MON_ID).Must((model, code) =>
            {
                if (model.CHE_DO_HAO_MON_ID >= 0)
                {
                    return validateFactory.CheckTonTaiCheDoHaoMon(model.CHE_DO_HAO_MON_ID.Value);
                }
                return true;
            }).WithMessage("CHE_DO_HAO_MON_ID Không tồn tại");
        }
    }
}
