using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solari.App.Core.Helpers
{
    public class Response
    {
        public string errors { get; set; }
    }

    public static class TransformValidationErrors
    {
        public static string Transform(string content)
        {
            dynamic response = JsonConvert.DeserializeObject(content);

            string message = "";

            foreach(var x in response.errors)
            {
                foreach (var y in x)
                {
                    message += x.Key + ": " + y + "\n";
                }
            }

            return message;
            try
            {
                

                //Debug.WriteLine(JsonConvert.DeserializeObject<Response>(@"{errors:[]}").errors.ToString());

                return content;
            }
            catch
            {
                return content;
            }
        }
    }
}
