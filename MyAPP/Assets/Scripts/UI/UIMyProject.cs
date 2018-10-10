using UnityEngine;
using UnityEngine.UI;

public class UIMyProject : MonoBehaviour
{
    private ScrollRect rect;

    // Use this for initialization
    private void Awake()
    {
        rect = this.transform.Find("Panel").GetChild(0).GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnEnable()
    {
        print("进入我的项目!");
        LoadMyProject();
    }

    //每次进入我的项目时，进行加载（加载文字、图片等）
    private void LoadMyProject()
    {
        //rect.content.localPosition = new Vector3(0, -2843.4f, 0);
        rect.verticalNormalizedPosition = 1;
    }
}