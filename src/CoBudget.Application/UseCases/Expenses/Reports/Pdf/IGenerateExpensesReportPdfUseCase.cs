﻿namespace CoBudget.Application.UseCases.Expenses.Reports.Pdf;

public interface IGenerateExpensesReportPdfUseCase
{
    Task<byte[]> Execute(DateOnly date);
}
