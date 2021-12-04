using System.Collections.Generic;
using System.Threading.Tasks;
using Bll.Interfaces;
using Dal.Interfaces;

namespace BLL
{
    public class StatsBLO:IStatsBLO
    {
        private IStatsDAO _statsDao;
        public StatsBLO(IStatsDAO statsDao)
        {
            this._statsDao = statsDao;
        }
        public async Task<List<string[]>> GetStats()
        {
            return await _statsDao.GetStats();
            // throw new System.NotImplementedException();
        }

        public Task<Dictionary<string, string>> GetVerticalDashboard()
        {
            return _statsDao.GetVerticalDashboard();
        }
    }
}