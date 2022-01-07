using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using myProject.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public NhanVienController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select MaNV, TenNV, GioiTinh, 
                            DATE_FORMAT(NgaySinh, '%Y-%m-%d') as NgaySinh,
                            DiaChi, SDT, ChucVu from nhanvien
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(NhanVien nhanvien)
        {
            string query = @"
                            insert into nhanvien 
                            (TenNV,GioiTinh,NgaySinh,DiaChi,SDT,ChucVu) 
                            values 
                            (@TenNV,@GioiTinh,@NgaySinh,@DiaChi,@SDT,@ChucVu) ;
                           ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@TenNV", nhanvien.TenNV);
                    myCommand.Parameters.AddWithValue("@GioiTinh", nhanvien.GioiTinh);
                    myCommand.Parameters.AddWithValue("@NgaySinh", nhanvien.NgaySinh);
                    myCommand.Parameters.AddWithValue("@DiaChi", nhanvien.DiaChi);
                    myCommand.Parameters.AddWithValue("@SDT", nhanvien.SDT);
                    myCommand.Parameters.AddWithValue("@ChucVu", nhanvien.ChucVu);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Thêm thành công!");
        }

        [HttpPut]
        public JsonResult Put(NhanVien nhanvien)
        {
            string query = @"
                            update nhanvien set 
                            MaNV = @MaNV,
                            TenNV = @TenNV,
                            GioiTinh = @GioiTinh,
                            NgaySinh = @NgaySinh,
                            DiaChi = @DiaChi,
                            SDT = @SDT,
                            ChucVu = @ChucVu
                            where MaNV = @MaNV;
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaNV", nhanvien.MaNV);
                    myCommand.Parameters.AddWithValue("@TenNV", nhanvien.TenNV);
                    myCommand.Parameters.AddWithValue("@GioiTinh", nhanvien.GioiTinh);
                    myCommand.Parameters.AddWithValue("@NgaySinh", nhanvien.NgaySinh);
                    myCommand.Parameters.AddWithValue("@DiaChi", nhanvien.DiaChi);
                    myCommand.Parameters.AddWithValue("@SDT", nhanvien.SDT);
                    myCommand.Parameters.AddWithValue("@ChucVu", nhanvien.ChucVu);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Cập nhật thành công!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from nhanvien 
                           where MaNV = @MaNV;
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaNV", id);


                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Xóa thành công!");
        }
    }
}
