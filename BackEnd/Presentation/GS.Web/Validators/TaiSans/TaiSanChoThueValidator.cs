//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 5/3/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Factories.TaiSans;
using GS.Web.Framework.Validators;
using GS.Web.Models.TaiSans;

namespace GS.Web.Validators.TaiSans
{
    public partial class TaiSanChoThueValidator : BaseGSValidator<TaiSanChoThueModel>
    {
        public TaiSanChoThueValidator(ITaiSanChoThueModelFactory taisanchothueModelFactory)
        {
            RuleFor(x => x.HOP_DONG_SO).NotEmpty().WithMessage("Hợp đồng không được để trống");
            RuleFor(x => x.HOP_DONG_NGAY).NotEmpty().WithMessage("Ngày hợp đồng không được để trống");
            RuleFor(x => x.NGAY_CHO_THUE_FROM).NotEmpty().WithMessage("Ngày bắt đầu sử dụng chung không được để trống ");
            //RuleFor(x => x.DON_GIA_CHO_THUE).NotEmpty().WithMessage("Đơn giá sử dụng chung không được để trống ");
            RuleFor(x => x.THU_TU_CHO_THUE).NotEmpty().WithMessage("Thu từ sử dụng chung không được để trống ");
            //RuleFor(x => x.NOP_NGAN_SACH).NotEmpty().WithMessage("Tiền nộp ngân sách không được để trống ");
            //RuleFor(x => x.GIU_LAI_DON_VI).NotEmpty().WithMessage("Tiền giữ lại đơn vị không được để trống ");
            RuleFor(x => x.NGAY_CHO_THUE_FROM).Must((model, ngaysudungchung) =>
            {
                if (ngaysudungchung.HasValue && model.ngaykekhaits.HasValue)
                {
                    if(ngaysudungchung < model.ngaykekhaits)
                    {
                        return false;
                    }
                    return true;
                }
                return true;
            }).WithMessage("Ngày bắt đầu không được nhỏ hơn ngày kê khai tài sản.");
            RuleFor(x => x.HOP_DONG_NGAY).Must((model, ngayhopdong) =>
            {
                if (ngayhopdong.HasValue && model.ngaykekhaits.HasValue)
                {
                    if(ngayhopdong < model.ngaykekhaits)
                    {
                        return false;
                    }
                    return true;
                }
                return true;
            }).WithMessage("Ngày văn bản không được nhỏ hơn ngày kê khai tài sản.");
            RuleFor(x => x.NGAY_CHO_THUE_FROM).Must((model, code) => {
                if (model.NGAY_CHO_THUE_FROM != null) { 
                    return taisanchothueModelFactory.CheckTimeChoThue(model.TAI_SAN_ID, model.NGAY_CHO_THUE_FROM, model.ID);
                }
                return true;
            }).WithMessage("Tài sản đã được đăng ký sử dụng chung");
            RuleFor(x => x.NGAY_CHO_THUE_TO).Must((model, ngaychothueden) =>
            {
                if (ngaychothueden.HasValue && model.NGAY_CHO_THUE_FROM.HasValue)
                {
                    if (ngaychothueden < model.NGAY_CHO_THUE_FROM)
                    {
                        return false;
                    }
                    return true;
                }
                return true;
            }).WithMessage("Ngày kết thúc không được nhỏ hơn ngày bắt đầu.");
        }
    }
}

