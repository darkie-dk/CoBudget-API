using AutoMapper;
using CoBudget.Communication.Responses;
using CoBudget.Domain.Repositories.Expenses;

namespace CoBudget.Application.UseCases.Expenses.GetAll;

public class GetAllExpensesUseCase(IExpensesReadRepository expensesRepository, IMapper mapper) : IGetAllExpensesUseCase
{
    private readonly IExpensesReadRepository _expensesRepository = expensesRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseExpensesJson> Execute()
    {
        var result = await _expensesRepository.GetAll();

        return new ResponseExpensesJson
        {
            Expenses = _mapper.Map<List<ResponseShortExpenseJson>>(result)
        };
    }
}
