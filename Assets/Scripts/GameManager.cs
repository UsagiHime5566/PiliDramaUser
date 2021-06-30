using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HimeLib;

public class GameManager : SingletonMono<GameManager>
{
    public UserManager userManager;
    public PageManager pageManager;

    [Header("Parameter")]
    public int QuestionAnsterCountDown = 30;

    public Action<QuestionData> OnRecieveNewQuestion;

    public void EnterGame(){
        pageManager.GoToGame();
    }

    public void RecieveNewQuestion(QuestionData data){
        OnRecieveNewQuestion?.Invoke(data);
    }

    public void EndingGame(){
        pageManager.GoToEnding();
    }
}
