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

    private InputField _accountInput;     //账号输入框
    private InputField _passwordInput;  //密码输入框

    //登陆
    public delegate void RegisterDel(string username,string password);
    public RegisterDel RegisterCallback = null;  //登陆验证(在UIController中调用)
    public RegisterDel RegisterSuccCallback = null;  //登陆成功


    private string _username;
    private string _password;

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

        //获取输入框
        _accountInput = this.transform.Find("AccountInput").GetComponent<InputField>();
        _passwordInput = this.transform.Find("PasswordInput").GetComponent<InputField>();
    }

    //登陆按钮点击事件，判断是否登陆成功
    public void OnClickRegisterBtn()
    {
        RegisterCallback(_username,_password);
    }

    private void OnClickSuccBtn()
    {
        //登录成功，打开我的页面，隐藏其他页面
        SuccessImg.SetActive(false);
        FailImg.SetActive(false);

        UIManager.Instance.ControlParentPages("MyPage");
        RegisterSuccCallback(_username, _password);  //在UIController中调用
    }

    private void OnClickFailBtn()
    {
        //登录失败
        SuccessImg.SetActive(false);
        FailImg.SetActive(false);
    }

    //账号输入框赋值
    public void OnAccountValueChanged()
    {
        _username = _accountInput.text;
        //print(_username);
    }

    //密码输入框赋值
    public void OnPasswordValueChanged()
    {
        _password = _passwordInput.text;
        //print(_password);
    }

    //控制登陆成功和登陆失败页面的显示
    public void ControlIsRegisterSuccess(bool isSuc)
    {
        if(isSuc)
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
}