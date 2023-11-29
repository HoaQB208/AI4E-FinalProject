using FinalProject.Core._Class;
using FinalProject.Server.Models;
using Microsoft.ML;
using Newtonsoft.Json;

namespace FinalProject.Server.API
{
    public class Predict
    {
        public static async Task Execute(HttpContext context)
        {
            var arg = context.Request.RouteValues["arg"];
            if (arg != null)
            {
                string para = arg.ToString()!;
                if (!string.IsNullOrEmpty(para))
                {
                    string resp = "";
                    if (RunAtStartup.model != null)
                    {
                        try
                        {
                            DataRow input = JsonConvert.DeserializeObject<DataRow>(para)!;
                            if (input != null)
                            {
                                Predict pre = new MLContext().Model.CreatePredictionEngine<DataRow, Predict>(RunAtStartup.model).Predict(input);
                                resp = JsonConvert.SerializeObject(pre);
                            }
                        }
                        catch (Exception ex){ resp = ex.Message; }
                    }
                    await context.Response.WriteAsync(resp);
                }
            }
        }
    }
}