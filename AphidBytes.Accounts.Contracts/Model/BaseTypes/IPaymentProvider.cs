using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model.BaseTypes
{
    public interface IPaymentProvider
    {
        string NameOnCard { get; set; }
        StripeConfigurationModel StripeConfig { get; set; }

        string StripeToken { get; set; }
    }
}
