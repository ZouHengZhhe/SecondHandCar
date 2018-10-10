using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPrintScrollRectPara : MonoBehaviour
{
    private ScrollRect rect;

    // Use this for initialization
    void Start()
    {
        rect = this.transform.GetComponent<ScrollRect>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rect.decelerationRate = 0;
            rect.content.localPosition = Vector3.zero;
            rect.decelerationRate = 0.135f;
        }
        print(rect.elasticity);
    }
}
