using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
   public class DataPlanDetail
    {
        public ValidationModel Validation { get; set; } = new ValidationModel();
        public string PlanId { get; set; }
        public string PlanDescription { get; set; }
        public DateTime? Expires { get; set; }
        public long Used { get; set; }
        public long Free { get; set; }
        public string UsedShow { get; set; }
        public string totalshow { get; set; }
        public string FreeShow { get; set; }
        public byte[] Used1 { get; set; }
        public byte[] Free1 { get; set; }
        public string NewCount { get; set; }
    }
}
