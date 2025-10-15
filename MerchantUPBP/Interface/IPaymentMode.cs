using Entity;
using System.Collections.Generic;

namespace AdminPanel.Interface
{
    public interface IPaymentMode
    {
       public void AddPayment(PaymentModeEntity merchant);
        List<PaymentModeEntity> AllPayment();
        public void UpdatePayment(PaymentModeEntity merchantEntity);
        public PaymentModeEntity GetByIdPayment(int Id);
        public List<PaymentModeEntity> GetByMerchantId(int Id);
        public void DeletePaymentsByMerchantId(int merchantId);
    }
}
