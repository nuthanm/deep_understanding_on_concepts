/*
 * Now this class,
 * 1. Changed from sync to async
 * 2. Now we move this DBContext from controller to Repository
 * 3. Code should be simple in Controller.
 */
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WalkMe.API.Models.DTO;
using WalkMe.API.Repositories;

namespace WalkMe.API.Controllers
{
    //https:localhost:<portnumber>/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController(IRegionRepository regionRepository,
        IMapper mapper) : ControllerBase
    {

        // GET all Regions
        // GET: https:localhost:<portnumber>/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            // Get Data from database - Domain
            var regions = await regionRepository.GetAllAsync();

            // Map domain data to Dto
            // Source: regions
            // Destination: List<RegionDto>
            var regionDtos = mapper.Map<List<RegionDto>>(regions);

            return Ok(regionDtos);
        }

        // GET Region details for a specific id
        // GET: https:localhost:<portnumber>/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain is null)
            {
                return NotFound();
            }

            // Map Domain Data to DTO
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }


        // POST: Create a new Region
        // POST: https:localhost:<portnumber>/api/regions
        [HttpPost]
        public async Task<IActionResult> CreateRegionAsync([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map or Convert Dto to Domain
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            // Use Domain Model to create Region
            regionDomainModel = await regionRepository.CreateRegionAsync(regionDomainModel);

            // Map Domain model to Dto
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = regionDto!.Id }, regionDto);
        }

        // Put: Update a existing region
        // Put: https:localhost:<portnumber>/api/regions/{id}
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // Map or Convert Dto to Domain
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            // Check first if Region exists
            regionDomainModel = await regionRepository.UpdateRegionAsync(id, regionDomainModel);

            if (regionDomainModel is null)
            {
                return NotFound();
            }

            // Map Domain model to Dto
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

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
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
