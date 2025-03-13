using FluentValidation;
using GS.Services.SHTD;
using GS.Web.Framework.Validators;
using GS.WebApi.Factories;
using GS.WebApi.Models.TaiSanXacLap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Validator.TaiSanXacLap
{
    public class TSToanDanXuLyValidator : BaseGSValidator<TSToanDanXuLyModel>
    {
        public TSToanDanXuLyValidator(ITaiSanTdService _taiSanTdService)
        {
            //RuleFor(c => c.TAI_SAN_ID).Must((model, code) =>
            //{

            //        if (model.TAI_SAN_ID > 0)
            //        { return true; }
            //        else { return false; }
            //}).WithMessage("TAI_SAN_ID null");
            //RuleFor(c => c.HINH_THUC_XU_LY_ID).Must((model, code) =>
            //{

            //        if (model.HINH_THUC_XU_LY_ID > 0)
            //        { return true; }
            //        else { return false; }

            //}).WithMessage("HINH_THUC_XU_LY_ID null");
            RuleFor(c => c.PHUONG_AN_XU_LY_ID).Must((model, code) =>
            {

                    if (model.PHUONG_AN_XU_LY_ID > 0)
                    { return true; }
                    else { return false; }
            }).WithMessage("PHUONG_AN_XU_LY_ID null");
            RuleFor(c => c.SO_LUONG).Must((model, code) =>
            {
                    if (model.SO_LUONG!=null && model.SO_LUONG > 0)
                    { return true; }
                    else { return false; }
            }).WithMessage("SO_LUONG null");
           
        }
    
    }
}
