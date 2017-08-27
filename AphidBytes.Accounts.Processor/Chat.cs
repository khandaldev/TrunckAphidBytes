using AphidBytes.Accounts.Contracts;
using AphidTise.Entity.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Processor
{
    public class Chat : IChat
    {
        RepositoryChat repository = new RepositoryChat();

        public int fetch_acType(string uid)
        {
            return repository.fetch_acType(uid);
        }

        public int fetch_guestacType(string uidd)
        {
            return repository.fetch_guestacType(uidd);
        }


    }
}
