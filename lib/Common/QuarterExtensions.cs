using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class QuarterExtensions
    {
        public static int GetQuarter(this DateTime date)
        {
            if (date.Month >= 1 && date.Month <= 3)
                return 1;
            else if (date.Month >= 4 && date.Month <= 6)
                return 2;
            else if (date.Month >= 7 && date.Month <= 9)
                return 3;
            else
                return 4;
        }
    }
}
