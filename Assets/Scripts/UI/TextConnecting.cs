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
    Coroutine coroutine;

    void Awake()
    {
        if(target == null)
            target = GetComponent<Text>();
    }

    private void OnEnable() {
        coroutine = StartCoroutine(Looping());
    }

    private void OnDisable() {
        if(coroutine != null)
            StopCoroutine(coroutine);
    }

    WaitForSeconds wait = new WaitForSeconds(0.5f);
    IEnumerator Looping(){
        while(true){
            yield return wait;
            
            target.text = toShow + " ";
            for (int i = 0; i < count; i++)
            {
                target.text += ".";
            }
            count = (count + 1 ) % 4;
        }
    }
}
