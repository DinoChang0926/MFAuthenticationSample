using MFAuthenticationSample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ZXing.QrCode;
using ZXing.Common;
using ZXing;
using System.Drawing;
using System.Drawing.Imaging;
using SkiaSharp;
using SkiaSharp.QrCode;
using MFAuthenticationSample.Helper;
using OtpNet;

namespace MFAuthenticationSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QrCodeHelper _qrcodeHelper;
        private readonly MFAuthenticationHelper _MFAHelper;

        public HomeController(ILogger<HomeController> logger,
                              QrCodeHelper qrcodeHelper,
                              MFAuthenticationHelper MFAHelper)
        {
            _logger = logger;
            _qrcodeHelper = qrcodeHelper;
            _MFAHelper = MFAHelper;
        }

        public IActionResult Index(string Result=null)
        {
            //string sercret = Uri.EscapeDataString(Base32Encoding.ToString(KeyGeneration.GenerateRandomKey()));
            string sercret = "7FXZV4THYPNLWPJHYFRNT7D2VWHZXUF7";   //實務上需另外儲存且每個人各不相同
            ViewBag.QrCode =_qrcodeHelper.CreateQrCode(_MFAHelper.GenQRCodeUrl(sercret));
            ViewBag.Sercret = sercret;
            ViewBag.Result = Result;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        public IActionResult VaildCode(string Code,string Secret)
        {
            var Result = _MFAHelper.ValidateTotp(Code,Secret);
            if(Result.Item1) //驗證通過
            {
            }

            return RedirectToAction("Index", new { Result = Result.Item2 });
        }





    }
}