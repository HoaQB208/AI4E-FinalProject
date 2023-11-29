using FinalProject.ML._MainApp;
using Microsoft.ML;
using Microsoft.ML.AutoML;
using Microsoft.ML.Data;
using System.IO;
using static Microsoft.ML.DataOperationsCatalog;

namespace FinalProject.ML.Models
{
    public class TrainingModel
    {
        public static async Task Execute(MainViewModel vm)
        {
            if (File.Exists(vm.DataSetPath))
            {
                vm.IsRunTraining = !vm.IsRunTraining;
                if (vm.IsRunTraining)
                {
                    vm.TrainingStatus = "Starting Training Model...";
                    startTime = DateTime.Now;
                    UpdateStatus(vm);

                    string fileName = Path.GetFileNameWithoutExtension(vm.DataSetPath);
                    fileName = fileName.Replace("DataSet_", $"Model_{vm.SelectedAlgorithm}_");
                    string folder = Path.GetDirectoryName(vm.DataSetPath)!;
                    string modelPath = Path.Combine(folder, fileName);

                    await Task.Run(() =>
                    {
                        MLContext context = new();
                        IDataView alldata = LoadData(vm.DataSetPath);
                        if (alldata != null)
                        {
                            TrainTestData splitDataView = context.Data.TrainTestSplit(alldata, testFraction: 0.2);  // 80% for Train, 20% for Test
                            if (vm.SelectedModelType == ModelType.Regression)
                            {
                                var experiment = context.Auto().CreateRegressionExperiment(maxExperimentTimeInSeconds: vm.MaxTime);
                                var experimentResult = experiment.Execute(splitDataView.TrainSet, splitDataView.TestSet, progressHandler: new RegressionProgressHandler(vm));
                                var bestRun = experimentResult.BestRun;
                                string valid = $"RMSE={bestRun.ValidationMetrics.RootMeanSquaredError:0.00}";
                                vm.TrainingStatus = $"Completed: {valid}, LossFunction={bestRun.ValidationMetrics.LossFunction:0.00}, Trainer={TrainingUtils.GetTrainerName(bestRun.TrainerName)}";
                                modelPath = $"{modelPath}_{valid}.zip";
                                context.Model.Save(bestRun.Model, splitDataView.TrainSet.Schema, modelPath);
                            }
                            else // Classification
                            {


                            }
                        }
                    });
                }
            }
            else vm.TrainingStatus = "DataSet Not Found";
        }

        static DateTime startTime;
        private static void UpdateStatus(MainViewModel vm)
        {
            Task.Run(() =>
            {
                do
                {
                    var totalSeconds = (DateTime.Now - startTime).TotalSeconds;
                    var per = vm.MaxTime > 0 ? 100 * totalSeconds / vm.MaxTime : 0;
                    per = Math.Min(per, 100);
                    vm.ProgressBarStatus = $"Progress: {per:0.0}%";
                    vm.ProgressBarValue = 100 * totalSeconds / vm.MaxTime;
                    Thread.Sleep(1000);
                }
                while (vm.IsRunTraining);
            });
        }

        private static IDataView LoadData(string dataSetPath)
        {
            return new MLContext().Data.LoadFromTextFile<Node>(dataSetPath);
        }
    }

    public class RegressionProgressHandler : IProgress<RunDetail<RegressionMetrics>>
    {
        private int _iterationIndex;
        double bestResult = 999;
        MainViewModel _vm;
        public RegressionProgressHandler(MainViewModel vm)
        {
            _vm = vm;
        }

        public void Report(RunDetail<RegressionMetrics> iterationResult)
        {
            _iterationIndex++;
            if (iterationResult.ValidationMetrics.RootMeanSquaredError < bestResult)
            {
                bestResult = iterationResult.ValidationMetrics.RootMeanSquaredError;
            }
            _vm.TrainingStatus = $"Best: {bestResult:0.00}. Current: RMSE={iterationResult.ValidationMetrics.RootMeanSquaredError:0.00}, LossFunction={iterationResult.ValidationMetrics.LossFunction:0.00}, Loop={_iterationIndex}, TrainerName={TrainingUtils.GetTrainerName(iterationResult.TrainerName)}";
        }
    }


    public class Node
    {
        [LoadColumn(0)] public float C0;
        [LoadColumn(1)] public float C1;
        [LoadColumn(2)] public float C2;
        [LoadColumn(3)] public float C3;
        [LoadColumn(4)] public float C4;
        [LoadColumn(5)] public float C5;
        [LoadColumn(6)] public float C6;
        [LoadColumn(7)] public float C7;
        [LoadColumn(8)] public float C8;
        [LoadColumn(9)] public float C9;
        [LoadColumn(10)] public float C10;
        [LoadColumn(11)] public float C11;
        [LoadColumn(12)] public float Label;
    }
}