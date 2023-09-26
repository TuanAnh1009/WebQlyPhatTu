using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using WebQlyPhatTu.Models;

namespace WebQlyPhatTu.Dto
{
    public class PasswordRefresh
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string RefreshCode { get; set; }
        [Required]
        public int PhatTuId { get; set; }
        public PhatTu PhatTu { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime ExpireAt { get; set; }
        public bool Expired { get; set; }
        public PasswordRefresh()
        {
            CreateAt = DateTime.UtcNow;
            ExpireAt = DateTime.UtcNow.AddMinutes(5);
            RefreshCode = Convert.ToHexString(RandomNumberGenerator.GetBytes(3));
            Expired = false;
        }
    }
}
