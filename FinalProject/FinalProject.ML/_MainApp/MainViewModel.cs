using CommunityToolkit.Mvvm.Input;
using FinalProject.ML.Models;
using Ookii.Dialogs.Wpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FinalProject.ML._MainApp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            // Download
            _DownloadStatus = "Ready";
            SymbolSelected = "BTCUSDT";
            _KlinesFolder = @"D:\PROJECTS\TTB\KLines\H1\";
            _KlineFiles = new();
            UpdateDataFiles();

            SelectedInterval = Interval.H1;
            SelectedMarketType = MarketType.Futures;
            SelectedExchange = Exchange.Binance;
            BtnSelectKlineFolder = new RelayCommand(BtnSelectKlineFolder_Click);
            BtntOpenKlineFolder = new RelayCommand(() => FileUtils.ShowFolder(KlinesFolder));
            BtnDownloadKlines = new RelayCommand(async () => await DownloadKlines.Download(this));

            // Export
            _ExportStatus = "Ready";
            _DataSetFolder = @"D:\PROJECTS\TTB\DataSet\";
            BtnRefreshDataFiles = new RelayCommand(UpdateDataFiles);
            BtnSelectDataSetFolder = new RelayCommand(BtnSelectDataSetFolder_Click);
            BtntOpenDataSetFolder = new RelayCommand(() => FileUtils.ShowFolder(DataSetFolder));
            BtnExport = new RelayCommand(() => ExportData.Execute(this));
        }


        #region Download

        public ICommand BtnDownloadKlines { get; }
        public ICommand BtnSelectKlineFolder { get; }
        public ICommand BtntOpenKlineFolder { get; }

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

        private string _KlinesFolder { get; set; }
        public string KlinesFolder
        {
            get { return _KlinesFolder; }
            set
            {
                _KlinesFolder = value;
                OnPropertyChanged(nameof(KlinesFolder));
                UpdateDataFiles();
            }
        }
        private void BtnSelectKlineFolder_Click()
        {
            VistaFolderBrowserDialog openFileDialog = new();
            if (KlinesFolder != "") openFileDialog.SelectedPath = KlinesFolder;
            if (openFileDialog.ShowDialog()!.Value) KlinesFolder = openFileDialog.SelectedPath;
        }

        public string SymbolSelected { get; set; }

        public Interval SelectedInterval { get; set; }
        public ObservableCollection<Interval> Intervals { get; } = new ObservableCollection<Interval>(Enum.GetValues(typeof(Interval)).Cast<Interval>());

        public MarketType SelectedMarketType { get; set; }
        public ObservableCollection<MarketType> MarketTypes { get; } = new ObservableCollection<MarketType>(Enum.GetValues(typeof(MarketType)).Cast<MarketType>());

        public Exchange SelectedExchange { get; set; }
        public ObservableCollection<Exchange> Exchanges { get; } = new ObservableCollection<Exchange>(Enum.GetValues(typeof(Exchange)).Cast<Exchange>());

        public DateTime SelectedStartDate { get; set; } = DateTime.Today.AddYears(-3);
        public DateTime SelectedToDate { get; set; } = DateTime.Today;

        private string _DownloadStatus { get; set; }
        public string DownloadStatus
        {
            get { return _DownloadStatus; }
            set
            {
                _DownloadStatus = value;
                OnPropertyChanged(nameof(DownloadStatus));
            }
        }

        #endregion



        #region Export
        public ICommand BtnRefreshDataFiles { get; }
        public ICommand BtnSelectDataSetFolder { get; }
        public ICommand BtntOpenDataSetFolder { get; }
        public ICommand BtnExport { get; }

        private void BtnSelectDataSetFolder_Click()
        {
            VistaFolderBrowserDialog openFileDialog = new();
            if (DataSetFolder != "") openFileDialog.SelectedPath = DataSetFolder;
            if (openFileDialog.ShowDialog()!.Value) DataSetFolder = openFileDialog.SelectedPath;
        }


        private List<string> _KlineFiles { get; set; }
        public List<string> KlineFiles
        {
            get { return _KlineFiles; }
            set
            {
                _KlineFiles = value;
                OnPropertyChanged(nameof(KlineFiles));
            }
        }

        private string _SelectedKlineFile = "";
        public string SelectedKlineFile
        {
            get { return _SelectedKlineFile; }
            set
            {
                _SelectedKlineFile = value;
                OnPropertyChanged(nameof(SelectedKlineFile));
            }
        }

        public void UpdateDataFiles()
        {
            KlineFiles = FileUtils.GetJsonFileNames(KlinesFolder);
            if (KlineFiles.Count > 0) SelectedKlineFile = KlineFiles[0];
        }

        private string _DataSetFolder;
        public string DataSetFolder
        {
            get { return _DataSetFolder; }
            set
            {
                _DataSetFolder = value;
                OnPropertyChanged(nameof(DataSetFolder));
            }
        }


        private bool _IsRunExport = false;
        public bool IsRunExport
        {
            get { return _IsRunExport; }
            set
            {
                _IsRunExport = value;
                OnPropertyChanged(nameof(IsRunExport));
            }
        }

        private string _ExportStatus;
        public string ExportStatus
        {
            get { return _ExportStatus; }
            set
            {
                _ExportStatus = value;
                OnPropertyChanged(nameof(ExportStatus));
            }
        }

        #endregion



        #region TrainingModel


        #endregion



        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}