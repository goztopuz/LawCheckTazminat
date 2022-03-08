using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.GeceCalismaB
{
    public class HaftaBilgiViewModel : ViewModelBase
    {
        double dinlemeSaat;
        public HaftaBilgiViewModel(GeceCalisma _gece, GeceMesaiHaftalar _hafta)
        {
            this.GC = new GeceCalisma();
            this.GC = _gece;
            this.HF = new GeceMesaiHaftalar();
            this.HF = _hafta;

            dinlemeSaat = GC.DinlenmeSure;

            VerileriHazirla();
            
        }

        //----------------------------------------------------------


        private GeceCalisma _gc;
        public GeceCalisma GC
        {
            get => _gc;
            set
            {
                _gc = value;
                OnPropertyChanged();
            }
        }

        private GeceMesaiHaftalar _hf;
        public GeceMesaiHaftalar HF
        {
            get => _hf;
            set
            {
                _hf = value;
                OnPropertyChanged();
            }

        }




        // Kaydet Command

        public ICommand IKaydetCommand => new Command(OnKaydet);
        async private void OnKaydet(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;
            // Kaydet İşlemi..


            KaydetHazirlik();


            
            var sonuc = GC.HaftalarBilgi.ToList().Find(o => o.Id == HF.Id);
            sonuc = HF;
            MessagingCenter.Send<GeceMesaiHaftalar>(sonuc, "YenileGCHaftalar");

            await Application.Current.MainPage.Navigation.PopModalAsync();


            this.HataMesaji = "";
            IsBusy = false;

        }
        private void KaydetHazirlik()
        {
           if(Gun1BasTar.DayOfWeek== DayOfWeek.Monday)
            {
                int saat1 = Gun1BasSaat.Hours;
                int dk1 =   Gun1BasSaat.Minutes;
                int saat2 = Gun1BitSaat.Hours;
                int dk2 = Gun1BitSaat.Minutes;
                DateTime t1 = new DateTime(2020, 12, 18, saat1, dk1, 0);
                DateTime t2 = new DateTime(2020, 12, 19, saat2, dk2, 0);
                HF.BasSaatPSal = t1;
                HF.BitSaatPSal = t2;
                HF.SaatlikPSal = Gun1Ucret;
                HF.NetSaatEllePSal = Gun1Saat;

                if (Gun1Var == true)
                {
                    HF.PSal = true;
                }
                else
                {
                    HF.PSal = false;
                }

                //2. Gün 
                int saat1b = Gun2BasSaat.Hours;
                int dk1b = Gun2BasSaat.Minutes;
                int saat2b = Gun2BitSaat.Hours;
                int dk2b = Gun2BitSaat.Minutes;
                DateTime t1b= new DateTime(2020, 12, 18, saat1b, dk1b, 0);
                DateTime t2b = new DateTime(2020, 12, 19, saat2b, dk2b, 0);
                HF.BasSaatSCar = t1b;
                HF.BitSaatSCar = t2b;
                HF.SaatlikSCar = Gun2Ucret;
                HF.NetSaatElleSCar = Gun2Saat;

                if(Gun2Var== true)
                {
                    HF.SCar = true;
                }
                else
                {
                    HF.SCar = false;
                }


                // 3. Gün
                int saat1c = Gun3BasSaat.Hours;
                int dk1c = Gun3BasSaat.Minutes;
                int saat2c = Gun3BitSaat.Hours;
                int dk2c = Gun3BitSaat.Minutes;
                DateTime t1c = new DateTime(2020, 12, 18, saat1c, dk1c, 0);
                DateTime t2c = new DateTime(2020, 12, 19, saat2c, dk2c, 0);
                HF.BasSaatCPer = t1c;
                HF.BitSaatCPer = t2c;
                HF.SaatlikCPer = Gun3Ucret;
                HF.NetSaatElleCPer = Gun3Saat;

                if(Gun3Var== true)
                {
                    HF.CPer = true;
                }
                else
                {
                    HF.CPer = false;
                }


                // 4. Gün
                int saat1d = Gun4BasSaat.Hours;
                int dk1d = Gun4BasSaat.Minutes;
                int saat2d = Gun4BitSaat.Hours;
                int dk2d = Gun4BitSaat.Minutes;
                DateTime t1d = new DateTime(2020, 12, 18, saat1d, dk1d, 0);
                DateTime t2d = new DateTime(2020, 12, 19, saat2d, dk2d, 0);
                HF.BasSaatPCum = t1d;
                HF.BitSaatPCum = t2d;
                HF.SaatlikPCum = Gun4Ucret;
                HF.NetSaatEllePCum = Gun4Saat;

                if(Gun4Var == true)
                {
                    HF.PCum = true;
                }
                else
                {
                    HF.PCum = false;
                }

                // 5. Gün
                int saat1e = Gun5BasSaat.Hours;
                int dk1e = Gun5BasSaat.Minutes;
                int saat2e = Gun5BitSaat.Hours;
                int dk2e = Gun5BitSaat.Minutes;
                DateTime t1e = new DateTime(2020, 12, 18, saat1e, dk1e, 0);
                DateTime t2e = new DateTime(2020, 12, 19, saat2e, dk2e, 0);
                HF.BasSaatCCmt = t1e;
                HF.BitSaatCCmt = t2e;
                HF.SaatlikCCmt = Gun5Ucret;
                HF.NetSaatElleCCmt = Gun5Saat;
                if(Gun5Var == true)
                {
                    HF.CCmt = true;
                }
                else
                {
                    HF.CCmt = false;
                }

                // 6. Gün
                int saat1f = Gun6BasSaat.Hours;
                int dk1f = Gun6BasSaat.Minutes;
                int saat2f = Gun6BitSaat.Hours;
                int dk2f = Gun6BitSaat.Minutes;
                DateTime t1f = new DateTime(2020, 12, 18, saat1f, dk1f, 0);
                DateTime t2f = new DateTime(2020, 12, 19, saat2f, dk2f, 0);
                HF.BasSaatCPzr = t1f;
                HF.BitSaatCPzr = t2f;
                HF.SaatlikCPzr = Gun6Ucret;
                HF.NetSaatelleCPzr = Gun6Saat;
                if(Gun6Var == true)
                {
                    HF.CPzr = true;
                }
                else
                {
                    HF.CPzr = false;
                }

                // 7. Gün
                int saat1h = Gun7BasSaat.Hours;
                int dk1h = Gun7BasSaat.Minutes;
                int saat2h = Gun7BitSaat.Hours;
                int dk2h = Gun7BitSaat.Minutes;
                DateTime t1h = new DateTime(2020, 12, 18, saat1h, dk1h, 0);
                DateTime t2h = new DateTime(2020, 12, 19, saat2h, dk2h, 0);
                HF.BasSaatPPzt = t1h;
                HF.BitSaatPPzt = t2h;
                HF.SaatlikPPzt = Gun7Ucret;
                HF.NetSaatEllePPzt = Gun7Saat;

                if(Gun7Var == true)
                {
                    HF.PPzt = true;
                }
                else
                {
                    HF.PPzt = false;
                }

             


            }

           else if(Gun1BasTar.DayOfWeek== DayOfWeek.Tuesday)
            {


                int saat1 = Gun1BasSaat.Hours;
                int dk1 = Gun1BasSaat.Minutes;
                int saat2 = Gun1BitSaat.Hours;
                int dk2 = Gun1BitSaat.Minutes;
                DateTime t1 = new DateTime(2020, 12, 18, saat1, dk1, 0);
                DateTime t2 = new DateTime(2020, 12, 19, saat2, dk2, 0);
                HF.BasSaatSCar = t1;
                HF.BitSaatSCar = t2;
                HF.SaatlikSCar = Gun1Ucret;
                HF.NetSaatElleSCar = Gun1Saat;
                
                if(GunVar1 == true)
                {
                    HF.SCar = true;
                  
                }
                else
                {
                    HF.SCar = false;
                }

                //2. Gün 
                int saat1b = Gun2BasSaat.Hours;
                int dk1b = Gun2BasSaat.Minutes;
                int saat2b = Gun2BitSaat.Hours;
                int dk2b = Gun2BitSaat.Minutes;
                DateTime t1b = new DateTime(2020, 12, 18, saat1b, dk1b, 0);
                DateTime t2b = new DateTime(2020, 12, 19, saat2b, dk2b, 0);
                HF.BasSaatCPer = t1b;
                HF.BitSaatCPer = t2b;
                HF.SaatlikCPer = Gun2Ucret;
                HF.NetSaatElleCPer = Gun2Saat;

                if(GunVar2 ==true)
                {
                    HF.CPer = true;
                }
                else
                {
                    HF.CPer = false;
                }

                // 3. Gün
                int saat1c = Gun3BasSaat.Hours;
                int dk1c = Gun3BasSaat.Minutes;
                int saat2c = Gun3BitSaat.Hours;
                int dk2c = Gun3BitSaat.Minutes;
                DateTime t1c = new DateTime(2020, 12, 18, saat1c, dk1c, 0);
                DateTime t2c = new DateTime(2020, 12, 19, saat2c, dk2c, 0);
                HF.BasSaatPCum = t1c;
                HF.BitSaatPCum = t2c;
                HF.SaatlikPCum = Gun3Ucret;
                HF.NetSaatEllePCum = Gun3Saat;
                if(GunVar3 == true)
                {
                    HF.PCum = true;
                }
                else
                {
                    HF.PCum = false;
                }

                // 4. Gün
                int saat1d = Gun4BasSaat.Hours;
                int dk1d = Gun4BasSaat.Minutes;
                int saat2d = Gun4BitSaat.Hours;
                int dk2d = Gun4BitSaat.Minutes;
                DateTime t1d = new DateTime(2020, 12, 18, saat1d, dk1d, 0);
                DateTime t2d = new DateTime(2020, 12, 19, saat2d, dk2d, 0);
                HF.BasSaatCCmt = t1d;
                HF.BitSaatCCmt = t2d;
                HF.SaatlikCCmt = Gun4Ucret;
                HF.NetSaatElleCCmt = Gun4Saat;

                if(GunVar4 ==true )
                {
                    HF.CCmt = true;
                }
                else
                {
                    HF.CCmt = false;
                }

                // 5. Gün
                int saat1e = Gun5BasSaat.Hours;
                int dk1e = Gun5BasSaat.Minutes;
                int saat2e = Gun5BitSaat.Hours;
                int dk2e = Gun5BitSaat.Minutes;
                DateTime t1e = new DateTime(2020, 12, 18, saat1e, dk1e, 0);
                DateTime t2e = new DateTime(2020, 12, 19, saat2e, dk2e, 0);
                HF.BasSaatCPzr = t1e;
                HF.BitSaatCPzr = t2e;
                HF.SaatlikCPzr= Gun5Ucret;
                HF.NetSaatelleCPzr = Gun5Saat;
                if(GunVar5==true )
                {
                    HF.CPzr = true;
                }
                else
                {
                    HF.CPzr = false;
                }

                // 6. Gün
                int saat1f = Gun6BasSaat.Hours;
                int dk1f = Gun6BasSaat.Minutes;
                int saat2f = Gun6BitSaat.Hours;
                int dk2f = Gun6BitSaat.Minutes;
                DateTime t1f = new DateTime(2020, 12, 18, saat1f, dk1f, 0);
                DateTime t2f = new DateTime(2020, 12, 19, saat2f, dk2f, 0);
                HF.BasSaatPPzt = t1f;
                HF.BitSaatPPzt = t2f;
                HF.SaatlikPPzt = Gun6Ucret;
                HF.NetSaatEllePPzt = Gun6Saat;

                if(GunVar6 == true)
                {
                    HF.PPzt = true;
                }
                else
                {
                    HF.PPzt = false;
                }

                // 7. Gün
                int saat1h = Gun7BasSaat.Hours;
                int dk1h = Gun7BasSaat.Minutes;
                int saat2h = Gun7BitSaat.Hours;
                int dk2h = Gun7BitSaat.Minutes;
                DateTime t1h = new DateTime(2020, 12, 18, saat1h, dk1h, 0);
                DateTime t2h = new DateTime(2020, 12, 19, saat2h, dk2h, 0);
                HF.BasSaatPSal = t1h;
                HF.BitSaatPSal = t2h;
                HF.SaatlikPSal = Gun7Ucret;
                HF.NetSaatEllePSal = Gun7Saat;

                if(GunVar7 == true)
                {
                    HF.PSal = true;
                }
                else
                {
                    HF.PSal = false;
                }









            }
          
            else if(Gun1BasTar.DayOfWeek== DayOfWeek.Wednesday)
            {


                int saat1 = Gun1BasSaat.Hours;
                int dk1 = Gun1BasSaat.Minutes;
                int saat2 = Gun1BitSaat.Hours;
                int dk2 = Gun1BitSaat.Minutes;
                DateTime t1 = new DateTime(2020, 12, 18, saat1, dk1, 0);
                DateTime t2 = new DateTime(2020, 12, 19, saat2, dk2, 0);
                HF.BasSaatCPer= t1;
                HF.BitSaatCPer = t2;
                HF.SaatlikCPer = Gun1Ucret;
                HF.NetSaatElleCPer = Gun1Saat;

                if(GunVar1 ==true)
                {
                    HF.CPer = true;
                }
                else
                {
                    HF.CPer = false;
                }

                //2. Gün 
                int saat1b = Gun2BasSaat.Hours;
                int dk1b = Gun2BasSaat.Minutes;
                int saat2b = Gun2BitSaat.Hours;
                int dk2b = Gun2BitSaat.Minutes;
                DateTime t1b = new DateTime(2020, 12, 18, saat1b, dk1b, 0);
                DateTime t2b = new DateTime(2020, 12, 19, saat2b, dk2b, 0);
                HF.BasSaatPCum = t1b;
                HF.BitSaatPCum = t2b;
                HF.SaatlikPCum = Gun2Ucret;
                HF.NetSaatEllePCum = Gun2Saat;
                
                if(GunVar2 ==true)
                {
                    HF.PCum = true;
                }
                else
                {
                    HF.PCum = false;
                }

                // 3. Gün
                int saat1c = Gun3BasSaat.Hours;
                int dk1c = Gun3BasSaat.Minutes;
                int saat2c = Gun3BitSaat.Hours;
                int dk2c = Gun3BitSaat.Minutes;
                DateTime t1c = new DateTime(2020, 12, 18, saat1c, dk1c, 0);
                DateTime t2c = new DateTime(2020, 12, 19, saat2c, dk2c, 0);
                HF.BasSaatCCmt = t1c;
                HF.BitSaatCCmt = t2c;
                HF.SaatlikCCmt = Gun3Ucret;
                HF.NetSaatElleCCmt = Gun3Saat;

                if(GunVar3 == true)
                {
                    HF.CCmt = true;
                }else
                {
                    HF.CCmt = false;
                }

                // 4. Gün
                int saat1d = Gun4BasSaat.Hours;
                int dk1d = Gun4BasSaat.Minutes;
                int saat2d = Gun4BitSaat.Hours;
                int dk2d = Gun4BitSaat.Minutes;
                DateTime t1d = new DateTime(2020, 12, 18, saat1d, dk1d, 0);
                DateTime t2d = new DateTime(2020, 12, 19, saat2d, dk2d, 0);
                HF.BasSaatCPzr = t1d;
                HF.BitSaatCPzr = t2d;
                HF.SaatlikCPzr = Gun4Ucret;
                HF.NetSaatelleCPzr = Gun4Saat;
                
                if(GunVar4 == true)
                {
                    HF.CPzr = true;
                }
                else
                {
                    HF.CPzr = false;
                }

                // 5. Gün
                int saat1e = Gun5BasSaat.Hours;
                int dk1e = Gun5BasSaat.Minutes;
                int saat2e = Gun5BitSaat.Hours;
                int dk2e = Gun5BitSaat.Minutes;
                DateTime t1e = new DateTime(2020, 12, 18, saat1e, dk1e, 0);
                DateTime t2e = new DateTime(2020, 12, 19, saat2e, dk2e, 0);
                HF.BasSaatPPzt = t1e;
                HF.BitSaatPPzt = t2e;
                HF.SaatlikPPzt = Gun5Ucret;
                HF.NetSaatEllePPzt = Gun5Saat;
                if(GunVar5 == true)
                {
                    HF.PPzt = true;
                }
                else
                {
                    HF.PPzt = false;
                }

                // 6. Gün
                int saat1f = Gun6BasSaat.Hours;
                int dk1f = Gun6BasSaat.Minutes;
                int saat2f = Gun6BitSaat.Hours;
                int dk2f = Gun6BitSaat.Minutes;
                DateTime t1f = new DateTime(2020, 12, 18, saat1f, dk1f, 0);
                DateTime t2f = new DateTime(2020, 12, 19, saat2f, dk2f, 0);
                HF.BasSaatPSal = t1f;
                HF.BitSaatPSal = t2f;
                HF.SaatlikPSal = Gun6Ucret;
                HF.NetSaatEllePSal = Gun6Saat;
                if(GunVar6 == true)
                {
                    HF.PSal = true;
                }
                else
                {
                    HF.PSal = false;
                }

                // 7. Gün
                int saat1h = Gun7BasSaat.Hours;
                int dk1h = Gun7BasSaat.Minutes;
                int saat2h = Gun7BitSaat.Hours;
                int dk2h = Gun7BitSaat.Minutes;
                DateTime t1h = new DateTime(2020, 12, 18, saat1h, dk1h, 0);
                DateTime t2h = new DateTime(2020, 12, 19, saat2h, dk2h, 0);
                HF.BasSaatSCar = t1h;
                HF.BitSaatSCar = t2h;
                HF.SaatlikSCar = Gun7Ucret;
                HF.NetSaatElleSCar = Gun7Saat;

                if(GunVar7 == true)
                {
                    HF.SCar = true;

                }
                else
                {
                    HF.SCar = false;
                }


            }
      
            else if(Gun1BasTar.DayOfWeek== DayOfWeek.Thursday)
            {


                int saat1 = Gun1BasSaat.Hours;
                int dk1 = Gun1BasSaat.Minutes;
                int saat2 = Gun1BitSaat.Hours;
                int dk2 = Gun1BitSaat.Minutes;
                DateTime t1 = new DateTime(2020, 12, 18, saat1, dk1, 0);
                DateTime t2 = new DateTime(2020, 12, 19, saat2, dk2, 0);
                HF.BasSaatPCum = t1;
                HF.BitSaatPCum = t2;
                HF.SaatlikPCum = Gun1Ucret;
                HF.NetSaatEllePCum = Gun1Saat;

                if(GunVar1 ==true)
                {
                    HF.PCum = true;
                }
                else
                {
                    HF.PCum = false;
                }
               


                //2. Gün 
                int saat1b = Gun2BasSaat.Hours;
                int dk1b = Gun2BasSaat.Minutes;
                int saat2b = Gun2BitSaat.Hours;
                int dk2b = Gun2BitSaat.Minutes;
                DateTime t1b = new DateTime(2020, 12, 18, saat1b, dk1b, 0);
                DateTime t2b = new DateTime(2020, 12, 19, saat2b, dk2b, 0);
                HF.BasSaatCCmt = t1b;
                HF.BitSaatCCmt = t2b;
                HF.SaatlikCCmt = Gun2Ucret;
                HF.NetSaatElleCCmt = Gun2Saat;

                if(GunVar2==true)
                {
                    HF.CCmt = true;
                }
                else
                {
                    HF.CCmt = false;
                }

                // 3. Gün
                int saat1c = Gun3BasSaat.Hours;
                int dk1c = Gun3BasSaat.Minutes;
                int saat2c = Gun3BitSaat.Hours;
                int dk2c = Gun3BitSaat.Minutes;
                DateTime t1c = new DateTime(2020, 12, 18, saat1c, dk1c, 0);
                DateTime t2c = new DateTime(2020, 12, 19, saat2c, dk2c, 0);
                HF.BasSaatCPzr = t1c;
                HF.BitSaatCPzr = t2c;
                HF.SaatlikCPzr = Gun3Ucret;
                HF.NetSaatelleCPzr = Gun3Saat;

                if(GunVar3 ==true )
                {
                    HF.CPzr = true;
                }
                else
                {
                    HF.CPzr = false;
                }

                // 4. Gün
                int saat1d = Gun4BasSaat.Hours;
                int dk1d = Gun4BasSaat.Minutes;
                int saat2d = Gun4BitSaat.Hours;
                int dk2d = Gun4BitSaat.Minutes;
                DateTime t1d = new DateTime(2020, 12, 18, saat1d, dk1d, 0);
                DateTime t2d = new DateTime(2020, 12, 19, saat2d, dk2d, 0);
                HF.BasSaatPPzt = t1d;
                HF.BitSaatPPzt = t2d;
                HF.SaatlikPPzt = Gun4Ucret;
                HF.NetSaatEllePPzt = Gun4Saat;
                if(GunVar4== true)
                {
                    HF.PPzt = true;
                }
                else
                {
                    HF.PPzt = false;
                }

                // 5. Gün
                int saat1e = Gun5BasSaat.Hours;
                int dk1e = Gun5BasSaat.Minutes;
                int saat2e = Gun5BitSaat.Hours;
                int dk2e = Gun5BitSaat.Minutes;
                DateTime t1e = new DateTime(2020, 12, 18, saat1e, dk1e, 0);
                DateTime t2e = new DateTime(2020, 12, 19, saat2e, dk2e, 0);
                HF.BasSaatPSal = t1e;
                HF.BitSaatPSal = t2e;
                HF.SaatlikPSal = Gun5Ucret;
                HF.NetSaatEllePSal = Gun5Saat;

                if(GunVar5 == true)
                {
                    HF.PSal = true;
                }
                else
                {
                    HF.PSal = false;
                }


                // 6. Gün
                int saat1f = Gun6BasSaat.Hours;
                int dk1f = Gun6BasSaat.Minutes;
                int saat2f = Gun6BitSaat.Hours;
                int dk2f = Gun6BitSaat.Minutes;
                DateTime t1f = new DateTime(2020, 12, 18, saat1f, dk1f, 0);
                DateTime t2f = new DateTime(2020, 12, 19, saat2f, dk2f, 0);
                HF.BasSaatSCar = t1f;
                HF.BitSaatSCar = t2f;
                HF.SaatlikSCar = Gun6Ucret;
                HF.NetSaatElleSCar = Gun6Saat;

                if(GunVar6 == true)
                {
                    HF.SCar = true;
                }
                else
                {
                    HF.SCar = false;
                }


                // 7. Gün
                int saat1h = Gun7BasSaat.Hours;
                int dk1h = Gun7BasSaat.Minutes;
                int saat2h = Gun7BitSaat.Hours;
                int dk2h = Gun7BitSaat.Minutes;
                DateTime t1h = new DateTime(2020, 12, 18, saat1h, dk1h, 0);
                DateTime t2h = new DateTime(2020, 12, 19, saat2h, dk2h, 0);
                HF.BasSaatCPer = t1h;
                HF.BitSaatCPer = t2h;
                HF.SaatlikCPer = Gun7Ucret;
                HF.NetSaatElleCPer = Gun7Saat;

                if(Gun7Var == true)
                {
                    HF.CPer = true;
                }
                else
                {
                    HF.CPer = false;
                }








            }
        
            else if(Gun1BasTar.DayOfWeek== DayOfWeek.Friday)
            {



                int saat1 = Gun1BasSaat.Hours;
                int dk1 = Gun1BasSaat.Minutes;
                int saat2 = Gun1BitSaat.Hours;
                int dk2 = Gun1BitSaat.Minutes;
                DateTime t1 = new DateTime(2020, 12, 18, saat1, dk1, 0);
                DateTime t2 = new DateTime(2020, 12, 19, saat2, dk2, 0);
                HF.BasSaatCCmt = t1;
                HF.BitSaatCCmt = t2;
                HF.SaatlikCCmt = Gun1Ucret;
                HF.NetSaatElleCCmt = Gun1Saat;
                if(GunVar1 == true)
                {
                    HF.CCmt = true;
                }
                else
                {
                    HF.CCmt = false;
                }

                //2. Gün 
                int saat1b = Gun2BasSaat.Hours;
                int dk1b = Gun2BasSaat.Minutes;
                int saat2b = Gun2BitSaat.Hours;
                int dk2b = Gun2BitSaat.Minutes;
                DateTime t1b = new DateTime(2020, 12, 18, saat1b, dk1b, 0);
                DateTime t2b = new DateTime(2020, 12, 19, saat2b, dk2b, 0);
                HF.BasSaatCPzr = t1b;
                HF.BitSaatCPzr = t2b;
                HF.SaatlikCPzr = Gun2Ucret;
                HF.NetSaatelleCPzr = Gun2Saat;
                if(GunVar2 == true)
                {
                    HF.CPzr = true;
                }
                else
                {
                    HF.CPzr = false;
                }

                // 3. Gün
                int saat1c = Gun3BasSaat.Hours;
                int dk1c = Gun3BasSaat.Minutes;
                int saat2c = Gun3BitSaat.Hours;
                int dk2c = Gun3BitSaat.Minutes;
                DateTime t1c = new DateTime(2020, 12, 18, saat1c, dk1c, 0);
                DateTime t2c = new DateTime(2020, 12, 19, saat2c, dk2c, 0);
                HF.BasSaatPPzt = t1c;
                HF.BitSaatPPzt = t2c;
                HF.SaatlikPPzt = Gun3Ucret;
                HF.NetSaatEllePPzt = Gun3Saat;

                if(GunVar3 == true)
                {
                    HF.PPzt = true;
                }
                else
                {
                    HF.PPzt = false;
                }


                // 4. Gün
                int saat1d = Gun4BasSaat.Hours;
                int dk1d = Gun4BasSaat.Minutes;
                int saat2d = Gun4BitSaat.Hours;
                int dk2d = Gun4BitSaat.Minutes;
                DateTime t1d = new DateTime(2020, 12, 18, saat1d, dk1d, 0);
                DateTime t2d = new DateTime(2020, 12, 19, saat2d, dk2d, 0);
                HF.BasSaatPSal = t1d;
                HF.BitSaatPSal = t2d;
                HF.SaatlikPSal = Gun4Ucret;
                HF.NetSaatEllePSal = Gun4Saat;
                if(GunVar4 == true)
                {
                    HF.PSal = true;
                }
                else
                {
                    HF.PSal = false;
                }

                // 5. Gün
                int saat1e = Gun5BasSaat.Hours;
                int dk1e = Gun5BasSaat.Minutes;
                int saat2e = Gun5BitSaat.Hours;
                int dk2e = Gun5BitSaat.Minutes;
                DateTime t1e = new DateTime(2020, 12, 18, saat1e, dk1e, 0);
                DateTime t2e = new DateTime(2020, 12, 19, saat2e, dk2e, 0);
                HF.BasSaatSCar = t1e;
                HF.BitSaatSCar = t2e;
                HF.SaatlikSCar = Gun5Ucret;
                HF.NetSaatElleSCar = Gun5Saat;
                if(GunVar5 == true)
                {
                    HF.SCar = true;
                }
                else
                {
                    HF.SCar = false;
                }

                // 6. Gün
                int saat1f = Gun6BasSaat.Hours;
                int dk1f = Gun6BasSaat.Minutes;
                int saat2f = Gun6BitSaat.Hours;
                int dk2f = Gun6BitSaat.Minutes;
                DateTime t1f = new DateTime(2020, 12, 18, saat1f, dk1f, 0);
                DateTime t2f = new DateTime(2020, 12, 19, saat2f, dk2f, 0);
                HF.BasSaatCPer = t1f;
                HF.BitSaatCPer = t2f;
                HF.SaatlikCPer = Gun6Ucret;
                HF.NetSaatElleCPer = Gun6Saat;
                if(GunVar6 == true)
                {
                    HF.CPer = true;
                }
                else
                {
                    HF.CPer = false;
                }

                // 7. Gün
                int saat1h = Gun7BasSaat.Hours;
                int dk1h = Gun7BasSaat.Minutes;
                int saat2h = Gun7BitSaat.Hours;
                int dk2h = Gun7BitSaat.Minutes;
                DateTime t1h = new DateTime(2020, 12, 18, saat1h, dk1h, 0);
                DateTime t2h = new DateTime(2020, 12, 19, saat2h, dk2h, 0);
                HF.BasSaatPCum= t1h;
                HF.BitSaatPCum = t2h;
                HF.SaatlikPCum = Gun7Ucret;
                HF.NetSaatEllePCum = Gun7Saat;

                if(GunVar7== true)
                {
                    HF.PCum = true;
                }
                else
                {
                    HF.PCum = false;
                }







            }
         
            else if(Gun1BasTar.DayOfWeek== DayOfWeek.Saturday)
            {


                int saat1 = Gun1BasSaat.Hours;
                int dk1 = Gun1BasSaat.Minutes;
                int saat2 = Gun1BitSaat.Hours;
                int dk2 = Gun1BitSaat.Minutes;
                DateTime t1 = new DateTime(2020, 12, 18, saat1, dk1, 0);
                DateTime t2 = new DateTime(2020, 12, 19, saat2, dk2, 0);
                HF.BasSaatCPzr = t1;
                HF.BitSaatCPzr = t2;
                HF.SaatlikCPzr = Gun1Ucret;
                HF.NetSaatelleCPzr = Gun1Saat;
                if(GunVar1== true)
                {
                    HF.CPzr = true;
                }
                else
                {
                    HF.CPzr = false;
                }

                //2. Gün 
                int saat1b = Gun2BasSaat.Hours;
                int dk1b = Gun2BasSaat.Minutes;
                int saat2b = Gun2BitSaat.Hours;
                int dk2b = Gun2BitSaat.Minutes;
                DateTime t1b = new DateTime(2020, 12, 18, saat1b, dk1b, 0);
                DateTime t2b = new DateTime(2020, 12, 19, saat2b, dk2b, 0);
                HF.BasSaatPPzt = t1b;
                HF.BitSaatPPzt = t2b;
                HF.SaatlikPPzt = Gun2Ucret;
                HF.NetSaatEllePPzt = Gun2Saat;

                if(GunVar2== true)
                {
                    HF.PPzt = true;
                }
                else
                {
                    HF.PPzt = false;
                }

                // 3. Gün
                int saat1c = Gun3BasSaat.Hours;
                int dk1c = Gun3BasSaat.Minutes;
                int saat2c = Gun3BitSaat.Hours;
                int dk2c = Gun3BitSaat.Minutes;
                DateTime t1c = new DateTime(2020, 12, 18, saat1c, dk1c, 0);
                DateTime t2c = new DateTime(2020, 12, 19, saat2c, dk2c, 0);
                HF.BasSaatPSal = t1c;
                HF.BitSaatPSal = t2c;
                HF.SaatlikPSal = Gun3Ucret;
                HF.NetSaatEllePSal = Gun3Saat;
                if(GunVar3 == true)
                {
                    HF.PSal = true;
                }
                else
                {
                    HF.PSal = false;
                }

                // 4. Gün
                int saat1d = Gun4BasSaat.Hours;
                int dk1d = Gun4BasSaat.Minutes;
                int saat2d = Gun4BitSaat.Hours;
                int dk2d = Gun4BitSaat.Minutes;
                DateTime t1d = new DateTime(2020, 12, 18, saat1d, dk1d, 0);
                DateTime t2d = new DateTime(2020, 12, 19, saat2d, dk2d, 0);
                HF.BasSaatSCar = t1d;
                HF.BitSaatSCar = t2d;
                HF.SaatlikSCar = Gun4Ucret;
                HF.NetSaatElleSCar = Gun4Saat;
                if(GunVar4== true)
                {
                    HF.SCar = true;
                }else
                {
                    HF.SCar = false;
                }

                // 5. Gün
                int saat1e = Gun5BasSaat.Hours;
                int dk1e = Gun5BasSaat.Minutes;
                int saat2e = Gun5BitSaat.Hours;
                int dk2e = Gun5BitSaat.Minutes;
                DateTime t1e = new DateTime(2020, 12, 18, saat1e, dk1e, 0);
                DateTime t2e = new DateTime(2020, 12, 19, saat2e, dk2e, 0);
                HF.BasSaatCPer = t1e;
                HF.BitSaatCPer = t2e;
                HF.SaatlikCPer = Gun5Ucret;
                HF.NetSaatElleCPer = Gun5Saat;
                if(GunVar5  == true)
                {
                    HF.CPer = true;
                }
                else
                {
                    HF.CPer = false;
                }

                // 6. Gün
                int saat1f = Gun6BasSaat.Hours;
                int dk1f = Gun6BasSaat.Minutes;
                int saat2f = Gun6BitSaat.Hours;
                int dk2f = Gun6BitSaat.Minutes;
                DateTime t1f = new DateTime(2020, 12, 18, saat1f, dk1f, 0);
                DateTime t2f = new DateTime(2020, 12, 19, saat2f, dk2f, 0);
                HF.BasSaatPCum = t1f;
                HF.BitSaatPCum = t2f;
                HF.SaatlikPCum = Gun6Ucret;
                HF.NetSaatEllePCum = Gun6Saat;
                if(GunVar6== true)
                {
                    HF.PCum = true;
                }
                else
                {
                    HF.PCum = false;
                }

                // 7. Gün
                int saat1h = Gun7BasSaat.Hours;
                int dk1h = Gun7BasSaat.Minutes;
                int saat2h = Gun7BitSaat.Hours;
                int dk2h = Gun7BitSaat.Minutes;
                DateTime t1h = new DateTime(2020, 12, 18, saat1h, dk1h, 0);
                DateTime t2h = new DateTime(2020, 12, 19, saat2h, dk2h, 0);
                HF.BasSaatCCmt = t1h;
                HF.BitSaatCCmt = t2h;
                HF.SaatlikCCmt = Gun7Ucret;
                HF.NetSaatElleCCmt = Gun7Saat;
                if(GunVar7 == true)
                {
                    HF.CCmt = true;
                }
                else
                {
                    HF.CCmt = false;
                }












            }
         
            else if(Gun1BasTar.DayOfWeek==DayOfWeek.Sunday)
            {





                int saat1 = Gun1BasSaat.Hours;
                int dk1 = Gun1BasSaat.Minutes;
                int saat2 = Gun1BitSaat.Hours;
                int dk2 = Gun1BitSaat.Minutes;
                DateTime t1 = new DateTime(2020, 12, 18, saat1, dk1, 0);
                DateTime t2 = new DateTime(2020, 12, 19, saat2, dk2, 0);
                HF.BasSaatPPzt = t1;
                HF.BitSaatPPzt = t2;
                HF.SaatlikPPzt = Gun1Ucret;
                HF.NetSaatEllePPzt = Gun1Saat;
               
                if(GunVar1== true)
                {
                    HF.PPzt = true;
                }
                else
                {
                    HF.PPzt = false;
                }

                //2. Gün 
                int saat1b = Gun2BasSaat.Hours;
                int dk1b = Gun2BasSaat.Minutes;
                int saat2b = Gun2BitSaat.Hours;
                int dk2b = Gun2BitSaat.Minutes;
                DateTime t1b = new DateTime(2020, 12, 18, saat1b, dk1b, 0);
                DateTime t2b = new DateTime(2020, 12, 19, saat2b, dk2b, 0);
                HF.BasSaatPSal = t1b;
                HF.BitSaatPSal = t2b;
                HF.SaatlikPSal = Gun2Ucret;
                HF.NetSaatEllePSal = Gun2Saat;
                if(GunVar2 ==true)
                {
                    HF.PSal = true;
                }else

                {
                    HF.PSal = false;
                }

                // 3. Gün
                int saat1c = Gun3BasSaat.Hours;
                int dk1c = Gun3BasSaat.Minutes;
                int saat2c = Gun3BitSaat.Hours;
                int dk2c = Gun3BitSaat.Minutes;
                DateTime t1c = new DateTime(2020, 12, 18, saat1c, dk1c, 0);
                DateTime t2c = new DateTime(2020, 12, 19, saat2c, dk2c, 0);
                HF.BasSaatSCar = t1c;
                HF.BitSaatSCar = t2c;
                HF.SaatlikSCar = Gun3Ucret;
                HF.NetSaatElleSCar = Gun3Saat;

                if(GunVar3== true)
                {
                    HF.SCar = true;
                }
                else
                {
                    HF.SCar = false;
                }

                // 4. Gün
                int saat1d = Gun4BasSaat.Hours;
                int dk1d = Gun4BasSaat.Minutes;
                int saat2d = Gun4BitSaat.Hours;
                int dk2d = Gun4BitSaat.Minutes;
                DateTime t1d = new DateTime(2020, 12, 18, saat1d, dk1d, 0);
                DateTime t2d = new DateTime(2020, 12, 19, saat2d, dk2d, 0);
                HF.BasSaatCPer = t1d;
                HF.BitSaatCPer = t2d;
                HF.SaatlikCPer = Gun4Ucret;
                HF.NetSaatElleCPer = Gun4Saat;

                if(Gun4Var== true)
                {
                    HF.CPer = true;
                }else
                {
                    HF.CPer = false;
                }

                // 5. Gün
                int saat1e = Gun5BasSaat.Hours;
                int dk1e = Gun5BasSaat.Minutes;
                int saat2e = Gun5BitSaat.Hours;
                int dk2e = Gun5BitSaat.Minutes;
                DateTime t1e = new DateTime(2020, 12, 18, saat1e, dk1e, 0);
                DateTime t2e = new DateTime(2020, 12, 19, saat2e, dk2e, 0);
                HF.BasSaatPCum = t1e;
                HF.BitSaatPCum = t2e;
                HF.SaatlikPCum = Gun5Ucret;
                HF.NetSaatEllePCum = Gun5Saat;
                if(GunVar5 == true)
                {
                    HF.PCum = true;
                }
                else
                {
                    HF.PCum = false;
                }

                // 6. Gün
                int saat1f = Gun6BasSaat.Hours;
                int dk1f = Gun6BasSaat.Minutes;
                int saat2f = Gun6BitSaat.Hours;
                int dk2f = Gun6BitSaat.Minutes;
                DateTime t1f = new DateTime(2020, 12, 18, saat1f, dk1f, 0);
                DateTime t2f = new DateTime(2020, 12, 19, saat2f, dk2f, 0);
                HF.BasSaatCCmt = t1f;
                HF.BitSaatCCmt = t2f;
                HF.SaatlikCCmt = Gun6Ucret;
                HF.NetSaatElleCCmt = Gun6Saat;
                if(GunVar6== true)
                {
                    HF.CCmt = true;
                }
                else
                {
                    HF.CCmt = false;
                }

                // 7. Gün
                int saat1h = Gun7BasSaat.Hours;
                int dk1h = Gun7BasSaat.Minutes;
                int saat2h = Gun7BitSaat.Hours;
                int dk2h = Gun7BitSaat.Minutes;
                DateTime t1h = new DateTime(2020, 12, 18, saat1h, dk1h, 0);
                DateTime t2h = new DateTime(2020, 12, 19, saat2h, dk2h, 0);
                HF.BasSaatCPzr = t1h;
                HF.BitSaatCPzr = t2h;
                HF.SaatlikCPzr = Gun7Ucret;
                HF.NetSaatelleCPzr = Gun7Saat;

                if(GunVar7 == true)
                {
                    HF.CPzr = true;
                }
                else
                {
                    HF.CPzr = false;
                }

            }

            double _toplam = 0;


           if(Gun1Var== true)
            {
                _toplam = _toplam +Gun1Saat;

                if (GC.TatilVardiyaiDus == true)
                {
                    DayOfWeek dd = Gun1BasTar.DayOfWeek;
                    bool durum = GunHaftaTatiliCakismas(dd);
                    if(durum==true)
                    {
                        _toplam = _toplam - Gun1Saat;
                    }
                }
             
            }
           
            if (Gun2Var == true)
            {
                _toplam = _toplam + Gun2Saat;

                if (GC.TatilVardiyaiDus == true)
                {
                    DayOfWeek dd = Gun2BasTar.DayOfWeek;
                    bool durum = GunHaftaTatiliCakismas(dd);
                    if (durum == true)
                    {
                        _toplam = _toplam - Gun2Saat;
                    }
                }


            }
                
            if (Gun3Var == true)
            {
                _toplam = _toplam + Gun3Saat;


                if (GC.TatilVardiyaiDus == true)
                {
                    DayOfWeek dd = Gun3BasTar.DayOfWeek;
                    bool durum = GunHaftaTatiliCakismas(dd);
                    if (durum == true)
                    {
                        _toplam = _toplam - Gun3Saat;
                    }
                }

            }
                    
            if (Gun4Var == true)
            {
                _toplam = _toplam + Gun4Saat;


                if (GC.TatilVardiyaiDus == true)
                {
                    DayOfWeek dd = Gun4BasTar.DayOfWeek;
                    bool durum = GunHaftaTatiliCakismas(dd);
                    if (durum == true)
                    {
                        _toplam = _toplam - Gun4Saat;
                    }
                }

            }
               
            if (Gun5Var == true)
            {
                _toplam = _toplam + Gun5Saat;


                if (GC.TatilVardiyaiDus == true)
                {
                    DayOfWeek dd = Gun5BasTar.DayOfWeek;
                    bool durum = GunHaftaTatiliCakismas(dd);
                    if (durum == true)
                    {
                        _toplam = _toplam - Gun5Saat;
                    }
                }

            }
        
            if (Gun6Var == true)
            {
                _toplam = _toplam + Gun6Saat;

                if (GC.TatilVardiyaiDus == true)
                {
                    DayOfWeek dd = Gun6BasTar.DayOfWeek;
                    bool durum = GunHaftaTatiliCakismas(dd);
                    if (durum == true)
                    {
                        _toplam = _toplam - Gun6Saat;
                    }
                }
            }

            if (Gun7Var == true)
            {
                _toplam = _toplam + Gun7Saat;

                if (GC.TatilVardiyaiDus == true)
                {
                    DayOfWeek dd = Gun7BasTar.DayOfWeek;
                    bool durum = GunHaftaTatiliCakismas(dd);
                    if (durum == true)
                    {
                        _toplam = _toplam - Gun7Saat;
                    }
                }


            }

            HF.ToplamSaatElle = _toplam;




        }

        private bool GunHaftaTatiliCakismas(DayOfWeek dd)
        {
            bool durum = false;

            if (GC.TatilGunu == "Pazartesi")
            {

                if (dd == DayOfWeek.Monday)
                {
                    durum = true;
                }
            }else if(GC.TatilGunu== "Salı")
            {
                if(dd == DayOfWeek.Tuesday)
                {
                    durum = true;
                }
            }
            else if(GC.TatilGunu=="Çarşamba")
            {
                if(dd == DayOfWeek.Wednesday)
                {
                    durum = true;
                }
            }
            else if(GC.TatilGunu=="Perşembe")
            {
                if(dd == DayOfWeek.Thursday)
                {
                    durum = true;
                }
            }
            else if(GC.TatilGunu =="Cuma")
            {
                if(dd == DayOfWeek.Friday)
                {
                    durum = true;
                }
            }
            else if(GC.TatilGunu=="Cumartesi")
            {
                if(dd== DayOfWeek.Saturday)
                {
                    durum = true;
                }
            }
            else if(GC.TatilGunu=="Pazar")
            {
                if(dd== DayOfWeek.Sunday)
                {
                    durum = true; 
                }
            }

            return durum;
            }




         
        


        public ICommand IptalCommand => new Command(OnIptal);
        async private void OnIptal(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            await Application.Current.MainPage.Navigation.PopModalAsync();
            IsBusy = false;

        }

        public bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        string _hataMesaji = "";
        public string HataMesaji
        {
            get => _hataMesaji;
            set
            {
                _hataMesaji = value;
                OnPropertyChanged();
            }
        }


        public ICommand Gun1SaatCommand => new Command(OnGun1Saat);
        
        private void OnGun1Saat(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            int saat1 = Gun1BasSaat.Hours;
            int dk1 = Gun1BasSaat.Minutes;
            int saat2 = Gun1BitSaat.Hours;
            int dk2 = Gun1BitSaat.Minutes;

            DateTime t1 = new DateTime(2020, 12, 18, saat1, dk1, 0);
            DateTime t2 = new DateTime(2020, 12, 19, saat2, dk2, 0);
            var saat = (t2 - t1).TotalHours;

            Gun1Saat = saat -(7.5 + dinlemeSaat);

            if(Gun1Saat<0)
            {
                Gun1Saat = 0;
            }
            IsBusy = false;

        }

        public ICommand Gun2SaatCommand => new Command(OnGun2Saat);
        private void OnGun2Saat()
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            int saat1 = Gun2BasSaat.Hours;
            int dk1 = Gun2BasSaat.Minutes;
            int saat2 = Gun2BitSaat.Hours;
            int dk2 = Gun2BitSaat.Minutes;

            DateTime t1 = new DateTime(2020, 12, 18, saat1, dk1, 0);
            DateTime t2 = new DateTime(2020, 12, 19, saat2, dk2, 0);
            var saat = (t2 - t1).TotalHours;

            Gun2Saat = saat - (7.5 + dinlemeSaat);

            if (Gun2Saat < 0)
            {
                Gun2Saat = 0;
            }

            IsBusy = false;

        }


        public ICommand Gun3SaatCommand => new Command(OnGun3Saat);
        private void OnGun3Saat()
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            int saat1 = Gun3BasSaat.Hours;
            int dk1 = Gun3BasSaat.Minutes;
            int saat2 = Gun3BitSaat.Hours;
            int dk2 = Gun3BitSaat.Minutes;

            DateTime t1 = new DateTime(2020, 12, 18, saat1, dk1, 0);
            DateTime t2 = new DateTime(2020, 12, 19, saat2, dk2, 0);
            var saat = (t2 - t1).TotalHours;

            Gun3Saat = saat - (7.5 + dinlemeSaat);

            if (Gun3Saat < 0)
            {
                Gun3Saat = 0;
            }
            IsBusy = false;
        }


        public ICommand Gun4SaatCommand => new Command(OnGun4Saat);
        private void OnGun4Saat()
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            int saat1 = Gun4BasSaat.Hours;
            int dk1 = Gun4BasSaat.Minutes;
            int saat2 = Gun4BitSaat.Hours;
            int dk2 = Gun4BitSaat.Minutes;

            DateTime t1 = new DateTime(2020, 12, 18, saat1, dk1, 0);
            DateTime t2 = new DateTime(2020, 12, 19, saat2, dk2, 0);
            var saat = (t2 - t1).TotalHours;

            Gun4Saat = saat - (7.5 + dinlemeSaat);

            if (Gun4Saat < 0)
            {
                Gun4Saat = 0;
            }
            IsBusy = false;
        }

        public ICommand Gun5SaatCommand => new Command(OnGun5Saat);
        private void OnGun5Saat()
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            int saat1 = Gun5BasSaat.Hours;
            int dk1 = Gun5BasSaat.Minutes;
            int saat2 = Gun5BitSaat.Hours;
            int dk2 = Gun5BitSaat.Minutes;

            DateTime t1 = new DateTime(2020, 12, 18, saat1, dk1, 0);
            DateTime t2 = new DateTime(2020, 12, 19, saat2, dk2, 0);
            var saat = (t2 - t1).TotalHours;

            Gun5Saat = saat - (7.5 + dinlemeSaat);

            if (Gun5Saat < 0)
            {
                Gun5Saat = 0;
            }
            IsBusy = false;

        }

        public ICommand Gun6SaatCommand => new Command(OnGun6Saat);
        private void OnGun6Saat()
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            int saat1 = Gun6BasSaat.Hours;
            int dk1 = Gun6BasSaat.Minutes;
            int saat2 = Gun6BitSaat.Hours;
            int dk2 = Gun6BitSaat.Minutes;

            DateTime t1 = new DateTime(2020, 12, 18, saat1, dk1, 0);
            DateTime t2 = new DateTime(2020, 12, 19, saat2, dk2, 0);
            var saat = (t2 - t1).TotalHours;

            Gun6Saat = saat - (7.5 + dinlemeSaat);

            if (Gun6Saat < 0)
            {
                Gun6Saat = 0;
            }
            IsBusy = false;

        }

        public ICommand Gun7SaatCommand => new Command(OnGun7Saat);
        private void OnGun7Saat()
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            int saat1 = Gun7BasSaat.Hours;
            int dk1 = Gun7BasSaat.Minutes;
            int saat2 = Gun7BitSaat.Hours;
            int dk2 = Gun7BitSaat.Minutes;

            DateTime t1 = new DateTime(2020, 12, 18, saat1, dk1, 0);
            DateTime t2 = new DateTime(2020, 12, 19, saat2, dk2, 0);
            var saat = (t2 - t1).TotalHours;

            Gun7Saat = saat - (7.5 + dinlemeSaat);

            if (Gun7Saat < 0)
            {
                Gun7Saat = 0;
            }
            IsBusy = false;

        }




        // 1. Gün Bilgileri
        private bool _gun1Var;
        public bool Gun1Var
        {
            get => _gun1Var;
            set
            {
                _gun1Var = value;
                OnPropertyChanged();
            }

        }

        private string _gun1baslik;
        public string Gun1Baslik
        {
            get => _gun1baslik;

            set
            {
                _gun1baslik = value;
                OnPropertyChanged();
            }
           }
        
        private DateTime _gun1BasTar;
        public DateTime Gun1BasTar
        {
            get => _gun1BasTar;
            set
            {
                _gun1BasTar = value;
                OnPropertyChanged();
            }
        }

        private DateTime _gun1BitTar;
        public DateTime Gun1BitTar
        {
            get => _gun1BitTar;
            set
            {
                _gun1BitTar = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _gun1BasSaat;
        public TimeSpan Gun1BasSaat
        {
            get => _gun1BasSaat;
            set
            {
                _gun1BasSaat = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _gun1BitSaat;
        public TimeSpan Gun1BitSaat
        {
            get => _gun1BitSaat;
            set
            {
                _gun1BitSaat = value;
                OnPropertyChanged();
            }
        }

        private decimal _gun1Ucret;
        public Decimal Gun1Ucret
        {
            get => _gun1Ucret;
            set
            {
                _gun1Ucret = value;
                OnPropertyChanged();
            }

        }

        private double _gun1Saat;
        public double Gun1Saat
        {

            get => _gun1Saat;

            set
            {
                _gun1Saat = value;
                OnPropertyChanged();
            }
        }



        // Gun2

        private bool _gun2Var;
        public bool Gun2Var
        {
            get => _gun2Var;
            set
            {
                _gun2Var = value;
                OnPropertyChanged();
            }

        }
      
        private string _gun2baslik;
        public string Gun2Baslik
        {
            get => _gun2baslik;

            set
            {
                _gun2baslik = value;
                OnPropertyChanged();
            }
        }

        private DateTime _gun2BasTar;
        public DateTime Gun2BasTar
        {
            get => _gun2BasTar;
            set
            {
                _gun2BasTar = value;
                OnPropertyChanged();
            }
        }

        private DateTime _gun2BitTar;
        public DateTime Gun2BitTar
        {
            get => _gun2BitTar;
            set
            {
                _gun2BitTar = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _gun2BasSaat;
        public TimeSpan Gun2BasSaat
        {
            get => _gun2BasSaat;
            set
            {
                _gun2BasSaat = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _gun2BitSaat;
        public TimeSpan Gun2BitSaat
        {
            get => _gun2BitSaat;
            set
            {
                _gun2BitSaat = value;
                OnPropertyChanged();
            }
        }

        private decimal _gun2Ucret;
        public Decimal Gun2Ucret
        {
            get => _gun2Ucret;
            set
            {
                _gun2Ucret = value;
                OnPropertyChanged();
            }

        }

        private double _gun2Saat;
        public double Gun2Saat
        {

            get => _gun2Saat;

            set
            {
                _gun2Saat = value;
                OnPropertyChanged();
            }
        }


        // 3. Gün

        private bool _gun3Var;
        public bool Gun3Var
        {
            get => _gun3Var;
            set
            {
                _gun3Var = value;
                OnPropertyChanged();
            }

        }

        private string _gun3baslik;
        public string Gun3Baslik
        {
            get => _gun3baslik;

            set
            {
                _gun3baslik = value;
                OnPropertyChanged();
            }
        }

        private DateTime _gun3BasTar;
        public DateTime Gun3BasTar
        {
            get => _gun3BasTar;
            set
            {
                _gun3BasTar = value;
                OnPropertyChanged();
            }
        }

        private DateTime _gun3BitTar;
        public DateTime Gun3BitTar
        {
            get => _gun3BitTar;
            set
            {
                _gun3BitTar = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _gun3BasSaat;
        public TimeSpan Gun3BasSaat
        {
            get => _gun3BasSaat;
            set
            {
                _gun3BasSaat = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _gun3BitSaat;
        public TimeSpan Gun3BitSaat
        {
            get => _gun3BitSaat;
            set
            {
                _gun3BitSaat = value;
                OnPropertyChanged();
            }
        }

        private decimal _gun3Ucret;
        public Decimal Gun3Ucret
        {
            get => _gun3Ucret;
            set
            {
                _gun3Ucret = value;
                OnPropertyChanged();
            }

        }

        private double _gun3Saat;
        public double Gun3Saat
        {

            get => _gun3Saat;

            set
            {
                _gun3Saat = value;
                OnPropertyChanged();
            }
        }

        // 4.Gün......
        private bool _gun4Var;
        public bool Gun4Var
        {
            get => _gun4Var;
            set
            {
                _gun4Var = value;
                OnPropertyChanged();
            }

        }

        private string _gun4baslik;
        public string Gun4Baslik
        {
            get => _gun4baslik;

            set
            {
                _gun4baslik = value;
                OnPropertyChanged();
            }
        }

        private DateTime _gun4BasTar;
        public DateTime Gun4BasTar
        {
            get => _gun4BasTar;
            set
            {
                _gun4BasTar = value;
                OnPropertyChanged();
            }
        }

        private DateTime _gun4BitTar;
        public DateTime Gun4BitTar
        {
            get => _gun4BitTar;
            set
            {
                _gun4BitTar = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _gun4BasSaat;
        public TimeSpan Gun4BasSaat
        {
            get => _gun4BasSaat;
            set
            {
                _gun4BasSaat = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _gun4BitSaat;
        public TimeSpan Gun4BitSaat
        {
            get => _gun4BitSaat;
            set
            {
                _gun4BitSaat = value;
                OnPropertyChanged();
            }
        }

        private decimal _gun4Ucret;
        public Decimal Gun4Ucret
        {
            get => _gun4Ucret;
            set
            {
                _gun4Ucret = value;
                OnPropertyChanged();
            }

        }

        private double _gun4Saat;
        public double Gun4Saat
        {

            get => _gun4Saat;

            set
            {
                _gun4Saat = value;
                OnPropertyChanged();
            }
        }

        // 5.Gün

        private bool _gun5Var;
        public bool Gun5Var
        {
            get => _gun5Var;
            set
            {
                _gun5Var = value;
                OnPropertyChanged();
            }

        }

        private string _gun5baslik;
        public string Gun5Baslik
        {
            get => _gun5baslik;

            set
            {
                _gun5baslik = value;
                OnPropertyChanged();
            }
        }

        private DateTime _gun5BasTar;
        public DateTime Gun5BasTar
        {
            get => _gun5BasTar;
            set
            {
                _gun5BasTar = value;
                OnPropertyChanged();
            }
        }

        private DateTime _gun5BitTar;
        public DateTime Gun5BitTar
        {
            get => _gun5BitTar;
            set
            {
                _gun5BitTar = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _gun5BasSaat;
        public TimeSpan Gun5BasSaat
        {
            get => _gun5BasSaat;
            set
            {
                _gun5BasSaat = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _gun5BitSaat;
        public TimeSpan Gun5BitSaat
        {
            get => _gun5BitSaat;
            set
            {
                _gun5BitSaat = value;
                OnPropertyChanged();
            }
        }

        private decimal _gun5Ucret;
        public Decimal Gun5Ucret
        {
            get => _gun5Ucret;
            set
            {
                _gun5Ucret = value;
                OnPropertyChanged();
            }

        }

        private double _gun5Saat;
        public double Gun5Saat
        {

            get => _gun5Saat;

            set
            {
                _gun5Saat = value;
                OnPropertyChanged();
            }
        }

        // 6. Gün

        private bool _gun6Var;
        public bool Gun6Var
        {
            get => _gun6Var;
            set
            {
                _gun6Var = value;
                OnPropertyChanged();
            }

        }

        private string _gun6baslik;
        public string Gun6Baslik
        {
            get => _gun6baslik;

            set
            {
                _gun6baslik = value;
                OnPropertyChanged();
            }
        }

        private DateTime _gun6BasTar;
        public DateTime Gun6BasTar
        {
            get => _gun6BasTar;
            set
            {
                _gun6BasTar = value;
                OnPropertyChanged();
            }
        }

        private DateTime _gun6BitTar;
        public DateTime Gun6BitTar
        {
            get => _gun6BitTar;
            set
            {
                _gun6BitTar = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _gun6BasSaat;
        public TimeSpan Gun6BasSaat
        {
            get => _gun6BasSaat;
            set
            {
                _gun6BasSaat = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _gun6BitSaat;
        public TimeSpan Gun6BitSaat
        {
            get => _gun6BitSaat;
            set
            {
                _gun6BitSaat = value;
                OnPropertyChanged();
            }
        }

        private decimal _gun6Ucret;
        public Decimal Gun6Ucret
        {
            get => _gun6Ucret;
            set
            {
                _gun6Ucret = value;
                OnPropertyChanged();
            }

        }

        private double _gun6saat;
        public double Gun6Saat
        {

            get => _gun6saat;

            set
            {
                _gun6saat = value;
                OnPropertyChanged();
            }
        }


        // 7. Gün......


        private bool _gun7Var;
        public bool Gun7Var
        {
            get => _gun7Var;
            set
            {
                _gun7Var = value;
                OnPropertyChanged();
            }

        }

        private string _gun7baslik;
        public string Gun7Baslik
        {
            get => _gun7baslik;

            set
            {
                _gun7baslik = value;
                OnPropertyChanged();
            }
        }

        private DateTime _gun7BasTar;
        public DateTime Gun7BasTar
        {
            get => _gun7BasTar;
            set
            {
                _gun7BasTar = value;
                OnPropertyChanged();
            }
        }

        private DateTime _gun7BitTar;
        public DateTime Gun7BitTar
        {
            get => _gun7BitTar;
            set
            {
                _gun7BitTar = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _gun7BasSaat;
        public TimeSpan Gun7BasSaat
        {
            get => _gun7BasSaat;
            set
            {
                _gun7BasSaat = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _gun7BitSaat;
        public TimeSpan Gun7BitSaat
        {
            get => _gun7BitSaat;
            set
            {
                _gun7BitSaat = value;
                OnPropertyChanged();
            }
        }

        private decimal _gun7Ucret;
        public Decimal Gun7Ucret
        {
            get => _gun7Ucret;
            set
            {
                _gun7Ucret = value;
                OnPropertyChanged();
            }

        }

        private double _gun7Saat;
        public double Gun7Saat
        {

            get => _gun7Saat;

            set
            {
                _gun7Saat = value;
                OnPropertyChanged();
            }
        }


        //---- Günler Bitiş


        List<GeceMesaiGunBilgi> ListeH = new List<GeceMesaiGunBilgi>();

        private void VerileriHazirla()
        {
            ListeH.Clear();

            GeceMesaiGunBilgi g1 = new GeceMesaiGunBilgi();
            g1.GunBaslik = "Pazartesi-Salı";
            g1.GunVar = HF.PSal;
            g1.GunBasTar = HF.TarPSal;
            g1.GunBitTar = HF.BitTarPSal;

            TimeSpan basSaat1 = new TimeSpan(HF.BasSaatPSal.Hour, HF.BasSaatPSal.Minute, 0);
            g1.GunBasSaat = basSaat1;

            TimeSpan bitSaat1 = new TimeSpan(HF.BitSaatPSal.Hour, HF.BitSaatPSal.Minute, 0);
            g1.GunBitSaat = bitSaat1;

            g1.GunUcret = HF.SaatlikPSal;
            g1.GunSaat = HF.NetSaatEllePSal;

            ListeH.Add(g1);


            GeceMesaiGunBilgi g2 = new GeceMesaiGunBilgi();
            g2.GunBaslik = "Salı-Çarşamba";
            g2.GunVar = HF.SCar;
            g2.GunBasTar = HF.TarSCar;
            g2.GunBitTar = HF.BitTarSCar;
            TimeSpan basSaat2 = new TimeSpan(HF.BasSaatSCar.Hour, HF.BasSaatSCar.Minute, 0);
            g2.GunBasSaat = basSaat2;
            TimeSpan bitSaat2 = new TimeSpan(HF.BitSaatSCar.Hour, HF.BitSaatSCar.Minute, 0);
            g2.GunBitSaat = bitSaat2;
            g2.GunUcret = HF.SaatlikSCar;
            g2.GunSaat = HF.NetSaatElleSCar;

            ListeH.Add(g2);

            GeceMesaiGunBilgi g3 = new GeceMesaiGunBilgi();
            g3.GunBaslik = "Çarşamba-Perşembe";
            g3.GunVar = HF.CPer;
            g3.GunBasTar = HF.TarCPer;
            g3.GunBitTar = HF.BitTarCPer;

            TimeSpan basSaat3 = new TimeSpan(HF.BasSaatCPer.Hour, HF.BasSaatCPer.Minute, 0);
            g3.GunBasSaat = basSaat3;
            TimeSpan bitSaat3 = new TimeSpan(HF.BitSaatCPer.Hour, HF.BitSaatCPer.Minute, 0);
            g3.GunBitSaat = bitSaat3;

            g3.GunUcret = HF.SaatlikCPer;
            g3.GunSaat = HF.NetSaatElleCPer;

            ListeH.Add(g3);

            GeceMesaiGunBilgi g4 = new GeceMesaiGunBilgi();
            g4.GunBaslik = "Perşembe-Cuma";
            g4.GunVar = HF.PCum;
            g4.GunBasTar = HF.TarPCum;
            g4.GunBitTar = HF.BitTarPCum;
            TimeSpan basSaat4 = new TimeSpan(HF.BasSaatPCum.Hour, HF.BasSaatPCum.Minute, 0);
            g4.GunBasSaat = basSaat4;
            TimeSpan bitSaat4 = new TimeSpan(HF.BitSaatPCum.Hour, HF.BitSaatPCum.Minute, 0);
            g4.GunBitSaat = bitSaat4;
            g4.GunUcret = HF.SaatlikPCum;
            g4.GunSaat = HF.NetSaatEllePCum;

            ListeH.Add(g4);

            GeceMesaiGunBilgi g5 = new GeceMesaiGunBilgi();
            g5.GunBaslik = "Cuma-Cumartesi";
            g5.GunVar = HF.CCmt;
            g5.GunBasTar = HF.TarCCmt;
            g5.GunBitTar = HF.BitTarCCmt;

            TimeSpan basSaat5 = new TimeSpan(HF.BasSaatCCmt.Hour, HF.BasSaatCCmt.Minute, 0);
            g5.GunBasSaat = basSaat5;
            TimeSpan bitSaat5 = new TimeSpan(HF.BitSaatCCmt.Hour, HF.BitSaatCCmt.Minute, 0);
            g5.GunBitSaat = bitSaat5;

            g5.GunUcret = HF.SaatlikCCmt;
            g5.GunSaat = HF.NetSaatElleCCmt;


            ListeH.Add(g5);

            GeceMesaiGunBilgi g6 = new GeceMesaiGunBilgi();
            g6.GunBaslik = "Cumartesi-Pazar";
            g6.GunVar = HF.CPzr;
            g6.GunBasTar = HF.TarCPzr;
            g6.GunBitTar = HF.BitTarCPzr;

            TimeSpan basSaat6 = new TimeSpan(HF.BasSaatCPzr.Hour, HF.BasSaatCPzr.Minute, 0);
            g6.GunBasSaat = basSaat6;
            TimeSpan bitSaat6 = new TimeSpan(HF.BitSaatCPzr.Hour, HF.BitSaatCPzr.Minute, 0);
            g6.GunBitSaat = bitSaat6;

            g6.GunUcret = HF.SaatlikCPzr;
            g6.GunSaat = HF.NetSaatelleCPzr;

            ListeH.Add(g6);

            GeceMesaiGunBilgi g7 = new GeceMesaiGunBilgi();
            g7.GunBaslik = "Pazar-Pazartesi";
            g7.GunVar = HF.PPzt;
            g7.GunBasTar = HF.TarPPzt;
            g7.GunBitTar = HF.BitTarPPzt;
            TimeSpan basSaat7 = new TimeSpan(HF.BasSaatPPzt.Hour, HF.BasSaatPPzt.Minute, 0);
            g7.GunBasSaat = basSaat7;
            TimeSpan bitSaat7 = new TimeSpan(HF.BitSaatPPzt.Hour, HF.BitSaatPPzt.Minute, 0);
            g7.GunBitSaat = bitSaat7;

            g7.GunUcret = HF.SaatlikPPzt;
            g7.GunSaat = HF.NetSaatPPzt;

            ListeH.Add(g7);

            var ListeSirali = ListeH.OrderBy(o => o.GunBasTar).ToList();


            var kayit1 = ListeSirali[0];
            if (kayit1 != null)
            {
                Gun1Var = kayit1.GunVar;
                Gun1Baslik = kayit1.GunBaslik;
                Gun1BasTar = kayit1.GunBasTar;
                Gun1BitTar = kayit1.GunBitTar;
                Gun1BasSaat = kayit1.GunBasSaat;
                Gun1BitSaat = kayit1.GunBitSaat;
                Gun1Ucret = kayit1.GunUcret;
                Gun1Saat = kayit1.GunSaat;
            }

            var kayit2 = ListeSirali[1];
            if (kayit2 != null)
            {
                Gun2Var = kayit2.GunVar;
                Gun2Baslik = kayit2.GunBaslik;
                Gun2BasTar = kayit2.GunBasTar;
                Gun2BitTar = kayit2.GunBitTar;
                Gun2BasSaat = kayit2.GunBasSaat;
                Gun2BitSaat = kayit2.GunBitSaat;
                Gun2Ucret = kayit2.GunUcret;
                Gun2Saat = kayit2.GunSaat;
            }

            var kayit3 = ListeSirali[2];
            if (kayit3 != null)
            {
                Gun3Var = kayit3.GunVar;
                Gun3Baslik = kayit3.GunBaslik;
                Gun3BasTar = kayit3.GunBasTar;
                Gun3BitTar = kayit3.GunBitTar;
                Gun3BasSaat = kayit3.GunBasSaat;
                Gun3BitSaat = kayit3.GunBitSaat;
                Gun3Ucret = kayit3.GunUcret;
                Gun3Saat = kayit3.GunSaat;
            }

            var kayit4 = ListeSirali[3];
            if (kayit4 != null)
            {
                Gun4Var = kayit4.GunVar;
                Gun4Baslik = kayit4.GunBaslik;
                Gun4BasTar = kayit4.GunBasTar;
                Gun4BitTar = kayit4.GunBitTar;
                Gun4BasSaat = kayit4.GunBasSaat;
                Gun4BitSaat = kayit4.GunBitSaat;
                Gun4Ucret = kayit4.GunUcret;
                Gun4Saat = kayit4.GunSaat;
            }

            var kayit5 = ListeSirali[4];
            if (kayit5 != null)
            {
                Gun5Var = kayit5.GunVar;
                Gun5Baslik = kayit5.GunBaslik;
                Gun5BasTar = kayit5.GunBasTar;
                Gun5BitTar = kayit5.GunBitTar;
                Gun5BasSaat = kayit5.GunBasSaat;
                Gun5BitSaat = kayit5.GunBitSaat;
                Gun5Ucret = kayit5.GunUcret;
                Gun5Saat = kayit5.GunSaat;
            }

            var kayit6 = ListeSirali[5];
            if (kayit6 != null)
            {
                Gun6Var = kayit6.GunVar;
                Gun6Baslik = kayit6.GunBaslik;
                Gun6BasTar = kayit6.GunBasTar;
                Gun6BitTar = kayit6.GunBitTar;
                Gun6BasSaat = kayit6.GunBasSaat;
                Gun6BitSaat = kayit6.GunBitSaat;
                Gun6Ucret = kayit6.GunUcret;
                Gun6Saat = kayit6.GunSaat;
            }

            var kayit7 = ListeSirali[6];
            if (kayit7 != null)
            {
                Gun7Var = kayit7.GunVar;
                Gun7Baslik = kayit7.GunBaslik;
                Gun7BasTar = kayit7.GunBasTar;
                Gun7BitTar = kayit7.GunBitTar;
                Gun7BasSaat = kayit7.GunBasSaat;
                Gun7BitSaat = kayit7.GunBitSaat;
                Gun7Ucret = kayit7.GunUcret;
                Gun7Saat = kayit7.GunSaat;
            }
        }



        private void VerileriEkrandanAl()
        {



            DayOfWeek ilkgun = HF.BasTarih.DayOfWeek;

            if (ilkgun == DayOfWeek.Sunday)
            {
                //Pazar Günü Başlangıçlı(1)
                  HF.TarPPzt = Tarih1
                 ;
                 HF.BitTarPPzt = Tarih1B;
                //GunAciklama1 = "Pazar-Pazartesi";

                  HF.PPzt= GunVar1;

                  HF.BasSaatPPzt = BasSaat1;
                  HF.BitSaatPPzt = BitSaat1;
                  HF.NetSaatEllePPzt = FazlaSaat1;
                  HF.SaatlikPPzt = SaatUcret1;

                //Pazar Günü Başlangıçlı(2)

                HF.TarPSal = Tarih2;
                  HF.BitTarPSal = Tarih2B;
                //GunAciklama2 = "Pazartesi - Salı";

                  HF.PPzt = GunVar2;

                  HF.BasSaatPPzt= BasSaat2;
                  HF.BitSaatPPzt = BitSaat2;
                  HF.NetSaatEllePPzt = FazlaSaat2;
                  HF.SaatlikPPzt = SaatUcret2;


                //Pazar Günü Başlangıçlı(3)(salı)

                  HF.TarSCar = Tarih3;
                  HF.BitTarSCar = Tarih3B;
                //GunAciklama3 = "Salı - Çarşamba";

                  HF.SCar = GunVar3;

                  HF.BasSaatSCar = BasSaat3;
                  HF.BitSaatSCar = BitSaat3;
                  HF.NetSaatElleSCar = FazlaSaat3;
                  HF.SaatlikSCar = SaatUcret3;


                 //Pazar Günü Başlangıçlı(4)(Çar)

                   HF.TarSCar = Tarih4;
                  HF.BitTarSCar= Tarih4B;
                //GunAciklama4 = "Çarşamba - Perşembe";

                  HF.SCar = GunVar4;

                  HF.BasSaatCPer= BasSaat4;
                  HF.BitSaatCPer = BitSaat4;
                  HF.NetSaatElleCPer = FazlaSaat4;
                  HF.SaatlikCPer = SaatUcret4;


                //Pazar Günü Başlangıçlı(5)(Per)
                 HF.TarPCum = Tarih5;
                  HF.BitTarPCum = Tarih5B;
                //GunAciklama5 = "Perşembe - Cuma";

                  HF.PCum = GunVar5;

                   HF.BasSaatPCum = BasSaat5;
                  HF.BitSaatPCum = BitSaat5;
                  HF.NetSaatEllePCum = FazlaSaat5;
                 HF.SaatlikPCum = SaatUcret5;

                //Pazar Günü Başlangıçlı(6)(Cum)
                  HF.TarCCmt = Tarih6;
                  HF.BitTarCCmt = Tarih6B;
                //GunAciklama6 = "Cuma - Cumartesi";

                 HF.CCmt = GunVar6;

                 HF.BasSaatCCmt= BasSaat6;
                  HF.BitSaatCCmt = BitSaat6;
                 HF.NetSaatElleCCmt = FazlaSaat6;
                 HF.SaatlikCCmt = SaatUcret6;


                //Pazar Günü Başlangıçlı(7)(Cmtsi)
                  HF.TarCPzr = Tarih7;
                  HF.BitTarCPzr = Tarih7B;
                //GunAciklama7 = "Cumartesi - Pazar";

                  HF.CPzr= GunVar7;

                HF.BasSaatCPzr = BasSaat7;
                 HF.BitSaatCPzr = BitSaat7;
                 HF.NetSaatelleCPzr = FazlaSaat7;
                HF.SaatlikCPzr = SaatUcret7;

            }
            else if (ilkgun == DayOfWeek.Monday)
            {



                //Pazartes Günü Başlangıçlı(1)

                  HF.TarPSal = Tarih1;
                HF.BitTarPSal = Tarih1B;
                //GunAciklama1 = "Pazartesi - Salı";

                 HF.PPzt = GunVar1;

                 HF.BasSaatPPzt = BasSaat1;
                  HF.BitSaatPPzt = BitSaat1;
                HF.NetSaatEllePPzt = FazlaSaat1;
                  HF.SaatlikPPzt= SaatUcret1;


                //Pazartes Günü Başlangıçlı(2)(salı)

                HF.TarSCar = Tarih2;
                  HF.BitTarSCar = Tarih2B;
                //GunAciklama2 = "Salı - Çarşamba";

               HF.SCar = GunVar2;

                 HF.BasSaatSCar = BasSaat2;
                HF.BitSaatSCar =  BitSaat2;
                HF.NetSaatElleSCar = FazlaSaat2;
                 HF.SaatlikSCar = SaatUcret2;


                //Pazartes Günü Başlangıçlı(3)(Çar)

                  HF.TarSCar = Tarih3;
                 HF.BitTarSCar = Tarih3B;
                //GunAciklama3 = "Çarşamba - Perşembe";

                HF.SCar = GunVar3;

                HF.BasSaatCPer = BasSaat3;
                HF.BitSaatCPer = BitSaat3;
                HF.NetSaatElleCPer = FazlaSaat3;
                HF.SaatlikCPer= SaatUcret3;


                //Pazartesi Günü Başlangıçlı(4)(Per)
                  HF.TarPCum = Tarih4;
                HF.BitTarPCum = Tarih4B;
                //GunAciklama4 = "Perşembe - Cuma";

                  HF.PCum = GunVar4;

                  HF.BasSaatPCum = BasSaat4;
                  HF.BitSaatPCum = BitSaat4;
                  HF.NetSaatEllePCum = FazlaSaat4;
                  HF.SaatlikPCum = SaatUcret4;

                //Pazartesi Günü Başlangıçlı(5)(Cum)
                  HF.TarCCmt= Tarih5;
                 HF.BitTarCCmt = Tarih5B;
                //GunAciklama5 = "Cuma - Cumartesi";

               HF.CCmt = GunVar5;


               HF.BasSaatCCmt= BasSaat5;
               HF.BitSaatCCmt = BitSaat5;
               HF.NetSaatElleCCmt = FazlaSaat5;
               HF.SaatlikCCmt = SaatUcret5;


                //Pazartesi Günü Başlangıçlı(6)(Cmtsi)
                 HF.TarCPzr = Tarih6;
                HF.BitTarCPzr = Tarih6B;
                //GunAciklama6 = "Cumartesi - Pazar";

                 HF.CPzr = GunVar6;

                HF.BasSaatCPzr = BasSaat6;
                HF.BitSaatCPzr = BitSaat6;
                HF.NetSaatelleCPzr = FazlaSaat6;
                HF.SaatlikCPzr = SaatUcret6;



                //Pazarrt Günü Başlangıçlı(7)
                HF.TarPPzt = Tarih7;
                HF.BitTarPPzt = Tarih7B;
                //GunAciklama7 = "Pazar-Pazartesi";

                HF.PPzt= GunVar7;

                 HF.BasSaatPPzt= BasSaat7;
                  HF.BitSaatPPzt= BitSaat7;
                 HF.NetSaatEllePPzt= FazlaSaat7;
               HF.SaatlikPPzt = SaatUcret7;


            }
            else if (ilkgun == DayOfWeek.Tuesday)
            {


                //Salı Günü Başlangıçlı(1)(salı)

                  HF.TarSCar = Tarih1;
                  HF.BitTarSCar = Tarih1B;
                //GunAciklama1 = "Salı - Çarşamba";

                 HF.SCar = GunVar1;

                  HF.BasSaatSCar = BasSaat1;
                     HF.BitSaatSCar = BitSaat1;
                  HF.NetSaatElleSCar = FazlaSaat1;
                  HF.SaatlikSCar= SaatUcret1;


                //Salı Günü Başlangıçlı(2)(Çar)

                  HF.TarSCar = Tarih2;
                  HF.BitTarSCar = Tarih2B;
                //GunAciklama2 = "Çarşamba - Perşembe";

                  HF.SCar = GunVar2;

                  HF.BasSaatCPer = BasSaat2;
                  HF.BitSaatCPer = BitSaat2;
                  HF.NetSaatElleCPer = FazlaSaat2;
                  HF.SaatlikCPer = SaatUcret2; 


                //salı Günü Başlangıçlı(3)(Per)
                  HF.TarPCum = Tarih3;
                  HF.BitTarPCum = Tarih3B;
                //GunAciklama3 = "Perşembe - Cuma";

                  HF.PCum = GunVar3;

                  HF.BasSaatPCum = BasSaat3;
                  HF.BitSaatPCum = BitSaat3;
                  HF.NetSaatEllePCum = FazlaSaat3;
                  HF.SaatlikPCum = SaatUcret3;

                //salı Günü Başlangıçlı(4)(Cum)
                  HF.TarCCmt= Tarih4;
                  HF.BitTarCCmt= Tarih4B;
                //GunAciklama4 = "Cuma - Cumartesi";

                  HF.CCmt = GunVar4;

                  HF.BasSaatCCmt = BasSaat4;
                  HF.BitSaatCCmt = BitSaat4;
                  HF.NetSaatElleCCmt = FazlaSaat4;
                  HF.SaatlikCCmt = SaatUcret4;


                //salı Günü Başlangıçlı(5)(Cmtsi)
                  HF.TarCPzr = Tarih5;
                  HF.BitTarCPzr = Tarih5B;
                //GunAciklama5 = "Cumartesi - Pazar";

                  HF.CPzr = GunVar5;

                  HF.BasSaatCPzr = BasSaat5;
                  HF.BitSaatCPzr = BitSaat5;
                  HF.NetSaatelleCPzr = FazlaSaat5;
                  HF.SaatlikCPzr = SaatUcret5;



                //salı Günü Başlangıçlı(6)
                 HF.TarPPzt = Tarih6;
                  HF.BitTarPPzt = Tarih6B;
                //GunAciklama6 = "Pazar-Pazartesi";

                  HF.PPzt = GunVar6;

                  HF.BasSaatPPzt = BasSaat6;
                  HF.BitSaatPPzt = BitSaat6;
                  HF.NetSaatEllePPzt = FazlaSaat6;
                  HF.SaatlikPPzt = SaatUcret6;


                //Salı Günü Başlangıçlı(7)

                  HF.TarPSal = Tarih7;
                  HF.BitTarPSal = Tarih7B;
                //GunAciklama7 = "Pazartesi - Salı";

                  HF.PPzt = GunVar7;

                  HF.BasSaatPPzt = BasSaat7;
                  HF.BitSaatPPzt= BitSaat7;
                  HF.NetSaatEllePPzt = FazlaSaat7;
                 HF.SaatlikPPzt = SaatUcret7;

            }
            else if (ilkgun == DayOfWeek.Wednesday)
            {





                //Çarşamba Günü Başlangıçlı(1)(Çar)

                  HF.TarSCar = Tarih1;
                  HF.BitTarSCar = Tarih1B;
                //GunAciklama1 = "Çarşamba - Perşembe";

                  HF.SCar = GunVar1;

                  HF.BasSaatCPer = BasSaat1;
                  HF.BitSaatCPer = BitSaat1;
                 HF.NetSaatElleCPer = FazlaSaat1;
                  HF.SaatlikCPer = SaatUcret1;


                //Çarşamba Günü Başlangıçlı(2)(Per)
                  HF.TarPCum = Tarih2;
                  HF.BitTarPCum = Tarih2B;
                //GunAciklama2 = "Perşembe - Cuma";

                  HF.PCum = GunVar2;

                  HF.BasSaatPCum = BasSaat2;
                  HF.BitSaatPCum = BitSaat2;
                  HF.NetSaatEllePCum = FazlaSaat2;
                  HF.SaatlikPCum = SaatUcret2;

                //Çarşamba Günü Başlangıçlı(3)(Cum)
                  HF.TarCCmt = Tarih3; 
                  HF.BitTarCCmt = Tarih3B;
                //GunAciklama3 = "Cuma - Cumartesi";

                  HF.CCmt = GunVar3;

                  HF.BasSaatCCmt = BasSaat3;
                  HF.BitSaatCCmt = BitSaat3;
                  HF.NetSaatElleCCmt = FazlaSaat3;
                  HF.SaatlikCCmt = SaatUcret3;


                //Çarşamba Günü Başlangıçlı(4)(Cmtsi)
                  HF.TarCPzr = Tarih4;
                  HF.BitTarCPzr = Tarih4B;
                //GunAciklama4 = "Cumartesi - Pazar";

                  HF.CPzr = GunVar4;

                  HF.BasSaatCPzr = BasSaat4;
                  HF.BitSaatCPzr = BitSaat4;
                 HF.NetSaatelleCPzr = FazlaSaat4;
                  HF.SaatlikCPzr = SaatUcret4;



                //Çarşamba Günü Başlangıçlı(5)Pazar
                  HF.TarPPzt = Tarih5;
                  HF.BitTarPPzt= Tarih5B;
                //GunAciklama5 = "Pazar-Pazartesi";

                  HF.PPzt= GunVar5;

                  HF.BasSaatPPzt = BasSaat5;
                  HF.BitSaatPPzt = BitSaat5;
                  HF.NetSaatEllePPzt = FazlaSaat5;
                  HF.SaatlikPPzt = SaatUcret5;


                //Çarşamba Günü Başlangıçlı(6)

                  HF.TarPSal = Tarih6;
                  HF.BitTarPSal = Tarih6B;
                //GunAciklama6 = "Pazartesi - Salı";

                  HF.PPzt= GunVar6;

                    HF.BasSaatPPzt= BasSaat6;
                  HF.BitSaatPPzt= BitSaat6;
                  HF.NetSaatEllePPzt= FazlaSaat6;
                  HF.SaatlikPPzt= SaatUcret6;


                //Çarşamba Günü Başlangıçlı(7)()

                  HF.TarSCar = Tarih7;

                  HF.BitTarSCar= Tarih7B;
                //GunAciklama7 = "Salı - Çarşamba";

                  HF.SCar = GunVar7;

                  HF.BasSaatSCar = BasSaat7;
                  HF.BitSaatSCar = BitSaat7;
                  HF.NetSaatElleSCar = FazlaSaat7;
                  HF.SaatlikSCar = SaatUcret7;




            }

            else if (ilkgun == DayOfWeek.Thursday)
            {





                //Perşembe Günü Başlangıçlı(1)
                  HF.TarPCum= Tarih1;
                  HF.BitTarPCum= Tarih1B;
                //GunAciklama1 = "Perşembe - Cuma";

                  HF.PCum= GunVar1;

                  HF.BasSaatPCum= BasSaat1;
                  HF.BitSaatPCum = BitSaat1;
                  HF.NetSaatEllePCum = FazlaSaat1;
                  HF.SaatlikPCum = SaatUcret1;

                //Perşembe Günü Başlangıçlı(2)(Cum)
                  HF.TarCCmt = Tarih2;
                  HF.BitTarCCmt= Tarih2B;
                //GunAciklama2 = "Cuma - Cumartesi";

                  HF.CCmt = GunVar2;

                  HF.BasSaatCCmt= BasSaat2;
                  HF.BitSaatCCmt= BitSaat2;
                  HF.NetSaatElleCCmt= FazlaSaat2;
                  HF.SaatlikCCmt = SaatUcret2;


                //Perşembe Günü Başlangıçlı(3).(Cmtsi)
                  HF.TarCPzr = Tarih3;
                  HF.BitTarCPzr= Tarih3B;
                //GunAciklama3 = "Cumartesi - Pazar";

                  HF.CPzr = GunVar3;

                  HF.BasSaatCPzr = BasSaat3;
                  HF.BitSaatCPzr = BitSaat3;
                  HF.NetSaatelleCPzr= FazlaSaat3;
                  HF.SaatlikCPzr = SaatUcret3;



                //Perşembe Günü Başlangıçlı(4)Pazar
                HF.TarPPzt = Tarih4;
                  HF.BitTarPPzt = Tarih4B;
                //GunAciklama4 = "Pazar-Pazartesi";

                  HF.PPzt = GunVar4;

                  HF.BasSaatPPzt  = BasSaat4;
                  HF.BitSaatPPzt = BitSaat4;
                  HF.NetSaatEllePPzt = FazlaSaat4;
                  HF.SaatlikPPzt = SaatUcret4;


                //Perşembe Günü Başlangıçlı(5)

                  HF.TarPSal = Tarih5;
                  HF.BitTarPSal = Tarih5B;
                //GunAciklama5 = "Pazartesi - Salı";

                HF.PPzt = GunVar5;

                  HF.BasSaatPPzt = BasSaat5;
                  HF.BitSaatPPzt = BitSaat5;
                  HF.NetSaatEllePPzt = FazlaSaat5;
                  HF.SaatlikPPzt = SaatUcret5;


                //Perşembe Günü Başlangıçlı(6)()

              HF.TarSCar = Tarih6;
                  HF.BitTarSCar= Tarih6B;
                //GunAciklama6 = "Salı - Çarşamba";

                  HF.SCar = GunVar6;

                HF.BasSaatSCar = BasSaat6;
                 HF.BitSaatSCar = BitSaat6;
                  HF.NetSaatElleSCar = FazlaSaat6;
                  HF.SaatlikSCar = SaatUcret6;


                //Perşembe Günü Başlangıçlı(7)(Çar)

                  HF.TarSCar = Tarih7;
                  HF.BitTarSCar = Tarih7B;
                //GunAciklama7 = "Çarşamba - Perşembe";

                  HF.SCar = GunVar7;

                  HF.BasSaatCPer = BasSaat7;
                  HF.BitSaatCPer = BitSaat7;
                  HF.NetSaatElleCPer = FazlaSaat7;
                  HF.SaatlikCPer = SaatUcret7;


            }
            else if (ilkgun == DayOfWeek.Friday)
            {



                //Cuma Günü Başlangıçlı(1)(Cum)
                  HF.TarCCmt = Tarih1;
                  HF.BitTarCCmt = Tarih1B; 
                //GunAciklama1 = "Cuma - Cumartesi";

                  HF.CCmt = GunVar1;

                HF.BasSaatCCmt = BasSaat1;
                 HF.BitSaatCCmt = BitSaat1;
                 HF.NetSaatElleCCmt = FazlaSaat1;
                HF.SaatlikCCmt = SaatUcret1;


                //Cuma Günü Başlangıçlı(2).(Cmtsi)
                  HF.TarCPzr = Tarih2;
                  HF.BitTarCPzr = Tarih2B;
                //GunAciklama2 = "Cumartesi - Pazar";

                  HF.CPzr = GunVar2;

                 HF.BasSaatCPzr = BasSaat2;
                 HF.BitSaatCPzr = BitSaat2;
                 HF.NetSaatelleCPzr = FazlaSaat2;
                 HF.SaatlikCPzr = SaatUcret2;



                //Cuma Günü Başlangıçlı(3)Pazar
                 HF.TarPPzt = Tarih3;
                 HF.BitTarPPzt = Tarih3B;
                //GunAciklama3 = "Pazar-Pazartesi";

                  HF.PPzt = GunVar3;

                 HF.BasSaatPPzt = BasSaat3;
                 HF.BitSaatPPzt = BitSaat3;
                  HF.NetSaatEllePPzt = FazlaSaat3;
                 HF.SaatlikPPzt = SaatUcret3;


                //Cuma Günü Başlangıçlı(4)

                  HF.TarPSal = Tarih4;
                  HF.BitTarPSal = Tarih4B;
                //GunAciklama4 = "Pazartesi - Salı";

                 HF.PPzt = GunVar4;

                 HF.BasSaatPPzt = BasSaat4;
                 HF.BitSaatPPzt = BitSaat4;
                HF.NetSaatEllePPzt = FazlaSaat4;
                 HF.SaatlikPPzt = SaatUcret4;


                //Cuma Günü Başlangıçlı(5)()

                 HF.TarSCar = Tarih5;
                 HF.BitTarSCar = Tarih5B;
                //GunAciklama5 = "Salı - Çarşamba";

                 HF.SCar = GunVar5;

                 HF.BasSaatSCar = BasSaat5;
                 HF.BitSaatSCar = BitSaat5;
                 HF.NetSaatElleSCar = FazlaSaat5;
                 HF.SaatlikSCar = SaatUcret5;


                //Cuma Günü Başlangıçlı(6)(Çar)

                HF.TarSCar = Tarih6;
                HF.BitTarSCar  = Tarih6B;
                //GunAciklama6 = "Çarşamba - Perşembe";

                 HF.SCar = GunVar6;

                HF.BasSaatCPer = BasSaat6;
                HF.BitSaatCPer = BitSaat6;
                HF.NetSaatElleCPer= FazlaSaat6;
                HF.SaatlikCPer = SaatUcret6;


                //Cuma Günü Başlangıçlı(7)
                HF.TarPCum= Tarih7;
                HF.BitTarPCum = Tarih7B;
                //GunAciklama7 = "Perşembe - Cuma";

                 HF.PCum = GunVar7;

                HF.BasSaatPCum = BasSaat7;
                HF.BitSaatPCum = BitSaat7;
                HF.NetSaatEllePCum = FazlaSaat7;
                HF.SaatlikPCum = SaatUcret7;

            }

            else if (ilkgun == DayOfWeek.Saturday)
            {





                //Cumartesi Günü Başlangıçlı(1).(Cmtsi)
                  HF.TarCPzr= Tarih1;
                  HF.BitTarCPzr = Tarih1B;
                //GunAciklama1 = "Cumartesi - Pazar";

                  HF.CPzr = GunVar1;

                  HF.BasSaatCPzr = BasSaat1;
                  HF.BitSaatCPzr = BitSaat1;
                 HF.NetSaatelleCPzr = FazlaSaat1;
                 HF.SaatlikCPzr = SaatUcret1;



                //Cumartesi Günü Başlangıçlı(2)Pazar
                 HF.TarPPzt = Tarih2;
                 HF.BitTarPPzt = Tarih2B;
                //GunAciklama2 = "Pazar-Pazartesi";

                 HF.PPzt = GunVar2;

                HF.BasSaatPPzt = BasSaat2;
                HF.BitSaatPPzt= BitSaat2;
                HF.NetSaatEllePPzt = FazlaSaat2;
                HF.SaatlikPPzt = SaatUcret2;


                //Cumartesi Günü Başlangıçlı(3)

                HF.TarPSal = Tarih3; 
                HF.BitTarPSal = Tarih3B;
                //GunAciklama3 = "Pazartesi - Salı";

                 HF.PPzt = GunVar3;

                 HF.BasSaatPPzt = BasSaat3;
                 HF.BitSaatPPzt = BitSaat3;
                 HF.NetSaatEllePPzt = FazlaSaat3;
                 HF.SaatlikPPzt = SaatUcret3;


                //Cumartesi Günü Başlangıçlı(4)()

                 HF.TarSCar = Tarih4;
                 HF.BitTarSCar = Tarih4B;
                //GunAciklama4 = "Salı - Çarşamba";

                 HF.SCar = GunVar4;

                HF.BasSaatSCar = BasSaat4;
                HF.BitSaatSCar = BitSaat4;
                HF.NetSaatElleSCar = FazlaSaat4;
                HF.SaatlikSCar = SaatUcret4;


                //Cumartesi Günü Başlangıçlı(5)(Çar)

                HF.TarSCar = Tarih5;
                HF.BitTarSCar = Tarih5B;
                //GunAciklama5 = "Çarşamba - Perşembe";

                HF.SCar = GunVar5;

                HF.BasSaatCPer = BasSaat5;
                HF.BitSaatCPer = BitSaat5;
                HF.NetSaatElleCPer = FazlaSaat5;
                HF.SaatlikCPer = SaatUcret5;


                //Cumartesi Günü Başlangıçlı(6)
                  HF.TarPCum = Tarih6;
                 HF.BitTarPCum = Tarih6B;
                //GunAciklama6 = "Perşembe - Cuma";

                  HF.PCum  = GunVar6;

                HF.BasSaatPCum = BasSaat6;
                HF.BitSaatPCum = BitSaat6;
                HF.NetSaatEllePCum = FazlaSaat6;
                HF.SaatlikPCum = SaatUcret6;



                //Cumartesi Günü Başlangıçlı(1)(Cum)
                HF.TarCCmt = Tarih7;
                HF.BitTarCCmt = Tarih7B;
                //GunAciklama7 = "Cuma - Cumartesi";

                 HF.CCmt = GunVar7;

                HF.BasSaatCCmt = BasSaat7;
                HF.BitSaatCCmt = BitSaat7;
                HF.NetSaatElleCCmt = FazlaSaat7;
                 HF.SaatlikCCmt = SaatUcret7;


            }






        }

        private void VerileriEkranaAktar()
        {
            //----
            DayOfWeek ilkgun = HF.BasTarih.DayOfWeek;
            if (ilkgun == DayOfWeek.Sunday)
            {
                //Pazar Günü Başlangıçlı(1)
                Tarih1 = HF.TarPPzt;
                Tarih1B = HF.BitTarPPzt;
                GunAciklama1 = "Pazar-Pazartesi";

                GunVar1 = HF.PPzt;

                BasSaat1 = HF.BasSaatPPzt;
                BitSaat1 = HF.BitSaatPPzt;
                FazlaSaat1 = HF.NetSaatEllePPzt;
                SaatUcret1 = HF.SaatlikPPzt;

                //Pazar Günü Başlangıçlı(2)

                Tarih2 = HF.TarPSal;
                Tarih2B = HF.BitTarPSal;
                GunAciklama2 = "Pazartesi - Salı";

                GunVar2 = HF.PPzt;

                BasSaat2 = HF.BasSaatPPzt;
                BitSaat2 = HF.BitSaatPPzt;
                FazlaSaat2 = HF.NetSaatEllePPzt;
                SaatUcret2 = HF.SaatlikPPzt;


                //Pazar Günü Başlangıçlı(3)(salı)

                Tarih3 = HF.TarSCar;
                Tarih3B = HF.BitTarSCar;
                GunAciklama3 = "Salı - Çarşamba";

                GunVar3 = HF.SCar;

                BasSaat3 = HF.BasSaatSCar;
                BitSaat3 = HF.BitSaatSCar;
                FazlaSaat3 = HF.NetSaatElleSCar;
                SaatUcret3 = HF.SaatlikSCar;


                //Pazar Günü Başlangıçlı(4)(Çar)

                Tarih4 = HF.TarSCar;
                Tarih4B = HF.BitTarSCar;
                GunAciklama4 = "Çarşamba - Perşembe";

                GunVar4 = HF.SCar;

                BasSaat4 = HF.BasSaatCPer;
                BitSaat4 = HF.BitSaatCPer;
                FazlaSaat4 = HF.NetSaatElleCPer;
                SaatUcret4 = HF.SaatlikCPer;


                //Pazar Günü Başlangıçlı(5)(Per)
                Tarih5 = HF.TarPCum;
                Tarih5B = HF.BitTarPCum;
                GunAciklama5 = "Perşembe - Cuma";

                GunVar5 = HF.PCum;

                BasSaat5 = HF.BasSaatPCum;
                BitSaat5 = HF.BitSaatPCum;
                FazlaSaat5 = HF.NetSaatEllePCum;
                SaatUcret5 = HF.SaatlikPCum;

                //Pazar Günü Başlangıçlı(6)(Cum)
                Tarih6 = HF.TarCCmt;
                Tarih6B = HF.BitTarCCmt;
                GunAciklama6 = "Cuma - Cumartesi";

                GunVar6 = HF.CCmt;

                BasSaat6 = HF.BasSaatCCmt;
                BitSaat6 = HF.BitSaatCCmt;
                FazlaSaat6 = HF.NetSaatElleCCmt;
                SaatUcret6 = HF.SaatlikCCmt;


                //Pazar Günü Başlangıçlı(7)(Cmtsi)
                Tarih7 = HF.TarCPzr;
                Tarih7B = HF.BitTarCPzr;
                GunAciklama7 = "Cumartesi - Pazar";

                GunVar7 = HF.CPzr;

                BasSaat7 = HF.BasSaatCPzr;
                BitSaat7 = HF.BitSaatCPzr;
                FazlaSaat7 = HF.NetSaatelleCPzr;
                SaatUcret7 = HF.SaatlikCPzr;

            }
            else if (ilkgun == DayOfWeek.Monday)
            {



                //Pazartes Günü Başlangıçlı(1)

                Tarih1 = HF.TarPSal;
                Tarih1B = HF.BitTarPSal;
                GunAciklama1 = "Pazartesi - Salı";

                GunVar1 = HF.PPzt;

                BasSaat1 = HF.BasSaatPPzt;
                BitSaat1 = HF.BitSaatPPzt;
                FazlaSaat1 = HF.NetSaatEllePPzt;
                SaatUcret1 = HF.SaatlikPPzt;


                //Pazartes Günü Başlangıçlı(2)(salı)

                Tarih2 = HF.TarSCar;
                Tarih2B = HF.BitTarSCar;
                GunAciklama2 = "Salı - Çarşamba";

                GunVar2 = HF.SCar;

                BasSaat2 = HF.BasSaatSCar;
                BitSaat2 = HF.BitSaatSCar;
                FazlaSaat2 = HF.NetSaatElleSCar;
                SaatUcret2 = HF.SaatlikSCar;


                //Pazartes Günü Başlangıçlı(3)(Çar)

                Tarih3 = HF.TarSCar;
                Tarih3B = HF.BitTarSCar;
                GunAciklama3 = "Çarşamba - Perşembe";

                GunVar3 = HF.SCar;

                BasSaat3 = HF.BasSaatCPer;
                BitSaat3 = HF.BitSaatCPer;
                FazlaSaat3 = HF.NetSaatElleCPer;
                SaatUcret3 = HF.SaatlikCPer;


                //Pazartesi Günü Başlangıçlı(4)(Per)
                Tarih4 = HF.TarPCum;
                Tarih4B = HF.BitTarPCum;
                GunAciklama4 = "Perşembe - Cuma";

                GunVar4 = HF.PCum;

                BasSaat4 = HF.BasSaatPCum;
                BitSaat4 = HF.BitSaatPCum;
                FazlaSaat4 = HF.NetSaatEllePCum;
                SaatUcret4 = HF.SaatlikPCum;

                //Pazartesi Günü Başlangıçlı(5)(Cum)
                Tarih5 = HF.TarCCmt;
                Tarih5B = HF.BitTarCCmt;
                GunAciklama5 = "Cuma - Cumartesi";

                GunVar5 = HF.CCmt;

                BasSaat5 = HF.BasSaatCCmt;
                BitSaat5 = HF.BitSaatCCmt;
                FazlaSaat5 = HF.NetSaatElleCCmt;
                SaatUcret5 = HF.SaatlikCCmt;


                //Pazartesi Günü Başlangıçlı(6)(Cmtsi)
                Tarih6 = HF.TarCPzr;
                Tarih6B = HF.BitTarCPzr;
                GunAciklama6 = "Cumartesi - Pazar";

                GunVar6 = HF.CPzr;

                BasSaat6 = HF.BasSaatCPzr;
                BitSaat6 = HF.BitSaatCPzr;
                FazlaSaat6 = HF.NetSaatelleCPzr;
                SaatUcret6 = HF.SaatlikCPzr;



                //Pazarrt Günü Başlangıçlı(7)
                Tarih7 = HF.TarPPzt;
                Tarih7B = HF.BitTarPPzt;
                GunAciklama7 = "Pazar-Pazartesi";

                GunVar7 = HF.PPzt;

                BasSaat7 = HF.BasSaatPPzt;
                BitSaat7 = HF.BitSaatPPzt;
                FazlaSaat7 = HF.NetSaatEllePPzt;
                SaatUcret7 = HF.SaatlikPPzt;


            }
            else if (ilkgun == DayOfWeek.Tuesday)
            {


                //Salı Günü Başlangıçlı(1)(salı)

                Tarih1 = HF.TarSCar;
                Tarih1B = HF.BitTarSCar;
                GunAciklama1 = "Salı - Çarşamba";

                GunVar1 = HF.SCar;

                BasSaat1 = HF.BasSaatSCar;
                BitSaat1 = HF.BitSaatSCar;
                FazlaSaat1 = HF.NetSaatElleSCar;
                SaatUcret1 = HF.SaatlikSCar;


                //Salı Günü Başlangıçlı(2)(Çar)

                Tarih2 = HF.TarSCar;
                Tarih2B = HF.BitTarSCar;
                GunAciklama2 = "Çarşamba - Perşembe";

                GunVar2 = HF.SCar;

                BasSaat2 = HF.BasSaatCPer;
                BitSaat2 = HF.BitSaatCPer;
                FazlaSaat2 = HF.NetSaatElleCPer;
                SaatUcret2 = HF.SaatlikCPer;


                //salı Günü Başlangıçlı(3)(Per)
                Tarih3 = HF.TarPCum;
                Tarih3B = HF.BitTarPCum;
                GunAciklama3 = "Perşembe - Cuma";

                GunVar3 = HF.PCum;

                BasSaat3 = HF.BasSaatPCum;
                BitSaat3 = HF.BitSaatPCum;
                FazlaSaat3 = HF.NetSaatEllePCum;
                SaatUcret3 = HF.SaatlikPCum;

                //salı Günü Başlangıçlı(4)(Cum)
                Tarih4 = HF.TarCCmt;
                Tarih4B = HF.BitTarCCmt;
                GunAciklama4 = "Cuma - Cumartesi";

                GunVar4 = HF.CCmt;

                BasSaat4 = HF.BasSaatCCmt;
                BitSaat4 = HF.BitSaatCCmt;
                FazlaSaat4 = HF.NetSaatElleCCmt;
                SaatUcret4 = HF.SaatlikCCmt;


                //salı Günü Başlangıçlı(5)(Cmtsi)
                Tarih5 = HF.TarCPzr;
                Tarih5B = HF.BitTarCPzr;
                GunAciklama5 = "Cumartesi - Pazar";

                GunVar5 = HF.CPzr;

                BasSaat5 = HF.BasSaatCPzr;
                BitSaat5 = HF.BitSaatCPzr;
                FazlaSaat5 = HF.NetSaatelleCPzr;
                SaatUcret5 = HF.SaatlikCPzr;



                //salı Günü Başlangıçlı(6)
                Tarih6 = HF.TarPPzt;
                Tarih6B = HF.BitTarPPzt;
                GunAciklama6 = "Pazar-Pazartesi";

                GunVar6 = HF.PPzt;

                BasSaat6 = HF.BasSaatPPzt;
                BitSaat6 = HF.BitSaatPPzt;
                FazlaSaat6 = HF.NetSaatEllePPzt;
                SaatUcret6 = HF.SaatlikPPzt;


                //Salı Günü Başlangıçlı(7)

                Tarih7 = HF.TarPSal;
                Tarih7B = HF.BitTarPSal;
                GunAciklama7 = "Pazartesi - Salı";

                GunVar7 = HF.PPzt;

                BasSaat7 = HF.BasSaatPPzt;
                BitSaat7 = HF.BitSaatPPzt;
                FazlaSaat7 = HF.NetSaatEllePPzt;
                SaatUcret7 = HF.SaatlikPPzt;

            }
            else if (ilkgun == DayOfWeek.Wednesday)
            {





                //Çarşamba Günü Başlangıçlı(1)(Çar)

                Tarih1 = HF.TarSCar;
                Tarih1B = HF.BitTarSCar;
                GunAciklama1 = "Çarşamba - Perşembe";

                GunVar1 = HF.SCar;

                BasSaat1 = HF.BasSaatCPer;
                BitSaat1 = HF.BitSaatCPer;
                FazlaSaat1 = HF.NetSaatElleCPer;
                SaatUcret1 = HF.SaatlikCPer;


                //Çarşamba Günü Başlangıçlı(2)(Per)
                Tarih2 = HF.TarPCum;
                Tarih2B = HF.BitTarPCum;
                GunAciklama2 = "Perşembe - Cuma";

                GunVar2 = HF.PCum;

                BasSaat2 = HF.BasSaatPCum;
                BitSaat2 = HF.BitSaatPCum;
                FazlaSaat2 = HF.NetSaatEllePCum;
                SaatUcret2 = HF.SaatlikPCum;

                //Çarşamba Günü Başlangıçlı(3)(Cum)
                Tarih3 = HF.TarCCmt;
                Tarih3B = HF.BitTarCCmt;
                GunAciklama3 = "Cuma - Cumartesi";

                GunVar3 = HF.CCmt;

                BasSaat3 = HF.BasSaatCCmt;
                BitSaat3 = HF.BitSaatCCmt;
                FazlaSaat3 = HF.NetSaatElleCCmt;
                SaatUcret3 = HF.SaatlikCCmt;


                //Çarşamba Günü Başlangıçlı(4)(Cmtsi)
                Tarih4 = HF.TarCPzr;
                Tarih4B = HF.BitTarCPzr;
                GunAciklama4 = "Cumartesi - Pazar";

                GunVar4 = HF.CPzr;

                BasSaat4 = HF.BasSaatCPzr;
                BitSaat4 = HF.BitSaatCPzr;
                FazlaSaat4 = HF.NetSaatelleCPzr;
                SaatUcret4 = HF.SaatlikCPzr;



                //Çarşamba Günü Başlangıçlı(5)Pazar
                Tarih5 = HF.TarPPzt;
                Tarih5B = HF.BitTarPPzt;
                GunAciklama5 = "Pazar-Pazartesi";

                GunVar5 = HF.PPzt;

                BasSaat5 = HF.BasSaatPPzt;
                BitSaat5 = HF.BitSaatPPzt;
                FazlaSaat5 = HF.NetSaatEllePPzt;
                SaatUcret5 = HF.SaatlikPPzt;


                //Çarşamba Günü Başlangıçlı(6)

                Tarih6 = HF.TarPSal;
                Tarih6B = HF.BitTarPSal;
                GunAciklama6 = "Pazartesi - Salı";

                GunVar6 = HF.PPzt;

                BasSaat6 = HF.BasSaatPPzt;
                BitSaat6 = HF.BitSaatPPzt;
                FazlaSaat6 = HF.NetSaatEllePPzt;
                SaatUcret6 = HF.SaatlikPPzt;


                //Çarşamba Günü Başlangıçlı(7)()

                Tarih7 = HF.TarSCar;
                Tarih7B = HF.BitTarSCar;
                GunAciklama7 = "Salı - Çarşamba";

                GunVar7 = HF.SCar;

                BasSaat7 = HF.BasSaatSCar;
                BitSaat7 = HF.BitSaatSCar;
                FazlaSaat7 = HF.NetSaatElleSCar;
                SaatUcret7 = HF.SaatlikSCar;




            }

            else if (ilkgun == DayOfWeek.Thursday)
            {





                //Perşembe Günü Başlangıçlı(1)
                Tarih1 = HF.TarPCum;
                Tarih1B = HF.BitTarPCum;
                GunAciklama1 = "Perşembe - Cuma";

                GunVar1 = HF.PCum;

                BasSaat1 = HF.BasSaatPCum;
                BitSaat1 = HF.BitSaatPCum;
                FazlaSaat1 = HF.NetSaatEllePCum;
                SaatUcret1 = HF.SaatlikPCum;

                //Perşembe Günü Başlangıçlı(2)(Cum)
                Tarih2 = HF.TarCCmt;
                Tarih2B = HF.BitTarCCmt;
                GunAciklama2 = "Cuma - Cumartesi";

                GunVar2 = HF.CCmt;

                BasSaat2 = HF.BasSaatCCmt;
                BitSaat2 = HF.BitSaatCCmt;
                FazlaSaat2 = HF.NetSaatElleCCmt;
                SaatUcret2 = HF.SaatlikCCmt;


                //Perşembe Günü Başlangıçlı(3).(Cmtsi)
                Tarih3 = HF.TarCPzr;
                Tarih3B = HF.BitTarCPzr;
                GunAciklama3 = "Cumartesi - Pazar";

                GunVar3 = HF.CPzr;

                BasSaat3 = HF.BasSaatCPzr;
                BitSaat3 = HF.BitSaatCPzr;
                FazlaSaat3 = HF.NetSaatelleCPzr;
                SaatUcret3 = HF.SaatlikCPzr;



                //Perşembe Günü Başlangıçlı(4)Pazar
                Tarih4 = HF.TarPPzt;
                Tarih4B = HF.BitTarPPzt;
                GunAciklama4 = "Pazar-Pazartesi";

                GunVar4 = HF.PPzt;

                BasSaat4 = HF.BasSaatPPzt;
                BitSaat4 = HF.BitSaatPPzt;
                FazlaSaat4 = HF.NetSaatEllePPzt;
                SaatUcret4 = HF.SaatlikPPzt;


                //Perşembe Günü Başlangıçlı(5)

                Tarih5 = HF.TarPSal;
                Tarih5B = HF.BitTarPSal;
                GunAciklama5 = "Pazartesi - Salı";

                GunVar5 = HF.PPzt;

                BasSaat5 = HF.BasSaatPPzt;
                BitSaat5 = HF.BitSaatPPzt;
                FazlaSaat5 = HF.NetSaatEllePPzt;
                SaatUcret5 = HF.SaatlikPPzt;


                //Perşembe Günü Başlangıçlı(6)()

                Tarih6 = HF.TarSCar;
                Tarih6B = HF.BitTarSCar;
                GunAciklama6 = "Salı - Çarşamba";

                GunVar6 = HF.SCar;

                BasSaat6 = HF.BasSaatSCar;
                BitSaat6 = HF.BitSaatSCar;
                FazlaSaat6 = HF.NetSaatElleSCar;
                SaatUcret6 = HF.SaatlikSCar;


                //Perşembe Günü Başlangıçlı(7)(Çar)

                Tarih7 = HF.TarSCar;
                Tarih7B = HF.BitTarSCar;
                GunAciklama7 = "Çarşamba - Perşembe";

                GunVar7 = HF.SCar;

                BasSaat7 = HF.BasSaatCPer;
                BitSaat7 = HF.BitSaatCPer;
                FazlaSaat7 = HF.NetSaatElleCPer;
                SaatUcret7 = HF.SaatlikCPer;


            }
            else if (ilkgun == DayOfWeek.Friday)
            {



                //Cuma Günü Başlangıçlı(1)(Cum)
                Tarih1 = HF.TarCCmt;
                Tarih1B = HF.BitTarCCmt;
                GunAciklama1 = "Cuma - Cumartesi";

                GunVar1 = HF.CCmt;

                BasSaat1 = HF.BasSaatCCmt;
                BitSaat1 = HF.BitSaatCCmt;
                FazlaSaat1 = HF.NetSaatElleCCmt;
                SaatUcret1 = HF.SaatlikCCmt;


                //Cuma Günü Başlangıçlı(2).(Cmtsi)
                Tarih2 = HF.TarCPzr;
                Tarih2B = HF.BitTarCPzr;
                GunAciklama2 = "Cumartesi - Pazar";

                GunVar2 = HF.CPzr;

                BasSaat2 = HF.BasSaatCPzr;
                BitSaat2 = HF.BitSaatCPzr;
                FazlaSaat2 = HF.NetSaatelleCPzr;
                SaatUcret2 = HF.SaatlikCPzr;



                //Cuma Günü Başlangıçlı(3)Pazar
                Tarih3 = HF.TarPPzt;
                Tarih3B = HF.BitTarPPzt;
                GunAciklama3 = "Pazar-Pazartesi";

                GunVar3 = HF.PPzt;

                BasSaat3 = HF.BasSaatPPzt;
                BitSaat3 = HF.BitSaatPPzt;
                FazlaSaat3 = HF.NetSaatEllePPzt;
                SaatUcret3 = HF.SaatlikPPzt;


                //Cuma Günü Başlangıçlı(4)

                Tarih4 = HF.TarPSal;
                Tarih4B = HF.BitTarPSal;
                GunAciklama4 = "Pazartesi - Salı";

                GunVar4 = HF.PPzt;

                BasSaat4 = HF.BasSaatPPzt;
                BitSaat4 = HF.BitSaatPPzt;
                FazlaSaat4 = HF.NetSaatEllePPzt;
                SaatUcret4 = HF.SaatlikPPzt;


                //Cuma Günü Başlangıçlı(5)()

                Tarih5 = HF.TarSCar;
                Tarih5B = HF.BitTarSCar;
                GunAciklama5 = "Salı - Çarşamba";

                GunVar5 = HF.SCar;

                BasSaat5 = HF.BasSaatSCar;
                BitSaat5 = HF.BitSaatSCar;
                FazlaSaat5 = HF.NetSaatElleSCar;
                SaatUcret5 = HF.SaatlikSCar;


                //Cuma Günü Başlangıçlı(6)(Çar)

                Tarih6 = HF.TarSCar;
                Tarih6B = HF.BitTarSCar;
                GunAciklama6 = "Çarşamba - Perşembe";

                GunVar6 = HF.SCar;

                BasSaat6 = HF.BasSaatCPer;
                BitSaat6 = HF.BitSaatCPer;
                FazlaSaat6 = HF.NetSaatElleCPer;
                SaatUcret6 = HF.SaatlikCPer;


                //Cuma Günü Başlangıçlı(7)
                Tarih7 = HF.TarPCum;
                Tarih7B = HF.BitTarPCum;
                GunAciklama7 = "Perşembe - Cuma";

                GunVar7 = HF.PCum;

                BasSaat7 = HF.BasSaatPCum;
                BitSaat7 = HF.BitSaatPCum;
                FazlaSaat7 = HF.NetSaatEllePCum;
                SaatUcret7 = HF.SaatlikPCum;

            }

            else if (ilkgun == DayOfWeek.Saturday)
            {





                //Cumartesi Günü Başlangıçlı(1).(Cmtsi)
                Tarih1 = HF.TarCPzr;
                Tarih1B = HF.BitTarCPzr;
                GunAciklama1 = "Cumartesi - Pazar";

                GunVar1= HF.CPzr;

                BasSaat1 = HF.BasSaatCPzr;
                BitSaat1 = HF.BitSaatCPzr;
                FazlaSaat1 = HF.NetSaatelleCPzr;
                SaatUcret1 = HF.SaatlikCPzr;



                //Cumartesi Günü Başlangıçlı(2)Pazar
                Tarih2 = HF.TarPPzt;
                Tarih2B = HF.BitTarPPzt;
                GunAciklama2 = "Pazar-Pazartesi";

                GunVar2 = HF.PPzt;

                BasSaat2 = HF.BasSaatPPzt;
                BitSaat2 = HF.BitSaatPPzt;
                FazlaSaat2 = HF.NetSaatEllePPzt;
                SaatUcret2 = HF.SaatlikPPzt;


                //Cumartesi Günü Başlangıçlı(3)

                Tarih3 = HF.TarPSal;
                Tarih3B = HF.BitTarPSal;
                GunAciklama3 = "Pazartesi - Salı";

                GunVar3 = HF.PPzt;

                BasSaat3 = HF.BasSaatPPzt;
                BitSaat3 = HF.BitSaatPPzt;
                FazlaSaat3 = HF.NetSaatEllePPzt;
                SaatUcret3 = HF.SaatlikPPzt;


                //Cumartesi Günü Başlangıçlı(4)()

                Tarih4 = HF.TarSCar;
                Tarih4B = HF.BitTarSCar;
                GunAciklama4 = "Salı - Çarşamba";

                GunVar4 = HF.SCar;

                BasSaat4 = HF.BasSaatSCar;
                BitSaat4 = HF.BitSaatSCar;
                FazlaSaat4 = HF.NetSaatElleSCar;
                SaatUcret4 = HF.SaatlikSCar;


                //Cumartesi Günü Başlangıçlı(5)(Çar)

                Tarih5 = HF.TarSCar;
                Tarih5B = HF.BitTarSCar;
                GunAciklama5 = "Çarşamba - Perşembe";

                GunVar5 = HF.SCar;

                BasSaat5 = HF.BasSaatCPer;
                BitSaat5 = HF.BitSaatCPer;
                FazlaSaat5 = HF.NetSaatElleCPer;
                SaatUcret5 = HF.SaatlikCPer;


                //Cumartesi Günü Başlangıçlı(6)
                Tarih6= HF.TarPCum;
                Tarih6B = HF.BitTarPCum;
                GunAciklama6 = "Perşembe - Cuma";

                GunVar6 = HF.PCum;

                BasSaat6 = HF.BasSaatPCum;
                BitSaat6 = HF.BitSaatPCum;
                FazlaSaat6 = HF.NetSaatEllePCum;
                SaatUcret6 = HF.SaatlikPCum;



                //Cumartesi Günü Başlangıçlı(1)(Cum)
                Tarih7 = HF.TarCCmt;
                Tarih7B = HF.BitTarCCmt;
                GunAciklama7 = "Cuma - Cumartesi";

                GunVar7 = HF.CCmt;

                BasSaat7 = HF.BasSaatCCmt;
                BitSaat7 = HF.BitSaatCCmt;
                FazlaSaat7 = HF.NetSaatElleCCmt;
                SaatUcret7 = HF.SaatlikCCmt;


            }

        }

        // EKRAN GÖRÜNTÜLERİ BİLGİLERİ......
        //----------------------------------------------

        // 1. Gün
        private bool _gunVar1;
        public bool GunVar1
        {
            get => _gunVar1;
            set
            {
                _gunVar1 = value;
                OnPropertyChanged();
            }
        }

        private bool _gun1Gorunur;
        public bool Gun1Gorunur
        {
            get => _gun1Gorunur;
            set
            {
                _gun1Gorunur = value;
                OnPropertyChanged();
            }
        }

        private DateTime _tarih1;
        public DateTime Tarih1
        {
            get => _tarih1;
            set
            {
                _tarih1 = value;
                OnPropertyChanged();
            }
        }

        private DateTime _tarih1B;
        public DateTime Tarih1B
        {
            get => _tarih1B;
            set
            {
                _tarih1B = value;
                OnPropertyChanged();
            }
        }

        private string _gunAciklama1;
        public string GunAciklama1
        {
            get => _gunAciklama1;
            set
            {
                _gunAciklama1 = value;
                OnPropertyChanged();
            }
        }

        private DateTime _basSaat1;
        public DateTime BasSaat1
        {
            get => _basSaat1;
            set
            {
                _basSaat1 = value;
                OnPropertyChanged();

            }
        }

        public DateTime _bitSaat1;
        public DateTime BitSaat1
        {
            get => _bitSaat1;
            set
            {
                _bitSaat1 = value;
                OnPropertyChanged();
            }
        }

        public double _fazlaSaat1;
        public double FazlaSaat1
        {
            get => _fazlaSaat1;
            set
            {
                _fazlaSaat1 = value;
                OnPropertyChanged();
            }
        }

        public decimal _saatUcret1;
        public decimal SaatUcret1
        {
            get => _saatUcret1;
            set
            {
                _saatUcret1 = value;
                OnPropertyChanged();
            }
        }




        // 2. Gün
        private bool _gunVar2;
        public bool GunVar2
        {
            get => _gunVar2;
            set
            {
                _gunVar2 = value;
                OnPropertyChanged();
            }
        }

        private bool _gun2Gorunur;
        public bool Gun2Gorunur
        {
            get => _gun2Gorunur;
            set
            {
                _gun2Gorunur = value;
                OnPropertyChanged();
            }
        }

        private DateTime _tarih2;
        public DateTime Tarih2
        {
            get => _tarih2;
            set
            {
                _tarih2 = value;
                OnPropertyChanged();
            }
        }

        private DateTime _tarih2B;
        public DateTime Tarih2B
        {
            get => _tarih2B;
            set
            {
                _tarih2B = value;
                OnPropertyChanged();
            }
        }

        private string _gunAciklama2;
        public string GunAciklama2
        {
            get => _gunAciklama2;
            set
            {
                _gunAciklama2 = value;
                OnPropertyChanged();
            }
        }

        private DateTime _basSaat2;
        public DateTime BasSaat2
        {
            get => _basSaat2;
            set
            {
                _basSaat2 = value;
                OnPropertyChanged();

            }
        }

        public DateTime _bitSaat2;
        public DateTime BitSaat2
        {
            get => _bitSaat2;
            set
            {
                _bitSaat2 = value;
                OnPropertyChanged();
            }
        }

        public double _fazlaSaat2;
        public double FazlaSaat2
        {
            get => _fazlaSaat2;
            set
            {
                _fazlaSaat2 = value;
                OnPropertyChanged();
            }
        }

        public decimal _saatUcret2;
        public decimal SaatUcret2
        {
            get => _saatUcret2;
            set
            {
                _saatUcret2 = value;
                OnPropertyChanged();
            }
        }



        // 3. Gün
        private bool _gunVar3;
        public bool GunVar3
        {
            get => _gunVar3;
            set
            {
                _gunVar3 = value;
                OnPropertyChanged();
            }
        }

        private bool _gun3Gorunur;
        public bool Gun3Gorunur
        {
            get => _gun3Gorunur;
            set
            {
                _gun3Gorunur = value;
                OnPropertyChanged();
            }
        }

        private DateTime _tarih3;
        public DateTime Tarih3
        {
            get => _tarih3;
            set
            {
                _tarih3 = value;
                OnPropertyChanged();
            }
        }

        private DateTime _tarih3B;
        public DateTime Tarih3B
        {
            get => _tarih3B;
            set
            {
                _tarih3B = value;
                OnPropertyChanged();
            }
        }

        private string _gunAciklama3;
        public string GunAciklama3
        {
            get => _gunAciklama3;
            set
            {
                _gunAciklama3 = value;
                OnPropertyChanged();
            }
        }

        private DateTime _basSaat3;
        public DateTime BasSaat3
        {
            get => _basSaat3;
            set
            {
                _basSaat3 = value;
                OnPropertyChanged();

            }
        }

        public DateTime _bitSaat3;
        public DateTime BitSaat3
        {
            get => _bitSaat3;
            set
            {
                _bitSaat3 = value;
                OnPropertyChanged();
            }
        }

        public double _fazlaSaat3;
        public double FazlaSaat3
        {
            get => _fazlaSaat3;
            set
            {
                _fazlaSaat3 = value;
                OnPropertyChanged();
            }
        }

        public decimal _saatUcret3;
        public decimal SaatUcret3
        {
            get => _saatUcret3;
            set
            {
                _saatUcret3 = value;
                OnPropertyChanged();
            }
        }


        // 4. Gün
        private bool _gunVar4;
        public bool GunVar4
        {
            get => _gunVar4;
            set
            {
                _gunVar4 = value;
                OnPropertyChanged();
            }
        }

        private bool _gun4Gorunur;
        public bool Gun4Gorunur
        {
            get => _gun4Gorunur;
            set
            {
                _gun4Gorunur = value;
                OnPropertyChanged();
            }
        }

        private DateTime _tarih4;
        public DateTime Tarih4
        {
            get => _tarih4;
            set
            {
                _tarih4 = value;
                OnPropertyChanged();
            }
        }

        private DateTime _tarih4B;
        public DateTime Tarih4B
        {
            get => _tarih4B;
            set
            {
                _tarih4B = value;
                OnPropertyChanged();
            }
        }

        private string _gunAciklama4;
        public string GunAciklama4
        {
            get => _gunAciklama4;
            set
            {
                _gunAciklama4 = value;
                OnPropertyChanged();
            }
        }

        private DateTime _basSaat4;
        public DateTime BasSaat4
        {
            get => _basSaat4;
            set
            {
                _basSaat4 = value;
                OnPropertyChanged();

            }
        }

        public DateTime _bitSaat4;
        public DateTime BitSaat4
        {
            get => _bitSaat4;
            set
            {
                _bitSaat4 = value;
                OnPropertyChanged();
            }
        }

        public double _fazlaSaat4;
        public double FazlaSaat4
        {
            get => _fazlaSaat4;
            set
            {
                _fazlaSaat4 = value;
                OnPropertyChanged();
            }
        }

        public decimal _saatUcret4;
        public decimal SaatUcret4
        {
            get => _saatUcret4;
            set
            {
                _saatUcret4 = value;
                OnPropertyChanged();
            }
        }



        // 5. Gün
        private bool _gunVar5;
        public bool GunVar5
        {
            get => _gunVar5;
            set
            {
                _gunVar5 = value;
                OnPropertyChanged();
            }
        }

        private bool _gun5Gorunur;
        public bool Gun5Gorunur
        {
            get => _gun5Gorunur;
            set
            {
                _gun5Gorunur = value;
                OnPropertyChanged();
            }
        }

        private DateTime _tarih5;
        public DateTime Tarih5
        {
            get => _tarih5;
            set
            {
                _tarih5 = value;
                OnPropertyChanged();
            }
        }

        private DateTime _tarih5B;
        public DateTime Tarih5B
        {
            get => _tarih5B;
            set
            {
                _tarih5B = value;
                OnPropertyChanged();
            }
        }

        private string _gunAciklama5;
        public string GunAciklama5
        {
            get => _gunAciklama5;
            set
            {
                _gunAciklama5 = value;
                OnPropertyChanged();
            }
        }

        private DateTime _basSaat5;
        public DateTime BasSaat5
        {
            get => _basSaat5;
            set
            {
                _basSaat5 = value;
                OnPropertyChanged();

            }
        }

        public DateTime _bitSaat5;
        public DateTime BitSaat5
        {
            get => _bitSaat5;
            set
            {
                _bitSaat5 = value;
                OnPropertyChanged();
            }
        }

        public double _fazlaSaat5;
        public double FazlaSaat5
        {
            get => _fazlaSaat5;
            set
            {
                _fazlaSaat5 = value;
                OnPropertyChanged();
            }
        }

        public decimal _saatUcret5;
        public decimal SaatUcret5
        {
            get => _saatUcret5;
            set
            {
                _saatUcret5 = value;
                OnPropertyChanged();
            }
        }


        // 6. Gün
        private bool _gunVar6;
        public bool GunVar6
        {
            get => _gunVar6;
            set
            {
                _gunVar6 = value;
                OnPropertyChanged();
            }
        }

        private bool _gun6Gorunur;
        public bool Gun6Gorunur
        {
            get => _gun6Gorunur;
            set
            {
                _gun6Gorunur = value;
                OnPropertyChanged();
            }
        }

        private DateTime _tarih6;
        public DateTime Tarih6
        {
            get => _tarih6;
            set
            {
                _tarih6 = value;
                OnPropertyChanged();
            }
        }

        private DateTime _tarih6B;
        public DateTime Tarih6B
        {
            get => _tarih6B;
            set
            {
                _tarih6B = value;
                OnPropertyChanged();
            }
        }

        private string _gunAciklama6;
        public string GunAciklama6
        {
            get => _gunAciklama6;
            set
            {
                _gunAciklama6 = value;
                OnPropertyChanged();
            }
        }

        private DateTime _basSaat6;
        public DateTime BasSaat6
        {
            get => _basSaat6;
            set
            {
                _basSaat6 = value;
                OnPropertyChanged();

            }
        }

        public DateTime _bitSaat6;
        public DateTime BitSaat6
        {
            get => _bitSaat6;
            set
            {
                _bitSaat6 = value;
                OnPropertyChanged();
            }
        }

        public double _fazlaSaat6;
        public double FazlaSaat6
        {
            get => _fazlaSaat6;
            set
            {
                _fazlaSaat6 = value;
                OnPropertyChanged();
            }
        }

        public decimal _saatUcret6;
        public decimal SaatUcret6
        {
            get => _saatUcret6;
            set
            {
                _saatUcret6 = value;
                OnPropertyChanged();
            }
        }


        // 7. Gün
        private bool _gunVar7;
        public bool GunVar7
        {
            get => _gunVar7;
            set
            {
                _gunVar7 = value;
                OnPropertyChanged();
            }
        }

        private bool _gun7Gorunur;
        public bool Gun7Gorunur
        {
            get => _gun7Gorunur;
            set
            {
                _gun7Gorunur = value;
                OnPropertyChanged();
            }
        }

        private DateTime _tarih7;
        public DateTime Tarih7
        {
            get => _tarih7;
            set
            {
                _tarih7 = value;
                OnPropertyChanged();
            }
        }

        private DateTime _tarih7B;
        public DateTime Tarih7B
        {
            get => _tarih7B;
            set
            {
                _tarih7B = value;
                OnPropertyChanged();
            }
        }

        private string _gunAciklama7;
        public string GunAciklama7
        {
            get => _gunAciklama7;
            set
            {
                _gunAciklama7 = value;
                OnPropertyChanged();
            }
        }

        private DateTime _basSaat7;
        public DateTime BasSaat7
        {
            get => _basSaat7;
            set
            {
                _basSaat7 = value;
                OnPropertyChanged();

            }
        }

        public DateTime _bitSaat7;
        public DateTime BitSaat7
        {
            get => _bitSaat7;
            set
            {
                _bitSaat7 = value;
                OnPropertyChanged();
            }
        }

        public double _fazlaSaat7;
        public double FazlaSaat7
        {
            get => _fazlaSaat7;
            set
            {
                _fazlaSaat7 = value;
                OnPropertyChanged();
            }
        }

        public decimal _saatUcret7;
        public decimal SaatUcret7
        {
            get => _saatUcret7;
            set
            {
                _saatUcret7 = value;
                OnPropertyChanged();
            }
        }

    }

    public class GeceMesaiGunBilgi
    { 
        public bool GunVar { get; set; }
        
        public string GunBaslik { get; set; }

        public DateTime GunBasTar { get; set; }

        public DateTime GunBitTar { get; set; }

        public TimeSpan GunBasSaat { get; set; }

        public TimeSpan GunBitSaat { get; set; }

        public decimal GunUcret { get; set; }

        public double GunSaat { get; set; }

    }
}
