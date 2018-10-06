using System;
using Microsoft.Extensions.DependencyInjection;
using Starter.ADOProvider.CommandBuilder;
using Starter.ADOProvider.CommandExecutor;
using Starter.Common.TypeActivator;
using Starter.DAL.Infrastructure;
using Starter.Services.Token;

namespace Starter.CompositionRoot
{
    /// <summary>
    /// Register all services from here
    /// </summary>
    public static class Compositor
    {
        public static void Compose(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped(typeof(ITypeActivator<>), typeof(TypeActivator<>));
            services.AddScoped(typeof(ICommandExecutor<>), typeof(CommandExecutor<>));
            services.AddScoped<ICommandBuilder, CommandBuilder>();
        }
    }
}