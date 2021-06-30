using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleLayout : MonoBehaviour
{
    public Button BTN_Start;
    public CanvasGroupExtend BTN_Image;
    public Image IMG_Loading;
    public Image IMG_ReadyBar;

    public float LoadingProccess;
    public float LoadingSpeed = 0.1f;
    void Start()
    {
        BTN_Start.onClick.AddListener(() => {
            GameManager.instance.pageManager.GoToNameInput();
        });

        StartCoroutine(ProccessingLoading());
    }

    WaitForSeconds wait = new WaitForSeconds(0.1f);
    IEnumerator ProccessingLoading(){
        LoadingProccess = 0;
        while(LoadingProccess < 1){
            LoadingProccess += LoadingSpeed;
            IMG_ReadyBar.fillAmount = LoadingProccess;
            yield return wait;
        }

        IMG_Loading.gameObject.SetActive(false);
        BTN_Image.OpenSelf();
    }
}
