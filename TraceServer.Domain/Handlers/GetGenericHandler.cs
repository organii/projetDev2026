using System.Threading;
using System.Threading.Tasks;
using AgileAi.Domain.Commands;
using AgileAi.Domain.Interfaces;
using MediatR;
using AgileAi.Domain.Queries;

namespace AgileAi.Domain.Handlers
{
    public class GetGenericHandler<TEntity> : IRequestHandler<GetGenericQuery<TEntity>, TEntity> where TEntity : class
    {
        private readonly IGenericRepository<TEntity> Repository;
        public GetGenericHandler(IGenericRepository<TEntity> Repository)
        {
            this.Repository = Repository;
        }

        public async Task<TEntity> Handle(GetGenericQuery<TEntity> request, CancellationToken cancellationToken)
        {
            var result = Repository.Get(request.Condition, request.Includes);
            return await Task.FromResult(result);
        }
    }
}