using AutoMapper;
using CoBudget.Communication.Request;
using CoBudget.Communication.Responses;
using CoBudget.Domain.Repositories.Users;
using CoBudget.Domain.Security.Cryptography;
using CoBudget.Domain.Security.Tokens;
using CoBudget.Exception.ExceptionsBase;

namespace CoBudget.Application.UseCases.Login;
public class LoginUseCase
    (
        IAcessTokenGenerator acessTokenGenerator,
        IUserReadRepository userReadRepository,
        IPasswordEncripter passwordEncripter
    ) : ILoginUseCase
{
    private readonly IAcessTokenGenerator _acessTokenGenerator = acessTokenGenerator;
    private readonly IUserReadRepository _userReadRepository = userReadRepository;
    private readonly IPasswordEncripter _encripter = passwordEncripter;

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJSON request)
    {
        var user = await _userReadRepository.GetUserByEmail(request.Email) ?? throw new InvalidLoginException();

        var passwordMatch = _encripter.Verify(request.Password, user.Password);
        if (passwordMatch == false) throw new InvalidLoginException();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Token = _acessTokenGenerator.GenerateToken(user),
        };
    }
}
