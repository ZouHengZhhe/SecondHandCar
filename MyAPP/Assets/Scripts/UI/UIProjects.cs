using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProjects : MonoBehaviour
{
    private Button _item1Btn;  //第一个项目的按钮
    private Button _item2Btn;  //第二个项目的按钮
    private Button _item3Btn;  //第三个项目的按钮

    private List<Button> _btnList = new List<Button>();  //项目按钮集合


    private GameObject[] _projectsArray = new GameObject[3];

    void Start()
    {
        //获得按钮
        for (int i = 0; i < this.transform.childCount; i++)
        {
            _btnList.Add(this.transform.GetChild(i).GetComponent<Button>());
        }

        for(int i = 0; i < _btnList.Count; i++)
        {
            _btnList[i].onClick.AddListener(
                delegate ()
                {
                    OnClick(i);
                }
                );
        }
        
        //项目初始化
        for (int i = 0; i < 3; i++)
        {
            _projectsArray[i] = this.transform.Find("Item" + (i + 1).ToString()).gameObject;
        }
        ShowProject(3);

    }


    #region 按钮点击事件

    public void OnClick(int i)
    {
        //显示选中的项目
        UIManager.Instance.ControlChildPages("MyProjectPage");
    }

    #endregion




    //显示几个项目
    private void ShowProject(int num)
    {
        for (int i = 0; i < _projectsArray.Length; i++)
        {
            if (i < num)
            {
                _projectsArray[i].SetActive(true);
            }
            else
            {
                _projectsArray[i].SetActive(false);
            }
        }
    }

    //各个项目显示的图片
    private void ProjectImage()
    {

    }
}
