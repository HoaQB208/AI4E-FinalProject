namespace FinalProject.Core._Class
{
    public class TKline
    {
        /// <summary>
        /// OpenTime
        /// </summary>
        public DateTime D { get; set; }

        /// <summary>
        /// OpenPrice
        /// </summary>
        public decimal O { get; set; }

        /// <summary>
        /// HighPrice
        /// </summary>
        public decimal H { get; set; }

        /// <summary>
        /// LowPrice
        /// </summary>
        public decimal L { get; set; }

        /// <summary>
        /// ClosePrice
        /// </summary>
        public decimal C { get; set; }

        /// <summary>
        /// Volume
        /// </summary>
        public decimal V { get; set; }

        /// <summary>
        /// TakerBuyBaseVolume
        /// </summary>
        public decimal B { get; set; }
    }
}