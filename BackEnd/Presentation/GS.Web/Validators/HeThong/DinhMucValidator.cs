//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 18/6/2021
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Factories.HeThong;
using GS.Web.Framework.Validators;
using GS.Web.Models.HeThong;

namespace GS.Web.Validators.HeThong
{
    public partial class DinhMucValidator : BaseGSValidator<DinhMucModel>
    {
        public DinhMucValidator(IDinhMucModelFactory dinhMucModelFactory)
        {
            //RuleFor(x => x.MA).NotEmpty().WithMessage("Mã không được để trống");
            //RuleFor(x => x.MA).MaximumLength(3).WithMessage("Mã không được lớn hơn 3 ký tự");
            //RuleFor(x => x.MA).Must((model, code) => {
            //    return dinhMucModelFactory.CheckMaDinhMuc(model.ID, model.MA);
            //}).WithMessage("Mã định mức đã tồn tại");
            RuleFor(x => x.QUYET_DINH_SO).NotEmpty().WithMessage("Số quyết định không được để trống");
            RuleFor(x => x.QUYET_DINH_SO).Must((model, code) => {
                return dinhMucModelFactory.CheckSoQuyetDinhDinhMuc(model.ID, model.QUYET_DINH_SO);
            }).WithMessage("Mã định mức đã tồn tại");
            RuleFor(x => x.QUYET_DINH_NGAY).NotEmpty().WithMessage("Ngày quyết định không được để trống");
            RuleFor(x => x.TU_NGAY).NotEmpty().WithMessage("Ngày bắt đầu không được để trống");
            RuleFor(x => x.DEN_NGAY).Must((model, denngay) =>
            {
                if (denngay.HasValue)
                {
                    if(denngay<= model.TU_NGAY)
                    {
                        return false;
                    }
                    return true;
                }
                return true;
            }).WithMessage("Ngày kết thúc không được nhỏ hơn hoặc bằng ngày bắt đầu");
        }
    }
}

