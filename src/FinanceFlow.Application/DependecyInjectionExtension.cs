using FinanceFlow.Application.AutoMapper;
using FinanceFlow.Application.UseCases.Accounts.Delete;
using FinanceFlow.Application.UseCases.Accounts.GetMonth;
using FinanceFlow.Application.UseCases.Accounts.Register;
using FinanceFlow.Application.UseCases.Login;
using FinanceFlow.Application.UseCases.Users.DeleteUser;
using FinanceFlow.Application.UseCases.Users.GetProfile;
using FinanceFlow.Application.UseCases.Users.Register;
using FinanceFlow.Application.UseCases.Users.UpdatePassword;
using FinanceFlow.Application.UseCases.Users.UpdateProfile;
using Microsoft.Extensions.DependencyInjection;
namespace FinanceFlow.Api;

public static class DependecyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services) 
    {
        AddUseCases(services);
        AddAutoMapper(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }
    private static void AddUseCases(IServiceCollection services)
    {

        //User
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IGetProfileUseCase, GetProfileUseCase>();
        services.AddScoped<IUpdateProfileUseCase, UpdateProfileUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
        services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();

        //Acount
        services.AddScoped<IRegisterAccountUseCase, RegisterAccountUseCase>();
        services.AddScoped<IGetMonthAccountsUseCase, GetMonthAccountsUseCase>();
        services.AddScoped<IDeleteAccountUseCase, DeleteAccountUseCase>();

    }
}
