using CommunityToolkit.Mvvm.Input;
using FinalProject.ML.Models;
using Ookii.Dialogs.Wpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FinalProject.ML._MainApp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            _DataFolder = @"D:\PROJECTS\TTB\KLines\H1\";
            BtnSelectDataFolder = new RelayCommand(BtnSelectDataFolder_Click);
            BtntOpenDataFolder = new RelayCommand(() => { if (Directory.Exists(DataFolder)) Process.Start(new ProcessStartInfo(DataFolder) { UseShellExecute = true }); });
            BtnDownloadKlines = new RelayCommand(async () => await DownloadKlines.Download(this));
        }


        #region  Download
        public ICommand BtnDownloadKlines { get; }
        public ICommand BtnSelectDataFolder { get; }
        public ICommand BtntOpenDataFolder { get; }

        private string _DataFolder;
        public string DataFolder
        {
            get { return _DataFolder; }
            set
            {
                _DataFolder = value;
                OnPropertyChanged(nameof(DataFolder));
            }
        }
        private void BtnSelectDataFolder_Click()
        {
            VistaFolderBrowserDialog openFileDialog = new();
            if (DataFolder != "") openFileDialog.SelectedPath = DataFolder;
            if (openFileDialog.ShowDialog()!.Value) DataFolder = openFileDialog.SelectedPath;
        }


        private bool _isRunDownloadKlines = false;
        public bool IsRunDownloadKlines
        {
            get { return _isRunDownloadKlines; }
            set
            {
                _isRunDownloadKlines = value;
                OnPropertyChanged(nameof(IsRunDownloadKlines));
            }
        }

        public string SymbolSelected { get; set; } = "BTCUSDT";

        public Interval SelectedInterval { get; set; } = Interval.H1;
        public ObservableCollection<Interval> Intervals { get; } = new ObservableCollection<Interval>(Enum.GetValues(typeof(Interval)).Cast<Interval>());

        public MarketType SelectedMarketType { get; set; } = MarketType.Futures;
        public ObservableCollection<MarketType> MarketTypes { get; } = new ObservableCollection<MarketType>(Enum.GetValues(typeof(MarketType)).Cast<MarketType>());

        public Exchange SelectedExchange { get; set; } = Exchange.Binance;
        public ObservableCollection<Exchange> Exchanges { get; } = new ObservableCollection<Exchange>(Enum.GetValues(typeof(Exchange)).Cast<Exchange>());

        public DateTime SelectedStartDate { get; set; } = DateTime.Today.AddYears(-3);
        public DateTime SelectedToDate { get; set; } = DateTime.Today;

        private string _Status = "Ready";
        public string Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        #endregion









        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}