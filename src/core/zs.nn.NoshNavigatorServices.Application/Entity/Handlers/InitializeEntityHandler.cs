using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using zs.nn.NoshNavigatorServices.Application.Entity.Commands;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Services;
using zs.nn.NoshNavigatorServices.Application.Enumerations;

namespace zs.nn.NoshNavigatorServices.Application.Entity.Handlers
{
    /// <summary>
    /// The handler responsible for initializing a new entity.
    /// </summary>
    public class InitializeEntityHandler<U, T> : IRequestHandler<InitializeEntityCommand<T>, T>
        where U : IRequest<T>
        where T : Domain.Entity.NoshNavigatorEntity
    {
        private readonly ILogger<InitializeEntityHandler<U, T>> _logger;
        private readonly IEntityIdGenerator _keyGenerator;

        public InitializeEntityHandler(
            ILogger<InitializeEntityHandler<U, T>> logger,
            IEntityIdGenerator keyGenerator
            )
        {
            _logger = logger;
            _keyGenerator = keyGenerator;
        }

        public async Task<T> Handle(InitializeEntityCommand<T> request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter InitializeEntityHandler - EntityType:{EntityType}", typeof(T));

            T result = default;

            try
            {
                var entity = (Domain.Entity.NoshNavigatorEntity)Activator.CreateInstance(typeof(T));

                var creationSourceId = !string.IsNullOrWhiteSpace(request.EntityCreationSourceId) ? request.EntityCreationSourceId : nameof(InitializeEntityHandler<U, T>);

                entity.Id = await _keyGenerator.GenerateId(cancellationToken);
                entity.IsActive = true;
                entity.CreatedOn = DateTime.Now;
                entity.CreatedBy = creationSourceId;
                entity.UpdatedOn = DateTime.Now;
                entity.UpdatedBy = creationSourceId;

                result = (T)entity;
            }
            catch (Exception ex)
            {
                var errMsg = "Entity initialization for entity with type {EntityType} failed.";
                _logger.LogError(ex, errMsg, typeof(T));

                _logger.LogTrace("Exit InitializeEntityHandler - EntityType:{EntityType}", typeof(T));

                throw new Exceptions.NoshNavigatorException(ApplicationConcern.Entity, ApplicationRules.MustInitialize, errMsg.Replace("{EntityType}", typeof(T).ToString()));
            }

            _logger.LogInformation("A new Entity of type {EntityType} and with EntityKey {EntityId} has been successfully created.",
                typeof(T), result.Id);

            _logger.LogTrace("Exit InitializeEntityHandler - EntityKey:{EntityId}   EntityType:{EntityType}", result.Id, typeof(T));

            return result;
        }
    }
}
