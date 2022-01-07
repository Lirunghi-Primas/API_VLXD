using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.Models
{
    public class HoaDon_Nhap
    {
        public int MaHDNhap{ get; set; }
        public int MaNCC { get; set; }
        public  NhaCungCap NhaCungCap { get; set; }
        public int MaNV { get; set; }
        public  NhanVien NhanVien { get; set; }
        public DateTime NgayNhap { get; set; }
    }
}
