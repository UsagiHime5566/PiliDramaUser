using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OnlineDataParameter
{
    public static string Type_WaitRoomRefresh = "WaitRoomRefresh";
    public static string Type_RecvQuestion = "RecvQuest";
    public static string Type_Ending = "Ending";
}



[System.Serializable]
public class OnlineData
{
    public string type;

    public UserActorData usersData;
    public QuestionData questionData;
}

[System.Serializable]
public class UserActorData
{
    public List<UserActor> users;
}

[System.Serializable]
public class UserActor
{
    public string name;
    public int assetId;
    public Vector2 pos;
}

[System.Serializable]
public class QuestionData
{
    public string content;
    public string a;
    public string b;
    public string c;
    public string d;
    public string ans;
}