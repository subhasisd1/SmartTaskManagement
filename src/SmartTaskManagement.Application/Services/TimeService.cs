using SmartTaskManagement.Application.Interfaces.Time;
using System;

namespace SmartTaskManagement.Infrastructure.Services
{
    public class TimeService : ITimeService
    {
        private static readonly TimeZoneInfo IstZone =
            TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public DateTime ConvertUtcToIst(DateTime utcTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, IstZone);
        }

        public DateTime GetCurrentIstTime()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IstZone);
        }
    }
}
