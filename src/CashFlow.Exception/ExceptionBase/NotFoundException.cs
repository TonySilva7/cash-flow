using System;

namespace CashFlow.Exception.ExceptionBase;

public class NotFoundException(string message) : CashFlowException(message)
{

}
