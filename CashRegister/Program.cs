using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CashRegister
{
    class Program
    {
        static void Main(string[] args)
        {
            string barcodes = @"[
                                    'ITEM000001',
                                    'ITEM000001',
                                    'ITEM000001',
                                    'ITEM000001',
                                    'ITEM000001',
                                    'ITEM000003-2',
                                    'ITEM000005',
                                    'ITEM000005',
                                    'ITEM000005'
                                ]";
            var store = new CashRegisterLib.Entity.Store()
            {
                StoreName = "没钱赚商店"
            };

            #region 没有优惠测试
#if DEBUG
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("没有任何优惠方式");
            Console.ForegroundColor = ConsoleColor.White;
#endif
            PrintTest(barcodes, store);
            #endregion 没有优惠测试

            #region 买二送一测试

#if DEBUG
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("可口可乐和羽毛球买二送一");
            Console.ForegroundColor = ConsoleColor.White;
#endif
            CashRegisterLib.TestData.TestData.AllRebates.Clear();
            CashRegisterLib.TestData.TestData.AllRebates.Add(new CashRegisterLib.Entity.GoodsRefRebate()
            {
                Reabte = new CashRegisterLib.Rebate.RebateBuyNFreeN(2, 1) { Description = "买二赠一商品" },
                Barcode = "ITEM000005"
            });

            CashRegisterLib.TestData.TestData.AllRebates.Add(new CashRegisterLib.Entity.GoodsRefRebate()
            {
                Reabte = new CashRegisterLib.Rebate.RebateBuyNFreeN(2, 1) { Description = "买二赠一商品" },
                Barcode = "ITEM000001"
            });

            CashRegisterLib.TestData.TestData.AllRebates.Add(new CashRegisterLib.Entity.GoodsRefRebate()
            {
                Reabte = new CashRegisterLib.Rebate.RebateFreePercent(0.95f) { Description = "打折优惠", IsPrint = true },
                Barcode = "ITEM000005"
            });

            PrintTest(barcodes, store);
            #endregion 买二送一测试

            #region 打折测试
#if DEBUG
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("苹果打折");
            Console.WriteLine("可口可乐买二曾一");
            Console.ForegroundColor = ConsoleColor.White;
#endif
            CashRegisterLib.TestData.TestData.AllRebates.Clear();
            CashRegisterLib.TestData.TestData.AllRebates.Add(new CashRegisterLib.Entity.GoodsRefRebate()
            {
                Reabte = new CashRegisterLib.Rebate.RebateBuyNFreeN(2, 1) { Description = "买二赠一商品" },
                Barcode = "ITEM000005"
            });

            CashRegisterLib.TestData.TestData.AllRebates.Add(new CashRegisterLib.Entity.GoodsRefRebate()
            {
                Reabte = new CashRegisterLib.Rebate.RebateFreePercent(0.95f) { Description = "打折优惠", IsPrint = false },
                Barcode = "ITEM000003"
            });

            PrintTest(barcodes, store);
            #endregion 打折测试

            Console.ReadLine();

        }

        /// <summary>
        /// 打印测试数据
        /// </summary>
        /// <param name="barcodes">输入条码信息</param>
        /// <param name="store">商店信息</param>
        private static void PrintTest(string barcodes, CashRegisterLib.Entity.Store store)
        {
            var orderList = CashRegisterLib.Calculator.Calculator.Instance.Calculate(barcodes);
            CashRegisterLib.Printer.Printer.Instance.Print(orderList, "<" + store.StoreName + ">购物清单");
        }
    }
}
