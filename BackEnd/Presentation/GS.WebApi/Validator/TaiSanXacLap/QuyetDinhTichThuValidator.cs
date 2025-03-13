using FluentValidation;
using GS.Web.Framework.Validators;
using GS.WebApi.Factories;
using GS.WebApi.Models.TaiSanXacLap;
using GS.WebApi.Models.TaiSan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Validator.TaiSan
{
    public class QuyetDinhTichThuValidator : BaseGSValidator<QuyetDinhTichThuModel>
    {
        public QuyetDinhTichThuValidator(IValidateFactory validateFactory)
        {
            RuleFor(c => c.DON_VI_ID).NotNull().WithMessage("DON_VI_ID null");
            RuleFor(c => c.NGUOI_TAO_ID).NotNull().WithMessage("NGUOI_TAO_ID null");
            RuleFor(c => c.TEN).Must((model, code) =>
                {

                        if (model.TEN != null && model.TEN != "")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                }).WithMessage("TEN null");
                RuleFor(c => c.QUYET_DINH_SO).Must((model, code) =>
                {

                        if (model.QUYET_DINH_SO != null && model.QUYET_DINH_SO != "")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    
                }).WithMessage("QUYET_DINH_SO null");
                RuleFor(c => c.QUYET_DINH_NGAY).Must((model, code) =>
                {

                        if (model.QUYET_DINH_NGAY != null)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                }).WithMessage("QUYET_DINH_NGAY null");
                RuleFor(c => c.QUYET_DINH_NGAY).Must((model, code) =>
                {

                        if (model.QUYET_DINH_NGAY != null && model.QUYET_DINH_NGAY <= DateTime.Now)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    
                }).WithMessage("QUYET_DINH_NGAY phải nhỏ hơn hoặc bằng ngày hiện tại");
                RuleFor(c => c.NGUON_GOC_ID).Must((model, code) =>
                {

                        if (model.NGUON_GOC_ID != null && model.NGUON_GOC_ID > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                }).WithMessage("NGUON_GOC_ID null");
            
        }
    }
}
