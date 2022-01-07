using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.Models
{
    public class Kho
    {
        public int MaKho { get; set; }
        public int MaHH { get; set; }
        public HangHoa HangHoa { get; set; }
        public int SoLuong { get; set; }
        public string GhiChu { get; set; }
    }
}
