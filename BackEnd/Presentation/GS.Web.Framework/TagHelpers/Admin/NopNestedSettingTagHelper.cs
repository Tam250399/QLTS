﻿using GS.Core;
using GS.Core.Domain.Common;
using GS.Web.Framework.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace GS.Web.Framework.TagHelpers.Admin
{
    /// <summary>
    /// nop-nested-setting tag helper
    /// </summary>
    [HtmlTargetElement("nop-nested-setting", Attributes = ForAttributeName)]
    public class GSNestedSettingTagHelper : TagHelper
    {
        private readonly AdminAreaSettings _adminAreaSettings;

        private const string ForAttributeName = "asp-for";

        /// <summary>
        /// HtmlGenerator
        /// </summary>
        protected IHtmlGenerator Generator { get; set; }

        /// <summary>
        /// An expression to be evaluated against the current model
        /// </summary>
        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        /// <summary>
        /// ViewContext
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="generator">HTML generator</param>
        /// <param name="adminAreaSettings">Admin area settings</param>
        public GSNestedSettingTagHelper(IHtmlGenerator generator, AdminAreaSettings adminAreaSettings)
        {
            Generator = generator;
            _adminAreaSettings = adminAreaSettings;
        }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="output">Output</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            var parentSettingName = For.Name;

            var random = CommonHelper.GenerateRandomInteger();
            var nestedSettingId = $"nestedSetting{random}";
            var parentSettingId = $"parentSetting{random}";

            //tag details
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "nested-setting");
            if (context.AllAttributes.ContainsName("id"))
                nestedSettingId = context.AllAttributes["id"].Value.ToString();

            //use javascript
            if (_adminAreaSettings.UseNestedSetting)
            {
                var script = new TagBuilder("script");
                script.InnerHtml.AppendHtml("$(document).ready(function () {" +
                                                $"initNestedSetting('{parentSettingName}', '{parentSettingId}', '{nestedSettingId}');" +
                                            "});");
                output.PreContent.SetHtmlContent(script.RenderHtmlContent());
            }
        }
    }
}