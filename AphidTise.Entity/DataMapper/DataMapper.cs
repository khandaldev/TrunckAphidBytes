using AphidBytes.Accounts.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidTise.Entity.DataMapper
{
    public static class DataMapper
    {
        

        public static AphidAccountType ToAphidAccountType(this tblAccountTypes tbl)
        {
            return new AphidAccountType
            {
                Active=tbl.Active.HasValue?tbl.Active.Value:false,
                AphidAccountName=tbl.AphidAccountName,
                AphidAccountTypeID = tbl.AphidAccountID,
                Description=tbl.Description
            };
        }
    }
}
