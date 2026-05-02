using System.Threading;
using System.Threading.Tasks;
using AgileAi.Domain.Commands;
using AgileAi.Domain.Interfaces;
using MediatR;

namespace AgileAi.Domain.Handlers
{
    public class PutGenericHandler<TEntity> : IRequestHandler<PutGenericCommand<TEntity>, TEntity> where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _repository;

        public PutGenericHandler(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<TEntity> Handle(PutGenericCommand<TEntity> request, CancellationToken cancellationToken)
        {
            // Assuming your repository has an Update method
            var result = _repository.Put(request.Entity);
            return await Task.FromResult(result);
        }
    }
}