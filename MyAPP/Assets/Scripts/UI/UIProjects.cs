using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProjects : MonoBehaviour
{
    private List<Button> _btnList = new List<Button>();  //项目按钮集合

    private Transform[] _projectsArray = new Transform[5];
    private Image[] _imageArray = new Image[5];  //项目显示的图片
    private Text[] _stateTxt = new Text[5];  //项目状态显示的文字

    public delegate void UpdateProjectPageDel(int index);
    public UpdateProjectPageDel UpdateProjectPageCallback = null;

    void Start()
    {
        //获得按钮
        for (int i = 0; i < this.transform.childCount; i++)
        {
            _btnList.Add(this.transform.GetChild(i).GetComponent<Button>());
        }

        //按钮监听事件绑定
        for (int i = 0; i < _btnList.Count; i++)
        {
            Button btn = _btnList[i];
            btn.onClick.AddListener(
                delegate ()
                {
                    OnClick(btn.gameObject);
                }
                );
        }

        //项目初始化
        for (int i = 0; i < 5; i++)
        {
            _projectsArray[i] = this.transform.Find("Item" + (i + 1).ToString());
            _imageArray[i] = _projectsArray[i].Find("Img").GetComponent<Image>();
            _stateTxt[i] = _imageArray[i].transform.GetChild(0).GetChild(0).GetComponent<Text>();
        }

    }


    #region 按钮点击事件

    public void OnClick(GameObject obj)
    {
        //显示选中的项目
        UIManager.Instance.ControlChildPages("MyProjectPage");

        //加载项目界面
        UpdateProjectPageCallback(obj.GetComponent<SignIndex>().Index);

    }

    #endregion

    //各个项目显示的图片（在TestEmailUI中被调用）
    public void OnChangeProjectImage(int index,Transform trans,List<Project> projectList)
    {
        //print("index=" + index + "   trans=" + trans.name);
        trans.Find("Img").GetComponent<Image>().sprite= projectList[index].SpriteTex;   //图片赋值
        Text changeTxt = trans.Find("Img").GetChild(0).GetChild(0).GetComponent<Text>();
        LoadState(changeTxt, projectList[index].State);    //状态赋值
        
    }

    private void LoadState(Text txt, int stateCode)
    {
        switch (stateCode)
        {
            case -1:
                txt.text = "取消";
                break;
            case 1:
                txt.text = "预热";
                break;
            case 2:
                txt.text = "众筹";
                break;
            case 3:
                txt.text = "筹满";
                break;
            case 4:
                txt.text = "投票";
                break;
            case 5:
                txt.text = "出售";
                break;
            case 6:
                txt.text = "分红";
                break;
            default:
                txt.text = "众筹";
                break;
        }
    }
}
