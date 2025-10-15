using AdminPanel.Interface;
using crypto;
using Entity;
using MerchantUPBP.App_Code;
using MerchantUPBP.Interface;
using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;

namespace MerchantUPBP.Controllers
{
    public class UserController : Controller
    {
        private readonly IMerchant _iMerchant;
        private readonly IPaymentModeMaster _paymentModeMaster;
        private readonly IPaymentMode _paymentMode;
        private readonly IMerchantPayment _merchantPayment;

        public UserController(IMerchant merchant, IPaymentModeMaster paymentModeMaster, IPaymentMode paymentMode, IMerchantPayment merchantPayment)
        {
            _iMerchant = merchant;
            _paymentModeMaster = paymentModeMaster;
            _paymentMode = paymentMode;
            _merchantPayment = merchantPayment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetByMerchantId()
        {
            string id = HttpContext.Session.GetString("Id");

            if (!string.IsNullOrEmpty(id))
            {
                MerchantEntity merchant = _iMerchant.GetByMerchantId(Convert.ToInt32(id));
                merchant.lstPaymentModeentity = _paymentMode.GetByMerchantId(Convert.ToInt32(id));
                return View(merchant);
            }
            else
            {
                return RedirectToAction("MerchantLogin", "Home");
            }
        }

        public IActionResult MerchantEdit(int id)
        {
            if (id != null)
            {
                MerchantEntity merchant = _iMerchant.GetByMerchantId(id);
                merchant.lstPaymentMode = _paymentModeMaster.AllPayment();
                merchant.lstPaymentModeentity = _paymentMode.GetByMerchantId(id);
                merchant.SelectedPaymentModes = merchant.lstPaymentModeentity?.Select(p => p.PaymentId).ToList();
                return View(merchant);
            }
            else
            {
                return RedirectToAction("GetByMerchantId");
            }

        }

        [HttpPost]
        public IActionResult MerchantEdit(MerchantEntity employee)
        {

            try
            {
                _iMerchant.UpdateMerchant(employee);
                _paymentMode.DeletePaymentsByMerchantId(employee.Id);

                if (employee.SelectedPaymentModes != null && employee.SelectedPaymentModes.Any())
                {
                    foreach (var modeId in employee.SelectedPaymentModes)
                    {
                        _paymentMode.AddPayment(new PaymentModeEntity
                        {
                            PaymentId = modeId,
                            MerchantId = employee.Id
                        });
                    }
                }

                TempData["SuccessMessage"] = "Merchant and Payment Mode have been updated successfully!";
                return RedirectToAction("GetByMerchantId");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred. Please try again.";
                employee.lstPaymentModeentity = _paymentMode.AllPayment();
                return RedirectToAction("MerchantEdit", new { Id = employee.Id });
            }

        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Clear();
            TempData.Clear();
            return RedirectToAction("MerchantLogin", "Home");

        }

        public ActionResult PaymentById(long id)
        {
            MerchantPaymentEntity model = _merchantPayment.PaymentById(id);

            if (model != null && !string.IsNullOrEmpty(model.MerchantId))
            {
                if (int.TryParse(model.MerchantId, out int merchantId))
                {
                    model.lstPaymentModeentity = _paymentMode.GetByMerchantId(merchantId);
                }

            }
            return View(model);
        }

        public IActionResult PaymentByMerchantId()
        {
            try
            {
                var merchantId = HttpContext.Session.GetString("MerchantId");
                var payments = _merchantPayment.PaymentByMerchantId(merchantId);

                if (payments == null || payments.Count == 0)
                {
                    TempData["ErrorMessage"] = "Payment details not found for the merchant.";           
                }

                return View(payments);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred. Please try again. Error: {ex.Message}";
                return RedirectToAction("Index");
            }
        }


    }
}
