
using AutoMapper;
using CoBudget.Domain.Repositories;
using CoBudget.Domain.Repositories.Expenses;
using CoBudget.Exception;
using CoBudget.Exception.ExceptionsBase;

namespace CoBudget.Application.UseCases.Expenses.Delete;

public class DeleteExpenseUseCase(IExpensesWriteRepository expensesRepository, IWorkUnity workUnit) : IDeleteExpenseUseCase
{
    private readonly IExpensesWriteRepository _expensesWriteRepository = expensesRepository;
    private readonly IWorkUnity _workUnit = workUnit;

    public async Task Execute(long id)
    {
        var result = await _expensesWriteRepository.Delete(id);

        if (result is false) 
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        await _workUnit.Commit();
    }
}
