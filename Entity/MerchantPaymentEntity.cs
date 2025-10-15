using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public class MerchantPaymentEntity
    {
        public long Id { get; set; }
        public string MerchantId { get; set; }
        public string Domain { get; set; }
        public decimal? Amount { get; set; }
        public string OrderId { get; set; }
        public string Currency { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public List<MerchantPaymentEntity> AllMerchantPaymentList { get; set; }
        public List<MerchantEntity> lstmerchant { get; set; }
        public List<PaymentModeMasterEntity> lstPaymentMode { get; set; }
        public List<PaymentModeEntity> lstPaymentModeentity { get; set; }
        public List<int> UpdatePaymentModes { get; set; } = new List<int>();
        public List<int> SelectedPaymentModes { get; set; } = new List<int>();
    }
}
