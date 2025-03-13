using FluentValidation;
using GS.Web.Framework.Validators;
using GS.WebApi.Factories;
using GS.WebApi.Models.DanhMuc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Validator.DanhMuc
{
    public class NguoiDungValidator : BaseGSValidator<NguoiDungModel>
    {
        public NguoiDungValidator(IValidateFactory validateFactory)
        {
            RuleFor(x => x.TEN_DANG_NHAP).NotEmpty().WithMessage("TEN_DANG_NHAP không được để trống");
            RuleFor(x => x.TEN_DAY_DU).NotEmpty().WithMessage("TEN_DAY_DU không được để trống");
            RuleFor(x => x.DB_ID).NotEmpty().WithMessage("DB_ID không được để trống");

            RuleFor(m => m).Custom((model, context) =>
            {
                List<string> ListError = new List<string>();
                if (model.LIST_DON_VI != null && model.LIST_DON_VI.Count > 0)
                {
                    int i = 0;
                    foreach (var item in model.LIST_DON_VI)
                    {
                        var result = validateFactory.CheckTonTaiDonVi(0, item.MA_DON_VI, item.MA_DVQHNS);
                        if (!result)
                        {
                            ListError.Add($"Đơn vị có  MaDonVi= { item.MA_DON_VI}, Ma DVQHNS = { item.MA_DVQHNS} không tồn tại ");
                            i++;
                        }


                    }
                    if (i < model.LIST_DON_VI.Count())
                    {
                        ListError = new List<string>();
                    }
                }
                else
                {
                    ListError.Add($"LIST_DON_VI không được để trống");
                }

                if (ListError.Count > 0)
                {
                    context.AddFailure(string.Join("; ", ListError));
                }
            });
        }
    }
}
