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
    public class ChiTietHD_XuatController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ChiTietHD_XuatController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select MaChiTietHDXuat, MaHH, MaHDXuat, 
                            SoLuongXuat, DonGia from chitiet_hd_xuat
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
        public JsonResult Post(ChiTiet_HD_Xuat cthdx)
        {
            string query = @"
                            insert into chitiet_hd_xuat 
                            (MaHH,MaHDXuat,SoLuongXuat,DonGia) 
                            values 
                            (@MaHH,@MaHDXuat,@SoLuongXuat,@DonGia);
                           ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaHH", cthdx.MaHH);
                    myCommand.Parameters.AddWithValue("@MaHDXuat", cthdx.MaHDXuat);
                    myCommand.Parameters.AddWithValue("@SoLuongXuat", cthdx.SoLuongXuat);
                    myCommand.Parameters.AddWithValue("@DonGia", cthdx.DonGia);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Thêm thành công!");
        }

        [HttpPut]
        public JsonResult Put(ChiTiet_HD_Xuat cthdx)
        {
            string query = @"
                            update chitiet_hd_xuat set 
                            MaChiTietHDXuat = @MaChiTietHDXuat,
                            MaHH = @MaHH,
                            MaHDXuat = @MaHDXuat,
                            SoLuongXuat = @SoLuongXuat,
                            DonGia = @DonGia
                            where MaChiTietHDXuat = @MaChiTietHDXuat;
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaChiTietHDXuat", cthdx.MaChiTietHDXuat);
                    myCommand.Parameters.AddWithValue("@MaHH", cthdx.MaHH);
                    myCommand.Parameters.AddWithValue("@MaHDXuat", cthdx.MaHDXuat);
                    myCommand.Parameters.AddWithValue("@SoLuongXuat", cthdx.SoLuongXuat);
                    myCommand.Parameters.AddWithValue("@DonGia", cthdx.DonGia);

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
                            delete from chitiet_hd_xuat 
                           where MaChiTietHDXuat = @MaChiTietHDXuat;
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaChiTietHDXuat", id);


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
