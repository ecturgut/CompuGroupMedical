using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ConpuGroupMedical.Models;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace ConpuGroupMedical.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly IDbConnection _dbConnection;
        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }
        
        public HomeController(ILogger<HomeController> logger,IDbConnection connection, IConfiguration configuration)
        {
            Configuration = configuration;
            _dbConnection=connection;
             var result= _dbConnection.Query("select * from hastane",null,commandType: CommandType.Text);
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Gruplar()
        {
            return View();
        }
         public IActionResult Grup_Islemleri()
        {
            var result = _dbConnection.Query("select * from Grup_Tanımı", null, commandType: CommandType.Text); 
            return View(result);
        }
        public IActionResult Sigortalar()
        {
            return View();
        }
         public IActionResult Sigorta_Islemleri()
        {
            var result = _dbConnection.Query("select * from Sigorta", null, commandType: CommandType.Text); 
            return View(result);
        }
        public IActionResult Hastaneler()
        {
            return View();
        }
        public IActionResult Hastane_Islemleri()
        {
           var result = _dbConnection.Query("select * from hastane", null, commandType: CommandType.Text); 
            return View(result);
            
        }

public ActionResult SigortaSil(int id)
 {
 string connectionString = this.Configuration.GetConnectionString("appDbConnection");

            Sigorta sgrt = new Sigorta();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete From Sigorta Where Id='"+ id +"'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                    sgrt.Id = Convert.ToInt32(dataReader["Id"]);
                    sgrt.Sigorta_Sirket_Kodu = Convert.ToInt32(dataReader["Sigorta_Sirket_Kodu"]);
                    sgrt.Sigorta_Adi = Convert.ToString(dataReader["Sigorta_Adi"]);
                    sgrt.Sigorta_Telefon = Convert.ToString(dataReader["Sigorta_Telefon"]);
                    sgrt.Sigorta_Adres = Convert.ToString(dataReader["Sigorta_Adres"]);
                    }
                }

                connection.Close();
            }
            return View(sgrt);
 }

        public ActionResult HastaneSil(int id)
 {
 string connectionString = this.Configuration.GetConnectionString("appDbConnection");

            Hastane hastane = new Hastane();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete From Hastane Where Id='"+ id +"'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                    hastane.Id = Convert.ToInt32(dataReader["Id"]);
                    hastane.Tesis_Kodu = Convert.ToInt32(dataReader["Tesis_Kodu"]);
                    hastane.Hastane_Adi = Convert.ToString(dataReader["Hastane_Adi"]);
                    hastane.Hastane_Telefon = Convert.ToString(dataReader["Hastane_Telefon"]);
                    hastane.Hastene_Adres = Convert.ToString(dataReader["Hastene_Adres"]);
                    hastane.Bagli_Oldugu_Grup_Id = Convert.ToInt32(dataReader["Bagli_Oldugu_Grup_Id"]);
                    }
                }

                connection.Close();
            }
            return View(hastane);
 }
