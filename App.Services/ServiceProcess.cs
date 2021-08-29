using App.Library;
using App.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public class ServiceProcess : IServiceProcess
    {
        private readonly ServiceConfig config;
        private readonly ILogger<ServiceProcess> logger;

        public ServiceProcess(IOptions<ServiceConfig> config, ILogger<ServiceProcess> logger)
        {
            this.config = config.Value;
            this.logger = logger;
        }
        public async Task Crawldata()
        {
            logger.LogInformation($"TimeStick {config.TimeTick} !!!");
            Console.Clear();
            var now = DateTime.Now;
            var timeATO = new DateTime(now.Year, now.Month, now.Day, 9, 15, 15);
            var timeATC = new DateTime(now.Year, now.Month, now.Day, 14, 30, 00);
            var timeOpen = new DateTime(now.Year, now.Month, now.Day, 9, 00, 00);
            var timeClose = new DateTime(now.Year, now.Month, now.Day, 15, 00, 00);
            if (now.TimeOfDay >= timeOpen.TimeOfDay && now.TimeOfDay <= timeClose.TimeOfDay && now.Date.DayOfWeek != DayOfWeek.Saturday && now.Date.DayOfWeek != DayOfWeek.Sunday)
            {
                List<TradingModel> input = new List<TradingModel>();
                Console.WriteLine($"Running: {now.ToString("HH:mm:ss")} ");
                foreach (var exchange in "HOSE,HNX,UPCOM".Split(','))
                {
                    var client = new RestClient($"https://online.bvsc.com.vn/datafeed/instruments?exchange={exchange}");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.GET);
                    IRestResponse response = client.Execute(request);
                    var objData = JsonConvert.DeserializeObject<JObject>(response.Content)["d"];
                    var lstData = JsonConvert.DeserializeObject<List<BVSCORDER>>(objData.ToString());
                    
                    foreach (var item in lstData)
                    {
                        if (item.exchange != "UPCOM" && (now.TimeOfDay < timeATO.TimeOfDay || now.TimeOfDay > timeATC.TimeOfDay))
                        {
                            continue;
                        }
                        if (item.StockType == "3" || item.StockType == "4" || item.closePrice <= 7)
                        {
                            continue;
                        }
                        if (item.changePercent > -1 && item.changePercent <= 3 && item.closePrice >= 7000 && item.closePrice < item.ceiling && item.high == item.closePrice && item.low <= item.open && item.totalTrading >= 50000)
                        {
                            input.Add(new TradingModel
                            {
                                MCK = item.symbol,
                                Close = item.closePrice,
                                Exchange = item.exchange,
                                Hight = item.high,
                                Open = item.open,
                                Low = item.low,
                                Volume = item.closeVol,
                                TotalVolume = item.totalTrading,
                                GB1 = item.offerPrice1,
                                GB2 = item.offerPrice2,
                                GB3 = item.offerPrice3,
                                GM1 = item.bidPrice1,
                                GM2 = item.bidPrice2,
                                GM3 = item.bidPrice3,
                                NNBan = item.foreignSell,
                                NNMua = item.foreignBuy,
                                Change = item.change,
                                ChangePercent = Convert.ToDecimal(string.Format("{0:0,#.00}", item.changePercent)) 
                            }); ;
                            if (item.closeVol >= 250000)
                            {
                                var msg = $"{item.symbol} Volumn: {string.Format("{0:#,0}", item.closeVol)} Price: {string.Format("{0:#,0}", item.closePrice)} Time: {now.ToString("HH:mm:ss")}";
                                PushTelegram(msg);
                            }
                        }
                        if ((item.bidPrice1 != "ATC" || item.bidPrice1 != "ATO"))
                        {

                            if (item.closeVol >= 300000)
                            {
                                var msg = $"{item.symbol} Volumn: {string.Format("{0:#,0}", item.closeVol)} Price: {string.Format("{0:#,0}", item.closePrice)} Time: {now.ToString("HH:mm:ss")}";
                                PushTelegram(msg);
                            }
                        }
                    }                    
                }
                if (input.Count > 0)
                {
                    var client = new RestClient($"{config.ApiHost}/tin-hieu-mua-ban");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("accept", "*/*");
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("application/json", JsonConvert.SerializeObject(input.OrderByDescending(t=>t.TotalVolume)), ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    Console.WriteLine(response.Content);
                }
            }
            await Task.Delay(TimeSpan.FromSeconds(config.TimeTick));
        }
        public static void PushTelegram(string message)
        {
            /*Bắn sang telegram*/
            string TOKEN_ID = "1752650613:AAF6IkUKdkFV_eOMmudMhlvMHL3aVHJ1B7Q";
            string CHAT_ID = "566483008";
            var client = new RestClient($"https://api.telegram.org/bot{TOKEN_ID}/sendMessage?chat_id=-{CHAT_ID}&text={message}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
        }
    }
}
