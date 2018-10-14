using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace ThisisGame
{

    public class TestEmailUI : MonoBehaviour
    {

        private InfinityGridLayoutGroup infinityGridLayoutGroup;    //我的项目
        private InfinityGridLayoutGroup _myCrowdLayoutGroup;     //我的众筹
        private InfinityGridLayoutGroup _myPackageLayoutGroup;  //我的红包

        int _projectsAmount = 100;
        private int _myCrowdAmount = 30;
        private int _myPackageAmount = 20;
        
        void Start()
        {

            ////初始化数据列表;
            /// “我的项目”页面
            Transform ProjectsPanel = this.transform.Find("ProjectsPage");
            infinityGridLayoutGroup = ProjectsPanel.Find("Panel_Scroll/Panel_Grid").GetComponent<InfinityGridLayoutGroup>();
            infinityGridLayoutGroup.SetAmount(_projectsAmount);
            infinityGridLayoutGroup.updateChildrenCallback = UpdateChildrenCallback;

            //“我的众筹”页面（包含在筹中和已结束）
            Transform MyCrowdFundingPanel = this.transform.Find("MyCrowdfunding");
            _myCrowdLayoutGroup=MyCrowdFundingPanel.Find("Panel_Scroll/Panel_Grid").GetComponent<InfinityGridLayoutGroup>();
            _myCrowdLayoutGroup.SetAmount(_myCrowdAmount);
            _myCrowdLayoutGroup.updateChildrenCallback = UpdateMyCrowdCallback;

            //“我的红包”页面
            Transform MyPackagePanel = this.transform.Find("MyPacketPage");
            _myPackageLayoutGroup = MyPackagePanel.Find("Panel_Scroll/Panel_Grid").GetComponent<InfinityGridLayoutGroup>();
            _myPackageLayoutGroup.SetAmount(_myPackageAmount);
        }
        
        //我的项目
        void UpdateChildrenCallback(int index, Transform trans)
        {
            //页面换位置后，更新页面
            Text text = trans.Find("Text").GetComponent<Text>();
            //text.text = index.ToString();
            text.text ="测试"+ index.ToString();
        }

        //我的众筹
        void UpdateMyCrowdCallback(int index, Transform trans)
        {
            //页面换位置后，更新页面
            Text text = trans.Find("Text").GetComponent<Text>();
            //text.text = index.ToString();
            text.text = "测试" + (index+1).ToString();
        }
    }

}
