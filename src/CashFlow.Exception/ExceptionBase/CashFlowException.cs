using System;

namespace CashFlow.Exception.ExceptionBase;

public abstract class CashFlowException : SystemException
{
    public abstract int StatuCode { get; }
    public abstract List<string> GetErrors();

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
