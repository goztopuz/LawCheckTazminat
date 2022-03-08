using JetBrains.Annotations;
using LawCheckTazminat.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LawCheckTazminat.ViewModels.Base
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        //  public bool IsPro => SettingsService.IsPro;

        bool _ispro;
        public bool IsPro
        {
            get => SettingsService.IsPro;

            set
            {
                _ispro = true;
                OnPropertyChanged();
            }
                        
             }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public virtual Task  InitializeAsync(object data)
            {

            return Task.FromResult(false);

        }
            
      }
}
