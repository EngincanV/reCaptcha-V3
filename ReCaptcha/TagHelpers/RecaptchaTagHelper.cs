using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using ReCaptcha.Options;

namespace ReCaptcha.TagHelpers
{
    [HtmlTargetElement("recaptcha")]
    public class RecaptchaTagHelper : TagHelper
    {
        [HtmlAttributeName("action-name")]
        public string ActionName { get; set; }

        [HtmlAttributeName("execute-method")]
        public string Execute { get; set; }
        

        private readonly RecaptchaOptions _recaptchaOptions;
        public RecaptchaTagHelper(IOptions<RecaptchaOptions> recaptchaOptions)
        {
            _recaptchaOptions = recaptchaOptions.Value;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;

            var preContent = new StringBuilder();
            preContent.AppendFormat($"<script src=\"https://www.google.com/recaptcha/api.js?render={_recaptchaOptions.SiteKey}\"></script>");
            output.PreContent.SetHtmlContent(preContent.ToString());

            var postContent = new StringBuilder();
            var recaptchaReady = 
                "<script>" +
                    "grecaptcha.ready(function() {" +
                        "grecaptcha.execute('" + _recaptchaOptions.SiteKey + (string.IsNullOrWhiteSpace(ActionName) ? "" : "', {action: '" + ActionName ) + "'}).then(function(token) {" + 
                            Execute + "(token);" +
                        "});" +
                    "});" +
                "</script>";

            postContent.AppendLine(recaptchaReady);
            output.PostContent.SetHtmlContent(postContent.ToString());
        }
    }
}