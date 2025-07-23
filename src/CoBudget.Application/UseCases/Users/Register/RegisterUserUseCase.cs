using AutoMapper;
using CoBudget.Communication.Request;
using CoBudget.Communication.Responses;
using CoBudget.Domain.Entities;
using CoBudget.Domain.Repositories;
using CoBudget.Domain.Repositories.Users;
using CoBudget.Domain.Security.Cryptography;
using CoBudget.Domain.Security.Tokens;
using CoBudget.Exception;
using CoBudget.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace CoBudget.Application.UseCases.Users.Register;

public class RegisterUserUseCase(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository, IAcessTokenGenerator tokenGenerator, IWorkUnity workUnity, IMapper mapper, IPasswordEncripter encripter) : IRegisterUserUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IWorkUnity _workUnity = workUnity;
    private readonly IPasswordEncripter _encripter = encripter;
    private readonly IAcessTokenGenerator _tokenGenerator = tokenGenerator;
    private readonly IUserReadRepository _userReadRepository = userReadRepository;
    private readonly IUserWriteRepository _userWriteRepository = userWriteRepository;

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<User>(request);

        user.Password = _encripter.Encrypt(request.Password);
        user.UserId = Guid.NewGuid();

        await _userWriteRepository.Add(user);

        await _workUnity.Commit();

        //return _mapper.Map<ResponseRegisteredUserJson>(entity);
        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Token = _tokenGenerator.GenerateToken(user),
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = new UserValidator().Validate(request);

        var emailExists = await _userReadRepository.ExistsActiveUserWithEmail(request.Email);

        if (emailExists) result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_EXISTS));

        if (!result.IsValid)
        {
            var errorMessage = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ValidationException(errorMessage);
        }
    }
}

