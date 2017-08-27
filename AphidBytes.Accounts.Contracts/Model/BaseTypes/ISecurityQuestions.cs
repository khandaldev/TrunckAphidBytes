using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model.BaseTypes
{
    public interface ISecurityQuestions
    {
        string SecurityQuestion1 { get; set; }
        string SecurityQuestion2 { get; set; }
        string Answer1 { get; set; }
        string Answer2 { get; set; }
    }
}
