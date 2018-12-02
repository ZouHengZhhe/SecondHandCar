using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMyProject : MonoBehaviour
{
    private ScrollRect rect;

    private Text _projectNameTxt;  //项目名字
    private Image _projectImg;  //项目图片
    private Text _targetAmountTxt;  //筹集目标
    private Text _minAmountTxt; //最小金额
    private Text _maxAmountTxt; //最大金额
    private Text _startTimeTxt; //开始时间
    private Text _stopTimeTxt;  //结束时间
    private Text _supportNumTxt;  //支持人数
    private Text _alreadyAccountTxt;  //已筹集
    private Text _waitAccountTxt;  //待筹集

    private Text _licenseTimeTxt;  //上牌时间
    private Text _kilometersTxt;  //行驶里程
    private Text _cityTxt;  //城市

    private Button _supportBtn;  //支持按钮
    private GameObject _payPage;  //支持页面
    //支持页面上的元素
    private Button _submitBtn;  //支持页面上的提交按钮
    private Button _closeBtn;   //支持页面上的关闭按钮
    private Text _titleTxt;  //项目名称
    private Text _totalTxt;  //集资目标
    private Text _remainTxt;  //剩余金额
    private Text _unitPriceTxt;  //支持单价
    private InputField _countInput;
    private InputField _pwdInput;
    private string _count;
    private string _pwd;
    private Dropdown _packageDropDown;  //红包选择下拉框


    private GameObject _msgPage;  //提示信息页面（显示支付提交后，后台返回的消息）
    private Text _msgTxt;   //显示提示信息
    private Button _ensureBtn;  //确定按钮

    // Use this for initialization
    private void Awake()
    {
        rect = this.transform.Find("Panel").GetChild(0).GetComponent<ScrollRect>();

        _projectNameTxt = this.transform.Find("ProjectName/Text").GetComponent<Text>();
        _projectImg = this.transform.Find("Panel/Scroll View/Content").Find("ProjectImg").GetComponent<Image>();
        _targetAmountTxt = this.transform.Find("Panel/Scroll View/Content").Find("TargetAmountTxt").GetComponent<Text>();
        _minAmountTxt = this.transform.Find("Panel/Scroll View/Content").Find("MinAmountTxt").GetComponent<Text>();
        _maxAmountTxt = this.transform.Find("Panel/Scroll View/Content").Find("MaxAmountTxt").GetComponent<Text>();
        _startTimeTxt = this.transform.Find("Panel/Scroll View/Content").Find("StartTimeTxt").GetComponent<Text>();
        _stopTimeTxt = this.transform.Find("Panel/Scroll View/Content").Find("StopTimeTxt").GetComponent<Text>();
        _supportNumTxt = this.transform.Find("Panel/Scroll View/Content").Find("SupportNumTxt").GetComponent<Text>();
        _alreadyAccountTxt = this.transform.Find("Panel/Scroll View/Content").Find("AlreadyAccountTxt").GetComponent<Text>();
        _waitAccountTxt = this.transform.Find("Panel/Scroll View/Content").Find("WaitAccountTxt").GetComponent<Text>();

        _licenseTimeTxt = this.transform.Find("Panel/Scroll View/Content/BasicInfoImg").Find("LicenseTimeTxt").GetComponent<Text>();
        _kilometersTxt = this.transform.Find("Panel/Scroll View/Content/BasicInfoImg").Find("KilometersTxt").GetComponent<Text>();
        _cityTxt = this.transform.Find("Panel/Scroll View/Content/BasicInfoImg").Find("CityTxt").GetComponent<Text>();

        //支持按钮
        _supportBtn = this.transform.Find("Panel/Scroll View/Content/SupportBtn").GetComponent<Button>();
        _supportBtn.onClick.AddListener(OnClickSupportBtn);

        //得到支持页面，和支持页面上的元素
        _payPage = this.transform.Find("PayPage").gameObject;
        _submitBtn = this.transform.Find("PayPage").GetChild(0).Find("SubmitBtn").GetComponent<Button>();
        _submitBtn.onClick.AddListener(OnClickSubmitBtn);
        _closeBtn = _payPage.transform.GetChild(0).Find("CloseBtn").GetComponent<Button>();
        _closeBtn.onClick.AddListener(OnClickCloseBtn);
        _titleTxt = _payPage.transform.GetChild(0).Find("TitleTxt").GetComponent<Text>();
        _totalTxt = _payPage.transform.GetChild(0).Find("TotalTxt").GetComponent<Text>();
        _remainTxt = _payPage.transform.GetChild(0).Find("RemainTxt").GetComponent<Text>();
        _unitPriceTxt = _payPage.transform.GetChild(0).Find("UnitPriceTxt").GetComponent<Text>();
        _countInput = _payPage.transform.GetChild(0).Find("CountInput").GetComponent<InputField>();
        _pwdInput = _payPage.transform.GetChild(0).Find("PasswordInput").GetComponent<InputField>();
        _countInput.onValueChanged.AddListener(OnValueChangeCountInput);
        _pwdInput.onValueChanged.AddListener(OnValueChangePwdInput);
        _packageDropDown= _payPage.transform.GetChild(0).Find("PacketDropdown").GetComponent<Dropdown>();
        _payPage.SetActive(false);

        //提示信息页面
        _msgPage = this.transform.Find("MsgPage").gameObject;
        _msgTxt = _msgPage.transform.GetChild(0).Find("MsgTxt").GetComponent<Text>();
        _ensureBtn = _msgPage.transform.GetChild(0).Find("EnsureBtn").GetComponent<Button>();
        _ensureBtn.onClick.AddListener(OnClickEnsureBtn);
        _msgPage.SetActive(false);
    }

    //每次进入我的项目时，进行加载（加载文字、图片等）
    //加载我的项目图片文字等(在RestController中调用)
    public void LoadMyProject(int index)
    {
        _projectNameTxt.text = RestController.Instance.ProjectDetailList[index].projectName;
        _projectImg.sprite = RestController.Instance.ProjectList[index].SpriteTex;
        double target = RestController.Instance.ProjectDetailList[index].targetAmount;
        _targetAmountTxt.text = "￥" + target.ToString();
        _minAmountTxt.text = "￥" + RestController.Instance.ProjectDetailList[index].minAmount.ToString();
        _maxAmountTxt.text = "￥" + RestController.Instance.ProjectDetailList[index].maxAmount.ToString();
        _startTimeTxt.text = RestController.Instance.ProjectDetailList[index].startTime.ToString();
        _stopTimeTxt.text = RestController.Instance.ProjectDetailList[index].endTime.ToString();
        _supportNumTxt.text = RestController.Instance.ProjectDetailList[index].supportNum.ToString();
        double already = RestController.Instance.ProjectDetailList[index].alreadyRaised;
        _alreadyAccountTxt.text = "￥" + already.ToString();
        double waitAmount = target - already;
        _waitAccountTxt.text = "￥" + waitAmount.ToString();

        //给UIMyProjectMsg中的变量赋值，用于加载支持页面
        UIMyProjectMsg.Instance.ProjectId = RestController.Instance.ProjectDetailList[index].id;
        UIMyProjectMsg.Instance.ProjectName = RestController.Instance.ProjectDetailList[index].projectName;
        UIMyProjectMsg.Instance.TargetAmount = RestController.Instance.ProjectDetailList[index].targetAmount.ToString();
        UIMyProjectMsg.Instance.RemainAmount = (RestController.Instance.ProjectDetailList[index].targetAmount - RestController.Instance.ProjectDetailList[index].alreadyRaised).ToString();
        UIMyProjectMsg.Instance.MinAmount = RestController.Instance.ProjectDetailList[index].minAmount.ToString();


        _licenseTimeTxt.text = RestController.Instance.ProjectDetailList[index].licenseTime;
        print(RestController.Instance.ProjectDetailList[index].kilometers);
        if (!string.IsNullOrEmpty(RestController.Instance.ProjectDetailList[index].kilometers))
        {
            _kilometersTxt.text = RestController.Instance.ProjectDetailList[index].kilometers + "KM";
        }
        else
        {
            _kilometersTxt.text = "";
        }
        _cityTxt.text = RestController.Instance.ProjectDetailList[index].city;
    }

    //支持按钮点击事件
    private void OnClickSupportBtn()
    {
        //加载支持页面，并让支持页面显示
        LoadPayPage();
        _payPage.SetActive(true);
    }

    //支持页面上的提交按钮点击事件
    public void OnClickSubmitBtn()
    {
        //TODO:提交相关信息给后台
        RestDataCommitRaise data = new RestDataCommitRaise();
        data.userId = Player.Instance.ID;
        data.projectId = UIMyProjectMsg.Instance.ProjectId;
        data.count = Int32.Parse(_count);
        data.payPwd = _pwd;
        //TODO:红包信息

        //弹出信息提示框
        //TODO:(委托)
        _msgPage.SetActive(true);

    }

    //支持页面上的关闭按钮点击事件
    public void OnClickCloseBtn()
    {
        _payPage.SetActive(false);
    }

    //确定按钮点击事件
    public void OnClickEnsureBtn()
    {
        //关闭提示信息页面
        _msgPage.SetActive(false);

        //关闭支持页面
        _payPage.SetActive(false);
    }

    private void OnValueChangeCountInput(string str)
    {
        _count = str;
    }

    private void OnValueChangePwdInput(string str)
    {
        _pwd = str;
    }

    private void OnDisable()
    {
        //关闭提示信息页面
        _msgPage.SetActive(false);

        //关闭支持页面
        _payPage.SetActive(false);
    }

    //加载支持页面
    private void LoadPayPage()
    {
        _titleTxt.text = UIMyProjectMsg.Instance.ProjectName;
        _totalTxt.text = UIMyProjectMsg.Instance.TargetAmount;
        _remainTxt.text = UIMyProjectMsg.Instance.RemainAmount;
        _unitPriceTxt.text = UIMyProjectMsg.Instance.MinAmount;
        //加载红包下拉框
        List<string> testList = new List<string>();
        testList.Add("1");
        testList.Add("2");
        testList.Add("3");
        testList.Add("4");
        testList.Add("5");
        testList.Add("6");
        _packageDropDown.options.Clear();
        foreach(string s in testList)
        {
            Dropdown.OptionData data = new Dropdown.OptionData();
            data.text = s;
            _packageDropDown.options.Add(data);
        }
        _packageDropDown.captionText.text = "无可用红包";
    }

    //提示页面显示消息
    public void ShowMsg(string msg)
    {

    }
}