namespace InvestmentApprovalTool.Services
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using MimeKit;
    using System.Collections.Generic;
    using System.IO;

    public class MailRequest
    {
        public List<string> ToEmail { get; set; }
        public List<string> ToCc { get; set; }
        public List<string> ToBcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> Attachments { get; set; }
    }

    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }

    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }

    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly ILogger<MimeMessage> _logger;

        public MailService(IOptions<MailSettings> mailSettings, ILogger<MimeMessage> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);

            email.From.Add(MailboxAddress.Parse(_mailSettings.Mail)); // RELAY SERVER

            if (mailRequest.ToEmail.Count() == 0) return;

            foreach (var mail in mailRequest.ToEmail) { email.To.Add(MailboxAddress.Parse(mail)); }
          
            if(mailRequest.ToCc != null)
            {
            foreach (var mail in mailRequest.ToCc) { email.Cc.Add(MailboxAddress.Parse(mail)); }
            }

            if (mailRequest.ToBcc != null)
            {
                foreach (var mail in mailRequest.ToBcc) { email.Cc.Add(MailboxAddress.Parse(mail)); }
            }
            
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();

            var body = new TextPart("html") { Text = mailRequest.Body };
            var multipart = new Multipart("mixed");
            multipart.Add(body);

            if (mailRequest.Attachments != null)
            {
                foreach (var path in mailRequest.Attachments)
                {
                    var filename = Path.GetFileName(path);
                    if (filename.Contains('.'))
                    {
                        var filePrefix = string.Join(".", filename.Split(".")[..^1]);
                        var filetype = filename.Split(".").Last();

                        var attachment = new MimePart(filePrefix, filetype)
                        {
                            Content = new MimeContent(File.OpenRead(path)),
                            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                            ContentTransferEncoding = ContentEncoding.Base64,
                            FileName = Path.GetFileName(path)
                        };
                        multipart.Add(attachment);
                    }
                }
            }

            email.Body = multipart;
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            //await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port);

            try
            {
                // Relay Server
                smtp.CheckCertificateRevocation = false;
                await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port);
                //await smtp.AuthenticateAsync("1293bbf6-da14-4ccb-b1cc-b6b71102c419", "U6XoFHK9S*:5");
                
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.InnerException?.ToString());
                _logger.LogWarning(ex,ex.Message);
            }

        }
    }
}
