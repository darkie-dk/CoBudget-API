using CoBudget.Communication.Responses;

namespace CoBudget.Application.UseCases.Expenses.GetById;

public interface IGetByIdUseCase
{
    Task<ResponseExpenseJson> Execute(long id);
}
