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

    public System.Action<OpenEventArgs> OnSocketOpened;
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

    // public async void WebSocket()
    // {
    //     try
    //     {
    //         ClientWebSocket ws = new ClientWebSocket();
    //         CancellationToken ct = new CancellationToken();
    //         //添加header
    //         //ws.Options.SetRequestHeader("X-Token", "eyJhbGciOiJIUzI1N");
    //         Uri url = new Uri("ws://121.40.165.18:8800/v1/test/test");
    //         await ws.ConnectAsync(url, ct);
    //         await ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes("hello")), WebSocketMessageType.Binary, true, ct); //发送数据
    //         while (true)
    //         {
    //             var result = new byte[1024];
    //             await ws.ReceiveAsync(new ArraySegment<byte>(result), new CancellationToken());//接受数据
    //             var str = Encoding.UTF8.GetString(result, 0, result.Length);
    //             Debug.Log(str);
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine(ex.Message);
    //     }
    // }


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

    public void DoConnectAsync(){
        // 连接
        socket.ConnectAsync();
    }

    void OnOpen(object sender, OpenEventArgs args){
        Debug.Log($"WebSocket opened");

        OnSocketOpened?.Invoke(args);
    }

    void OnClose(object sender, CloseEventArgs args){
        Debug.Log($"WebSocket closed : {args.Reason}");

        OnSocketClose?.Invoke(args);
    }

    void OnMessage(object sender, MessageEventArgs args){
        Debug.Log($"From server : {args.Data}");

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

    public void SendData(FromClinetData clientdata){
        //socket.SendAsync(str); // 发送 string 类型数据
        //socket.SendAsync(bytes); // 发送 byte[] 类型数据
        InternetData data = new InternetData();
        data.DataType = InternetDataParameter.Type_Client;
        data.fromClinetData = clientdata;
        string json = JsonUtility.ToJson(data, false);

        socket.SendAsync(json);
    }


    void JsonPaser(string json){
        try{
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
        } catch (System.Exception e){
            Debug.LogError(e.Message.ToString());
        }
    }

    public void DoEmuRecieve(string json){
        Debug.Log($"[Fake] From server :\n{json}");
        JsonPaser(json);
    }
}
