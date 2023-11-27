﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FinalProject.ML._MainApp
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {

        }








        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}