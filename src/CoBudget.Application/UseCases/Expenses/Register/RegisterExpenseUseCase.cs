using CoBudget.Communication.Request;
using CoBudget.Communication.Responses;
using CoBudget.Domain.Entities;
using CoBudget.Domain.Repositories;
using CoBudget.Domain.Repositories.Expenses;
using CoBudget.Exception.ExceptionsBase;
namespace CoBudget.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpensesRepository _repository;
    private readonly IWorkUnity _workUnity;
    public RegisterExpenseUseCase(IExpensesRepository repository, IWorkUnity workUnity)
    {
        _repository = repository;
        _workUnity = workUnity;
    }
    public ResponseExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);

        var entity = new Expense
        {
            Amount = request.Amount,
            Date = request.Date,
            Title = request.Title,
            Description = request.Description, 
            ExpenseType = (Domain.Enum.ExpenseType)request.ExpenseType
        };

        _repository.Add(entity);

        _workUnity.Commit();

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

