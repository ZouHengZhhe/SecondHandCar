using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Buy : MonoBehaviour
{
    private UdpClient client;
    private Thread SendThread;
    private string CommodityName = "充值余额";//商品名称
    private string BuyCuont = "0";//购买的数量
    private string serverIP = "47.110.225.20";
    //我发送一个购买的商品名称给服务器，服务器根据商品名称，获得一个字符串 未加签，然后再将这个字符串加签 返回给客户端

    private Thread GetThread;

    public InputField NumInput;

    private void Start()
    {
        NumInput.onValueChanged.AddListener(OnNumInputValueChanged);

        //获取本机IP
        foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
            if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
            {
                //在实例化的时候,绑定客户端的IP和端口,如果不绑定,路由不会自动处理内网IP和外网IP的转换,使得只能发送数据,无法接收数据.
                client = new UdpClient(new IPEndPoint(_IPAddress, 7789));
            }
        }
        //接收服务端返回的数据 在线程里接收 更建议项目中采用异步的方式.
        GetThread = new Thread(GetStringInfo);
        GetThread.IsBackground = true;
        GetThread.Start();
    }

    #region 从服务端获取最终的请求安卓支付的参数,然后在主线程调安卓发起支付请求.
    private bool zhifu = false;
    private string strInfo = "";//最终加签后的字符串

    private void GetStringInfo()
    {
        while (true)
        {
            //接收数据
            IPEndPoint point = new IPEndPoint(IPAddress.Parse(serverIP), 0);
            //通过point 确定数据来自哪个ip的哪个端口,ref是代表可修改后面的数据
            byte[] data = client.Receive(ref point);
            strInfo = Encoding.UTF8.GetString(data);

            //如果接收到数据,则要在主线程去调用安卓的支付方法.
            zhifu = strInfo == "" ? false : true;
        }
    }

    private void Update()
    {
        //不能直接在其它线程中调用调用安卓层的方法,而Update是在主线程执行的
        if (zhifu)
        {
            //实际项目还要对消息加多一层判断 确认接收到的是加签后的数据 也就是跟服务端定个协议.
            Debug.Log(strInfo);
            //调用安卓的固定方式,通过AndroidJavaClass与AndroidJavaObject 具体参考官方文档,有这些情况: 调无参的方法、调带参数的方法、无返回值、有返回值的
            AndroidJavaClass uintyPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");//参数必须为com.unity3d.player.UnityPlayer
            AndroidJavaObject currentActivity = uintyPlayer.GetStatic<AndroidJavaObject>("currentActivity");//参数必须为currentActivity

            currentActivity.Call("PayING", strInfo);
            strInfo = "";
            zhifu = false;
        }
    }

    private void OnNumInputValueChanged(string str)
    {
        if(str=="")
        {
            BuyCuont = "0";
        }
        else
        {
            BuyCuont = str;
        }
        print(str);
    }

    #endregion
    #region 告诉服务端要购买商品了,购买的商品名称和数量,让它返回一个参数,可以调安卓支付的时候传递.

    /// <summary>
        /// 购买物品 需要将该方法绑定到按钮上
        /// </summary>
    public void BuyObject()
    {
        SendThread = new Thread(SendData);
        SendThread.Start();
    }

    //发送数据的线程
    private void SendData()
    {
        //发送数据
        string message = CommodityName + "*" + BuyCuont.ToString();
        byte[] data = Encoding.UTF8.GetBytes(message);
        client.Send(data, data.Length, new
        IPEndPoint(IPAddress.Parse(serverIP), 7788));

        SendThread.Abort();//中止线程
    }

    #endregion

    //正常关闭应用程序时候调用的,需要终止线程、关闭socket
    private void OnApplicationQuit()
    {
        client.Close();
        GetThread.Abort();
        SendThread.Abort();
    }
}