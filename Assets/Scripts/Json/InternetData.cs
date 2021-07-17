using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class InternetDataParameter
{
    public static string Type_Client = "IsClient";
    public static string Type_Server = "IsServer";
}


[System.Serializable]
public class InternetData
{
    public string DataType;
    public FromClinetData fromClinetData;
    public FromServerData fromServerData;
}
