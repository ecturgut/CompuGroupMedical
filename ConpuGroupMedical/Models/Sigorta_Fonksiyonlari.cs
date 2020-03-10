using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConpuGroupMedical.Models
{
public class Sigorta_Fonksiyonlari
{
[Key]
public int Id { get; set; }
public int Sigorta_Id { get; set; }
public int Fonksiyon_Id { get; set; }
public Fonksiyonlar Fonksiyonlar { get; set; }
public Sigorta Sigorta { get; set; }

}

}
