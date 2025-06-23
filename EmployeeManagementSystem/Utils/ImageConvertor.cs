using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EmployeeManagementSystem.Utils
{
    public class ImageConvertor
    {
        public static ImageSource? ConvertBase64ToImage(string base64)
        {
            if (string.IsNullOrEmpty(base64)) return null;

            byte[] imageBytes = Convert.FromBase64String(base64);

            using (var ms = new MemoryStream(imageBytes))
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = ms;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                return bitmap;
            }
        }
    }
}
