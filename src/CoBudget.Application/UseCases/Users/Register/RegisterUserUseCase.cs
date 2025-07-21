using AutoMapper;
using CoBudget.Communication.Request;
using CoBudget.Communication.Responses;
using CoBudget.Domain.Entities;
using CoBudget.Domain.Repositories;
using CoBudget.Domain.Repositories.User;
using CoBudget.Domain.Security;
using CoBudget.Exception;
using CoBudget.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace CoBudget.Application.UseCases.Users.Register;

public class RegisterUserUseCase(IUserReadRepository userReadRepository, IWorkUnity workUnity, IMapper mapper, IPasswordEncripter encripter) : IRegisterUserUseCase
{
    private readonly IUserReadRepository _UserReadRepository = userReadRepository;
    private readonly IWorkUnity _workUnity = workUnity;
    private readonly IMapper _mapper = mapper;
    private readonly IPasswordEncripter _encripter = encripter;

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

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

    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = new UserValidator().Validate(request);

        var emailExists = await _UserReadRepository.ExistsActiveUserWithEmail(request.Email);

        if (emailExists) result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_EXISTS));

        if (!result.IsValid)
        {
            var errorMessage = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ValidationException(errorMessage);
        }
    }
}

