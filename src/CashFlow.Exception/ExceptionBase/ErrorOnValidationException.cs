using System;

namespace CashFlow.Exception.ExceptionBase;

public class ErrorOnValidationException : CashFlowException
{

  public List<string> Errors { get; set; }

  public ErrorOnValidationException(List<string> message)
  {
    Errors = message;
  }
}
