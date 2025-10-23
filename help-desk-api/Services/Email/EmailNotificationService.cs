using Amazon.SimpleEmailV2.Model;
using Models.Entities.Notification;
using Models.Entities.Setup;
using Models.Entities.UserManagement;
using Models.Enum;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Utils;
using Utils.EmailUtil;

namespace Services.Email
{
    public interface IEmailNotificationService
    {
        Task<bool> SendInfitation(UserModel user, string otpCode);
        Task<bool> SendUserPasswordRecoverEmail(UserModel user, string otpCode);
    }
    public class EmailNotificationService: IEmailNotificationService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IEmailService _emailService;
        public EmailNotificationService(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<bool> SendInfitation(UserModel user, string otpCode)
        {
            NotificationTemplateModel emailTemplate = await _unitOfWork.Repository<NotificationTemplateModel, int>().FirstOrDefaultAsync(s => s.Event == NotificationEvent.UserInvitation && s.NotificationType == EnumNotificationType.Email) ?? throw new BadRequestException("No Template found");
            EmailConfigurationModel mailClient = await _unitOfWork.Repository<EmailConfigurationModel, int>().FirstOrDefaultAsync(s => s.RStatus == EnumRStatus.Active && s.Id == emailTemplate.EmailConfigurationId) ?? throw new BadRequestException("No Email Configuration found");


            MailClientDto emailConfig = new MailClientDto()
            {
                HostName = mailClient.Host,
                PortNoImap = mailClient.IMAPPort,
                Email = mailClient.UserName,
                Password = mailClient.Password,
                PortNoSMTP = mailClient.SMTPPort,
                AccessKey = mailClient.AccessKey,
                SecretKey = mailClient.SecretKey,
                ReplyTo = mailClient.ReplyTo,
                BCC = mailClient.BCC,
                Name = mailClient.Name,

            };


            var properties = new Dictionary<string, string>();
            properties.Add("[user_full_name]", user.FirstName + " " + user.LastName);
            properties.Add("[otp_code]", otpCode);

            return await _emailService.SendEmailAsync(
                new EmailSendDto
                {
                    ToEmailAddresses = new List<string> { user.Email },
                    MailSubject = Common.ReplaceTextWithContent(emailTemplate.SubjectTemplate, properties),
                    Mailbody = Common.ReplaceTextWithContent(emailTemplate.BodyTemplate, properties),

                }, emailConfig);
        }

        public async Task<bool> SendUserPasswordRecoverEmail(UserModel user, string otpCode)
        {
            NotificationTemplateModel emailTemplate =  await _unitOfWork.Repository<NotificationTemplateModel,int>().FirstOrDefaultAsync(s=> s.Event == NotificationEvent.RecoveryPassword && s.NotificationType == EnumNotificationType.Email) ?? throw new  BadRequestException("No Template found");
            EmailConfigurationModel mailClient = await _unitOfWork.Repository<EmailConfigurationModel, int>().FirstOrDefaultAsync(s => s.RStatus == EnumRStatus.Active && s.Id == emailTemplate.EmailConfigurationId) ?? throw new BadRequestException("No Email Configuration found");


            MailClientDto emailConfig = new MailClientDto()
            {
                HostName = mailClient.Host,
                PortNoImap = mailClient.IMAPPort,
                Email = mailClient.UserName,
                Password = mailClient.Password,
                PortNoSMTP = mailClient.SMTPPort,
                AccessKey = mailClient.AccessKey,
                SecretKey = mailClient.SecretKey,
                ReplyTo = mailClient.ReplyTo,
                BCC = mailClient.BCC,
                Name = mailClient.Name,

            };


            var properties = new Dictionary<string, string>();
            properties.Add("[user_full_name]", user.FirstName + " " + user.LastName);
            properties.Add("[otp_code]", otpCode);

            return await _emailService.SendEmailAsync(
                new EmailSendDto
                {
                     ToEmailAddresses = new List<string> { user.Email },
                    MailSubject = Common.ReplaceTextWithContent(emailTemplate.SubjectTemplate, properties),
                    Mailbody = Common.ReplaceTextWithContent(emailTemplate.BodyTemplate, properties),
                    
                }, emailConfig);
        }
    }
}
