using FluentValidation;
using GS.Core;
using GS.Core.Domain.TaiSans;
using GS.Web.Framework.Validators;
using GS.WebApi.Factories;
using GS.WebApi.Models.TaiSan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Validator.TaiSan
{
    public partial class KhaiTacTaiSanValidator : BaseGSValidator<DBKhaiThacTaiSanModel>
    {
        public KhaiTacTaiSanValidator(IValidateFactory validateFactory)
        {
            RuleFor(m => m.DON_VI_ID).Must((model, code) =>
            {
                return validateFactory.CheckTonTaiDonVi(model.DON_VI_ID.Value);
            }).WithMessage("DON_VI_ID not exist");
            RuleFor(m => m.HOP_DONG_SO).Must((model, code) =>
            {
                if (model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.CHO_THUE_TS || model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.LDLK || model.LOAI_HINH_KHAI_THAC_ID == 0/*cập nhật số tiền khai thác*/)
                {
                    return (!string.IsNullOrEmpty(model.HOP_DONG_SO.Trim()));
                }
                return true;
            }).WithMessage("HOP_DONG_SO is null");
            RuleFor(m => m.NGAY_KHAI_THAC).Must((model, code) =>
            {
                var ngayKhaiThac = model.NGAY_KHAI_THAC.toDateSys(CommonHelper.DATE_FORMAT_DB);
                if(!ngayKhaiThac.HasValue)
                {
                    return false;
                }
                return true;
            }).WithMessage("NGAY_KHAI_THAC không đúng định dạng");
            RuleFor(m => m.QUYET_DINH_NGAY).Must((model, code) =>
            {
                var ngayKhaiThac = model.QUYET_DINH_NGAY.toDateSys(CommonHelper.DATE_FORMAT_DB);
                if (!ngayKhaiThac.HasValue)
                {
                    return false;
                }
                return true;
            }).WithMessage("QUYET_DINH_NGAY không đúng định dạng");
            // RuleFor(m => m.LST_MA_TAI_SAN).Custom((model, context) =>
            //{
            //    if (model == null)
            //    {
            //        context.AddFailure("LST_MA_TAI_SAN null");
            //    }
            //    else
            //    {
            //        var result = validateFactory.CheckTonTaiTaiSan(model);
            //        if (result.Count > 0)
            //        {
            //            context.AddFailure(string.Join("; ", result));
            //        }
            //    }

            //});
            RuleFor(m => m).Custom((model, context) =>
            {
                if (model == null)
                {
                    context.AddFailure("LST_MA_TAI_SAN_DB null");
                }
                else
                {
                    List<string> ListError = new List<string>();
                    foreach (var item in model.LST_MA_TAI_SAN_DB)
                    {
                        if(!validateFactory.CheckTonTaiTaiSanDongBo(item))
                        {
                            ListError.Add($"MA_TAI_SAN_DB={item} Không tồn tại");
                        }
                    }
                    
                    if (ListError.Count > 0)
                    {
                        context.AddFailure(string.Join("; ", ListError));
                    }
                }

            });
            RuleFor(m => m.ID).Must((model, code,contex) =>
              {
                  return validateFactory.CheckTonTaiKhaiThac(model.ID);
              }).WithMessage("ID not exist");
        }
    }
}
