namespace WebQlyPhatTu.Models
{
    public class PhatTuDaoTrang
    {
        public int PhatTuDaoTrangId { get; set; }
        public Boolean DaThamGia { get; set; }
        public string LiDoKhongThamGia { get; set; }
        public int DaoTrangId { get; set; }
        public DaoTrang? DaoTrang { get; set; }
        public int PhatTuId { get; set; }
        public PhatTu? PhatTu { get; set; }
    }
}
