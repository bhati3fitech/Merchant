using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class PaymentModeMasterEntity : CommonColumns
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        public List<PaymentModeMasterEntity> lstpaymentmodemaster { get; set; }
    }
}
