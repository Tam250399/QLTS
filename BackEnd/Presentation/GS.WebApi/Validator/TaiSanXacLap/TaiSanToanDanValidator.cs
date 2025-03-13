using FluentValidation;
using GS.Core.Domain.SHTD;
using GS.Web.Framework.Validators;
using GS.WebApi.Factories;
using GS.WebApi.Models.TaiSanXacLap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Validator.TaiSanXacLap
{
    public class TaiSanToanDanValidator : BaseGSValidator<GS.WebApi.Models.TaiSanXacLap.TaiSanToanDanModel>
    {
        public TaiSanToanDanValidator(IValidateFactory validateFactory)
        {
            RuleFor(c => c.TEN_LOAI_TAI_SAN).NotEmpty().WithMessage("TEN_LOAI_TAI_SAN NOT NULL");
            RuleFor(c => c.LOAI_HINH_TAI_SAN_ID).NotEmpty().WithMessage("LOAI_HINH_TAI_SAN_ID NOT NULL");
            RuleFor(c => c.LOAI_TAI_SAN_ID).Must((model, code) =>
            {
                if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA)
                {
                    if (model.LOAI_TAI_SAN_ID != null && model.LOAI_TAI_SAN_ID > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }).WithMessage("LOAI_TAI_SAN_ID null");
            RuleFor(c => c.LOAI_TAI_SAN_ID).Must((model, code) =>
            {
                if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.OTO)
                {
                    if (model.LOAI_TAI_SAN_ID != null && model.LOAI_TAI_SAN_ID > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }).WithMessage("LOAI_TAI_SAN_ID null");
            RuleFor(c => c.GIA_TRI_TINH).Must((model, code) =>
            {
                if (model.GIA_TRI_TINH != null && model.SO_LUONG > 0 && (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.TAI_SAN_KHAC))
                {
                    return true;
                }
                else if (model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.TAI_SAN_KHAC)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("GIA_TRI_TINH null");
            RuleFor(c => c.GIA_TRI_TINH).Must((model, code) =>
            {
                if (model.GIA_TRI_TINH != null && (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT || model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA))
                {
                    return true;
                }
                else if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.OTO || model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.TAI_SAN_KHAC)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("GIA_TRI_TINH null");
            RuleFor(c => c.GIA_TRI_TINH).Must((model, code) =>
            {
                if ((model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT || model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA))
                {
                    if (model.GIA_TRI_TINH != null && model.GIA_TRI_TINH > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }

            }).WithMessage("GIA_TRI_TINH phải lớn hơn 0");
            RuleFor(c => c.NAM_SU_DUNG).Must((model, code) =>
            {
                if (model.NAM_SU_DUNG != null && model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA)
                {
                    return true;
                }
                else if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT || model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.OTO || model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.TAI_SAN_KHAC)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("NAM_SU_DUNG null");
            RuleFor(c => c.NAM_SU_DUNG).Must((model, code) =>
            {
                if (model.NAM_SU_DUNG != null && model.NAM_SU_DUNG <= DateTime.Now.Year && model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA)
                {
                    return true;
                }
                else if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT || model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.OTO || model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.TAI_SAN_KHAC)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("NAM_SU_DUNG phải nhỏ hơn hoặc bằng năm hiện tại");
            RuleFor(c => c.DON_VI_TINH).Must((model, code) =>
            {
                if (model.DON_VI_TINH != null && model.DON_VI_TINH.Trim().Length > 1 && (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.TAI_SAN_KHAC))
                {
                    return true;
                }
                else if (model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.TAI_SAN_KHAC)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("DON_VI_TINH null");
        }
    }
}
