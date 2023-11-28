namespace FinalProject.ML.Models
{
    public class TrainingUtils
    {
        public static string GetTrainerName(string fullTrainerName)
        {
            return fullTrainerName.Split("=>").Last();
        }
    }
}