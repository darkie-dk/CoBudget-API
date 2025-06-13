using AutoMapper;
using CoBudget.Communication.Request;
using CoBudget.Communication.Responses;
using CoBudget.Domain.Entities;
using CoBudget.Domain.Repositories;
using CoBudget.Exception.ExceptionsBase;
using CoBudget.Infrastructure.DataAccess.Repositories;

namespace CoBudget.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpensesWriteRepository _repository;
    private readonly IWorkUnity _workUnity;
    private readonly IMapper _mapper;
    public RegisterExpenseUseCase(IExpensesWriteRepository repository, IWorkUnity workUnity, IMapper mapper)
    {
        _repository = repository;
        _workUnity = workUnity;
        _mapper = mapper;
    }
    public async Task<ResponseRegisteredExpenseJson> Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);

        var entity = _mapper.Map<Expense>(request);

        await _repository.Add(entity);

        await _workUnity.Commit();

        return _mapper.Map<ResponseRegisteredExpenseJson>(entity);
    }

    private void Validate(RequestRegisterExpenseJson request)
    {
        var validator = new RegisterExpenseValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessage = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ValidationException(errorMessage);
        }
    }
}

