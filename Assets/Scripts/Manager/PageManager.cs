using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : HimeLib.SingletonMono<PageManager>
{
    public CanvasGroupExtend PG_Title;
    public CanvasGroupExtend PG_InputName;
    public CanvasGroupExtend PG_Game;

    void Start()
    {
        GameManager.instance.userManager.OnUserNameSetup += GoToGame;
    }

    public void GoToNameInput(){
        PG_Title.CloseSelf();
    }

    public void GoToGame(string _name){
        PG_InputName.CloseSelf();
    }
}
