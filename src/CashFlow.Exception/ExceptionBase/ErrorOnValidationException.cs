using System;

namespace CashFlow.Exception.ExceptionBase;

public class ErrorOnValidationException(List<string> errorMessages) : CashFlowException(string.Join(", ", errorMessages))
{
    public List<string> Errors { get; set; } = errorMessages;
}
