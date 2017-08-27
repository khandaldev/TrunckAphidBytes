using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Core.Utility
{
    public enum ByteTypes
    {
        [Description("B")]
        Byte = 1,
        [Description("KB")]
        Kilobyte = 2,
        [Description("MB")]
        Megabyte = 3,
        [Description("GB")]
        Gigabyte = 4,
        [Description("TB")]
        Terabyte = 5,
    }
}
