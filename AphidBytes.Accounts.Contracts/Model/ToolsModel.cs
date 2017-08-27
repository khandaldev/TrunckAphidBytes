using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
   public class ToolsModel
    {
        public List<AllTools> AllToolsInfo { get; set; }
        public List<UserTool> UserTools { get; set; }
        public List<Filecontent> filecontent { get; set; }
    }

   public class AllTools
   {
       public int ToolId { get; set; }
       public string ToolName { get; set; }
       public int IsActive { get; set; }
       public string ToolINfo { get; set; }
       public string ImagePath { get; set; }
       
   }
   public class UserTool
   {
      
       public int ToolId { get; set; }
       public Guid userid { get; set; }
       public DateTime CreatedOn { get; set; }
       public DateTime Modify { get; set; }
   }
   public class Filecontent
   {
       public int ToolId { get; set; }
       public string FileName { get; set; }
       public Guid Userid { get; set; }
       public string ToolContent { get; set; }

   }
}
