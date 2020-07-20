using System;
using System.Collections.Generic;
using System.Text;

namespace ResponseCreator.Utils
{
    public class SizeConvertionUtil
    {
        public static decimal FromByteLongToDecimalMB(long size)
        {
            decimal result = (decimal)size / (1024 * 1024);

            return result;
        }
    }
}
