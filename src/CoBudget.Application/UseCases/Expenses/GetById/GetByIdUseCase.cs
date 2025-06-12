using AutoMapper;
using CoBudget.Communication.Responses;
using CoBudget.Domain.Repositories.Expenses;

namespace CoBudget.Application.UseCases.Expenses.GetById;

public class GetByIdUseCase : IGetByIdUseCase
{
    private readonly IExpensesRepository _expensesRepository;
    private readonly IMapper _mapper;
    public GetByIdUseCase(IExpensesRepository expensesRepository, IMapper mapper)
    {
        _expensesRepository = expensesRepository;
        _mapper = mapper;
    }
    public async Task<ResponseExpenseJson> Execute(long id)
    {
        var result = await _expensesRepository.GetById(id);

        return _mapper.Map<ResponseExpenseJson>(result);
    }
}
