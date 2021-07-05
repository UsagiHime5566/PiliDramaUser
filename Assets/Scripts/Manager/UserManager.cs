using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HimeLib;

public class UserManager : MonoBehaviour
{
    public Text TXT_UserList;

    [Header("Assets")]
    public List<Sprite> UserIcons;
    public Sprite GetUserAssetById(int assetId) => UserIcons[UserIcons.Count % assetId];


    [Header("Host User Infos")]
    public string userName;
    public int assetId;
    public int readyAnswer;
    public int lastSendAnswer;
    public List<int> sendAnswers;



    //Delegates
    public Action<string> OnUserNameSetup;
    public Action<string> OnAnswerSend;
    public Action<UserActorData> OnUserCountChanged;


    QueueMessage qm;

    void Start()
    {
        qm = new QueueMessage(20, TXT_UserList);
        sendAnswers = new List<int>();
    }


    public void SetupUserName(string _name){
        userName = _name;
        assetId = UnityEngine.Random.Range(0, UserIcons.Count);

        OnUserNameSetup?.Invoke(_name);
        
        Debug.Log($"Setup Name to {_name}.");
    }

    
    public void SendUserAnswer(){

        lastSendAnswer = readyAnswer;
        sendAnswers.Add(readyAnswer);

        OnAnswerSend?.Invoke(readyAnswer.ToString());
        Debug.Log($"Send Answer :{readyAnswer}");

        if(!WebSocketClient.IsConnected){
            Debug.Log("Socket not ready , answer cannot be send.");
            return;
        }

        BuildUserAnswerToServer(readyAnswer);
    }

    void BuildUserAnswerToServer(int answer){
        
        FromClinetData data = new FromClinetData();
        data.type = FromClientDataParameter.Type_SendAnswer;
        data.name = userName;
        data.answer = answer.ToString();
        string json = JsonUtility.ToJson(data, false);
        WebSocketClient.instance.SendData(json);
    }



    // void OnRecieveUserAdd(){
    //     //add user
    //     ReCalcuUsers();
    // }

    // void OnUserDisconnect(){
    //     //distroy user
    //     ReCalcuUsers();
    // }

    public void ReCalcuUsers(UserActorData data){
        OnUserCountChanged?.Invoke(data);
    }

}
