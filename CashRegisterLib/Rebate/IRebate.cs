using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CashRegisterLib.Entity;

namespace CashRegisterLib.Rebate
{
    /// <summary>
    /// 优惠接口
    /// </summary>
    public interface IRebate
    {
        /// <summary>
        /// 权重 权重越小 优先
        /// </summary>
        int Weight { get; set; }
        /// <summary>
        /// 描述信息 打印优惠信息时的Title
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// 是否打印，每个子类有自己的初始值，需要修改可在初始化时修改
        /// </summary>
        bool IsPrint { get; set; }
        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="goodsnum">商品数量</param>
        /// <param name="price">单价</param>
        /// <returns>结算信息</returns>
        Settlement Calculate(float goodsnum, float price);
    }
}
