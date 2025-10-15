using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class PaymentModeEntity : CommonColumns
    {
        public int Id { get; set; }

        public int MerchantId { get; set; }
        public string Title { get; set; }
        public int PaymentId { get; set; }
        public List<PaymentModeEntity> lstpayment { get; set; }
        public List<PaymentModeEntity> UpdatePaymentMode { get; set; }
    }
}
