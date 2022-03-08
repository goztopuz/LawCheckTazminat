using LawCheckTazminat.Contracts.Services;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.ViewModels.DestektenYoksunlukB;
using LawCheckTazminat.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LawCheckTazminat.Services
{
    public class NavigationService : INavigationService
    {

        private readonly Dictionary<Type, Type> _mappings;
        protected Application CurrentApplication => Application.Current;

        public NavigationService()
        {

            _mappings = new Dictionary<Type, Type>();

            CreatePageViewModelMappings();
            
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            var page = CreatePage(viewModelType, parameter);
        }


        protected Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if(pageType== null)
            {
                throw new Exception($"Mapping type for ${viewModelType} is  not a page");
            }
            Page page = Activator.CreateInstance(pageType) as Page;

            return page;

        }
        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if(! _mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return _mappings[viewModelType];


        }
        
    
        private void CreatePageViewModelMappings()
        {
            _mappings.Add(typeof(Wiz1DYViewModel), typeof(Wiz1DYView));

        }




        public async Task InitializeAsync()
        {
            await NavigateToAsync<Wiz1DYViewModel>();
        }

        public Task ClearBackStack()
        {
            throw new NotImplementedException();
        }

    

        public Task NavigateBackAsync()
        {
            throw new NotImplementedException();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            throw new NotImplementedException();
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            throw new NotImplementedException();
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            throw new NotImplementedException();
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            throw new NotImplementedException();
        }

        public Task PopToRootAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveLastFromBackStackAsync()
        {
            throw new NotImplementedException();
        }
    }
}
