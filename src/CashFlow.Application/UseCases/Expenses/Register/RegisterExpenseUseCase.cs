﻿using CashFlow.Domain.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories;
using AutoMapper;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase(IExpensesRepository expensesRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRegisterExpenseUseCase
{
    public async Task<ResponseRegisteredExpense> Execute(RequestRegisterExpense expense)
    {
        Validate(expense);

        var entity = mapper.Map<Expense>(expense);

        await expensesRepository.AddAsync(entity);
        await unitOfWork.Commit();

        return mapper.Map<ResponseRegisteredExpense>(entity);
    }

    private static void Validate(RequestRegisterExpense expense)
    {
        var validator = new RegisterExpenseValidator();
        var result = validator.Validate(expense);

        if (!result.IsValid)
        {
            var errorsMessage = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorsMessage);
        }
    }
}
