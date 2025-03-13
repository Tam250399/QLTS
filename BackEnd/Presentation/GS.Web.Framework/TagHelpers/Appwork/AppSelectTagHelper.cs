using GS.Web.Framework.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;

namespace GS.Web.Framework.TagHelpers.Appwork
{
    /// <summary>
    /// nop-select tag helper
    /// </summary>
    [HtmlTargetElement("app-select", TagStructure = TagStructure.WithoutEndTag)]
    public class AppSelectTagHelper : TagHelper
    {
        private const string ForAttributeName = "asp-for";
        private const string NameAttributeName = "asp-for-name";
        private const string ItemsAttributeName = "asp-items";
        private const string DisabledAttributeName = "asp-disabled";
        private const string MultipleAttributeName = "asp-multiple";
        private const string RequiredAttributeName = "asp-required";
        private const string FilterAttributeName = "asp-filter";
        private const string DataUrlAttributeName = "asp-data-url";

        private readonly IHtmlHelper _htmlHelper;

        /// <summary>
        /// An expression to be evaluated against the current model
        /// </summary>
        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        /// <summary>
        /// Name for a dropdown list
        /// </summary>
        [HtmlAttributeName(NameAttributeName)]
        public string Name { get; set; }

        /// <summary>
        /// Items for a dropdown list
        /// </summary>
        [HtmlAttributeName(ItemsAttributeName)]
        public IEnumerable<SelectListItem> Items { set; get; } = new List<SelectListItem>();

        /// <summary>
        /// Indicates whether the field is required
        /// </summary>
        [HtmlAttributeName(RequiredAttributeName)]
        public string IsRequired { set; get; }
        /// <summary>
        /// Indicates whether the field is disabled
        /// </summary>
        [HtmlAttributeName(DisabledAttributeName)]
        public string IsDisabled { set; get; }
        [HtmlAttributeName(DataUrlAttributeName)]
        public string DataUrl { set; get; }
        /// <summary>
        /// Indicates whether the field is filter
        /// </summary>
        [HtmlAttributeName(FilterAttributeName)]
        public string IsFilter { set; get; }

        /// <summary>
        /// Indicates whether the input is multiple
        /// </summary>
        [HtmlAttributeName(MultipleAttributeName)]
        public string IsMultiple { set; get; }

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
        public AppSelectTagHelper(IHtmlHelper htmlHelper)
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

            //get htmlAttributes object
            var htmlAttributes = new Dictionary<string, object>();

            //disabled attribute
            bool.TryParse(IsDisabled, out bool disabled);
            if (disabled)
            {
                htmlAttributes.Add("disabled", "disabled");
                //htmlAttributes.Add("style", "background-color: red;");
            }
            var attributes = context.AllAttributes;
            foreach (var attribute in attributes)
            {
                if (!attribute.Name.Equals(ForAttributeName) &&
                    !attribute.Name.Equals(NameAttributeName) &&
                    !attribute.Name.Equals(ItemsAttributeName) &&
                    !attribute.Name.Equals(MultipleAttributeName) &&
                    !attribute.Name.Equals(RequiredAttributeName)
                    )
                {
                    htmlAttributes.Add(attribute.Name, attribute.Value);
                }
            }

            //generate editor
            var tagName = For != null ? For.Name : Name;
            bool.TryParse(IsMultiple, out bool multiple);
            bool.TryParse(IsFilter, out bool filter);
            if (!string.IsNullOrEmpty(tagName))
            {
                IHtmlContent selectList;
                if (multiple)
                {
                    selectList = _htmlHelper.Editor(tagName, "MultiSelect", new { htmlAttributes, SelectList = Items });
                }
                else if (filter)
                {
                    //selectList = _htmlHelper.Editor(tagName, "FilterSelect", new { htmlAttributes, url = DataUrl });
                    selectList = _htmlHelper.Editor(tagName, "FilterSelect", new { htmlAttributes, SelectList = Items });
                }
                else
                {
                    selectList = _htmlHelper.Editor(tagName, "DropdownList", new { htmlAttributes, SelectList = Items });
                    //if (htmlAttributes.ContainsKey("class"))
                    //    htmlAttributes["class"] += " form-control";
                    //else
                    //    htmlAttributes.Add("class", "form-control");

                    //selectList = _htmlHelper.DropDownList(tagName, Items, htmlAttributes);
                }
                output.Content.SetHtmlContent(selectList.RenderHtmlContent());
            }
        }
    }
}
