using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FromClientDataParameter
{
    public static string Type_UserName = "MyName";
    public static string Type_SendAnswer = "MyAnswer";
}

[System.Serializable]
public class FromClinetData
{
    public string type;
    public string name;
    public string answer;
}