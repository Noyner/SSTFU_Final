using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dal.Interfaces;
using DAO.Storage;
using DTO;
using GarbageDetectionService;
using Microsoft.EntityFrameworkCore;

namespace DAO.DB
{
    public class DetectorConveyor:IDetectorConveyor
    {
        private IGarbageDetector _detector;
        private EFDatabaseContext _context; 
        public DetectorConveyor(IGarbageDetector detector,EFDatabaseContext context)
        {
            this._detector = detector;
            this._context = context;
        }
        public async Task HandleQueue()
        {
            OperationQueue.FillQueue();

            var districtsList =await _context.Districts
                .Include(x => x.Cameras)
                .ToListAsync();
            while (!OperationQueue.QueueEmpty())
            {
                var frame=OperationQueue.DequeueFrame();
                var res = _detector.Check(frame);
                
                EIncident incident = new EIncident()
                {
                    Type = _context.IncidentTypes.First(x => x.TypeName == "Garbage"),
                    DateTime = DateTime.Now,
                    Generation = 1
                };  
                var fid = frame.Name.Split("#").Last().Split(".").First();
                incident.CameraId = (await _context.Cameras.FirstOrDefaultAsync(x => x.Id.ToString() == fid)).Id;
                //generation calculations
                var parent = await (from inc
                        in _context.Incidents
                    where inc.CameraId == incident.CameraId
                    select inc).FirstOrDefaultAsync();
                
                if (res&&parent != null)
                {
                    parent.Generation++;
                    parent.DateTime=DateTime.Now;
                    if (parent.Generation == 2)
                    {
                        var statIncident = new EGarbageStatIncident()
                        {
                            CameraId = parent.CameraId,
                            DateTime = DateTime.Now,
                            DistrictId = districtsList
                                .First(x => x.Cameras
                                    .Any(x => x.Id == parent.CameraId)).Id
                        };
                        await _context.GarbageStatIncidents.AddAsync(statIncident);
                    }
                }
                else if(parent==null)
                   await _context.Incidents.AddAsync(incident);

                if (!res&&parent!=null)
                { 
                    _context.Incidents.Remove(parent);
                }
            }

            await _context.SaveChangesAsync();
        }
        
        
    }
}