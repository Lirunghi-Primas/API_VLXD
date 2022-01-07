using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.Models
{
    public class HoaDon_Xuat
    {
        public int MaHDXuat { get; set; }
        public int MaKH { get; set; }
        public KhachHang KhachHang { get; set; }
        public int MaNV { get; set; }
        public DateTime NgayXuat { get; set; }
        public string GhiChu { get; set; }
    }
}
