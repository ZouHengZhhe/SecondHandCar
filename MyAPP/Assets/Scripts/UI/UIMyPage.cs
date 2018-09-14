using UnityEngine;
using UnityEngine.UI;

//"我的"界面
public class UIMyPage : MonoBehaviour
{
    public GameObject AccountImg;
    public GameObject RegisterBtn;

    private Text _availableMoneyTxt;  //可用余额
    private Text _totalMoneyTxt;         //资产总额
    private Text _projectNumTxt;         //众筹项目

    private Button _withDrawBtn;  //提现按钮
    private Button _rechargeBtn;  //充值按钮
    private Button _myRedPacketBtn;  //我的红包按钮
    private Button _myCrowdfundingBtn;  //我的众筹按钮
    private Button _fundDetailBtn;  //资金明细按钮
    private Button _registerBtn;  //登录按钮

    void Start()
    {


        InitUI();
    }

    void Update()
    {
        ControlText();
        IsRegisterSucc();
    }


    //得到Text
    private void InitUI()
    {
        //得到“可用余额”、“资产总额”、“众筹项目”
        _availableMoneyTxt = this.transform.Find("AvailableMoneyTxt").GetComponent<Text>();
        _totalMoneyTxt = this.transform.Find("TotalMoneyTxt").GetComponent<Text>();
        _projectNumTxt = this.transform.Find("ProjectNumTxt").GetComponent<Text>();

        //得到按钮
        _withDrawBtn = this.transform.Find("WithdrawBtn").GetComponent<Button>();
        _rechargeBtn = this.transform.Find("RechargeBtn").GetComponent<Button>();
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
    }

    //控制显示“可用余额”，“资产总额”，“众筹项目”
    private void ControlText()
    {
        if (Player.Instance.IsRegister)  //登录成功
        {
            _availableMoneyTxt.text =  RestData.Instance.AvailableMoney.ToString("2");
            _totalMoneyTxt.text =  RestData.Instance.TotalMoney.ToString("2");
            _projectNumTxt.text = RestData.Instance.TotalProjectNum.ToString("2");
        }
        else  //登录失败
        {
            _availableMoneyTxt.text = "0.00";
            _totalMoneyTxt.text = "0.00";
            _projectNumTxt.text =  "0";
        }
    }

    //检测是否成功登录
    public void IsRegisterSucc()
    {
        if (Player.Instance.IsRegister)
        {
            AccountImg.SetActive(true);
            RegisterBtn.SetActive(false);
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
    }

    //我的众筹按钮点击事件
    public void OnClickMyCrowFundingBtn()
    {
        UIManager.Instance.ControlChildPages("MyCrowdfunding");
    }

    //资金明细按钮点击事件
    public void OnClickFundDetailBtn()
    {
        UIManager.Instance.ControlChildPages("BuyHistoryPage");
    }

    //登录按钮点击事件
    public void OnClickRegisterBtn()
    {
        UIManager.Instance.ControlChildPages("RegisterPage");
    }
    #endregion

}