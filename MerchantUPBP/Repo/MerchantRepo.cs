using BAL;
using Entity;
using MerchantUPBP.Interface;

namespace MerchantUPBP.Repo
{
    public class MerchantRepo : IMerchant
    {
        private  string _con;
        public MerchantRepo(IConfiguration _config)
        {
            _con = _config.GetConnectionString("Upbp");
        }

        public MerchantEntity MerchantRegister(MerchantEntity merchantentity)
        {
            MerchantEntity merchant = MerchantBAL.MerchantRegister(merchantentity, _con);
            return merchant;
        }
        public MerchantEntity UpdateMerchant(MerchantEntity merchantEntity)
        {
            return MerchantBAL.UpdateMerchant(merchantEntity, _con);
        }
        public MerchantEntity GetByMerchantId(int id)
        {
            return MerchantBAL.GetByMerchantId(id, _con);
        }

        public MerchantEntity MerchantLogin(string Email, string Password)
        {
            MerchantEntity merchantentity1 = MerchantBAL.MerchantLogin(Email, Password, _con);
            return merchantentity1;

        }
    }
}
