using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CashRegisterLib.Rebate;

namespace CashRegisterLib.Entity
{
    public class GoodsOrder
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public Goods GoodsInfo { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public float Number { get; set; }
        /// <summary>
        /// 优惠规则
        /// </summary>
        public IRebate Rebate { get; set; }
        /// <summary>
        /// 结算信息
        /// </summary>
        public Settlement SettlementInfo { get; set; }

        public override string ToString()
        {
            return "名称：" + GoodsInfo.GoodsName + "，数量："
                + Number + GoodsInfo.Unit + "，单价："
                + GoodsInfo.Price + "(元)，" + SettlementInfo.ToString();
        }

        public string ToFreeString()
        {
            return "名称：" + GoodsInfo.GoodsName + "，数量：" + SettlementInfo.FreeNumber + GoodsInfo.Unit;
        }
    }
}
