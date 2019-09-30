using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using XamarinHomework7.Models;

namespace XamarinHomework7.ViewModels
{
    public class AddNewEventPageViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Event Event { get; set; }
        protected IPageDialogService _dialogService;
        protected INavigationService _navigationService;

        public string DateString { get; set; }

        public DelegateCommand CreateEventCommand { get; }
        public AddNewEventPageViewModel(IPageDialogService dialogService, INavigationService navigationService)
        {
            Event = new Event();
            _navigationService = navigationService;
            _dialogService = dialogService;

            CreateEventCommand = new DelegateCommand(async () =>
            {
                await CreateEvent();
            });
        }


        async Task CreateEvent()
        {
            bool validEvent = true;
            if(String.IsNullOrEmpty(Event.Title) || String.IsNullOrEmpty(Event.Description)
                || String.IsNullOrEmpty(Event.Place))
            {
                validEvent = false;
                await _dialogService.DisplayAlertAsync("The information is incomplete", "Please check that all fields are correctly filled and try again", "OK");
            }

            DateTime date = new DateTime();
            if (DateTime.TryParse(DateString, out date))
            {
                Event.Date = date;
            }
            else
            {
                validEvent = false;
                await _dialogService.DisplayAlertAsync("Invalid date", "Please input a valid date in the format yyyy/mm/dd and try again", "OK");
            }
            
            if(validEvent)
            {
                var navParams = new NavigationParameters();
                navParams.Add("NewEvent", Event);

                await _navigationService.NavigateAsync(new Uri("HomePage", UriKind.Relative), navParams);
            }


        }
        
    }
}
