using System;

namespace CashFlow.Exception.ExceptionBase;

public abstract class CashFlowException : SystemException
{
  public CashFlowException()
  {
  }

  protected CashFlowException(string message)
      : base(message)
  {
  }

  protected CashFlowException(string message, SystemException inner)
      : base(message, inner)
  {
  }
}
