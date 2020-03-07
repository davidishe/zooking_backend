using System.Drawing;
using System.Threading.Tasks;

namespace MyAppBack.Data
{
  public interface IQrRepository
  {
    string CreateQrCode(string qrText);
  }
}