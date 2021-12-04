using System.Collections.Generic;

namespace DTO
{
    public class EDistrict
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ECamera> Cameras { get; set; } 
    }
}