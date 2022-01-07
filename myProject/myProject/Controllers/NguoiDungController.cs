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
    public class NguoiDungController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public NguoiDungController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select MaND, UserName, PassWord, 
                            Loai from nguoi_dung
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
        public JsonResult Post(NguoiDung nguoidung)
        {
            string query = @"
                            insert into nguoi_dung 
                            (UserName,PassWord,Loai) 
                            values 
                            (@UserName,@PassWord,@Loai)  ;
                           ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@UserName", nguoidung.UserName);
                    myCommand.Parameters.AddWithValue("@PassWord", nguoidung.PassWord);
                    myCommand.Parameters.AddWithValue("@Loai", nguoidung.Loai);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Thêm thành công!");
        }

        [HttpPut]
        public JsonResult Put(NguoiDung nguoidung)
        {
            string query = @"
                            update nguoi_dung set 
                            MaND = @MaND,
                            UserName = @UserName,
                            PassWord = @PassWord,
                            Loai = @Loai
                            where MaND = @MaND;
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaND", nguoidung.MaND);
                    myCommand.Parameters.AddWithValue("@UserName", nguoidung.UserName);
                    myCommand.Parameters.AddWithValue("@PassWord", nguoidung.PassWord);
                    myCommand.Parameters.AddWithValue("@Loai", nguoidung.Loai);

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
                            delete from nguoi_dung 
                           where MaND = @MaND;
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaND", id);


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
