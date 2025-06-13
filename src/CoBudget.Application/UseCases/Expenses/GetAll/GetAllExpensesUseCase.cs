using AutoMapper;
using CoBudget.Communication.Responses;
using CoBudget.Infrastructure.DataAccess.Repositories;

namespace CoBudget.Application.UseCases.Expenses.GetAll;

public class GetAllExpensesUseCase : IGetAllExpensesUseCase
{
    private readonly IExpensesReadRepository _expensesRepository;
    private readonly IMapper _mapper;
    public GetAllExpensesUseCase(IExpensesReadRepository expensesRepository, IMapper mapper)
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
