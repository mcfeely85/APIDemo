using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIDemo.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using APIDemo.Services;

namespace APIDemo.Controllers
{
    [Route("api/cities")]
    public class PointOfInterestController : Controller
    {
        private ILogger<PointOfInterestController> _logger;
        private IMailService _mailService;

        public PointOfInterestController(ILogger<PointOfInterestController> logger, 
            IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        [HttpGet("{cityId}/pointOfInterests")]
        public IActionResult GetPointOfInterests(int cityId)
        {
            var city = CityDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointOfInterest);
        }

        [HttpGet("{cityId}/pointOfInterest/{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            var city = CityDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointOfInterest
                .FirstOrDefault(p => p.Id == id);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(pointOfInterest);
        }

        [HttpPost("{cityId}/pointofinterest")]
        public IActionResult CreatePointOfInterest(int cityId,
            [FromBody] PointOfInterestForCreationDto pointOfInterestForCreation)
        {
            if (pointOfInterestForCreation == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CityDataStore.Current.Cities
                           .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var maxPOIId = CityDataStore.Current.Cities.SelectMany(
                c => c.PointOfInterest).Max(p => p.Id)+1;

            var finalPOI = new PointOfInterestDto()
            {
                Id = maxPOIId,
                Name = pointOfInterestForCreation.Name,
                Description = pointOfInterestForCreation.Description
            };

            city.PointOfInterest.Add(finalPOI);

            return CreatedAtRoute("GetPointOfInterest", new
            { cityId = cityId, id = finalPOI.Id }, finalPOI);

        }

        [HttpPut("{cityId}/pointOfinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id,
            [FromBody] PointOfInterestForUpdateDto pointOfInterestForUpdate)
        {
            if (pointOfInterestForUpdate == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CityDataStore.Current.Cities
                           .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var poi = city.PointOfInterest.FirstOrDefault(p => p.Id == id);

            if (poi == null)
            {
                return NotFound();
            }

            poi.Name = pointOfInterestForUpdate.Name;
            poi.Description = pointOfInterestForUpdate.Description;

            return NoContent();

        }

        [HttpPatch("{cityId}/pointOfinterest/{id}")]
        public IActionResult PatchPointOfInterest(int cityId, int id,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var city = CityDataStore.Current.Cities
                           .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var poi = city.PointOfInterest.FirstOrDefault(p => p.Id == id);

            if (poi == null)
            {
                return NotFound();
            }

            var poiToPatch = new PointOfInterestForUpdateDto()
            {
                Name = poi.Name,
                Description = poi.Description
            };

            patchDoc.ApplyTo(poiToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TryValidateModel(poiToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            poi.Name = poiToPatch.Name;
            poi.Description = poiToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{cityId}/pointOfinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            var city = CityDataStore.Current.Cities
                           .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var poi = city.PointOfInterest.FirstOrDefault(p => p.Id == id);

            if (poi == null)
            {
                return NotFound();
            }

            city.PointOfInterest.Remove(poi);

            _mailService.SendMail(poi.Name, poi.Description);

            return NoContent();
        }



    }
}
