using FluentValidation;
using GS.Web.Framework.Validators;
using GS.Web.Models.CCDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GS.Core.Domain.CCDC;
using GS.Core;

namespace GS.Web.Validators.CCDC
{
    public partial class GiamDieuChuyenValidator : BaseGSValidator<GiamDieuChuyenModel>
    {
        public GiamDieuChuyenValidator(IWorkContext _workContext)
        {
            RuleFor(m => m.NGAY_DIEU_CHUYEN).NotEmpty().WithMessage("Ngày chuyển không được để trống");
            RuleFor(m => m.TenDonViTo).Must((model, code) =>
            {
                if (model.LoaiXuatNhapId == (int)enumLoaiXuatNhap.DIEU_CHUYEN && model.DON_VI_ID <= 0)
                {
                    return false;                    
                }
                return true;

            }).WithMessage("Đơn vị điều chuyển không được để trống");
            RuleFor(m => m.TenDonViTo).Must((model, code) =>
            {
                if (model.LoaiXuatNhapId == (int)enumLoaiXuatNhap.DIEU_CHUYEN && model.DON_VI_ID == _workContext.CurrentDonVi.ID)
                {
                    return false;
                }
                return true;

            }).WithMessage("Đơn vị điều chuyển trùng");
        }
    }
}
