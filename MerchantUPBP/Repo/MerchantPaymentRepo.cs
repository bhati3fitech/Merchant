using AdminPanel.Interface;
using Bal;
using BAL;
using Entity;

namespace AdminPanel.Repository
{
    public class MerchantPaymentRepo : IMerchantPayment
    {
        private string con;
        public MerchantPaymentRepo(IConfiguration _con)
        {
            con = _con.GetConnectionString("upbp");
        }

        public MerchantPaymentEntity PaymentById(long id)
        {
            return MerchanAllPaymentBal.PaymentById(id, con);
        }

        public List<MerchantPaymentEntity> PaymentByMerchantId(string merchantId)
        {
            return MerchanAllPaymentBal.PaymentByMerchantId(merchantId, con);
        }
    }
}
