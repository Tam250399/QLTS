//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Framework.Validators;
using GS.Web.Models.SHTD;
using System;

namespace GS.Web.Validators.SHTD
{
    public partial class QuyetDinhTichThuValidator : BaseGSValidator<QuyetDinhTichThuModel>
    {
        public QuyetDinhTichThuValidator()
        {
            RuleFor(c => c.TEN).Must((model,code)=>
            { 
                if (model.is_vali)
                {
                    if (model.TEN != null && model.TEN != "")
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
                }).WithMessage("Tên quyết định không được để trống");
            RuleFor(c => c.QUYET_DINH_SO).Must((model, code) =>
            {
                if (model.is_vali)
                {
                    if (model.QUYET_DINH_SO != null && model.QUYET_DINH_SO != "")
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
            RuleFor(c => c.QUYET_DINH_NGAY).Must((model, code) =>
            {
                if (model.is_vali)
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
            RuleFor(c => c.QUYET_DINH_NGAY).Must((model, code) =>
            {
                if (model.is_vali)
                {
                    if (model.QUYET_DINH_NGAY!= null && model.QUYET_DINH_NGAY <= DateTime.Now)
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
            }).WithMessage("Ngày quyết định phải nhỏ hơn ngày hiện tại");

            RuleFor(c => c.NGUON_GOC_ID).Must((model, code) =>
            {
                if (model.is_vali)
                {
                    if (model.NGUON_GOC_ID != null && model.NGUON_GOC_ID >0)
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
            }).WithMessage("Nguồn gốc tài sản không được để trống");
        }
    }
}

