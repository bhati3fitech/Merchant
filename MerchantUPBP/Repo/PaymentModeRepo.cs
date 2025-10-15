using AdminPanel.Interface;
using Bal;
using Entity;

namespace AdminPanel.Repository
{
    public class PaymentModeRepo : IPaymentMode
    {
        private string _con;
        public PaymentModeRepo(IConfiguration _config)
        {
            _con = _config.GetConnectionString("Upbp");
        }


        public void AddPayment(PaymentModeEntity merchant)
        {
            PaymentModeBal.Insert(merchant, _con);
        }

        public List<PaymentModeEntity> AllPayment()
        {
            return PaymentModeBal.GetAllPayment(_con);
        }

        public void UpdatePayment(PaymentModeEntity merchantEntity)
        {
            PaymentModeBal.UpdatePayment(merchantEntity, _con);
        }


        public PaymentModeEntity GetByIdPayment(int Id)
        {
            return PaymentModeBal.GetByIdPayment(Id, _con);
        }

        public List<PaymentModeEntity> GetByMerchantId(int Id)
        {
            return PaymentModeBal.GetByMerchantId(Id, _con);
        }

        public void DeletePaymentsByMerchantId(int merchantId)
        {
            PaymentModeBal.DeletePaymentsByMerchantId(merchantId, _con);
        }
    }
}
