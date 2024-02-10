namespace WalkMe.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();

        Task<Region?> GetByIdAsync(Guid regionId);

        Task<Region> CreateRegionAsync(Region region);

        Task<Region?> UpdateRegionAsync(Guid regionId, Region region);

        Task<Region?> DeleteRegionAsync(Guid regionId);
    }
}
