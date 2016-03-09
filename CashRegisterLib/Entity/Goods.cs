using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashRegisterLib.Entity
{
    public class Goods
    {
        /// <summary>
        /// 条码
        /// </summary>
        public string Barcode { get; set; }
        /// <summary>
        /// 商品名
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public float Price { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
    }
}
