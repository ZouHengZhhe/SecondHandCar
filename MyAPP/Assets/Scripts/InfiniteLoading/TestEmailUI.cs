using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace ThisisGame
{

    public class TestEmailUI : MonoBehaviour
    {

        InfinityGridLayoutGroup infinityGridLayoutGroup;

        int amount = 100;
        
        void Start()
        {

            ////初始化数据列表;
            Transform ProjectsPanel = this.transform.Find("ProjectsPage");
            infinityGridLayoutGroup = ProjectsPanel.Find("Panel_Scroll/Panel_Grid").GetComponent<InfinityGridLayoutGroup>();
            infinityGridLayoutGroup.SetAmount(amount);
            infinityGridLayoutGroup.updateChildrenCallback = UpdateChildrenCallback;
        }
        
        void UpdateChildrenCallback(int index, Transform trans)
        {
            //Debug.Log("UpdateChildrenCallback: index=" + index + " name:" + trans.name);

            //页面换位置后，更新页面
            Text text = trans.Find("Text").GetComponent<Text>();
            text.text = index.ToString();
            //print("Callback");
        }
        
    }

}
