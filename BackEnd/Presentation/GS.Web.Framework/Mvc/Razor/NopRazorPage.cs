using GS.Core.Infrastructure;
using GS.Services.HeThong;
using GS.Web.Framework.Localization;

namespace GS.Web.Framework.Mvc.Razor
{
    /// <summary>
    /// Web view page
    /// </summary>
    /// <typeparam name="TModel">Model</typeparam>
    public abstract class GSRazorPage<TModel> : Microsoft.AspNetCore.Mvc.Razor.RazorPage<TModel>
    {
        private INhanHienThiService __nhanHienThiService;
        private Localizer _localizer;

        /// <summary>
        /// Get a localized resources
        /// </summary>
        public Localizer T
        {
            get
            {
                if (__nhanHienThiService == null)
                    __nhanHienThiService = EngineContext.Current.Resolve<INhanHienThiService>();
                if (_localizer == null)
                {
                    _localizer = (format, args) =>
                    {

                        var resFormat = __nhanHienThiService.GetGiaTri(format);
                        if (string.IsNullOrEmpty(resFormat))
                        {
                            return new LocalizedString(format);
                        }
                        return new LocalizedString((args == null || args.Length == 0)
                            ? resFormat
                            : string.Format(resFormat, args));
                    };
                }
                return _localizer;
            }
        }

        /// <summary>
        /// Return a value indicating whether the working language and theme support RTL (right-to-left)
        /// </summary>
        /// <returns></returns>
        public bool ShouldUseRtlTheme()
        {
            //var workContext = EngineContext.Current.Resolve<IWorkContext>();
            //var supportRtl = workContext.WorkingLanguage.Rtl;
            //if (supportRtl)
            //{
            //    //ensure that the active theme also supports it
            //    var themeProvider = EngineContext.Current.Resolve<IThemeProvider>();
            //    var themeContext = EngineContext.Current.Resolve<IThemeContext>();
            //    supportRtl = themeProvider.GetThemeBySystemName(themeContext.WorkingThemeName)?.SupportRtl ?? false;
            //}
            return false;
        }
    }

    /// <summary>
    /// Web view page
    /// </summary>
    public abstract class GSRazorPage : GSRazorPage<dynamic>
    {
    }
}