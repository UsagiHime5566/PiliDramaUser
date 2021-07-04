using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TextConnecting : MonoBehaviour
{
    public Text target;
    public string toShow;

    int count = 0;
    void Start()
    {
        if(target == null)
            target = GetComponent<Text>();

        Looping();
    }

    async void Looping(){
        while(true){
            await Task.Delay(500);

            if(this == null)
                return;
            
            target.text = toShow + " ";
            for (int i = 0; i < count; i++)
            {
                target.text += ".";
            }
            count = (count + 1 ) % 4;
        }
    }
}
