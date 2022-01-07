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
    public class ChiTietHD_NhapController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ChiTietHD_NhapController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select MaChiTietHDNhap, MaHH, MaHDNhap, 
                            SoLuongNhap, DonGia from chitiet_hd_nhap
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
        public JsonResult Post(ChiTiet_HD_Nhap cthdn)
        {
            string query = @"
                            insert into chitiet_hd_nhap 
                            (MaHH,MaHDNhap,SoLuongNhap,DonGia) 
                            values 
                            (@MaHH,@MaHDNhap,@SoLuongNhap,@DonGia);
                           ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaHH", cthdn.MaHH);
                    myCommand.Parameters.AddWithValue("@MaHDNhap", cthdn.MaHDNhap);
                    myCommand.Parameters.AddWithValue("@SoLuongNhap", cthdn.SoLuongNhap);
                    myCommand.Parameters.AddWithValue("@DonGia", cthdn.DonGia);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Thêm thành công!");
        }

        [HttpPut]
        public JsonResult Put(ChiTiet_HD_Nhap cthdn)
        {
            string query = @"
                            update chitiet_hd_nhap set 
                            MaChiTietHDNhap = @MaChiTietHDNhap,
                            MaHH = @MaHH,
                            MaHDNhap = @MaHDNhap,
                            SoLuongNhap = @SoLuongNhap,
                            DonGia = @DonGia
                            where MaChiTietHDNhap = @MaChiTietHDNhap;
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaChiTietHDNhap", cthdn.MaChiTietHDNhap);
                    myCommand.Parameters.AddWithValue("@MaHH", cthdn.MaHH);
                    myCommand.Parameters.AddWithValue("@MaHDNhap", cthdn.MaHDNhap);
                    myCommand.Parameters.AddWithValue("@SoLuongNhap", cthdn.SoLuongNhap);
                    myCommand.Parameters.AddWithValue("@DonGia", cthdn.DonGia);

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
                            delete from chitiet_hd_nhap 
                           where MaChiTietHDNhap = @MaChiTietHDNhap;
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaChiTietHDNhap", id);


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
