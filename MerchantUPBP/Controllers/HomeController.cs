using System.Runtime.Intrinsics.X86;
using AdminPanel.Interface;
using BusinessObjects;
using crypto;
using Entity;
using MerchantUPBP.App_Code;
using MerchantUPBP.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MerchantUPBP.Controllers
{
    //[Auth]
    public class HomeController : Controller
    {
        private readonly IMerchant _iMerchant;
        private readonly IPaymentModeMaster _paymentModeMaster;
        private readonly IPaymentMode _paymentMode;
        public HomeController(IMerchant merchant, IPaymentModeMaster paymentModeMaster, IPaymentMode paymentMode)
        {
            _iMerchant = merchant;
            _paymentModeMaster = paymentModeMaster;
            _paymentMode = paymentMode;
        }
        public IActionResult Index()
        {
            return View();
        }

        //public ActionResult MerchantRegister()
        //{
        //    MerchantEntity merchantEntity = new MerchantEntity();
        //    merchantEntity.lstPaymentMode = _paymentModeMaster.AllPayment();
        //    return View(merchantEntity);
        //}

        //[HttpPost]
        //public IActionResult MerchantRegister(MerchantEntity merchantEntity, List<int> SelectedPaymentModes)
        //{
        //    try
        //    {
        //        merchantEntity.MerchantId = Security_.GenerateMerchantId();
        //        merchantEntity.PrivateKey = Security_.GeneratePrivateKey();
        //        merchantEntity.Password = Security_.encrypt(merchantEntity.Password);
        //        MerchantEntity merchantEntity1 = _iMerchant.MerchantRegister(merchantEntity);
        //        if (merchantEntity1.Id != 0)
        //        {
        //            if (SelectedPaymentModes != null && SelectedPaymentModes.Any())
        //            {   
        //                foreach (var modeId in SelectedPaymentModes)
        //                {
        //                    PaymentModeEntity payment = new PaymentModeEntity
        //                    {
        //                        PaymentId = modeId,
        //                        MerchantId = merchantEntity1.Id
        //                    };
        //                    _paymentMode.AddPayment(payment);
        //                }
        //            }

        //            HttpContext.Session.SetString("Id", merchantEntity1.Id.ToString());
        //            HttpContext.Session.SetString("Name", merchantEntity1.Name.ToString());
        //            return RedirectToAction("MerchantLogin");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Email or Phone already used!");
        //            return RedirectToAction("MerchantRegister");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError(string.Empty, "An error occurred while creating the merchant.");
        //        return RedirectToAction("MerchantRegister");
        //    }
        //}


        public IActionResult MerchantLogin()
        {
            return View();
        }

        [HttpPost]

        public IActionResult MerchantLogin(MerchantEntity merchantentity)
        {
            try
            {
                merchantentity.Password = Security_.encrypt(merchantentity.Password);
                MerchantEntity merchant = _iMerchant.MerchantLogin(merchantentity.Email, merchantentity.Password);
                if (merchant != null && merchant.Id != 0)
                {
                    HttpContext.Session.SetString("Id", merchant.Id.ToString());
                    HttpContext.Session.SetString("Name", merchant.Name.ToString());
                    HttpContext.Session.SetString("MerchantId", merchant.MerchantId.ToString());
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    TempData["Msg"] = "Email or Password not matched";
                    return RedirectToAction("MerchantLogin");
                }

            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Some error occurred: " + ex.Message;
                return RedirectToAction("MerchantLogin");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("MerchantLogin"); // Redirect to login page
        }
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Detals()
        {
            return View();
        }

    }

}
