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

                    string fileName = Path.GetFileNameWithoutExtension(vm.DataSetPath);
                    fileName = fileName.Replace("DataSet_", $"Model_{vm.SelectedAlgorithm}_");
                    string folder = Path.GetDirectoryName(vm.DataSetPath)!;
                    string modelPath = Path.Combine(folder, $"{fileName}.zip");

                    await Task.Run(() =>
                    {
                        MLContext context = new();

                        IDataView alldata = LoadData(vm.DataSetPath);
                        if (alldata != null)
                        {
                            if (vm.SelectedModelType == ModelType.Regression)
                            {
                                TrainTestData splitDataView = context.Data.TrainTestSplit(alldata, testFraction: 0.2);  // Tách dữ liệu thành 2 phần: 80% cho train và 20% cho test
                                var experiment = context.Auto().CreateRegressionExperiment(maxExperimentTimeInSeconds: vm.MaxTime);
                                var experimentResult = experiment.Execute(splitDataView.TrainSet, splitDataView.TestSet, progressHandler: new RegressionProgressHandler(vm));
                                var bestRun = experimentResult.BestRun;
                                string valid = $"RMSE={bestRun.ValidationMetrics.RootMeanSquaredError:0.00}";
                                vm.TrainingStatus = $"{valid}, LossFunction={bestRun.ValidationMetrics.LossFunction:0.00}, Trainer={TrainingUtils.GetTrainerName(bestRun.TrainerName)}";
                                modelPath = $"{modelPath}_{valid}.zip";
                                context.Model.Save(bestRun.Model, splitDataView.TrainSet.Schema, modelPath);
                            }
                        }
                    });

                    vm.TrainingStatus = "Completed";
                }
            }
            else vm.TrainingStatus = "DataSet Not Found";
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