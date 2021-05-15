using System;
using Services;

namespace TestaTudo
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {


            var servico = new SteamMarketService();

            var x = await servico.ObterInventarioAsync("https://steamcommunity.com/inventory/76561198097594150/730/2?l=brazilian&count=75");
            Console.ReadKey();
        }
    }
}
