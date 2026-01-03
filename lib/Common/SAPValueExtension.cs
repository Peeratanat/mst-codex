using System;

namespace Common
{
    public static class SAPValueExtension
    {
        public static string FollowSpace(this string value, int iLength)
        {
            value = value ?? "";

            if (iLength == 0)
                return "";

            if (value.Length > iLength)
                value = value.Substring(0, iLength);
            else
                while (value.Length < iLength)
                {
                    value += " ";
                }

            return value;
        }

        public static string LeadingZero(this string value, int iZero)
        {
            if (!string.IsNullOrEmpty(value))
            {
                string sZero = "";
                for (int i = 0; i + value.Length < iZero; i++)
                {
                    sZero += "0";
                }

                value = sZero + value;
            }

            return value;
        }

        public static string StrSAPDateTime(this DateTime value)
        {
            string strDate = value.Year.ToString() + value.ToString("MMdd");
            return strDate;
        }

        public static string StrSAPDateTime(this DateTime? value)
        {
            DateTime date = value ?? DateTime.Now;
            string strDate = date.Year.ToString() + date.ToString("MMdd");
            return strDate;
        }

        public static string StrSAPAmount(this object value)
        {
            string strAmount = "";
            if (value != null)
            {
                if (decimal.TryParse((value ?? 0).ToString(), out decimal decObj))
                    strAmount = decObj.ToString("0.00");
            }

            return strAmount;
        }

        public static string StrSAPAmount(this decimal value)
        {
            string strAmount = "";

            if (decimal.TryParse(value.ToString(), out decimal decObj))
                strAmount = decObj.ToString("0.00");

            return strAmount;
        }

        public static string StrSAPAmount(this decimal? value)
        {
            string strAmount = "";
            if (value.HasValue)
            {
                if (decimal.TryParse((value ?? 0).ToString(), out decimal decObj))
                    strAmount = decObj.ToString("0.00");
            }

            return strAmount;
        }
    }
}
