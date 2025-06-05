﻿using CoBudget.Application.UseCases.Expenses.Register;
using Microsoft.Extensions.DependencyInjection;

namespace CoBudget.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
        }
    }
}
