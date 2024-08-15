using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using secondYear.Models;


namespace secondYear.service
{
    public class EmailServices
    {
        private readonly SmtpSettings _smtpSettings;

    public EmailServices(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var smtpClient = new SmtpClient(_smtpSettings.Host)
        {
            Port = _smtpSettings.Port,
            Credentials = new NetworkCredential(_smtpSettings.Email, _smtpSettings.Password),
            EnableSsl = _smtpSettings.EnableSsl,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_smtpSettings.Email ??string.Empty),
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(toEmail);

        await smtpClient.SendMailAsync(mailMessage);
    }
} }