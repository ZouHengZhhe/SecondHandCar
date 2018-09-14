using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Dictionary<string, GameObject> ParentPagesDic = new Dictionary<string, GameObject>();  //父页面字典
    public Dictionary<string, GameObject> ChildPagesDic = new Dictionary<string, GameObject>();    //子页面字典
    public GameObject HomePage;     //首页
    public GameObject ProjectsPage;    //众筹项目
    public GameObject MyPage;          //我的

    //首页按钮中的元素
    private Transform _homeBtnTrans;
    private GameObject _homeImage1;  //1是未选中状态
    private GameObject _homeImage2;  //2是选中状态
    private Text _homeText;
    //众筹项目按钮中的元素
    private Transform _projectsBtnTrans;
    private GameObject _projectsImage1;
    private GameObject _projectsImage2;
    private Text _projectsText;
    //我的按钮中的元素
    private Transform _myBtnTrans;
    private GameObject _myImage1;
    private GameObject _myImage2;
    private Text _myText;


    private void Awake()
    {
        Instance = this;
    }
    
    // Use this for initializationf
    private void Start()
    {

        //将父页面添加进字典
        ParentPagesDic.Add("HomePage", GameObject.Find("HomePage"));
        ParentPagesDic.Add("ProjectsPage", GameObject.Find("ProjectsPage"));
        ParentPagesDic.Add("MyPage", GameObject.Find("MyPage"));

        //将子页面添加进字典
        //首页
        ChildPagesDic.Add("AboutUsPage", GameObject.Find("AboutUsPage"));
        ChildPagesDic.Add("HelpCenterPage", GameObject.Find("HelpCenterPage"));
        //我的
        ChildPagesDic.Add("WithDrawPage", GameObject.Find("WithDrawPage"));
        ChildPagesDic.Add("RechargePage", GameObject.Find("RechargePage"));
        ChildPagesDic.Add("MyPacketPage", GameObject.Find("MyPacketPage"));
        ChildPagesDic.Add("MyCrowdfunding", GameObject.Find("MyCrowdfunding"));
        ChildPagesDic.Add("BuyHistoryPage", GameObject.Find("BuyHistoryPage"));
        ChildPagesDic.Add("AccountSettingPage", GameObject.Find("AccountSettingPage"));
        ChildPagesDic.Add("RegisterPage", GameObject.Find("RegisterPage"));
        //我的项目页面
        ChildPagesDic.Add("MyProjectPage", GameObject.Find("MyProjectPage"));  //具体的项目页面

        ControlParentPages("HomePage");  //初始时显示首页
        HideAllChildPages();  //初始时隐藏所有子页面
        
        InitBtn();
    }
    
    private void InitBtn()
    {
        //初始时，在首页界面中
        _homeBtnTrans = this.transform.Find("DownBackground").Find("HomeBtn");
        _homeImage1 = _homeBtnTrans.Find("Image1").gameObject;
        _homeImage2 = _homeBtnTrans.Find("Image2").gameObject;
        _homeText = _homeBtnTrans.Find("Text").GetComponent<Text>();
        _homeImage1.SetActive(false);  //选中状态
        _homeText.color=new Color(0, 255, 255, 255);

        _projectsBtnTrans = this.transform.Find("DownBackground").Find("ProjectsBtn");
        _projectsImage1 = _projectsBtnTrans.Find("Image1").gameObject;
        _projectsImage2 = _projectsBtnTrans.Find("Image2").gameObject;
        _projectsText = _projectsBtnTrans.Find("Text").GetComponent<Text>();
        _projectsImage2.SetActive(false);  //未选中状态
        _projectsText.color = new Color(161, 161, 161, 255);

        _myBtnTrans = this.transform.Find("DownBackground").Find("MyBtn");
        _myImage1 = _myBtnTrans.Find("Image1").gameObject;
        _myImage2 = _myBtnTrans.Find("Image2").gameObject;
        _myText= _myBtnTrans.Find("Text").GetComponent<Text>();
        _myImage2.SetActive(false);  //未选中状态
        _myText.color = new Color(161, 161, 161, 255);
    }

    #region 按钮点击事件，控制三大父页面的显示

    //“首页”按钮点击事件
    public void OnClickHomeBtn()
    {
        HideAllChildPages();  //隐藏所有子页面
        ControlParentPages("HomePage");
        ControlThreeBtnColor("HomeBtn");
    }

    //“众筹项目”按钮点击事件
    public void OnClickProjectBtn()
    {
        HideAllChildPages();  //隐藏所有子页面
        ControlParentPages("ProjectsPage");
        ControlThreeBtnColor("ProjectsBtn");
    }

    //“我的”按钮点击事件
    public void OnClickMyBtn()
    {
        HideAllChildPages();  //隐藏所有子页面
        ControlParentPages("MyPage");
        ControlThreeBtnColor("MyBtn");
    }
    
    #endregion 按钮点击事件

    //控制父页面显示，除了传入名字的页面显示，其他页面全部隐藏
    public void ControlParentPages(string pageName)
    {
        HideAllChildPages();  //隐藏所有子页面
        foreach (KeyValuePair<string, GameObject> k in ParentPagesDic)
        {
            k.Value.SetActive(false);
        }
        ParentPagesDic[pageName].SetActive(true);
    }

    //控制子页面显示
    public void ControlChildPages(string pageName)
    {
        HideAllParentPages();  //隐藏所有父页面
        foreach (KeyValuePair<string, GameObject> k in ChildPagesDic)
        {
            k.Value.SetActive(false);
        }
        ChildPagesDic[pageName].SetActive(true);
    }

    //隐藏所有父页面
    private void HideAllParentPages()
    {
        foreach (KeyValuePair<string, GameObject> k in ParentPagesDic)
        {
            k.Value.SetActive(false);
        }
    }

    //隐藏所有子页面
    private void HideAllChildPages()
    {
        foreach (KeyValuePair<string, GameObject> k in ChildPagesDic)
        {
            k.Value.SetActive(false);
        }
    }

    //首页、众筹项目、我的，三个按钮的选中状态颜色变换
    private void ControlThreeBtnColor(string btnName)
    {
        _homeImage1.SetActive(false);
        _homeImage2.SetActive(true);
        _homeText.color = new Color(0, 255, 255, 255);
        _homeText.color = new Color(161, 161, 161, 255);

        switch (btnName)
        {
            case "HomeBtn":
                _homeImage1.SetActive(false);
                _homeImage2.SetActive(true);
                _homeText.color = new Color(0, 255, 255, 255);  //选中

                _projectsImage1.SetActive(true);
                _projectsImage2.SetActive(false);
                _projectsText.color = new Color(161, 161, 161, 255);

                _myImage1.SetActive(true);
                _myImage2.SetActive(false);
                _myText.color = new Color(161, 161, 161, 255);
                break;

            case "ProjectsBtn":
                _homeImage1.SetActive(true);
                _homeImage2.SetActive(false);
                _homeText.color = new Color(161, 161, 161, 255);

                _projectsImage1.SetActive(false);
                _projectsImage2.SetActive(true);
                _projectsText.color = new Color(0, 255, 255, 255);  //选中

                _myImage1.SetActive(true);
                _myImage2.SetActive(false);
                _myText.color = new Color(161, 161, 161, 255);
                break;

            case "MyBtn":
                _homeImage1.SetActive(true);
                _homeImage2.SetActive(false);
                _homeText.color = new Color(161, 161, 161, 255);

                _projectsImage1.SetActive(true);
                _projectsImage2.SetActive(false);
                _projectsText.color = new Color(161, 161, 161, 255);

                _myImage1.SetActive(false);
                _myImage2.SetActive(true);
                _myText.color = new Color(0, 255, 255, 255);  //选中
                break;
        }
    }
    
}