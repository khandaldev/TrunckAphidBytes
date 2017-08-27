using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
    public class myAdminModel
    {
        public string ConnectionId { get; set; }
        public string AdminName { get; set; }
        public string UserGroup { get; set; }
        public string GroupId { get; set; }
        public bool IsFree { get; set; }
        //if freeflag==0 ==> Busy
        //if freeflag==1 ==> Free
        public string freeflag { get; set; }

        //if tpflag==2 ==> User Admin
        //if tpflag==0 ==> User Member
        //if tpflag==1 ==> Admin

        public string tpflag { get; set; }
        public string AdminID { get; set; } 
    }
}
