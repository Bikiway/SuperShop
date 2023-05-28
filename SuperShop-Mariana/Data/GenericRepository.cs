using Microsoft.EntityFrameworkCore;
using SuperShop_Mariana.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop_Mariana.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntenty
    {
        //Vai implementar dois interfaces.
        private readonly DataContext _context;
        public GenericRepository(DataContext context) 
        {
            _context = context; //Nosso construtor
        }
        Task IGenericRepository<T>.CreateAsync(T entity)
        {
            throw new System.NotImplementedException();
        }

       public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking(); 
            //Vai à tabela T e vai buscar os tudo o que tiver que ir buscar e desliga da tabela.
            //Não faz o track das entidades.
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id); //Genérico do ID.
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await SaveAllAsync();
        }

        private async Task<bool> SaveAllAsync()
        {
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await SaveAllAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await SaveAllAsync();
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Set<T>().AnyAsync(e => e.Id == id);
        }
    }
}
