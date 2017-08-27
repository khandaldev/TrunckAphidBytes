using AphidBytes.Core.Configuration;
using Stripe;
using System;
using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Core.PaymentServices
{
    public class StripeClient
    {
        private const string AphidbyteChargeDescription = "Aphidbyte subscription";
        private const string DataPlanKeyMetadata = "aphid_data_plan";
        public static readonly ILog Log = LogManager.GetLogger(typeof(StripeClient)); 
        public static readonly ConfigurationValue<string> StripeSecretApiKey = new ConfigurationValue<string>("stripe.secretKey"); 
        public static readonly ConfigurationValue<string> StripePublishApiKey = new ConfigurationValue<string>("stripe.publishKey");

        public static void ConfigureStripeClient()
        {
            StripeConfiguration.SetApiKey(StripeSecretApiKey.Value); 
        }

        public static bool CreateStripeCharge(StripePackages package, string token)
        {
            try
            {
                var request = new StripeChargeCreateOptions
                {
                    Capture = true,
                    Amount = package.Amount,
                    Currency = package.Currency,
                    SourceTokenOrExistingSourceId = token,
                    Description = AphidbyteChargeDescription //potentially add customerId here
                };

                var chargeService = new StripeChargeService();
                StripeCharge stripeCharge = chargeService.Create(request);
            }
            catch (Exception e)
            {
                Log.Error("Failed to charge the account", e);
                return false;
            }

            return true;
        }

        public static StripeCustomer CreateStripeCustomer(StripePackages package, string emailAddress, string firstName, string lastName, string userId, string token, string couponCode = null)
        {
            try
            {
                var request = new StripeCustomerCreateOptions
                {
                    Email = emailAddress,
                    SourceToken = token,
                    PlanId = package.PlanId,
                    Description = $"{firstName} {lastName} ({emailAddress})", 
                    Metadata = new Dictionary<string, string>
                    {
                        {"FirstName", firstName },
                        {"LastName", lastName },
                        {"UserId", userId}
                    },
                };

                if (!string.IsNullOrWhiteSpace(couponCode))
                {
                    request.CouponId = couponCode;
                }

                var customerService = new StripeCustomerService();
                return customerService.Create(request);
            }
            catch (Exception e)
            {
                Log.Error("Failed to create the customer account", e);
                return null;
            }
        }

        public static StripeCustomer UpdateStripeCustomer(string customerId, string token)
        {
            try
            {
                var request = new StripeCustomerUpdateOptions
                {
                    SourceToken = token,
                };

                var customerService = new StripeCustomerService();
                return customerService.Update(customerId, request);
            }
            catch (Exception e)
            {
                Log.Error("Failed to create the customer account", e);
                return null;
            }
        }

        public static StripeSubscription UpdateDataPlanSubscription(string customerId, string planId)
        {
            try
            {
                var subscriptionService = new StripeSubscriptionService();
                IEnumerable<StripeSubscription> customerSubscriptions = subscriptionService.List(customerId);
                if (customerSubscriptions.Any(s => s.Metadata.ContainsKey(DataPlanKeyMetadata)))
                {
                    //remove the existing subscription first 
                    var existingSubscription = customerSubscriptions.First(s => s.Metadata.ContainsKey(DataPlanKeyMetadata));
                    subscriptionService.Cancel(customerId, existingSubscription.Id); 
                }

                if (string.IsNullOrWhiteSpace(planId))
                {
                    return null;
                } 

                return subscriptionService.Create(customerId, planId, new StripeSubscriptionCreateOptions
                {
                    Metadata = new Dictionary<string, string>
                    {
                        { DataPlanKeyMetadata, planId }
                    }
                }); 
            }
            catch (Exception e)
            {
                Log.Error("Failed to create the customer subscription", e);
                return null;
            }
        }

        public static StripeCoupon ValidatePromotion(string promotioncode)
        {
            try
            {
                var couponService = new StripeCouponService();
                return couponService.Get(promotioncode);
            }
            catch (Exception e)
            {
                Log.Error("Failed to retrieve a coupon", e);
                return null;
            }
        }

        public static bool DeleteStripeCustomer(string customerId)
        {
            try
            {
                var customerService = new StripeCustomerService();
                customerService.Delete(customerId);
            }
            catch (Exception e)
            {
                Log.Error("Failed to create the customer account", e);
                return false;
            }

            return true;
        }
    }
}
