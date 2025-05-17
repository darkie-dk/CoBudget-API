using CoBudget.Communication.Request;
using CoBudget.Communication.Responses;
using CoBudget.Exception.ExceptionsBase;

namespace CoBudget.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase
{
    public ResponseExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);

        return new ResponseExpenseJson();
    }

    private void Validate(RequestRegisterExpenseJson request)
    {
        var validator = new RegisterExpenseValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessage = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ValidationException(errorMessage);
        }
    }
}

