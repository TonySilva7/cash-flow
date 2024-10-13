using System;

namespace CashFlow.Exception.ExceptionBase;

public abstract class CashFlowException : SystemException
{
  public CashFlowException()
  {
  }

  public CashFlowException(string message)
      : base(message)
  {
  }

  public CashFlowException(string message, SystemException inner)
      : base(message, inner)
  {
  }
}
