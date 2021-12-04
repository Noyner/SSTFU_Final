using System;
using System.Threading.Tasks;
using Bll.Interfaces;
using Dal.Interfaces;

namespace BLL
{
    public class BLO:ITechBLO
    {
        private ITechDao _techDao;
        private IDetectorConveyor _conveyor;
        public BLO(ITechDao techDao,IDetectorConveyor detectorConveyor)
        {
            this._techDao = techDao;
            this._conveyor = detectorConveyor;
        }
        public async Task PrefillDB(int districts_count)
        {
           await _techDao.PrefillDB(districts_count);
           // throw new NotImplementedException();
        }

        public async Task HandleQueue()
        {
           await _conveyor.HandleQueue();
        }
    }
}
