using AutoMapper;
using CoBudget.Communication.Responses;
using CoBudget.Domain.Repositories.Expenses;

namespace CoBudget.Application.UseCases.Expenses.GetAll;

public class GetAllExpensesUseCase : IGetAllExpensesUseCase
{
    private readonly IExpensesRepository _expensesRepository;
    private readonly IMapper _mapper;
    public GetAllExpensesUseCase(IExpensesRepository expensesRepository, IMapper mapper)
    {
        _expensesRepository = expensesRepository;
        _mapper = mapper;
    }
    public async Task<ResponseExpensesJson> Execute()
    {
        var result = await _expensesRepository.GetAll();

        return new ResponseExpensesJson
        {
            Expenses = _mapper.Map<List<ResponseShortExpenseJson>>(result)
        };
    }
}
