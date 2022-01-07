using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using myProject.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public HangHoaController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select MaHH, MaLoai, TenHH, 
                                   GiaHH, HinhAnh, DonViTinh,
                                   XuatXu, MoTa from hanghoa
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
        public JsonResult Post(HangHoa hang)
        {
            string query = @"
                            insert into hanghoa
                            (MaLoai,TenHH,GiaHH,HinhAnh,DonViTinh,XuatXu,MoTa) 
                            values
                            (@MaLoai,@TenHH,@GiaHH,@HinhAnh,@DonViTinh,@XuatXu,@MoTa) ;
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaLoai", hang.MaLoai);
                    myCommand.Parameters.AddWithValue("@TenHH", hang.TenHH);
                    myCommand.Parameters.AddWithValue("@GiaHH", hang.GiaHH);
                    myCommand.Parameters.AddWithValue("@HinhAnh", hang.HinhAnh);
                    myCommand.Parameters.AddWithValue("@DonViTinh", hang.DonViTinh);
                    myCommand.Parameters.AddWithValue("@XuatXu", hang.XuatXu);
                    myCommand.Parameters.AddWithValue("@MoTa", hang.MoTa);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Thêm thành công!");
        }

        [HttpPut]
        public JsonResult Put(HangHoa hang)
        {
            string query = @"
                                    update hanghoa set 
                                    MaLoai = @MaLoai,
                                    TenHH = @TenHH,
                                    GiaHH = @GiaHH,
                                    HinhAnh = @HinhAnh,
                                    DonViTinh = @DonViTinh,
                                    XuatXu = @XuatXu,
                                    MoTa = @MoTa
                                    where MaHH = @MaHH;
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaHH", hang.MaHH);
                    myCommand.Parameters.AddWithValue("@MaLoai", hang.MaLoai);
                    myCommand.Parameters.AddWithValue("@TenHH", hang.TenHH);
                    myCommand.Parameters.AddWithValue("@GiaHH", hang.GiaHH);
                    myCommand.Parameters.AddWithValue("@HinhAnh", hang.HinhAnh);
                    myCommand.Parameters.AddWithValue("@DonViTinh", hang.DonViTinh);
                    myCommand.Parameters.AddWithValue("@XuatXu", hang.XuatXu);
                    myCommand.Parameters.AddWithValue("@MoTa", hang.MoTa);

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
                            delete from hanghoa 
                           where MaHH = @MaHH;
                        ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MyProjectConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@MaHH", id);


                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Xóa thành công!");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);

            }
            catch(Exception)
            {
                return new JsonResult("sat.png");
            }
        }
    }
}
