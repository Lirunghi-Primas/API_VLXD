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
    public class HoaDon_XuatController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public HoaDon_XuatController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select MaHDXuat, MaKH, MaNV, 
                            DATE_FORMAT(NgayXuat, '%Y-%m-%d') as NgayXuat,
                            GhiChu from hoadon_xuat
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
        public JsonResult Post(HoaDon_Xuat hdx)
        {
            string query = @"
                            insert into hoadon_xuat 
                            (MaKH,MaNV,NgayXuat,GhiChu) 
                            values 
                            (@MaKH,@MaNV,@NgayXuat,@GhiChu);
                           ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaKH", hdx.MaKH);
                    myCommand.Parameters.AddWithValue("@MaNV", hdx.MaNV);
                    myCommand.Parameters.AddWithValue("@NgayXuat", hdx.NgayXuat);
                    myCommand.Parameters.AddWithValue("@GhiChu", hdx.GhiChu);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Thêm thành công!");
        }

        [HttpPut]
        public JsonResult Put(HoaDon_Xuat hdx)
        {
            string query = @"
                            update hoadon_nhap set 
                            MaHDXuat = @MaHDXuat,
                            MaKH = @MaKH,
                            MaNV = @MaNV,
                            NgayXuat = @NgayXuat,
                            GhiChu = @GhiChu,
                            where MaHDXuat = @MaHDXuat;
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaHDXuat", hdx.MaHDXuat);
                    myCommand.Parameters.AddWithValue("@MaKH", hdx.MaKH);
                    myCommand.Parameters.AddWithValue("@MaNV", hdx.MaNV);
                    myCommand.Parameters.AddWithValue("@NgayXuat", hdx.NgayXuat);
                    myCommand.Parameters.AddWithValue("@GhiChu", hdx.GhiChu);

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
                            delete from hoadon_xuat 
                           where MaHDXuat = @MaHDXuat;
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaHDXuat", id);


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
