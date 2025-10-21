using PU.Services.Interfaces;
using PU.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PU.Services.Implementations
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<T>> GetAllAsync() => _repository.GetAllAsync();
        public Task<T?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<T> AddAsync(T entity) => _repository.AddAsync(entity);
        public Task UpdateAsync(T entity) => _repository.UpdateAsync(entity);
        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}