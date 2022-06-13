using System.ComponentModel.DataAnnotations;
using IO.TedTalk.Core.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IO.TedTalk.Api.Framrwork.AspNetCore.Mvc.Validation;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            // ModelState is in Microsoft.AspNetCore.Mvc so we can't include it in our AivwaValidationException.
            // So we use System.ComponentModel.DataAnnotations.ValidationResult which is in more general namespace
            // Any alternative candidate?!

            IList<ValidationResult> validationErrors = new List<ValidationResult>();
            foreach (var key in context.ModelState.Keys)
            {
                var data = context.ModelState[key];
                foreach (ModelError modelError in data.Errors)
                {
                    var validationError = new ValidationResult(modelError.ErrorMessage, new[] { key });
                    validationErrors.Add(validationError);
                }
            }

            throw new IOValidationException("Invalid inpit!", validationErrors);
        }
    }
}
