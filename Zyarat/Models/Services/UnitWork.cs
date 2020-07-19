using System.Threading.Tasks;
using Zyarat.Data;

namespace Zyarat.Models.Services
{
    public class UnitWork:IUnitWork
    {
        private ApplicationContext _context;

        public UnitWork(ApplicationContext context)
        {
            _context = context;
        }

        public Task CommitAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}