namespace FinalProject.Core.Data
{
    public class PriceUtils
    {
        public static decimal Percent(decimal entryPrice, decimal targetPrice, int levX = 10, bool abs = false)
        {
            decimal percent = levX * 100 * (targetPrice - entryPrice) / entryPrice;
            if (abs) percent = Math.Abs(percent);
            return percent;
        }
    }
}