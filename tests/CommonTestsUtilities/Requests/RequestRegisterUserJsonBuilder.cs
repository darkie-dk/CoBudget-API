using Bogus;
using CoBudget.Communication.Request;

namespace CommonTestsUtilities.Requests;

public class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Build()
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(r => r.Name, f => f.Person.FirstName)
            .RuleFor(r => r.Email, f => f.Person.Email)
            .RuleFor(r => r.Password, f => f.Internet.Password(prefix:"!Aa1", length:8))
            .Generate();
            //ADD
            //.RuleFor(r => r.ConfirmPassword, (f, r) => r.Password)
    }
}
