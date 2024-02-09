using Microsoft.AspNetCore.Mvc;
using WalkMe.API.Data;

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

            var regions = dbContext.Regions;
            // SQL Query: SELECT [r].[Id], [r].[Code], [r].[Name], [r].[RegionImageUrl]
            // FROM[Regions] AS[r]
            var regions1 = dbContext.Regions.ToList();
            // SQL Query: SELECT [r].[Id], [r].[Code], [r].[Name], [r].[RegionImageUrl]
            // FROM[Regions] AS[r]

            return Ok(regions);
        }

        // GET Region details for a specific id
        // GET: https:localhost:<portnumber>/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // Option 1: 
            var regionWithFind = dbContext.Regions.Find(id);


            // Option 2: Combined using Where and FirstOrDefault
            var regionWithWhere = dbContext.Regions.Where(x => x.Id == id).FirstOrDefault();

            // Option 3: 
            var regionWithFirstOrDefault = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            // Option 4: 
            var regionWithSingleOrDefault = dbContext.Regions.SingleOrDefault(x => x.Id == id);


            if (regionWithSingleOrDefault is null)
            {
                return NotFound();
            }

            return Ok(regionWithSingleOrDefault);
        }
    }
}
