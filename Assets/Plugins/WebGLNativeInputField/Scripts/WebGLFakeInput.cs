using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebGLFakeInput : MonoBehaviour
{
    public InputField INP_Target;
    public Button BTN_Source;
    public string DialogTitle = "請輸入姓名";
    public string Test_Input = "";
    void Start()
    {
        BTN_Source.onClick.AddListener(PopDialog);
    }

    void PopDialog(){
        #if UNITY_EDITOR
            INP_Target.text = Test_Input;
        #else
            INP_Target.text = WebNativeDialog.OpenNativeStringDialog(DialogTitle, INP_Target.text);
        #endif
        Debug.Log("Fake BTN Event");
    }
}
