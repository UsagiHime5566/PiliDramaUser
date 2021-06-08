using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserSelectionInput : MonoBehaviour
{
    public List<Button> userButtons;

    void Start()
    {
        int id = 0;
        foreach (var item in userButtons)
        {
            int thisId = id;
            item.onClick.AddListener(delegate {
                
                if(!WebSocketClient.IsConnected){
                    Debug.Log("Socket not ready");
                    return;
                }

                GameManager.instance.userManager.SendUserAnswer(thisId);
            });
            id++;
        }
    }
}
