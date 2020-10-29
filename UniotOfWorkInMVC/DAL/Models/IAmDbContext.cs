using System.Data.Entity;
using System.Threading.Tasks;

namespace DAL.Models
{
    public interface IAmDbContext
    {
        Database Database { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();

    }
}