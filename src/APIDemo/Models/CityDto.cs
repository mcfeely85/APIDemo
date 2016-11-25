using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDemo.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }

        public int numOfInterests { get {

                return PointOfInterest.Count; }
        }


        public ICollection<PointOfInterestDto> PointOfInterest { get; set; }

    }
}
