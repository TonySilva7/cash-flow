using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase
{
    public ResponseRegisteredExpense Execute(RequestRegisterExpense expense)
    {
       Validate(expense);

        return new ResponseRegisteredExpense
         {
              Title = expense.Title
         };
    }

    private void Validate(RequestRegisterExpense expense)
    {
        var titleIsInvalid = string.IsNullOrWhiteSpace(expense.Title);
        var amountIsInvalid = expense.Amount < 0;
        var dateIsInvalid = DateTime.Compare(expense.Date, DateTime.UtcNow) > 0;
        var paymentTypeIsInvalid = !Enum.IsDefined(typeof(PaymentType), expense.PaymentType);
        
        if (titleIsInvalid)
        {
            throw new ArgumentException("Title is required");
        }

        if (amountIsInvalid)
        {
            throw new ArgumentException("Amount must be greater than zero");
        }

        if (dateIsInvalid)
        {
            throw new ArgumentException("Date is required");
        }

        if (paymentTypeIsInvalid)
        {
            throw new ArgumentException("Payment type is invalid");
        }
    }
}
