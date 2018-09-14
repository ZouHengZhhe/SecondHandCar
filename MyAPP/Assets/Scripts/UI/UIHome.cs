using UnityEngine;
using UnityEngine.UI;

public class UIHome : MonoBehaviour
{
    private Button _goodProjectBtn;  //优质项目推荐按钮
    private Button _aboutUsBtn;  //关于我们按钮
    private Button _helpCenterBtn;  //帮助中心按钮

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

        //按钮添加监听函数
        _goodProjectBtn.onClick.AddListener(OnClickGoodProjectBtn);
        _aboutUsBtn.onClick.AddListener(OnClickAboutUsBtn);
        _helpCenterBtn.onClick.AddListener(OnClickHelpCenterBtn);
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

    #endregion
}