
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using XamarinHomework7.Models;

namespace XamarinHomework7.ViewModels
{
    public class HomePageViewModel:INotifyPropertyChanged, INavigatedAware
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string TextToDisplay { get; set; }
        public User User { get; set; }

        Event _event;
        public Event SelectedEvent
        {
            get
            {
                return _event;
            }
            set
            {
                _event = value;

                if (_event != null)
                    OnSelectItem(_event);

            }
        }

        public ObservableCollection<Event> Events { get; set; } = new ObservableCollection<Event>();
        public DelegateCommand<Event> DeleteElementCommand { get; }
        public DelegateCommand AddEventCommand{ get; }

        protected INavigationService _navigationService; 
        public HomePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            var testEvent = new Event
            {
                Title = "Daily meeting",
                Description = "Usual daily meeting to discuss the project's progress.",
                Date = new DateTime(1998,09,30),
                Place = "Charlin's office"
            };

            Events.Add(testEvent);
            DeleteElementCommand = new DelegateCommand<Event>(async (param) =>
            {
                
                var result = await App.Current.MainPage.DisplayAlert("Delete event", "Are you sure you want to delete this event?", "Yes", "Cancel");
                
                if (result == true)
                {
                    Events.Remove(param);
                }
            });

            AddEventCommand = new DelegateCommand(async () =>
            {
                await GoToAddEventPage();
            });

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if(parameters.ContainsKey("User"))
            {
                User = (User)parameters["User"];
            }
            if(parameters.ContainsKey("NewEvent"))
            {
                Events.Add((Event)parameters["NewEvent"]);
            }
          
        }

        async Task GoToAddEventPage()
        {
            await _navigationService.NavigateAsync(new Uri("AddNewEventPage", UriKind.Relative));
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        void OnSelectItem(Event _event)
        {

        }
    }
}
