using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 该类中的变量用于加载支持页面
/// </summary>
public class UIMyProjectMsg : MonoBehaviour
{
    public static UIMyProjectMsg Instance;

    public int ProjectId;  //项目ID
    public string ProjectName;  //项目名字
    public string TargetAmount;  //集资目标
    public string RemainAmount;  //剩余金额
    public string MinAmount;  //支持单价

    private void Awake()
    {
        Instance = this;

        ProjectId = 0;
        ProjectName = "";
        TargetAmount = "";
        RemainAmount = "";
        MinAmount = "";
    }

}
