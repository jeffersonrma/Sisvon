using System.Configuration;
using System.Net.Http;

namespace Sisvon.Infra.ServiceAgent
{
    internal class HttpConnect
    {
        public string HttpRequest(string cmd)
        {
            using (var client = new HttpClient())
            {
                if (string.IsNullOrEmpty(cmd)) return null;

                string conn = ConfigurationManager.ConnectionStrings["WebApi"].ToString();

                var response = client.GetAsync(conn + cmd).Result;

                if (!response.IsSuccessStatusCode) return null;

                var responseContent = response.Content;
                string responseString = responseContent.ReadAsStringAsync().Result;

                return responseString;
            }

        }


    }
}
