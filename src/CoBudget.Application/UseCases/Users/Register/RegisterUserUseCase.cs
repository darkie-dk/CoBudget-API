using AutoMapper;
using CoBudget.Communication.Request;
using CoBudget.Communication.Responses;
using CoBudget.Domain.Entities;
using CoBudget.Domain.Repositories;
using CoBudget.Domain.Repositories.Expenses;
using CoBudget.Domain.Security;
using CoBudget.Exception.ExceptionsBase;

namespace CoBudget.Application.UseCases.Users.Register;

public class RegisterUserUseCase(IExpensesWriteRepository repository, IWorkUnity workUnity, IMapper mapper, IPasswordEncripter encripter) : IRegisterUserUseCase
{
    private readonly IExpensesWriteRepository _repository = repository;
    private readonly IWorkUnity _workUnity = workUnity;
    private readonly IMapper _mapper = mapper;
    private readonly IPasswordEncripter _encripter = encripter;

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        Validate(request);

        var user = _mapper.Map<User>(request);
        user.Password = _encripter.Encrypt(request.Password);

        //await _repository.Add(entity);

        //await _workUnity.Commit();

        //return _mapper.Map<ResponseRegisteredUserJson>(entity);
        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
        };
    }

    private static void Validate(RequestRegisterUserJson request)
    {
        var result = new UserValidator().Validate(request);

        if (!result.IsValid)
        {
            var errorMessage = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ValidationException(errorMessage);
        }
    }
}

