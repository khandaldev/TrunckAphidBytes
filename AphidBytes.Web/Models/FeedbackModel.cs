using AphidBytes.Accounts.Contracts.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AphidBytes.Web.Models
{
    public class FeedbackModel
    {
        public ValidationModel Validation { get; set; } = new ValidationModel();
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Feedback subject is required")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Feedback body is required")]
        public string Body { get; set; }

        public bool ConfirmationSent { get; set; }
    }
}