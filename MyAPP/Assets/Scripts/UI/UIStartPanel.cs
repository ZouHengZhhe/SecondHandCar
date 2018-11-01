using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStartPanel : MonoBehaviour
{
    private Text _valueTxt;

    // Use this for initialization
    void Start()
    {
        _valueTxt = this.transform.Find("Value").GetComponent<Text>();
    }

    public void ChangeLoadValue(string value)
    {
        _valueTxt.text = value+ "%";
    }
}
