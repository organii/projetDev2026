using System.Threading;
using System.Threading.Tasks;
using AgileAi.Domain.Commands;
using AgileAi.Domain.Interfaces;
using MediatR;

namespace AgileAi.Domain.Handlers
{
    public class RemoveGenericHandler<TEntity> : IRequestHandler<RemoveGenericCommand<TEntity>, TEntity> where TEntity : class
    {
        private readonly IGenericRepository<TEntity> Repository;
        public RemoveGenericHandler(IGenericRepository<TEntity> Repository)
        {
            this.Repository = Repository;
        }
        public async Task<TEntity> Handle(Commands.RemoveGenericCommand<TEntity> request, CancellationToken cancellationToken)
        {
            var result = Repository.Remove(request.Id);
            return await Task.FromResult(result);
        }
    }
}