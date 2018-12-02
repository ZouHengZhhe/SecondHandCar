using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestController : MonoBehaviour
{
    public static RestController Instance;

    private UIRegister _uiRegister;
    private UIHome _uiHome;
    private UIManager _uiManager;
    private UIMyPage _uiMyPage;
    private UIStartPanel _uiStartPanel;
    private UIProjects _uiProjects;
    private UIMyProject _uiMyProject;
    private UIMoneyDetail _uiMoneyDetail;
    private UIWithDraw _uiWithDraw;

    //所有项目图片和状态
    //传入TestEmailUI中，用于更新项目图片
    public List<Project> ProjectList = new List<Project>();  //项目列表
    public List<RestDataProjectDetail> ProjectDetailList = new List<RestDataProjectDetail>();  //项目详细列表
    public List<Project> MyProjectsInList = new List<Project>();  //我的在投中的众筹项目
    public List<Project> MyProjectsEndList = new List<Project>();  // //我的已结束的众筹项目
    public List<RestDataRedPacketDetail> RedPacketList = new List<RestDataRedPacketDetail>();  //我的红包列表

    private Restful _restful;
    //无限加载
    private TestEmailUI _loadUI;

    private int _loadTextureNum = 0;
    private int _projectsNum = 0;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        //得到Restful组件
        _restful = this.transform.GetComponent<Restful>();

        //得到UIRegister组件，并监听
        _uiRegister = GameObject.Find("AppPage").transform.Find("RegisterPage").GetComponent<UIRegister>();
        _uiRegister.RegisterCallback += OnRegister;

        _uiHome = GameObject.Find("AppPage").transform.Find("HomePage").GetComponent<UIHome>();
        _uiMyPage = GameObject.Find("AppPage").transform.Find("MyPage").GetComponent<UIMyPage>();
        _uiStartPanel = GameObject.Find("AppPage").transform.Find("StartPanel").GetComponent<UIStartPanel>();
        _uiProjects= GameObject.Find("AppPage").transform.Find("ProjectsPage/Panel_Scroll/Panel_Grid").GetComponent<UIProjects>();
        _uiMyProject= GameObject.Find("AppPage").transform.Find("MyProjectPage").GetComponent<UIMyProject>();

        _uiMyPage.UpdateMyProjectsCallback += OnUpdateMyProjects;  //监听“我的众筹”
        _uiMyPage.UpdateMyRedPacketCallback += OnUpdateMyRedPacket;  //监听“我的红包”
        _uiMyPage.UpdateMoneyDetailCallback += OnUpdateMoneyDetail;  //监听“资金明细”

        _uiProjects.UpdateProjectPageCallback += LoadProjectPage;  //点击项目列表，加载我的项目
        _uiHome.UpdateProjectPageCallback += LoadProjectPageInHome;  //点击轮播图，加载我的项目

        _uiMoneyDetail = GameObject.Find("AppPage").transform.Find("MoneyDetailPage").GetComponent<UIMoneyDetail>();

        //提现
        _uiWithDraw = GameObject.Find("AppPage").transform.Find("WithDrawPage").GetComponent<UIWithDraw>();
        _uiWithDraw.WithDrawCallback += WithDraw;

        //更新“Home”页面中的会员总数、众筹项目，总资产
        _uiManager = GameObject.Find("AppPage").GetComponent<UIManager>();
        _uiManager.UpdateHomePageCallback += OnUpdateHomePage;
        _uiManager.UpdateProjectsPageCallback += OnUpdateProjectsPage;
        _uiManager.UpdateMyPageCallback += OnUpdateMyPage;

        _loadUI = GameObject.Find("AppPage").GetComponent<TestEmailUI>();
        _uiHome.UpdateProjectsListCallback += OnUpdateProjectsPage;

        //得到会员总数、众筹项目，总资产
        RestDataStatistics statistics = _restful.GetStatistics();
        int memCount = statistics.data.memberCount;
        double totalAssets = statistics.data.raiseCount;
        int projectsCount = statistics.data.projectCount;
        LoadControl(memCount, projectsCount, totalAssets);

        //加载项目列表
        AnalysisProjectsListRest();
        
    }

    private void Update()
    {
        //图片下载完成
        if (_loadTextureNum == _projectsNum)
        {
            //加载轮播图
            LoadScrollItems();
            //加载项目列表个数
            LoadProjectsNum();

            //得到项目详细，并存入列表
            for(int i=0;i<ProjectList.Count;i++)
            {
                RestDataProjectInfo proInfo = _restful.GetProjectDetail(ProjectList[i].Id);
                ProjectDetailList.Add(proInfo.detail);
            }

            //图片加载完成，显示APP页面
            _uiManager.ShowApp();

            _loadTextureNum = 0;
        }
    }

    //解析项目列表接口，下载图片
    private void AnalysisProjectsListRest()
    {
        RestDataProjectList restDataProList = _restful.GetProjectsList();
        _projectsNum = restDataProList.total;
        //_loadUI.InitProjects(_projectsNum);
        //提前确定ProjectList的容量，方便后面按顺序将图片存入列表
        for (int i = 0; i < restDataProList.rows.Length; i++)
        {
            ProjectList.Add(new Project());
        }


        for (int i = 0; i < restDataProList.rows.Length; i++)
        {
            int value = int.Parse(restDataProList.rows[i].id);
            string str = "jrdcar.com" + restDataProList.rows[i].mainUrl;

            ProjectList[i].Url = str;  //图片URL赋值
            ProjectList[i].State = restDataProList.rows[i].state;  //状态赋值
            ProjectList[i].Id = int.Parse(restDataProList.rows[i].id);  //Id赋值
            ProjectList[i].ProjectName = restDataProList.rows[i].projectName;
            ProjectList[i].TargetAmount = restDataProList.rows[i].targetAmount;
            ProjectList[i].AlreadyAmount = restDataProList.rows[i].alreadyAmount;
            ProjectList[i].SupportNum = restDataProList.rows[i].supportNum;
            ProjectList[i].City = restDataProList.rows[i].city;

            StartCoroutine(DownloadTexture(value, str, i));

            //手机端
            if (i < 11)
            {
                ProjectList[i].SpriteTex = Resources.Load<Sprite>((i + 1).ToString());
            }
            else
            {
                int num = Random.Range(1, 12);
                ProjectList[i].SpriteTex = Resources.Load<Sprite>((i).ToString());
            }
            //print(str);
        }
    }

    //加载轮播图
    private void LoadScrollItems()
    {
        Sprite[] spritesArray = new Sprite[4];
        int[] statesArray = new int[4];
        //得到最新的四个项目
        for (int i = ProjectList.Count - 4; i < ProjectList.Count; i++)
        {
            spritesArray[i - ProjectList.Count + 4] = ProjectList[i].SpriteTex;
            statesArray[i - ProjectList.Count + 4] = ProjectList[i].State;
        }
        _uiHome.LoadScrollItems(spritesArray, statesArray);
    }

    //加载众筹项目个数
    private void LoadProjectsNum()
    {
        _loadUI.InitProjects(_projectsNum);
    }


    //项目加载时，更新“Home”页面中的会员总数、众筹项目，总资产
    private void LoadControl(int memCount, int projectsCount, double totalAssets)
    {
        _uiHome.ControlThreeTextInfo(memCount, projectsCount, totalAssets);
    }

    //点击“Home”按钮，进入“Home”页面，更新会员总数、众筹项目，总资产
    private void OnUpdateHomePage()
    {
        RestDataStatistics statistics = _restful.GetStatistics();
        int memCount = statistics.data.memberCount;
        double totalAssets = statistics.data.raiseCount;
        int projectsCount = statistics.data.projectCount;
        _uiHome.ControlThreeTextInfo(memCount, projectsCount, totalAssets);
    }

    //点击“众筹项目”按钮，进入“众筹项目”页面，更新众筹项目
    private void OnUpdateProjectsPage()
    {
        LoadProjectsNum();
    }

    //点击“我的”按钮，进入”我的“页面，更新页面
    private void OnUpdateMyPage()
    {
        //检测是否登陆成功
        _uiMyPage.OnRegisterSucc(Player.Instance.Username);
    }

    //点击“我的”页面中的“我的众筹”按钮，进入“我的众筹”页面，加载我的众筹项目
    private void OnUpdateMyProjects()
    {
        LoadMyProjectsNum();  //加载我的众筹个数
    }

    //加载我的众筹个数
    private void LoadMyProjectsNum()
    {
        //加载我的众筹（接口八：众筹记录）
        if (Player.Instance.IsRegister)  //已经登陆
        {
            RestDataJoinProject rdjp = _restful.GetJoinProjects(Player.Instance.ID);
            int count = rdjp.total;
            //给我的项目列表赋值
            if (rdjp.rows != null)
            {
                for (int i = 0; i < rdjp.rows.Length; i++)
                {
                    foreach (Project p in ProjectList)
                    {
                        //从项目列表中找到对应的项目
                        if (p.Id == rdjp.rows[i].id)
                        {
                            //将所有我的项目分类存入列表
                            if (p.State <= 1)//在投中的项目
                            {
                                MyProjectsInList.Add(p);
                            }
                            else
                            {
                                MyProjectsEndList.Add(p);
                            }
                            break;
                        }
                    }

                }
            }
            
            
            //加载在投中的项目个数
            _loadUI.InitMyProjectsIn(MyProjectsInList.Count);
        }
        else  //未登陆
        {
            _loadUI.InitMyProjectsIn(0);
        }

    }

    private void OnUpdateMyRedPacket()
    {
        LoadMyRedPacket();
    }

    //加载我的红包
    private void LoadMyRedPacket()
    {
        int count = 0;
        if (Player.Instance.IsRegister)  //已经登陆
        {
            RestDataRedPacketInfo rpInfo = _restful.GetRedPacket(Player.Instance.ID);
            count = rpInfo.total;
            for(int i=0;i<count;i++)
            {
                if (rpInfo.rows[i].state == "未使用")  //把未使用的红包放进列表
                {
                    RedPacketList.Add(rpInfo.rows[i]);
                }
                
            }

            //加载红包个数
            _loadUI.InitMyRedPacket(count);
        }
        else
        {
            _loadUI.InitMyRedPacket(count);
        }
    }

    //加载资金明细
    private void OnUpdateMoneyDetail()
    {
        if (Player.Instance.IsRegister)
        {
            _uiMoneyDetail.UpdateBalance(Player.Instance.Balance);
        }
        else
        {
            _uiMoneyDetail.UpdateBalance(0);
        }
    }

    //加载项目页面(MyProjectPage)
    private void LoadProjectPage(int index)
    {
        _uiMyProject.LoadMyProject(index);
    }

    private void LoadProjectPageInHome(int index)
    {
        switch (index)
        {
            case 1:
                _uiMyProject.LoadMyProject(ProjectList.Count - 4);
                break;
            case 2:
                _uiMyProject.LoadMyProject(ProjectList.Count - 3);
                break;
            case 3:
                _uiMyProject.LoadMyProject(ProjectList.Count - 2);
                break;
            case 4:
                _uiMyProject.LoadMyProject(ProjectList.Count-1);
                break;
        }
    }

    //提现,并弹出信息提示框进行提示
    private void WithDraw(RestDataWithDraw data)
    {
        string result = _restful.PostWithDraw(data);
        _uiWithDraw.ShowMsg(result);
    }

    //登陆验证
    private void OnRegister(string username, string password)
    {
        RestDataRegister data = _restful.GetRegister(username, password);   //登陆验证，得到返回参数
        print(data.id);
        RegisterControl(data, username, password);
    }

    //确认登陆状态并进行相应控制
    private void RegisterControl(RestDataRegister data, string username, string password)
    {
        if (data.code == 0)  //账号存在，登陆成功
        {
            Player.Instance.IsRegister = true;
            Player.Instance.Username = username;
            Player.Instance.Password = password;
            Player.Instance.ID = data.id;

            //TODO:得到Player的相关信息
            //得到用户余额
            double balance = _restful.GetBalance(data.id);
            Player.Instance.Balance = balance;
            //得到众筹项目(众筹记录)
            RestDataJoinProject rdjp = _restful.GetJoinProjects(data.id);
            int num = rdjp.total;
            Player.Instance.JoinProjectsNum = num;
        }
        else  //账号不存在登陆失败
        {
            Player.Instance.IsRegister = false;
        }

        if (Player.Instance.IsRegister)  //登陆成功,显示登陆成功的弹出框
        {
            _uiRegister.ControlIsRegisterSuccess(true);
        }
        else
        {
            _uiRegister.ControlIsRegisterSuccess(false);
        }
    }

    IEnumerator DownloadTexture(int id, string url, int index)
    {
        WWW www = new WWW(url);
        yield return www;
        //PC端
        //Texture2D tex2d = www.texture;
        //Sprite m_sprite = Sprite.Create(tex2d, new Rect(0, 0, tex2d.width, tex2d.height), new Vector2(0, 0));
        //ProjectList[index].SpriteTex = m_sprite;
        _loadTextureNum++;
        string value = (_loadTextureNum * 100 / _projectsNum).ToString();
        _uiStartPanel.ChangeLoadValue(value);  //加载进度显示
    }
}
