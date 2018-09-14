using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player:MonoBehaviour
{
    public static Player Instance;

    public bool IsRegister;

    public void Awake()
    {
        Instance = this;
    }
}