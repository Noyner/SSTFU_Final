using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DAO
{
    public class StatsDAO:IStatsDAO
    {
        private EFDatabaseContext _context;
        public StatsDAO(EFDatabaseContext context)
        {
            this._context = context;
        }

        public async Task<List<string[]>> GetStats()
        {
            var res = new List<string[]>();

            var incidents = await _context.Incidents
                .Include(x => x.Type)
               // .Include(x => x.DateTime)
                .Where(x=>x.Type.TypeName=="Garbage")
                .ToListAsync();
            
            var districts = await _context.Districts.ToListAsync();
            
            var camerasData = await (from camD
                    in _context.Cameras.Include(c=>c.District)
                select camD).ToListAsync();

            foreach (var camD in camerasData)
            {
                res.Add(
                    new string[]
                    {
                        camD.Address,
                        districts.First(x => x.Id == camD.District.Id).Name.ToString(),
                        incidents.Any(x=>x.CameraId==camD.Id&&x.Generation>=2) ? "Заполнен" : "Не Заполнен",
                        incidents.FirstOrDefault(x=>x.CameraId==camD.Id)?.DateTime.ToShortDateString() ?? "-",
                        incidents.FirstOrDefault(x=>x.CameraId==camD.Id)?.DateTime.ToShortTimeString() ?? "-",
                        incidents.FirstOrDefault(x=>x.CameraId==camD.Id)?.Generation.ToString() ?? "-"
                    }
                    );
            }
            /*}*/
            return res;

        }

        public async Task<Dictionary<string, string>> GetVerticalDashboard()
        {
            var res = new Dictionary<string, string>();

            var districts = await _context.Districts.ToListAsync();
            var groups =  (from si in _context.GarbageStatIncidents
                group si by si.DistrictId
                into gr
                select new {district = gr.Key, count=gr.Count()});

            await groups.ForEachAsync(x => res.Add(
                districts.First(di => x.district == di.Id).Name, x.count.ToString()
            ));
            
            return res;
        }
    }
}