using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinHomework7.Models;

namespace XamarinHomework7.ViewModels
{
    public class LoginPageViewModel
    {

        public User User {get; set;}
        public DelegateCommand LoginCommand { get; set; }
        protected INavigationService _navigationService;
        protected IPageDialogService _dialogService;
        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _dialogService = dialogService;
            User = new User();
            _navigationService = navigationService;
            LoginCommand = new DelegateCommand(async () =>
            {
                await Login();
            });
        }

        async Task Login()
        {
            bool validUser = true;
            if(String.IsNullOrEmpty(User.Username) || String.IsNullOrEmpty(User.Password))
            {
                validUser = false;
                await _dialogService.DisplayAlertAsync("The information is incomplete", "Please check that all fields are correctly filled and try again", "OK");
            }
            if(validUser)
            {
                var navParams = new NavigationParameters();
                navParams.Add("User", User);

                await _navigationService.NavigateAsync(new Uri("HomePage", UriKind.Relative), navParams);
            }
            
        }

    }
}
