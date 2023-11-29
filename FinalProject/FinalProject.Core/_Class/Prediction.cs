using Microsoft.ML.Data;

namespace FinalProject.Core._Class
{
    public class Prediction
    {
        // Dự đoán
        [ColumnName("Score")]
        public float Score;


        // Phân loại
        [ColumnName("PredictedLabel")]
        public bool Label;

        [ColumnName("Probability")]
        public float Probability;
    }
}