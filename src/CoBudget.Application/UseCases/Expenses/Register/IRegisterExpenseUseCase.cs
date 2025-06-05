using CoBudget.Communication.Request;
using CoBudget.Communication.Responses;

namespace CoBudget.Application.UseCases.Expenses.Register;

public interface IRegisterExpenseUseCase
{
    ResponseExpenseJson Execute(RequestRegisterExpenseJson request);
}
