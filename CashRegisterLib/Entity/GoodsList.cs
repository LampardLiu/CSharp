using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CashRegisterLib.Rebate;

namespace CashRegisterLib.Entity
{
    public class OrderList
    {
        public OrderList()
        {
            FreeGoodsList = new Dictionary<IRebate, List<GoodsOrder>>();
        }
        /// <summary>
        /// 购物列表
        /// </summary>
        public List<GoodsOrder> GoodsList { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public float TotalPrice { get; set; }
        /// <summary>
        /// 节省价格
        /// </summary>
        public float FreePrice { get; set; }
        /// <summary>
        /// 优惠物品清单
        /// </summary>
        public Dictionary<IRebate, List<GoodsOrder>> FreeGoodsList { get; set; }

        /// <summary>
        /// 重写ToString，输出总金额和优惠金额
        /// </summary>
        public override string ToString()
        {
            if (FreePrice > 0)
            {
                return "总计：" + TotalPrice.ToString("f2") + "(元)\r\n节省：" + FreePrice.ToString("f2") + "(元)";
            }
            else
            {
                return "总计：" + TotalPrice.ToString("f2") + "(元)";
            }
        }
    }
}
