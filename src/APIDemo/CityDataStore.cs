using APIDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDemo
{
    public class CityDataStore
    {
        public static CityDataStore Current { get; } = new CityDataStore();

        public List<CityDto> Cities { get; set; }

        public CityDataStore()
        {
            Cities = new List<CityDto>() {
                new CityDto() { Id=1, Name="beijing", Desciption="captial",
                    PointOfInterest = new List<PointOfInterestDto>() {
                        new PointOfInterestDto() {Id=1, Name="forbidden city", Description="center" },
                        new PointOfInterestDto() {Id=2, Name="great wall", Description="far" },
                        new PointOfInterestDto() {Id=3, Name="tian an men", Description="gov" }

                    } },
                new CityDto() { Id=2, Name="shanghai", Desciption="mibao" ,
                    PointOfInterest = new List<PointOfInterestDto>() {
                        new PointOfInterestDto() {Id=1, Name="xu jia hui", Description="home" },
                        new PointOfInterestDto() {Id=2, Name="lu jia zui", Description="finance" }

                    } },
                new CityDto() { Id=3, Name="wuhan", Desciption="da bao bei",
                    PointOfInterest = new List<PointOfInterestDto>() {
                        new PointOfInterestDto() {Id=1, Name="rongqiao", Description="home" },
                        new PointOfInterestDto() {Id=2, Name="huang he lou", Description="center" }

                    }
                }

            };
        }
    }
}
