using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.Models
{
    public class ChiTiet_HD_Nhap
    {
        public int MaChiTietHDNhap { get; set; }
        public int MaHH { get; set; }
        public HangHoa HangHoa { get; set; }
        public int MaHDNhap { get; set; }
        public HoaDon_Nhap hoaDon_Nhap {get; set;}
        public int SoLuongNhap { get; set; }
        public int DonGia { get; set; }
    }
}
