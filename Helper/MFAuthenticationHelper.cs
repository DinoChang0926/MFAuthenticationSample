using OtpNet;

namespace MFAuthenticationSample.Helper
{
    public class MFAuthenticationHelper
    {
        //需安裝套件Otp.Net
        public string GenQRCodeUrl(string Secret,string Lable="Dino",string Issuer= "TOTP測試")
        {
            return $"otpauth://totp/{Lable}?issuer={Uri.EscapeDataString(Issuer)}&secret={Secret}";
        }
        public (bool isValid,string info) ValidateTotp(string totp,string Secret = null, Totp totpInstance = null)
        {
            try
            {
                if(totp == null  || Secret==null && totpInstance == null)
                    return (false, "驗證失敗");
                if (totpInstance == null)
                    totpInstance = new Totp(Base32Encoding.ToBytes(Secret));
                long timedWindowUsed;
                if (totpInstance.VerifyTotp(totp, out timedWindowUsed))
                    return (true,$"驗證通過 - {timedWindowUsed}");
                else
                    return (false, "驗證失敗");
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Top Vaild Faill:{1}",DateTime.Now,ex.ToString());
                return (false, "驗證失敗");
            }
    
        }
    }

}
