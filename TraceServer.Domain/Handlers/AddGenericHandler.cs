using System.Threading;
using System.Threading.Tasks;
using AgileAi.Domain.Commands;
using AgileAi.Domain.Interfaces;
using MediatR;

namespace AgileAi.Domain.Handlers
{
    public class AddGenericHandler<TEntity> : IRequestHandler<AddGenericCommand<TEntity>, TEntity> where TEntity : class
    {
        private readonly IGenericRepository<TEntity> Repository;

        public AddGenericHandler(IGenericRepository<TEntity> Repository)
        {
            this.Repository = Repository;
        }

        public async Task<TEntity> Handle(AddGenericCommand<TEntity> request, CancellationToken cancellationToken)
        {
            var result = Repository.Add(request.Entity);
            return await Task.FromResult(result);
        }
    }
}