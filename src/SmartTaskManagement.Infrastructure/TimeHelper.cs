namespace SmartTaskManagement.Infrastructure

{
    public static class TimeHelper
    {
        private static readonly TimeZoneInfo IstZone =
            TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        /// <summary>
        /// Converts a UTC DateTime to Indian Standard Time (IST).
        /// </summary>
        public static DateTime ConvertUtcToIst(DateTime utcTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, IstZone);
        }

        /// <summary>
        /// Gets the current IST time.
        /// </summary>
        public static DateTime GetCurrentIstTime()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IstZone);
        }
    }

}