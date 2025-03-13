//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 25/3/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Framework.Validators;
using GS.Web.Models.DanhMuc;

namespace GS.Web.Validators.DanhMuc
{
    public partial class DonViChuyenDoiValidator : BaseGSValidator<DonViChuyenDoiModel>
    {
        public DonViChuyenDoiValidator()
        {
            RuleFor(x => x.TEN).NotEmpty().WithMessage("Tên không được để trống");

            //RuleFor(x => x.QUYET_DINH_GIAO_VON_SO).Must((model, code) =>
            //{
            //    if (model.DA_CO_QUYET_DINH_GIAO_VON)
            //    {
            //        if (!string.IsNullOrEmpty(model.QUYET_DINH_GIAO_VON_SO))
            //        {
            //            return true;
            //        }
            //        else
            //        {
            //            return false;
            //        }
            //    }
            //    else
            //    {
            //        return true;
            //    }
            //}).WithMessage("Số quyết định không được để trống");

            RuleFor(x => x.QUYET_DINH_GIAO_VON_NGAY).Must((model, code) => 
            {
                if (model.DA_CO_QUYET_DINH_GIAO_VON)
                {
                    if (model.QUYET_DINH_GIAO_VON_NGAY != null)
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
            }).WithMessage("Ngày quyết định không được để trống");

            RuleFor(x => x.QUYET_DINH_SO).Must((model, code) =>
            {
                if (!model.DA_CO_QUYET_DINH_GIAO_VON)
                {
                    if (!string.IsNullOrEmpty(model.QUYET_DINH_SO))
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
            }).WithMessage("Số quyết định không được để trống");

            RuleFor(x => x.QUYET_DINH_NGAY).Must((model, code) =>
            {
                if (!model.DA_CO_QUYET_DINH_GIAO_VON)
                {
                    if (model.QUYET_DINH_NGAY != null)
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
            }).WithMessage("Ngày quyết định không được để trống");

            RuleFor(x => x.LOAI_DON_VI_ID).NotEmpty().WithMessage("Loại đơn vị không được để trống");

            RuleFor(x => x.NGAY_TAO).NotEmpty().WithMessage("Ngày tạo không được để trống");
        }
    }
}

