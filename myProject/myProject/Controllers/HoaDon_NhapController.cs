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
    public class HoaDon_NhapController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public HoaDon_NhapController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select MaHDNhap, MaNCC, MaNV, 
                            DATE_FORMAT(NgayNhap, '%Y-%m-%d') as NgayNhap
                            from hoadon_nhap
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
        public JsonResult Post(HoaDon_Nhap hdn)
        {
            string query = @"
                            insert into hoadon_nhap 
                            (MaNCC,MaNV,NgayNhap) 
                            values 
                            (@MaNCC,@MaNV,@NgayNhap);
                           ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaNCC", hdn.MaNCC);
                    myCommand.Parameters.AddWithValue("@MaNV", hdn.MaNV);
                    myCommand.Parameters.AddWithValue("@NgayNhap", hdn.NgayNhap);
                    
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Thêm thành công!");
        }

        [HttpPut]
        public JsonResult Put(HoaDon_Nhap hdn)
        {
            string query = @"
                            update hoadon_nhap set 
                            MaHDNhap = @MaHDNhap,
                            MaNCC = @MaNCC,
                            MaNV = @MaNV,
                            NgayNhap = @NgayNhap
                            where MaHDNhap = @MaHDNhap;
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaHDNhap", hdn.MaHDNhap);
                    myCommand.Parameters.AddWithValue("@MaNCC", hdn.MaNCC);
                    myCommand.Parameters.AddWithValue("@MaNV", hdn.MaNV);
                    myCommand.Parameters.AddWithValue("@NgayNhap", hdn.NgayNhap);

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
                            delete from hoadon_nhap 
                           where MaHDNhap = @MaHDNhap;
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaHDNhap", id);


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
