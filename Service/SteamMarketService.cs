using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    public class SteamMarketService
    {
        private HttpClient _httpClient;

        public SteamMarketService()
        {
            _httpClient = new HttpClient();
        }

        private async Task<Dictionary<string, object>> ObterGenerico(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(await response.Content.ReadAsStreamAsync());
                    var responseObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(sr.ReadToEnd());
                    return responseObj;
                }
                throw new Exception("Erro ao requisitar " + url);
            }
            catch (Exception)
            {
                throw new Exception("Erro ao requisitar " + url);
            }
        }

        public async Task<Inventario> ObterItensDeUmInventarioAsync(string url)
        {
            try
            {
                var dataStr = "";
                var inventarioDeserializado = null as Inventario;
                var responseObj = await ObterGenerico(url);
                dataStr = JsonConvert.SerializeObject(responseObj);
                inventarioDeserializado = JsonConvert.DeserializeObject<Inventario>(dataStr);
                
                return inventarioDeserializado;
            }
            catch (Exception)
            {
                throw new Exception("error connecting to the CoinLore API");
            }
        }

        //public async Task<Inventario> ObterItensDeUmInventarioAsync(string url)
        //{
        //    try
        //    {
        //        var response = await _httpClient.GetAsync(url);

        //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            var responseObj = new Dictionary<string, object>();
        //            var dataStr = "";
        //            var inventarioDeserializado = null as Inventario;
        //            using (System.IO.StreamReader sr = new System.IO.StreamReader(await response.Content.ReadAsStreamAsync()))
        //            {
        //                responseObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(sr.ReadToEnd());
        //                dataStr = JsonConvert.SerializeObject(responseObj);

        //                inventarioDeserializado = JsonConvert.DeserializeObject<Inventario>(dataStr);
        //            }
        //            return inventarioDeserializado;
        //        }
        //        return new Inventario();
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("error connecting to the CoinLore API");
        //    }
        //}
    }
}
