using System;

namespace DTO
{
    public class EIncident
    {
        public int Id { get; set; }
        public EIncidentType Type { get; set; }
        public DateTime  DateTime { get; set; }
        public int CameraId { get; set; }
        public int Generation { get; set; }
        
    }
}