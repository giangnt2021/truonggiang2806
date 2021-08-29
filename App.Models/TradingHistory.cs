using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    public class TradingHistory
    {
        public string id { get; set; }
        public int sequenceMsg { get; set; }
        public string tradingdate { get; set; }
        public string symbol { get; set; }
        public string formattedTime { get; set; }
        public string lastColor { get; set; }
        public decimal formattedMatchPrice { get; set; }
        public string changeColor { get; set; }
        public decimal formattedChangeValue { get; set; }
        public decimal formattedVol { get; set; }
        public decimal formattedAccVal { get; set; }
    }
}
