namespace FinalProject.ML.Models
{
    public class KlineUtils
    {
        public static List<Tuple<DateTime, int>> CaclStepTimes(DateTime startDate, DateTime endDate, Interval interval)
        {
            List<Tuple<DateTime, int>> times = new();
            int intervalSeconds = (int)interval;
            if (endDate > DateTime.Now) endDate = DateTime.Now;

            DateTime startTime = startDate;
            do
            {
                DateTime endTime = endDate;
                int klineCount = (int)Math.Round((endTime - startTime).TotalSeconds / intervalSeconds);
                if (klineCount > 1500)
                {
                    klineCount = 1500;
                    endTime = startTime.AddSeconds(1500 * intervalSeconds);
                }
                times.Add(new Tuple<DateTime, int>(startTime, klineCount));

                startTime = endTime;

                if (endTime >= endDate) break;
            } while (true);

            return times;
        }
    }
}