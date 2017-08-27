using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAnnotationsExtensions;

namespace AphidBytes.Test.Contracts.Model
{
    public class TestViewModel
    {


        public int ID { get; set; }
        [Numeric]
        public string Name { get; set; }
    }
}
