using GS.Core.Domain.Security;
using GS.Services.HeThong;

namespace GS.Web.Framework.Security.Captcha
{
    /// <summary>
    /// Captcha extensions
    /// </summary>
    public static class CaptchaSettingsExtension
    {
        /// <summary>
        /// Get warning message if a selected Captcha version is not supported
        /// </summary>
        /// <param name="captchaSettings"></param>
        /// <param name="NhanHienThiService"></param>
        /// <returns></returns>
        public static string GetWrongCaptchaMessage(this CaptchaSettings captchaSettings,
            INhanHienThiService _nhanHienThiService)
        {
            return _nhanHienThiService.GetGiaTri("Common.WrongCaptchaMessage");
        }
    }
}