using UnityEngine;
using UnityEngine.UI;

//"我的"界面
public class UIMyPage : MonoBehaviour
{
    public GameObject AccountImg;
    public GameObject RegisterBtn;

    private Text _availableMoneyTxt;  //可用余额
    //private Text _totalMoneyTxt;         //资产总额
    private Text _projectNumTxt;         //众筹项目

    private Button _withDrawBtn;  //提现按钮
    private Button _rechargeBtn;  //充值按钮
    private Image _withDrawBtnImg;  //提现按钮上的Image组件
    private Image _rechargeBtnImg;  //充值按钮上的Image组件
    private Button _myRedPacketBtn;  //我的红包按钮
    private Button _myCrowdfundingBtn;  //我的众筹按钮
    private Button _fundDetailBtn;  //资金明细按钮
    private Button _registerBtn;  //登录按钮

    private Text _accountText;  //显示账号的Text

    public delegate void UpdateDel(); 
    public UpdateDel UpdateMyProjectsCallback =null;  //点击“我的众筹”触发，在RestController中被监听
    public UpdateDel UpdateMyRedPacketCallback = null;  //点击“我的红包”触发，在RestController中被监听
    public UpdateDel UpdateMoneyDetailCallback = null;  //点击“资金明细”出发，在RestController中被监听

    void Start()
    {
        InitUI();
    }

    void Update()
    {
        ControlText();

        if(Player.Instance.IsRegister)
        {
            //登陆状态
            _withDrawBtn.enabled = true;
            _rechargeBtn.enabled = true;
            _withDrawBtnImg.color = new Color(1, 1, 1);
            _rechargeBtnImg.color = new Color(1, 1, 1);
        }
        else
        {
            //未登录状态
            _withDrawBtn.enabled = false;
            _rechargeBtn.enabled = false;
            _withDrawBtnImg.color = new Color(0.5f,0.5f,0.5f);
            _rechargeBtnImg.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }
    
    //得到Text
    private void InitUI()
    {
        //得到“可用余额”、“资产总额”、“众筹项目”
        _availableMoneyTxt = this.transform.Find("AvailableMoneyTxt").GetComponent<Text>();  //可用余额
        //_totalMoneyTxt = this.transform.Find("TotalMoneyTxt").GetComponent<Text>();               //资产总额
        _projectNumTxt = this.transform.Find("ProjectNumTxt").GetComponent<Text>();              //众筹项目

        //得到按钮
        _withDrawBtn = this.transform.Find("WithdrawBtn").GetComponent<Button>();
        _rechargeBtn = this.transform.Find("RechargeBtn").GetComponent<Button>();
        _withDrawBtnImg = _withDrawBtn.GetComponent<Image>();
        _rechargeBtnImg = _rechargeBtn.GetComponent<Image>();
        _myRedPacketBtn = this.transform.Find("MyRedPacketBtn").GetComponent<Button>();
        _myCrowdfundingBtn = this.transform.Find("MyCrowdfundingBtn").GetComponent<Button>();
        _fundDetailBtn = this.transform.Find("FundDetailBtn").GetComponent<Button>();
        _registerBtn = this.transform.Find("RegisterBtn").GetComponent<Button>();

        //控制账号和登录按钮的显示
        RegisterBtn.SetActive(true);
        AccountImg.SetActive(false);

        //按钮监听点击事件
        _withDrawBtn.onClick.AddListener(OnClickWithDrawBtn);
        _rechargeBtn.onClick.AddListener(OnClickRechargeBtn);
        _myRedPacketBtn.onClick.AddListener(OnClickMyRedPacketBtn);
        _myCrowdfundingBtn.onClick.AddListener(OnClickMyCrowFundingBtn);
        _fundDetailBtn.onClick.AddListener(OnClickFundDetailBtn);
        _registerBtn.onClick.AddListener(OnClickRegisterBtn);

        //登陆成功显示账号的Text
        _accountText = AccountImg.transform.Find("Text").GetComponent<Text>();
    }

    //控制显示“可用余额”，“资产总额”，“众筹项目”
    private void ControlText()
    {
        if (!Player.Instance.IsRegister)  //未登陆时
        {
            _availableMoneyTxt.text = "0.00";
            //_totalMoneyTxt.text = "0.00";
            _projectNumTxt.text = "0";
        }
    }

    //检测是否成功登录(在UIController中调用)
    public void OnRegisterSucc(string username)
    {
        //传过来的都是手机号码，1xx xxxx xxxx
        if (Player.Instance.IsRegister)  //登陆成功
        {
            //显示账号
            AccountImg.SetActive(true);
            RegisterBtn.SetActive(false);
            string str1=username.Substring(0,3);
            string str2 = username.Substring(7, 4);
            string str = str1 + "****" + str2;
            _accountText.text = str;
            //更新余额
            string balanceStr = string.Format("{0:F}", Player.Instance.Balance);  //余额保留两位小数
            _availableMoneyTxt.text = balanceStr;
            //更新众筹项目个数
            int joinProNum = Player.Instance.JoinProjectsNum;
            _projectNumTxt.text = joinProNum.ToString();

        }
        else
        {
            AccountImg.SetActive(false);
            RegisterBtn.SetActive(true);
        }
    }

    #region 按钮点击事件

    //充值按钮点击事件
    public void OnClickWithDrawBtn()
    {
        print("显示提现界面");
        UIManager.Instance.ControlChildPages("WithDrawPage");
    }

    //提现按钮点击事件
    public void OnClickRechargeBtn()
    {
        UIManager.Instance.ControlChildPages("RechargePage");
    }

    //我的红包按钮点击事件
    public void OnClickMyRedPacketBtn()
    {
        UIManager.Instance.ControlChildPages("MyPacketPage");

        //加载红包
        UpdateMyRedPacketCallback();
    }

    //我的众筹按钮点击事件
    public void OnClickMyCrowFundingBtn()
    {
        UIManager.Instance.ControlChildPages("MyCrowdfunding");
        //加载项目
        UpdateMyProjectsCallback();
    }

    //资金明细按钮点击事件
    public void OnClickFundDetailBtn()
    {
        UIManager.Instance.ControlChildPages("MoneyDetailPage");
        UpdateMoneyDetailCallback();
    }

    //登录按钮点击事件
    public void OnClickRegisterBtn()
    {
        UIManager.Instance.ControlChildPages("RegisterPage");
    }
    #endregion

}