using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashRegisterLib.Entity
{  
    /// <summary>
    /// 结算信息
    /// </summary>
    public class Settlement
    {  
        /// <summary>
        /// 总价
        /// </summary>
        public float TotalPrice { get; set; }
        /// <summary>
        /// 节省价格
        /// </summary>
        public float FreePrice { get; set; }
        /// <summary>
        /// 节省数量
        /// </summary>
        public int FreeNumber { get; set; }

        public override string ToString()
        {
            if (FreeNumber > 0 || FreePrice == 0)
            {
                return "小计：" + TotalPrice.ToString("f2") + "(元)";

            }
            else
            {
                return "小计：" + TotalPrice.ToString("f2") + "(元)，节省" + FreePrice.ToString("f2") + "(元)";
            }
        }
    }
}
