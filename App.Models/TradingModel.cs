using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    public class TradingModel
    {
        public string MCK { get; set; }
        public decimal Close { get; set; }
        public decimal Open { get; set; }
        public decimal Hight { get; set; }
        public decimal Low { get; set; }
        public decimal Volume { get; set; }
        public decimal TotalVolume { get; set; }
        public string GM1 { get; set; }
        public decimal GM2 { get; set; }
        public decimal GM3 { get; set; }
        public string GB1 { get; set; }
        public decimal GB2 { get; set; }
        public decimal GB3 { get; set; }
        public decimal NNMua { get; set; }
        public decimal NNBan { get; set; }
        public string Exchange { get; set; }
        public decimal Change { get; set; }
        public decimal ChangePercent { get; set; }
    }
}
