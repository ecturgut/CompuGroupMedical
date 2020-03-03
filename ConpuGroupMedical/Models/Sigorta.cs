using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConpuGroupMedical.Models
{
public class Sigorta
{
[Key]
public int Id { get; set; }
public int Sigorta_Sirket_Kodu { get; set; }
public string Sigorta_Adi { get; set; }
public string Sigorta_Telefon { get; set; }
public string Sigorta_Adres { get; set; }
}

}
