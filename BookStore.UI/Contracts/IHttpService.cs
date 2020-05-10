using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.UI.Contracts
{
    public interface IHttpService<T> where T : class
    {
        Task<IList<T>> Get(string url);
        Task<T> Get(string url, int id);
        Task<bool> Create(string url, T obj);
        Task<bool> Update(string url, T obj);
        Task<bool> Delete(string url, int id);
    }
}
