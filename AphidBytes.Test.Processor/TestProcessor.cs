using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AphidBytes.Test.Contracts;

namespace AphidBytes.Test.Processor
{
    public class TestProcessor  :ITest 
    {
        public string TestMethod(string name)
        {

            return name + " is called ";
        }
    }
}
