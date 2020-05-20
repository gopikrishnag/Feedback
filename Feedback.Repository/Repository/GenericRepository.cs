using Feedback.Repository.DbContextes;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback.Repository.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;


        public GenericRepository(FeedbackContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }


        public virtual IQueryable<T> GetAll()
        {
             return _dbSet.AsQueryable<T>().AsNoTracking();
        }

        public T Get<TKey>(TKey id)
        {
            return _dbSet.Find(id);
        }
        public async Task<bool> Add(T entity)
        {
           _context.Set<T>().Add(entity);

           var ret = Save();
            return ret;
        }
        public async Task<bool> Update(T entity)
        {
            _context.Update(entity);
           var ret = Save();
            return ret;
        }
        private bool Save()
        {
            _context.SaveChanges();
            return true;
        }

    }
}
