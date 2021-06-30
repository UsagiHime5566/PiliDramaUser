using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameEndingLayout : MonoBehaviour
{
    public Text TXT_UserName;
    void Start()
    {
        GameManager.instance.userManager.OnUserNameSetup += DoUserNameSetup;
    }

    void DoUserNameSetup(string src){
        TXT_UserName.text = src;
    }
}
