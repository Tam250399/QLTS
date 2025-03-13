//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/2/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Core;
using GS.Web.Framework.Validators;
using GS.Web.Models.CCDC;

namespace GS.Web.Validators.CCDC
{
    public partial class SuaChuaBaoDuongValidator : BaseGSValidator<SuaChuaBaoDuongModel>
    {
        public SuaChuaBaoDuongValidator()
        {
            RuleFor(m => m.NGAY_SUA_CHUA_FROM).NotEmpty().WithMessage("Ngày sửa chữa không được để trống");
            //RuleFor(m => m.NGAY_SUA_CHUA_TO).NotEmpty().WithMessage("Đến ngày không được để trống");
            RuleFor(m => m.SO_LUONG_SUA_CHUA).NotEmpty().WithMessage("Số lượng sửa chữa không được để trống");
            RuleFor(m => m.GIA_TRI_SUA_CHUA).NotEmpty().WithMessage("Giá trị sửa chữa không được để trống");
            RuleFor(m => m.SO_LUONG_SUA_CHUA).Must((model,code)=>
            {
                if (model.SO_LUONG_SUA_CHUA > 0 && model.SO_LUONG_SUA_CHUA <= model.SoLuongText.ToNumber())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("Số lượng vượt quá cho phép");
            //RuleFor(m => m.NGAY_SUA_CHUA_FROM).Must((model, code) =>
            //{
            //    if (model.NGAY_SUA_CHUA_FROM <= model.NGAY_SUA_CHUA_TO)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}).WithMessage("Ngày sửa chữa từ phải nhỏ hơn ngày sửa chữa đến");
            //RuleFor(m => m.NGAY_SUA_CHUA_TO).Must((model, code) =>
            //{
            //    if (model.NGAY_SUA_CHUA_FROM <= model.NGAY_SUA_CHUA_TO)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}).WithMessage("Ngày sửa đến phải lớn hơn ngày sửa chữa từ");
            RuleFor(m => m.GIA_TRI_SUA_CHUA).Must((model, code) =>
            {
                if (model.GIA_TRI_SUA_CHUA <= model.TongNguyenGia)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("Cho phí sửa chữa phải nhỏ hơn tổng nguyên giá");
            RuleFor(m => m.CHUNG_TU_NGAY).Must((model, code) =>
            {
                if ((model.CHUNG_TU_NGAY == null) ||(model.CHUNG_TU_NGAY !=null && model.CHUNG_TU_NGAY >= model.NgayXnBefore))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("Ngày chứng từ/hóa đơn phải lớn hơn ngày phân bổ");
            RuleFor(m => m.NGAY_QUYET_DINH).Must((model, code) =>
            {
                if ((model.NGAY_QUYET_DINH != null && model.NGAY_QUYET_DINH >= model.NgayXnBefore) || model.NGAY_QUYET_DINH==null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("Ngày quyết định phải lớn hơn ngày phân bổ");
            RuleFor(m => m.NGAY_SUA_CHUA_FROM).Must((model, code) =>
            {
                if (model.NGAY_SUA_CHUA_FROM != null && model.NGAY_SUA_CHUA_FROM >= model.NgayXnBefore)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("Ngày sửa chữa phải lớn hơn ngày phân bổ");
            RuleFor(m => m.CAP_QUYET_DINH).Must((model, code) =>
            {
                if (model.CAP_QUYET_DINH != null && model.CAP_QUYET_DINH.Length >= 1 && model.CAP_QUYET_DINH.Length <= 255)
                {
                    return true;
                }
                else if (model.CAP_QUYET_DINH == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("Cấp quyết định vượt quá giới hạn cho phép");
            RuleFor(m => m.GHI_CHU).Must((model, code) =>
            {
                if (model.CAP_QUYET_DINH != null && model.CAP_QUYET_DINH.Length <= 255)
                {
                    return true;
                }
                else if (model.CAP_QUYET_DINH == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("Diễn giải vượt quá giới hạn cho phép");
            RuleFor(m => m.SO_QUYET_DINH).Must((model, quyetdinhso) =>
            {
                if (quyetdinhso != null && quyetdinhso.Length > 255)
                {
                    return false;
                }
                return true;
            }).WithMessage("Số quyết định vượt quá 255 kí tự");
            RuleFor(m => m.SO_QUYET_DINH).Must((model, quyetdinhso) =>
            {
                if (quyetdinhso != null && quyetdinhso.Length < 3)
                {
                    return false;
                }
                return true;
            }).WithMessage("Số quyết định phải lớn hơn 3 kí tự");
            RuleFor(m => m.GHI_CHU).Must((model, ghichu) =>
            {
                if (ghichu != null && ghichu.Length < 3)
                {
                    return false;
                }
                return true;
            }).WithMessage("Diễn giải phải lớn hơn 3 kí tự");
            RuleFor(m => m.GHI_CHU).Must((model, ghichu) =>
            {
                if (ghichu != null && ghichu.Length > 255)
                {
                    return false;
                }
                return true;
            }).WithMessage("Diễn giải phải lớn hơn 255 kí tự");

        }
    }
}

