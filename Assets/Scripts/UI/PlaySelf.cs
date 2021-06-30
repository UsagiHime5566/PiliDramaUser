using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class PlaySelf : MonoBehaviour
{
    Animation anim;
    void Start()
    {
        anim = GetComponent<Animation>();
        anim.Play();
    }
}
