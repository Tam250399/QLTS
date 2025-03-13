//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 7/12/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Core.Infrastructure;
using GS.Services.SHTD;
using GS.Web.Factories.SHTD;
using GS.Web.Framework.Validators;
using GS.Web.Models.SHTD;
using System;
using System.Linq;

namespace GS.Web.Validators.SHTD
{
    public partial class ThuChiValidator : BaseGSValidator<ThuChiModel>
    {
        public ThuChiValidator( IThuChiModelFactory thuChiModelFactory)
        {
            RuleFor(c => c.SO_TIEN_PHAI_THU).NotEmpty().WithMessage("Số tiền phải thu không được để trống");
            RuleFor(c => c.SO_TIEN_CON_PHAI_THU).NotEmpty().WithMessage("Số tiền còn phải thu không được để trống");
            RuleFor(c => c.SO_TIEN_THU).NotEmpty().WithMessage("Số tiền đã thu không được để trống");
            RuleFor(c => c.NGAY_THU).NotEmpty().WithMessage("Ngày thu không được để trống");
            RuleFor(c => c.CHI_PHI).Must((model, chiPhi) =>
            {
                
                return thuChiModelFactory.CheckValidChiPhiDauTien(model);
            }).WithMessage("Chi phí xử lý không được để trống");
            RuleFor(c => c.SO_TIEN_CON_PHAI_THU).Must((model, code) =>
            {
                if (model.SO_TIEN_CON_PHAI_THU != null && model.SO_TIEN_CON_PHAI_THU != null && model.SO_TIEN_CON_PHAI_THU <= model.SO_TIEN_PHAI_THU)
                    return true;
                else
                    return false;
            }).WithMessage("Số tiền còn phải thu phải nhỏ hơn hoặc bằng số tiền phải thu");
            RuleFor(c => c.SO_TIEN_THU).Must((model, code) =>
            {
                if (model.SO_TIEN_PHAI_THU != null && model.SO_TIEN_THU != null && model.SO_TIEN_THU <= model.SO_TIEN_PHAI_THU)
                    return true;
                else
                    return false;
            }).WithMessage("Số tiền đã thu phải nhỏ hơn hoặc bằng số tiền phải thu");
            RuleFor(c => c.NGAY_THU).Must((model, code) =>
            {
                if (model.NGAY_THU != null && model.NGAY_THU <= DateTime.Now)
                    return true;
                else
                    return false;
            }).WithMessage("Phải nhỏ hơn hoặc bằng ngày hiện tại");
            RuleFor(c => c.TG_NGAY_NOP).Must((model, code) =>
            {
                if (model.TG_NGAY_NOP != null && model.TG_NGAY_NOP <= DateTime.Now)
                    return true;
                else if(model.TG_NGAY_NOP == null)
                    return true;
                else
                    return false;
            }).WithMessage("Phải nhỏ hơn hoặc bằng ngày hiện tại");

            RuleFor(c => c.NGAY_THU).Must((model, code) =>
            {
                var _itemService = EngineContext.Current.Resolve<IThuChiService>();
                var ThuChiDauTien = _itemService.GetThuChiDauTien(model.LIST_XU_LY_ID);
                var isValid = true;
                if (ThuChiDauTien != null && model.ID == ThuChiDauTien.ID)
                {
                    // Check tồn tại
                    isValid = !_itemService.GetThuChis(ListThuChiId: model.LIST_XU_LY_ID).Any(c => c.ID != ThuChiDauTien.ID && model.NGAY_THU > c.NGAY_THU);
                   
                }
                return isValid;
            }).WithMessage("Đã tồn tại ngày thu chi nhỏ hơn ngày thu chi đầu tiên");
        }
    }
}

