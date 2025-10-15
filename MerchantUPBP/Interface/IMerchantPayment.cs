using Entity;
namespace AdminPanel.Interface
{
    public interface IMerchantPayment
    {
     
         public  List<MerchantPaymentEntity> PaymentByMerchantId(string merchantId);
        public MerchantPaymentEntity PaymentById(long id);
    }
}
