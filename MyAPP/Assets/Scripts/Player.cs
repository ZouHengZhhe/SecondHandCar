using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player:MonoBehaviour
{
    public static Player Instance;

    public bool IsRegister;  //用户是否已经登陆
    public int ID;  //用户ID
    public string Username;
    public string Password;
    public double Balance;  //用户余额
    public int JoinProjectsNum;  //用户参加的众筹项目

    public void Awake()
    {
        Instance = this;
    }
}