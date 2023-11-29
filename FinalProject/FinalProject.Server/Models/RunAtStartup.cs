using Microsoft.ML;

namespace FinalProject.Server.Models
{
    public class RunAtStartup : BackgroundService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public static ITransformer model = null!;

        public RunAtStartup(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string wwwrootPath = _webHostEnvironment.WebRootPath;
            string modelPath = Path.Combine(wwwrootPath, @"Model.zip");
            if (File.Exists(modelPath))  model = new MLContext().Model.Load(modelPath, out _);
            return Task.CompletedTask;
        }
    }
}