using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMoneyDetail : MonoBehaviour
{
    private Text _balanceText;
    private Button _rechargeBtn;
    private Button _withdrawBtn;
    private Image _rechargeImg;
    private Image _withdrawImg;

    // Use this for initialization
    void Start()
    {
        _balanceText = this.transform.Find("Balance/BalanceTxt").GetComponent<Text>();
        _rechargeBtn = this.transform.Find("RechargeBtn").GetComponent<Button>();
        _withdrawBtn = this.transform.Find("WithdrawBtn").GetComponent<Button>();
        _rechargeBtn.onClick.AddListener(OnClickRechargeBtn);
        _withdrawBtn.onClick.AddListener(OnClickWithdrawBtn);
        _rechargeImg = this.transform.Find("RechargeBtn").GetComponent<Image>();
        _withdrawImg = this.transform.Find("WithdrawBtn").GetComponent<Image>();
    }

    private void Update()
    {
        if(Player.Instance.IsRegister)
        {
            _rechargeBtn.enabled = true;
            _withdrawBtn.enabled = true;
            _rechargeImg.color = new Color(1, 1, 1);
            _withdrawImg.color = new Color(1, 1, 1);
        }
        else
        {
            _rechargeBtn.enabled = false;
            _withdrawBtn.enabled = false;
            _rechargeImg.color = new Color(0.6f,0.6f,0.6f);
            _withdrawImg.color = new Color(0.6f, 0.6f, 0.6f);
        }
    }

    //在RestController中被调用，点击资金明细按钮时，会触发此函数
    public void UpdateBalance(double balance)
    {
        _balanceText.text = balance.ToString();
    }

    private void OnClickRechargeBtn()
    {
        UIManager.Instance.ControlChildPages("RechargePage");
    }

    private void OnClickWithdrawBtn()
    {
        UIManager.Instance.ControlChildPages("WithDrawPage");
    }
    
}
