using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityWebSocket;

public class WebSocketClient : MonoBehaviour
{
    public string address = "ws://127.0.0.1:4649";
    public string proto = "Echo";
    public string str = "5566";
    public byte [] bytes;

    WebSocket socket;

    void Start()
    {
        // 创建实例
        socket = new WebSocket(address);

        // 注册回调
        socket.OnOpen += OnOpen;
        socket.OnClose += OnClose;
        socket.OnMessage += OnMessage;
        socket.OnError += OnError;

        // 连接
        socket.ConnectAsync();

        // 发送数据（两种方式）
        //socket.SendAsync(str); // 发送 string 类型数据
        //socket.SendAsync(bytes); // 发送 byte[] 类型数据

        // 关闭连接
        socket.CloseAsync();
    }

    void OnOpen(object sender, OpenEventArgs args){
        Debug.Log("WebSocket opened");
    }

    void OnClose(object sender, CloseEventArgs args){
        Debug.Log("WebSocket closed");
    }

    void OnMessage(object sender, MessageEventArgs args){
        Debug.Log($"WebSocket message {args.Data}");
    }

    void OnError(object sender, ErrorEventArgs args){

    }

    [ContextMenu("Send Data ~")]
    void SendData(){
        socket.SendAsync(str);
    }
}
