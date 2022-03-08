using System;
using System.Collections.Generic;
using System.Text;

namespace LawCheckTazminat.Models
{
  public  class AsgariUcretTablosu
    {
            public string Id { get; set; }
            public string yil { get; set; }
            public decimal brut { get; set; }
            public decimal net { get; set; }
            public int yil2 { get; set; }
            public int donem { get; set; }
            public string aciklama { get; set; }
            public string ekbilgi1 { get; set; }
            public decimal ekbilgi2 { get; set; }
            public string ekbilgi3 { get; set; }
            public string ekbilgi4 { get; set; }
            public double ekbilgi5 { get; set; }
            public int ekbilgi6 { get; set; }
      
    }
}
