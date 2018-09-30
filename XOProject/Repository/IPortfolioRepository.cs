using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XOProject
{
    public interface IPortfolioRepository : IGenericRepository<Portfolio>
    {
        Task<List<Portfolio>> GetAll();
    }
}