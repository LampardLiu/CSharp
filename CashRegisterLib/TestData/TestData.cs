using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CashRegisterLib.Entity;

namespace CashRegisterLib.TestData
{
    public class TestData
    {
        public static List<Goods> AllGoodsBaseInfos = new List<Goods> { 
         new Goods(){
          Barcode="ITEM000003",
          GoodsName ="苹果",
          Price = 5.5f,
          Unit = "斤"             
         },
          new Goods(){
          Barcode="ITEM000001",
          GoodsName ="羽毛球",
          Price = 1,
          Unit = "个"             
         },
          new Goods(){
          Barcode="ITEM000005",
          GoodsName ="可口可乐",
          Price = 3,
          Unit = "瓶"             
         }
        };

        public static List<GoodsRefRebate> AllRebates = new List<GoodsRefRebate>();
    }
}
