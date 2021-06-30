using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserNameInput : MonoBehaviour
{
    public InputField INP_Name;
    public Button BTN_EnterName;

    void Start()
    {
        BTN_EnterName.onClick.AddListener(SubmitUserName);
    }

    void SubmitUserName(){
        if(!WebSocketClient.IsConnected){
            Debug.Log("Socket not ready");
            return;
        }

        if(string.IsNullOrWhiteSpace(INP_Name.text)){
            Debug.Log("Name not Correct");
            return;
        }

        INP_Name.interactable = false;
        GameManager.instance.userManager.SetupUserName(INP_Name.text);
        GameManager.instance.pageManager.GoToWaitingRoom();
    }
}
