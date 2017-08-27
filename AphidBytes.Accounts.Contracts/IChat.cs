using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AphidBytes.Accounts.Contracts.Model;

namespace AphidBytes.Accounts.Contracts
{
    public interface IChat
    {

        int fetch_acType(string uid);
        int fetch_guestacType(string uidd);

    }
}
