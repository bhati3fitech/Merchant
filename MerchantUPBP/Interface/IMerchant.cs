using Entity;

namespace MerchantUPBP.Interface
{
    public interface IMerchant
    {
        public MerchantEntity MerchantRegister(MerchantEntity merchantentity);
        public MerchantEntity UpdateMerchant(MerchantEntity merchantEntity);
        public MerchantEntity GetByMerchantId(int Id);
        public MerchantEntity MerchantLogin(string Email, string Password);
    }
}