public ActionResult GrupSil(int id)
 {
 string connectionString = this.Configuration.GetConnectionString("appDbConnection");

            Grup_Tanimi grup = new Grup_Tanimi();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete From Grup_Tanımı Where Id='"+ id +"'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                    grup.Id = Convert.ToInt32(dataReader["Id"]);
                    grup.Grup_Kodu = Convert.ToInt32(dataReader["Grup_Kodu"]);
                    grup.Grup_Adi = Convert.ToString(dataReader["Grup_Adi"]);
                    }
                }

                connection.Close();
            }
            return View(grup);
 }
        public IActionResult SilIslemi(int id){
        
            var delete = _dbConnection.Query("delete from Hastane where Id='"+ id +"'" );
            
            return RedirectToAction("Hastane_Islemleri","Home");
        }

        
        public IActionResult HastaneDuzenle(int id)
        {
          string connectionString = this.Configuration.GetConnectionString("appDbConnection");

            Hastane hastane = new Hastane();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From Hastane Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                    
                    hastane.Id = Convert.ToInt32(dataReader["Id"]);
                    hastane.Tesis_Kodu = Convert.ToInt32(dataReader["Tesis_Kodu"]);
                    hastane.Hastane_Adi = Convert.ToString(dataReader["Hastane_Adi"]);
                    hastane.Hastene_Adres = Convert.ToString(dataReader["Hastene_Adres"]);
                    hastane.Hastane_Telefon = Convert.ToString(dataReader["Hastane_Telefon"]);
                    hastane.Bagli_Oldugu_Grup_Id = Convert.ToInt32(dataReader["Bagli_Oldugu_Grup_Id"]);
                    }
                }

                connection.Close();
            }
            return View(hastane);
        }
        
        [HttpPost]
        public IActionResult HastaneDuzenle(Hastane hastane)
        {
             string connectionString = this.Configuration.GetConnectionString("appDbConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Update Hastane SET TesisKodu='"+ hastane.Tesis_Kodu + "', HastaneAdı='" + hastane.Hastane_Adi +"', HastaneAdres='"+ hastane.Hastene_Adres +"', HastaneTelefon='"+ hastane.Hastane_Telefon +"',BaglıOlduguGrupId='"+ hastane.Bagli_Oldugu_Grup_Id +"' Where Id='{hastane.Id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return RedirectToAction("Hastane_Islemleri");
        }
        [HttpPost]
         public IActionResult SigortaDuzenle(Sigorta sgrt)
        {
         string connectionString = this.Configuration.GetConnectionString("appDbConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Update Sigorta SET SigortaSirketKodu='"+ sgrt.Sigorta_Sirket_Kodu + "', SigortaAdı='"+sgrt.Sigorta_Adi+"', SigortaAdres='"+sgrt.Sigorta_Adres+"', SigortaTelefon='"+sgrt.Sigorta_Telefon+"' Where Id='{sgrt.Id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return RedirectToAction("Sigorta_Islemleri");
        }

        public IActionResult SigortaDuzenle(int id)
        {
          string connectionString = this.Configuration.GetConnectionString("appDbConnection");

            Sigorta sigorta = new Sigorta();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From Sigorta Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                    
                    sigorta.Id = Convert.ToInt32(dataReader["Id"]);
                    sigorta.Sigorta_Sirket_Kodu = Convert.ToInt32(dataReader["Sigorta_Sirket_Kodu"]);
                    sigorta.Sigorta_Adi = Convert.ToString(dataReader["Sigorta_Adi"]);
                    sigorta.Sigorta_Adres = Convert.ToString(dataReader["Sigorta_Adres"]);
                    sigorta.Sigorta_Telefon = Convert.ToString(dataReader["Sigorta_Telefon"]);
                    }
                }

                connection.Close();
            }
            return View(sigorta);
        }
        public IActionResult GrupDuzenle(int id)
        {
            
            string connectionString = this.Configuration.GetConnectionString("appDbConnection");

            Grup_Tanimi grup = new Grup_Tanimi();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From Grup_Tanımı Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                    grup.Id = Convert.ToInt32(dataReader["Id"]);
                    grup.Grup_Kodu = Convert.ToInt32(dataReader["Grup_Kodu"]);
                    grup.Grup_Adi = Convert.ToString(dataReader["Grup_Adi"]);
                    }
                }

                connection.Close();
            }
            return View(grup);
        }
        [HttpPost]
        public IActionResult GrupDuzenle(Grup_Tanimi grp)
        {
            string connectionString = this.Configuration.GetConnectionString("appDbConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Update Grup_Tanımı SET GrupKodu='"+ grp.Grup_Kodu + "', GrupAdı='"+grp.Grup_Adi+"' Where Id='{grp.Id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return RedirectToAction("Grup_Islemleri");
        }

       [HttpPost]
        public IActionResult HastaneEkle(Hastane hastane)
        {
            if (ModelState.IsValid)
            {
                string connectionString = this.Configuration.GetConnectionString("appDbConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = "Insert Into Hastane (Tesis_Kodu, Hastane_Adi, Hastane_Telefon, Hastane_Adres,Bagli_Oldugu_Grup_Id) Values ('" + hastane.Tesis_Kodu + "','" + hastane.Hastane_Adi + "','" + hastane.Hastane_Telefon + "','" +  hastane.Hastene_Adres + "','" + hastane.Bagli_Oldugu_Grup_Id + "')"; 
                   using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    return RedirectToAction("Hastane_Islemleri");
                }
            }
            else
                return View();
        }
       
        public IActionResult HastaneEkle()
        { 
            return View();
        }

        public IActionResult GrupEkle()
        { 
            return View();
        }

        [HttpPost]
        public IActionResult GrupEkle(Grup_Tanimi grup)
        {
          if (ModelState.IsValid)
            {
                string connectionString = this.Configuration.GetConnectionString("appDbConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = "Insert Into Grup_Tanımı (Grup_Kodu, Grup_Adi) Values ('" + grup.Grup_Kodu + "','" + grup.Grup_Adi + "')"; 
                   using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    return RedirectToAction("Grup_Islemleri");
                }
            }
            else
                return View();  
          
        }


        [HttpPost]
        public IActionResult SigortaEkle(Sigorta sigorta)
        {
            if (ModelState.IsValid)
            {
                string connectionString = this.Configuration.GetConnectionString("appDbConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = "Insert Into Sigorta (Sigorta_Sirket_Kodu, Sigorta_Adi, Sigorta_Telefon, Sigorta_Adres) Values ('" + sigorta.Sigorta_Sirket_Kodu + "','" + sigorta.Sigorta_Adi + "','" + sigorta.Sigorta_Telefon + "','" +  sigorta.Sigorta_Adres + "')";
                    
                   using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    return RedirectToAction("Sigorta_Islemleri");
                }
            }
            else
                return View();
        }
        public IActionResult SigortaEkle()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ActionResult HastaneSec() {
             
        
            return View();
    

        }

        public ActionResult SigortaSec() {

        return View();

        }
        public ActionResult GrupSec() {

        return View();

        }
    }
}
