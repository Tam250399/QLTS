using FluentValidation;
using GS.Web.Framework.Validators;
using GS.WebApi.Factories;
using GS.WebApi.Models.TaiSan;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Validator.TaiSan
{
    public class BienDongValidator : BaseGSValidator<BienDongDBModel>
    {
        public BienDongValidator(IValidateFactory validateFactory)
        {
            //RuleFor(x => x.MA_TAI_SAN).Must((model, code) =>
            //{
            //    return !String.IsNullOrEmpty(model.MA_TAI_SAN);
            //}).WithMessage("MA_TAI_SAN không được trống");
            //RuleFor(m => m.MA_TAI_SAN).Must((model, code) =>
            //{
            //    return validateFactory.CheckTonTaiTaiSan(model.MA_TAI_SAN);
            //}).WithMessage("MA_TAI_SAN không tồn tại");
            //RuleFor(m => m.MA_TAI_SAN_DB).Must((model, code) =>
            //{
            //    return validateFactory.CheckTonTaiTaiSanDongBo(model.MA_TAI_SAN_DB);
            //}).WithMessage("MA_TAI_SAN_DB not exist");
            RuleFor(m => m.GUID).Must((model, code,context) =>
            {
                context.MessageFormatter.AppendArgument("ID_DB", model.ID_DB);
                context.MessageFormatter.AppendArgument("Value", model.GUID);
                return validateFactory.CheckTonTaiBienDong(model.GUID);
            }).WithMessage("ID_DB={ID_DB} : GUID={Value} Không tồn tại");
            RuleFor(m => m).Custom((model, context) =>
            {
                var result = validateFactory.CheckBienDong(model);
                if (result.Count > 0)
                {
                    context.AddFailure($"ID_DB= {model.ID_DB}: " + string.Join("; ", result));
                }
            });
            RuleFor(m => m.DON_VI_BO_PHAN_ID).Must((model, code, context) =>
             {
                 context.MessageFormatter.AppendArgument("DB_MA", model.ID_DB);
                 context.MessageFormatter.AppendArgument("Value", model.DON_VI_BO_PHAN_ID);
                 return validateFactory.CheckTonTaiDonViBoPhan(model.DON_VI_BO_PHAN_ID);
             }).WithMessage("ID_DB={ID_DB} : DON_VI_BO_PHAN_ID={Value} Không tồn tại");
            RuleFor(m => m).Custom((model, context) =>
             {
                 var result = validateFactory.CheckLoaiTaisanForNhanTaiSan(model.LOAI_TAI_SAN_ID, model.LOAI_TAI_SAN_DON_VI_ID);
                 if (!string.IsNullOrEmpty(result))
                 {
                     context.AddFailure(result);
                 }
             });
            //RuleFor(m => m.NGAY_BIEN_DONG).Must((model, code) =>
            //{
            //    DateTime dt;
            //    string[] formats = { "yyyy-MM-dd HH:mm:ss" };
            //    if (!DateTime.TryParseExact(model.NGAY_BIEN_DONG, formats,
            //                    System.Globalization.CultureInfo.InvariantCulture,
            //                    DateTimeStyles.None, out dt))
            //    {
            //        //your condition fail code goes here
            //        return false;
            //    }
            //    else
            //    {
            //        return true;
            //    }


            //}).WithMessage("NGAY_BIEN_DONG không đúng định dạng yyyy-MM-dd HH:mm:ss");
            //RuleFor(m => m.NGAY_DUYET).Must((model, code) =>
            //{
            //    DateTime dt;
            //    string[] formats = { "yyyy-MM-dd HH:mm:ss" };
            //    if (!DateTime.TryParseExact(model.NGAY_DUYET, formats,
            //                    System.Globalization.CultureInfo.InvariantCulture,
            //                    DateTimeStyles.None, out dt))
            //    {
            //        //your condition fail code goes here
            //        return false;
            //    }
            //    else
            //    {
            //        return true;
            //    }


            //}).WithMessage("NGAY_DUYET không đúng định dạng yyyy-MM-dd HH:mm:ss");
            //RuleFor(m => m.NGAY_SU_DUNG).Must((model, code) =>
            //{
            //    DateTime dt;
            //    string[] formats = { "yyyy-MM-dd HH:mm:ss" };
            //    if (!DateTime.TryParseExact(model.NGAY_SU_DUNG, formats,
            //                    System.Globalization.CultureInfo.InvariantCulture,
            //                    DateTimeStyles.None, out dt))
            //    {
            //        //your condition fail code goes here
            //        return false;
            //    }
            //    else
            //    {
            //        return true;
            //    }


            //}).WithMessage("NGAY_SU_DUNG không đúng định dạng yyyy-MM-dd HH:mm:ss");
            //RuleFor(m => m.NGAY_TAO).Must((model, code) =>
            //{
            //    DateTime dt;
            //    string[] formats = { "yyyy-MM-dd HH:mm:ss" };
            //    if (!DateTime.TryParseExact(model.NGAY_TAO, formats,
            //                    System.Globalization.CultureInfo.InvariantCulture,
            //                    DateTimeStyles.None, out dt))
            //    {
            //        //your condition fail code goes here
            //        return false;
            //    }
            //    else
            //    {
            //        return true;
            //    }


            //}).WithMessage("NGAY_TAO không đúng định dạng yyyy-MM-dd HH:mm:ss");
        }
    }
}
