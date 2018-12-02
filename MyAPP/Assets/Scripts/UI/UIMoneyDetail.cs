using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMoneyDetail : MonoBehaviour
{
    private Text _balanceText;

    // Use this for initialization
    void Start()
    {
        _balanceText = this.transform.Find("Balance/BalanceTxt").GetComponent<Text>();
    }

    public void UpdateBalance(double balance)
    {
        _balanceText.text = balance.ToString();
    }
    
}
