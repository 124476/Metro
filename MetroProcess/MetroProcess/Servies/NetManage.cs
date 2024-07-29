using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Metro.Servies
{
    public class NetManage
    {
        public static HttpClient httpClient = new HttpClient();
        public async static Task<T> Get<T>(string controller)
        {
            var response = await httpClient.GetAsync("http://localhost:59567/" + controller);
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(content);
            return data;
        }
        public async static void Set(string controller, object T)
        {
            var content = new StringContent(JsonConvert.SerializeObject(T), Encoding.UTF8, "application/json");
            await httpClient.PutAsync("http://localhost:59567/" + controller, content);
        }
    }
}
