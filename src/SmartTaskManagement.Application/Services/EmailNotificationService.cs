using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SmartTaskManagement.Application.DTOs.Configuration;
using SmartTaskManagement.Application.Interfaces;

namespace SmartTaskManagement.Application.Services;

public class EmailNotificationService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailNotificationService(
        IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    //public async Task SendAsync(string recipient, string message)
    //{
    //    // Email sending logic will go here

    //    Console.WriteLine($"Sending EMAIL to: {recipient}");
    //    Console.WriteLine($"Message: {message}");

    //    await Task.CompletedTask;
    //}

    public async Task SendAsync(
        string recipient,
        string message)
    {
        var email = new MimeMessage();

        email.From.Add(
            new MailboxAddress(
                _emailSettings.DisplayName,
                _emailSettings.From));

        email.To.Add(
            MailboxAddress.Parse(recipient));

        email.Subject = "Smart Task Management Notification";

        email.Body = new TextPart("plain")
        {
            Text = message
        };

        using var smtp = new SmtpClient();

        await smtp.ConnectAsync(
            _emailSettings.Host,
            _emailSettings.Port,
            SecureSocketOptions.StartTls);

        await smtp.AuthenticateAsync(
            _emailSettings.Username,
            _emailSettings.Password);

        await smtp.SendAsync(email);

        await smtp.DisconnectAsync(true);
    }
}