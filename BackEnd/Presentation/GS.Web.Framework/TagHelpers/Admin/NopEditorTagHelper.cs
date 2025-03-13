using GS.Core;
using GS.Web.Framework.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;

namespace GS.Web.Framework.TagHelpers.Admin
{
    /// <summary>
    /// nop-editor tag helper
    /// </summary>
    [HtmlTargetElement("nop-editor", Attributes = ForAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class GSEditorTagHelper : TagHelper
    {
        private const string ForAttributeName = "asp-for";
        private const string DisabledAttributeName = "asp-disabled";
        private const string ReadonlyAttributeName = "asp-readonly";
        private const string RequiredAttributeName = "asp-required";
        private const string RenderFormControlClassAttributeName = "asp-render-form-control-class";
        private const string TemplateAttributeName = "asp-template";
        private const string PostfixAttributeName = "asp-postfix";
        private const string AddClassName = "asp-class";
        private const string AddDVT = "asp-dvt";
        private const string _AddMinValue = "asp-min-number";
        private const string _AddMaxValue = "asp-max-number";
        private const string _Amount = "asp-amount";

        private readonly IHtmlHelper _htmlHelper;

        /// <summary>
        /// An expression to be evaluated against the current model
        /// </summary>
        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        /// <summary>
        /// Indicates whether the field is disabled
        /// </summary>
        [HtmlAttributeName(DisabledAttributeName)]
        public string IsDisabled { set; get; }

		/// <summary>
		/// Indicates whether the field is readonly
		/// </summary>
		[HtmlAttributeName(ReadonlyAttributeName)]
		public string IsReadonly { set; get; }

		/// <summary>
		/// Indicates whether the field is required
		/// </summary>
		[HtmlAttributeName(RequiredAttributeName)]
        public string IsRequired { set; get; }

        /// <summary>
        /// Indicates whether the "form-control" class shold be added to the input
        /// </summary>
        [HtmlAttributeName(RenderFormControlClassAttributeName)]
        public string RenderFormControlClass { set; get; }

        /// <summary>
        /// Editor template for the field
        /// </summary>
        [HtmlAttributeName(TemplateAttributeName)]
        public string Template { set; get; }

        /// <summary>
        /// Postfix
        /// </summary>
        [HtmlAttributeName(PostfixAttributeName)]
        public string Postfix { set; get; }

        [HtmlAttributeName(AddClassName)]
        public string AddClass { set; get; }
        [HtmlAttributeName(AddDVT)]
        public string AddDonViTinh { set; get; }
        /// <summary>
        /// Số tối thiểu
        /// </summary>
        [HtmlAttributeName(_AddMinValue)]
        public string AddMinValue { set; get; }
        [HtmlAttributeName(_AddMaxValue)]
        public string AddMaxValue { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        [HtmlAttributeName(_Amount)]
        public string IsSoLuong { set; get; }
        /// <summary>
        /// ViewContext
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="htmlHelper">HTML helper</param>
        public GSEditorTagHelper(IHtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
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

            //clear the output
            output.SuppressOutput();

            //container for additional attributes
            var htmlAttributes = new Dictionary<string, object>();

            //disabled attribute
            bool.TryParse(IsDisabled, out bool disabled);
            if (disabled)
            {
                htmlAttributes.Add("disabled", "disabled");
			}
			//readonly attribute
			bool.TryParse(IsReadonly, out bool _readonly);
			if (_readonly)
			{
				htmlAttributes.Add("readonly", "readonly");
			}

			//required asterisk
			bool.TryParse(IsRequired, out bool required);
            if (required)
            {
                output.PreElement.SetHtmlContent("<div class='input-group input-group-required'>");
                output.PostElement.SetHtmlContent("<div class=\"input-group-btn\"><span class=\"required\">*</span></div></div>");
            }

            //contextualize IHtmlHelper
            var viewContextAware = _htmlHelper as IViewContextAware;
            viewContextAware?.Contextualize(ViewContext);

            //add form-control class
            bool.TryParse(RenderFormControlClass, out bool renderFormControlClass);
            if (!string.IsNullOrEmpty(AddClass))
            {
                htmlAttributes.Add("class", "form-control " + AddClass);
            }
            else
            {
                if (string.IsNullOrEmpty(RenderFormControlClass) && For.Metadata.ModelType.Name.Equals("String") || renderFormControlClass)
                    htmlAttributes.Add("class", "form-control");
            }
            if (!string.IsNullOrEmpty(AddDonViTinh))
            {
                htmlAttributes.Add("donvitinh", AddDonViTinh);
            }
            else
            {
                htmlAttributes.Add("donvitinh", "");
            }
            int minVal = 0;
            if (!string.IsNullOrEmpty(AddMinValue) && int.TryParse(AddMinValue,out minVal))
            {
                htmlAttributes.Add("minvalue", AddMinValue);
            }
            else
            {
                htmlAttributes.Add("minvalue", "");
            }
            decimal maxVal = 0;
            if (!string.IsNullOrEmpty(AddMaxValue) && decimal.TryParse(AddMaxValue, out maxVal))
            {
                htmlAttributes.Add("maxvalue", AddMaxValue);
            }
            else
            {
                htmlAttributes.Add("maxvalue", "");
            }
            // bool soluong
            bool.TryParse(IsSoLuong, out bool isSoLuong);
            if (isSoLuong)
            {
                htmlAttributes.Add("IsSoLuong", "true");
            }
            else
            {
                htmlAttributes.Add("IsSoLuong", "fasle");
            }
            //generate editor
            var attributes = context.AllAttributes;
            foreach (var attribute in attributes)
            {
                if (!attribute.Name.Equals(ForAttributeName) &&
                    !attribute.Name.Equals(DisabledAttributeName) &&
                    !attribute.Name.Equals(AddClassName) &&
                    !attribute.Name.Equals(RequiredAttributeName))
                {
                    htmlAttributes.Add(attribute.Name, attribute.Value);
                }
            }
            //we have to invoke strong typed "EditorFor" method of HtmlHelper<TModel>
            //but we cannot do it because we don't have access to Expression<Func<TModel, TValue>>
            //more info at https://github.com/aspnet/Mvc/blob/dev/src/Microsoft.AspNetCore.Mvc.ViewFeatures/ViewFeatures/HtmlHelperOfT.cs

            //so we manually invoke implementation of "GenerateEditor" method of HtmlHelper
            //more info at https://github.com/aspnet/Mvc/blob/dev/src/Microsoft.AspNetCore.Mvc.ViewFeatures/ViewFeatures/HtmlHelper.cs

            //little workaround here. we need to access private properties of HtmlHelper
            //just ensure that they are not renamed by asp.net core team in future versions
            var viewEngine = CommonHelper.GetPrivateFieldValue(_htmlHelper, "_viewEngine") as IViewEngine;
            var bufferScope = CommonHelper.GetPrivateFieldValue(_htmlHelper, "_bufferScope") as IViewBufferScope;
            var templateBuilder = new TemplateBuilder(
                viewEngine,
                bufferScope,
                _htmlHelper.ViewContext,
                _htmlHelper.ViewData,
                For.ModelExplorer,
                For.Name,
                Template,
                readOnly: false,
                additionalViewData: new { htmlAttributes, postfix = this.Postfix });

            var htmlOutput = templateBuilder.Build();
            output.Content.SetHtmlContent(htmlOutput.RenderHtmlContent());
        }
    }
}