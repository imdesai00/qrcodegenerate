using Microsoft.AspNetCore.Mvc;
using QRCoder; // Make sure you have this using directive
using System;
using System.Drawing;
using System.IO;
using static QRCoder.PayloadGenerator;
using System.Reflection;
using qrimage.Model;
using System.Text;
using Microsoft.AspNetCore.Hosting.Server;

namespace qrimage.Controllers // Make sure this namespace matches your project namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodeController : ControllerBase
    {
        [HttpPost]
        public IActionResult GenerateQRCode(QRCodeModel model)
        {

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(model.QRCodeText, QRCodeGenerator.ECCLevel.Q);
                BitmapByteQRCode qrCode = new(qrCodeData);
                string base64String = Convert.ToBase64String(qrCode.GetGraphic(20));
                byte[] imageBytes = Convert.FromBase64String(base64String);
                string filePath = "QRCodeImage.png";
                System.IO.File.WriteAllBytes(filePath, imageBytes);
                return Ok($"QR code image saved as: {filePath}");
            }
        }
    }
}
