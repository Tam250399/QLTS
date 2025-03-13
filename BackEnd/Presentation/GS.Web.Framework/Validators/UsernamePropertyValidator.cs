using FluentValidation.Validators;
using GS.Core.Domain.CauHinh;

namespace GS.Web.Framework.Validators
{
    /// <summary>
    /// Username validator
    /// </summary>
    public class UsernamePropertyValidator : PropertyValidator
    {
        private readonly CauHinhNguoiDung _customerSettings;

        /// <summary>
        /// Ctor
        /// </summary>
        public UsernamePropertyValidator(CauHinhNguoiDung customerSettings)
            : base("Username is not valid")
        {
            this._customerSettings = customerSettings;
        }

        /// <summary>
        /// Is valid?
        /// </summary>
        /// <param name="context">Validation context</param>
        /// <returns>Result</returns>
        protected override bool IsValid(PropertyValidatorContext context)
        {
            return IsValid(context.PropertyValue as string, _customerSettings);
        }

        /// <summary>
        /// Kiem tra cac dieu luat lien quan den viec dat ten dang nha
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="customerSettings">Customer settings</param>
        /// <returns>Result</returns>
        public static bool IsValid(string username, CauHinhNguoiDung customerSettings)
        {

            if (string.IsNullOrEmpty(username))
                return false;
            return true;
            //return customerSettings.UsernameValidationUseRegex
            //    ? Regex.IsMatch(username, customerSettings.UsernameValidationRule, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase)
            //    : username.All(l => customerSettings.UsernameValidationRule.Contains(l));
        }
    }
}
