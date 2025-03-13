//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Core.Domain.DanhMuc;
using GS.Web.Factories.NghiepVu;
using GS.Web.Framework.Validators;
using GS.Web.Models.NghiepVu;

namespace GS.Web.Validators.NghiepVu
{
    public partial class YeuCauChiTietValidator : BaseGSValidator<YeuCauChiTietModel>
    {
        public YeuCauChiTietValidator(IYeuCauChiTietModelFactory yeuCauChiTietModelFactory)
        {
            //RuleFor(x => x.HM_GIA_TRI_CON_LAI).Must((model, giaTriConLai) =>
            //{
            //    if (giaTriConLai.HasValue  && giaTriConLai > model.NGUYEN_GIA_SAU_BD)
            //    {
            //        return false;
            //    }
            //    return true;
            //}).WithMessage("Giá trị còn lại không được lớn hơn nguyên giá.");
            RuleFor(x => x.HM_LUY_KE).Must((model, HMLK) =>
            {
                if (HMLK.HasValue && model.HM_LUY_KE.HasValue && HMLK > model.NGUYEN_GIA)
                {
                    return false;
                }
                return true;
            }).WithMessage("Hao mòn luỹ kế không được lớn hơn nguyên giá.");
            RuleFor(x => x.HM_LUY_KE).Must((model, HMLK) =>
            {
                if (HMLK<0)
                {
                    return false;
                }
                return true;
            }).WithMessage("Hao mòn luỹ kế không được âm.");
           
        }

    }
}

