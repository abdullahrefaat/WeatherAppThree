using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAppThree.Model;
using WeatherAppThree.ViewModel.Commands;
using WeatherAppThree.ViewModel.Helpers;

namespace WeatherAppThree.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
		private string query;

		public string Query
		{
			get { return query; }
			set 
			{
				query = value;
				OnPropertyChanged("Query");
			}
		}


		private CurrentConditions currentConditions;

		public CurrentConditions CurrentConditions
		{
			get { return currentConditions; }
			set 
			{ 
				currentConditions = value;
				OnPropertyChanged("CurrentConditions");
			}
		}

		private City selectedCity;

		public City SelectedCity
		{
			get { return selectedCity; }
			set 
			{ 
				selectedCity = value;
				OnPropertyChanged("SelectedCity");
				GetCurrentConditions();
			}
		}

		public SearchCommand SearchCommand { get; set; }

        public ObservableCollection<City> Cities { get; set; }

        public WeatherVM()
		{
			if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
			{
                SelectedCity = new City
                {
                    LocalizedName = "New York"
                };
                CurrentConditions = new CurrentConditions
                {
                    WeatherText = "Partly Cloud",
                    Temperature = new Temperature
                    {
                        Metric = new Units
                        {
                            Value = "21"
                        }
                    }
                };
            }
			SearchCommand = new SearchCommand(this);
			Cities = new ObservableCollection<City>();
		}
		private async void GetCurrentConditions()
		{
			Query = string.Empty;
			Cities.Clear();
			CurrentConditions = await AccWeatherHelper.GetCurrentConditions(SelectedCity.Key);
		}

		public async void MakeQuery()
        {
            var cities = await AccWeatherHelper.GetCities(Query);

            Cities.Clear();
            foreach (var city in cities)
            {
                Cities.Add(city);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
