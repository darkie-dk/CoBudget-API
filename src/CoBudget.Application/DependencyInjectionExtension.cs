using CoBudget.Application.AutoMapper;
using CoBudget.Application.UseCases.Expenses.Delete;
using CoBudget.Application.UseCases.Expenses.GetAll;
using CoBudget.Application.UseCases.Expenses.GetById;
using CoBudget.Application.UseCases.Expenses.Register;
using CoBudget.Application.UseCases.Expenses.Update;
using CoBudget.Application.UseCases.Reports;
using Microsoft.Extensions.DependencyInjection;

namespace CoBudget.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddAutoMapper(services);
            AddUseCase(services);
        }

        private static void AddAutoMapper(IServiceCollection services) 
        {
            services.AddAutoMapper(typeof(AutoMapping));
        }

        private static void AddUseCase(IServiceCollection services) 
        {
            services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
            services.AddScoped<IGetAllExpensesUseCase, GetAllExpensesUseCase>();
            services.AddScoped<IGetByIdUseCase, GetByIdUseCase>();
            services.AddScoped<IDeleteExpenseUseCase, DeleteExpenseUseCase>();
            services.AddScoped<IUpdateExpenseUseCase, UpdateExpenseUseCase>();
         
            services.AddScoped<IGenerateExpenseReportUseCase, GenerateExpenseReportUseCase>();
        }
    }
}
