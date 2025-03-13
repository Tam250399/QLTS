//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Core.Domain.SHTD;
using GS.Web.Framework.Validators;
using GS.Web.Models.SHTD;
using System;

namespace GS.Web.Validators.SHTD
{
    public partial class TaiSanTdValidator : BaseGSValidator<TaiSanTdModel>
    {
        public TaiSanTdValidator()
        {
            RuleFor(c => c.TEN).NotEmpty().WithMessage("Tên không được để trống");
            RuleFor(c => c.LOAI_TAI_SAN_ID).Must((model, code) =>
            {
                if(model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA)
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
            }).WithMessage("Cấp nhà không được để trống");
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
            }).WithMessage("Loại xe không được để trống");
            RuleFor(c => c.GIA_TRI_TINH).Must((model, code) =>
            {
                if (model.GIA_TRI_TINH != null && model.GIA_TRI_TINH > 0 && (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.TAI_SAN_KHAC))
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
            }).WithMessage("Giá trị không được để trống");
            RuleFor(c => c.GIA_TRI_TINH).Must((model, code) =>
            {
                if (model.GIA_TRI_TINH != null  && (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT || model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA))
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
            }).WithMessage("Diện tích không được để trống");
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
               
            }).WithMessage("Diện tích phải lớn hơn 0");
            RuleFor(c => c.GIA_TRI_XAC_LAP).Must((model, code) =>
            {
                if (model.GIA_TRI_XAC_LAP != null && model.GIA_TRI_XAC_LAP > 0 )
                {
                    return true;
                }
                else if(model.GIA_TRI_XAC_LAP == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("Giá trị xác lập phải lớn hơn 0");
            //RuleFor(c => c.NGAY_SU_DUNG).Must((model, code) =>
            //{
            //    if (model.NGAY_SU_DUNG != null && model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA)
            //    {
            //        return true;
            //    }
            //    else if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT ||  model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.OTO || model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.TAI_SAN_KHAC)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}).WithMessage("Ngày sử dụng không được để trống");
            //RuleFor(c => c.NGAY_SU_DUNG).Must((model, code) =>
            //{
            //    if (model.NGAY_SU_DUNG != null && model.NGAY_SU_DUNG <= DateTime.Now && model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA)
            //    {
            //        return true;
            //    }
            //    else if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT || model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.OTO || model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.TAI_SAN_KHAC)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}).WithMessage("Ngày sử dụng phải nhỏ hơn ngày hiện tại");
            RuleFor(c => c.NamSuDung).Must((model, code) =>
            {
                if (model.NamSuDung != null && model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA)
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
            }).WithMessage("Năm sử dụng không được để trống");
            RuleFor(c => c.NamSuDung).Must((model, code) =>
            {
                if (model.NamSuDung != null && model.NamSuDung <= DateTime.Now.Year && model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA)
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
            }).WithMessage("Năm sử dụng phải nhỏ hơn năm hiện tại");
            RuleFor(c => c.NamSuDung).Must((model, code) =>
            {
                if (model.NamSuDung != null && model.NamSuDung > 0 && model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA)
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
            }).WithMessage("Năm sử dụng phải lớn hơn 0");
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
            }).WithMessage("Đơn vị tính không được để trống");
            //RuleFor(c => c.GIA_TRI).Must((model, code) =>
            //{
            //if (model.NGUYEN_GIA != null && model.GIA_TRI!=null && model.GIA_TRI <= model.NGUYEN_GIA)
            //    {
            //        return true;
            //    }
            //else if (model.NGUYEN_GIA != null && model.GIA_TRI != null)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}).WithMessage("Giá trị phải nhỏ hơn hoặc bằng nguyên giá");
        }
    }
}

