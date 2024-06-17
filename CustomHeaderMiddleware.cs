using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem
{
  public class CustomHeaderMiddleware
  {
    private readonly RequestDelegate _next;

    public CustomHeaderMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      context.Response.Headers.Add("AdminAccount", "A");

      // Call the next middleware in the pipeline
      await _next(context);
    }



  }
}
