using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CashRegisterLib.Rebate;

namespace CashRegisterLib.Entity
{
    /// <summary>
    /// 商品优惠实体
    /// </summary>
    public class GoodsRefRebate
    {
        /// <summary>
        /// 条码
        /// </summary>
        public string Barcode { get; set; }
        /// <summary>
        /// 优惠规则
        /// </summary>
        public IRebate Reabte { get; set; }
    }
}
