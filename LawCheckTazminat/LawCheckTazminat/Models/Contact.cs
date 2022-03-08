using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace LawCheckTazminat.Models
{
    /// <summary>
    /// Model for the contact.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class Contact
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        /// 
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "ad")]
        public string Ad { get; set; }

        [DataMember(Name = "soyad")]
        public string Soyad { get; set; }

        [DataMember(Name = "cinsiyet")]
        public string Cinsiyet { get; set; } = "Erkek";

        [DataMember(Name = "dogumTarihi")]
        public DateTime DogumTarihi { get; set; }

        [DataMember(Name = "kurumu")]
        public string Kurumu { get; set; }

        public string SearchTerm => $"{Ad } {Soyad} {Adres1} {Il1} {Ilce1} {Adres2} {Il2} {Ilce2} {Iletisim1} {Iletisim2} {Aciklama}";

        public string FullName => $"{Ad} {Soyad}";

        [DataMember(Name = "telefon1")]
        public string Telefon1 { get; set; }

        [DataMember(Name = "telefon2")]
        public string Telefon2 { get; set; }

        [DataMember(Name = "adres1")]
        public string Adres1 { get; set; }

        [DataMember(Name = "adres2")]
        public string Adres2 { get; set; }

        [DataMember(Name = "il1")]
        public string Il1 { get; set; }

        [DataMember(Name = "ilce1")]
        public string Ilce1 { get; set; }

        [DataMember(Name = "pKodu1")]
        public string PKodu1 { get; set; }

        [DataMember(Name = "il2")]
        public string Il2 { get; set; }

        [DataMember(Name = "ilce2")]
        public string Ilce2 { get; set; }

        [DataMember(Name = "pKodu2")]
        public string PKodu2 { get; set; }

        [DataMember(Name = "sektor")]
        public string Sektor { get; set; }

        [DataMember(Name = "grup")]
        public string Grup { get; set; }

        [DataMember(Name = "iletisimTur1")]
        public string IletisimTur1 { get; set; }

        [DataMember(Name = "iletisim1")]
        public string Iletisim1 { get; set; }

        [DataMember(Name = "iletisimTur2")]
        public string IletisimTur2 { get; set; }

        [DataMember(Name = "iletisim2")]
        public string Iletisim2 { get; set; }

        [DataMember(Name = "aciklama")]
        public string Aciklama { get; set; }

        /// Gets or sets the background color for the contacts inside avatar view.
        /// </summary>
        [DataMember(Name = "backgroundColor")]
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the names selected.
        /// </summary>
        public bool IsSelected { get; set; }

        #endregion
    }

}
