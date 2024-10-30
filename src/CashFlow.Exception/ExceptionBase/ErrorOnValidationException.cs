using System;
using System.Net;

namespace CashFlow.Exception.ExceptionBase;

public class ErrorOnValidationException(List<string> errorMessages) : CashFlowException(string.Join(", ", errorMessages))
{
    private readonly List<string> _errors = errorMessages;

    public override int StatuCode => (int)HttpStatusCode.BadRequest;

    public override List<string> GetErrors()
    {
        return _errors;
    }
}
