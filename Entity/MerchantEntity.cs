using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class MerchantEntity
    {

        public int Id { get; set; }

         public string MerchantId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required] 
        public string PhoneNo { get; set; }

        [Required]
        public string Domain { get; set; }

        [Required]
        public decimal Commission { get; set; }

        [Required]
        public string NatureBusiness { get; set; }

        [Required]
        public decimal Tax { get; set; }

        public string PrivateKey { get; set; }

        [Required]
        public string Password { get; set; }

        public string Method { get; set; }
        public string Message { get; set; }
        public string ErrorCode { get; set; }

        public List<MerchantEntity> lstmerchant { get; set; }
        public List<PaymentModeMasterEntity> lstPaymentMode { get; set; }
        public List<PaymentModeEntity> lstPaymentModeentity { get; set; }
        public List<int> UpdatePaymentModes { get; set; } = new List<int>();
        public List<int> SelectedPaymentModes { get; set; } = new List<int>();
    }
}
