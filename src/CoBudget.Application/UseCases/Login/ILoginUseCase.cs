using CoBudget.Communication.Request;
using CoBudget.Communication.Responses;

namespace CoBudget.Application.UseCases.Login;
public interface ILoginUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestLoginJSON request);
}
