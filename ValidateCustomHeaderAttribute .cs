//using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace LearningSystem
{
  public class ValidateCustomHeaderAttribute:ActionFilterAttribute
  {
    private readonly string _customHeaderName;
    private readonly string _expectedHeaderValue;

    public ValidateCustomHeaderAttribute(string customHeaderName, string expectedHeaderValue)
    {
      _customHeaderName = customHeaderName ?? throw new ArgumentNullException(nameof(customHeaderName));
      _expectedHeaderValue = expectedHeaderValue ?? throw new ArgumentNullException(nameof(expectedHeaderValue));
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
      if (!context.HttpContext.Request.Headers.TryGetValue(_customHeaderName, out var headerValues) ||
          (headerValues.Count != 1 || headerValues[0] != _expectedHeaderValue))
      {
        // Header is missing or has an invalid value, return a 403 Forbidden response
        context.Result = new ForbidResult();
      }
    }

  }
}
