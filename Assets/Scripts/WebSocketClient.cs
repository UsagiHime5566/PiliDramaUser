using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityWebSocket;
using HimeLib;
using System.Threading.Tasks;

public class WebSocketClient : SingletonMono<WebSocketClient>
{
    public bool Debug_FakeSocket = false;
    public string address = "ws://127.0.0.1:4649/Echo";


    public bool autoInitialize = true;
    WebSocket socket;


    public System.Action<CloseEventArgs> OnSocketClose;

    public static bool IsConnected {
        get {
            if(instance.Debug_FakeSocket)
                return true;
            if(instance.socket == null){
                Debug.Log("WebSocket not Ready");
                return false;
            }

            return instance.socket.ReadyState == WebSocketState.Open;
        }
    }

    public void CallWebInitialize(){
        Application.ExternalCall("InitializeURL");
    }

    //Call by javascript
    public void SetupURL(string url){
        address = url;
        Debug.Log($"Setup url to : {url}");
        StartWebSocket();
    }

    void Start(){
    #if UNITY_EDITOR
        if(autoInitialize)
            StartWebSocket();
    #endif
    }

    void StartWebSocket()
    {
        // 创建实例
        socket = new WebSocket(address);

        // 注册回调
        socket.OnOpen += OnOpen;
        socket.OnClose += OnClose;
        socket.OnMessage += OnMessage;
        socket.OnError += OnError;
    }

    public async Task DoConnectAsync(){
        // 连接
        socket.ConnectAsync();

        while(socket.ReadyState != WebSocketState.Open){
            await Task.Yield();
        }
        await Task.Yield();
        
        Debug.Log("Connect success!");
    }

    void OnOpen(object sender, OpenEventArgs args){
        Debug.Log($"WebSocket opened");
    }

    void OnClose(object sender, CloseEventArgs args){
        Debug.Log($"WebSocket closed : {args.Reason}");

        OnSocketClose?.Invoke(args);
    }

    void OnMessage(object sender, MessageEventArgs args){
    #if UNITY_EDITOR
        Debug.Log($"From server : {args.Data}");
    #endif

        JsonPaser(args.Data);
    }

    void OnError(object sender, ErrorEventArgs args){
        Debug.Log($"WebSocket error : {args.Message.ToString()}");
    }

    private void OnApplicationQuit() {
        // 关闭连接
        if(socket != null)
            socket.CloseAsync();
    }

    public void SendData(string str){
        //socket.SendAsync(str); // 发送 string 类型数据
        //socket.SendAsync(bytes); // 发送 byte[] 类型数据
        socket.SendAsync(str);
    }


    void JsonPaser(string json){
        FromServerData data = JsonUtility.FromJson<FromServerData>(json);

        if(data == null)
            return;

        if(data.type == FromServerDataParameter.Type_WaitRoomRefresh){
            GameManager.instance.userManager.ReCalcuUsers(data.usersData);
        }

        if(data.type == FromServerDataParameter.Type_RecvQuestion){
            GameManager.instance.EnterGame();
            GameManager.instance.RecieveNewQuestion(data.questionData);
        }

        if(data.type == FromServerDataParameter.Type_Ending){
            GameManager.instance.EndingGame();
        }
    }

    public void DoEmuRecieve(string json){
        Debug.Log($"[Fake] From server :\n{json}");
        JsonPaser(json);
    }
}
