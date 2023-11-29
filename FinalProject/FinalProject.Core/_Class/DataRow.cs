using Microsoft.ML.Data;

namespace FinalProject.Core._Class
{
    public class DataRow
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