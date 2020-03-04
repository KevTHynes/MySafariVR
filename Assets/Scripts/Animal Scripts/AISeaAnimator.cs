using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISeaAnimator : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Swim(bool swim)
    {
        anim.SetBool("Swim", swim);
    }

    public void SwimFast(bool fast_Swim)
    {
        anim.SetBool("Fast Swim", fast_Swim);
    }

    public void SwimSpeed(float swim_Speed)
    {
        anim.SetFloat("swimSpeed", swim_Speed);
    }
}
