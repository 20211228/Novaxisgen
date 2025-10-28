namespace Novaxisgen.Models
{
    public class CayNhiPhanViewModel
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string? HoTen { get; set; }
        public string? NhaBaoTro { get; set; }

        public CayNhiPhanViewModel? Trai { get; set; }
        public CayNhiPhanViewModel? Phai { get; set; }
    }


}
