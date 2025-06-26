using FTD_Asia_Test.Entities;

namespace FTD_Asia_Test.Interface
{
    public interface IPartnerRepository
    {
        List<PartnerInfo> GetPartnerInfoList();
    }
}
