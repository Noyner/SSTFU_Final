using System;

namespace DTO
{
    public class EGarbageStatIncident
    {
        public int Id { get; set; }
        public DateTime  DateTime { get; set; }
        public int CameraId { get; set; }
        public int DistrictId { get; set; }
    }
}