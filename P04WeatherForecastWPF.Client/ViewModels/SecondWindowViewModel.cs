using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P04WeatherForecastWPF.Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace P04WeatherForecastWPF.Client.ViewModels
{
    public partial class SecondWindowViewModel : ObservableObject, IMainViewModel
    {
        private string _parametr;
        private readonly string _favouriteListFilePath = AppDomain.CurrentDomain.BaseDirectory + $"FavouriteCityDataBase.txt";
        


        public string Parametr
        {
            get => _parametr;
            set => SetProperty(ref _parametr, value);
        }

        public void AddNewFavouriteCiy(string cityName)
        {
            FavouriteCitiesList.Add(cityName);
            File.AppendAllText(_favouriteListFilePath, cityName);
        }

        public ObservableCollection<string> FavouriteCitiesList { get; set; }

        public ICommand RemoveCity;

        private void RemoveCityExecute2(string city)
        {
            FavouriteCitiesList.Remove(city);
            var readCities = File.ReadAllText(_favouriteListFilePath);
            readCities.Replace(city, "");
            File.WriteAllText(_favouriteListFilePath, readCities);
        }

        [RelayCommand]
        public async Task RemoveCityExecute(string city)
        {
            FavouriteCitiesList.Remove(city);
            var readCities = await File.ReadAllTextAsync(_favouriteListFilePath);
            readCities.Replace(city, "");
            await File.WriteAllTextAsync(_favouriteListFilePath, readCities);
        }

        // Konstruktor, który przyjmuje parametr
        public SecondWindowViewModel()
        {
            RemoveCity = new RelayCommand<string>(RemoveCityExecute2);
            FavouriteCitiesList = new();
            var readFileWithCities = File.ReadAllLines(_favouriteListFilePath);
            foreach(string city in readFileWithCities)
            {
                FavouriteCitiesList.Add(city);
            }
        }
    }
}
