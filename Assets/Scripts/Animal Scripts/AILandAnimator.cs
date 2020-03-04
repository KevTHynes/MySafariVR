using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILandAnimator : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Idle(bool idle)
    {
        anim.SetBool("idle", idle);
    }

    public void Run(bool run)
    {
        anim.SetBool("Run", run);
    }

    public void Walk(bool walk)
    {
        anim.SetBool("Walk", walk);
    }

    public void Eat()
    {
        anim.SetTrigger("Eat");

    }

    public void Alert()
    {
        anim.SetTrigger("Alert");
    }
}
