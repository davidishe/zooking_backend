using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using QRCoder;

namespace MyAppBack.Data
{


  public class QrRepository : IQrRepository
  {

    private readonly IWebHostEnvironment _env;
    public QrRepository(IWebHostEnvironment env)
    {
      _env = env;
    }


    public string CreateQrCode(string qrText)
    {
      QRCodeGenerator qrGenerator = new QRCodeGenerator();
      QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
      string fileGuid = Guid.NewGuid().ToString().Substring(0, 6);
      string fileName = "file-" + fileGuid + ".png";
      QRCode qrCode = new QRCode(qrCodeData);
      Bitmap qrCodeImage = qrCode.GetGraphic(20);

      var webRoot = _env.WebRootPath;
      var PathWithFolderName = System.IO.Path.Combine(webRoot, "assets", "qrr/");
      Console.WriteLine(PathWithFolderName.ToString());

      qrCodeImage.Save(PathWithFolderName + fileName, ImageFormat.Png);
      Console.WriteLine("The image was save into directory " + PathWithFolderName);
      return fileName;

    }

  }
}