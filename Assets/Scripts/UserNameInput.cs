using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserNameInput : MonoBehaviour
{
    public InputField INP_Name;
    public Button BTN_EnterName;
    public Text TXT_Connecting;

    void Start()
    {
        BTN_EnterName.onClick.AddListener(SubmitUserName);
        WebSocketClient.instance.OnSocketClose += UIReset;
    }

    void UIReset(UnityWebSocket.CloseEventArgs args){
        BTN_EnterName.interactable = true;
    }

    async void SubmitUserName(){
        TXT_Connecting.gameObject.SetActive(true);
        BTN_EnterName.interactable = false;
        
        await WebSocketClient.instance.DoConnectAsync();

        if(string.IsNullOrWhiteSpace(INP_Name.text)){
            Debug.Log("Name not Correct");
            return;
        }

        INP_Name.interactable = false;
        GameManager.instance.userManager.SetupUserName(INP_Name.text);
        GameManager.instance.pageManager.GoToWaitingRoom();
        BuildUserInfoToServer(INP_Name.text);

        TXT_Connecting.gameObject.SetActive(false);
        BTN_EnterName.interactable = true;
    }

    void BuildUserInfoToServer(string userName){
        
        FromClinetData data = new FromClinetData();
        data.type = FromClientDataParameter.Type_UserName;
        data.name = userName;
        string json = JsonUtility.ToJson(data, false);
        WebSocketClient.instance.SendData(json);
    }
}
