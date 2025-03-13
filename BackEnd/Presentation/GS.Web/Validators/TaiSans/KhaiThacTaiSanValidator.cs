//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/7/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Core.Domain.TaiSans;
using GS.Services.TaiSans;
using GS.Web.Framework.Validators;
using GS.Web.Models.TaiSans;

namespace GS.Web.Validators.TaiSans
{
    public partial class KhaiThacTaiSanValidator : BaseGSValidator<KhaiThacTaiSanModel>
    {
        private readonly IKhaiThacService _khaiThacService;

        public KhaiThacTaiSanValidator(IKhaiThacService khaiThacService)
        {
            this._khaiThacService = khaiThacService;
            RuleFor(x => x.TU_NGAY).NotEmpty().WithMessage("Ngày bắt đầu kỳ khai thác không được bỏ trống");
            RuleFor(x => x.DEN_NGAY).NotEmpty().WithMessage("Ngày Kết thúc kỳ khai thác không được bỏ trống");
            RuleFor(x => x.SO_TIEN).NotEmpty().WithMessage("Số tiền thu được của kỳ không được bỏ trống");
            RuleFor(x => x.DEN_NGAY).Must((model, ngaykhaithac) =>
            {
                if (model.TU_NGAY > model.DEN_NGAY) {
                    return false;
                }
                return true;
            }).WithMessage("Ngày Kết thúc kỳ khai thác không được nhỏ hơn ngày bắt đầu kỳ khai thác");
            RuleFor(x => x.DEN_NGAY).Must((model, ngaykhaithac) =>
            {
                if (model.DEN_NGAY != null&& model.TU_NGAY != null)
                {
                    int namBatDau = model.TU_NGAY.Value.Year;
                    int namKetThuc = model.DEN_NGAY.Value.Year;
                    if (namBatDau != namKetThuc)
                    {
                        return false;
                    }
                    return true;
                }
                return true;
            }).WithMessage("Ngày Kết thúc kỳ khai thác và ngày bắt đầu kỳ khai thác phải nằm trong cùng 1 năm");
            RuleFor(x => x.DEN_NGAY).Must((model, ngaykhaithac) =>
            {
                if (model.DEN_NGAY != null )
                {
                    var khaiThac = _khaiThacService.GetKhaiThacById(model.KHAI_THAC_ID);
                    if (khaiThac != null)
                    {
                        if (khaiThac.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.LDLK || khaiThac.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.CHO_THUE_TS)
                        {
                            if (model.TU_NGAY < khaiThac.KHAI_THAC_NGAY_TU)
                            {
                                return false;
                            }
                           
                        }
                        
                    }
                    return true;
                }
                return true;
            }).WithMessage("Ngày bắt đầu cập nhật số tiền không được nhỏ hơn ngày bắt đầu khai thác");
            
        }
    }
}

