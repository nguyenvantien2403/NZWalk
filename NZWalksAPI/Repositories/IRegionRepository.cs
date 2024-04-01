using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    //repository se dung de cho controller truy cap qua, va lay du lieu o database, controller se khong lay du lieu truc tiep
    //toi database thong qua lop dbcontext nua
    //repository chiu trach nhiem giao tiep voi database de lay du lieu
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetByIdAsync(Guid id);
        Task<Region> CreateAsync(Region region);
        Task<Region?> UpdateAsync(Guid id,Region region);
        Task<Region?> DeleteAsync(Guid id);


    }

}
