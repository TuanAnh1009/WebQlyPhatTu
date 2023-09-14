namespace WebQlyPhatTu.Helper
{
    public class ReturnObject<T>
    {
        public string Mes { get; set; }
        public bool Error { get; set; }
        public T Data { get; set; }
        public ReturnObject()
        {
            Error = false;
        }
    }
}
