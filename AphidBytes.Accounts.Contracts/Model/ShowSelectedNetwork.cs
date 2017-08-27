using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
   public class ShowSelectedNetwork
    {
        public int? Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string category { get; set; }
    }
}
