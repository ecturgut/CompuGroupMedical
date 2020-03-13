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
        public IActionResult Fonksiyonlar()
        {
            var result = _dbConnection.Query("select * from Fonksiyonlar", null, commandType: CommandType.Text); 
            ViewBag.Fonksiyons = new SelectList(result.ToList(), "Id","Fonksiyon_Adi");
            return View();
        }
        public IActionResult Gruplar()
        { 
            var result = _dbConnection.Query("select * from Grup_Tanımı", null, commandType: CommandType.Text); 

            ViewBag.Groups = new SelectList(result.ToList(), "Grup_Kodu", "Grup_Adi");
            return View();

        }
        public IActionResult Fonksiyon_Islemleri()
        {
            var result = _dbConnection.Query("select * from Fonksiyonlar", null, commandType: CommandType.Text); 
            return View(result);
        }
        public IActionResult Grup_Islemleri()
        {
            var result = _dbConnection.Query("select * from Grup_Tanımı", null, commandType: CommandType.Text); 
            return View(result);
        }
        public IActionResult Sigortalar()
        {
            var result = _dbConnection.Query("select * from Sigorta", null, commandType: CommandType.Text); 
            ViewBag.Sigortas = new SelectList(result.ToList(), "Id","Sigorta_Adi");
            return View();
        }
         public IActionResult Sigorta_Islemleri()
        {
            var result = _dbConnection.Query("select * from Sigorta", null, commandType: CommandType.Text); 
            return View(result);
        }
        public IActionResult Sigorta_Fonksiyonlari()
        {
            var result = _dbConnection.Query("select s.Id, f.Fonksiyon_Adi ,si.Sigorta_Adi from SigortaFonksiyonlari s inner join fonksiyonlar f on s.Fonksiyon_Id = f.Id inner join Sigorta si on si.Id=s.Sigorta_Id;", null, commandType: CommandType.Text); 

            ViewBag.SigortaFonk = new SelectList(result.ToList(),"Id","Sigorta_Id","Fonksiyon_Adi");  
            return View(result);
        }
        public IActionResult Hastaneler()
        {
            var result = _dbConnection.Query("select * from hastane", null, commandType: CommandType.Text); 
            ViewBag.Hastanes = new SelectList(result.ToList(), "Id","Hastane_Adi");
   
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
 public ActionResult FonksiyonSil(int id)
 {
 string connectionString = this.Configuration.GetConnectionString("appDbConnection");

            Fonksiyonlar fnk = new Fonksiyonlar();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete From Fonksiyonlar Where Id='"+ id +"'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                    fnk.Id = Convert.ToInt32(dataReader["Id"]);
                    fnk.Fonksiyon_Adi = Convert.ToString(dataReader["Fonksiyon_Adi"]);
                    fnk.Fonksiyon_Kodu = Convert.ToInt32(dataReader["Fonksiyon_Kodu"]);
                    }
                }

                connection.Close();
            }
            return View(fnk);
 }

 public ActionResult Sigorta_Fonksiyonlari_Sil(int id)
 {
 string connectionString = this.Configuration.GetConnectionString("appDbConnection");

            Sigorta_Fonksiyonlari sg = new Sigorta_Fonksiyonlari();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete From SigortaFonksiyonlari Where Id='"+ id +"'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                    sg.Id = Convert.ToInt32(dataReader["Id"]);
                    sg.Sigorta_Id = Convert.ToInt32(dataReader["Sigorta_Id"]);
                    sg.Fonksiyon_Id = Convert.ToInt32(dataReader["Fonksiyon_Id"]);
                    }
                }

                connection.Close();
            }
            return View(sg);
 }
      
        public IActionResult FonksiyonDuzenle(int id)
        {
          string connectionString = this.Configuration.GetConnectionString("appDbConnection");

            Fonksiyonlar fnk = new Fonksiyonlar();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From Fonksiyonlar Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {   
                    fnk.Id = Convert.ToInt32(dataReader["Id"]);
                    fnk.Fonksiyon_Adi  = Convert.ToString(dataReader["Fonksiyon_Adi"]);
                    fnk.Fonksiyon_Kodu = Convert.ToInt32(dataReader["Fonksiyon_Kodu"]);
                    }
                }

                connection.Close();
            }
            return View(fnk);
        }
        
        [HttpPost]
        public IActionResult FonksiyonDuzenle(Fonksiyonlar fnk)
        {
             string connectionString = this.Configuration.GetConnectionString("appDbConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Update Fonksiyonlar SET Fonksiyon_Adi='"+ fnk.Fonksiyon_Adi + "', Fonksiyon_Kodu='" + fnk.Fonksiyon_Kodu +"' Where Id="+fnk.Id;
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return RedirectToAction("Fonksiyon_Islemleri");
        }

         public IActionResult Sigorta_Fonksiyonlari_Duzenle(int id)
        {
          string connectionString = this.Configuration.GetConnectionString("appDbConnection");

            Sigorta_Fonksiyonlari sgrtfnk = new Sigorta_Fonksiyonlari();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From SigortaFonksiyonlari Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {   
                    sgrtfnk.Id = Convert.ToInt32(dataReader["Id"]);
                    sgrtfnk.Fonksiyon_Id  = Convert.ToInt32(dataReader["Fonksiyon_Id"]);
                    sgrtfnk.Sigorta_Id = Convert.ToInt32(dataReader["Sigorta_Id"]);
                    }
                }

                connection.Close();
            }
            return View(sgrtfnk);
        }

 [HttpPost]
        public IActionResult Sigorta_Fonksiyonlari_Duzenle(Sigorta_Fonksiyonlari sgrtfnk)
        {
             string connectionString = this.Configuration.GetConnectionString("appDbConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Update SigortaFonksiyonlari SET Fonksiyon_Id='"+ sgrtfnk.Fonksiyon_Id + "', Sigorta_Id='" + sgrtfnk.Sigorta_Id +"' Where Id="+sgrtfnk.Id;
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return RedirectToAction("Sigorta_Fonksiyonlari");
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
                    hastane.Hastene_Adres = Convert.ToString(dataReader["Hastane_Adres"]);
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
                string sql = $"Update Hastane SET Tesis_Kodu='"+ hastane.Tesis_Kodu + "', Hastane_Adi='" + hastane.Hastane_Adi +"', Hastane_Adres='"+ hastane.Hastene_Adres +"', Hastane_Telefon='"+ hastane.Hastane_Telefon +"',Bagli_Oldugu_Grup_Id='"+ hastane.Bagli_Oldugu_Grup_Id +"' Where Id="+hastane.Id;
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
                string sql = $"Update Sigorta SET Sigorta_Sirket_Kodu='"+ sgrt.Sigorta_Sirket_Kodu + "', Sigorta_Adi='"+sgrt.Sigorta_Adi+"', Sigorta_Adres='"+sgrt.Sigorta_Adres+"', Sigorta_Telefon='"+sgrt.Sigorta_Telefon+"' Where Id="+sgrt.Id;
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
                string sql = $"Update Grup_Tanımı SET Grup_Kodu='"+ grp.Grup_Kodu + "', Grup_Adi='"+grp.Grup_Adi+"' Where Id="+grp.Id;
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

       [HttpPost]
        public IActionResult HastaneEkle(Hastane hastane)
        {
            if (ModelState.IsValid)
            {
                string connectionString = this.Configuration.GetConnectionString("appDbConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = "Insert Into Hastane (Tesis_Kodu, Hastane_Adi, Hastane_Telefon, Hastane_Adres, Bagli_Oldugu_Grup_Id) Values ('" + hastane.Tesis_Kodu + "','" + hastane.Hastane_Adi + "','" + hastane.Hastane_Telefon + "','" +  hastane.Hastene_Adres + "','" + hastane.Bagli_Oldugu_Grup_Id + "')"; 
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

        [HttpPost]
        public IActionResult FonksiyonEkle(Fonksiyonlar fnk)
        {
            if (ModelState.IsValid)
            {
                string connectionString = this.Configuration.GetConnectionString("appDbConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = "Insert Into Fonksiyonlar (Fonksiyon_Kodu, Fonksiyon_Adi) Values ('" + fnk.Fonksiyon_Kodu + "','" + fnk.Fonksiyon_Adi + "')"; 
                   using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    return RedirectToAction("Fonksiyon_Islemleri");
                }
                
            }
            else
                return View();
        }

        [HttpPost]
        public IActionResult Sigorta_Fonksiyonlari_Ekle(Sigorta_Fonksiyonlari sgfnk)
        {
            if (ModelState.IsValid)
            {
                string connectionString = this.Configuration.GetConnectionString("appDbConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                 string sql = "Insert Into SigortaFonksiyonlari (Fonksiyon_Id,Sigorta_Id) Values ('" + sgfnk.Fonksiyon_Id + "','" + sgfnk.Sigorta_Id + "')";
                   using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    return RedirectToAction("Sigorta_Fonksiyonlari");
                }
                
            }
            else

            return View();
        }
       public IActionResult Sigorta_Fonksiyonlari_Ekle()
        { 
          var result = _dbConnection.Query("select * from Sigorta", null, commandType: CommandType.Text); 
          ViewBag.Sigortas = new SelectList(result.ToList(), "Id","Sigorta_Adi");
            
         var result2 = _dbConnection.Query("select * from Fonksiyonlar", null, commandType: CommandType.Text);
            ViewBag.Fonk = new SelectList(result2.ToList(), "Id","Fonksiyon_Adi");
            
        return View();
        }
        public IActionResult FonksiyonEkle()
        { 
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
        public IActionResult SigortaEkle()
        {
            return View();
        }      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}
