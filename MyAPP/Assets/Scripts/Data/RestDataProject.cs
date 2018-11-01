using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class RestDataProject
{
    public string id;
    public string projectName;
    public string mainUrl;  //项目图片
    public double targetAmount;  //项目金额
    public double alreadyAmount;  //已众筹金额
    public int state;  //项目状态
    public int supportNum;  //支持次数
    public string city;  //项目城市

    //默认state为-2
    public RestDataProject()
    {
        state = -2;
    }
}

