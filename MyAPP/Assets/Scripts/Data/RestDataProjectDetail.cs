using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//用于请求项目详细列表（接口7，子对象）
public class RestDataProjectDetail
{
    public int id;  //项目ID
    public string projectName;  //项目名称
    public string category;  //项目类型
    public string initiator; //发起人
    public double maxAmount;  //最大金额
    public double minAmount;  //最小金额
    public double targetAmount;  //目标金额
    public double alreadyRaised;  //已筹集
    public string startTime;  //开始时间
    public string endTime;  //结束时间
    public string licenseTime;  //上牌时间
    public string kilometers;  //公里数
    public string city;  //城市
    public string[] picture;  //缩略图
    public string basicInfo;  //基本信息
    public string repairRecord;  //维修记录
    public string contract;  //合同
    public int supportNum;  //支持人数
    public string mainPicture;  //项目主图片
    public int attention;  //关注数
    public int state;  //状态
}

