using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Metro.Servies
{
    public static class NetManage
    {
        public static HttpClient httpClient = new HttpClient();
        public async static Task<T> Get<T>(string controller)
        {
            var response = await httpClient.GetAsync("http://localhost:59567/" + controller);
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(content);
            return data;
        }
    }
}
