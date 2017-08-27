using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
    public class myChatUserModel
    {
        public string ConnectionId { get; set; }
        public string uname { get; set; }
        public string UserGroup { get; set; }
        public string GroupId { get; set; }
        //if freeflag==0 ==> Busy
        //if freeflag==1 ==> Free
        //if tpflag==2 ==> User Admin
        //if tpflag==0 ==> User Member
        //if tpflag==1 ==> Admin
        public string userId { get; set; } 
    }
}
