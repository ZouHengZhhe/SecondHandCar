using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProjects : MonoBehaviour
{
    private Button _item1Btn;  //第一个项目的按钮
    private Button _item2Btn;  //第二个项目的按钮
    private Button _item3Btn;  //第三个项目的按钮
    

    private GameObject[] _projectsArray=new GameObject[3];

    void Start()
    {
        //获得按钮
        _item1Btn = this.transform.Find("Item1").GetComponent<Button>();
        _item2Btn = this.transform.Find("Item2").GetComponent<Button>();
        _item3Btn = this.transform.Find("Item3").GetComponent<Button>();

        //按钮监听函数注册
        _item1Btn.onClick.AddListener(OnClickItem1Btn);
        _item2Btn.onClick.AddListener(OnClickItem2Btn);
        _item3Btn.onClick.AddListener(OnClickItem3Btn);
        //项目初始化
        for (int i = 0; i < 3; i++)
        {
            _projectsArray[i] = this.transform.Find("Item" + (i+1).ToString()).gameObject;
        }
        ShowProject(3);
        
    }


    #region 按钮点击事件

    //上一页按钮点击事件
    private void OnClickLastPageBtn()
    {

    }

    //下一页按钮点击事件
    private void OnClickNextPageBtn()
    {

    }

    //第一个项目按钮
    private void OnClickItem1Btn()
    {
        Debug.Log("点击第一个项目！");
        UIManager.Instance.ControlChildPages("MyProjectPage");
    }

    //第二个项目按钮
    private void OnClickItem2Btn()
    {
        Debug.Log("点击第二个项目！");
        UIManager.Instance.ControlChildPages("MyProjectPage");
    }

    //第三个项目按钮
    private void OnClickItem3Btn()
    {
        Debug.Log("点击第三个项目！");
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
