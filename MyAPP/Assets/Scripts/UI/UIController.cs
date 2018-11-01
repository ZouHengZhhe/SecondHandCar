using UnityEngine;

//UI界面之间的通信
public class UIController:MonoBehaviour
{
    private UIMyPage _uiMyPage;
    private UIRegister _uiRegister;

    private void Start()
    {
        _uiMyPage = GameObject.Find("AppPage").transform.Find("MyPage").GetComponent<UIMyPage>();
        _uiRegister = GameObject.Find("AppPage").transform.Find("RegisterPage").GetComponent<UIRegister>();

        _uiRegister.RegisterSuccCallback += OnRegisterSucc;
        
    }

    //登陆成功
    private void OnRegisterSucc(string username,string password)
    {
        //登陆成功，显示账号,显示余额,显示众筹项目个数
        _uiMyPage.OnRegisterSucc(username);
    }
    
}
