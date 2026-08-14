using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTaskManagement.Application.Factories
{
    public class NotificationFactory : INotificationFactory
    {
        private readonly EmailNotificationService _emailService;
        private readonly SmsNotificationService _smsService;

        public NotificationFactory(
            EmailNotificationService emailService,
            SmsNotificationService smsService)
        {
            _emailService = emailService;
            _smsService = smsService;
        }

        public INotificationService Create(string type)
        {
            return type.ToLower() switch
            {
                "email" => _emailService,
                "sms" => _smsService,

                _ => throw new ArgumentException("Invalid notification type")
            };
        }
    }
}
