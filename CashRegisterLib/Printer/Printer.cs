using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CashRegisterLib.Entity;
using CashRegisterLib.Rebate;

namespace CashRegisterLib.Printer
{
    /// <summary>
    /// 打印器
    /// </summary>
    public class Printer
    {
        private static readonly Printer instance = new Printer();
        public static Printer Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="orderList">购物单</param>
        /// <param name="title">小票标题</param>
        public void Print(OrderList orderList, string title)
        {
            //打印标题
            Print("\t***" + title + "***");
            //打印购物清单
            PrintOrderList(orderList.GoodsList);
            //打印优惠清单
            PrintFreeOrderList(orderList.FreeGoodsList);
            //打印总金额和优惠金额
            Print(orderList.ToString());
            PrintHorStart();
        }

        /// <summary>
        /// 打印优惠清单
        /// </summary>
        /// <param name="freegoodlist">优惠列表</param>
        private void PrintFreeOrderList(Dictionary<IRebate, List<GoodsOrder>> freegoodlist)
        {
            foreach (var rebate in freegoodlist.Keys)
            {
                if (rebate.IsPrint)
                {
                    Print(rebate.Description + ":");
                    foreach (var goodsOrder in freegoodlist[rebate])
                    {
                        Print(goodsOrder.ToFreeString());
                    }
                }
            }
            if (freegoodlist.Where(item=>item.Key.IsPrint).Count() > 0)
            {
                PrintHorLine();
            }
        }

        /// <summary>
        /// 打印购物清单
        /// </summary>
        /// <param name="goodslist">购物列表</param>
        private void PrintOrderList(List<GoodsOrder> goodslist)
        {
            foreach (var goodsOrder in goodslist)
            {
                Print(goodsOrder.ToString());
            }
            PrintHorLine();
        }

        /// <summary>
        /// 打印消息
        /// </summary>
        /// <param name="msg">消息内容</param>
        private void Print(string msg)
        {
            Console.WriteLine(msg);
        }

        /// <summary>
        /// 打印横线
        /// </summary>
        private void PrintHorLine()
        {
            Console.WriteLine("----------------------");
        }

        /// <summary>
        /// 打印星星
        /// </summary>
        private void PrintHorStart()
        {
            Console.WriteLine("**********************");
        }
    }
}
