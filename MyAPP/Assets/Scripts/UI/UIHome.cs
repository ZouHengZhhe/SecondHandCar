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
    }

    #region 按钮点击事件

    private void OnClickGoodProjectBtn()
    {
        UIManager.Instance.ControlParentPages("ProjectsPage");
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
    }

    private void OnClickItem2Btn()
    {
        UIManager.Instance.ControlChildPages("MyProjectPage");
    }

    private void OnClickItem3Btn()
    {
        UIManager.Instance.ControlChildPages("MyProjectPage");
    }

    private void OnClickItem4Btn()
    {
        UIManager.Instance.ControlChildPages("MyProjectPage");
    }

    #endregion
}