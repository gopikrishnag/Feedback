using System.Linq;
using System.Threading.Tasks;

namespace Feedback.Repository.Repository
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> GetAll();

        T Get<TKey>(TKey id);

        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
    }
}
