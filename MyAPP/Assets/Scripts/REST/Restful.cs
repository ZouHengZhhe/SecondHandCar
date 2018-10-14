using LitJson;
using System.Collections;
using System.Collections.Generic;
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

    private int _registerCode=-1;

    private void Awake()
    {
        _instance = this;
    }

    //private string _getUrl= "http://jrdcar.com/front/totalAssets";
    private string _totalAssetsUrl = "http://jrdcar.com/front/totalAssets";

    // Use this for initialization
    private void Start()
    {
        StartCoroutine(IEPost1("15618028732", "zhhe9008321334"));

        //StartCoroutine(IEGet1());

        string url = "http://jrdcar.com/front/totalAssets";
        url = "http://jrdcar.com/front/countMem";
        url = "http://jrdcar.com/front/memWallet?memID=4";
        url = "http://jrdcar.com/front/memCoupons?memID=4";
        url = "http://jrdcar.com/front/countMem";
        url = "http://jrdcar.com/front/projectList";
        url = "http://jrdcar.com/front/joinProject?memID=7";
        url = "http://jrdcar.com/front/statistics";
        url = "http://jrdcar.com/front/projectDetail?id=82";
        StartCoroutine(IEGet(url));
    }

    //外部调用Rest接口
    public int OnRest(string str)
    {
        StartCoroutine(IEPost1("15618028732", "zhhe9008321334"));
        return _registerCode;
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
                RestRegisterData data=JsonMapper.ToObject<RestRegisterData>(www.downloadHandler.text);
                _registerCode = data.code;
            }
        }
    }

    //2、Get，用户可用余额(不支持带参数的GET)
    private IEnumerator IEGet1()
    {
        string url = "http://jrdcar.com/front/memWallet";

        Dictionary<string, int> dic = new Dictionary<string, int>();
        dic.Add("memID", 4);
        string json = JsonMapper.ToJson(dic);

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
}