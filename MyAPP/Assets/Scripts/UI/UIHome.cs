using UnityEngine;
using UnityEngine.UI;

public class UIHome : MonoBehaviour
{
    private Button _goodProjectBtn;  //优质项目推荐按钮
    private Button _aboutUsBtn;  //关于我们按钮
    private Button _helpCenterBtn;  //帮助中心按钮

    private Button _item1;
    private Button _item2;
    private Button _item3;
    private Button _item4;

    //四个轮播图
    private Image[] _itemImgArray=new Image[4];

    private Text _memCountTxt;
    private Text _projectsCountTxt;
    private Text _totalAssetsTxt;

    //加载我的项目委托
    public delegate void UpdateProjectPageDel(int index);
    public UpdateProjectPageDel UpdateProjectPageCallback = null;  //在RestController中被监听
    public delegate void UpdateProjectListDel();
    public UpdateProjectListDel UpdateProjectsListCallback = null;

    private void Start()
    {
        InitUI();
    }

    private void InitUI()
    {
        //获取按钮
        _goodProjectBtn = this.transform.Find("GoodProjectBtn").GetComponent<Button>();
        _aboutUsBtn = this.transform.Find("AboutUsBtn").GetComponent<Button>();
        _helpCenterBtn = this.transform.Find("HelpCenterBtn").GetComponent<Button>();
        //项目按钮
        _item1 = this.transform.Find("Panel").Find("ScrollView").Find("Content").GetChild(0).GetComponent<Button>();
        _item2 = this.transform.Find("Panel").Find("ScrollView").Find("Content").GetChild(1).GetComponent<Button>();
        _item3 = this.transform.Find("Panel").Find("ScrollView").Find("Content").GetChild(2).GetComponent<Button>();
        _item4 = this.transform.Find("Panel").Find("ScrollView").Find("Content").GetChild(3).GetComponent<Button>();

        //按钮添加监听函数
        _goodProjectBtn.onClick.AddListener(OnClickGoodProjectBtn);
        _aboutUsBtn.onClick.AddListener(OnClickAboutUsBtn);
        _helpCenterBtn.onClick.AddListener(OnClickHelpCenterBtn);
        _item1.onClick.AddListener(OnClickItem1Btn);
        _item2.onClick.AddListener(OnClickItem2Btn);
        _item3.onClick.AddListener(OnClickItem3Btn);
        _item4.onClick.AddListener(OnClickItem4Btn);

        //得到显示 会员数量，众筹项目，资产总额的Text
        _memCountTxt = this.transform.Find("MemCount").Find("ValueTxt").GetComponent<Text>();
        _projectsCountTxt = this.transform.Find("ProjectsCount").Find("ValueTxt").GetComponent<Text>();
        _totalAssetsTxt = this.transform.Find("TotalAssets").Find("ValueTxt").GetComponent<Text>();
        //print(_memCountTxt == null);
        //得到四个轮播图
        for(int i=0;i<4;i++)
        {
            _itemImgArray[i]= this.transform.Find("Panel").Find("ScrollView").Find("Content").Find("Image"+i).GetComponent<Image>();
        }
    }

    public void ControlThreeTextInfo(int memCount,int projectsCount,double totalAssets)
    {
        //print(_memCountTxt == null);
        //print(memCount);
        _memCountTxt.text = memCount.ToString();
        _projectsCountTxt.text = projectsCount.ToString();
        _totalAssetsTxt.text = totalAssets.ToString();
    }

    //加载4个轮播图
    public void LoadScrollItems(Sprite[] spritesArray,int[] statesArray)
    {
        for(int i=0;i<4;i++)
        {
            _itemImgArray[i].sprite = spritesArray[i];
            switch(statesArray[i])
            {
                case -1:
                    _itemImgArray[i].transform.Find("StateImg").Find("StateTxt").GetComponent<Text>().text = "取消";
                    break;
                case 1:
                    _itemImgArray[i].transform.Find("StateImg").Find("StateTxt").GetComponent<Text>().text = "预热";
                    break;
                case 2:
                    _itemImgArray[i].transform.Find("StateImg").Find("StateTxt").GetComponent<Text>().text = "众筹";
                    break;
                case 3:
                    _itemImgArray[i].transform.Find("StateImg").Find("StateTxt").GetComponent<Text>().text = "筹满";
                    break;
                case 4:
                    _itemImgArray[i].transform.Find("StateImg").Find("StateTxt").GetComponent<Text>().text = "投票";
                    break;
                case 5:
                    _itemImgArray[i].transform.Find("StateImg").Find("StateTxt").GetComponent<Text>().text = "出售";
                    break;
                case 6:
                    _itemImgArray[i].transform.Find("StateImg").Find("StateTxt").GetComponent<Text>().text = "分红";
                    break;
            }
        }
    }

    #region 按钮点击事件

    //优质项目推荐，点击进入项目列表
    private void OnClickGoodProjectBtn()
    {
        UIManager.Instance.ControlParentPages("ProjectsPage");

        //进入项目列表
        UpdateProjectsListCallback();
    }

    private void OnClickAboutUsBtn()
    {
        UIManager.Instance.ControlChildPages("AboutUsPage");
    }

    private void OnClickHelpCenterBtn()
    {
        UIManager.Instance.ControlChildPages("HelpCenterPage");
    }

    private void OnClickItem1Btn()
    {
        UIManager.Instance.ControlChildPages("MyProjectPage");

        //加载我的项目
        UpdateProjectPageCallback(1);
    }

    private void OnClickItem2Btn()
    {
        UIManager.Instance.ControlChildPages("MyProjectPage");

        //加载我的项目
        UpdateProjectPageCallback(2);
    }

    private void OnClickItem3Btn()
    {
        UIManager.Instance.ControlChildPages("MyProjectPage");

        //加载我的项目
        UpdateProjectPageCallback(3);
    }

    private void OnClickItem4Btn()
    {
        UIManager.Instance.ControlChildPages("MyProjectPage");

        //加载我的项目
        UpdateProjectPageCallback(4);
    }

    private void OnDestroy()
    {
        UpdateProjectPageCallback = null;
        UpdateProjectsListCallback = null;
    }
    

    #endregion


}