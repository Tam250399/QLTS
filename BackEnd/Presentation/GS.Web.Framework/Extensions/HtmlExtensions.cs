﻿using GS.Core.Infrastructure;
using GS.Services.HeThong;
using GS.Web.Framework.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;

namespace GS.Web.Framework.Extensions
{
    /// <summary>
    /// HTML extensions
    /// </summary>
    public static class HtmlExtensions
    {
        #region Admin area extensions

        /// <summary>
        /// Generate editor for localizable entities
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <typeparam name="TLocalizedModelLocal">Type</typeparam>
        /// <param name="helper">HTML helper</param>
        /// <param name="name">ID of control</param>
        /// <param name="localizedTemplate">Template with localizable values</param>
        /// <param name="standardTemplate">Template for standard (default) values</param>
        /// <param name="ignoreIfSeveralStores">A value indicating whether to ignore localization if we have multiple stores</param>
        /// <returns></returns>
        public static IHtmlContent LocalizedEditor<T, TLocalizedModelLocal>(this IHtmlHelper<T> helper,
            string name,
            Func<int, HelperResult> localizedTemplate,
            Func<T, HelperResult> standardTemplate,
            bool ignoreIfSeveralStores = false)
            where T : ILocalizedModel<TLocalizedModelLocal>
            where TLocalizedModelLocal : ILocalizedLocaleModel
        {
            var localizationSupported = helper.ViewData.Model.Locales.Count > 1;
            if (ignoreIfSeveralStores)
            {
                localizationSupported = false;
            }
            if (localizationSupported)
            {
                var tabStrip = new StringBuilder();
                tabStrip.AppendLine($"<div id=\"{name}\" class=\"nav-tabs-custom nav-tabs-localized-fields\">");

                //render input contains selected tab name
                var tabNameToSelect = GetSelectedTabName(helper, name);
                var selectedTabInput = new TagBuilder("input");
                selectedTabInput.Attributes.Add("type", "hidden");
                selectedTabInput.Attributes.Add("id", $"selected-tab-name-{name}");
                selectedTabInput.Attributes.Add("name", $"selected-tab-name-{name}");
                selectedTabInput.Attributes.Add("value", tabNameToSelect);
                tabStrip.AppendLine(selectedTabInput.RenderHtmlContent());

                tabStrip.AppendLine("<ul class=\"nav nav-tabs\">");

                //default tab
                var standardTabName = $"{name}-standard-tab";
                var standardTabSelected = string.IsNullOrEmpty(tabNameToSelect) || standardTabName == tabNameToSelect;
                tabStrip.AppendLine(string.Format("<li{0}>", standardTabSelected ? " class=\"active\"" : null));
                tabStrip.AppendLine($"<a data-tab-name=\"{standardTabName}\" href=\"#{standardTabName}\" data-toggle=\"tab\">{EngineContext.Current.Resolve<INhanHienThiService>().GetGiaTri("Admin.Common.Standard")}</a>");
                tabStrip.AppendLine("</li>");

                //var languageService = EngineContext.Current.Resolve<ILanguageService>();
                var urlHelper = EngineContext.Current.Resolve<IUrlHelperFactory>().GetUrlHelper(helper.ViewContext);

                foreach (var locale in helper.ViewData.Model.Locales)
                {
                    //languages
                    //var language = languageService.GetLanguageById(locale.LanguageId);
                    //if (language == null)
                    //    throw new Exception("Language cannot be loaded");

                    //var localizedTabName = $"{name}-{language.Id}-tab";
                    //tabStrip.AppendLine(string.Format("<li{0}>", localizedTabName == tabNameToSelect ? " class=\"active\"" : null));
                    //var iconUrl = urlHelper.Content("~/images/flags/" + language.FlagImageFileName);
                    //tabStrip.AppendLine($"<a data-tab-name=\"{localizedTabName}\" href=\"#{localizedTabName}\" data-toggle=\"tab\"><img alt='' src='{iconUrl}'>{WebUtility.HtmlEncode(language.Name)}</a>");

                    //tabStrip.AppendLine("</li>");
                }
                tabStrip.AppendLine("</ul>");

                //default tab
                tabStrip.AppendLine("<div class=\"tab-content\">");
                tabStrip.AppendLine(string.Format("<div class=\"tab-pane{0}\" id=\"{1}\">", standardTabSelected ? " active" : null, standardTabName));
                tabStrip.AppendLine(standardTemplate(helper.ViewData.Model).ToHtmlString());
                tabStrip.AppendLine("</div>");

                for (var i = 0; i < helper.ViewData.Model.Locales.Count; i++)
                {
                    //languages
                    //var language = languageService.GetLanguageById(helper.ViewData.Model.Locales[i].LanguageId);
                    //if (language == null)
                    //    throw new Exception("Language cannot be loaded");

                    //var localizedTabName = $"{name}-{language.Id}-tab";
                    //tabStrip.AppendLine(string.Format("<div class=\"tab-pane{0}\" id=\"{1}\">", localizedTabName == tabNameToSelect ? " active" : null, localizedTabName));
                    //tabStrip.AppendLine(localizedTemplate(i).ToHtmlString());
                    //tabStrip.AppendLine("</div>");
                }
                tabStrip.AppendLine("</div>");
                tabStrip.AppendLine("</div>");

                //render tabs script
                var script = new TagBuilder("script");
                script.InnerHtml.AppendHtml("$(document).ready(function () {bindBootstrapTabSelectEvent('" + name + "', 'selected-tab-name-" + name + "');});");
                tabStrip.AppendLine(script.RenderHtmlContent());

                return new HtmlString(tabStrip.ToString());
            }
            else
            {
                return new HtmlString(standardTemplate(helper.ViewData.Model).RenderHtmlContent());
            }
        }

