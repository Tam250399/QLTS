//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Core;
using GS.Web.Framework.Validators;
using GS.Web.Models.CCDC;
using System;

namespace GS.Web.Validators.CCDC
{
    public partial class ChoThueValidator : BaseGSValidator<ChoThueModel>
    {
        public ChoThueValidator()
        {
            RuleFor(m => m.SO_TIEN_THU_DUOC).NotEmpty().WithMessage("Số tiền thu được không được để trống");
            RuleFor(m => m.NGAY_CHO_THUE_FROM).NotEmpty().WithMessage("Ngày cho thuê từ không được để trống");
            RuleFor(m => m.NGAY_CHO_THUE_TO).NotEmpty().WithMessage("Ngày cho thuê đến không được để trống");
            RuleFor(m => m.SO_TIEN_THU_DUOC).Must((model, code) => {
                if (model.SO_TIEN_THU_DUOC >0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("Số tiền thu được phải lớn hơn 0");
            RuleFor(m => m.QUYET_DINH_NGAY).Must((model, code) => {
                if (model.QUYET_DINH_NGAY != null && model.NgayXuatNhap > model.QUYET_DINH_NGAY)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }).WithMessage("Ngày quyết định phải lớn hơn ngày phân bổ");
            RuleFor(m => m.QUYET_DINH_NGAY).Must((model, code) => {
                if (model.QUYET_DINH_NGAY != null &&  model.QUYET_DINH_NGAY > DateTime.Now)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }).WithMessage("Ngày quyết định phải bé hơn ngày hiện tại");
            RuleFor(m => m.NGAY_CHO_THUE_FROM).Must((model, code) => {
                if (model.NgayXuatNhap < model.NGAY_CHO_THUE_FROM)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("Ngày cho thuê phải lớn hơn ngày phân bổ");
            RuleFor(m => m.NGAY_CHO_THUE_FROM).Must((model, code) => {
                if (model.NGAY_CHO_THUE_FROM < DateTime.Now)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("Ngày cho thuê phải bé hơn ngày hiện tại");
            RuleFor(m => m.HOP_DONG_NGAY).Must((model, code) => {
                if (model.HOP_DONG_NGAY != null &&  model.NgayXuatNhap > model.HOP_DONG_NGAY)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }).WithMessage("Ngày hợp đồng phải lớn hơn ngày phân bổ");
            RuleFor(m => m.HOP_DONG_NGAY).Must((model, code) => {
                if (model.HOP_DONG_NGAY != null &&  model.HOP_DONG_NGAY > DateTime.Now)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }).WithMessage("Ngày hợp đồng phải bé hơn ngày hiện tại");
            RuleFor(m => m.NGAY_CHO_THUE_FROM).Must((model, code) => {
                if (model.NGAY_CHO_THUE_FROM != null && model.NGAY_CHO_THUE_FROM >= model.NGAY_CHO_THUE_TO)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }).WithMessage("Ngày cho thuê từ phải bé hơn hoặc bằng ngày thuê xong");
            //RuleFor(m => m.NGAY_CHO_THUE_TO).Must((model, code) => {
            //    if (model.NGAY_CHO_THUE_TO != null && model.NGAY_CHO_THUE_TO < DateTime.Now)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}).WithMessage("Ngày cho thuê xong phải bé hơn ngày hiện tại");
            RuleFor(m => m.SO_LUONG).Must((model, code) => {
                if (model.SO_LUONG > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }).WithMessage("Số lượng không được để trống");
            RuleFor(m => m.SO_LUONG).Must((model,code)=> {
                if( 0 < model.SO_LUONG  && model.SO_LUONG  <= (model.SoLuongCon))
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }).WithMessage("Số lượng vượt quá cho phép");
            //RuleFor(m => m.HOP_DONG_NGAY).NotEmpty().WithMessage("Đến ngày không được để trống");
            RuleFor(m => m.QUYET_DINH_SO).Must((model, quyetdinhso) =>
            {
                if (quyetdinhso != null && quyetdinhso.Length > 255)
                {
                    return false;
                }
                return true;
            }).WithMessage("Số quyết định vượt quá 255 kí tự");
            RuleFor(m => m.QUYET_DINH_SO).Must((model, quyetdinhso) =>
            {
                if (quyetdinhso != null && quyetdinhso.Length < 3)
                {
                    return false;
                }
                return true;
            }).WithMessage("Số quyết định phải lớn hơn 3 kí tự");
            RuleFor(m => m.CAP_QUYET_DINH).Must((model, cap) =>
            {
                if (cap != null && cap.Length > 255)
                {
                    return false;
                }
                return true;
            }).WithMessage("Cấp quyết định vượt quá 255 kí tự");
            RuleFor(m => m.CAP_QUYET_DINH).Must((model, cap) =>
            {
                if (cap != null && cap.Length < 3)
                {
                    return false;
                }
                return true;
            }).WithMessage("Cấp quyết định phải lớn hơn 3 kí tự");
            RuleFor(m => m.HOP_DONG_SO).Must((model, hd) =>
            {
                if (hd != null && hd.Length > 255)
                {
                    return false;
                }
                return true;
            }).WithMessage("Số hợp đồng vượt quá 255 kí tự");
            RuleFor(m => m.HOP_DONG_SO).Must((model, hd) =>
            {
                if (hd != null && hd.Length < 3)
                {
                    return false;
                }
                return true;
            }).WithMessage("Số hợp đồng phải lớn hơn 3 kí tự");
            RuleFor(m => m.GHI_CHU).Must((model, gc) =>
            {
                if (gc != null && gc.Length > 255)
                {
                    return false;
                }
                return true;
            }).WithMessage("Diễn giải vượt quá 255 kí tự");
            RuleFor(m => m.GHI_CHU).Must((model, gc) =>
            {
                if (gc != null && gc.Length < 3)
                {
                    return false;
                }
                return true;
            }).WithMessage("Diễn giải đồng phải lớn hơn 3 kí tự");
        }
    }
}

