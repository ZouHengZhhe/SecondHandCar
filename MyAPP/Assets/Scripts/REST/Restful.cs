using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Restful : MonoBehaviour
{
    private static Restful _instance;
    public static Restful Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Restful();
            }
            return _instance;
        }
        private set { _instance = value; }
    }

    private void Awake()
    {
        _instance = this;
        //GetProjectsList();
    }

    //private string _getUrl= "http://jrdcar.com/front/totalAssets";
    private string _totalAssetsUrl = "http://jrdcar.com/front/totalAssets";
    
    private void Start()
    {
        
    }

    //累计注册会员数
    private IEnumerator IEHttpGet2(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.Send();

            if (www.isError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);

                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
            }
        }
    }

    //项目列表
    private IEnumerator IEHttpGet3(string url)
    {
        //加入http 头信息
        Dictionary<string, string> JsonDic = new Dictionary<string, string>();
        JsonDic.Add("Content-Type", "application/json");

        Dictionary<string, int> UserDic = new Dictionary<string, int>();
        UserDic["offset"] = 0;
        UserDic["limit"] = 1;
        string data = JsonMapper.ToJson(UserDic);
        byte[] byteData = Encoding.UTF8.GetBytes(data);

        WWW www = new WWW(url, byteData, JsonDic);
        yield return www;

        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        print(www.text);
    }

    //1、Post，登录验证
    private IEnumerator IEPost1(string phoneNum, string password)
    {
        string url = "http://jrdcar.com/front/login?phoneNum=" + phoneNum + "&password=" + password;
        WWWForm form = new WWWForm();
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.Send();

            if (www.isError)
            {
                Debug.Log(www.error);
            }
            else
            {
                print(www.downloadHandler.text);
                //解析结果
                RestDataRegister data = JsonMapper.ToObject<RestDataRegister>(www.downloadHandler.text);
                //RegisterCallback(data.code, phoneNum, password, data.id);
            }
        }
    }

    //2、Get，用户可用余额(不支持带参数的GET)
    private IEnumerator IEGetBalance()
    {
        string url = "http://jrdcar.com/front/memWallet";

        Dictionary<string, int> dic = new Dictionary<string, int>();
        dic.Add("memID", 7);
        string json = JsonMapper.ToJson(dic);
        print("dkfs");
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            byte[] data = Encoding.UTF8.GetBytes(json);
            UploadHandlerRaw upHandler = new UploadHandlerRaw(data);
            upHandler.contentType = "application/json";
            www.uploadHandler = upHandler;
            yield return www.Send();

            if (www.isError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("Form upload complete!");
                print(www.downloadHandler.text);
            }
        }
    }

    //3、Get，网站资产总额
    private IEnumerator IEGet(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.Send();

            if (request.isError)
            {
                Debug.Log("Get请求出错!");
            }
            else
            {
                print(request.downloadHandler.text);
            }
        }
    }

    //-----------------------------------------------------------------------------------------------
    //不带参数的get请求，同步
    private string NoParameterGet(string url)
    {
        string result = "";
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        Stream stream = resp.GetResponseStream();
        try
        {
            //获取内容
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }
        }
        finally
        {
            stream.Close();
        }
        return result;
    }

    //带参数的Get请求，同步
    private string HaveParameterGet(string url, Dictionary<string, int> dic)
    {
        string result = "";
        StringBuilder builder = new StringBuilder();
        builder.Append(url);
        if (dic.Count > 0)
        {
            builder.Append("?");
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
        }
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(builder.ToString());
        //添加参数
        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        Stream stream = resp.GetResponseStream();
        try
        {
            //获取内容
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }
        }
        finally
        {
            stream.Close();
        }
        return result;
    }

    //不带参数的Post请求
    private string Post(string url)
    {
        string result = "";
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        req.Method = "POST";
        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        Stream stream = resp.GetResponseStream();
        //获取内容
        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
        {
            result = reader.ReadToEnd();
        }
        return result;
    }

    //带参数的Post请求，指定键值对
    private string Post(string url, Dictionary<string, string> dic)
    {
        string result = "";
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        req.Method = "POST";
        req.ContentType = "application/x-www-form-urlencoded";
        #region 添加Post 参数
        StringBuilder builder = new StringBuilder();
        int i = 0;
        foreach (var item in dic)
        {
            if (i > 0)
                builder.Append("&");
            builder.AppendFormat("{0}={1}", item.Key, item.Value);
            i++;
        }
        byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
        req.ContentLength = data.Length;
        using (Stream reqStream = req.GetRequestStream())
        {
            reqStream.Write(data, 0, data.Length);
            reqStream.Close();
        }
        #endregion
        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        Stream stream = resp.GetResponseStream();
        //获取响应内容
        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
        {
            result = reader.ReadToEnd();
        }
        return result;
    }

    //带参数的Post请求，指定发送字符串内容
    private string Post(string url, string content)
    {
        string result = "";
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        req.Method = "POST";
        req.ContentType = "application/x-www-form-urlencoded";

        #region 添加Post 参数
        byte[] data = Encoding.UTF8.GetBytes(content);
        req.ContentLength = data.Length;
        using (Stream reqStream = req.GetRequestStream())
        {
            reqStream.Write(data, 0, data.Length);
            reqStream.Close();
        }
        #endregion

        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        Stream stream = resp.GetResponseStream();
        //获取响应内容
        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
        {
            result = reader.ReadToEnd();
        }
        return result;
    }

    //====================================================

    //网站统计，得到项目总数，注册总数，累计众筹金额
    public RestDataStatistics GetStatistics()
    {
        string url = "http://jrdcar.com/front/statistics";
        string str = NoParameterGet(url);
        RestDataStatistics statistics = JsonMapper.ToObject<RestDataStatistics>(str);
        //print("projectCount = " + statistics.data.projectCount);
        //print("memberCount = " + statistics.data.memberCount);
        //print("raiseCount = " + statistics.data.raiseCount);
        return statistics;
    }

    //得到项目列表
    public RestDataProjectList GetProjectsList()
    {
        string url = "http://jrdcar.com/front/projectList";
        Dictionary<string, int> dic = new Dictionary<string, int>();
        string str = "";
        int total = JsonMapper.ToObject<RestDataProjectList>(HaveParameterGet(url, dic)).total;
        int count = total / 10 + 1;
        RestDataProjectList result = new RestDataProjectList();
        for (int i = 0; i < count; i++)
        {
            if(dic.ContainsKey("offset")&&dic.ContainsKey("limit"))  //非第一次加载
            {
                dic["offset"] = 0 + i * 10;
                dic["limit"] = 10;
            }
            else  //第一次加载
            {
                dic.Add("offset", 0 + i * 10);
                dic.Add("limit", 10);
            }
            
            str = HaveParameterGet(url, dic);
            //数组叠加
            RestDataProjectList data = JsonMapper.ToObject<RestDataProjectList>(str);//rest接口获得数据，并进行解析
            if (data.rows.Length==0)
            {
                break;
            }
            result.total = data.total;
            int length = result.rows.Length;
            int length1 = length+data.rows.Length;
            RestDataProject[] array = result.rows;   //将已经取得的结果存入array数组
            result.rows = new RestDataProject[length1];
            for(int j =0;j<length1;j++)
            {
                if(j<length)  //将之前的结果重新存入result
                {
                    result.rows[j] = array[j];
                }
                else
                {
                    result.rows[j] = data.rows[j-length];
                }
            }
        }
        return result;
        
    }


    //1、登陆验证
    public RestDataRegister GetRegister(string username, string password)
    {
        string url = "http://jrdcar.com/front/login?phoneNum=" + username + "&password=" + password;
        string result = Post(url);
        //解析返回值
        RestDataRegister data = JsonMapper.ToObject<RestDataRegister>(result);
        return data;
    }

    //2、得到余额,url:http://jrdcar.com/front/memWallet
    public double GetBalance(int playerId)
    {
        string url = "http://jrdcar.com/front/memWallet";
        Dictionary<string, int> dic = new Dictionary<string, int>();
        dic.Add("memID", playerId);
        string str = HaveParameterGet(url, dic);
        //解析json
        RestDataBalance data = JsonMapper.ToObject<RestDataBalance>(str);
        return data.count;
    }

    //3、得到众筹记录(众筹项目个数)
    public RestDataJoinProject GetJoinProjects(int memberId )
    {
        string url = "http://jrdcar.com/front/joinProject";
        Dictionary<string, int> dic = new Dictionary<string, int>();

        dic.Add("memberId", memberId);  //用户ID
        int total = JsonMapper.ToObject<RestDataJoinProject>(HaveParameterGet(url, dic)).total;  //得到总数
        int count = total / 10 + 1;  //计算出要请求几页
        string str = "";
        RestDataJoinProject result = new RestDataJoinProject();
        for (int i=0;i< count; i++)
        {
            if (dic.ContainsKey("offset") && dic.ContainsKey("limit"))
            {
                //第一次循环
                dic["offset"] = 0 + i * 10;
                dic["limit"] = 10;
            }
            else
            {
                dic.Add("offset", 0 + i * 10);
                dic.Add("limit", 10);
            }
            str = HaveParameterGet(url, dic);
            //数组叠加
            RestDataJoinProject data = JsonMapper.ToObject<RestDataJoinProject>(str);
            if (data.rows.Length == 0)
            {
                break;
            }
            result.total = data.total;
            //如果是第一次循环请求，length1=0；否则，length1
            int length1 = result.rows.Length;  
            int length = length1 + data.rows.Length;  //之前的数组长度+这次请求得到结果的长度，总长度
            RestDataProjectState[] array = result.rows;
            result.rows = new RestDataProjectState[length];
            for (int j = 0; j < length; j++)
            {
                if (j < length1)
                {
                    result.rows[j] = array[j];
                }
                else
                {
                    result.rows[j] = data.rows[j];
                }
            }
        }
        return result;

    }

    //得到项目详细
    public RestDataProjectInfo GetProjectDetail(int id)
    {
        string url = "http://jrdcar.com/front/projectDetail";
        Dictionary<string, int> dic = new Dictionary<string, int>();
        dic.Add("id", id);
        string str = HaveParameterGet(url, dic);
        RestDataProjectInfo proInfo = JsonMapper.ToObject<RestDataProjectInfo>(str);
        return proInfo;
    }

    //得到用户所拥有的红包
    public RestDataRedPacketInfo GetRedPacket(int memberId)
    {
        string url = "http://jrdcar.com/front/memCoupons";
        //string url = "http://jrdcar.com/front/availableCoupons";
        Dictionary<string, int> dic = new Dictionary<string, int>();

        dic.Add("memID", memberId);  //用户ID
        int total = JsonMapper.ToObject<RestDataRedPacketInfo>(HaveParameterGet(url, dic)).total;  //得到总数
        int count = total / 10 + 1;  //计算出要请求几页
        string str = "";
        RestDataRedPacketInfo result = new RestDataRedPacketInfo();
        for (int i = 0; i < count; i++)
        {
            if (dic.ContainsKey("page") && dic.ContainsKey("limit"))
            {
                //第一次循环
                dic["page"] = 0 + i * 10;
                dic["limit"] = 10;
            }
            else
            {
                dic.Add("page", 0 + i * 10);
                dic.Add("limit", 10);
            }
            str = HaveParameterGet(url, dic);
            //数组叠加
            RestDataRedPacketInfo data = JsonMapper.ToObject<RestDataRedPacketInfo>(str);
            if (data.rows.Length == 0)
            {
                break;
            }
            result.total = data.total;
            //如果是第一次循环请求，length1=0；否则，length1
            int length1 = result.rows.Length;
            int length = length1 + data.rows.Length;  //之前的数组长度+这次请求得到结果的长度，总长度
            RestDataRedPacketDetail[] array = result.rows;
            result.rows = new RestDataRedPacketDetail[length];
            for (int j = 0; j < length; j++)
            {
                if (j < length1)
                {
                    result.rows[j] = array[j];
                }
                else
                {
                    result.rows[j] = data.rows[j];
                }
            }
        }
        return result;
    }

    //提现接口
    public string PostWithDraw(RestDataWithDraw data)
    {
        string url = "http://jrdcar.com/front/withdrawCash";
        string content = JsonMapper.ToJson(data);
        print("content : " + content);
        string result = Post(url, content);
        print(result);
        RestDataWithDrawBack back= JsonMapper.ToObject<RestDataWithDrawBack>(result);
        return back.msg;
    }
}