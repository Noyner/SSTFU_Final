using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DAO;
using DTO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Technical
{
    public class TDAO
    {
        private EFDatabaseContext _context;
        public TDAO(EFDatabaseContext context)
        {
            this._context = context;
        }

        //public async 
        public async void GenerateExampleDistricts(int count)
        {
            
            for (int i = 0; i < count; i++)
            {
                _context.Districts.Add(new EDistrict() {Name = $"Какой-то район №{i}"});
            }
            await _context.SaveChangesAsync();
        }

        public async void PrefillIncidentTypes()
        {
            _context.IncidentTypes.Add(new EIncidentType()
            {
                TypeName = "Garbage"
            });
            await _context.SaveChangesAsync();
        }
        
        public async void PrefillCameras(List<Tuple<string,string>> source)
        {
            
            foreach (var pair in source)
            {
                if(!_context.Cameras.Any(x=>x.Address==pair.Item2))
                    _context.Cameras.Add(new ECamera() {Address = pair.Item2});
            }

            await _context.SaveChangesAsync();
        }
        
        public async void AssignCamerasToDistrictsRandom()
        {
           
           //var dists = _context.Districts.OrderBy(x=>r.Next()).ToList();
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

        public bool DbEmpty()
        {
            return !(_context.Cameras.Any() || _context.Districts.Any() || _context.IncidentTypes.Any());
        }
        
        public List<Tuple<string,string>> ParseAddresses()
        {
            string jstring = File.ReadAllText("A:\\Final 2021\\SSTFU LOD 2021\\GarbageDetectionService\\JSONs\\data.txt");
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

            return pairs;
        }
    }
}