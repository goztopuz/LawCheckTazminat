using System;
namespace LawCheckTazminat.Models
{
    public class KidemTavan
    {
       public string Id { get; set; }
       public int yil { get; set; }
       public  DateTime baslangic { get; set; }
       public DateTime bitis { get; set; }
       public int donem { get; set; }
       public decimal miktar { get; set; }
       public string ek1 { get; set; }
        public decimal ek2 { get; set; }

    }
}
