using LawCheckTazminat.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace LawCheckTazminat.Services.DestektenYoksunlukHesapService
{
    public class YasamSuresiService
    {

        List<PmfYapi> PmfTablo = new List<PmfYapi>();
        List<TrhYapi> TRHTablo = new List<TrhYapi>();

        public YasamSuresiService()
        {
            PmfTabloDoldur();
            TrhDoldur();
        }
        public double PmfSureHesapla(int yass)
        {
            double sonuc1=1;
            
            if(yass>0&& yass<85)
            {
                sonuc1 = PmfTablo.Find(o => o.yas == yass).sure;
            }
            else if( yass>84 && yass<90)
            {
                sonuc1 = 3.71;
            }
            else if (yass > 89 && yass < 95)
            {
                sonuc1 = 2.71;
            }
            else if (yass > 94 && yass < 100)
            {
                sonuc1 = 2.40;
            }
            else if (yass > 99 && yass < 105)
            {
                sonuc1 =2;
            }
            else if(yass >104)
             {
                sonuc1 = 1;
            }

            return sonuc1;
        }

        public double TrhSureHesapla(int yass, string cinsiyet2)
        {
            double deger = 1;

            int cinsiyet = 1;
            if(cinsiyet2=="Kadın")
            {
                cinsiyet = 1;
            }

            var kayit = TRHTablo.Find(o => o.yas == yass && o.cinsiyet == cinsiyet);

            if(kayit != null)
            {
                deger = kayit.sure;
            }

            return deger;

        }

        private void PmfTabloDoldur()
        {
            PmfYapi i0 = new PmfYapi();
            i0.yas = 0;
            i0.sure = 56.64;
            PmfTablo.Add(i0);

            PmfYapi i1 = new PmfYapi();
            i1.yas = 1;
            i1.sure = 60.60;
            PmfTablo.Add(i1);

            PmfYapi i2 = new PmfYapi();
            i2.yas = 2;
            i2.sure = 60.58;
            PmfTablo.Add(i2);


            PmfYapi i3 = new PmfYapi();
            i3.yas = 3;
            i3.sure = 59.97;
            PmfTablo.Add(i3);

            PmfYapi i4 = new PmfYapi();
            i4.yas = 4;
            i4.sure = 59.22;
            PmfTablo.Add(i4);

            PmfYapi i5 = new PmfYapi();
            i5.yas = 5;
            i5.sure = 58.41;
            PmfTablo.Add(i5);

            PmfYapi i6 = new PmfYapi();
            i6.yas = 6;
            i6.sure = 57.57;
            PmfTablo.Add(i6);

            PmfYapi i7 = new PmfYapi();
            i7.yas = 7;
            i7.sure = 56.71;
            PmfTablo.Add(i7);

            PmfYapi i8 = new PmfYapi();
            i8.yas = 8;
            i8.sure = 55.83;
            PmfTablo.Add(i8);

            PmfYapi i9 = new PmfYapi();
            i9.yas = 9;
            i9.sure = 54.93;
            PmfTablo.Add(i9);

            PmfYapi i10 = new PmfYapi();
            i10.yas = 10;
            i10.sure = 54.03;
            PmfTablo.Add(i10);

            PmfYapi i11 = new PmfYapi();
            i11.yas = 11;
            i11.sure = 53.11;
            PmfTablo.Add(i11);

            PmfYapi i12 = new PmfYapi();
            i12.yas = 12;
            i12.sure = 52.19;
            PmfTablo.Add(i12);

            PmfYapi i13 = new PmfYapi();
            i13.yas = 13;
            i13.sure = 51.28;
            PmfTablo.Add(i13);

            PmfYapi i14 = new PmfYapi();
            i14.yas = 14;
            i14.sure = 50.37;
            PmfTablo.Add(i14);

            PmfYapi i15 = new PmfYapi();
            i15.yas = 15;
            i15.sure = 49.49;
            PmfTablo.Add(i15);

            PmfYapi i16 = new PmfYapi();
            i16.yas = 16;
            i16.sure = 48.62;
            PmfTablo.Add(i16);

            PmfYapi i17 = new PmfYapi();
            i17.yas = 17;
            i17.sure = 47.78;
            PmfTablo.Add(i17);

            PmfYapi i18 = new PmfYapi();
            i18.yas = 18;
            i18.sure = 46.96;
            PmfTablo.Add(i18);

            PmfYapi i19 = new PmfYapi();
            i19.yas = 19;
            i19.sure = 46.15;
            PmfTablo.Add(i19);

            PmfYapi i20 = new PmfYapi();
            i20.yas = 20;
            i20.sure = 45.90;
            PmfTablo.Add(i20);

            PmfYapi i21 = new PmfYapi();
            i21.yas = 21;
            i21.sure = 44.59;
            PmfTablo.Add(i21);

            PmfYapi i22 = new PmfYapi();
            i22.yas = 22;
            i22.sure = 43.83;
            PmfTablo.Add(i22);

            PmfYapi i23 = new PmfYapi();
            i23.yas = 23;
            i23.sure = 43.03;
            PmfTablo.Add(i23);

            PmfYapi i24 = new PmfYapi();
            i24.yas = 24;
            i24.sure = 42.27;
            PmfTablo.Add(i24);

            PmfYapi i25 = new PmfYapi();
            i25.yas = 25;
            i25.sure = 41.49;
            PmfTablo.Add(i25);

            PmfYapi i26 = new PmfYapi();
            i26.yas = 26;
            i26.sure = 40.70;
            PmfTablo.Add(i26);

            PmfYapi i27 = new PmfYapi();
            i27.yas = 27;
            i27.sure = 39.90;
            PmfTablo.Add(i27);

            PmfYapi i28 = new PmfYapi();
            i28.yas = 28;
            i28.sure = 39.10;
            PmfTablo.Add(i28);

            PmfYapi i29 = new PmfYapi();
            i29.yas = 29;
            i29.sure = 38.32;
            PmfTablo.Add(i29);

            PmfYapi i30 = new PmfYapi();
            i30.yas = 30;
            i30.sure = 37.50;
            PmfTablo.Add(i30);

            PmfYapi i31 = new PmfYapi();
            i31.yas = 31;
            i31.sure = 36.70;
            PmfTablo.Add(i31);

            PmfYapi i32 = new PmfYapi();
            i32.yas = 32;
            i32.sure = 35.90;
            PmfTablo.Add(i32);

            PmfYapi i33 = new PmfYapi();
            i33.yas = 33;
            i33.sure = 35.10;
            PmfTablo.Add(i33);

            PmfYapi i34 = new PmfYapi();
            i34.yas = 34;
            i34.sure = 34.29;
            PmfTablo.Add(i34);

            PmfYapi i35 = new PmfYapi();
            i35.yas = 35;
            i35.sure = 33.49;
            PmfTablo.Add(i35);

            PmfYapi i36 = new PmfYapi();
            i36.yas = 36;
            i36.sure = 32.69;
            PmfTablo.Add(i36);

            PmfYapi i37 = new PmfYapi();
            i37.yas = 37;
            i37.sure = 31.90;
            PmfTablo.Add(i37);

            PmfYapi i38 = new PmfYapi();
            i38.yas = 38;
            i38.sure = 31.10;
            PmfTablo.Add(i38);

            PmfYapi i39 = new PmfYapi();
            i39.yas = 39;
            i39.sure = 30.31;
            PmfTablo.Add(i39);

            PmfYapi i40 = new PmfYapi();
            i40.yas = 40;
            i40.sure = 29.73;
            PmfTablo.Add(i40);

            PmfYapi i41 = new PmfYapi();
            i41.yas = 41;
            i41.sure = 28.73;
            PmfTablo.Add(i41);

            PmfYapi i42 = new PmfYapi();
            i42.yas = 42;
            i42.sure = 27.95;
            PmfTablo.Add(i42);

            PmfYapi i43 = new PmfYapi();
            i43.yas = 43;
            i43.sure = 27.18;
            PmfTablo.Add(i43);

            PmfYapi i44 = new PmfYapi();
            i44.yas = 44;
            i44.sure = 26.40;
            PmfTablo.Add(i44);

            PmfYapi i45 = new PmfYapi();
            i45.yas = 45;
            i45.sure = 25.64;
            PmfTablo.Add(i45);

            PmfYapi i46 = new PmfYapi();
            i46.yas = 46;
            i46.sure = 24.78;
            PmfTablo.Add(i46);

            PmfYapi i47 = new PmfYapi();
            i47.yas = 47;
            i47.sure = 24.12;
            PmfTablo.Add(i47);


            PmfYapi i48 = new PmfYapi();
            i48.yas = 48;
            i48.sure = 23.36;
            PmfTablo.Add(i48);

            PmfYapi i49 = new PmfYapi();
            i49.yas = 49;
            i49.sure = 22.62;
            PmfTablo.Add(i49);

            PmfYapi i50 = new PmfYapi();
            i50.yas = 50;
            i50.sure = 21.88;
            PmfTablo.Add(i50);

            PmfYapi i51 = new PmfYapi();
            i51.yas = 51;
            i51.sure = 21.15;
            PmfTablo.Add(i51);

            PmfYapi i52 = new PmfYapi();
            i52.yas = 52;
            i52.sure = 20.42;
            PmfTablo.Add(i52);

            PmfYapi i53 = new PmfYapi();
            i53.yas = 53;
            i53.sure = 19.70;
            PmfTablo.Add(i53);


            PmfYapi i54 = new PmfYapi();
            i54.yas = 54;
            i54.sure = 18.98;
            PmfTablo.Add(i54);


            PmfYapi i55 = new PmfYapi();
            i55.yas = 55;
            i55.sure = 18.28;
            PmfTablo.Add(i55);

            PmfYapi i56 = new PmfYapi();
            i56.yas = 56;
            i56.sure = 17.82;
            PmfTablo.Add(i56);

            PmfYapi i57 = new PmfYapi();
            i57.yas = 57;
            i57.sure = 16.90;
            PmfTablo.Add(i57);

            PmfYapi i58 = new PmfYapi();
            i58.yas = 58;
            i58.sure = 16.10;
            PmfTablo.Add(i58);

            PmfYapi i59 = new PmfYapi();
            i59.yas = 59;
            i59.sure = 15.55;
            PmfTablo.Add(i59);

            PmfYapi i60 = new PmfYapi();
            i60.yas = 60;
            i60.sure = 14.89;
            PmfTablo.Add(i60);

            PmfYapi i61 = new PmfYapi();
            i61.yas = 61;
            i61.sure = 14.23;
            PmfTablo.Add(i61);

            PmfYapi i62 = new PmfYapi();
            i62.yas = 62;
            i62.sure = 13.59;
            PmfTablo.Add(i62);

            PmfYapi i63 = new PmfYapi();
            i63.yas = 63;
            i63.sure = 12.97;
            PmfTablo.Add(i63);

            PmfYapi i64 = new PmfYapi();
            i64.yas = 64;
            i64.sure = 12.35;
            PmfTablo.Add(i64);

            PmfYapi i65 = new PmfYapi();
            i65.yas = 65;
            i65.sure = 11.75;
            PmfTablo.Add(i65);

            PmfYapi i66 = new PmfYapi();
            i66.yas = 66;
            i66.sure = 11.17;
            PmfTablo.Add(i66);

            PmfYapi i67 = new PmfYapi();
            i67.yas = 67;
            i67.sure = 10.51;
            PmfTablo.Add(i67);

            PmfYapi i68 = new PmfYapi();
            i68.yas = 68;
            i68.sure = 10.05;
            PmfTablo.Add(i68);

            PmfYapi i69 = new PmfYapi();
            i69.yas = 69;
            i69.sure = 9.50;
            PmfTablo.Add(i69);

            PmfYapi i70 = new PmfYapi();
            i70.yas = 70;
            i70.sure = 8.98;
            PmfTablo.Add(i70);

            PmfYapi i71 = new PmfYapi();
            i71.yas = 71;
            i71.sure = 8.47;
            PmfTablo.Add(i71);

            PmfYapi i72 = new PmfYapi();
            i72.yas = 72;
            i72.sure = 7.98;
            PmfTablo.Add(i72);

            PmfYapi i73 = new PmfYapi();
            i73.yas = 73;
            i73.sure = 7.54;
            PmfTablo.Add(i73);

            PmfYapi i74 = new PmfYapi();
            i74.yas = 74;
            i74.sure = 7.08;
            PmfTablo.Add(i74);

            PmfYapi i75 = new PmfYapi();
            i75.yas = 75;
            i75.sure = 6.88;
            PmfTablo.Add(i75);

            PmfYapi i76 = new PmfYapi();
            i76.yas = 76;
            i76.sure = 6.25;
            PmfTablo.Add(i76);

            PmfYapi i77 = new PmfYapi();
            i77.yas = 77;
            i77.sure = 5.86;
            PmfTablo.Add(i77);

            PmfYapi i78 = new PmfYapi();
            i78.yas = 78;
            i78.sure = 5.50;
            PmfTablo.Add(i78);

            PmfYapi i79 = new PmfYapi();
            i79.yas = 79;
            i79.sure = 5.15;
            PmfTablo.Add(i79);

            PmfYapi i80 = new PmfYapi();
            i80.yas = 80;
            i80.sure = 4.85;
            PmfTablo.Add(i80);

            PmfYapi i81 = new PmfYapi();
            i81.yas = 81;
            i81.sure = 4.52;
            PmfTablo.Add(i81);

            PmfYapi i82 = new PmfYapi();
            i82.yas = 82;
            i82.sure = 4.22;
            PmfTablo.Add(i82);

            PmfYapi i83 = new PmfYapi();
            i83.yas = 83;
            i83.sure = 3.95;
            PmfTablo.Add(i83);

            PmfYapi i84 = new PmfYapi();
            i84.yas = 84;
            i84.sure = 3.71;
            PmfTablo.Add(i84);

            PmfYapi i90 = new PmfYapi();
            i90.yas = 90;
            i90.sure = 2.71;
            PmfTablo.Add(i90);

            PmfYapi i95 = new PmfYapi();
            i95.yas = 95;
            i95.sure = 2.40;
            PmfTablo.Add(i95);

            PmfYapi i100 = new PmfYapi();
            i100.yas = 95;
            i100.sure = 2.00;
            PmfTablo.Add(i100);

            PmfYapi i105 = new PmfYapi();
            i105.yas = 95;
            i105.sure = 1.00;
            PmfTablo.Add(i105);


        }


        private void TrhDoldur()
        {
            TrhYapi t0 = new TrhYapi();
            t0.yas = 0;
            t0.sure = 71.93;
            t0.cinsiyet = 1;
            TRHTablo.Add(t0);

            TrhYapi t0B = new TrhYapi();
            t0B.yas = 0;
            t0B.sure = 78.02;
            t0B.cinsiyet = 2;
            TRHTablo.Add(t0B);

            TrhYapi t1 = new TrhYapi();
            t1.yas = 1;
            t1.sure = 72.35;
            t1.cinsiyet = 1;
            TRHTablo.Add(t1);

            TrhYapi t1B = new TrhYapi();
            t1B.yas = 1;
            t1B.sure = 77.66;
            t1B.cinsiyet = 2;
            TRHTablo.Add(t1B);

            TrhYapi t2 = new TrhYapi();
            t2.yas = 2;
            t2.sure = 71.42;
            t2.cinsiyet = 1;
            TRHTablo.Add(t2);

            TrhYapi t2B = new TrhYapi();
            t2B.yas = 2;
            t2B.sure = 76.68;
            t2B.cinsiyet = 2;
            TRHTablo.Add(t2B);

            TrhYapi t3 = new TrhYapi();
            t3.yas = 3;
            t3.sure = 70.47;
            t3.cinsiyet = 1;
            TRHTablo.Add(t3);

            TrhYapi t3B = new TrhYapi();
            t3B.yas = 3;
            t3B.sure = 75.70;
            t3B.cinsiyet = 2;
            TRHTablo.Add(t3B);

            TrhYapi t4 = new TrhYapi();
            t4.yas = 4;
            t4.sure = 69.52;
            t4.cinsiyet = 1;
            TRHTablo.Add(t4);

            TrhYapi t4B = new TrhYapi();
            t4B.yas = 4;
            t4B.sure = 74.72;
            t4B.cinsiyet = 2;
            TRHTablo.Add(t4B);

            TrhYapi t5 = new TrhYapi();
            t5.yas = 5;
            t5.sure = 68.57;
            t5.cinsiyet = 1;
            TRHTablo.Add(t5);

            TrhYapi t5B = new TrhYapi();
            t5B.yas = 5;
            t5B.sure = 73.73;
            t5B.cinsiyet = 2;
            TRHTablo.Add(t5B);

            TrhYapi t6 = new TrhYapi();
            t6.yas = 6;
            t6.sure = 67.60;
            t6.cinsiyet = 1;
            TRHTablo.Add(t6);

            TrhYapi t6B = new TrhYapi();
            t6B.yas = 6;
            t6B.sure = 72.74;
            t6B.cinsiyet = 2;
            TRHTablo.Add(t6B);

            TrhYapi t7 = new TrhYapi();
            t7.yas = 7;
            t7.sure = 66.63;
            t7.cinsiyet = 1;
            TRHTablo.Add(t7);

            TrhYapi t7B = new TrhYapi();
            t7B.yas = 7;
            t7B.sure = 71.75;
            t7B.cinsiyet = 2;
            TRHTablo.Add(t7B);

            TrhYapi t8 = new TrhYapi();
            t8.yas = 8;
            t8.sure = 65.66;
            t8.cinsiyet = 1;
            TRHTablo.Add(t8);

            TrhYapi t8B = new TrhYapi();
            t8B.yas = 8;
            t8B.sure = 70.76;
            t8B.cinsiyet = 2;
            TRHTablo.Add(t8B);

            TrhYapi  t9= new TrhYapi();
            t9.yas = 9;
            t9.sure = 64.68;
            t9.cinsiyet = 1;
            TRHTablo.Add(t9);

            TrhYapi t9B = new TrhYapi();
            t9B.yas = 9;
            t9B.sure = 69.76;
            t9B.cinsiyet = 2;
            TRHTablo.Add(t9B);

            TrhYapi t10 = new TrhYapi();
            t10.yas = 10;
            t10.sure = 63.70;
            t10.cinsiyet = 1;
            TRHTablo.Add(t10);

            TrhYapi t10B = new TrhYapi();
            t10B.yas = 10;
            t10B.sure = 68.77;
            t10B.cinsiyet = 2;
            TRHTablo.Add(t10B);

            TrhYapi t11 = new TrhYapi();
            t11.yas = 11;
            t11.sure = 62.72;
            t11.cinsiyet = 1;
            TRHTablo.Add(t11);

            TrhYapi t11B = new TrhYapi();
            t11B.yas = 11;
            t11B.sure = 67.78;
            t11B.cinsiyet = 2;
            TRHTablo.Add(t11B);

            TrhYapi t12 = new TrhYapi();
            t12.yas = 12;
            t12.sure = 61.74;
            t12.cinsiyet = 1;
            TRHTablo.Add(t12);

            TrhYapi t12B = new TrhYapi();
            t12B.yas = 12;
            t12B.sure = 66.78;
            t12B.cinsiyet = 2;
            TRHTablo.Add(t12B);

            TrhYapi t13 = new TrhYapi();
            t13.yas = 13;
            t13.sure = 60.76;
            t13.cinsiyet = 1;
            TRHTablo.Add(t13);

            TrhYapi t13B = new TrhYapi();
            t13B.yas = 13;
            t13B.sure = 65.79;
            t13.cinsiyet = 2;
            TRHTablo.Add(t13B);

            TrhYapi t14 = new TrhYapi();
            t14.yas = 14;
            t14.sure= 59.78;
            t14.cinsiyet = 1;
            TRHTablo.Add(t14);

            TrhYapi t14B = new TrhYapi();
            t14B.yas = 14;
            t14B.sure = 64.79;
            t14B.cinsiyet = 2;
            TRHTablo.Add(t14B);

            TrhYapi t15 = new TrhYapi();
            t15.yas = 15;
            t15.sure = 58.80;
            t15.cinsiyet = 1;
            TRHTablo.Add(t15);

            TrhYapi t15B = new TrhYapi();
            t15B.yas = 15;
            t15B.sure = 63.80;
            t15B.cinsiyet = 2;
            TRHTablo.Add(t15B);


            TrhYapi t16 = new TrhYapi();
            t16.yas = 16;
            t16.sure = 57.84;
            t16.cinsiyet = 1;
            TRHTablo.Add(t16);

            TrhYapi t16B = new TrhYapi();
            t16B.yas = 16;
            t16B.sure = 62.81;
            t16B.cinsiyet = 2;
            TRHTablo.Add(t16B);

            TrhYapi t17 = new TrhYapi();
            t17.yas = 17;
            t17.sure = 56.87;
            t17.cinsiyet = 1;
            TRHTablo.Add(t17);

            TrhYapi t17B = new TrhYapi();
            t17B.yas = 17;
            t17B.sure = 61.82;
            t17B.cinsiyet = 2;
            TRHTablo.Add(t17B);


            TrhYapi t18 = new TrhYapi();
            t18.yas = 18;
            t18.sure = 55.91;
            t18.cinsiyet = 1;
            TRHTablo.Add(t18);

            TrhYapi t18B = new TrhYapi();
            t18B.yas = 18;
            t18B.sure = 60.83;
            t18B.cinsiyet = 2;
            TRHTablo.Add(t18B);

            TrhYapi t19 = new TrhYapi();
            t19.yas = 19;
            t19.sure = 54.95;
            t19.cinsiyet = 1;
            TRHTablo.Add(t19);

            TrhYapi t19B = new TrhYapi();
            t19B.yas = 19;
            t19B.sure = 59.84;
            t19B.cinsiyet = 2;
            TRHTablo.Add(t19B);

            TrhYapi t20 = new TrhYapi();
            t20.yas = 20;
            t20.sure = 53.99;
            t20.cinsiyet = 1;
            TRHTablo.Add(t20);

            TrhYapi t20B = new TrhYapi();
            t20B.yas = 20;
            t20B.sure = 58.85;
            t20B.cinsiyet = 2;
            TRHTablo.Add(t20B);

            TrhYapi t21 = new TrhYapi();
            t21.yas = 21;
            t21.sure = 53.04;
            t21.cinsiyet = 1;
            TRHTablo.Add(t21);

            TrhYapi t21B = new TrhYapi();
            t21B.yas = 21;
            t21B.sure = 57.86;
            t21B.cinsiyet = 2;
            TRHTablo.Add(t21B);

            TrhYapi t22 = new TrhYapi();
            t22.yas = 22;
            t22.sure = 52.09;
            t22.cinsiyet = 1;
            TRHTablo.Add(t22);

            TrhYapi t22B = new TrhYapi();
            t22B.yas = 22;
            t22B.sure = 56.88;
            t22B.cinsiyet = 2;
            TRHTablo.Add(t22B);

            TrhYapi t23 = new TrhYapi();
            t23.yas = 23;
            t23.sure = 51.14;
            t23.cinsiyet = 1;
            TRHTablo.Add(t23);

            TrhYapi t23B = new TrhYapi();
            t23B.yas = 23;
            t23B.sure = 55.89;
            t23B.cinsiyet = 2;
            TRHTablo.Add(t23B);

            TrhYapi t24 = new TrhYapi();
            t24.yas = 24;
            t24.sure = 50.19;
            t24.cinsiyet = 1;
            TRHTablo.Add(t24);

            TrhYapi t24B = new TrhYapi();
            t24B.yas = 24;
            t24B.sure = 54.90;
            t24B.cinsiyet = 2;
            TRHTablo.Add(t24B);

            TrhYapi t25 = new TrhYapi();
            t25.yas = 25;
            t25.sure = 49.24;
            t25.cinsiyet = 1;
            TRHTablo.Add(t25);

            TrhYapi t25B = new TrhYapi();
            t25B.yas = 25;
            t25B.sure = 53.92;
            t25B.cinsiyet = 2;
            TRHTablo.Add(t25B);

            TrhYapi t26 = new TrhYapi();
            t26.yas = 26;
            t26.sure = 48.28;
            t26.cinsiyet = 1;
            TRHTablo.Add(t26);

            TrhYapi t26B = new TrhYapi();
            t26B.yas = 26;
            t26B.sure = 52.93;
            t26B.cinsiyet = 2;
            TRHTablo.Add(t26B);

            TrhYapi t27 = new TrhYapi();
            t27.yas = 27;
            t27.sure = 47.33;
            t27.cinsiyet = 1;
            TRHTablo.Add(t27);

            TrhYapi t27B = new TrhYapi();
            t27B.yas = 27;
            t27B.sure = 51.95;
            t27B.cinsiyet = 2;
            TRHTablo.Add(t27B);

            TrhYapi t28 = new TrhYapi();
            t28.yas = 28;
            t28.sure = 46.37;
            t28.cinsiyet = 1;
            TRHTablo.Add(t28);

            TrhYapi t28B = new TrhYapi();
            t28B.yas = 28;
            t28B.sure = 50.97;
            t28B.cinsiyet = 2;
            TRHTablo.Add(t28B);

            TrhYapi t29 = new TrhYapi();
            t29.yas = 29;
            t29.sure = 45.41;
            t29.cinsiyet = 1;
            TRHTablo.Add(t29);

            TrhYapi t29B = new TrhYapi();
            t29B.yas = 29;
            t29B.sure = 49.98;
            t29B.cinsiyet = 2;
            TRHTablo.Add(t29B);

            TrhYapi t30 = new TrhYapi();
            t30.yas = 30;
            t30.sure = 44.45;
            t30.cinsiyet = 1;
            TRHTablo.Add(t30);

            TrhYapi t30B = new TrhYapi();
            t30B.yas = 30;
            t30B.sure = 49.00;
            t30B.cinsiyet = 2;
            TRHTablo.Add(t30B);

            TrhYapi t31 = new TrhYapi();
            t31.yas = 31;
            t31.sure = 43.50;
            t31.cinsiyet = 1;
            TRHTablo.Add(t31);

            TrhYapi t31B = new TrhYapi();
            t31B.yas = 31;
            t31B.sure = 48.02;
            t31B.cinsiyet = 2;
            TRHTablo.Add(t31B);

            TrhYapi t32 = new TrhYapi();
            t32.yas = 32;
            t32.sure = 42.54;
            t32.cinsiyet = 1;
            TRHTablo.Add(t32);

            TrhYapi t32B = new TrhYapi();
            t32B.yas = 32;
            t32B.sure = 47.4;
            t32B.cinsiyet = 2;
            TRHTablo.Add(t32B);

            TrhYapi t33 = new TrhYapi();
            t33.yas = 33;
            t33.sure = 41.58;
            t33.cinsiyet = 1;
            TRHTablo.Add(t33);

            TrhYapi t33B = new TrhYapi();
            t33B.yas = 33;
            t33B.sure = 46.06;
            t33B.cinsiyet = 2;
            TRHTablo.Add(t33B);

            TrhYapi t34 = new TrhYapi();
            t34.yas = 34;
            t34.sure = 40.62;
            t34.cinsiyet = 1;
            TRHTablo.Add(t34);

            TrhYapi t34B = new TrhYapi();
            t34B.yas = 34;
            t34B.sure = 45.08;
            t34B.cinsiyet = 2;
            TRHTablo.Add(t34B);

            TrhYapi t35 = new TrhYapi();
            t35.yas = 35;
            t35.sure = 39.67;
            t35.cinsiyet = 1;
            TRHTablo.Add(t35);

            TrhYapi t35B = new TrhYapi();
            t35B.yas = 35;
            t35B.sure = 44.10;
            t35B.cinsiyet = 2;
            TRHTablo.Add(t35B);

            TrhYapi t36 = new TrhYapi();
            t36.yas = 36;
            t36.sure = 38.72;
            t36.cinsiyet = 1;
            TRHTablo.Add(t36);

            TrhYapi t36B = new TrhYapi();
            t36B.yas = 36;
            t36B.sure = 43.12;
            t36B.cinsiyet = 2;
            TRHTablo.Add(t36B);

            TrhYapi t37 = new TrhYapi();
            t37.yas = 37;
            t37.sure = 37.77;
            t37.cinsiyet = 1;
            TRHTablo.Add(t37);

            TrhYapi t37B = new TrhYapi();
            t37B.yas = 37;
            t37B.sure = 42.15;
            t37B.cinsiyet = 2;
            TRHTablo.Add(t37B);

            TrhYapi t38 = new TrhYapi();
            t38.yas = 38;
            t38.sure = 36.81;
            t38.cinsiyet = 1;
            TRHTablo.Add(t38);

            TrhYapi t38B = new TrhYapi();
            t38B.yas = 38;
            t38B.sure = 41.17;
            t38B.cinsiyet = 2;
            TRHTablo.Add(t38B);

            TrhYapi t39 = new TrhYapi();
            t39.yas = 39;
            t39.sure = 35.87;
            t39.cinsiyet = 1;
            TRHTablo.Add(t39);

            TrhYapi t39B = new TrhYapi();
            t39B.yas = 39;
            t39B.sure = 40.20;
            t39B.cinsiyet = 2;
            TRHTablo.Add(t39B);

            TrhYapi t40 = new TrhYapi();
            t40.yas = 40;
            t40.sure = 34.93;
            t40.cinsiyet = 1;
            TRHTablo.Add(t40);

            TrhYapi t40B = new TrhYapi();
            t40B.yas = 40;
            t40B.sure = 39.23;
            t40B.cinsiyet = 2;
            TRHTablo.Add(t40B);

            TrhYapi t41 = new TrhYapi();
            t41.yas = 41;
            t41.sure = 33.99;
            t41.cinsiyet = 1;
            TRHTablo.Add(t41);

            TrhYapi t41B = new TrhYapi();
            t41B.yas = 41;
            t41B.sure = 38.26;
            t41B.cinsiyet = 2;
            TRHTablo.Add(t41B);

            TrhYapi t42 = new TrhYapi();
            t42.yas = 42;
            t42.sure = 33.05;
            t42.cinsiyet = 1;
            TRHTablo.Add(t42);

            TrhYapi t42B = new TrhYapi();
            t42B.yas = 42;
            t42B.sure = 37.30;
            t42B.cinsiyet = 2;
            TRHTablo.Add(t42B);

            TrhYapi t43 = new TrhYapi();
            t43.yas = 43;
            t43.sure = 32.12;
            t43.cinsiyet = 1;
            TRHTablo.Add(t43);

            TrhYapi t43B = new TrhYapi();
            t43B.yas = 43;
            t43B.sure = 36.34;
            t43B.cinsiyet = 2;
            TRHTablo.Add(t43B);

            TrhYapi t44 = new TrhYapi();
            t44.yas = 44;
            t44.sure = 31.19;
            t44.cinsiyet = 1;
            TRHTablo.Add(t44);

            TrhYapi t44B = new TrhYapi();
            t44B.yas = 44;
            t44B.sure = 35.38;
            t44B.cinsiyet = 2;
            TRHTablo.Add(t44B);


            TrhYapi t45 = new TrhYapi();
            t45.yas = 45;
            t45.sure = 30.27;
            t45.cinsiyet = 1;
            TRHTablo.Add(t45);

            TrhYapi t45B = new TrhYapi();
            t45B.yas = 45;
            t45B.sure = 34.43;
            t45B.cinsiyet = 2;
            TRHTablo.Add(t45B);

            TrhYapi t46 = new TrhYapi();
            t46.yas = 46;
            t46.sure = 29.36;
            t46.cinsiyet = 1;
            TRHTablo.Add(t46);

            TrhYapi t46B = new TrhYapi();
            t46B.yas = 46;
            t46B.sure = 33.48;
            t46B.cinsiyet = 2;
            TRHTablo.Add(t46B);


            TrhYapi t47 = new TrhYapi();
            t47.yas = 47;
            t47.sure = 28.46;
            t47.cinsiyet = 1;
            TRHTablo.Add(t47);

            TrhYapi t47B = new TrhYapi();
            t47B.yas = 47;
            t47B.sure = 32.54;
            t47B.cinsiyet = 2;
            TRHTablo.Add(t47B);

            TrhYapi t48 = new TrhYapi();
            t48.yas = 48;
            t48.sure = 27.56;
            t48.cinsiyet = 1;
            TRHTablo.Add(t48);

            TrhYapi t48B = new TrhYapi();
            t48B.yas = 48;
            t48B.sure = 31.60;
            t48B.cinsiyet = 2;
            TRHTablo.Add(t48B);

            TrhYapi t49 = new TrhYapi();
            t49.yas = 49;
            t49.sure = 26.67;
            t49.cinsiyet = 1;
            TRHTablo.Add(t49);

            TrhYapi t49B = new TrhYapi();
            t49B.yas = 49;
            t49B.sure = 30.67;
            t49B.cinsiyet = 2;
            TRHTablo.Add(t49B);

            TrhYapi t50 = new TrhYapi();
            t50.yas = 50;
            t50.sure = 25.79;
            t50.cinsiyet = 1;
            TRHTablo.Add(t50);

            TrhYapi t50B = new TrhYapi();
            t50B.yas = 50;
            t50B.sure = 29.74;
            t50B.cinsiyet = 2;
            TRHTablo.Add(t50B);

            TrhYapi t51 = new TrhYapi();
            t51.yas = 51;
            t51.sure = 24.93;
            t51.cinsiyet = 1;
            TRHTablo.Add(t51);

            TrhYapi t51B = new TrhYapi();
            t51B.yas = 51;
            t51B.sure = 28.82;
            t51B.cinsiyet = 2;
            TRHTablo.Add(t51B);

            TrhYapi t52 = new TrhYapi();
            t52.yas = 52;
            t52.sure = 24.06;
            t52.cinsiyet = 1;
            TRHTablo.Add(t52);

            TrhYapi t52B = new TrhYapi();
            t52B.yas = 52;
            t52B.sure = 27.90;
            t52B.cinsiyet = 2;
            TRHTablo.Add(t52B);

            TrhYapi t53 = new TrhYapi();
            t53.yas = 53;
            t53.sure = 23.21;
            t53.cinsiyet = 1;
            TRHTablo.Add(t53);

            TrhYapi t53B = new TrhYapi();
            t53B.yas = 53;
            t53B.sure = 26.98;
            t53B.cinsiyet = 2;
            TRHTablo.Add(t53B);

            TrhYapi t54 = new TrhYapi();
            t54.yas = 54;
            t54.sure = 22.37;
            t54.cinsiyet = 1;
            TRHTablo.Add(t54);

            TrhYapi t54B = new TrhYapi();
            t54B.yas = 54;
            t54B.sure = 26.08;
            t54B.cinsiyet = 2;
            TRHTablo.Add(t54B);

            TrhYapi t55 = new TrhYapi();
            t55.yas = 55;
            t55.sure = 21.54;
            t55.cinsiyet = 1;                                                               
            TRHTablo.Add(t55);

            TrhYapi t55B = new TrhYapi();
            t55B.yas = 55;
            t55B.sure = 25.18;
            t55B.cinsiyet = 2;
            TRHTablo.Add(t55B);

            TrhYapi t56 = new TrhYapi();
            t56.yas = 56;
            t56.sure = 20.74;
            t56.cinsiyet = 1;
            TRHTablo.Add(t56);

            TrhYapi t56B = new TrhYapi();
            t56B.yas = 56;
            t56B.sure = 24.29;
            t56B.cinsiyet = 2;
            TRHTablo.Add(t56B);

            TrhYapi t57 = new TrhYapi();
            t57.yas = 57;
            t57.sure = 19.94;
            t57.cinsiyet = 1;
            TRHTablo.Add(t57);

            TrhYapi t57B = new TrhYapi();
            t57B.yas = 57;
            t57B.sure = 23.40;
            t57B.cinsiyet = 2;
            TRHTablo.Add(t57B);

            TrhYapi t58 = new TrhYapi();
            t58.yas = 58;
            t58.sure = 19.15;
            t58.cinsiyet = 1;
            TRHTablo.Add(t58);

            TrhYapi t58B = new TrhYapi();
            t58B.yas = 58;
            t58B.sure = 22.52;
            t58B.cinsiyet = 2;
            TRHTablo.Add(t58B);

            TrhYapi t59 = new TrhYapi();
            t59.yas = 59;
            t59.sure = 18.38;
            t59.cinsiyet = 1;
            TRHTablo.Add(t59);

            TrhYapi t59B = new TrhYapi();
            t59B.yas = 59;
            t59B.sure = 21.65;
            t59B.cinsiyet = 2;
            TRHTablo.Add(t59B);

            TrhYapi t60 = new TrhYapi();
            t60.yas = 60;
            t60.sure = 17.62;
            t60.cinsiyet = 1;
            TRHTablo.Add(t60);

            TrhYapi t60B = new TrhYapi();
            t60B.yas = 60;
            t60B.sure = 20.79;
            t60B.cinsiyet = 2;
            TRHTablo.Add(t60B);

            TrhYapi t61 = new TrhYapi();
            t61.yas = 61;
            t61.sure = 16.88;
            t61.cinsiyet = 1;
            TRHTablo.Add(t61);

            TrhYapi t61B = new TrhYapi();
            t61B.yas = 61;
            t61B.sure = 19.94;
            t61B.cinsiyet = 2;
            TRHTablo.Add(t61B);

            TrhYapi t62 = new TrhYapi();
            t62.yas = 62;
            t62.sure = 16.14;
            t62.cinsiyet = 1;
            TRHTablo.Add(t62);

            TrhYapi t62B = new TrhYapi();
            t62B.yas = 62;
            t62B.sure = 19.09;
            t62B.cinsiyet = 2;
            TRHTablo.Add(t62B);


            TrhYapi t63 = new TrhYapi();
            t63.yas = 63;
            t63.sure = 15.42;
            t63.cinsiyet = 1;
            TRHTablo.Add(t63);

            TrhYapi t63B = new TrhYapi();
            t63B.yas = 63;
            t63B.sure = 18.26;
            t63B.cinsiyet = 2;
            TRHTablo.Add(t63B);


            TrhYapi t64 = new TrhYapi();
            t64.yas = 64;
            t64.sure = 14.72;
            t64.cinsiyet = 1;
            TRHTablo.Add(t64);

            TrhYapi t64B = new TrhYapi();
            t64B.yas = 64;
            t64B.sure = 17.43;
            t64B.cinsiyet = 2;
            TRHTablo.Add(t64B);

            TrhYapi t65 = new TrhYapi();
            t65.yas = 65;
            t65.sure = 14.04;
            t65.cinsiyet = 1;
            TRHTablo.Add(t65);

            TrhYapi t65B = new TrhYapi();
            t65B.yas = 65;
            t65B.sure = 16.63;
            t65B.cinsiyet = 2;
            TRHTablo.Add(t65B);

            TrhYapi t66 = new TrhYapi();
            t66.yas = 66;
            t66.sure = 13.37;
            t66.cinsiyet = 1;
            TRHTablo.Add(t66);

            TrhYapi t66B = new TrhYapi();
            t66B.yas = 66;
            t66B.sure = 15.85;
            t66B.cinsiyet = 2;
            TRHTablo.Add(t66B);

            TrhYapi t67 = new TrhYapi();
            t67.yas = 67;
            t67.sure = 12.72;
            t67.cinsiyet = 1;
            TRHTablo.Add(t67);

            TrhYapi t67B = new TrhYapi();
            t67B.yas = 30;
            t67B.sure = 15.08;
            t67B.cinsiyet = 2;
            TRHTablo.Add(t67B);

            TrhYapi t68 = new TrhYapi();
            t68.yas = 68;
            t68.sure = 12.08;
            t68.cinsiyet = 1;
            TRHTablo.Add(t68);

            TrhYapi t68B = new TrhYapi();
            t68B.yas = 30;
            t68B.sure = 14.32;
            t68B.cinsiyet = 2;
            TRHTablo.Add(t68B);

            TrhYapi t69 = new TrhYapi();
            t69.yas = 69;
            t69.sure = 11.47;
            t69.cinsiyet = 1;
            TRHTablo.Add(t69);

            TrhYapi t69B = new TrhYapi();
            t69B.yas = 69;
            t69B.sure = 13.58;
            t69B.cinsiyet = 2;
            TRHTablo.Add(t69B);

            TrhYapi t70 = new TrhYapi();
            t70.yas = 70;
            t70.sure = 10.87;
            t70.cinsiyet = 1;
            TRHTablo.Add(t70);

            TrhYapi t70B = new TrhYapi();
            t70B.yas = 70;
            t70B.sure = 10.29;
            t70B.cinsiyet = 2;
            TRHTablo.Add(t70B);

            TrhYapi t71 = new TrhYapi();
            t71.yas = 71;
            t71.sure = 10.29;
            t71.cinsiyet = 1;
            TRHTablo.Add(t71);

            TrhYapi t71B = new TrhYapi();
            t71B.yas = 71;
            t71B.sure = 12.18;
            t71B.cinsiyet = 2;
            TRHTablo.Add(t71B);

            TrhYapi t72 = new TrhYapi();
            t72.yas = 72;
            t72.sure = 9.73;
            t72.cinsiyet = 1;
            TRHTablo.Add(t72);

            TrhYapi t72B = new TrhYapi();
            t72B.yas = 72;
            t72B.sure = 11.51;
            t72B.cinsiyet = 2;
            TRHTablo.Add(t72B);

            TrhYapi t73 = new TrhYapi();
            t73.yas = 73;
            t73.sure = 9.20;
            t73.cinsiyet = 1;
            TRHTablo.Add(t73);

            TrhYapi t73B = new TrhYapi();
            t73B.yas = 73;
            t73B.sure = 10.85;
            t73B.cinsiyet = 2;
            TRHTablo.Add(t73B);


            TrhYapi t74 = new TrhYapi();
            t74.yas = 74;
            t74.sure = 8.68;
            t74.cinsiyet = 1;
            TRHTablo.Add(t74);

            TrhYapi t74B = new TrhYapi();
            t74B.yas = 74;
            t74B.sure = 10.22;
            t74B.cinsiyet = 2;
            TRHTablo.Add(t74B);

            TrhYapi t75 = new TrhYapi();
            t75.yas = 75;
            t75.sure = 8.17;
            t75.cinsiyet = 1;
            TRHTablo.Add(t75);

            TrhYapi t75B = new TrhYapi();
            t75B.yas = 75;
            t75B.sure = 9.62;
            t75B.cinsiyet = 2;
            TRHTablo.Add(t75B);

            TrhYapi t76 = new TrhYapi();
            t76.yas = 76;
            t76.sure = 7.69;
            t76.cinsiyet = 1;
            TRHTablo.Add(t76);

            TrhYapi t76B = new TrhYapi();
            t76B.yas = 76;
            t76B.sure = 9.05;
            t76B.cinsiyet = 2;
            TRHTablo.Add(t76B);

            TrhYapi t77 = new TrhYapi();
            t77.yas = 77;
            t77.sure = 7.24;
            t77.cinsiyet = 1;
            TRHTablo.Add(t77);

            TrhYapi t77B = new TrhYapi();
            t77B.yas = 77;
            t77B.sure = 8.51;
            t77B.cinsiyet = 2;
            TRHTablo.Add(t77B);

            TrhYapi t78 = new TrhYapi();
            t78.yas = 78;
            t78.sure = 6.81;
            t78.cinsiyet = 1;
            TRHTablo.Add(t78);

            TrhYapi t78B = new TrhYapi();
            t78B.yas = 78;
            t78B.sure = 8.00;
            t78B.cinsiyet = 2;
            TRHTablo.Add(t78B);


            TrhYapi t79 = new TrhYapi();
            t79.yas = 79;
            t79.sure = 6.40;
            t79.cinsiyet = 1;
            TRHTablo.Add(t79);

            TrhYapi t79B = new TrhYapi();
            t79B.yas = 79;
            t79B.sure = 7.50;
            t79B.cinsiyet = 2;
            TRHTablo.Add(t79B);


            TrhYapi t80 = new TrhYapi();
            t80.yas = 80;
            t80.sure = 5.99;
            t80.cinsiyet = 1;
            TRHTablo.Add(t80);

            TrhYapi t80B = new TrhYapi();
            t80B.yas = 80;
            t80B.sure = 7.01;
            t80B.cinsiyet = 2;
            TRHTablo.Add(t80B);

            TrhYapi t81 = new TrhYapi();
            t81.yas = 81;
            t81.sure = 5.59;
            t81.cinsiyet = 1;
            TRHTablo.Add(t81);

            TrhYapi t81B = new TrhYapi();
            t81B.yas = 81;
            t81B.sure = 6.55;
            t81B.cinsiyet = 2;
            TRHTablo.Add(t81B);


            TrhYapi t82 = new TrhYapi();
            t82.yas = 82;
            t82.sure = 5.23;
            t82.cinsiyet = 1;
            TRHTablo.Add(t82);

            TrhYapi t82B = new TrhYapi();
            t82B.yas = 82;
            t82B.sure = 6.12;
            t82B.cinsiyet = 2;
            TRHTablo.Add(t82B);


            TrhYapi t83 = new TrhYapi();
            t83.yas = 83;
            t83.sure = 4.90;
            t83.cinsiyet = 1;
            TRHTablo.Add(t83);

            TrhYapi t83B = new TrhYapi();
            t83B.yas = 83;
            t83B.sure = 5.71;
            t83B.cinsiyet = 2;
            TRHTablo.Add(t83B);

            TrhYapi t84 = new TrhYapi();
            t84.yas = 84;
            t84.sure = 4.57;
            t84.cinsiyet = 1;
            TRHTablo.Add(t84);

            TrhYapi t84B = new TrhYapi();
            t84B.yas = 84;
            t84B.sure = 5.32;
            t84B.cinsiyet = 2;
            TRHTablo.Add(t84B);

            TrhYapi t85 = new TrhYapi();
            t85.yas = 85;
            t85.sure = 4.25;
            t85.cinsiyet = 1;
            TRHTablo.Add(t85);

            TrhYapi t85B = new TrhYapi();
            t85B.yas = 85;
            t85B.sure = 4.93;
            t85B.cinsiyet = 2;
            TRHTablo.Add(t85B);

            TrhYapi t86 = new TrhYapi();
            t86.yas = 86;
            t86.sure = 3.93;
            t86.cinsiyet = 1;
            TRHTablo.Add(t86);

            TrhYapi t86B = new TrhYapi();
            t86B.yas = 86;
            t86B.sure = 4.54;
            t86B.cinsiyet = 2;
            TRHTablo.Add(t86B);

            TrhYapi t87 = new TrhYapi();
            t87.yas = 87;
            t87.sure = 3.64;
            t87.cinsiyet = 1;
            TRHTablo.Add(t87);

            TrhYapi t87B = new TrhYapi();
            t87B.yas = 87;
            t87B.sure = 4.20;
            t87B.cinsiyet = 2;
            TRHTablo.Add(t87B);

            TrhYapi t88 = new TrhYapi();
            t88.yas = 88;
            t88.sure = 3.37;
            t88.cinsiyet = 1;
            TRHTablo.Add(t88);

            TrhYapi t88B = new TrhYapi();
            t88B.yas = 88;
            t88B.sure = 3.88;
            t88B.cinsiyet = 2;
            TRHTablo.Add(t88B);

            TrhYapi t89 = new TrhYapi();
            t89.yas = 89;
            t89.sure = 3.12;
            t89.cinsiyet = 1;
            TRHTablo.Add(t89);

            TrhYapi t89B = new TrhYapi();
            t89B.yas = 89;
            t89B.sure = 3.59;
            t89B.cinsiyet = 2;
            TRHTablo.Add(t89B);


            TrhYapi t90 = new TrhYapi();
            t90.yas = 90;
            t90.sure = 2.90;
            t90.cinsiyet = 1;
            TRHTablo.Add(t90);

            TrhYapi t90B = new TrhYapi();
            t90B.yas = 90;
            t90B.sure = 3.29;
            t90B.cinsiyet = 2;
            TRHTablo.Add(t90B);

            TrhYapi t91 = new TrhYapi();
            t91.yas = 91;
            t91.sure = 2.66;
            t91.cinsiyet = 1;
            TRHTablo.Add(t91);

            TrhYapi t91B = new TrhYapi();
            t91B.yas = 91;
            t91B.sure = 2.97;
            t91B.cinsiyet = 2;
            TRHTablo.Add(t91B);

            TrhYapi t92 = new TrhYapi();
            t92.yas = 92;
            t92.sure = 2.39;
            t92.cinsiyet = 1;
            TRHTablo.Add(t92);

            TrhYapi t92B = new TrhYapi();
            t92B.yas = 92;
            t92B.sure = 2.64;
            t92B.cinsiyet = 2;
            TRHTablo.Add(t92B);

            TrhYapi t93 = new TrhYapi();
            t93.yas = 93;
            t93.sure = 2.10;
            t93.cinsiyet = 1;
            TRHTablo.Add(t93);

            TrhYapi t93B = new TrhYapi();
            t93B.yas = 93;
            t93B.sure = 2.32;
            t93B.cinsiyet = 2;
            TRHTablo.Add(t93B);


            TrhYapi t94 = new TrhYapi();
            t94.yas = 94;
            t94.sure = 1.80;
            t94.cinsiyet = 1;
            TRHTablo.Add(t94);

            TrhYapi t94B = new TrhYapi();
            t94B.yas = 94;
            t94B.sure = 1.99;
            t94B.cinsiyet = 2;
            TRHTablo.Add(t94B);

            TrhYapi t95 = new TrhYapi();
            t95.yas = 95;
            t95.sure = 1.55;
            t95.cinsiyet = 1;
            TRHTablo.Add(t95);

            TrhYapi t95B = new TrhYapi();
            t95B.yas = 95;
            t95B.sure = 1.67;
            t95B.cinsiyet = 2;
            TRHTablo.Add(t95B);

            TrhYapi t96 = new TrhYapi();
            t96.yas = 96;
            t96.sure = 1.40;
            t96.cinsiyet = 1;
            TRHTablo.Add(t96);

            TrhYapi t96B = new TrhYapi();
            t96B.yas = 96;
            t96B.sure = 1.36;
            t96B.cinsiyet = 2;
            TRHTablo.Add(t96B);

            TrhYapi t97 = new TrhYapi();
            t97.yas = 97;
            t97.sure = 1.23;
            t97.cinsiyet = 1;
            TRHTablo.Add(t97);

            TrhYapi t97B = new TrhYapi();
            t97B.yas = 97;
            t97B.sure = 1.05;
            t97B.cinsiyet = 2;
            TRHTablo.Add(t97B);


        }
        public class PmfYapi
        {
            public int yas { get; set; }
            public double sure { get; set; }
        }

        public class TrhYapi
        {
            public int yas { get; set;}
            public double sure { get; set; }
            //1- Erkek 2- Kadın
            public int cinsiyet { get; set; }
        }

    }
}
