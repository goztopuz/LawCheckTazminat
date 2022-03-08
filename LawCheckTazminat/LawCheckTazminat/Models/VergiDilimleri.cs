using System;
namespace LawCheckTazminat.Models
{
    public class VergiDilimleri
    {
        public int Id { get; set; }
        public Nullable<decimal> Vd1Miktar { get; set; }
        public Nullable<decimal> Vd1Yuzde { get; set; }
        public Nullable<decimal> Vd2Miktar { get; set; }
        public Nullable<decimal> Vd2Yuzde { get; set; }
        public Nullable<decimal> Vd3Miktar { get; set; }
        public Nullable<decimal> Vd3Yuzde { get; set; }
        public Nullable<decimal> Vd4Miktar { get; set; }
        public Nullable<decimal> Vd4Yuzde { get; set; }
        public Nullable<decimal> VdX1Miktar { get; set; }
        public Nullable<decimal> Vd5Yuzde { get; set; }
        public Nullable<decimal> Vd5Miktar { get; set; }

        public Nullable<decimal> VdX1Yuzde { get; set; }
        public Nullable<decimal> VdX2Miktar { get; set; }
        public Nullable<decimal> VdX2Yude { get; set; }
        public decimal SgkTaban { get; set; }
        public decimal SgkTavan { get; set; }
        public decimal SgkTaban2 { get; set; }
        public decimal SgkTavan2 { get; set; }

        public string ek1 { get; set; }
        public string ek2 { get; set; }

    }
}
