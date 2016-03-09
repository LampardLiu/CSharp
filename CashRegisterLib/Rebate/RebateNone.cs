using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CashRegisterLib.Entity;

namespace CashRegisterLib.Rebate
{
    /// <summary>
    /// 没有优惠 正常结算
    /// </summary>
    public class RebateNone : IRebate
    {
        public Settlement Calculate(float goodsnum, float price)
        {
            return new Settlement()
            {
                TotalPrice = goodsnum * price
            };
        }

        private int weight = int.MaxValue;
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

        private bool isPrint;
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
