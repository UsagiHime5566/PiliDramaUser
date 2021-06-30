using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameViewController : MonoBehaviour
{
    [Header("Ref UI")]
    public List<Button> answerButtons;
    public List<Image> focusBorders;
    public Text TXT_CountDown;
    public Text TXT_QuestionContent;
    public Text TXT_QuestionA;
    public Text TXT_QuestionB;
    public Text TXT_QuestionC;
    public Text TXT_QuestionD;
    public CanvasGroupExtend View_Correct;
    public CanvasGroupExtend View_Incorrect;

    [Header("Runtime Params")]
    [SerializeField] int countDown;
    [SerializeField] int QuestionAns;

    void Start()
    {
        int id = 0;
        foreach (var item in answerButtons)
        {
            int thisId = id;
            item.onClick.AddListener(delegate {
                
                foreach (var border in focusBorders)
                {
                    border.enabled = false;
                }

                focusBorders[thisId].enabled = true;
                GameManager.instance.userManager.readyAnswer = thisId;
                Debug.Log($"select :{thisId}");
            });
            id++;
        }

        GameManager.instance.OnRecieveNewQuestion += NewQuestion;
    }

    public void NewQuestion(QuestionData data){
        
        TXT_QuestionContent.text = data.content;
        TXT_QuestionA.text = data.a;
        TXT_QuestionB.text = data.b;
        TXT_QuestionC.text = data.c;
        TXT_QuestionD.text = data.d;
        
        int.TryParse(data.ans, out QuestionAns);

        foreach (var border in focusBorders)
        {
            border.enabled = false;
        }
        GameManager.instance.userManager.readyAnswer = -1;
        View_Correct.CloseSelf();
        View_Incorrect.CloseSelf();
        StartCoroutine(CountDownWork());
    }

    WaitForSeconds wait = new WaitForSeconds(1);
    IEnumerator CountDownWork(){
        countDown = GameManager.instance.QuestionAnsterCountDown;
        while(countDown > 0){
            countDown -= 1;
            TXT_CountDown.text = countDown.ToString();
            yield return wait;
        }

        QuestionEndTime();
    }

    void QuestionEndTime(){

        if(GameManager.instance.userManager.readyAnswer == QuestionAns){
            //ok
            View_Correct.OpenSelf();
            GameManager.instance.userManager.SendUserAnswer();
        }
        else {
            View_Incorrect.OpenSelf();
        }
    }
}
