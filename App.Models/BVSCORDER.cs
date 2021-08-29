using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Models
{
    public class BVSCORDER
    {
        public string symbol { get; set; }
        public string FullName { get; set; }
        public string StockType { get; set; }
        public decimal ceiling { get; set; }
        public decimal floor { get; set; }
        public decimal reference { get; set; }
        public decimal bidPrice3 { get; set; }
        public decimal bidVol3 { get; set; }
        public decimal bidPrice2 { get; set; }
        public decimal bidVol2 { get; set; }
        public string bidPrice1 { get; set; }
        public decimal bidVol1 { get; set; }
        public decimal closePrice { get; set; }
        public decimal closeVol { get; set; }
        public decimal change { get; set; }
        public string offerPrice1 { get; set; }
        public decimal offerVol1 { get; set; }
        public decimal offerPrice2 { get; set; }
        public decimal offerVol2 { get; set; }
        public decimal offerPrice3 { get; set; }
        public decimal offerVol3 { get; set; }
        public decimal totalTrading { get; set; }
        public decimal open { get; set; }
        public decimal high { get; set; }
        public decimal low { get; set; }
        public decimal changePercent { get; set; }
        public decimal foreignBuy { get; set; }
        public decimal foreignSell { get; set; }
        public string exchange { get; set; }
    }
}
