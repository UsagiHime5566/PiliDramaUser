using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingRoom : MonoBehaviour
{
    public RectTransform areaOfUsers;
    public JoinedUser Prefab_JoinedUser;
    void Start()
    {
        GameManager.instance.userManager.OnUserCountChanged += OnUserCountChanged;
    }

    public void OnUserCountChanged(UserActorData data){

        foreach (Transform child in areaOfUsers.transform) {
            GameObject.Destroy(child.gameObject);
        }

        foreach (var user in data.users)
        {
            if(user == null)
                continue;
            
            JoinedUser temp = Instantiate(Prefab_JoinedUser, areaOfUsers);
            temp.SetupUser(user.name, user.assetId, user.pos);
        }
    }
}
