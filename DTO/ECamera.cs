using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
   public class ECamera
    {
        public int Id { get; set; }
        public string Address { get; set; }
        
        public EDistrict District { get; set; }
    }
}
