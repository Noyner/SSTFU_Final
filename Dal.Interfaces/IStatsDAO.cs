using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface IStatsDAO
    {
        public Task<List<string[]>> GetStats();
        public Task<Dictionary<string, string>> GetVerticalDashboard();
    }
}