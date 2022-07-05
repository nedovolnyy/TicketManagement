using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TicketManagement.MVC.Helpers
{
    [HtmlTargetElement("input", Attributes = ForAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class InvariantDecimalTagHelper : InputTagHelper
    {
        private const string ForAttributeName = "asp-for";

        private readonly IHtmlGenerator _generator;
        public InvariantDecimalTagHelper(IHtmlGenerator generator)
            : base(generator)
        {
            _generator = generator;
        }

        [HtmlAttributeName("asp-is-invariant")]
        public bool IsInvariant { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            if (IsInvariant && output.TagName == "input" && For.Model != null && For.Model.GetType() == typeof(decimal))
            {
                decimal value = (decimal)For.Model;
                var invariantValue = value.ToString(System.Globalization.CultureInfo.InvariantCulture);
                output.Attributes.SetAttribute(new TagHelperAttribute("value", invariantValue));
            }
        }
    }
}
