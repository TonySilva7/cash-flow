using CashFlow.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Attributes;

public class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute(params string[] roles) : base(typeof(AuthenticatedUserFilter))
    {
        Arguments = [roles];
    }
}
