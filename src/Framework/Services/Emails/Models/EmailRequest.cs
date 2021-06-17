using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Services.Emails
{
    public record EmailRequest
    {
        public string ToEmail { get; init; }

        public string Subject { get; init; }

        public string Body { get; init; }

        public List<IFormFile> Attachments { get; init; }
    }
}