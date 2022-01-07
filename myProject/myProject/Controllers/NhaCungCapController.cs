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
    public class NhaCungCapController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public NhaCungCapController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select MaNCC, TenNCC, VatLieuCungCap, 
                            SDT, DiaChi from nhacungcap
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
        public JsonResult Post(NhaCungCap ncc)
        {
            string query = @"
                            insert into nhacungcap 
                            (TenNCC,VatLieuCungCap,SDT,DiaChi) 
                            values 
                            (@TenNCC,@VatLieuCungCap,@SDT,@DiaChi) ;
                           ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@TenNCC", ncc.TenNCC);
                    myCommand.Parameters.AddWithValue("@VatLieuCungCap", ncc.VatLieuCungCap);
                    myCommand.Parameters.AddWithValue("@SDT", ncc.SDT);
                    myCommand.Parameters.AddWithValue("@DiaChi", ncc.DiaChi);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Thêm thành công!");
        }

        [HttpPut]
        public JsonResult Put(NhaCungCap ncc)
        {
            string query = @"
                            update nhacungcap set 
                            MaNCC = @MaNCC,
                            TenNCC = @TenNCC,
                            VatLieuCungCap = @VatLieuCungCap,
                            SDT = @SDT,
                            DiaChi = @DiaChi
                            where MaNCC = @MaNCC;
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaNCC", ncc.MaNCC);
                    myCommand.Parameters.AddWithValue("@TenNCC", ncc.TenNCC);
                    myCommand.Parameters.AddWithValue("@VatLieuCungCap", ncc.VatLieuCungCap);
                    myCommand.Parameters.AddWithValue("@SDT", ncc.SDT);
                    myCommand.Parameters.AddWithValue("@DiaChi", ncc.DiaChi);

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
                            delete from nhacungcap 
                           where MaNCC = @MaNCC;
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaNCC", id);


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
