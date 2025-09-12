using AutoMapper;
using CoBudget.Communication.Request;
using CoBudget.Communication.Responses;
using CoBudget.Domain.Entities;
using CoBudget.Domain.Repositories;
using CoBudget.Domain.Repositories.Expenses;
using CoBudget.Exception.ExceptionsBase;

namespace CoBudget.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase(IExpensesWriteRepository repository, IWorkUnit workUnity, IMapper mapper) : IRegisterExpenseUseCase
{
    private readonly IExpensesWriteRepository _repository = repository;
    private readonly IWorkUnit _workUnity = workUnity;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseRegisteredExpenseJson> Execute(RequestExpenseJson request)
    {
        Validate(request);

        var entity = _mapper.Map<Expense>(request);

        await _repository.Add(entity);

        await _workUnity.Commit();

        return _mapper.Map<ResponseRegisteredExpenseJson>(entity);
    }

    private static void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessage = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ValidationException(errorMessage);
        }
    }
}

