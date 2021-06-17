﻿using System.Threading;
using System.Threading.Tasks;

namespace Services.Emails
{
    public interface IEmailsLogService
    {
        public Task SaveLogAsync(EmailRequest emailRequest, CancellationToken cancellationToken);

        public Task SaveLogAsync(EmailRequestToUser emailRequestToUser, CancellationToken cancellationToken);
    }
}
