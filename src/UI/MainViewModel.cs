﻿using System.Windows.Input;
using Assignment.UI.ViewModels;
using Caliburn.Micro;

namespace Assignment.UI
{
    public class MainViewModel : PropertyChangedBase
    {
        private readonly IWindowManager _windowManager;

        public ICommand TodoListManagment { get; }
        public ICommand WeatherForecast { get; }

        public MainViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            TodoListManagment = new RelayCommand(ShowTodoListManagment);
            WeatherForecast = new RelayCommand(ShowWeatherForecast);
        }

        private async void ShowWeatherForecast(object obj)
        {
            var weatherForecast = IoC.Get<WeatherForecastViewModel>();
            await _windowManager.ShowDialogAsync(weatherForecast);
        }

        private async void ShowTodoListManagment(object obj)
        {
            var todoList = IoC.Get<TodoManagmentViewModel>();
            await _windowManager.ShowDialogAsync(todoList);
        }
    }
}
