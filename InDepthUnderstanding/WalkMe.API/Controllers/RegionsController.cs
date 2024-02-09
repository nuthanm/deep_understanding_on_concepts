using Microsoft.AspNetCore.Mvc;
using WalkMe.API.Data;
using WalkMe.API.Models.DTO;

namespace WalkMe.API.Controllers
{
    //https:localhost:<portnumber>/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly WalkMeDbContext dbContext;

        public RegionsController(WalkMeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET all Regions
        // GET: https:localhost:<portnumber>/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get Data from database - Domain
            var regions = dbContext.Regions;

            // Map domain data to Dto
            var regionDto = new List<RegionDto>();
            foreach (var regionDomain in regions)
            {
                regionDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageUrl = regionDomain?.RegionImageUrl,
                });
            }

            return Ok(regionDto);
        }

        // GET Region details for a specific id
        // GET: https:localhost:<portnumber>/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // Option 1: 
            var regionDomain = dbContext.Regions.Find(id);

            if (regionDomain is null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDto);
        }


        // POST: Create a new Region
        // POST: https:localhost:<portnumber>/api/regions
        [HttpPost]
        public IActionResult CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map or Convert Dto to Domain
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };


            // Use Domain Model to create Region
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            // Map Domain model to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { Id = regionDto.Id }, regionDto);
        }

        // Put: Update a existing region
        // Put: https:localhost:<portnumber>/api/regions/{id}
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateRegion([FromRoute] Guid id, [FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Check first if Region exists
            var isExists = dbContext.Regions.Any(x => x.Id == id);
            if (!isExists)
            {
                return NotFound();
            }

            // Map or Convert Dto to Domain
            var regionDomainModel = new Region
            {
                Id = id,
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };


            // Use Domain Model to create Region
            dbContext.Regions.Update(regionDomainModel);
            dbContext.SaveChanges();

            // Map Domain model to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { Id = regionDto.Id }, regionDto);
        }

        // Put: Update a existing region
        // Put: https:localhost:<portnumber>/api/regions/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteRegion([FromRoute] Guid id)
        {
            // Check first if Region exists
            var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (region is null)
            {
                return NotFound();
            }

            // Delete the region
            dbContext.Regions.Remove(region);
            dbContext.SaveChanges();

            return NoContent();
        }
    }
}
