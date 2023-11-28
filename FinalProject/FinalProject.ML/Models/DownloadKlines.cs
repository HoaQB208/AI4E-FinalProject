using FinalProject.Core._Class;
using FinalProject.Core.Data;
using FinalProject.ML._MainApp;
using Newtonsoft.Json;
using System.IO;

namespace FinalProject.ML.Models
{
    public class DownloadKlines
    {
        public static async Task Download(MainViewModel vm)
        {
            vm.IsRunDownloadKlines = !vm.IsRunDownloadKlines;
            if (vm.IsRunDownloadKlines)
            {
                vm.Status = "Starting download...";
                List<Tuple<DateTime, int>> times = KlineUtils.CaclStepTimes(vm.SelectedStartDate, vm.SelectedToDate.AddDays(1), vm.SelectedInterval);
                Dictionary<string, List<TKline>> data = new();

                List<TKline> klines = new();
                int count = 0;
                foreach (Tuple<DateTime, int> item in times)
                {
                    List<TKline> stepklines = await Market.Download(vm.SymbolSelected, vm.SelectedInterval, item.Item1, item.Item2);
                    klines.AddRange(stepklines);
                    count++;
                    vm.Status = $"Downloading {100d * count / times.Count:0}%";

                    if (!vm.IsRunDownloadKlines) break;
                }
                data.Add(vm.SymbolSelected, klines);

                string fileName = $"{vm.SelectedStartDate:yyyy-MM-dd} → {vm.SelectedToDate:yyyy-MM-dd} [{vm.SelectedInterval}-{vm.SymbolSelected}].json";
                string pathFile = Path.Combine(vm.DataFolder, fileName);
                string content = JsonConvert.SerializeObject(data);
                FileUtils.WriteFile(pathFile, content);

                vm.IsRunDownloadKlines = false;
                vm.Status = "Completed";
            }
        }
    }
}