//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Framework.Validators;
using GS.Web.Models.CCDC;
using System;
using System.Linq;

namespace GS.Web.Validators.CCDC
{
    public partial class CongCuValidator : BaseGSValidator<CongCuModel>
    {
        public CongCuValidator()
        {
            RuleFor(m => m.TenXuatNhap).NotEmpty().WithMessage("Tên không được để trống");
            RuleFor(m => m.NgayXuatNhap).NotEmpty().WithMessage("Ngày tăng không được để trống");
            RuleFor(m => m.NgayXuatNhap).Must((model, code) =>
            {
                return model.NgayXuatNhap < DateTime.Now ? true : false;
            }).WithMessage("Ngày tăng không được lớn hơn ngày hiện tại");
            RuleFor(m => m.MucDichXuatNhapId).Must((model, code) =>
            {
                return model.MucDichXuatNhapId > 0 ? true : false;
            }).WithMessage("Chọn lý do tăng");
            RuleFor(m => m.TRANG_THAI_ID).Must((model, code) =>
            {
                return model.TRANG_THAI_ID > 0 ? true : false;
            }).WithMessage("Chọn trạng thái");
        }
    }
}
