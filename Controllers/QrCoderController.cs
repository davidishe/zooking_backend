using System;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace MyAppBack.Controllers
{

  [AllowAnonymous]
  [Route("qr-coder")]
  [ApiController]
  public class QRCoderController : ControllerBase
  {


    [HttpPost]
    [AllowAnonymous]
    [Route("create-qr/")]
    public IActionResult GenerateFile([FromQuery(Name = "qrText")] string qrText)
    {
      QRCodeGenerator qrGenerator = new QRCodeGenerator();
      QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
      string fileGuid = Guid.NewGuid().ToString().Substring(0, 6);
      QRCode qrCode = new QRCode(qrCodeData);
      Bitmap qrCodeImage = qrCode.GetGraphic(20);
      string path = "wwwroot/assets/qrr/file-" + fileGuid + ".png";
      qrCodeImage.Save(path, ImageFormat.Png);
      return Ok(new JsonResult("file-" + fileGuid + ".png"));
    }

  }
}