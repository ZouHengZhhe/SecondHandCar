using UnityEngine;
using UnityEngine.UI;

//登录界面  “RegisterPage”
public class UIRegister : MonoBehaviour
{
    private GameObject SuccessImg;
    private GameObject FailImg;

    private Button _registerBtn;  //登录界面的登录按钮
    private Button _succBtn;       //登录成功界面的确定按钮
    private Button _failBtn;         //登录失败界面的确定按钮

    // Use this for initialization
    private void Start()
    {
        InitUI();

        SuccessImg.SetActive(false);
        FailImg.SetActive(false);
    }

    private void InitUI()
    {
        //得到成功和失败页面
        SuccessImg = this.transform.Find("SuccessImg").gameObject;
        FailImg = this.transform.Find("FailImg").gameObject;

        //得到按钮
        _registerBtn = this.transform.Find("RegisterBtn").GetComponent<Button>();
        _succBtn = SuccessImg.transform.GetChild(0).Find("SuccBtn").GetComponent<Button>();
        _failBtn = FailImg.transform.GetChild(0).Find("FailBtn").GetComponent<Button>();

        //按钮事件注册
        _registerBtn.onClick.AddListener(OnClickRegisterBtn);
        _succBtn.onClick.AddListener(OnClickSuccBtn);
        _failBtn.onClick.AddListener(OnClickFailBtn);
    }

    //登陆按钮点击事件，判断是否登陆成功
    public void OnClickRegisterBtn()
    {
        int data=Restful.Instance.OnRest(RestInterfaceName.RegisterRest);  //登陆验证
        //Player.Instance.IsRegister = true;
        if (data == 0)  //账号存在，登陆成功
        {
            Player.Instance.IsRegister = true;
        }
        else  //账号不存在登陆失败
        {
            Player.Instance.IsRegister = false;
        }

        if (Player.Instance.IsRegister)
        {
            SuccessImg.SetActive(true);
            FailImg.SetActive(false);
        }
        else
        {
            SuccessImg.SetActive(false);
            FailImg.SetActive(true);
        }
    }

    private void OnClickSuccBtn()
    {
        //登录成功，打开我的页面，隐藏其他页面
        SuccessImg.SetActive(false);
        FailImg.SetActive(false);

        UIManager.Instance.ControlParentPages("MyPage");
    }

    private void OnClickFailBtn()
    {
        //登录失败
        SuccessImg.SetActive(false);
        FailImg.SetActive(false);
    }
}