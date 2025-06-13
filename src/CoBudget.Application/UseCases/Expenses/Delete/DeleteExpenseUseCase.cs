
using AutoMapper;
using CoBudget.Domain.Repositories;
using CoBudget.Exception;
using CoBudget.Exception.ExceptionsBase;
using CoBudget.Infrastructure.DataAccess.Repositories;

namespace CoBudget.Application.UseCases.Expenses.Delete;

public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{
    private readonly IExpensesWriteRepository _expensesWriteRepository;
    private readonly IWorkUnity _workUnit;
    public DeleteExpenseUseCase(IExpensesWriteRepository expensesRepository, IWorkUnity workUnit)
    {
        _expensesWriteRepository = expensesRepository;
        _workUnit = workUnit;
    }
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
