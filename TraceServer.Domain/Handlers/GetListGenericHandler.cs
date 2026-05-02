using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AgileAi.Domain.Commands;
using AgileAi.Domain.Interfaces;
using AgileAi.Domain.Queries;

namespace AgileAi.Domain.Handlers
{
    public class GetListGenericHandler<TEntity> : IRequestHandler<GetListGenericQuery<TEntity>, IEnumerable<TEntity>> where TEntity : class
    {
        private readonly IGenericRepository<TEntity> Repository;
        public GetListGenericHandler(IGenericRepository<TEntity> Repository)
        {
            this.Repository = Repository;
        }

        public async Task<IEnumerable<TEntity>> Handle(GetListGenericQuery<TEntity> request, CancellationToken cancellationToken)
        {

            var result = Repository.GetList(request.Condition, request.Includes);
            return await Task.FromResult(result);
        }
    }
}