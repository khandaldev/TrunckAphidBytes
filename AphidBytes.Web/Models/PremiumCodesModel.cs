using AphidBytes.Accounts.Contracts.Model;

namespace AphidBytes.Web.Models
{
    public class PremiumCodesModel
    {
        public string PremiumEntryCode { get; set; }
        public ValidationModel Validation { get; set; } = new ValidationModel();
    }
}