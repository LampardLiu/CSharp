using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CashRegisterLib.Entity;
using System.Runtime.Serialization.Json;
using System.IO;


namespace CashRegisterLib
{
    internal class BarcodeAnalysis
    {
        private static readonly BarcodeAnalysis instance = new BarcodeAnalysis();
        public static BarcodeAnalysis Instace
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// 分析条码信息对应到数据实体
        /// </summary>
        /// <param name="barcodejson">条码字符串</param>
        /// <returns>购物清单列表</returns>
        public List<GoodsOrder> AnalyzeBarcode(string barcodejson)
        {
            //将条码转为String列表
            var barcodes = GetBarcodes(barcodejson);
            return GetGoodsList(barcodes);
        }

        /// <summary>
        /// 将Json形式的条码转成String列表
        /// </summary>
        /// <param name="barcodejson">json格式的条码</param>
        /// <returns>String列表</returns>
        private List<string> GetBarcodes(string barcodejson)
        {
            //防止转换过程中“'”导致异常
            barcodejson = barcodejson.Replace("'", "");
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(barcodejson)))
            {
                return (List<string>)new DataContractJsonSerializer(typeof(List<string>)).ReadObject(ms);
            }

            //DataContractJsonSerializer dsjs = new DataContractJsonSerializer(typeof(List<string>));
            //System.IO.Stream stream = new System.IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(barcodejson));
            //return dsjs.ReadObject(stream) as List<string>;
        }

        /// <summary>
        /// 获取购物清单
        /// </summary>
        /// <param name="barcodes">条码信息</param>
        /// <returns>GoodsOrder的列表</returns>
        private List<GoodsOrder> GetGoodsList(List<string> barcodes)
        {
            List<GoodsOrder> goodslist = new List<GoodsOrder>();
            foreach (var barcode in barcodes)
            {
                //特别处理在条码中包含数量的条码信息
                if (barcode.IndexOf('-') > -1)
                {
                    string[] barcodeAndNumber = barcode.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    float number;
                    if (barcodeAndNumber.Length == 2 && float.TryParse(barcodeAndNumber[1], out number))
                    {
                        AddGoodsOrder(goodslist, barcodeAndNumber[0], number);
                    }
                    else
                    {
                        AddGoodsOrder(goodslist, barcode);
                    }
                }
                else
                {
                    AddGoodsOrder(goodslist, barcode);
                }
            }
            return goodslist;
        }

        /// <summary>
        /// 添加购物信息到购物清单对象
        /// </summary>
        /// <param name="goodslist">购物清单</param>
        /// <param name="barcode">条码</param>
        /// <param name="number">商品数量  默认是1个</param>
        private void AddGoodsOrder(List<GoodsOrder> goodslist, string barcode, float number = 1)
        {
            var goodsInfo = GetGoods(barcode);
            if (goodsInfo != null)
            {
                //已存在该商品信息
                if (ExistGoods(goodslist, goodsInfo))
                {
                    goodslist.Find(item => item.GoodsInfo.Barcode == goodsInfo.Barcode).Number += number;
                }
                else
                {
                    goodslist.Add(new GoodsOrder()
                    {
                        GoodsInfo = goodsInfo,
                        Number = number,
                        Rebate = GetRebate(barcode)
                    });
                }
            }
            else
            {
                throw new Exception("无法查询到此商品");
            }
        }

        /// <summary>
        /// 判断商品是否存在
        /// </summary>
        /// <param name="goodslist"></param>
        /// <param name="goods"></param>
        /// <returns></returns>
        private bool ExistGoods(List<GoodsOrder> goodslist, Goods goods)
        {
            return goodslist.Any(item => item.GoodsInfo.Barcode == goods.Barcode);
        }

        /// <summary>
        /// 得到商品的优惠规则
        /// </summary>
        /// <param name="barcode">商品条码</param>
        /// <returns>优惠规则对象</returns>
        private Rebate.IRebate GetRebate(string barcode)
        {
            if (TestData.TestData.AllRebates.Any(item => item.Barcode == barcode))
            {
                return TestData.TestData.AllRebates.Where(item => item.Barcode == barcode).OrderBy(item => item.Reabte.Weight).ToList()[0].Reabte;
            }
            else
            {
                return new Rebate.RebateNone();
            }
        }

        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <param name="barcode">商品条码</param>
        /// <returns>商品信息</returns>
        private Goods GetGoods(string barcode)
        {
            try
            {
                //这里调用测试数据中数据
                var entity = TestData.TestData.AllGoodsBaseInfos.Find(item => item.Barcode == barcode);
                return new Goods()
                {
                    Barcode = barcode,
                    GoodsName = entity.GoodsName,
                    Unit = entity.Unit,
                    Price = entity.Price
                };
            }
            catch (ArgumentNullException ex)
            {
                return null;
            }
        }
    }
}
