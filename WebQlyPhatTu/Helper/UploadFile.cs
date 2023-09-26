using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace WebQlyPhatTu.Helper
{
    public class UploadFile
    {
        static string cloudName = "dfr9hw77f";
        static string apiKey = "386378358332995";
        static string apiSecret = "RXveJ4SChbGAcTLj04Z7j23g2uI";
        static private readonly Random rnd = new Random();

        static public Account account = new Account(cloudName, apiKey, apiSecret);
        static public Cloudinary _cloudinary = new Cloudinary(account);
        public static async Task<string> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("No file selected.");
            }

            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = "avatar" + "_" + DateTime.Now.Ticks + "image",// ID công khai tùy ý cho file phải tuyệt đối là không được giống nhau
                    Transformation = new Transformation().Width(150).Height(150).Crop("fill") // Cố định kích thước ảnh thành tỷ lệ 3*4 pixel
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                {
                    // Xử lý lỗi tải lên
                    throw new Exception(uploadResult.Error.Message);
                }

                // Lấy URL công khai của file tải lên


                string imageUrl = uploadResult.SecureUrl.ToString();

                // Tiếp tục xử lý hoặc lưu thông tin về file trong cơ sở dữ liệu


                return imageUrl;
            }
        }
    }
}
