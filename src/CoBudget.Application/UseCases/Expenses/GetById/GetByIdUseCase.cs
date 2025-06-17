using AutoMapper;
using CoBudget.Communication.Responses;
using CoBudget.Domain.Repositories.Expenses;
using CoBudget.Exception;
using CoBudget.Exception.ExceptionsBase;

namespace CoBudget.Application.UseCases.Expenses.GetById;

public class GetByIdUseCase : IGetByIdUseCase
{
    private readonly IExpensesReadRepository _expensesRepository;
    private readonly IMapper _mapper;
    public GetByIdUseCase(IExpensesReadRepository expensesRepository, IMapper mapper)
    {
        _expensesRepository = expensesRepository;
        _mapper = mapper;
    }
    public async Task<ResponseExpenseJson> Execute(long id)
    {
        var result = await _expensesRepository.GetById(id);

        if (result == null) 
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        return _mapper.Map<ResponseExpenseJson>(result);
    }
}
