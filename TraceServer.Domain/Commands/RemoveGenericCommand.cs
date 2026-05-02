using System;
using MediatR;

namespace AgileAi.Domain.Commands
{
    public class RemoveGenericCommand<TEntity> : IRequest<TEntity> where TEntity : class
    {
        public RemoveGenericCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}