using Microsoft.EntityFrameworkCore.Update.Internal;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;

namespace NZwalks.API.Repositories
{
    public interface IRegionRepository
    {

        Task<List<Region>> GetAllAsync();
        Task<Region?> GetbyIdAsync(Guid id);
        Task<Region> CreateAsync(Region region);
        Task<Region?> Update(Guid id, Region region);
        Task<Region?> DeleteAsync(Guid id); 
        
    }
}
