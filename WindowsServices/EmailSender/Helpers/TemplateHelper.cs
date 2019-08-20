using EmailSender.Models;
using System;
using System.Text;

namespace EmailSender.Helpers
{
    public class TemplateHelper
    {
        public static string BuildBody(string content, TemplateData templateData)
        {
            StringBuilder template = new StringBuilder();
            template.Append($"<p>{content}</div>");
            template.Append("<hr>");
            template.Append($"<p>{templateData.Signature}</div>");
            return template.ToString();
        }
    }
}
