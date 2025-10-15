using AdminPanel.Interface;
using Bal;
using Entity;

namespace AdminPanel.Repository
{
    public class PaymentModeMasterRepo : IPaymentModeMaster
    {
        private string _con;
        public PaymentModeMasterRepo(IConfiguration _config)
        {
            _con = _config.GetConnectionString("Upbp");
        }


        public PaymentModeMasterEntity AddPayment(PaymentModeMasterEntity merchant)
        {
            PaymentModeMasterEntity userEntity = PaymentModeMasterBal.Insert(merchant, _con);
            return userEntity;
        }

        public List<PaymentModeMasterEntity> AllPayment()
        {
            return PaymentModeMasterBal.GetAllPayment(_con);
        }

        public void Delete(int Id)
        {
            PaymentModeMasterBal.Delete(Id, _con);
        }
        public PaymentModeMasterEntity GetByPaymentId(int id)
        {
            return PaymentModeMasterBal.GetByPaymentId(id, _con);
        }






    }
}
