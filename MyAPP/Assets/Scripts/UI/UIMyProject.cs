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
        
    }

    //每次进入我的项目时，进行加载（加载文字、图片等）
    //加载我的项目图片文字等(在RestController中调用)
    public void LoadMyProject(int index)
    {
        _projectNameTxt.text = RestController.Instance.ProjectDetailList[index].projectName;
        _projectImg.sprite = RestController.Instance.ProjectList[index].SpriteTex;
        double target = RestController.Instance.ProjectDetailList[index].targetAmount;
        _targetAmountTxt.text= "￥" + target.ToString();
        _minAmountTxt.text = "￥" + RestController.Instance.ProjectDetailList[index].minAmount.ToString();
        _maxAmountTxt.text = "￥" + RestController.Instance.ProjectDetailList[index].maxAmount.ToString();
        _startTimeTxt.text = RestController.Instance.ProjectDetailList[index].startTime.ToString();
        _stopTimeTxt.text = RestController.Instance.ProjectDetailList[index].endTime.ToString();
        _supportNumTxt.text = RestController.Instance.ProjectDetailList[index].supportNum.ToString();
        double already = RestController.Instance.ProjectDetailList[index].alreadyRaised;
        _alreadyAccountTxt.text = "￥" + already.ToString();
        double waitAmount = target - already;
        _waitAccountTxt.text = "￥" + waitAmount.ToString();

        _licenseTimeTxt.text= RestController.Instance.ProjectDetailList[index].licenseTime;
        _kilometersTxt.text = RestController.Instance.ProjectDetailList[index].kilometers+"KM";
        _cityTxt.text = RestController.Instance.ProjectDetailList[index].city;
    }
}