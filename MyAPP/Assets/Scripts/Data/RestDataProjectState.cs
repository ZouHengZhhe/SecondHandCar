using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//用于解析众筹记录(接口八，子对象)
public class RestDataProjectState
{
    public int id;                                 //众筹记录ID
    public string projectName;         //项目名称
    public double amount;               //众筹总额
    public double shareAmount;     //分红金额
    public double couponAmount;  //优惠券金额
    public DateTime createTime;  //参与实践
    public double alreadyRaised;  //已筹集
    public int state;  //状态(0:未支付 1：支付)
}
