//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Core.Domain.SHTD;
using GS.Web.Framework.Validators;
using GS.Web.Models.SHTD;
using System;
using System.Linq;

namespace GS.Web.Validators.SHTD
{
    public partial class XuLyValidator : BaseGSValidator<XuLyModel>
    {
        public XuLyValidator()
        {
            RuleFor(c => c.QUYET_DINH_SO).NotEmpty().WithMessage("Số quyết định không được để trống");
            RuleFor(c => c.QUYET_DINH_NGAY).NotEmpty().WithMessage("Ngày quyết định ngày không được để trống");
            RuleFor(c => c.QUYET_DINH_NGAY).Must((model, code) =>
            {
                if (model.QUYET_DINH_NGAY != null && model.QUYET_DINH_NGAY <= DateTime.Now)
                    return true;
                else
                    return false;
            }).WithMessage("Phải nhỏ hơn hoặc bằng ngày hiện tại");

            //RuleFor(c => c.listQuyetDinh).Must((model, code) =>
            //{
            //    if (model.listQuyetDinh != null && model.listQuyetDinh.Count()>0)
            //        return true;
            //    else
            //        return false;
            //}).WithMessage("Chọn quyết định tịch thu");
        }
    }
}

