using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.Models
{
    public class HangHoa
    {
        public int MaHH { get; set; }
        public int MaLoai { get; set; }
        public LoaiHang loaiHang { get; set; }
        public string TenHH { get; set; }
        public int GiaHH { get; set; }
        public string HinhAnh { get; set; }
        public string DonViTinh { get; set; }
        public string XuatXu { get; set; }
        public string MoTa { get; set; }
    }
}
