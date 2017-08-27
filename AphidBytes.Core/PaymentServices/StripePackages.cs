using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Core.PaymentServices
{
    public class StripePackages
    {
        public const string USDollars = "usd";
        public static StripePackages Byter = new StripePackages { Amount = 0, Currency = USDollars, PlanId = "byter_account" };
        public static StripePackages Basic = new StripePackages { Amount = 7999, Currency = USDollars, PlanId = "basic_account" };
        public static StripePackages Premium = new StripePackages { Amount = 0, Currency = USDollars, PlanId = "premium_account" };
        public static StripePackages AphidLabs = new StripePackages { Amount = 7999, Currency = USDollars, PlanId = "aphidlab_account" };

        public class DataPlans : StripePackages
        {
            public static DataPlans Gb2Free = new DataPlans { Amount = 0, StorageAmount = 2, Currency = USDollars, PlanId = string.Empty };

            public static DataPlans Gb10Yearly = new DataPlans { Amount = 2999, StorageAmount = 10, Currency = USDollars, PlanId = "gb_20_yearly" };
            public static DataPlans Gb10Monthly = new DataPlans { Amount = 250, StorageAmount = 10, Currency = USDollars, PlanId = "gb_20_monthly" };

            public static DataPlans Gb50Yearly = new DataPlans { Amount = 4999, StorageAmount = 50, Currency = USDollars, PlanId = "gb_50_yearly" };
            public static DataPlans Gb50Monthly = new DataPlans { Amount = 416, StorageAmount = 50, Currency = USDollars, PlanId = "gb_50_monthly" };

            public static DataPlans Gb100Yearly = new DataPlans { Amount = 9999, StorageAmount = 100, Currency = USDollars, PlanId = "gb_100_yearly" };
            public static DataPlans Gb100Monthly = new DataPlans { Amount = 833, StorageAmount = 100, Currency = USDollars, PlanId = "gb_100_monthly" };

            public static DataPlans Gb200Yearly = new DataPlans { Amount = 19999, StorageAmount = 200, Currency = USDollars, PlanId = "gb_200_yearly" };
            public static DataPlans Gb200Monthly = new DataPlans { Amount = 1666, StorageAmount = 200, Currency = USDollars, PlanId = "gb_200_monthly" };

            public static DataPlans Gb500Yearly = new DataPlans { Amount = 42999, StorageAmount = 500, Currency = USDollars, PlanId = "gb_500_yearly" };
            public static DataPlans Gb500Monthly = new DataPlans { Amount = 3583, StorageAmount = 500, Currency = USDollars, PlanId = "gb_500_monthly" };

            public int StorageAmount { get; set; } = 0;

            public static List<DataPlans> AvailablePlans()
            {
                return new List<DataPlans>
                {
                    Gb2Free,
                    Gb10Yearly, Gb10Monthly,
                    Gb50Yearly, Gb50Monthly,
                    Gb100Yearly, Gb100Monthly,
                    Gb200Yearly, Gb200Monthly,
                    Gb500Yearly, Gb500Monthly,
                };
            }

            public static DataPlans GetFromId(string id)
            {
                return AvailablePlans().FirstOrDefault(s => s.PlanId == id) ?? Gb2Free;
            }
        }

        public int Amount { get; set; } = 0;
        public string PlanId { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty; 
    }
}
