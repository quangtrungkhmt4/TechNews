using System;
using System.Globalization;

namespace EcommerceCore.Utilities.Extensions
{
    public static class DatetimeExtension
    {
        public static string ConvertDatetimeToString(this DateTime dateTime, string typeFormat = "yyyyMMdd")
        {         
            return dateTime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture).Replace('/', '-'); ;
        }

        public static DateTime? ParseDateTime(this string s , string format = "yyyyMMdd" , CultureInfo provider = null
            , DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        {
            DateTime? result = null;
            provider = provider ?? CultureInfo.InvariantCulture;
            if (DateTime.TryParseExact(s, format, provider: provider, style: dateTimeStyles, result: out DateTime dateTime))
            {
                result = dateTime;
            }
            return result;
        }
    }
}
