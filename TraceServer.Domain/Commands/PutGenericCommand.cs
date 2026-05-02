using MediatR;
using System;
namespace AgileAi.Domain.Commands
{
    

    public class PutGenericCommand<TEntity> : IRequest<TEntity> where TEntity : class
    {
        public Guid Id { get; }
        public TEntity Entity { get; }

        public PutGenericCommand(Guid id, TEntity entity)
        {
            Id = id;
            Entity = entity;
        }
    }
}