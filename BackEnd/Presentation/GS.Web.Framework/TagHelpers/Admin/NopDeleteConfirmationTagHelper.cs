using GS.Web.Framework.Extensions;
using GS.Web.Framework.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace GS.Web.Framework.TagHelpers.Admin
{
    /// <summary>
    /// nop-delete-confirmation tag helper
    /// </summary>
    [HtmlTargetElement("nop-delete-confirmation", Attributes = ModelIdAttributeName + "," + ButtonIdAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class GSDeleteConfirmationTagHelper : TagHelper
    {
        private const string ModelIdAttributeName = "asp-model-id";
        private const string ButtonIdAttributeName = "asp-button-id";
        private const string ActionAttributeName = "asp-action";
        //private const string ControllerAttributeName = "asp-controller";

        private readonly IHtmlHelper _htmlHelper;

        /// <summary>
        /// HtmlGenerator
        /// </summary>
        protected IHtmlGenerator Generator { get; set; }

        /// <summary>
        /// Model identifier
        /// </summary>
        [HtmlAttributeName(ModelIdAttributeName)]
        public string ModelId { get; set; }

        /// <summary>
        /// Button identifier
        /// </summary>
        [HtmlAttributeName(ButtonIdAttributeName)]
        public string ButtonId { get; set; }

        /// <summary>
        /// Delete action name
        /// </summary>
        [HtmlAttributeName(ActionAttributeName)]
        public string Action { get; set; }
        /// <summary>
        /// Controller name
        /// </summary>
        //[HtmlAttributeName(ControllerAttributeName)]
        //public string Controller { get; set; }

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
        /// <param name="htmlHelper">HTML helper</param>
        public GSDeleteConfirmationTagHelper(IHtmlGenerator generator, IHtmlHelper htmlHelper)
        {
            Generator = generator;
            _htmlHelper = htmlHelper;
        }


        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="output">Output</param>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            //contextualize IHtmlHelper
            var viewContextAware = _htmlHelper as IViewContextAware;
            viewContextAware?.Contextualize(ViewContext);

            if (string.IsNullOrEmpty(Action))
                Action = "Delete";

            var modelName = _htmlHelper.ViewData.ModelMetadata.ModelType.Name.ToLower();
            if (!string.IsNullOrEmpty(Action))
                modelName += "-" + Action;
            var modalId = new HtmlString(modelName + "-delete-confirmation").ToHtmlString();

            if (int.TryParse(ModelId, out int modelId))
            {
                //Controller = string.IsNullOrEmpty(Controller) ? _htmlHelper.ViewContext.RouteData.Values["controller"].ToString() : Controller;
                var deleteConfirmationModel = new DeleteConfirmationModel
                {
                    ID = modelId,
                    ControllerName = _htmlHelper.ViewContext.RouteData.Values["controller"].ToString(),
                    //ControllerName = Controller,
                    ActionName = Action,
                    WindowId = modalId
                };

                //tag details
                output.TagName = "div";
                output.TagMode = TagMode.StartTagAndEndTag;

                output.Attributes.Add("id", modalId);
                output.Attributes.Add("class", "modal fade");
                output.Attributes.Add("tabindex", "-1");
                output.Attributes.Add("role", "dialog");
                output.Attributes.Add("aria-labelledby", $"{modalId}-title");
                output.Content.SetHtmlContent(await _htmlHelper.PartialAsync("Delete", deleteConfirmationModel));

                //modal script
                var script = new TagBuilder("script");
                script.InnerHtml.AppendHtml("$(document).ready(function () {" +
                                            $"$('#{ButtonId}').attr(\"data-toggle\", \"modal\").attr(\"data-target\", \"#{modalId}\")" + "});");
                output.PostContent.SetHtmlContent(script.RenderHtmlContent());
            }
        }
    }
}