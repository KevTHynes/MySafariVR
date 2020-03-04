using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void TurnOnCamera()
    {
        anim.SetTrigger("On");
    }

    public void TurnOffCamera()
    {
        anim.SetTrigger("Off");
    }

    public void TurnOn(bool on)
    {
        anim.SetBool("Turn On", on);
    }

    public void TurnOff(bool off)
    {
        anim.SetBool("Turn Off", off);
    }


}
