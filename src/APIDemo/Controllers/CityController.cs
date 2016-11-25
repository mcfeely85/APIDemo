using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDemo.Controllers
{
    [Route("api/cities")]
    public class CityController : Controller
    {
        [HttpGet()]
        public IActionResult GetAllCities()
        {
            //return new JsonResult(new List<object>() {
            //     new { id=1, name= "beijing" },
            //      new { id=2, name= "shanghai" }
            //});

            return Ok(new JsonResult(CityDataStore.Current.Cities));
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var cityToReturn = CityDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == id);

            if (null != cityToReturn)
            {
                return Ok(cityToReturn);
            }

            return NotFound();
        }
    }
}
