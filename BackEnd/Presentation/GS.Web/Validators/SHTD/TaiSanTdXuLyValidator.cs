//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Framework.Validators;
using GS.Web.Models.SHTD;

namespace GS.Web.Validators.SHTD
{
    public partial class TaiSanTdXuLyValidator : BaseGSValidator<TaiSanTdXuLyModel>
    {
        public TaiSanTdXuLyValidator()
        {

            RuleFor(c => c.TAI_SAN_ID).Must((model, code) =>
              {
                  if (model.is_vali == true && model.isThemNhieuTaiSan == false)
                  {
                      if (model.TAI_SAN_ID > 0)
                      { return true; }
                      else { return false; }
                  }
                  else { return true; }
              }).WithMessage("Trường không được để trống");
            RuleFor(c => c.HINH_THUC_XU_LY_ID).Must((model, code) =>
            {
                if (model.is_vali)
                {
                    if (model.HINH_THUC_XU_LY_ID > 0)
                    { return true; }
                    else { return false; }
                }
                else { return true; }
            }).WithMessage("Trường không được để trống");
            RuleFor(c => c.PHUONG_AN_XU_LY_ID).Must((model, code) =>
            {
                if (model.is_vali)
                {
                    if (model.PHUONG_AN_XU_LY_ID > 0)
                    { return true; }
                    else { return false; }
                }
                else { return true; }
            }).WithMessage("Trường không được để trống");
            //RuleFor(c => c.CHI_PHI_XU_LY).Must((model, code) =>
            //{
            //    if (model.is_vali)
            //    {
            //        if (model.CHI_PHI_XU_LY >= 0)
            //        { return true; }
            //        else { return false; }
            //    }
            //    else { return true; }
            //}).WithMessage("Trường không được để trống");
            RuleFor(c => c.SO_LUONG).Must((model, code) =>
            {
                if (model.is_vali)
                {
                    if (model.SO_LUONG > 0)
                    { return true; }
                    else { return false; }
                }
                else { return true; }
            }).WithMessage("Trường không được để trống");
            //RuleFor(c => c.SO_LUONG).Must((model, code) =>
            //{
            //    if (model.is_vali && model.isThemNhieuTaiSan == false)
            //    {
            //        if (model.SO_LUONG <= model.SoLuongCon)
            //        { return true; }
            //        else { return false; }
            //    }
            //    else { return true; }
            //}).WithMessage("Quá số lượng cho phép");
        }
    }   
}

