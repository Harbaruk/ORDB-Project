using Microsoft.Extensions.DependencyInjection;
using Starter.ADOProvider.CommandBuilder;
using Starter.ADOProvider.CommandExecutor;
using Starter.Common.TypeActivator;
using Starter.Common.TypeHelper;
using Starter.DAL.Infrastructure;
using Starter.Services.ADOServices.UserManagement;
using Starter.Services.Token;
using Starter.Services.UserManagement;

namespace Starter.CompositionRoot
{
    /// <summary>
    /// Register all services here
    /// </summary>
    public static class Compositor
    {
        public static void Compose(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<IADOUserManagement, ADOUserService>();

            services.AddScoped(typeof(ITypeActivator<>), typeof(TypeActivator<>));
            services.AddScoped<ITypeHelper, TypeHelper>();

            services.AddScoped(typeof(ICommandExecutor<>), typeof(CommandExecutor<>));
            services.AddScoped<ICommandBuilder, CommandBuilder>();
        }
    }
}