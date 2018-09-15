using UnityEngine;
using UnityEngine.UI;

public class UIMyCrowdfunding : MonoBehaviour
{
    private Button _inBtn;      //在投中按钮
    private Button _endBtn;   //已结束按钮
    private Image _inBtnImage;
    private Image _endBtnImage;
    private Text _inBtnTxt;
    private Text _endBtnTxt;

    // Use this for initialization
    private void Start()
    {
        UiInit();
    }

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

        //初始状态为在投中
        SetStatu(true);
    }

    private void SetStatu(bool isIn)
    {
        if (isIn)
        {
            _inBtnImage.color = new Color(0.1f, 0.5f, 0.7f, 1);
            _endBtnImage.color = new Color(1, 1, 1, 1);
            _inBtnTxt.color = new Color(1, 1, 1, 1);
            _endBtnTxt.color = new Color(0, 0, 0, 1);
        }
        else
        {
            print("点击了已结束按钮");
            _inBtnImage.color = new Color(1, 1, 1, 1);
            _endBtnImage.color = new Color(0.1f, 0.5f, 0.7f, 1);
            _inBtnTxt.color = new Color(0, 0, 0, 1);
            _endBtnTxt.color = new Color(1, 1, 1, 1);
        }
    }

    #region 按钮点击事件

    private void OnClickInBtn()
    {
        SetStatu(true);
    }

    private void OnClickEndBtn()
    {
        SetStatu(false);
    }

    #endregion 按钮点击事件
}