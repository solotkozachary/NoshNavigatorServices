﻿using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.Ingredient;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.InstructionStep;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.Recipe;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Services;
using zs.nn.NoshNavigatorServices.Events;
using zs.nn.NoshNavigatorServices.Infrastructure.Services;
using zs.nn.NoshNavigatorServices.Persistence.MsSql;
using zs.nn.NoshNavigatorServices.Persistence.MsSql.Services.Ingredient;
using zs.nn.NoshNavigatorServices.Persistence.MsSql.Services.InstructionStep;
using zs.nn.NoshNavigatorServices.Persistence.MsSql.Services.Recipe;

namespace zs.nn.NoshNavigatorServices.Infrastructure
{
    /// <summary>
    /// Registers dependent services for the application.
    /// </summary>
    public static class ApplicationServiceRegistrar
    {
        /// <summary>
        /// Register services in the service pipeline.
        /// </summary>
        /// <param name="services">The service pipeline.</param>
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            // MediatR with FluentValidation
            var domainAssembly = AppDomain.CurrentDomain.GetAssemblies();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(domainAssembly));
            services.AddFluentValidation(domainAssembly);

            // Application utilities
            services.AddScoped<IEntityIdGenerator, EntityIdGenerator>();
            services.AddScoped(typeof(IRequestHandler<,>), typeof(Application.Entity.Handlers.InitializeEntityHandler<,>));

            // Events
            services.AddScoped(typeof(INoshNavigatorEventService<,>), typeof(NoshNavigatorEventService<,>));

            // Persistence
            services.AddDbContext<NoshNavigatorServicesDbContext>();
            services.AddScoped<IIngredientPersistenceCommands, IngredientPersistenceCommands>();
            services.AddScoped<IIngredientPersistenceQueries, IngredientPersistenceQueries>();
            services.AddScoped<IInstructionStepPersistenceCommands, InstructionStepPersistenceCommands>();
            services.AddScoped<IInstructionStepPersistenceQueries, InstructionStepPersistenceQueries>();
            services.AddScoped<IRecipePersistenceCommands, RecipePersistenceCommands>();
            services.AddScoped<IRecipePersistenceQueries, RecipePersistenceQueries>();

            return services;
        }
    }
}
