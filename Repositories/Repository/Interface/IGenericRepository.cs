using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(object id);
        IEnumerable<T> GetAll(Func<T, bool> where, params Expression<Func<T, object>>[] includes);
        Task<bool> Add(T item);
        Task<bool> Update(object id,T item);
        Task<bool> Delete(object id);
    }
}
