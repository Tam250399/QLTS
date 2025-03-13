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
    public class TaiSanValidator : BaseGSValidator<TaiSanDBModel>
    {
        public TaiSanValidator(IValidateFactory validateFactory)
        {
            RuleFor(m => m.TEN).Must((model, code, context) =>
            {
                context.MessageFormatter.AppendArgument("DB_MA", model.DB_MA);
                // context.MessageFormatter.AppendArgument("Value", model.MA_DON_VI);
                if (string.IsNullOrEmpty(model.TEN))
                {
                    return false;
                }
                return true;

            }).WithMessage("DB_MA={DB_MA}: TEN null");
            RuleFor(m => m.NGAY_DANG_KY).Must((model, code, context) =>
            {
                context.MessageFormatter.AppendArgument("DB_MA", model.DB_MA);
                // context.MessageFormatter.AppendArgument("Value", model.MA_DON_VI);
                if (string.IsNullOrEmpty(model.TEN))
                {
                    return false;
                }
                return true;

            }).WithMessage("DB_MA={DB_MA}: NGAY_DANG_KY null");
            RuleFor(m => m.NGAY_SU_DUNG).Must((model, code, context) =>
            {
                context.MessageFormatter.AppendArgument("DB_MA", model.DB_MA);
                // context.MessageFormatter.AppendArgument("Value", model.MA_DON_VI);
                if (string.IsNullOrEmpty(model.TEN))
                {
                    return false;
                }
                return true;

            }).WithMessage("DB_MA={DB_MA}: NGAY_SU_DUNG null");
            RuleFor(m => m.MA).Must((model, code, context) =>
            {
                context.MessageFormatter.AppendArgument("MA", model.MA);
                return validateFactory.CheckTonTaiTaiSan(model.MA);
            }).WithMessage("MA = {MA}  Không tồn tại");
            //RuleFor(m => m.DB_MA).Must((model, code, context) =>
            //{
            //    context.MessageFormatter.AppendArgument("DB_MA", model.DB_MA);
            //    return !validateFactory.CheckTonTaiTaiSanDongBo(model.DB_MA);
            //}).WithMessage("DB_MA={DB_MA}: DB_MA Đã tồn tại");
            RuleFor(m => m.MA_DON_VI).Must((model, code, context) =>
            {
                context.MessageFormatter.AppendArgument("DB_MA", model.DB_MA);
                context.MessageFormatter.AppendArgument("Value", model.MA_DON_VI);
                return !String.IsNullOrEmpty(model.MA_DON_VI);
            }).WithMessage("DB_MA={DB_MA}: MA_DON_VI={Value} NULL");
            RuleFor(m => m.MA_DON_VI).Must((model, code, context) =>
            {
                context.MessageFormatter.AppendArgument("DB_MA", model.DB_MA);
                context.MessageFormatter.AppendArgument("Value", model.MA_DON_VI);
                return validateFactory.CheckTonTaiDonVi(MaDonVi: model.MA_DON_VI);
            }).WithMessage("DB_MA={DB_MA}: MA_DON_VI={Value} Không tồn tại");
            RuleFor(m => m.LOAI_TAI_SAN_ID).Must((model, code, context) =>
            {
                context.MessageFormatter.AppendArgument("DB_MA", model.DB_MA);
                context.MessageFormatter.AppendArgument("Value", model.LOAI_TAI_SAN_ID);
                return validateFactory.CheckTonTaiLoaiTaiSan(model.LOAI_TAI_SAN_ID);
            }).WithMessage("DB_MA={DB_MA}: LOAI_TAI_SAN_ID={Value} Không tồn tại");
            RuleFor(m => m.DON_VI_BO_PHAN_ID).Must((model, code, context) =>
            {
                context.MessageFormatter.AppendArgument("DB_MA", model.DB_MA);
                context.MessageFormatter.AppendArgument("Value", model.DON_VI_BO_PHAN_ID);
                return validateFactory.CheckTonTaiDonViBoPhan(model.DON_VI_BO_PHAN_ID);
            }).WithMessage("DB_MA={DB_MA} : DON_VI_BO_PHAN_ID={Value} Không tồn tại");
            //RuleFor(m => m.NUO).Must((model, code) =>
            //{
            //    return validateFactory.CheckTonTaiQuocGia(model.DON_VI_BO_PHAN_ID);
            //}).WithMessage("QUOC_GIA_ID Không tồn tại");
            RuleFor(m => m).Custom((model, context) =>
            {
                var result = validateFactory.CheckThongTinTaiSanChiTiet(model, model.LOAI_TAI_SAN_ID, model.LOAI_TAI_SAN_DON_VI_ID);
                if (result != null && result.Count > 0)
                {
                    context.AddFailure($"DB_MA= {model.DB_MA}: " + string.Join("; ", result));
                }
            });
            RuleFor(m => m).Custom((model, context) =>
            {
                var result = validateFactory.CheckBienDongTheoTaiSan(model);
                if (result != null && result.Count > 0)
                {
                    context.AddFailure($"DB_MA= {model.DB_MA}: " + string.Join("; ", result.OrderByDescending(x => x)));
                }
            });
            RuleFor(m => m).Custom((model, context) =>
            {
                var result = validateFactory.CheckLoaiTaisanForNhanTaiSan(model.LOAI_TAI_SAN_ID, model.LOAI_TAI_SAN_DON_VI_ID);
                if (!string.IsNullOrEmpty(result))
                {
                    context.AddFailure($"DB_MA= {model.DB_MA}: " + string.Join("; ", result));
                }
            });
            //RuleFor(m => m.NGAY_NHAP).Must((model, code) =>
            //{
            //    DateTime dt;
            //    string[] formats = { "yyyy-MM-dd HH:mm:ss" };
            //    if (!DateTime.TryParseExact(model.NGAY_NHAP, formats,
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
            //}).WithMessage("NGAY_NHAP không đúng định dạng yyyy-MM-dd HH:mm:ss");
            //RuleFor(m => m.NGAY_DANG_KY).Must((model, code) =>
            //{
            //    DateTime dt;
            //    string[] formats = { "yyyy-MM-dd HH:mm:ss" };
            //    if (!DateTime.TryParseExact(model.NGAY_DANG_KY, formats,
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
            //}).WithMessage("NGAY_DANG_KY không đúng định dạng yyyy-MM-dd HH:mm:ss");
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
        }
    }
}
