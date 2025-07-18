namespace NhaHang.Models
{
    public class CartItem
    {
        public string MaMonAn { get; set; } = string.Empty;
        public string TenMonAn { get; set; } = string.Empty;
        public int SoLuong { get; set; } = 1;
        public decimal DonGia { get; set; }
        public int? MucCay { get; set; }
        public decimal ThanhTien => SoLuong * DonGia;
    }
} 