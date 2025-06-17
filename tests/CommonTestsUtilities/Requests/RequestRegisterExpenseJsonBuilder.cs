using Bogus;
using CoBudget.Communication.Enum;
using CoBudget.Communication.Request;

namespace CommonTestsUtilities.Requests;

public class RequestRegisterExpenseJsonBuilder
{
    public static RequestExpenseJson Build()
    { 
      return new Faker<RequestExpenseJson>()
            .RuleFor(req => req.Title, faker => faker.Commerce.ProductName())
            .RuleFor(req => req.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(req => req.Amount, faker => faker.Random.Decimal())
            .RuleFor(req => req.Date, faker => faker.Date.Past())
            .RuleFor(req => req.PaymentType, faker => faker.PickRandom<PaymentType>());
    }
}
