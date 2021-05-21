using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserSelection : MonoBehaviour
{
    public List<Button> userButtons;
    void Start()
    {
        int id = 0;
        foreach (var item in userButtons)
        {
            int thisId = id;
            item.onClick.AddListener(delegate {
                Debug.Log($"select :{thisId}");
            });
            id++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
