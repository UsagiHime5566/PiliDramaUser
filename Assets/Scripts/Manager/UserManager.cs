using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HimeLib;

public class UserManager : MonoBehaviour
{
    public Text TXT_UserList;

    public string UserName;
    public int lastSendAnswer;
    public List<int> sendAnswers;



    public Action OnUsersCountChange;

    //Delegates
    public Action<string> OnUserNameSetup;
    public Action<string> OnAnswerSend;




    QueueMessage qm;

    void Start()
    {
        qm = new QueueMessage(20, TXT_UserList);
        sendAnswers = new List<int>();
    }


    public void SetupUserName(string _name){
        UserName = _name;

        OnUserNameSetup.Invoke(_name);

        //PageManager.instance.GoToGame();
        
        Debug.Log($"Setup Name to {_name}.");
    }

    
    public void SendUserAnswer(int ans){
        lastSendAnswer = ans;
        sendAnswers.Add(ans);

        OnAnswerSend.Invoke(ans.ToString());

        Debug.Log($"select :{ans}");
    }

}
