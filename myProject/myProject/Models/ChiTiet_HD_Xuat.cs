using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.Models
{
    public class ChiTiet_HD_Xuat
    {
        public int MaChiTietHDXuat { get; set; }
        public int MaHH { get; set; }
        public HangHoa HangHoa { get; set; }
        public int MaHDXuat { get; set; }
        public HoaDon_Xuat  HoaDon_Xuat {get; set;}
        public int SoLuongXuat { get; set; }
        public int DonGia { get; set; }
    }
}
