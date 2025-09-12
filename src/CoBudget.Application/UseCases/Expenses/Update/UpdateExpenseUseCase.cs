using AutoMapper;
using CoBudget.Communication.Request;
using CoBudget.Domain.Repositories;
using CoBudget.Domain.Repositories.Expenses;
using CoBudget.Exception;
using CoBudget.Exception.ExceptionsBase;

namespace CoBudget.Application.UseCases.Expenses.Update;

public class UpdateExpenseUseCase(IExpenseUpdateRepository repository, IWorkUnit workUnity, IMapper mapper) : IUpdateExpenseUseCase
{
    private readonly IExpenseUpdateRepository _repository = repository;
    private readonly IWorkUnit _workUnity = workUnity;
    private readonly IMapper _mapper = mapper;

    public async Task Execute(long id, RequestExpenseJson request)
    {
        Validate(request);

        var expense = await _repository.GetById(id) ?? throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);

        _mapper.Map(request, expense);

        _repository.Update(expense);

        await _workUnity.Commit();

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
