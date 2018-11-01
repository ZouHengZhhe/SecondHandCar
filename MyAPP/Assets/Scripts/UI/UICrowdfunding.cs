using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICrowdfunding : MonoBehaviour
{
    private Button _inBtn;      //在投中按钮
    private Button _endBtn;   //已结束按钮
    private Image _inBtnImage;
    private Image _endBtnImage;
    private Text _inBtnTxt;
    private Text _endBtnTxt;
    
    
    private Transform _itemsParent;

    private List<Button> _btnList = new List<Button>();

    private TestEmailUI _testEmailUI;


    private void Start() { UiInit(); }

    //得到UI
    private void UiInit()
    {
        _inBtn = this.transform.Find("InBtn").GetComponent<Button>();
        _endBtn = this.transform.Find("EndBtn").GetComponent<Button>();
        _inBtnImage = this.transform.Find("InBtn").GetComponent<Image>();
        _endBtnImage = this.transform.Find("EndBtn").GetComponent<Image>();
        _inBtnTxt = this.transform.Find("InBtn").GetChild(0).GetComponent<Text>();
        _endBtnTxt = this.transform.Find("EndBtn").GetChild(0).GetComponent<Text>();

        //按钮点击事件注册
        _inBtn.onClick.AddListener(OnClickInBtn);
        _endBtn.onClick.AddListener(OnClickEndBtn);

        //得到所有Item,并为按钮注册点击事件
        _itemsParent = this.transform.Find("Panel_Scroll/Panel_Grid");
        for(int i = 0; i < _itemsParent.childCount; i++)
        {
            _btnList.Add(_itemsParent.GetChild(i).GetComponent<Button>());
            _btnList[i].onClick.AddListener(
                delegate ()
                {
                    OnClickItem(i);
                }
                );
        }

        _testEmailUI = this.transform.parent.GetComponent<TestEmailUI>();

        //初始状态为在投中
        SetStatu(true);

    }
   

   

    private void SetStatu(bool isIn)
    {
        if (isIn)  //在投中
        {
            _inBtnImage.color = new Color(0.1f, 0.5f, 0.7f, 1);
            _endBtnImage.color = new Color(1, 1, 1, 1);
            _inBtnTxt.color = new Color(1, 1, 1, 1);
            _endBtnTxt.color = new Color(0, 0, 0, 1);

            //加载在投中项目个数
            if (RestController.Instance.MyProjectsInList.Count==0)
            {
                _testEmailUI.InitMyProjectsIn(0);
            }
            else
            {
                _testEmailUI.InitMyProjectsIn(RestController.Instance.MyProjectsInList.Count);
            }

        }
        else  //已结束
        {
            _inBtnImage.color = new Color(1, 1, 1, 1);
            _endBtnImage.color = new Color(0.1f, 0.5f, 0.7f, 1);
            _inBtnTxt.color = new Color(0, 0, 0, 1);
            _endBtnTxt.color = new Color(1, 1, 1, 1);

            //加载已结束项目个数
            if (RestController.Instance.MyProjectsEndList.Count==0)
            {
                _testEmailUI.InitMyProjectsEnd(0);
            }
            else
            {
                _testEmailUI.InitMyProjectsEnd(RestController.Instance.MyProjectsEndList.Count);
            }
        }
    }
    

    #region 按钮点击事件

    //在投中
    private void OnClickInBtn()
    {
        SetStatu(true);
    }

    //已结束
    private void OnClickEndBtn()
    {
        SetStatu(false);
    }

    //Item按钮点击事件
    private void OnClickItem(int index)
    {
        UIManager.Instance.ControlChildPages("MyProjectPage");
    }

    #endregion 按钮点击事件
}