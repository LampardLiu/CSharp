using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CashRegisterLib.Entity;

namespace CashRegisterLib.Rebate
{
    /// <summary>
    /// 买N送N活动
    /// </summary>
    public class RebateBuyNFreeN : IRebate
    {
        private int buyNum, freeNum;
        /// <summary>
        ///  构造
        /// </summary>
        /// <param name="buynum">买N</param>
        /// <param name="freenum">送N</param>
        public RebateBuyNFreeN(int buynum, int freenum)
        {
            buyNum = buynum;
            freeNum = freenum;
        }

        public Settlement Calculate(float goodsnum, float price)
        {
            freeNum = (int)goodsnum / (buyNum + freeNum) * freeNum;
            return new Settlement()
            {
                TotalPrice = (goodsnum - freeNum) * price,
                FreePrice = freeNum * price,
                FreeNumber = freeNum
            };
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

        private bool isPrint = true;
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
