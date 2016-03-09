using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CashRegisterLib.Entity;

namespace CashRegisterLib.Rebate
{
    /// <summary>
    /// 打折
    /// </summary>
    public class RebateFreePercent : IRebate
    {
        private float percent;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="percent">打折折扣 0~1.0</param>
        public RebateFreePercent(float percent)
        {
            this.percent = percent;
        }

        public Settlement Calculate(float goodsnum, float price)
        {
            var totalPrice = goodsnum * price;
            var settlement = new Settlement()
            {
                TotalPrice = totalPrice * percent
            };
            settlement.FreePrice = totalPrice - settlement.TotalPrice;
            return settlement;
        }

        private int weight = 1;
        public int Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        private bool isPrint = false;
        public bool IsPrint
        {
            get
            {
                return isPrint;
            }
            set
            {
                isPrint = value;
            }
        }

        public override int GetHashCode()
        {
            return Weight;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return obj.GetHashCode() == this.GetHashCode();
        }
    }
}
