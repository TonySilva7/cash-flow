﻿using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace CashFlow.Api.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;

    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var cultureInfo = new CultureInfo("en");

        var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

        if (!string.IsNullOrWhiteSpace(requestedCulture) && supportedLanguages.Exists(language => language.Name.Equals(requestedCulture)))
        {
            cultureInfo = new CultureInfo(requestedCulture);
        }

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await _next(context);
    }


}
