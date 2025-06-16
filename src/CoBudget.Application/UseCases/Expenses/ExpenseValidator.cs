using CoBudget.Communication.Request;
using CoBudget.Exception;
using FluentValidation;

namespace CoBudget.Application.UseCases.Expenses;

public class ExpenseValidator : AbstractValidator<RequestExpenseJson>
{
    public ExpenseValidator()
    {
        RuleFor(expense => expense.Title).NotEmpty().WithMessage(ResourceErrorMessages.TITLE_REQUIRED);
        RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage(ResourceErrorMessages.AMOUNT_MUST_GREATER_ZERO);
        RuleFor(expense => expense.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.DATE_CANNOT_FUTURE);
        RuleFor(expense => expense.ExpenseType).IsInEnum().WithMessage(ResourceErrorMessages.INVALID_EXPENSE_TYPE);
    }
}
