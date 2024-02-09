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
    }
}
