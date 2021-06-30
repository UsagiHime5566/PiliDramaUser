using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinedUser : MonoBehaviour
{
    public string UserName;
    public Text Ref_Text;
    public Image Ref_Image;
    void Start()
    {
        // image = GetComponent<Image>();
        // image.color = HimeLib.RandomValue.GetRandomColor();
    }

    public void SetupUser(string n, int assetId, Vector2 pos){
        UserName = n;
        Ref_Text.text = n;
        Ref_Image.sprite = GameManager.instance.userManager.GetUserAssetById(assetId);
        GetComponent<RectTransform>().anchoredPosition = pos;
    }
}
