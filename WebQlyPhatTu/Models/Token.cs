namespace WebQlyPhatTu.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string Stoken { get; set; }
        public int Sxpired { get; set; }
        public int Revoked { get; set; }
        public string TokenType { get; set; }
        public int PhatTuId { get; set; }
        public PhatTu PhatTu { get; set; }
    }
}
