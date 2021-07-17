using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmuData : MonoBehaviour
{
    public FromServerData emuOnlineData;
    public FromServerData emuQuestion;

    public WebSocketClient webSocketClient;

    public Button EmuButton_UserCount;
    //public Button EmuButton_Start;
    public Button EmuButton_Question;
    public Button EmuButton_Ending;
    public Button EmuButton_Websocket;

    void Start(){
        EmuButton_UserCount.onClick.AddListener(EmuReceiveData);
        //EmuButton_Start.onClick.AddListener(EmuGameStart);
        EmuButton_Question.onClick.AddListener(EmuQuestion);
        EmuButton_Ending.onClick.AddListener(EmuEnding);
        EmuButton_Websocket.onClick.AddListener(EmuWebsocket);
    }
    
    public void EmuReceiveData(){
        webSocketClient.DoEmuRecieve(JsonUtility.ToJson(emuOnlineData, false));
    }

    // public void EmuGameStart(){
    //     OnlineData emuData = new OnlineData();
    //     emuData.type = OnlineDataParameter.Type_EnterGame;
    //     webSocketClient.DoEmuRecieve(JsonUtility.ToJson(emuData, false));
    // }

    public void EmuQuestion(){
        webSocketClient.DoEmuRecieve(JsonUtility.ToJson(emuQuestion, false));
    }

    public void EmuEnding(){
        FromServerData emuData = new FromServerData();
        emuData.type = FromServerDataParameter.Type_Ending;
        webSocketClient.DoEmuRecieve(JsonUtility.ToJson(emuData, false));
    }

    public void EmuWebsocket(){
        Application.ExternalCall("ExtraWebsocket");
    }
}
