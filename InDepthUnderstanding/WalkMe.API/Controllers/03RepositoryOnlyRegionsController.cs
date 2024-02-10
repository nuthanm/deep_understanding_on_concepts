/*
 * Now this class,
 * 1. Changed from sync to async
 * 2. Now we move this DBContext from controller to Repository
 * 3. Code should be simple in Controller.
 */
using Microsoft.AspNetCore.Mvc;
using WalkMe.API.Models.DTO;
using WalkMe.API.Repositories;

namespace WalkMe.API.Controllers
{
    //https:localhost:<portnumber>/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoryOnlyRegionsController(IRegionRepository regionRepository) : ControllerBase
    {

        // GET all Regions
        // GET: https:localhost:<portnumber>/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            // Get Data from database - Domain
            var regions = await regionRepository.GetAllAsync();

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
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain is null)
            {
                return NotFound();
            }

            // Map Domain Data to DTO
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
            regionDomainModel = await regionRepository.CreateRegionAsync(regionDomainModel);

            // Map Domain model to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            var data = RouteData.Values;

            return CreatedAtAction(nameof(GetByIdAsync), new { id = regionDto!.Id }, regionDto);
        }

        // Put: Update a existing region
        // Put: https:localhost:<portnumber>/api/regions/{id}
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

            // Map or Convert Dto to Domain
            var regionDomainModel = new Region
            {
                Id = id,
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            };

            // Check first if Region exists
            regionDomainModel = await regionRepository.UpdateRegionAsync(id, regionDomainModel);

            if (regionDomainModel is null)
            {
                return NotFound();
            }

            // Map Domain model to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            var data = RouteData.Values;

            return CreatedAtAction(nameof(GetByIdAsync), new { id = regionDto.Id }, regionDto);
        }

        // Put: Update a existing region
        // Put: https:localhost:<portnumber>/api/regions/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync([FromRoute] Guid id)
        {
            // Check first if Region exists
            var regionDomainModel = await regionRepository.DeleteRegionAsync(id);
            if (regionDomainModel is null)
            {
                return NotFound();
            }

            // Map Domain model to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }
    }
}
