using SkiaSharp;
using SkiaSharp.QrCode;

namespace MFAuthenticationSample.Helper
{    
    public class QrCodeHelper
    {
        //由於.net 6 Img會出現不適用於非Windows平台故使用 SkiaSharp 來處理繪圖功能
        //搭配套件 SkiaSharp、SkiaSharp.QrCode
        public string CreateQrCode(string message, int width = 246, int height = 246)
        {       
            try
            {
                if (string.IsNullOrWhiteSpace(message))
                    return null;
                SKImageInfo info = new SKImageInfo(width, height);
                using (var surface = SKSurface.Create(info))
                {
                    QRCodeGenerator generator = new QRCodeGenerator();
                    QRCodeData qr = generator.CreateQrCode(message, ECCLevel.H);
                    var canvas = surface.Canvas;
                    // 渲染到Canvas
                    canvas.Render(qr, info.Width, info.Height);
                    // 轉為byte array
                    SKData snapshot = surface.Snapshot().Encode();
                    byte[] bytes = snapshot.ToArray();
                    // 轉為Image
                    var base64 = Convert.ToBase64String(bytes);
                    string Result = String.Format("data:image/gif;base64,{0}", base64);
                    return Result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} QrCode Create fail:{1}",DateTime.Now,ex.Message);
                return null;
            }            
        }

    }
}
