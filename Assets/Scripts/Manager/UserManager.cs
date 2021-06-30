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


    [Header("Host User Infos")]
    public string userName;
    public int assetId;
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

    
    public void SendUserAnswer(int ans){
        lastSendAnswer = ans;
        sendAnswers.Add(ans);

        OnAnswerSend.Invoke(ans.ToString());

        Debug.Log($"select :{ans}");
    }

    void OnRecieveUserAdd(){
        //add user
        ReCalcuUsers();
    }

    void OnUserDisconnect(){
        //distroy user
        ReCalcuUsers();
    }

    void ReCalcuUsers(){

        // new UserActorData data
        UserActorData temp = new UserActorData();
        OnUserCountChanged?.Invoke(temp);
    }


    public Sprite GetUserAssetById(int assetId){
        return UserIcons[UserIcons.Count % assetId];
    }

}
