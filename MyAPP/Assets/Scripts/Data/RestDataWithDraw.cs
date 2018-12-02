using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//提现接口请求的参数
public class RestDataWithDraw
{
    public int memID;  //用户ID
    public string accountName;  //开户名
    public string bankCategory;  //开户行
    public string bankNum;  //银行卡号
    public double withdraw;  //提现金额
}
