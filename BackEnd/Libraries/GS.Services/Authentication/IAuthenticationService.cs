using GS.Core.Domain.HeThong;

namespace GS.Services.Authentication
{
    public partial interface IAuthenticationService
    {
        /// <summary>
        /// SignIn
        /// </summary>
        /// <param name="nguoiDung"></param>
        /// <param name="isPersistent"></param>
        void DangNhap(NguoiDung nguoiDung, bool isPersistent);

        /// <summary>
        /// Sign out
        /// </summary>
        void DangXuat();

        /// <summary>
        /// Get authenticated customer
        /// </summary>
        /// <returns>Customer</returns>
        NguoiDung GetNguoiDungDangNhap();
        NguoiDung ValidateToken(string tokenString);
        string GenerateToken(NguoiDung item);
    }
}
