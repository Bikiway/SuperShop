using System.Linq;
using System.Threading.Tasks;

namespace SuperShop_Mariana.Data
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll(); //Método que devolve todas as entidades que usarmos o T.

        Task<T> GetByIdAsync(int id); //Unica coisa em comum. 

        Task CreateAsync(T entity); //entidade que se vai chamar T

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<bool> ExistAsync(int id);


    }
}
