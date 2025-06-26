using FTD_Asia_API.Entities;

namespace FTD_Asia_API.Interface
{
    public interface IPartnerRepository
    {
        List<PartnerInfo> GetPartnerInfoList();
    }
}
