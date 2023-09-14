namespace WebQlyPhatTu.Models
{
    public class UserRoles
    {
        public int Id { get; set; }
        public int PhatTuId { get; set; }
        public PhatTu PhatTu { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
