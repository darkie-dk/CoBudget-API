﻿using CoBudget.Communication.Responses;

namespace CoBudget.Application.UseCases.Expenses.GetAll;

public interface IGetAllExpensesUseCase
{
    Task<ResponseExpensesJson> Execute();
}
