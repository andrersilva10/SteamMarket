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

        public async Task<Inventario> ObterInventarioAsync(string url)
        {
            try
            {
                var dataStr = "";
                var inventarioDeserializado = null as Inventario;
                var responseObj = await ObterGenerico(url);
                dataStr = JsonConvert.SerializeObject(responseObj);
                inventarioDeserializado = JsonConvert.DeserializeObject<Inventario>(dataStr);
                await ObterPrecosDosItensDoInventario(inventarioDeserializado);
                return inventarioDeserializado;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private async Task ObterPrecosDosItensDoInventario(Inventario inventario)
        {
            var urlBase = "https://steamcommunity.com/market/priceoverview/?appid=730&currency=7&market_hash_name=";
            foreach (var item in inventario.Descriptions)
            {
                try
                {
                    var marketHashName = Uri.EscapeUriString(item.MarketHashName);
                    var url = urlBase + marketHashName;
                    var responseObj = await ObterGenerico(url);
                    item.LowestPrice = ConverteStringParaDecimal(responseObj["lowest_price"].ToString());
                    item.MedianPrice = ConverteStringParaDecimal(responseObj["median_price"].ToString());
                    item.Volume = (int)ConverteStringParaDecimal(responseObj["volume"].ToString());
                }
                catch(Exception err)
                {

                }

            }
        }

        private decimal ConverteStringParaDecimal(string precoStr)
        {
            var pattern = @"(?<moeda>R\$|\$)";
            var regex = new System.Text.RegularExpressions.Regex(pattern);
            var match = regex.Match(precoStr);
            if(match.Success && match.Groups.Count > 0)
            {
                var moeda = match.Groups["moeda"].ToString();
                var preco = Convert.ToDecimal(precoStr.Replace(moeda, ""));
                return preco;
            }
            throw new Exception("Não foi possivel converter a moeda");
        }
    }
}
