using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AphidBytes.Core.Extensions;

namespace AphidBytes.Core.Utility
{
    public class ByteConverter
    {
        private const double BytesPerPower = 1024D;

        public static ByteMeasurement Convert(ByteMeasurement byteMeasurement, ByteTypes type)
        {
            var powerOfConversion = (int)byteMeasurement.Type - (int)type;
            if (powerOfConversion > 0)
            {
                var value = byteMeasurement.Units / (Math.Pow(BytesPerPower, Math.Abs(powerOfConversion)));
                return ByteMeasurement.Create(type, value);
            }

            if (powerOfConversion < 0)
            {
                var value = byteMeasurement.Units * (Math.Pow(BytesPerPower, Math.Abs(powerOfConversion)));
                return ByteMeasurement.Create(type, value); 
            }

            return byteMeasurement;
        }

        public static ByteMeasurement Simplify(ByteMeasurement byteMeasurement)
        {
            var values = Enum.GetValues(typeof(ByteTypes)).Cast<ByteTypes>().ToList();
            var currValue = byteMeasurement.Units;
            var currByte = (int)byteMeasurement.Type;
            var maxByte = (int)values.Max();

            while(currValue >= BytesPerPower && currByte < maxByte)
            {
                currByte += 1;
                currValue *= BytesPerPower;
            }

            return ByteMeasurement.Create((ByteTypes)currByte, currValue);
        }
    }
}
