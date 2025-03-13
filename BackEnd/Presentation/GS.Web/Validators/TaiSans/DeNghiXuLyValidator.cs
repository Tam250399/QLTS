//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/11/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Factories.TaiSans;
using GS.Web.Framework.Validators;
using GS.Web.Models.TaiSans;

namespace GS.Web.Validators.TaiSans
{
    public partial class DeNghiXuLyValidator : BaseGSValidator<DeNghiXuLyModel>
    {
        public DeNghiXuLyValidator(IDeNghiXuLyModelFactory deNghiXuLyModelFactory)
        {
            RuleFor(x => x.SO_PHIEU).NotEmpty().WithMessage("Số phiếu không được để trống");
            RuleFor(x => x.NGAY_DE_NGHI).NotEmpty().WithMessage("Ngày đề nghị không được để trống");
            RuleFor(x => x.SO_PHIEU).Must((model, code) =>
            {
                return !deNghiXuLyModelFactory.CheckTrungSoPhieuTrongNgay(model.ID,model.SO_PHIEU, model.NGAY_DE_NGHI,model.DON_VI_ID);
            }).WithMessage("Số phiếu trùng");
            //RuleFor(x => x.NGAY_XU_LY).NotEmpty().WithMessage("Ngày xử lý không được để trống");
        }
    }
}

