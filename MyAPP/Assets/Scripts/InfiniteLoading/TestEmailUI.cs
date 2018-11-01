using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class TestEmailUI : MonoBehaviour
{

    private InfinityGridLayoutGroup infinityGridLayoutGroup;    //我的项目
    private InfinityGridLayoutGroup _myCrowdLayoutGroup;     //我的众筹
    private InfinityGridLayoutGroup _myPackageLayoutGroup;  //我的红包
    private UIProjects _uiProjects;

    int _projectsAmount = 100;     //所有项目，项目列表
    private int _myCrowdAmount = 30;  //我的众筹个数（包括在投中和已结束）
    private int _myPackageAmount = 20;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        ////初始化数据列表;
        /// “我的项目”页面
        Transform ProjectsPanel = this.transform.Find("ProjectsPage");
        infinityGridLayoutGroup = ProjectsPanel.Find("Panel_Scroll/Panel_Grid").GetComponent<InfinityGridLayoutGroup>();
        //infinityGridLayoutGroup.SetAmount(_projectsAmount);
        //infinityGridLayoutGroup.updateChildrenCallback = UpdateChildrenCallback;

        //“我的众筹”页面（包含在筹中和已结束）
        Transform MyCrowdFundingPanel = this.transform.Find("MyCrowdfunding");
        _myCrowdLayoutGroup = MyCrowdFundingPanel.Find("Panel_Scroll/Panel_Grid").GetComponent<InfinityGridLayoutGroup>();
        //_myCrowdLayoutGroup.SetAmount(_myCrowdAmount);
        //_myCrowdLayoutGroup.updateChildrenCallback = UpdateMyCrowdCallback;

        //“我的红包”页面
        Transform MyPackagePanel = this.transform.Find("MyPacketPage");
        _myPackageLayoutGroup = MyPackagePanel.Find("Panel_Scroll/Panel_Grid").GetComponent<InfinityGridLayoutGroup>();
        _myPackageLayoutGroup.SetAmount(_myPackageAmount);

        _uiProjects = this.transform.Find("ProjectsPage/Panel_Scroll/Panel_Grid").GetComponent<UIProjects>();
    }

    //我的项目
    void UpdateChildrenCallback(int index, Transform trans)
    {
        //加载图片和状态
        //print(index);
        _uiProjects.OnChangeProjectImage(index, trans, RestController.Instance.ProjectList);
        //页面换位置后，更新页面
        trans.GetComponent<SignIndex>().Index = index;
        //项目名字
        Text text = trans.Find("Text").GetComponent<Text>();
        text.text = RestController.Instance.ProjectList[index].ProjectName;
        //目标金额
        Text targetTxt = trans.Find("TargetTxt").GetComponent<Text>();
        targetTxt.text = "目标：￥" + RestController.Instance.ProjectList[index].TargetAmount;
        //已众筹金额
        Text alreadyAmountTxt = trans.Find("AlreadyAmountTxt").GetComponent<Text>();
        alreadyAmountTxt.text = "￥" + RestController.Instance.ProjectList[index].AlreadyAmount + "\n" + "已众筹";
        //已众筹百分比
        Text alreadyTxt = trans.Find("AlreadyTxt").GetComponent<Text>();
        double percent = RestController.Instance.ProjectList[index].AlreadyAmount / RestController.Instance.ProjectList[index].TargetAmount * 100;
        string percentF = percent.ToString("f2") + "%";
        alreadyTxt.text = percentF + "\n" + "已众筹";
    }

    //我的在投中众筹
    void UpdateMyCrowdInCallback(int index, Transform trans)
    {
        //页面换位置后，更新页面
        Text text = trans.Find("Text").GetComponent<Text>();
        if (RestController.Instance.MyProjectsInList.Count!=0)
        {
            text.text = RestController.Instance.MyProjectsInList[index].ProjectName;  //给在投中项目赋值
        }

    }
    //我的已结束众筹
    void UpdateMyCrowdEndCallback(int index, Transform trans)
    {
        //页面换位置后，更新页面
        Text text = trans.Find("Text").GetComponent<Text>();
        if (RestController.Instance.MyProjectsEndList.Count != 0)
        {
            text.text = RestController.Instance.MyProjectsEndList[index].ProjectName;  //给在投中项目赋值
        }

    }

    //我的红包
    void UpdateMyRedPacketCallback(int index,Transform trans)
    {
        Text moneyTxt = trans.Find("Money").GetComponent<Text>();
        Text termDateTxt = trans.Find("TermDateTxt").GetComponent<Text>();
        Text stateTxt = trans.Find("StateTxt").GetComponent<Text>();
        if (RestController.Instance.RedPacketList.Count != 0)
        {
            string money = RestController.Instance.RedPacketList[index].couponSize;
            string[] strArray = money.Split('元');
            moneyTxt.text = strArray[0];
            termDateTxt.text = RestController.Instance.RedPacketList[index].termDate;
            string t= RestController.Instance.RedPacketList[index].condition;
            string[] array = t.Split('额');
            stateTxt.text = array[1] + "可用";
        }
    }


    //初始化项目个数
    public void InitProjects(int projectsCount)
    {
        _projectsAmount = projectsCount;
        infinityGridLayoutGroup.SetAmount(_projectsAmount);
        infinityGridLayoutGroup.updateChildrenCallback = UpdateChildrenCallback;
    }

    //初始化我的在投中众筹个数(在RestController和UICrowdfunding中调用)
    public void InitMyProjectsIn(int myProjectsInCount)
    {
        _myCrowdAmount = myProjectsInCount;
        _myCrowdLayoutGroup.SetAmount(_myCrowdAmount);
        _myCrowdLayoutGroup.updateChildrenCallback = UpdateMyCrowdInCallback;
    }
    //初始化我的已结束众筹个数（在UICrowdfunding中调用）
    public void InitMyProjectsEnd(int myProjectsEndCount)
    {
        _myCrowdAmount = myProjectsEndCount;
        _myCrowdLayoutGroup.SetAmount(myProjectsEndCount);
        _myCrowdLayoutGroup.updateChildrenCallback = UpdateMyCrowdEndCallback;
    }
    //初始化我的红包个数
    public void InitMyRedPacket(int myRedPacketCount)
    {
        _myPackageAmount = myRedPacketCount;
        _myPackageLayoutGroup.SetAmount(_myPackageAmount);
        _myPackageLayoutGroup.updateChildrenCallback = UpdateMyRedPacketCallback;
    }
}

