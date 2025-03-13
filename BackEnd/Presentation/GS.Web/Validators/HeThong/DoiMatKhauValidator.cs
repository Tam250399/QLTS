using FluentValidation;
using GS.Services.HeThong;
using GS.Web.Factories.HeThong;
using GS.Web.Framework.Validators;
using GS.Web.Models.HeThong;
using System.Text.RegularExpressions;

namespace GS.Web.Validators.HeThong
{
    public partial class DoiMatKhauValidator : BaseGSValidator<DoiMatKhauModel>
    {
        public DoiMatKhauValidator(INhanHienThiService _nhanHienThiService, INguoiDungModelFactory nguoiDungModelFactory)
        {
            RuleFor(x => x.MATKHAUMOI).NotEmpty().WithMessage(_nhanHienThiService.GetGiaTri("Mật khẩu mới không được để trống"));
            RuleFor(x => x.XACNHANMK).NotEmpty().WithMessage(_nhanHienThiService.GetGiaTri("Xác nhận mật khẩu không được để trống"));

            RuleFor(x => x.MATKHAUMOI).Must((model, matkhau) => {
                //8 ký tự, 1 chữ hoa, một chữ thường, một chữ số
                var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d].{8,}$");
                return regex.IsMatch(matkhau ?? "");
            }).WithMessage("Mật khẩu phải có ít nhất 8 ký tự, một chữ hoa, một chữ thường, một chữ số");
            RuleFor(x => x.XACNHANMK).Equal(x => x.MATKHAUMOI).WithMessage(_nhanHienThiService.GetGiaTri("Xác nhận mật khẩu không trùng với mật khẩu mới"));
            RuleFor(x => x.MATKHAUCU).Must((model, code) =>
            {
                return nguoiDungModelFactory.CheckResetPassword(model.MATKHAUCU, model.isResetmk);
            }).WithMessage("Mật khẩu cũ không được để trống");
            RuleFor(x => x.IDNguoiDung).NotEmpty().WithMessage(_nhanHienThiService.GetGiaTri("Mật khẩu mới không được để trống"));
        }
    }
}
