using AphidBytes.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Core.Extensions
{
    public static class NumberExtensions
    {
        public static ByteMeasurement Bytes(this double num)
        {
            return ByteMeasurement.Create(ByteTypes.Byte, num);
        }
        public static ByteMeasurement Bytes(this int num)
        {
            return Bytes((double)num);
        }
        public static ByteMeasurement Bytes(this long num)
        {
            return Bytes((double)num);
        }

        public static ByteMeasurement Kilobytes(this double num)
        {
            return ByteMeasurement.Create(ByteTypes.Kilobyte, num);
        }
        public static ByteMeasurement Kilobytes(this int num)
        {
            return Kilobytes((double)num);
        }
        public static ByteMeasurement Kilobytes(this long num)
        {
            return Kilobytes((double)num);
        }


        public static ByteMeasurement Megabytes(this double num)
        {
            return ByteMeasurement.Create(ByteTypes.Megabyte, num);
        }
        public static ByteMeasurement Megabytes(this int num)
        {
            return Megabytes((double)num);
        }
        public static ByteMeasurement Megabytes(this long num)
        {
            return Megabytes((double)num);
        }

        public static ByteMeasurement Gigabytes(this double num)
        {
            return ByteMeasurement.Create(ByteTypes.Gigabyte, num);
        }
        public static ByteMeasurement Gigabytes(this int num)
        {
            return Gigabytes((double)num);
        }
        public static ByteMeasurement Gigabytes(this long num)
        {
            return Gigabytes((double)num);
        }

        public static ByteMeasurement Terabytes(this double num)
        {
            return ByteMeasurement.Create(ByteTypes.Terabyte, num);
        }
        public static ByteMeasurement Terabytes(this int num)
        {
            return Terabytes((double)num);
        }
        public static ByteMeasurement Terabytes(this long num)
        {
            return Terabytes((double)num);
        }

    }
}
