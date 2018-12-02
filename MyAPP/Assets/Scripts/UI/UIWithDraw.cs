using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 提现界面
/// </summary>
public class UIWithDraw : MonoBehaviour
{
    private InputField _input1;  //开户名输入框
    private InputField _input2;  //开户行输入框
    private InputField _input3;  //银行卡号输入框
    private InputField _input4;  //确认卡号输入框
    private InputField _input5;  //提现金额输入框

    private Button _submitBtn;   //提交申请按钮
    private GameObject _msgPage;  //信息提示框（点击提交按钮后弹出）
    private Text _msgTxt;
    private Button _ensureBtn;

    //输入框中的信息
    private string str1;
    private string str2;
    private string str3;
    private string str4;
    private string str5;

    public delegate void WithDrawDel(RestDataWithDraw data);
    public WithDrawDel WithDrawCallback = null;

    public void Awake()
    {
        //得到5个输入框
        _input1 = this.transform.Find("Page/Input1").GetComponent<InputField>();
        _input2 = this.transform.Find("Page/Input2").GetComponent<InputField>();
        _input3 = this.transform.Find("Page/Input3").GetComponent<InputField>();
        _input4 = this.transform.Find("Page/Input4").GetComponent<InputField>();
        _input5 = this.transform.Find("Page/Input5").GetComponent<InputField>();
        _input1.onValueChanged.AddListener(OnValueChangeInput1);
        _input2.onValueChanged.AddListener(OnValueChangeInput2);
        _input3.onValueChanged.AddListener(OnValueChangeInput3);
        _input4.onValueChanged.AddListener(OnValueChangeInput4);
        _input5.onValueChanged.AddListener(OnValueChangeInput5);

        _submitBtn = this.transform.Find("Page/SubmitBtn").GetComponent<Button>();
        _submitBtn.onClick.AddListener(OnClickSubmitBtn);

        //信息提示
        _msgPage = this.transform.Find("MsgPage").gameObject;
        _msgPage.SetActive(false);
        _msgTxt = _msgPage.transform.Find("MsgTxt").GetComponent<Text>();
        _ensureBtn = _msgPage.transform.Find("EnsureBtn").GetComponent<Button>();
        _ensureBtn.onClick.AddListener(OnClickEnsureBtn);
    }
    

    private void OnValueChangeInput1(string value)
    {
        str1 = value;
    }
    private void OnValueChangeInput2(string value)
    {
        str2 = value;
    }
    private void OnValueChangeInput3(string value)
    {
        str3 = value;
    }
    private void OnValueChangeInput4(string value)
    {
        str4 = value;
    }
    private void OnValueChangeInput5(string value)
    {
        str5 = value;
    }

    private void OnClickSubmitBtn()
    {
        if(str3!=str4)
        {
            ShowMsg("两次输入的卡号不一致");
            return;
        }
        if (Int32.Parse(str5) < 100)
        {
            ShowMsg("提现金额最低100元");
            return;
        }

        RestDataWithDraw data = new RestDataWithDraw();
        data.memID = Player.Instance.ID;
        data.accountName = str1;  //开户名
        data.bankCategory = str2;  //开户行
        data.bankNum = str3;  //银行卡号
        double withdraw = double.Parse(str5);
        data.withdraw = withdraw;

        WithDrawCallback(data);
    }

    private void OnClickEnsureBtn()
    {
        //关闭信息提示框
        _msgPage.SetActive(false);
    }

    private void OnDisable()
    {
        _msgPage.SetActive(false);
    }

    public void ShowMsg(string str)
    {
        _msgPage.SetActive(true);
        _msgTxt.text = str;
    }

    

}