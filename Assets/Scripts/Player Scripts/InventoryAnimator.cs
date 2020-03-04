using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAnimator : MonoBehaviour
{
    private Animator inven_Anim;

    private void Awake()
    {
        inven_Anim = GetComponentInChildren<Animator>();
    }

    public void HoverNothing()
    {
        //inven_Anim.ResetTrigger("Meat");
        
    }

    public void HoverMeat(bool meat)
    {
        inven_Anim.SetBool("IsMeat" , meat);
        //inven_Anim.SetTrigger("Meat");
    }

    public void HoverFish(bool fish)
    {
        inven_Anim.SetBool("IsFish", fish);
        //inven_Anim.SetTrigger("Fish");
    }

    public void HoverCamera(bool camera)
    {
        inven_Anim.SetBool("IsCamera", camera);
        //inven_Anim.SetTrigger("Camera");
    }


}
