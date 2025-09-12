using CoBudget.Application.UseCases.Users.Register;
using CommonTestsUtilities.Mapper;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Requests;
using Shouldly;

namespace UseCases.Test.Users;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.ShouldNotBeNull();  
        result.Name.ShouldBe(request.Name);
        result.Token.ShouldNotBeNull();
    }
    private RegisterUserUseCase CreateUseCase()
    {
        var mapper = MapperBuilder.Build();
        var workunit = WorkUnitBuilder.Build();

        return new RegisterUserUseCase(null, null, null, null, workunit, mapper);
    }
}