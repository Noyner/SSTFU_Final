using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bll.Interfaces
{
    public interface IStatsBLO
    {
        public Task<List<string[]>> GetStats();
        public Task<Dictionary<string, string>> GetVerticalDashboard();
    }
}