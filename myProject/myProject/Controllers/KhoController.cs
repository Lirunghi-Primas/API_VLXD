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
    public class KhoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public KhoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select MaKho, MaHH, SoLuong, GhiChu from kho
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
        public JsonResult Post(Kho kho)
        {
            string query = @"
                            insert into kho 
                            (MaHH,SoLuong,GhiChu) 
                            values 
                            (@MaHH,@SoLuong,@GhiChu);
                           ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaHH", kho.MaHH);
                    myCommand.Parameters.AddWithValue("@SoLuong", kho.SoLuong);
                    myCommand.Parameters.AddWithValue("@GhiChu", kho.GhiChu);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Thêm thành công!");
        }

        [HttpPut]
        public JsonResult Put(Kho kho)
        {
            string query = @"
                            update kho set 
                            MaHH = @MaHH,
                            SoLuong = @SoLuong,
                            GhiChu = @GhiChu
                            where MaKho = @MaKho;
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaKho", kho.MaKho);
                    myCommand.Parameters.AddWithValue("@MaHH", kho.MaHH);
                    myCommand.Parameters.AddWithValue("@SoLuong", kho.SoLuong);
                    myCommand.Parameters.AddWithValue("@GhiChu", kho.GhiChu);

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
                            delete from kho 
                           where MaKho = @MaKho;
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaKho", id);


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
