﻿namespace VendingMachine.Application.Common.Services
{
    /// <summary>
    /// Obsolete class to provide date time conversion. This class is made obsolete because
    /// we cannot promise the deployment of this API will be same as user's timezone. We also cannot 
    /// promise the use of windows specific timezone ids since this is a cross-platform API. 
    /// </summary>
    [Obsolete]
    public static class DateTimeProvider
    {
        /// <summary>
        /// Suggested approach is that timezones should be kept against the user table. Then timezone 
        /// information could be obtained from user context. Using NodaTime or similar, the timezone conversion 
        /// could be done before presenting this informaiton to the user. Do not use this method in production. 
        /// </summary>
        /// <param name="utcTime"></param>
        /// <returns></returns>
        [Obsolete]
        public static DateTime ConvertUtcToLocal(DateTime utcTime)
        {
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("E. Australia Standard Time");
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(tst.Id);
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZone);
        }
    }
}
