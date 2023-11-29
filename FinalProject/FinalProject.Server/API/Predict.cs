using FinalProject.Core._Class;
using FinalProject.Server.Models;
using Microsoft.ML;
using Newtonsoft.Json;

namespace FinalProject.Server.API
{
    public class Predict
    {
        public static string Execute(HttpContext context)
        {
            string resp = "";

            var arg = context.Request.RouteValues["arg"];
            if (arg != null)
            {
                string para = arg.ToString()!;
                if (!string.IsNullOrEmpty(para))
                {
                    if (RunAtStartup.model != null)
                    {
                        try
                        {
                            DataRow input = JsonConvert.DeserializeObject<DataRow>(para)!;
                            if (input != null)
                            {
                                Prediction pre = new MLContext().Model.CreatePredictionEngine<DataRow, Prediction>(RunAtStartup.model).Predict(input);
                                resp = JsonConvert.SerializeObject(pre);
                            }
                        }
                        catch (Exception ex){ resp = ex.Message; }
                    }

                }
            }

            return resp;
        }
    }
}