        /// <summary>
        /// Gets a selected tab name (used in admin area to store selected tab name)
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="dataKeyPrefix">Key prefix. Pass null to ignore</param>
        /// <returns>Name</returns>
        public static string GetSelectedTabName(this IHtmlHelper helper, string dataKeyPrefix = null)
        {
            //keep this method synchronized with
            //"SaveSelectedTab" method of \Area\Admin\Controllers\BaseAdminController.cs
            var tabName = string.Empty;
            var dataKey = "nop.selected-tab-name";
            if (!string.IsNullOrEmpty(dataKeyPrefix))
                dataKey += $"-{dataKeyPrefix}";

            if (helper.ViewData.ContainsKey(dataKey))
                tabName = helper.ViewData[dataKey].ToString();

            if (helper.ViewContext.TempData.ContainsKey(dataKey))
                tabName = helper.ViewContext.TempData[dataKey].ToString();

            return tabName;
        }

        #region Form fields

        /// <summary>
        /// Generate hint control
        /// </summary>
        /// <param name="helper">HTML helper</param>
        /// <param name="value">TexHint text</param>
        /// <returns>Result</returns>
        public static IHtmlContent Hint(this IHtmlHelper helper, string value)
        {
            //create tag builder
            var builder = new TagBuilder("div");
            builder.MergeAttribute("title", value);
            builder.MergeAttribute("class", "ico-help");
            builder.MergeAttribute("data-toggle", "tooltip");
            var icon = new StringBuilder();
            icon.Append("<i class='fa fa-question-circle'></i>");
            builder.InnerHtml.AppendHtml(icon.ToString());
            //render tag
            return new HtmlString(builder.ToHtmlString());
        }

        #endregion

        #endregion

        #region Common extensions

        /// <summary>
        /// Convert IHtmlContent to string
        /// </summary>
        /// <param name="htmlContent">HTML content</param>
        /// <returns>Result</returns>
        public static string RenderHtmlContent(this IHtmlContent htmlContent)
        {
            using (var writer = new StringWriter())
            {
                htmlContent.WriteTo(writer, HtmlEncoder.Default);
                var htmlOutput = writer.ToString();
                return htmlOutput;
            }
        }

        /// <summary>
        /// Convert IHtmlContent to string
        /// </summary>
        /// <param name="tag">Tag</param>
        /// <returns>String</returns>
        public static string ToHtmlString(this IHtmlContent tag)
        {
            using (var writer = new StringWriter())
            {
                tag.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }
        /// <summary>
        /// Thêm dòng đầu tiên của dropdown list
        /// </summary>
        /// <param name="items"></param>
        /// <param name="TextDisplay"></param>
        public static void AddFirstRow(this IList<SelectListItem> Items, string TextDisplay = "-----------------------------", string ValueFirst = "0")
        {
            Items.Insert(0, new SelectListItem
            {
                Value = ValueFirst,
                Text = TextDisplay
            });
        }
        #endregion
    }
}