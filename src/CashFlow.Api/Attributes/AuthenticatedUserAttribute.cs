using CashFlow.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Attributes;

public class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
    {
    }
}
