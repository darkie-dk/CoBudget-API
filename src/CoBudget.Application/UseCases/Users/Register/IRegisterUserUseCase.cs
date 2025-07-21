using CoBudget.Communication.Request;
using CoBudget.Communication.Responses;

namespace CoBudget.Application.UseCases.Users.Register;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}
