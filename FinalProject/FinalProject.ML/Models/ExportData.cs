using FinalProject.Core._Class;
using FinalProject.Core.Data;
using FinalProject.ML._MainApp;
using Newtonsoft.Json;
using System.IO;

namespace FinalProject.ML.Models
{
    public class ExportData
    {
        const int ColumnNumb = 13; // Include Label

        public static void Execute(MainViewModel vm)
        {
            if (string.IsNullOrEmpty(vm.SelectedKlineFile))
            {
                vm.ExportStatus = "Please select Kline File"; return;
            }

            if (string.IsNullOrEmpty(vm.DataSetFolder))
            {
                vm.ExportStatus = "Please select DataSet Folder"; return;
            }

            vm.IsRunExport = !vm.IsRunExport;
            if (vm.IsRunExport)
            {
                vm.ExportStatus = "Exporting...";
                List<string> rows = new();
                string klinesPath = Path.Combine(vm.KlinesFolder, vm.SelectedKlineFile);
                string content = FileUtils.ReadFile(klinesPath);
                var symbolKlines = JsonConvert.DeserializeObject<Dictionary<string, List<TKline>>>(content);
                if (symbolKlines != null)
                {
                    foreach (var symbolKline in symbolKlines)
                    {
                        string symbol = symbolKline.Key;
                        List<TKline> klines = symbolKline.Value;
                        if (klines.Count > 0)
                        {
                            for (int current = ColumnNumb - 1; current < klines.Count; current++)
                            {
                                List<string> featuresLabel = new();
                                for (int i = 0; i < ColumnNumb; i++)
                                {
                                    var kline = klines[current - i];
                                    var per = PriceUtils.Percent(kline.O, kline.C, 1);  // Convert Open-Close Price to %
                                    featuresLabel.Insert(0, per.ToString("0.0000"));
                                }
                                rows.Add(string.Join("\t", featuresLabel));

                                vm.ExportStatus = $"Exporting {symbol} DataSet: {100d * current / klines.Count:0}%";
                            }
                        }
                    }
                }

                if (rows.Count > 0)
                {
                    string klineFileName = Path.GetFileNameWithoutExtension(klinesPath);
                    string outputDataSetPath = Path.Combine(vm.DataSetFolder, $"DataSet_{klineFileName}.txt");
                    FileUtils.WriteFile(outputDataSetPath, string.Join("\n", rows));
                }

                vm.IsRunExport = false;
                vm.ExportStatus = $"Completed: Exported {rows.Count} rows";
            }
        }
    }
}