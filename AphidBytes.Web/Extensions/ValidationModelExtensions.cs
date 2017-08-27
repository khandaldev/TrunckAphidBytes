using AphidBytes.Accounts.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AphidBytes.Web.Extensions
{
    public static class ValidationModelExtensions
    {
        public static void FillFromModelState(this ValidationModel model, ModelStateDictionary modelState)
        {
            modelState.Values.ToList().ForEach(v =>
            {
                v.Errors.ToList().ForEach(e =>
                {
                    model.AddError(e.ErrorMessage);
                });
            });
        }
    }
}