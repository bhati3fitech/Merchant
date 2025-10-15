using Entity;
using System.Collections.Generic;

namespace AdminPanel.Interface
{
    public interface IPaymentModeMaster
    {
        PaymentModeMasterEntity AddPayment(PaymentModeMasterEntity merchant);
        List<PaymentModeMasterEntity> AllPayment();
        void Delete(int Id);
        public PaymentModeMasterEntity GetByPaymentId(int Id);
    }
}
