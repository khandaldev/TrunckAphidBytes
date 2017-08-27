using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
    public class AphidAccountType
    {
        public int AphidAccountTypeID { get; set; }
        public string AphidAccountName { get; set; }

        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
