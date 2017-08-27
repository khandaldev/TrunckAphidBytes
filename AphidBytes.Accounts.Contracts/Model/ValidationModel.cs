using System.Collections.Generic;
using System.Linq;

namespace AphidBytes.Accounts.Contracts.Model
{
    public class ValidationModel
    {
        public List<ValidationCtaModel> ValidationCtas { get; set; } = new List<ValidationCtaModel>();

        public bool HasCta { get { return ValidationCtas != null && ValidationCtas.Any(); } }

        public void AddError(string message)
        {
            AddCta(ValidationTypes.Error, message);
        }
        public void AddWarning(string message)
        {
            AddCta(ValidationTypes.Warning, message);
        }
        public void AddInformation(string message)
        {
            AddCta(ValidationTypes.Information, message);
        }
        private void AddCta(ValidationTypes type, string message)
        {
            ValidationCtas.Add(new ValidationCtaModel
            {
                Type = type,
                Message = message
            });
        }
    }

    public class ValidationCtaModel
    {
        public ValidationTypes Type { get; set; }
        public string Message { get; set; }
    }

    public enum ValidationTypes
    {
        Information, 
        Warning, 
        Error
    }
}
