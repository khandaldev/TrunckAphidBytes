using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Core.Utility
{
    public class ByteMeasurement
    {
        private ByteMeasurement(ByteTypes type, double units)
        {
            Type = type;
            Units = units;
        }

        public ByteTypes Type { get; set; } = ByteTypes.Byte;

        public double Units { get; set; } = 0L;

        public static ByteMeasurement Create(ByteTypes type, double units)
        {
            return new ByteMeasurement(type, units);
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return string.Empty;
        }

        public override string ToString()
        {
            var description = GetEnumDescription(Type);
            return $"{Units}{description}";
        }
    }
}
