/*
 * Traditional Controller where it contains non async methods.
 * Change this sync to async actions methods.
 */
using Microsoft.AspNetCore.Mvc;
using WalkMe.API.Data;
using WalkMe.API.Models.DTO;

namespace WalkMe.API.Controllers
{
    //https:localhost:<portnumber>/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsTraditionalController(WalkMeDbContext dbContext) : ControllerBase
    {

        // GET all Regions
        // GET: https:localhost:<portnumber>/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            // Approach 1: Without DBContext
            //var regions = new List<Region>
            //{
            //    new() {
            //        Id = Guid.NewGuid(),
            //        Name= "Region 1",
            //        Code="NASA",
            //    },
            //    new() {
            //        Id = Guid.NewGuid(),
            //        Name= "Region 2",
            //        Code="SANA",
            //    }
            //};

            // Approach 2: Get data from a table

            //var regions = dbContext.Regions;
            // SQL Query: SELECT [r].[Id], [r].[Code], [r].[Name], [r].[RegionImageUrl]
            // FROM[Regions] AS[r]
            //var regions1 = dbContext.Regions.ToList();
            // SQL Query: SELECT [r].[Id], [r].[Code], [r].[Name], [r].[RegionImageUrl]
            // FROM[Regions] AS[r]

            // Get Data from database - Domain
            var regions = dbContext.Regions;

            // Map domain data to Dto
            //var regionDto = regions.ToList();

            // Traditional way
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


            // Option 2: Combined using Where and FirstOrDefault
            //var regionWithWhere = dbContext.Regions.Where(x => x.Id == id).FirstOrDefault();

            // Option 3: 
            //var regionWithFirstOrDefault = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            // Option 4: 
            //var regionWithSingleOrDefault = dbContext.Regions.SingleOrDefault(x => x.Id == id);


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

            // SQL Query
            /*  SET IMPLICIT_TRANSACTIONS OFF;
                SET NOCOUNT ON;
                INSERT INTO[Regions] ([Id], [Code], [Name], [RegionImageUrl])
                VALUES(@p0, @p1, @p2, @p3);
            */

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

            // SQL Query
            /*   SET IMPLICIT_TRANSACTIONS OFF;
                 SET NOCOUNT ON;
                 UPDATE [Regions] SET [Code] = @p0, [Name] = @p1, [RegionImageUrl] = @p2
                 OUTPUT 1
                 WHERE [Id] = @p3;
            */

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

            // SQL Query
            /*  SET IMPLICIT_TRANSACTIONS OFF;
                SET NOCOUNT ON;
                DELETE FROM [Regions]
                OUTPUT 1
                WHERE [Id] = @p0;
            */

        }
    }
}
