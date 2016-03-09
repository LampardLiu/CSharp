using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CashRegisterLib.Entity;

namespace CashRegisterLib.Calculator
{
    public class Calculator
    {
        private static readonly Calculator instance = new Calculator();
        public static Calculator Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="barcodejson">条码字符串</param>
        /// <returns>购物单</returns>
        public OrderList Calculate(string barcodejson)
        {
            var orderList = new OrderList();
            var goodsList = BarcodeAnalysis.Instace.AnalyzeBarcode(barcodejson);
            //结算
            foreach (var goodsorder in goodsList)
            {
                //结算
                goodsorder.SettlementInfo = goodsorder.Rebate.Calculate(goodsorder.Number, goodsorder.GoodsInfo.Price);
                //追加购物单总价
                orderList.TotalPrice += goodsorder.SettlementInfo.TotalPrice;
                //统计优惠信息
                if (goodsorder.SettlementInfo.FreePrice > 0)
                {
                    orderList.FreePrice += goodsorder.SettlementInfo.FreePrice;
                    if (orderList.FreeGoodsList.ContainsKey(goodsorder.Rebate))
                    {
                        orderList.FreeGoodsList[goodsorder.Rebate].Add(goodsorder);
                    }
                    else
                    {
                        orderList.FreeGoodsList.Add(goodsorder.Rebate, new List<GoodsOrder> { goodsorder });
                    }
                }
            }
            orderList.GoodsList = goodsList;
            return orderList;
        }
    }
}
