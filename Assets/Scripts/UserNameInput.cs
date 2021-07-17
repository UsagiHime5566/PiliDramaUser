using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        WebSocketClient.instance.OnSocketOpened += OnConnected;
        WebSocketClient.instance.OnSocketClose += OnConnectFail;
    }

    void SubmitUserName(){
        if(string.IsNullOrWhiteSpace(INP_Name.text)){
            Debug.Log("Name not Correct");
            return;
        }

        TXT_Connecting.gameObject.SetActive(true);
        BTN_EnterName.interactable = false;
        
        WebSocketClient.instance.DoConnectAsync();
    }

    void OnConnectFail(UnityWebSocket.CloseEventArgs args){
        BTN_EnterName.interactable = true;
    }

    void OnConnected(UnityWebSocket.OpenEventArgs args){
        Debug.Log("Connect success!");

        INP_Name.interactable = false;
        GameManager.instance.userManager.SetupUserName(INP_Name.text);
        GameManager.instance.pageManager.GoToWaitingRoom();

        StartCoroutine(DelayToServer());
    }

    //WebGL cannot use Task.Delay
    IEnumerator DelayToServer(){
        yield return new WaitForSeconds(0.5f);

        BuildUserInfoToServer(INP_Name.text);

        TXT_Connecting.gameObject.SetActive(false);
        BTN_EnterName.interactable = true;
    }

    void BuildUserInfoToServer(string userName){
        
        FromClinetData data = new FromClinetData();
        data.type = FromClientDataParameter.Type_UserName;
        data.name = userName;
        WebSocketClient.instance.SendData(data);
    }
}
