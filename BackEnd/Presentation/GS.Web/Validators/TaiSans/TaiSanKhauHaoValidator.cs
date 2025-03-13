//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 28/5/2021
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Services.TaiSans;
using GS.Web.Factories.TaiSans;
using GS.Web.Framework.Validators;
using GS.Web.Models.TaiSans;

namespace GS.Web.Validators.TaiSans
{
    public partial class TaiSanKhauHaoValidator : BaseGSValidator<TaiSanKhauHaoModel>
    {
        public TaiSanKhauHaoValidator(ITaiSanKhauHaoModelFactory taiSanKhauHaoModelFactory, ITaiSanKhauHaoService taiSanKhauHaoService)
        {
            RuleFor(x => x.NGAY_BAT_DAU).Must((model, startdate) =>
            {
                return taiSanKhauHaoService.CheckTrungNgayBatDau(model.NGAY_BAT_DAU, model.TAI_SAN_ID ?? 0, model.ID);
                //return taiSanKhauHaoModelFactory.Checktrungngaybatdau(ngaybatdau: startdate, taiSanKhauHaoid: model.TAI_SAN_ID, id: model.taisankhauhaoID);

            }).WithMessage("Ngày nhập phải lớn hơn ngày bắt đầu tính khấu hao cũ");
            RuleFor(x => x.NGAY_BAT_DAU).Must((model, startdate) =>
            {
                if (model.NGAY_KET_THUC == null)
                {
                    return true;
                }
                return model.NGAY_KET_THUC >= model.NGAY_BAT_DAU;

            }).WithMessage("Ngày bắt đầu phải >= ngày kết thúc");
            RuleFor(x => x.TY_LE_KHAU_HAO).Must((model, tyLeKH) =>
            {
                
                return (model.TY_LE_KHAU_HAO != null);

            }).WithMessage("Tỷ lệ khấu hao không được bỏ trống");

            RuleFor(x => x.SO_THANG_KHAU_HAO).Must((model, tyLeKH) =>
            {
                return (model.SO_THANG_KHAU_HAO != null && model.SO_THANG_KHAU_HAO != 0);
            }).WithMessage("Số tháng khấu hao được bỏ trống");

            RuleFor(x => x.TAI_SAN_ID).Must((model, taiSanId) =>
            {
                return taiSanId > 0;

            }).WithMessage("Tài sản không tồn tại");
            RuleFor(x => x.NGAY_BAT_DAU).Must((model, ngayBatDau) =>
            {
                return (ngayBatDau != null);

            }).WithMessage("Ngày bắt đầu không được bỏ trống");
        }
    }
}

