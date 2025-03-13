//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/1/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Framework.Validators;
using GS.Web.Models.DanhMuc;

namespace GS.Web.Validators.DanhMuc
{
    public partial class NhomCongCuValidator : BaseGSValidator<NhomCongCuModel>
    {
        public NhomCongCuValidator()
        {
			RuleFor(x => x.TEN).NotEmpty().WithMessage("Tên không được để trống");
			//RuleFor(x => x.DON_VI_TINH_ID).NotEmpty().WithMessage("Đơn vị tính không được để trống");
            RuleFor(x => x.THOI_HAN_PHAN_BO).NotEmpty().WithMessage("Thời hạn phân bổ không được để trống");
            //RuleFor(x => x.THOI_HAN_PHAN_BO).Must((model,code)=> { if (model.THOI_HAN_PHAN_BO < 1) return false;
            //    else return true;
            //}).WithMessage("Thời hạn phân bổ phải lớn hơn 0");
            RuleFor(x => x.TEN).Must((model, code) => {
                if (model.TEN!=null && model.TEN.Trim().Length < 3) return false;
                else return true;
            }).WithMessage("Tên phải nhiều hơn 3 kí tự");
            RuleFor(x => x.TEN).Must((model, code) => {
                if (model.TEN != null && model.TEN.Trim().Length > 255) return false;
                else return true;
            }).WithMessage("Tên phải ít hơn 255 kí tự");
            //RuleFor(x => x.DON_VI_TINH_ID).Must((model, code) => {
            //    if (model.DON_VI_TINH_ID != null && model.DON_VI_TINH_ID.Trim().Length < 3) return false;
            //    else return true;
            //}).WithMessage("Đơn vị tính nhiều phải hơn 3 kí tự");
            RuleFor(x => x.DON_VI_TINH_ID).Must((model, code) => {
                if (model.DON_VI_TINH_ID != null && model.DON_VI_TINH_ID.Trim().Length > 255) return false;
                else return true;
            }).WithMessage("Đơn vị tính phải ít hơn 255 kí tự");
        }
    }
}

