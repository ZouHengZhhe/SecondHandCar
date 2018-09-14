//红包种类
public enum RedPacketType
{
    First,
    Second,
    Third
}

public class RestData
{
    private static RestData _instance;
    public static RestData Instance
    {
        get
        {
            if (_instance==null)
            {
                _instance = new RestData();
            }
            return _instance;
        }
    }

    public bool IsRegisterStatu;  //现在是否是登录状态
    public float AvailableMoney; //可用余额
    public float TotalMoney;  //资产总额
    public int TotalProjectNum;  //所有众筹项目总数
    public int MyProjectNum;     //我参与的众筹项目数量
    //public MyRedPacket

    //public RestData()
    //{
    //    IsRegisterStatu = true;
    //}
}