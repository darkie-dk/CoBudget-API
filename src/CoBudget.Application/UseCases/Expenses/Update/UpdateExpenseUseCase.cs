using AutoMapper;
using CoBudget.Communication.Request;
using CoBudget.Domain.Entities;
using CoBudget.Domain.Repositories;
using CoBudget.Domain.Repositories.Expenses;
using CoBudget.Exception;
using CoBudget.Exception.ExceptionsBase;
using CoBudget.Infrastructure.DataAccess.Repositories;

namespace CoBudget.Application.UseCases.Expenses.Update;

public class UpdateExpenseUseCase : IUpdateExpenseUseCase
{
    private readonly IExpenseUpdateRepository _repository;
    private readonly IWorkUnity _workUnity;
    private readonly IMapper _mapper;
    public UpdateExpenseUseCase(IExpenseUpdateRepository repository, IWorkUnity workUnity, IMapper mapper)
    {
        _repository = repository;
        _workUnity = workUnity;
        _mapper = mapper;
    }
    public async Task Execute(long id, RequestExpenseJson request)
    {
        Validate(request);

        var expense = await _repository.GetById(id);

        if (expense is null)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        _mapper.Map(request, expense);

        _repository.Update(expense);

        await _workUnity.Commit();

    }

    private void Validate(RequestExpenseJson request)
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
