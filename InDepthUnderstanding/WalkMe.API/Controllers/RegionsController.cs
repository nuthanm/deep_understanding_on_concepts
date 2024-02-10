/*
 * Now this class,
 * 1. Changed from sync to async
 * 2. Now we move this DBContext from controller to Repository
 * 3. Code should be simple in Controller.
 */
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetAllAsync()
        {
            // Get Data from database - Domain
            var regions = await dbContext.Regions.ToListAsync();

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
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            // Option 1: 
            var regionDomain = await dbContext.Regions.FindAsync(id);

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
        public async Task<IActionResult> CreateRegionAsync([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map or Convert Dto to Domain
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };


            // Use Domain Model to create Region
            await dbContext.Regions.AddAsync(regionDomainModel);
            await dbContext.SaveChangesAsync();

            // Map Domain model to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetByIdAsync), new { id = regionDto.Id }, regionDto);
        }

        // Put: Update a existing region
        // Put: https:localhost:<portnumber>/api/regions/{id}
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto addRegionRequestDto)
        {
            // Check first if Region exists
            var isExists = await dbContext.Regions.AnyAsync(x => x.Id == id);
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
            await dbContext.SaveChangesAsync();

            // Map Domain model to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetByIdAsync), new { id = regionDto.Id }, regionDto);
        }

        // Put: Update a existing region
        // Put: https:localhost:<portnumber>/api/regions/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync([FromRoute] Guid id)
        {
            // Check first if Region exists
            var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region is null)
            {
                return NotFound();
            }

            // Delete the region
            dbContext.Regions.Remove(region);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
