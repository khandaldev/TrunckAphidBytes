using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
   public  class BindDropDown
    {
        public int id { get; set; }
       [Display(Name = "Value")]
        public string Value { get; set; }
       public string AudioFileName { get; set; }
       public string Name { get; set; }
       public string ImageName { get; set; }
       public string  Path { get; set; }
    }
}
