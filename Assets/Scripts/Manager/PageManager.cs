using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : HimeLib.SingletonMono<PageManager>
{
    public bool AutoInitializePagesVisible = true;
    public CanvasGroupExtend PG_Title;
    public CanvasGroupExtend PG_InputName;
    public CanvasGroupExtend PG_WaitRoom;
    public CanvasGroupExtend PG_Game;

    void Start()
    {
        if(AutoInitializePagesVisible){
            PG_Title.OpenSelfImmediate();
            PG_InputName.OpenSelfImmediate();
            PG_Game.OpenSelfImmediate();
        }
    }

    public void GoToNameInput(){
        PG_InputName.OpenSelfImmediate();
        PG_Title.CloseSelf();
    }

    public void GoToWaitingRoom(){
        PG_WaitRoom.OpenSelfImmediate();
        PG_Title.CloseSelf();
        PG_InputName.CloseSelf();
    }

    public void GoToGame(string _name){
        PG_Game.OpenSelfImmediate();
        PG_Title.CloseSelf();
        PG_InputName.CloseSelf();
        PG_WaitRoom.CloseSelf();
    }
}
