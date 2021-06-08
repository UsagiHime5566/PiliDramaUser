using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueueMessage
{
    public bool showTimeLog = false;
    public int maxQueueNum = 10;
    public Text TXT_Output;
    Queue<string> queueMessage;

    public QueueMessage(){
        queueMessage = new Queue<string>();
    }

    public QueueMessage(int count, Text target){
        queueMessage = new Queue<string>();
        maxQueueNum = count;
        TXT_Output = target;
    }
    
    void EnqueueMessage(string x){
        if(showTimeLog)
            queueMessage.Enqueue($"{x} ({System.DateTime.Now})");
        else
            queueMessage.Enqueue(x);

        if(queueMessage.Count > maxQueueNum){
            queueMessage.Dequeue();
        }

        if(TXT_Output == null)
            return;

        TXT_Output.text = "";
        foreach (var item in queueMessage)
        {
            TXT_Output.text += item + "\n";
        }
    }
}
