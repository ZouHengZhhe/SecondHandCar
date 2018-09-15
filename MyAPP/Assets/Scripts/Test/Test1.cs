using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test1 : MonoBehaviour
{
    public bool IsChange;
    public Image MyImage;

    // Use this for initialization
    void Start()
    {
        IsChange = false;
        MyImage = this.transform.GetComponent<Image>();
    }

    public void OnCLick()
    {
        if (IsChange)
        {
            MyImage.color=new Color(255,255,255,255);
            Debug.Log(MyImage);
            IsChange = false;
        }
        else
        {
            MyImage.color=new Color(0.1f,05f,0.7f,255);
            Debug.Log(MyImage);
            IsChange = true;
        }
    }
}
