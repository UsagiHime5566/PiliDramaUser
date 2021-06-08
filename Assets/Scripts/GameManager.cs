using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HimeLib;

public class GameManager : SingletonMono<GameManager>
{
    public UserNameInput userNameInput;
    public UserManager userManager;



    // Start is called before the first frame update
    void Start()
    {
        
    }



    //等待遠端命令 , 由 Socket client 呼叫
    public void RecieveUserCounts(){

    }

    public void RecieveStay(){

    }
}
