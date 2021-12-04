using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Dal.Interfaces;
using DTO;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAO
{
    public class TechDao:ITechDao
    {
        private EFDatabaseContext _context;
        public TechDao(EFDatabaseContext context)
        {
            this._context = context;
        }
        public async Task PrefillDB(int districts_count)
        {
            if(_context.Cameras.Any() || _context.Districts.Any() || _context.IncidentTypes.Any())
                return;
            for (int i = 0; i < districts_count; i++)
            {
                _context.Districts.Add(new EDistrict() {Name = $"Какой-то район №{i}"});
            }
            _context.IncidentTypes.Add(new EIncidentType()
            {
                TypeName = "Garbage"
            });
            
            string jstring = File.ReadAllText("./../GarbageDetectionService/JSONs/data.txt");
            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(jstring);

            DataTable dataTable = dataSet.Tables["cctrt_cams"];

            // Console.WriteLine(dataTable.Rows.Count);
// 2
            List<Tuple<string, string>> pairs = new List<Tuple<string, string>>();
            foreach (DataRow row in dataTable.Rows)
            {
                pairs.Add(new Tuple<string, string>(row["id"] as string,row["addr"] as string));
                //Console.WriteLine(row["id"] + " - " + row["item"]);
            }
            foreach (var pair in pairs)
            {
                if(!_context.Cameras.Any(x=>x.Address==pair.Item2))
                    _context.Cameras.Add(new ECamera() {Address = pair.Item2});
            }
            await _context.SaveChangesAsync();
            var dists = _context.Districts.ToList();
            dists.Shuffle();
            foreach (var cam in _context.Cameras)
            {
                cam.District ??= dists.Last();
                dists.Shuffle();
                //dists.Remove(dists.Last());
                // cam.District = _context.Districts.First(d => d.Id == newList.First());
            }

            await _context.SaveChangesAsync();
        }
    }
}