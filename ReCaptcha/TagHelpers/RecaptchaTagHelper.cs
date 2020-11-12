using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ReCaptcha.TagHelpers
{
    [HtmlTargetElement(tag: "Recaptcha")]
    public class RecaptchaTagHelper : TagHelper
    {
        [HtmlAttributeName("site-key")] 
        public string SiteKey { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            
            var preContent = new StringBuilder();
            preContent.AppendFormat("<button class=\"g-recaptcha" +
                            $"data-sitekey=\"{SiteKey}" +
                            "data-callback='onSubmit'" +
                            "data-action='submit'>Submit</button>");

            output.PreContent.SetHtmlContent(preContent.ToString());

            var postContent = new StringBuilder();
            postContent.AppendFormat($"<script src=\"https://www.google.com/recaptcha/api.js\"></script>");
            postContent.AppendLine("<script> function onSubmit(token) { document.getElementById(\"demo-form\").submit() } </script>");
            
            output.PostContent.SetHtmlContent(postContent.ToString());
        }
    }
}