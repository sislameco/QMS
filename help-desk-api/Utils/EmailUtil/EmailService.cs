using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmailV2;
using Amazon.SimpleEmailV2.Model;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.EmailUtil
{
    public interface IEmailService: IBaseUtil
    {
        Task SendEmail(EmailSendDto sendEmailInfo);
    }
    public class EmailService: IEmailService
    {
        private readonly IConfiguration _config;
        private readonly IMailBackgroundTaskQueue _queue;
        public EmailService(IConfiguration config, IMailBackgroundTaskQueue queue)
        {
            _config = config;
            _queue = queue;
        }

        public Task SendEmail(EmailSendDto sendEmailInfo)
        {
            _queue.QueueBackgroundWorkItem(async _ =>
            {
                var credentials = new BasicAWSCredentials(_config["Email:AccessKey"], _config["Email:SecretKey"]);
                using (var client = new AmazonSimpleEmailServiceV2Client(credentials, RegionEndpoint.EUWest1))
                {
                    using var messageStream = new MemoryStream();
                    var message = new MimeMessage();
                    var builder = new BodyBuilder() { HtmlBody = sendEmailInfo.Mailbody };
                    message.Subject = sendEmailInfo.MailSubject;
                    if (sendEmailInfo.ToEmailAddresses.Any())
                    {
                        foreach (var item in sendEmailInfo.ToEmailAddresses)
                        {
                            message.To.Add(new MailboxAddress("", item));
                        }
                    }
                    if (sendEmailInfo.CcEmailAddresses.Any())
                    {
                        foreach (var item in sendEmailInfo.CcEmailAddresses)
                        {
                            message.Cc.Add(new MailboxAddress("", item));
                        }
                    }
                    message.From.Add(new MailboxAddress(_config["Email:From"], _config["Email:UserName"]));

                    message.Body = builder.ToMessageBody();
                    message.WriteTo(messageStream);

                    var sendEmailReq = new SendEmailRequest();
                    sendEmailReq.Content = new EmailContent()
                    {
                        Raw = new RawMessage { Data = messageStream }
                    };

                    var MailResponse = await client.SendEmailAsync(sendEmailReq);
                }
            });

            return Task.CompletedTask;
        }
    }
}
