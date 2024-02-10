using Microsoft.EntityFrameworkCore;
using WalkMe.API.Data;

namespace WalkMe.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly WalkMeDbContext dbContext;

        public RegionRepository(WalkMeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Region> CreateRegionAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteRegionAsync(Guid regionId)
        {
            var regionData = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == regionId);
            if (regionData is null)
            {
                return null;
            }

            dbContext.Regions.Remove(regionData);
            await dbContext.SaveChangesAsync();
            return regionData;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid regionId)
        {
            return await dbContext.Regions.FindAsync(regionId);
        }

        public async Task<Region?> UpdateRegionAsync(Guid regionId, Region region)
        {
            var regionData = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == regionId);
            if (regionData is null)
            {
                return null;
            }

            regionData.Name = region.Name;
            regionData.Code = region.Code;
            regionData.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return regionData;
        }
    }
}
