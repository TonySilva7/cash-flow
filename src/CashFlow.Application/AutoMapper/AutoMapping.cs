using System;
using AutoMapper;
using CashFlow.Application.UseCases;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Cryptography;

namespace CashFlow.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestExpense, Expense>();

        CreateMap<RequestRegisterUser, User>().ForMember(
            dest => dest.Password,
            opt => opt.Ignore()
        );
    }

    private void EntityToResponse()
    {
        CreateMap<Expense, ResponseRegisteredExpense>();
        CreateMap<Expense, ResponseShortExpense>();
        CreateMap<Expense, ResponseExpense>();

        CreateMap<User, ResponseRegisteredUser>();
        CreateMap<User, ResponseLoggedUser>();
    }
}
