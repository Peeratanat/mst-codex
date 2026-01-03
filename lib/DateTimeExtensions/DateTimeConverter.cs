using System;
using System.Collections.Generic;
using System.Text;

namespace DateTimeExtensions
{
    public class DateTimeConverter
    {
        public static DateTime? ExcelToDate(string dateValue)
        {
            if (dateValue != null)
            {
                string[] s = dateValue.ToString().Split('/');
                int y = 2000;
                int m = 1;
                int d = 1;

                if (s.Length == 3)
                {
                    y = int.Parse(s[2]);
                    m = int.Parse(s[1]);
                    d = int.Parse(s[0]);
                }
                else if (s.Length == 2)
                {
                    y = int.Parse(s[1]);
                    m = int.Parse(s[0]);
                    d = 1;
                }
                else
                {
                    return null;
                }

                return new DateTime(y, m, d);
            }
            else
            {
                return null;
            }
        }
    }
}
